using Newtonsoft.Json;

namespace PopOps.Model
{
    public class Money
    {
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }
        [JsonProperty(PropertyName = "currency_code")]
        public string CurrencyCode { get; set; }
    }
}
