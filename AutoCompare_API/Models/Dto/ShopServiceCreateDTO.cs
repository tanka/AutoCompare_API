using System.ComponentModel.DataAnnotations;

namespace AutoCompare_API.Models.Dto
{
    public class ShopServiceCreateDTO
    {
        [Required]
        public int shopId { get; set; }

        [Required]
        public int masterServiceId { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        public double price { get; set; }

        [Required]
        public string priceType { get; set; } = string.Empty;

        public int durationMin { get; set; }
        public int etaMin { get; set; }
        public bool isAtShop { get; set; }
        public bool isMobile { get; set; }
        public bool isRoadside { get; set; }
        public bool active { get; set; } = true;
        public int sortOrder { get; set; }
    }
}
