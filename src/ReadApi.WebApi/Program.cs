using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using ReadApi.Infrastructure;
using ReadApi.Infrastructure.Peristence.EfCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables().Build();

var connectionString = Configuration["ConnectionStrings:DbConnection"];
Console.WriteLine("*****************************************************");
Console.WriteLine(connectionString);
Console.WriteLine("*****************************************************");

// add automapper
builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.
 builder.Services.AddInfrastructureDependencies(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging( c=>
{
    c.AddConsole();
});


//// add dbcontext
//builder.Services.AddDbContext<ViolationDbContext>(
//        o => o.UseNpgsql(connectionString)
//    );


var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//        db.Database.Migrate();
//    }
   
//}

// Configure the HTTP request pipeline.
app.UseSwagger();
//app.UseSwaggerUI(sw =>
//{
//    sw.SwaggerEndpoint("/swagger/v1/swagger.json", "New P2P Reads.Api");
//});
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
