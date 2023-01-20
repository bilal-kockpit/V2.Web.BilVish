using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2.Models.Account;
using V2.Models.Catalogue;
using V2.Models.Master;
using V2.Models.SPResponse;
using V2.Utility;

namespace V2.Controllers.Catalogue
{
    public class CatalogueApprovalController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public CatalogueApprovalController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index(int nCId = 0)
            {
            if (!ISADMIN) return RedirectToAction("Logout", "Login");

            List<VendorGetForApproval> vendorGetForApprovals = new List<VendorGetForApproval>();
            apiManager = new ApiManager(ServiceUrl + $"/api/Approval/getVendorForAppr/{USERID}", AUTHTOKEN);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK) {
                vendorGetForApprovals = JsonConvert.DeserializeObject<List<VendorGetForApproval>>(res.Item2);

                vendorGetForApprovals = vendorGetForApprovals.GroupBy(c => new
                {
                    c.CityId,
                    c.EmailId,
                    c.GSTIN,
                    c.Mobile,
                    c.StateName,
                    c.StatusId,                    
                    c.UserId,
                    c.UserName
                }).Select(o => o.FirstOrDefault()).ToList();
               // TempData["VendorDetails"] = vendorGetForApprovals;
            }
            else
                toastNotification.AddErrorToastMessage(res.Item2);

            ViewBag.City = await ListCity();

            if (nCId != 0)
            {
                vendorGetForApprovals = vendorGetForApprovals.Where(c => c.CityId == nCId).ToList();
            }

            //TempData["vendorname"] = vendorGetForApprovals;
            

            ViewBag.SelectedCityId = (nCId == 0) ? "" : nCId.ToString();
            return View(vendorGetForApprovals);
        }

        public async Task<IActionResult> Catalogues(int id)
        {
            if (!ISADMIN) return RedirectToAction("Logout", "Login");
            List<CatalogueHeader> catalogueHeaders1 = new List<CatalogueHeader>();

            List<CatalogueGetForApproval> catalogueGetForApprovals = new List<CatalogueGetForApproval>();
            //code to run API
            var objParm = new
            {
                EmployeeId = USERID,
                VendorId = id
            };
            apiManager = new ApiManager(ServiceUrl + $"/api/Approval/getforapproval", AUTHTOKEN);
            var res = await apiManager.Post(JsonConvert.SerializeObject(objParm));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                catalogueGetForApprovals = JsonConvert.DeserializeObject<List<CatalogueGetForApproval>>(res.Item2);
                var catalogueHeaders = catalogueGetForApprovals
                                         .Select(x => new CatalogueHeader
                                         {
                                             CatalogueHeaderId = x.CatalogueHeaderId,
                                             UserId = x.UserId,
                                             UploadDate = x.UploadDate,
                                             TotalProduct = x.TotalProduct,
                                             TotalAmount = x.TotalAmount,
                                             Status = new Models.Master.Status
                                             {
                                                 StatusId = x.HeaderStatusId
                                             }
                                         }).ToList().Distinct();

                catalogueHeaders = catalogueHeaders
                    .GroupBy(c => new { c.CatalogueHeaderId, c.UserId, c.UploadDate, c.TotalProduct, c.TotalAmount })
                    .Select(o => o.FirstOrDefault());

                foreach (var catHead in catalogueHeaders.Distinct())
                {
                    var obj = new CatalogueHeader();
                    obj = catHead;
                    var catalogueLines = catalogueGetForApprovals.Where(c => c.CatalogueHeaderId == catHead.CatalogueHeaderId)
                        .Select(x => new CatalogueLines
                        {
                            CatalogueLinesId = x.CatalogueLinesId,
                            CatalogueHeaderId = x.CatalogueHeaderId,
                            ProductName = x.ProductName,
                            ProductCode = x.ProductCode,
                            Color = x.Color,
                            Size = x.Size,
                            NoOfPiece = x.NoOfPiece,
                            PricePerPiece = x.PricePerPiece,
                            MajorCategory = new MajorCategory { MajorCategoryName = x.MajorCategoryName },
                            Division = new Division { DivisionName = x.DivisionName },
                            SubDivision = new SubDivisions { SubDivisionName = x.SubDivisionName },
                            NoOfPrint = x.NoOfPrint,
                            NoOfSize = x.NoOfSize,
                            NoOfColor = x.NoOfColor,
                            VendorDesignNo = x.VendorDesignNo,
                            Yarn = x.Yarn,
                            Weave1 = x.Weave1,
                            AvailableQty = x.AvailableQty,
                            NetRate = x.NetRate,
                            CatalogueLinesApprovals = new List<CatalogueLinesApproval> { new CatalogueLinesApproval { 
                                CatalogueLinesApprovalId = x.CatalogueLinesApprovalId
                            } },
                            Status = new Status { StatusId = x.LineStatusId  }
                        }).ToList().Distinct();

                    catalogueLines = catalogueLines
                        .GroupBy(c => new {
                            c.CatalogueLinesId,
                            //c.CatalogueHeaderId,
                            c.ProductName,
                            c.ProductCode,
                            c.Color,
                            c.Size,
                            c.NoOfPiece,
                            c.PricePerPiece,
                            //c.MajorCategory,
                            //c.Division,
                            //c.SubDivision,
                            c.NoOfPrint,
                            c.NoOfSize,
                            c.NoOfColor,
                            c.VendorDesignNo,
                            c.Yarn,
                            c.Weave1,
                            c.AvailableQty,
                            c.NetRate,
                            //c.CatalogueLinesApprovals,
                            //c.Status
                        }).Select(o => o.FirstOrDefault()).ToList();

                    obj.CatalogueLines = catalogueLines.ToList();

                    foreach (var catLine in catalogueLines.ToList())
                    {
                        var catalogueImages = catalogueGetForApprovals.Where(c => c.CatalogueLinesId == catLine.CatalogueLinesId)
                            .Select(x => new CatalogueLineImages
                            {
                                CatalogueLineImagesId = x.CatalogueLineImagesId,
                                ProductImage = x.ProductImage
                            }).ToList().Distinct();

                        var objLine = obj.CatalogueLines.Where(c => c.CatalogueLinesId == catLine.CatalogueLinesId).FirstOrDefault();
                        objLine.CatalogueLineImages = catalogueImages
                            .GroupBy(c=> new { c.CatalogueLineImagesId, c.ProductImage })
                            .Select(o => o.FirstOrDefault())
                            .ToList();
                    }

                    catHead.CatalogueLines = catalogueLines.ToList();

                    catalogueHeaders1.Add(obj);
                }
            }
            else
                toastNotification.AddErrorToastMessage(res.Item2);

            ViewBag.VendorId = id;
            return View(catalogueHeaders1);
        }

        public async Task<IActionResult> Details(int cHId, int lId, int aId, int vId)
        {
            if (!ISADMIN) return RedirectToAction("Logout", "Login");

            CatalogueHeader catalogueHeader = new CatalogueHeader();
            apiManager = new ApiManager(ServiceUrl + $"/api/Catalogue/{cHId}", AUTHTOKEN);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
                catalogueHeader = JsonConvert.DeserializeObject<CatalogueHeader>(res.Item2);
            else
                toastNotification.AddErrorToastMessage(res.Item2);

            ViewBag.ApprovalId = aId;
            ViewBag.LineId = lId;
            ViewBag.VendorId = vId;
            ViewBag.VendorDetails = await GetVendorDeteils(catalogueHeader.UserId);
            ViewBag.SizeList = ListSize().Result.Item2;
            ViewBag.ColorList = ListColor().Result.Item2;


            //ViewBag.vendorname = TempData["vendorname"];

            return View(catalogueHeader);
        }


        public async Task<JsonResult> UpdateStatus(int apId, int stid, string rem)
        {
            var objParm = new
            {
                CatLineApprId = apId,
                StatusId = stid,
                Remarks = rem
            };
            apiManager = new ApiManager(ServiceUrl + $"/api/Approval/updatecatstatus", AUTHTOKEN);
            var res = await apiManager.Post(JsonConvert.SerializeObject(objParm));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, msg= "Failed to post response" });
            }
        }
        private async Task<User> GetVendorDeteils(int VendorId)
        {
            User user = new User();
            try
            {
                apiManager = new ApiManager(ServiceUrl + $"/api/Vendor/getvendorbyId/" + VendorId);
                var res = await apiManager.Get();
                if (res.Item1== System.Net.HttpStatusCode.OK)
                {
                    user = JsonConvert.DeserializeObject<User>(res.Item2);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex) {
                toastNotification.AddErrorToastMessage(ex.Message); 
                return null;    
            } 
           
        }
    }
}
