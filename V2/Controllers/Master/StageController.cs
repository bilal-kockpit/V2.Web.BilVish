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
    public class StageController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public StageController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllStageHeaders()
        {

            List<StageHeader> stageHeaders = new List<StageHeader>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles");
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                stageHeaders = JsonConvert.DeserializeObject<List<StageHeader>>(res.Item2);
            }
            return View(stageHeaders);
        }

        [HttpGet]
        public IActionResult Stage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Stage(StageHeader stageHeader)
        {
            if (ModelState.IsValid)
            {

                apiManager = new ApiManager(ServiceUrl + "/api/role");
                var res = await apiManager.Post(JsonConvert.SerializeObject(stageHeader));

                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {
                    toastNotification.AddSuccessToastMessage("Created Sucesfully");


                    return RedirectToAction("GetAllStageHeaders");

                }
            }
            toastNotification.AddSuccessToastMessage("Divisions Created Sucesfully");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetStageById(int? id)
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
        public async Task<IActionResult> UpdateStage(int? id)
        {
            List<StageHeader> stageHeaders = new List<StageHeader>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles?id=" + id.ToString());
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                stageHeaders = JsonConvert.DeserializeObject<List<StageHeader>>(res.Item2);
            }
            return View(stageHeaders);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateStage(StageHeader stageHeader)
        {

            apiManager = new ApiManager(ServiceUrl + "/api/roleupdate");
            var res = await apiManager.Put(JsonConvert.SerializeObject(stageHeader));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Updated Sucesfully");

                return RedirectToAction("GetAllStageHeaders");
            }

            toastNotification.AddSuccessToastMessage("User Updated Sucesfully");
            return View();

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteStage(int? id, StageHeader stageHeader)
        {
            apiManager = new ApiManager(ServiceUrl + "/api/roledelete?id=" + id.ToString());
            var res = await apiManager.Delete(JsonConvert.SerializeObject(stageHeader));
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
