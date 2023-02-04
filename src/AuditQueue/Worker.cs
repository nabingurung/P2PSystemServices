using AuditQueue.Models;
using AuditQueue.Repo;
using AuditQueue.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Data.Common;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace AuditQueue;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration configuration;
    private readonly IViolationRepo violationRepo;
    private readonly IALPRData aLPRData;
    private ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;
    private const string QueueName = "newreads";
    public Worker(ILogger<Worker> logger, IConfiguration configuration, IViolationRepo violationRepo
        )
    {
        _logger = logger;
        this.configuration = configuration;
        this.violationRepo = violationRepo;        
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        
        var rabbitHostName = Environment.GetEnvironmentVariable("RABBIT_HOSTNAME");
        _connectionFactory = new ConnectionFactory
        {
            // make sure to run your rabbitmq container
            HostName = rabbitHostName ?? "host.docker.internal",
            Port = 5672,
            UserName = Environment.GetEnvironmentVariable("RABBIT_USER") ?? "myuser",
            Password = Environment.GetEnvironmentVariable("RABBIT_PASS") ?? "mypassworld",
            DispatchConsumersAsync = true
        };
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclarePassive(QueueName);
        _channel.BasicQos(0, 1, false);
        _logger.LogInformation($"Queue [{QueueName}] is waiting for messages.");
        


        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        stoppingToken.ThrowIfCancellationRequested();

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            try
            {
                var messageCount = _channel.MessageCount(QueueName);
                if (messageCount > 0)
                {
                    _logger.LogInformation($"\tDetected {messageCount} message(s).");
                }

                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.Received += async (bc, ea) =>
                {
                    if (ea.BasicProperties.UserId != Environment.GetEnvironmentVariable("RABBIT_USER"))
                    {
                        _logger.LogInformation($"\tIgnored a message sent by [{ea.BasicProperties.UserId}].");
                       // return;
                    }

                    var t = DateTimeOffset.FromUnixTimeMilliseconds(ea.BasicProperties.Timestamp.UnixTime);
                    _logger.LogInformation($"{t.LocalDateTime:O} ID=[{ea.BasicProperties.MessageId}]");
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    _logger.LogInformation($"Processing msg: '{message}'.");

                    try
                    {
                        var newRead = JsonSerializer.Deserialize<NewReadRequest>(message);
                        _logger.LogInformation($"Creating new read #{newRead?.Id} to [{newRead?.SystemId}].");
                        await violationRepo.UpdateAsync(newRead.Id);
                        await violationRepo.GetAsync(newRead.Id);
                        await violationRepo.GetAllAsync();
                        await Task.Delay(new Random().Next(1, 3) * 1000, stoppingToken);

                        _logger.LogInformation($"NewRead #{newRead?.Id} created successfully");
                        _channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (JsonException)
                    {
                        _logger.LogError($"JSON Parse Error: '{message}'.");
                        _channel.BasicNack(ea.DeliveryTag, false, false);
                    }
                    catch (AlreadyClosedException)
                    {
                        _logger.LogInformation("RabbitMQ is closed!");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(default, e, e.Message);
                    }
                };

                _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }
            catch(RabbitMQClientException e)
            {
                _logger.LogError(default, e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(default, e, e.Message);
            }
            await Task.CompletedTask;

        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
        _logger.LogInformation("RabbitMQ connection is closed.");
    }
}
