using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V2.Models.Catalogue
{
    public class CatalogueLinesApproval
    {
        public int CatalogueLinesApprovalId { get; set; }
        public int CatalogueLinesApprovalHeadId { get; set; }
        public int CatalogueLinesId { get; set; }
        public int ApprovedBy { get; set; }
        public int StatusId { get; set; }
        public string Remarks { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastupdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string UDF1 { get; set; }
        public string UDF2 { get; set; }
        public string UDF3 { get; set; }
        public string UDF4 { get; set; }
        public string UDF5 { get; set; }
    }
}
