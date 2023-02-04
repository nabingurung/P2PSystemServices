using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ReadApi.Application;
using ReadApi.Application.Contracts;
using ReadApi.Application.Interfaces.EfCore;
using ReadApi.Application.Services;
using ReadApi.Core.Entities;
using ReadApi.Core.Services;
using ReadApi.Infrastructure.MessageBroker;
using ReadApi.WebApi.Models;
using System.Text.Json;

namespace ReadApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViolationController : ControllerBase
    {
        private readonly ILogger<ViolationController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUnitOfWorkCore unitOfWorkCore;
        private readonly IMapper mapper;
        private readonly IRabbitMQClient rabbitMQClient;
        private readonly IViolationService violationService;

        public ViolationController(ILogger<ViolationController> logger, IUnitOfWork unitOfWork, 
            IUnitOfWorkCore unitOfWorkCore, IMapper mapper , IRabbitMQClient rabbitMQClient, IViolationService violationService)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.unitOfWorkCore = unitOfWorkCore;
            this.mapper = mapper;
            this.rabbitMQClient = rabbitMQClient;
            this.violationService = violationService;
            logger.LogInformation("beging logging");
        }

        [HttpGet]
        public async Task<ApiResponse<List<DtoViolation>>> GetAll()
        {
          logger.LogInformation("Gettting data from violationRepo",nameof(GetAll));
            var result = new ApiResponse<List<DtoViolation>>();
            try
            {
                 var data = unitOfWorkCore.ViolationRepositoryCore.GetViolations();
                // var data3 = unitOfWorkCore.MediaRepositoryCore.GetAll();
             //   var data = await unitOfWork.ViolationRepository.GetAllAsync();           

                result.Data = mapper.Map<List<DtoViolation>>(data);
                result.Success = true;
                result.Message = "success";// JsonSerializer.Serialize(data2);
            }
            catch (NpgsqlException ex)
            {
                result.Success = false;
                result.Message = $"Postgres exception occured: Exception is {ex.Message} ";
                logger.LogError("Postgres exception ", ex.Message);
            } 
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Exception occured: Exception is {ex.Message} ";
                logger.LogError("Exception ", ex.Message);
            }          


            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Add(DtoEvent newReadRequest)
        {
           // to do add the validator in the future... 
            
            var violation = mapper.Map<Violation>(newReadRequest);

            // discuss the time take .. assuming your are getting in seconds
            violation.TimeTaken = 7200 / 3600; // convert it to hours 
            violation.LicenseState = "UNK";
            violation.Violationdate= DateTime.UtcNow;
            violation.MetricUnitSystem = "km/hr";

            

            violation.VehicleSpeed = unitOfWorkCore
                .CalculateVehicleSpeed(violation.TotalDistanceTravelled,violation.TimeTaken);

            logger.LogInformation($"time taken = {violation.TimeTaken}");
            logger.LogInformation($"vehicle speed  = {violation.VehicleSpeed}");

            violation.Medias = new List<Media>();

            Media media = new()
            {
                MediaStatus = 0,
                ImageId = newReadRequest.id,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow
            };

            Media media2 = new()
            {
                MediaStatus = 0,
                ImageId = newReadRequest.id,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow
            };

            violation.Medias.Add(media);
            violation.Medias.Add(media2);
              
            

            unitOfWorkCore.ViolationRepositoryCore.Add(violation);
            await unitOfWorkCore.CompleteAsync();

            newReadRequest.insertedId = violation.VioId;
            var payload = JsonSerializer.Serialize(newReadRequest);
            logger.LogInformation($"New read created: {payload}");



            return Ok();

            //rabbitMQClient.Publish("", "newreads", payload);

            //return Ok(new ApiResponse<DtoNewViolationInserted>()
            //{
            //    Success = true,
            //    Data = new DtoNewViolationInserted { InsertedId = violation.VioId },
            //    Message = "Inserted"
            //});
        }
    }
}
