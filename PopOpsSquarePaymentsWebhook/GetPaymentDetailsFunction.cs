using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PopOpsSquarePaymentsWebhook
{
    public static class GetPaymentDetailsFunction
    {
        private static HttpClient HttpClient;

        static GetPaymentDetailsFunction()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("SquareAuthToken"));
        }

        [FunctionName("GetPaymentDetailsFunction")]
        public static async Task Run(
            [ServiceBusTrigger("paymentstopic", "getpaymentdetailssubscription", Connection = "ServiceBusConnection")]
            string paymentUpdateText,
            [CosmosDB("popops", "paymentdetails", ConnectionStringSetting = "CosmosDbConnection", CreateIfNotExists = true)]
            IAsyncCollector<Payment> paymentsCollection,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processing message: {paymentUpdateText}");
            try
            {
                var paymentUpdate = JsonConvert.DeserializeObject<PaymentUpdate>(paymentUpdateText);
                var paymentHttpResponse = await HttpClient.GetAsync($"https://connect.squareup.com/v1/{paymentUpdate.LocationId}/payments/{paymentUpdate.EntityId}");
                var paymentDetails = await paymentHttpResponse.Content.ReadAsAsync<Payment>();
                log.LogInformation($"Payment request is {paymentDetails.Id} ({paymentDetails.CreatedAt})");
                await paymentsCollection.AddAsync(paymentDetails);
                log.LogInformation("Document created successfully.");
            }
            catch (Exception e)
            {
                log.LogError(e, e.Message);
            }
        }
    }
}
