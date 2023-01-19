using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V2.Models.SPResponse
{
    public class CatalogueGetForApproval
    {
        public int CatalogueHeaderId { get; set; }
        public int UserId { get; set; }
        public DateTime UploadDate { get; set; }
        public int TotalProduct { get; set; }
        public Decimal TotalAmount { get; set; }
        public int HeaderStatusId { get; set; }
        public int CatalogueLinesId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int NoOfPiece { get; set; }
        public Decimal PricePerPiece { get; set; }
        public string MajorCategoryName { get; set; }
        public string DivisionName { get; set; }
        public string SubDivisionName { get; set; }
        public int NoOfPrint { get; set; }
        public int NoOfSize { get; set; }
        public int NoOfColor { get; set; }
        public string VendorDesignNo { get; set; }
        public string Yarn { get; set; }
        public string Weave1 { get; set; }
        public int AvailableQty { get; set; }
        public decimal NetRate { get; set; }
        public int LineStatusId { get; set; }
        public int CatalogueLineImagesId { get; set; }
        public string ProductImage { get; set; }
        public int CatalogueLinesApprovalId { get; set; }
    }
}
