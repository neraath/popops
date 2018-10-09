using Newtonsoft.Json;
using System;

namespace PopOps.Model
{
    public class Item
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        [JsonProperty(PropertyName = "item_variation_name")]
        public string NameVariation { get; set; }
        [JsonProperty(PropertyName = "item_detail")]
        public ItemDetails Details { get; set; }
        [JsonProperty(PropertyName = "total_money")]
        public Money TotalMoney { get; set; }
        [JsonProperty(PropertyName = "single_quantity_money")]
        public Money SingleQuantityMoney { get; set; }
    }

    public class ItemDetails
    {
        public string Sku { get; set; }
        public Guid Id { get; set; }
    }
}
