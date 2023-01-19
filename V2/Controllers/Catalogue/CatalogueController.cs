using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2.Models.Catalogue;
using V2.Utility;
using V2.ViewModels.Catalogue;

namespace V2.Controllers.Catalogue
{
    public class CatalogueController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public CatalogueController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            if (!ISVENDOR) return RedirectToAction("Logout", "Login");

            List<CatalogueHeader> catalogueHeaders = new List<CatalogueHeader>();
            apiManager = new ApiManager(ServiceUrl + $"/api/Catalogue/getbyvendorid/{USERID}", AUTHTOKEN);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
                catalogueHeaders = JsonConvert.DeserializeObject<List<CatalogueHeader>>(res.Item2);
            //else if(res.Item1 != System.Net.HttpStatusCode.NotFound)
            //    toastNotification.AddErrorToastMessage(res.Item2);

            return View(catalogueHeaders);
        }

        public async Task<IActionResult> Create(int? Id)
        {
            if (!ISVENDOR) return RedirectToAction("Logout", "Login");

            ViewBag.DivisionList = await ListDivision();
            ViewBag.MajorCatList = await ListMajorCategory();
            ViewBag.SizeList = ListSize().Result.Item1;

            CatalogueHeader catalogueHeader = new CatalogueHeader();
            CatalogueLines catalogueLines = new CatalogueLines();
            if (Id != null && Id != 0)
            {
                catalogueLines.CatalogueHeaderId = Convert.ToInt32(Id);
                apiManager = new ApiManager(ServiceUrl + $"/api/Catalogue/{Id}", AUTHTOKEN);
                var res = await apiManager.Get();
                if (res.Item1 == System.Net.HttpStatusCode.OK)
                    catalogueHeader = JsonConvert.DeserializeObject<CatalogueHeader>(res.Item2);
            }

            var CatalogueUploadvm = new CatalogueUploadViewModel()
            {
                CatalogueHeaderId = catalogueHeader.CatalogueHeaderId,
                ShowControl = catalogueHeader == null ? true : catalogueHeader.Status == null ? true : catalogueHeader.Status.StatusId == 1 ? true : false,
                CatalogueLines = catalogueHeader.CatalogueLines != null ? catalogueHeader.CatalogueLines : new List<CatalogueLines>(),
                CatalogueLine = catalogueLines
            };

            return View(CatalogueUploadvm);
        }

        [HttpPost]
        public async Task<JsonResult> Create(string catalogueLines, int mode)
        {

            IFormFileCollection images = HttpContext.Request.Form.Files;
            if(mode == 0)
            {
                if(images != null && images.Count > 0) { }
                else
                    return Json(new { success = false, msg = "images not found", catalogueid = 0 });
            }

            if (!string.IsNullOrEmpty(catalogueLines))
            {
                apiManager = new ApiManager(ServiceUrl + $"/api/Catalogue", AUTHTOKEN);
                var res = await apiManager.Post(catalogueLines, images);
                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {
                    var catalogueHeader = JsonConvert.DeserializeObject<CatalogueHeader>(res.Item2);
                    return Json(new { success = true, msg = "Success", catalogueid = catalogueHeader.CatalogueHeaderId });
                }
                else
                    return Json(new { success = false, msg = res.Item2, catalogueid = 0 });
            }
            else
                return Json(new { success = false, msg = "invalid inputs", catalogueid = 0 });
        }

        public async Task<IActionResult> Detail(int catalogueHead, int lineId, bool lMainCall = false)
        {
            if (!ISVENDOR) return RedirectToAction("Logout", "Login");

            CatalogueHeader catalogueHeader = new CatalogueHeader();
            apiManager = new ApiManager(ServiceUrl + $"/api/Catalogue/{catalogueHead}", AUTHTOKEN);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
                catalogueHeader = JsonConvert.DeserializeObject<CatalogueHeader>(res.Item2);
            else
                toastNotification.AddErrorToastMessage(res.Item2);

            ViewBag.LineId = lineId;
            ViewBag.MainCall = lMainCall;
            ViewBag.SizeList = ListSize().Result.Item2;
            return View(catalogueHeader);
        }

        public async Task<IActionResult> Remove(int id, int nCatalogueHeaderId)
        {
            if (!ISVENDOR) return RedirectToAction("Logout", "Login");

            apiManager = new ApiManager(ServiceUrl + $"/api/Catalogue/DeleteCatalogueLinesById/{id}", AUTHTOKEN);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage(res.Item2);
                return RedirectToAction("Create", "Catalogue", new { id = nCatalogueHeaderId });
            }
            else
                toastNotification.AddErrorToastMessage(res.Item2);

            return View();
        }

        public async Task<IActionResult> SubmitCatalogue(int nCatHeaderId)
        {
            if (!ISVENDOR) return RedirectToAction("Logout", "Login");

            var objParm = new
            {
                CatalogueHeaderId = nCatHeaderId,
                UserId = USERID
            };

            apiManager = new ApiManager(ServiceUrl + $"/api/Approval/sendforapproval", AUTHTOKEN);
            var res = await apiManager.Post(JsonConvert.SerializeObject(objParm));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
                toastNotification.AddSuccessToastMessage("Successfully Submitted for approval");
            else
                toastNotification.AddErrorToastMessage("Failed to sent for approval");

            return RedirectToAction("Create", "Catalogue", new { id = nCatHeaderId });
        }

        public async Task<JsonResult> GetSubDivisionByDivisionId(int nDivisionId)
        {
            var subDivisionList = await ListSubDivision(nDivisionId);
            return Json(new { data = subDivisionList });
        }
    }
}
