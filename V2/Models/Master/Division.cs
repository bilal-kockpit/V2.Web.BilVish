using NToastNotify;
using System.ComponentModel.DataAnnotations;

namespace V2.Models.Master
{
    public class Division
    {
        public int DivisionId { get; set; }
        [Required]
        [StringLength(100)]
        public string DivisionName { get; set; }
        [StringLength(300)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
