using System.ComponentModel.DataAnnotations;

namespace V2.Models.Master
{
    public class MajorCategory
    {
        public int MajorCategoryId { get; set; }
        public string MajorCategoryName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
