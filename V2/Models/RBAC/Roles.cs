using System;
using System.ComponentModel.DataAnnotations;

namespace V2.Models.RBAC
{
    public class Roles
    {

        public string RoleID { get; set; }
        [Required(ErrorMessage = "*")]
        public string Role { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        [Required(ErrorMessage = "*")]
        public bool IsVendor { get; set; }
        [Required(ErrorMessage = "*")]
        public bool IsActive { get; set; }


        [Required(ErrorMessage = "*")]
        public DateTime CreatedOn { get; set; }


        [Required(ErrorMessage = "*")]
        public DateTime LastUpdatedOn { get; set; }

        [Required(ErrorMessage = "*")]
        public int LastUpdatedBy { get; set; }

        [Required(ErrorMessage = "*")]
        public bool IsDeleted { get; set; }


    }
}
