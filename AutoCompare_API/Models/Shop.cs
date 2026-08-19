using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCompare_API.Models
{
    [Table("shops")]
    public class Shop
    {
        [Key]
        public int Id { get; set; }

        // OwnerId is a FK relation to shop_users table
        public string? OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Province { get; set; } = string.Empty;

        [Required]
        public string PostalCode { get; set; } = string.Empty;

        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsOpen { get; set; }
        public string WorkHours { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        //public List<ShopUser> ShopUsers { get; set; } = new();  // implies shopId is in many ShopUser records
        //public List<ShopService> ShopServices { get; set; } = new();
        //public List<ShopBooking> ShopBookings { get; set; } = new();
        //public List<ShopSubscription> ShopSubscriptions { get; set; } = new();
    }

}
