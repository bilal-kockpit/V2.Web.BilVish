using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NToastNotify;
using System.Collections.Generic;
using System.Threading.Tasks;
using V2.Models.Master;
using V2.Utility;

namespace V2.Controllers.Master
{
    public class MajorCategoryController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public MajorCategoryController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllMajorCategories()
        {

            //List<MajorCategories> majorCategories = new List<MajorCategories>();
            //apiManager = new ApiManager(ServiceUrl + "/api/Getallroles");
            //var res = await apiManager.Get();
            //if (res.Item1 == System.Net.HttpStatusCode.OK)
            //{
            //    majorCategories = JsonConvert.DeserializeObject<List<MajorCategories>>(res.Item2);
            //}
            return View();
        }

        [HttpGet]
        public IActionResult MajorCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MajorCategory(MajorCategory majorCategories)
        {
            if (ModelState.IsValid)
            {

                apiManager = new ApiManager(ServiceUrl + "/api/role");
                var res = await apiManager.Post(JsonConvert.SerializeObject(majorCategories));

                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {
                    toastNotification.AddSuccessToastMessage("Created Sucesfully");


                    return RedirectToAction("GetAllMajorCategories");

                }
            }
            toastNotification.AddSuccessToastMessage("Divisions Created Sucesfully");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetMajorCategoryById(int? id)
        {
            List<MajorCategory> majorCategories = new List<MajorCategory>();
            apiManager = new ApiManager(ServiceUrl + "/api/GetRoleByid?id=" + id);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                majorCategories = JsonConvert.DeserializeObject<List<MajorCategory>>(res.Item2);
            }
            return View(majorCategories);
        }
        [HttpGet]

        public async Task<IActionResult> UpdateMajorCategory(int? id)
        {
            List<MajorCategory> majorCategories = new List<MajorCategory>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles?id=" + id.ToString());
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                majorCategories = JsonConvert.DeserializeObject<List<MajorCategory>>(res.Item2);
            }
            return View(majorCategories);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateMajorCategory(MajorCategory majorCategories)
        {

            apiManager = new ApiManager(ServiceUrl + "/api/roleupdate");
            var res = await apiManager.Put(JsonConvert.SerializeObject(majorCategories));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Updated Sucesfully");

                return RedirectToAction("GetAllMajorCategories");
            }

            toastNotification.AddSuccessToastMessage("User Updated Sucesfully");
            return View();

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteMajorCities(int? id, MajorCategory majorCategories)
        {

            apiManager = new ApiManager(ServiceUrl + "/api/roledelete?id=" + id.ToString());
            var res = await apiManager.Delete(JsonConvert.SerializeObject(majorCategories));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Deleted Sucesfully");

                return RedirectToAction("GetAllMajorCategories");

            }

            toastNotification.AddSuccessToastMessage("Deleted Sucesfully");
            return View();


        }
    }
}
