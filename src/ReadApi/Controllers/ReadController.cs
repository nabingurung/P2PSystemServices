using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadApi.MessageBroker;
using ReadApi.Models;

namespace ReadApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadController : ControllerBase
    {

        public ReadController(ILogger<ReadController> logger, IRabbitMQClient rabbitMQClient)
        {
            Logger = logger;
            RabbitMQClient = rabbitMQClient;
            Logger = logger;
        }

        public ILogger<ReadController> Logger { get; }
        public IRabbitMQClient RabbitMQClient { get; }

        [HttpPost]
        public IActionResult Post(NewReadRequest newReadRequest)
        {

            var readRequest = new NewReadRequest
            {
                SystemId = newReadRequest.SystemId,
                Id = 1
            };
            var payload = JsonSerializer.Serialize(newReadRequest);
            Logger.LogInformation($"New order created: {payload}");

            RabbitMQClient.Publish("ordering", "order.created", payload);

            return Ok();

        }
    }
}