using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReadApi.DTOModels;
using ReadApi.MessageBroker;
using ReadApi.Models;
using ReadApi.Peristence;

namespace ReadApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadController : ControllerBase
    {
        private readonly IDataAccessProvider dataAccessProvider;
        private readonly IMapper mapper;

        public ReadController(ILogger<ReadController> logger, IRabbitMQClient rabbitMQClient, IDataAccessProvider dataAccessProvider, IMapper mapper)
        {
            Logger = logger;
            RabbitMQClient = rabbitMQClient;
            this.dataAccessProvider = dataAccessProvider;
            this.mapper = mapper;
            Logger = logger;
        }

        public ILogger<ReadController> Logger { get; }
        public IRabbitMQClient RabbitMQClient { get; }

        [HttpPost]
        public async Task<IActionResult> Post(DtoViolation newReadRequest)
        {

           var violation = mapper.Map<Violation>(newReadRequest);

            await dataAccessProvider.AddNewViolationAsync(violation);
            var payload = JsonSerializer.Serialize(newReadRequest);
            Logger.LogInformation($"New read created: {payload}");

           // RabbitMQClient.Publish("", "newreads", payload);

            return Ok();

        }
    }
}