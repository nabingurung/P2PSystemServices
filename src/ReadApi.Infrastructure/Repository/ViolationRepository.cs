using Dapper;
using Dapper.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using ReadApi.Application;
using ReadApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadApi.Infrastructure.Repository
{
    public class ViolationRepository : IViolationRepository
    {
        private readonly IConfiguration configuration;
        private readonly string? ConnectionString;

        private const string TABLE_NAME = "violation";
        private readonly NpgsqlConnection connection;
        private readonly ILogger<ViolationRepository> logger;
        private readonly string? CONNECTION_STRING = string.Empty;

        public ViolationRepository(IConfiguration configuration, ILogger<ViolationRepository> logger)
        {
            this.logger = logger;
            this.configuration = configuration;
                
                CONNECTION_STRING = this.configuration["ConnectionStrings:DbConnection"];
                connection = new NpgsqlConnection(CONNECTION_STRING);
                //connection.Open();
               // logger.LogInformation($"Connection open for database : connection string : {CONNECTION_STRING}");
           
            
        }
        public async Task<int> AddAsync(Violation violation)
        {
            string commandText = $"INSERT INTO {TABLE_NAME} (id, location, systemid, threshold, vehiclespeed,status,transdate) " +
                  $" VALUES (@id, @location, @systemid, @threshold, @vehiclespeed, @status, @transdate)";

            var queryArguments = new
            {
                id = violation.VioId,
                location = violation.Location,
                systemid = violation.SystemId,
                threshold = violation.ThresholdSpeed,
                vehiclespeed = violation.VehicleSpeed,
                status = violation.VioStatus,
                transdate = DateTime.UtcNow
            };

            return await connection.ExecuteAsync(commandText, queryArguments);
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Violation>> GetAllAsync()
        {
            string commandText = $"select  * from {TABLE_NAME} v inner join media m on m.vio_id = v.vio_id";

            var violationDictionary = new Dictionary<long, Violation>();


            var list = connection.Query<Violation, Media, Violation>(
                commandText,
                (violation, mediaDetail) =>
                {
                    Violation? violationEntry;

                    if (!violationDictionary.TryGetValue(violation.VioId, out violationEntry))
                    {
                        violationEntry = violation;
                        violationEntry.Medias = new List<Media>();
                        violationDictionary.Add(violationEntry.VioId, violationEntry);
                    }

                    violationEntry.Medias.Add(mediaDetail);
                    return violation;
                },
                splitOn: "vio_id")
            .Distinct()
            .ToList();

            //var violations = await connection.QueryAsync<Media, Violation, Violation>(commandText, (media, violation) =>
            //{
            //    media.Violation = violation;
            //    return violation;
            //}, splitOn: "violation_id");

           // var violations1 = await connection.QueryAsync<Media, Violation>(commandText);

            return list.ToList();
        }

        public Task<Violation> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Violation entity)
        {
            throw new NotImplementedException();
        }
    }
}
