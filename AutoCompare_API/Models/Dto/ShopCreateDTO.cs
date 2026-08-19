using System.ComponentModel.DataAnnotations;

namespace AutoCompare_API.Models.Dto
{
    public class ShopCreateDTO
    {
      
        public string? OwnerId { get; set; }

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
    }
}
