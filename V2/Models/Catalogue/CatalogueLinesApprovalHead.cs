using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V2.Models.Catalogue
{
    public class CatalogueLinesApprovalHead
    {
        public int CatalogueLinesApprovalHeadId { get; set; }
        public int CatalogueId { get; set; }
        public string StageName { get; set; }
        public int Prority { get; set; }
        public int ApproverCount { get; set; }
        public int RejecterCount { get; set; }


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
