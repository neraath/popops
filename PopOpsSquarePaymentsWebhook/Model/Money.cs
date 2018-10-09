using Newtonsoft.Json;

namespace PopOpsSquarePaymentsWebhook
{
    public class Money
    {
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }
        [JsonProperty(PropertyName = "currency_code")]
        public string CurrencyCode { get; set; }
    }
}
