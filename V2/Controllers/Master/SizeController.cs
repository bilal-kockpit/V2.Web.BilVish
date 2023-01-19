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
    public class SizeController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public SizeController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ///TODO
            ///get the list of size
            ///call get api
            ///cast response in model and pass to viewpage

            var listSize = new List<Size>();
            return View(listSize);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            ///if id == 0 then new entry then pass blank model
            ///if id != 0 then call getbyid API and cast in model and pass to view page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Size size)
        {
            //code to insert or update
            if (ModelState.IsValid)
            {
                apiManager = new ApiManager(ServiceUrl + "/api/role");
                var res = await apiManager.Post(JsonConvert.SerializeObject(size));

                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {
                    toastNotification.AddSuccessToastMessage("Created Sucesfully");
                    return RedirectToAction("GetAllSize");
                }
            }
            toastNotification.AddSuccessToastMessage("Divisions Created Sucesfully");
            return View();
        }


        public async Task<IActionResult> Remove(int Id)
        {
            var size = new Size
            {
                
            };

            apiManager = new ApiManager(ServiceUrl + "/api/roledelete?id=" + Id.ToString());
            var res = await apiManager.Delete(JsonConvert.SerializeObject(size));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Deleted Sucesfully");
                return RedirectToAction("GetAllSize");
            }

            toastNotification.AddSuccessToastMessage("Deleted Sucesfully");
            return View();
        }


        //[HttpGet]
        //public async Task<IActionResult> GetSizeById(int? id)
        //{
        //    List<Size> size = new List<Size>();
        //    apiManager = new ApiManager(ServiceUrl + "/api/GetRoleByid?id=" + id);
        //    var res = await apiManager.Get();
        //    if (res.Item1 == System.Net.HttpStatusCode.OK)
        //    {
        //        size = JsonConvert.DeserializeObject<List<Size>>(res.Item2);
        //    }
        //    return View(size);
        //}

        //[HttpGet]
        //public async Task<IActionResult> UpdateSize(int? id)
        //{
        //    List<Size> size = new List<Size>();
        //    apiManager = new ApiManager(ServiceUrl + "/api/Getallroles?id=" + id.ToString());
        //    var res = await apiManager.Get();
        //    if (res.Item1 == System.Net.HttpStatusCode.OK)
        //    {
        //        size = JsonConvert.DeserializeObject<List<Size>>(res.Item2);
        //    }
        //    return View(size);

        //}
        //[HttpPut]
        //public async Task<IActionResult> UpdateSize(Size size)
        //{

        //    apiManager = new ApiManager(ServiceUrl + "/api/roleupdate");
        //    var res = await apiManager.Put(JsonConvert.SerializeObject(size));
        //    if (res.Item1 == System.Net.HttpStatusCode.OK)
        //    {
        //        toastNotification.AddSuccessToastMessage("Updated Sucesfully");

        //        return RedirectToAction("GetAllSize");
        //    }

        //    toastNotification.AddSuccessToastMessage("User Updated Sucesfully");
        //    return View();

        //}


    }
}
