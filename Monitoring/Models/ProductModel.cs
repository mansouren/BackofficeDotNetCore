using System.Text.Json.Serialization;

namespace Monitoring.Models
{
    public class ProductModel
    {
        [JsonPropertyName("description")]
        public string Name { get; set; }

        [JsonPropertyName("stock")]
        public double Stock { get; set; }
    }
}
