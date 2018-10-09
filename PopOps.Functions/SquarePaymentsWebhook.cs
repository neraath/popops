
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace PopOps.Functions
{
    public static class SquarePaymentsWebhook
    {
        [FunctionName("SquarePaymentsWebhook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest req,
            [ServiceBus("paymentstopic", Connection = "ServiceBusConnection", EntityType = Microsoft.Azure.WebJobs.ServiceBus.EntityType.Topic)]
            ICollector<string> outputSbMsg, ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string entityId = data?.entity_id;

                log.LogInformation($"Request body {requestBody}");
                outputSbMsg.Add(requestBody);

                return entityId != null
                    ? (ActionResult)new OkObjectResult($"New payment {entityId}")
                    : new BadRequestObjectResult("Please pass an entity id in the request body");
            }
            catch (Exception e)
            {
                log.LogError(e, $"Error occurred {e.Message}");
                throw;
            }
        }
    }
}
