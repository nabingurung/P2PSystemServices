using Amazon.S3;
using AuditQueue;
using AuditQueue.Persistence;
using AuditQueue.Repo;
using AuditQueue.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
      .ConfigureAppConfiguration((hostContext,configurationBuilder) =>
      {
          configurationBuilder.Sources.Clear();

          IHostEnvironment env = hostContext.HostingEnvironment;
          configurationBuilder
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables() ;

      })
    .ConfigureServices((hostContext, services) =>
    {

        services.AddHostedService<Worker>();
        services.AddSingleton<IViolationRepo, ViolationRepo>();

        //services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();
      //  services.AddScoped<IHttpService, HttpService>();
       services.AddScoped<IALPRData, ALPRData>();

        services.AddHttpClient();
        //// get the configuration 
        IConfiguration configuration = hostContext.Configuration;
        
        services.AddDbContext<ViolationDbContext>(
            optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(configuration["ConnectionStrings:DbConnection"]);
            });

        var connectionString = configuration["ConnectionStrings:DbConnection"];
        Console.WriteLine("*********************************");
        Console.WriteLine($"The Worker connections strings is {connectionString}");
        Console.WriteLine("**********************************");

        /**** AWS 
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.AddAWSService<IAmazonS3>();
        **/

    })
   
.Build();


//// migrate the database
//using (var scope = host.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ViolationDbContext>(); 
//    db.Database.Migrate();
//}

host.Run();
