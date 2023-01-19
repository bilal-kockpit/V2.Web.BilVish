using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NToastNotify;
using System.Collections.Generic;
using System.Threading.Tasks;
using V2.Models.RBAC;
using V2.Utility;

namespace V2.Controllers.RBAC
{
    public class RoleController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public RoleController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllRoles()
        {

            //List<Roles> roles = new List<Roles>();
            //apiManager = new ApiManager(ServiceUrl + "/api/Getallroles");
            //var res = await apiManager.Get();
            //if (res.Item1 == System.Net.HttpStatusCode.OK)
            //{
            //    roles = JsonConvert.DeserializeObject<List<Roles>>(res.Item2);
            //}
            return View();
        }
        [HttpGet]
        public IActionResult Role()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Role(Roles roles)
        {
            if (ModelState.IsValid)
            {

                apiManager = new ApiManager(ServiceUrl + "/api/role");
                var res = await apiManager.Post(JsonConvert.SerializeObject(roles));

                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {

                    return RedirectToAction("GetAllRoles");

                }
            }
            toastNotification.AddSuccessToastMessage("User Created Sucesfully");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetRoleById(int? id)
        {

            List<Roles> roles = new List<Roles>();
            apiManager = new ApiManager(ServiceUrl + "/api/GetRoleByid?id=" + id);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                roles = JsonConvert.DeserializeObject<List<Roles>>(res.Item2);
            }
            return View(roles);
        }
        [HttpGet]

        public async Task<IActionResult> UpdateRole(int id)
        {
            List<Roles> roles = new List<Roles>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles?id=" + id.ToString());
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                roles = JsonConvert.DeserializeObject<List<Roles>>(res.Item2);
            }
            return View(roles);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateRole(Roles roles)
        {


            apiManager = new ApiManager(ServiceUrl + "/api/roleupdate");
            var res = await apiManager.Put(JsonConvert.SerializeObject(roles));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllRoles");
            }

            toastNotification.AddSuccessToastMessage("User Updated Sucesfully");
            return View();

        }

        [HttpGet]
        public IActionResult DeleteRole(Roles role)
        {


            return View();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int? id, Roles role)
        {

            apiManager = new ApiManager(ServiceUrl + "/api/roledelete?id=" + id.ToString());
            var res = await apiManager.Delete(JsonConvert.SerializeObject(role));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {

                return RedirectToAction("GetAllRoles");

            }

            toastNotification.AddSuccessToastMessage("Deleted Sucesfully");
            return View();


        }
    }
}
