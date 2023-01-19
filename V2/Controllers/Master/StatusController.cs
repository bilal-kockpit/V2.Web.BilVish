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
    public class StatusController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public StatusController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllStatus()
        {

            List<Status> statuses = new List<Status>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles");
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                statuses = JsonConvert.DeserializeObject<List<Status>>(res.Item2);
            }
            return View(statuses);
        }

        [HttpGet]
        public IActionResult Status()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Status(Status status)
        {
            if (ModelState.IsValid)
            {

                apiManager = new ApiManager(ServiceUrl + "/api/role");
                var res = await apiManager.Post(JsonConvert.SerializeObject(status));

                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {
                    toastNotification.AddSuccessToastMessage("Created Sucesfully");


                    return RedirectToAction("GetAllStatus");

                }
            }
            toastNotification.AddSuccessToastMessage("Divisions Created Sucesfully");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetStatusById(int? id)
        {

            List<Status> statuses = new List<Status>();
            apiManager = new ApiManager(ServiceUrl + "/api/GetRoleByid?id=" + id);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                statuses = JsonConvert.DeserializeObject<List<Status>>(res.Item2);
            }
            return View(statuses);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            List<Status> statuses = new List<Status>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles?id=" + id.ToString());
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                statuses = JsonConvert.DeserializeObject<List<Status>>(res.Item2);
            }
            return View(statuses);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateStatus(Status status)
        {

            apiManager = new ApiManager(ServiceUrl + "/api/roleupdate");
            var res = await apiManager.Put(JsonConvert.SerializeObject(status));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Updated Sucesfully");

                return RedirectToAction("GetAllMajorCategories");
            }

            toastNotification.AddSuccessToastMessage("User Updated Sucesfully");
            return View();

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteStatus(int? id, Status status)
        {

            apiManager = new ApiManager(ServiceUrl + "/api/roledelete?id=" + id.ToString());
            var res = await apiManager.Delete(JsonConvert.SerializeObject(status));
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
