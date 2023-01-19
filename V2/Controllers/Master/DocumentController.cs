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
    public class DocumentController : BaseController
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public DocumentController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            _baseConfig = _configuration;
            this.toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllDocuments()
        {

            List<Document> documents = new List<Document>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles");
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                documents = JsonConvert.DeserializeObject<List<Document>>(res.Item2);
            }
            return View(documents);
        }

        [HttpGet]
        public IActionResult Document()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Document(Document document)
        {
            if (ModelState.IsValid)
            {

                apiManager = new ApiManager(ServiceUrl + "/api/role");
                var res = await apiManager.Post(JsonConvert.SerializeObject(document));

                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {
                    toastNotification.AddSuccessToastMessage("Created Sucesfully");


                    return RedirectToAction("GetAllDocuments");

                }
            }
            toastNotification.AddSuccessToastMessage("Divisions Created Sucesfully");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentById(int? id)
        {

            List<Document> documents = new List<Document>();
            apiManager = new ApiManager(ServiceUrl + "/api/GetRoleByid?id=" + id);
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                documents = JsonConvert.DeserializeObject<List<Document>>(res.Item2);
            }
            return View(documents);
        }
        [HttpGet]

        public async Task<IActionResult> UpdateDocument(int? id)
        {
            List<Document> documents = new List<Document>();
            apiManager = new ApiManager(ServiceUrl + "/api/Getallroles?id=" + id.ToString());
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                documents = JsonConvert.DeserializeObject<List<Document>>(res.Item2);
            }
            return View(documents);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateDocument(Document document)
        {

            apiManager = new ApiManager(ServiceUrl + "/api/roleupdate");
            var res = await apiManager.Put(JsonConvert.SerializeObject(document));
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                toastNotification.AddSuccessToastMessage("Updated Sucesfully");

                return RedirectToAction("GetAllMajorCategories");
            }

            toastNotification.AddSuccessToastMessage("User Updated Sucesfully");
            return View();

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDocument(int? id, MajorCategory majorCategories)
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
