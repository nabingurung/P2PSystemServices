using RabbitMQ.Client;
using ReadApi.MessageBroker;

var builder = WebApplication.CreateBuilder(args);

var rabbitHostName = Environment.GetEnvironmentVariable("RABBIT_HOSTNAME");
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

app.Run();
