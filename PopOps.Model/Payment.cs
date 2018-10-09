using Newtonsoft.Json;
using System;

namespace PopOps.Model
{
    public class Payment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "total_collected_money")]
        public Money TotalCollectedMoney { get; set; }
        [JsonProperty(PropertyName = "tender")]
        public Tender[] Tender { get; set; }
        public Item[] Itemizations { get; set; }
    }

    public class Tender
    {
        public string Type { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = "total_money")]
        public Money TotalMoney { get; set; }
    }
}
