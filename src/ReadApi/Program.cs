using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using ReadApi.MessageBroker;
using ReadApi.Peristence;

var builder = WebApplication.CreateBuilder(args);

var rabbitHostName = Environment.GetEnvironmentVariable("RABBIT_HOSTNAME");
System.Console.WriteLine($"the username for rabbitmq is {Environment.GetEnvironmentVariable("RABBIT_USER")}");
System.Console.WriteLine($"the username for rabbitmq is {Environment.GetEnvironmentVariable("RABBIT_PASS")}");
var connectionFactory = new ConnectionFactory
{
    HostName = rabbitHostName ?? "localhost",
    Port = 5672,
    UserName = Environment.GetEnvironmentVariable("RABBIT_USER"),
    Password = Environment.GetEnvironmentVariable("RABBIT_PASS")
};

var rabbitMqConnection = connectionFactory.CreateConnection();
builder.Services.AddSingleton(rabbitMqConnection);
builder.Services.AddSingleton<IRabbitMQClient, RabbitMQClient>();

// add dbcontext
builder.Services.AddDbContext<ViolationDbContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
    );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

IHostApplicationLifetime lifetime = app.Lifetime;

lifetime.ApplicationStarted.Register(() => { });
lifetime.ApplicationStopping.Register(() =>
{
    var rabbitMqClient = app.Services.GetRequiredService<IRabbitMQClient>();
    rabbitMqClient.CloseConnection();
});

app.Run();
