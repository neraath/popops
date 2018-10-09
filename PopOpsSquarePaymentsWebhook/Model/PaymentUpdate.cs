using Newtonsoft.Json;

namespace PopOpsSquarePaymentsWebhook
{
    public class PaymentUpdate
    {
        [JsonProperty(PropertyName = "entity_id")]
        public string EntityId { get; set; }
        [JsonProperty(PropertyName = "merchant_id")]
        public string MerchantId { get; set; }
        [JsonProperty(PropertyName = "location_id")]
        public string LocationId { get; set; }
    }
}
