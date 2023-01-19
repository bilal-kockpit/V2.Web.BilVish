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
    public class DivisionController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public DivisionController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllDivisions()
        {

            //List<Divisions> divisions = new List<Divisions>();
            //apiManager = new ApiManager(ServiceUrl + "/api/Getallroles");
            //var res = await apiManager.Get();
            //if (res.Item1 == System.Net.HttpStatusCode.OK)
            //{
            //    divisions = JsonConvert.DeserializeObject<List<Divisions>>(res.Item2);
            //}
            //return View(divisions);
            return View();
        }

        [HttpGet]
        public IActionResult Division()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Division(Division divisions)
        {
            if (ModelState.IsValid)
            {

                apiManager = new ApiManager(ServiceUrl + "/api/role");
                var res = await apiManager.Post(JsonConvert.SerializeObject(divisions));

                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {

                    return RedirectToAction("GetAllDivisions");

                }
            }
            toastNotification.AddSuccessToastMessage("Divisions Created Sucesfully");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetDivisionById(int? id)
        {

            List<Division> divisions = new List<Division>();
            apiManager = new ApiManager(ServiceUrl + "/api/GetRoleByid?id=" + id);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                divisions = JsonConvert.DeserializeObject<List<Division>>(res.Item2);
            }
            return View(divisions);
        }
        [HttpGet]

        public async Task<IActionResult> UpdateDivision(int? id)
        {
            List<Division> divisions = new List<Division>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles?id=" + id.ToString());
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                divisions = JsonConvert.DeserializeObject<List<Division>>(res.Item2);
            }
            return View(divisions);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateDivision(Division divisions)
        {


            apiManager = new ApiManager(ServiceUrl + "/api/roleupdate");
            var res = await apiManager.Put(JsonConvert.SerializeObject(divisions));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Updated Sucesfully");

                return RedirectToAction("GetAllDivisions");
            }

            toastNotification.AddSuccessToastMessage("User Updated Sucesfully");
            return View();

        }
    }
}
