using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Domain.Entities;
using InputModels;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DadosController : ControllerBase
    {
        private readonly ILogger<DadosController> _logger;

        public DadosController(ILogger<DadosController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderInputModel model)
        {
            Order myOrder = model;

            if (myOrder.Invalid) {
                return BadRequest("BadRequest =T");
            }
            
            string azureServiceBusConnectionString = "";
            
            await using (ServiceBusClient client = new ServiceBusClient(azureServiceBusConnectionString))
            {
                ServiceBusSender sender = client.CreateSender("main");

                string json = JsonSerializer.Serialize(myOrder);
                ServiceBusMessage serializedContents = new ServiceBusMessage(json);
                await sender.SendMessageAsync(serializedContents);
            }

            return Ok("Sucess...");
        }
    }
}