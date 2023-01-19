using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2.Models.Master;

namespace V2.Models.Catalogue
{
    public class CatalogueLines
    {
        public int CatalogueLinesId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int NoOfPiece { get; set; }
        public decimal PricePerPiece { get; set; }
        public int MajorCategoryId { get; set; }
        public int DivisionId { get; set; }
        public int SubDivisionId { get; set; }
        public int NoOfPrint { get; set; }
        public int NoOfSize { get; set; }
        public int NoOfColor { get; set; }
        public string VendorDesignNo { get; set; }
        public string Yarn { get; set; }
        public string Weave1 { get; set; }
        public int AvailableQty { get; set; }
        public decimal NetRate { get; set; }
        public int CatalogueHeaderId { get; set; }


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

        public List<CatalogueLineImages> CatalogueLineImages { get; set; }

        public List<CatalogueLinesApproval> CatalogueLinesApprovals { get; set; }

        public Division Division { get; set; }
        public SubDivisions SubDivision { get; set; }
        public MajorCategory MajorCategory { get; set; }
        public Status Status { get; set; }
    }
}
