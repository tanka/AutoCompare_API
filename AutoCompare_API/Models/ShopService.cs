using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCompare_API.Models
{
    [Table("shop_services")]
    public class ShopService
    {
        [Key]
        public int shopServiceId { get; set; }

        // shopId is a FK relation to shops table
        public int shopId { get; set; }
        public Shop? Shop { get; set; }

        // masterServiceId is a FK relation to master_services table
        public int masterServiceId { get; set; }
        public MasterService? MasterService { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        public double price { get; set; }

        [Required]
        public string priceType { get; set; } = string.Empty;

        public int durationMin { get; set; }
        public int etaMin { get; set; }  // estimated time of arival
        public bool isAtShop { get; set; } 
        public bool isMobile { get; set; }
        public bool isRoadside { get; set; }
        public bool active { get; set; }  // open or close
        public int sortOrder { get; set; }  // assume sort order

        //public List<ShopBooking> shopBookings { get; set; } = new();
    }

}
