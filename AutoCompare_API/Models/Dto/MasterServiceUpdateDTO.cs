using System.ComponentModel.DataAnnotations;

namespace AutoCompare_API.Models.Dto
{
    public class MasterServiceUpdateDTO
    {
        [Required]
        public int masterServiceId { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        //[Required]
        public string serviceType { get; set; } = string.Empty;

        //[Required]
        public string category { get; set; } = string.Empty;

        public string icon { get; set; } = string.Empty;
        public bool active { get; set; }
    }
}
