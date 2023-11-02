using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Domain.Entities;
using InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DadosController : ControllerBase
    {
        private readonly ILogger<DadosController> _logger;
        private readonly CosmosClient _dbClient;

        private readonly string _azureServiceBusConnectionString;

        public DadosController(
            ILogger<DadosController> logger,
            CosmosClient dbClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _dbClient = dbClient;
            _azureServiceBusConnectionString = configuration["AzureServiceBus:ConnectionString"];
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderInputModel model)
        {
            try
            {
                Order myOrder = model;

                if (myOrder.Invalid) {
                    return BadRequest("BadRequest =T");
                }
                
                // await using (ServiceBusClient client = new ServiceBusClient(_azureServiceBusConnectionString))
                // {
                //     ServiceBusSender sender = client.CreateSender("main");

                //     string json = JsonSerializer.Serialize(myOrder);
                //     ServiceBusMessage serializedContents = new ServiceBusMessage(json);
                //     await sender.SendMessageAsync(serializedContents);
                // }

                Database db = await _dbClient.CreateDatabaseIfNotExistsAsync("");
                Container container = await db.CreateContainerIfNotExistsAsync("","");// TODO
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Post. Return: {msg}", ex.Message);
            }

            return Ok("Sucess...");
        }
    }
}