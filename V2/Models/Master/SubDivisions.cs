using System.ComponentModel.DataAnnotations;

namespace V2.Models.Master
{
    public class SubDivisions
    {
        public int SubDivisionId { get; set; }
        [Required]
        [StringLength(100)]
        public string SubDivisionName { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}