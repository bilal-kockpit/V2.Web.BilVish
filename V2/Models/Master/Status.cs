using System.ComponentModel.DataAnnotations;

namespace V2.Models.Master
{
    public class Status
    {
        public int StatusId { get; set; }
        [StringLength(100)]
        [Display(Name = "Status")]
        public string StatusName { get; set; }
        public bool IsActive { get; set; }
    }
}
