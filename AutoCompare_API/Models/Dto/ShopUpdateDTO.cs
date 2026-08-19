using System.ComponentModel.DataAnnotations;

namespace AutoCompare_API.Models.Dto
{
    public class ShopUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? OwnerId { get; set; }

        
        public string Name { get; set; } = string.Empty;

       
        public string Category { get; set; } = string.Empty;

       
        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Province { get; set; } = string.Empty;

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
