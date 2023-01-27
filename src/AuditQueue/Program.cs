using AuditQueue;
using AuditQueue.Persistence;
using AuditQueue.Repo;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
     
        services.AddHostedService<Worker>();
        services.AddSingleton<IViolationRepo, ViolationRepo>();
        
        //// get the configuration 
        IConfiguration configuration = hostContext.Configuration;
        services.AddDbContext<ViolationDbContext>(
            optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            });
    })
.Build();
host.Run();
