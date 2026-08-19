using System.ComponentModel.DataAnnotations;

namespace AutoCompare_API.Data
{
    public class RegisterRequestDTO
    {
        [Required]
        public string name { get; set; } = string.Empty;
        [Required]
        public string password { get; set; }    = string.Empty;
        [Required]
        public string email { get; set; } = string.Empty;

        public string role { get; set; } = string.Empty;
    }
}
