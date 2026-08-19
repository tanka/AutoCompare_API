using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCompare_API.Models
{
    [Table("master_services")]
    public class MasterService
    {
        [Key]
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
