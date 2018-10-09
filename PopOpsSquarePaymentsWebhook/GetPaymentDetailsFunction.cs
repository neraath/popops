using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PopOpsSquarePaymentsWebhook
{
    public static class GetPaymentDetailsFunction
    {
        [FunctionName("GetPaymentDetailsFunction")]
        public static void Run(
            [ServiceBusTrigger("paymentstopic", "getpaymentdetailssubscription", Connection = "ServiceBusConnection")]
            string paymentUpdateText, 
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processing message: {paymentUpdateText}");
            var paymentUpdate = JsonConvert.DeserializeObject<PaymentUpdate>(paymentUpdateText);
            // TODO: Invoke HTTP Request to get payment details.
            var paymentResponse = System.IO.File.ReadAllText("SamplePayment.json");
            var paymentDetails = JsonConvert.DeserializeObject<Payment>(paymentResponse);
            log.LogInformation($"Payment request is {paymentDetails.Id} ({paymentDetails.CreatedAt}");
        }
    }
}
