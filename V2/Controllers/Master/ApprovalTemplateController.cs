using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NToastNotify;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using V2.Controllers;
using V2.Models.Master;
using V2.Utility;

namespace V2.Controllers.Master
{
    public class ApprovalTemplateController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public ApprovalTemplateController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllApprovalTemplate()
        {

            List<ApprovalTemplate> approvalTemplates = new List<ApprovalTemplate>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles");
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                approvalTemplates = JsonConvert.DeserializeObject<List<ApprovalTemplate>>(res.Item2);
            }
            return View(approvalTemplates);
        }

        [HttpGet]
        public IActionResult ApprovalTemplate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApprovalTemplate(ApprovalTemplate approvalTemplate)
        {
            if (ModelState.IsValid)
            {

                apiManager = new ApiManager(ServiceUrl + "/api/role");
                var res = await apiManager.Post(JsonConvert.SerializeObject(approvalTemplate));

                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {
                    toastNotification.AddSuccessToastMessage("Created Sucesfully");


                    return RedirectToAction("GetAllApprovalTemplate");

                }
            }
            toastNotification.AddSuccessToastMessage("Divisions Created Sucesfully");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetApprovalTemplateById(int? id)
        {

            List<ApprovalTemplate> approvalTemplates = new List<ApprovalTemplate>();
            apiManager = new ApiManager(ServiceUrl + "/api/GetRoleByid?id=" + id);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                approvalTemplates = JsonConvert.DeserializeObject<List<ApprovalTemplate>>(res.Item2);
            }
            return View(approvalTemplates);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateApprovalTemplate(int? id)
        {
            List<ApprovalTemplate> approvalTemplates = new List<ApprovalTemplate>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles?id=" + id.ToString());
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                approvalTemplates = JsonConvert.DeserializeObject<List<ApprovalTemplate>>(res.Item2);
            }
            return View(approvalTemplates);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateApprovalTemplate(ApprovalTemplate approvalTemplate)
        {

            apiManager = new ApiManager(ServiceUrl + "/api/roleupdate");
            var res = await apiManager.Put(JsonConvert.SerializeObject(approvalTemplate));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Updated Sucesfully");

                return RedirectToAction("GetAllApprovalTemplate");
            }

            toastNotification.AddSuccessToastMessage("User Updated Sucesfully");
            return View();

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteApprovalTemplate(int? id, ApprovalTemplate approvalTemplate)
        {

            apiManager = new ApiManager(ServiceUrl + "/api/roledelete?id=" + id.ToString());
            var res = await apiManager.Delete(JsonConvert.SerializeObject(approvalTemplate));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Deleted Sucesfully");

                return RedirectToAction("GetAllApprovalTemplate");

            }

            toastNotification.AddSuccessToastMessage("Deleted Sucesfully");
            return View();


        }
    }
}
