using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2.Models.Master;

namespace V2.Models.Catalogue
{
    public class CatalogueHeader
    {
        public int CatalogueHeaderId { get; set; }
        public int UserId { get; set; }
        public DateTime UploadDate { get; set; }
        public int TotalProduct { get; set; }
        public decimal TotalAmount { get; set; }

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
        public List<CatalogueLines> CatalogueLines { get; set; }
        public List<CatalogueLinesApprovalHead> CatalogueLinesApprovalHeads { get; set; }
        public Status Status { get; set; }
    }
}
