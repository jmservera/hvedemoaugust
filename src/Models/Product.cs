using System.Text.Json.Serialization;

namespace ZavaStorefront.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the available stock for the static demo catalog (synthetic seed data, not warehouse data).
        /// Never serialized into the session cart, so a stored snapshot cannot carry stock; the current catalog
        /// is always the source of truth.
        /// </summary>
        [JsonIgnore]
        public int AvailableQuantity { get; set; }
    }
}
