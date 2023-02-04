using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadApi.Application;
using ReadApi.Infrastructure.Peristence.EfCore;
using ReadApi.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using ReadApi.Application.Interfaces.EfCore;
using ReadApi.Infrastructure.Repository.EfCore;
using RabbitMQ.Client;
using ReadApi.Core.Services;
using ReadApi.Infrastructure.MessageBroker;

namespace ReadApi.Infrastructure
{
    public static class ServiceRegistration
    {
     
        public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterStorage(services, configuration);
            RegisterServices(services, configuration);
            RegisterMessagingQueue(services,configuration);
        }

        private static void RegisterMessagingQueue(IServiceCollection services, IConfiguration configuration)
        {
            var rabbitHostName = Environment.GetEnvironmentVariable("RABBIT_HOSTNAME");
            Console.WriteLine($"the username for rabbitmq is {Environment.GetEnvironmentVariable("RABBIT_USER")}");
            Console.WriteLine($"the username for rabbitmq is {Environment.GetEnvironmentVariable("RABBIT_PASS")}");         

            var connectionString = configuration["ConnectionStrings:DbConnection"];
            Console.WriteLine("*****************************************************");
            Console.WriteLine(connectionString);
            Console.WriteLine("*****************************************************");

            var connectionFactory = new ConnectionFactory
            {
                HostName = rabbitHostName ?? "localhost",
                Port = 5672,
                UserName = Environment.GetEnvironmentVariable("RABBIT_USER") ?? "myuser",
                Password = Environment.GetEnvironmentVariable("RABBIT_PASS") ?? "mypassworld"
            };

           var rabbitMqConnection = connectionFactory.CreateConnection();
            services.AddSingleton(rabbitMqConnection);
            services.AddSingleton<IRabbitMQClient, RabbitMQClient>();
        }

        public static void ApplyDatabaseMigration(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }

        private static void RegisterStorage(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>            
              options.UseNpgsql(configuration["ConnectionStrings:DbConnection"])
             // .UseLazyLoadingProxies()
              .UseSnakeCaseNamingConvention()
              );
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            //services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>()!);
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IViolationRepository, ViolationRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // ef core repositories
            services.AddTransient(typeof(IGenericRepositoryCore<>), typeof(GenericRepositoryCore<>));
            services.AddTransient<IViolationRepositoryCore, ViolationRepositoryCore>();
            services.AddTransient<IUnitOfWorkCore, UnitOfWorkCore>();
        }
    }
}
