using Newtonsoft.Json;
using System;

namespace PopOpsSquarePaymentsWebhook
{
    public class Payment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "total_collected_money")]
        public Money TotalCollectedMoney { get; set; }
    }
}
