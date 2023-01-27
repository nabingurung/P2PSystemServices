using System;
using AuditQueue.DbModels;
using Dapper;
using Npgsql;

namespace AuditQueue.Repo
{
	public class ViolationRepo : IViolationRepo
    {
        private const string TABLE_NAME = "violation";
        private readonly NpgsqlConnection connection;
        private readonly ILogger<ViolationRepo> logger;
        private const string CONNECTION_STRING = "Host=localhost:5455;" +
                   "Username=postgresUser;" +
                   "Password=postgresPW;" +
                   "Database=postgresDB";

        public ViolationRepo(ILogger<ViolationRepo> logger)
		{
            connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();
            this.logger = logger;
            logger.LogInformation($"Connection open for Database");
        }

        public async Task<int> AddAsync(Violation violation)
        {
            string commandText = $"INSERT INTO {TABLE_NAME} (id, location, systemid, threshold, vehiclespeed,status,transdate) " +
                $" VALUES (@id, @location, @systemid, @threshold, @vehiclespeed, @status, @transdate)";

            var queryArguments = new
            {
                id = violation.Id,
                location = violation.Location,
                systemid = violation.SystemId,
                threshold = violation.ThresholdSpeed,
                vehiclespeed = violation.VehicleSpeed,
                status = violation.Status,
                transdate = DateTime.UtcNow
            };

           return await connection.ExecuteAsync(commandText, queryArguments);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string commandText = $"DELETE FROM {TABLE_NAME} WHERE ID=(@p)";

            var queryArguments = new
            {
                p = id
            };

            return await connection.ExecuteAsync(commandText, queryArguments);
        }

        public async Task<Violation> GetAsync(int id)
        {
            string commandText = $"SELECT * FROM {TABLE_NAME} WHERE ID = @id";

            var queryArgs = new { Id = id };
            var violation = await connection.QueryFirstAsync<Violation>(commandText, queryArgs);
            return violation;
        }

        public async Task<IEnumerable<Violation>> GetAllAsync()
        {
            string commandText = $"SELECT * FROM {TABLE_NAME}";
            var violations = await connection.QueryAsync<Violation>(commandText);

            return violations;
        }

       
    }
}

