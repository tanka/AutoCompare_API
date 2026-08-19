using System.ComponentModel.DataAnnotations;

namespace AutoCompare_API.Models.Dto
{
    public class MasterServiceCreateDTO
    {
        [Required]
        public string name { get; set; } = string.Empty;

    
        public string serviceType { get; set; } = string.Empty;

       
        public string category { get; set; } = string.Empty;

        public string icon { get; set; } = string.Empty;
        public bool active { get; set; } = true;
    }
}
