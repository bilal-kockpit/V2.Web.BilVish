using System.ComponentModel.DataAnnotations;

namespace V2.Models.Master
{
    public class Size
    {
        public string SizeId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Size")]
        public string SizeName { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
