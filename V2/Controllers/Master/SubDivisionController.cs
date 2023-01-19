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
    public class SubDivisionController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public SubDivisionController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllSubDivisions()
        {

            //List<SubDivisions> subdivisions = new List<SubDivisions>();
            //apiManager = new ApiManager(ServiceUrl + "/api/Getallroles");
            //var res = await apiManager.Get();
            //if (res.Item1 == System.Net.HttpStatusCode.OK)
            //{
            //    subdivisions = JsonConvert.DeserializeObject<List<SubDivisions>>(res.Item2);
            //}
            return View();
        }

        [HttpGet]
        public IActionResult SubDivision()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubDivisions(SubDivisions subdivisions)
        {
            if (ModelState.IsValid)
            {

                apiManager = new ApiManager(ServiceUrl + "/api/role");
                var res = await apiManager.Post(JsonConvert.SerializeObject(subdivisions));

                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {
                    toastNotification.AddSuccessToastMessage("SubDivisions Created Sucesfully");


                    return RedirectToAction("GetAllSubDivisions");

                }
            }
            toastNotification.AddSuccessToastMessage("Divisions Created Sucesfully");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetSubDivisionById(int? id)
        {

            List<SubDivisions> subdivisions = new List<SubDivisions>();
            apiManager = new ApiManager(ServiceUrl + "/api/GetRoleByid?id=" + id);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                subdivisions = JsonConvert.DeserializeObject<List<SubDivisions>>(res.Item2);
            }
            return View(subdivisions);
        }
        [HttpGet]

        public async Task<IActionResult> UpdateSubDivision(int? id)
        {
            List<SubDivisions> subdivision = new List<SubDivisions>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles?id=" + id.ToString());
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                subdivision = JsonConvert.DeserializeObject<List<SubDivisions>>(res.Item2);
            }
            return View(subdivision);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateSubDivision(SubDivisions subdivisions)
        {


            apiManager = new ApiManager(ServiceUrl + "/api/roleupdate");
            var res = await apiManager.Put(JsonConvert.SerializeObject(subdivisions));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Updated Sucesfully");

                return RedirectToAction("GetAllSubDivisions");
            }

            toastNotification.AddSuccessToastMessage("User Updated Sucesfully");
            return View();

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteSubDivisions(int? id, SubDivisions subdivisions)
        {

            apiManager = new ApiManager(ServiceUrl + "/api/roledelete?id=" + id.ToString());
            var res = await apiManager.Delete(JsonConvert.SerializeObject(subdivisions));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Deleted Sucesfully");

                return RedirectToAction("GetAllRoles");

            }

            toastNotification.AddSuccessToastMessage("Deleted Sucesfully");
            return View();


        }

    }
}
