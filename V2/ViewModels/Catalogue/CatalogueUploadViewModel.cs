using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2.Models.Catalogue;

namespace V2.ViewModels.Catalogue
{
    public class CatalogueUploadViewModel
    {
        public bool ShowControl { get; set; }
        public int CatalogueHeaderId { get; set; }
        public List<CatalogueLines> CatalogueLines { get; set; }
        public CatalogueLines CatalogueLine { get; set; }
    }
}
