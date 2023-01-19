using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using V2.Models.Account;
using V2.Models.Master;
using V2.Utility;

namespace V2.Controllers.Account
{
    public class LoginController : Controller
    {
        private ApiManager apiManager;
        private IConfiguration _configuration;
        private readonly IToastNotification toastNotification;
        public LoginController(IConfiguration configuration, IToastNotification toastNotification)
        {
            _configuration = configuration;
            this.toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login user)
        {
            if (ModelState.IsValid)
            {
                apiManager = new ApiManager(_configuration["ServiceUrl"].ToString().Trim() + "/api/Authentication");
                var res = await apiManager.Post(JsonConvert.SerializeObject(user));
                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(res.Item2);

                    HttpContext.Session.SetString("TOKEN", loginResponse.Token);
                    HttpContext.Session.SetInt32("USERID", loginResponse.UserId);
                    HttpContext.Session.SetString("EMAIL", loginResponse.EmailId);
                    HttpContext.Session.SetString("ROLE", loginResponse.RoleName.ToUpper());

                    if(loginResponse.RoleName.ToUpper() == "SUPERADMIN")
                        return RedirectToAction("SuperAdmin", "Home");
                    if (loginResponse.RoleName.ToUpper() == "ADMIN")
                        return RedirectToAction("Admin", "Home");
                    else if(loginResponse.RoleName.ToUpper() == "VENDOR")
                        return RedirectToAction("Index", "Home");
                }
                else
                    toastNotification.AddErrorToastMessage(res.Item2);
            }
            return View();
        }


        public async Task<IActionResult> Register()
        {
            ViewBag.cities = await ListCity();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            ViewBag.cities = await ListCity();
            if (ModelState.IsValid)
            {
                apiManager = new ApiManager(_configuration["ServiceUrl"].ToString().Trim() + "/api/Vendor");
                var res = await apiManager.Post(JsonConvert.SerializeObject(user));
                if (res.Item1 == System.Net.HttpStatusCode.OK)
                {
                    toastNotification.AddSuccessToastMessage("Successfully Registered");
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                   toastNotification.AddErrorToastMessage(res.Item2);

                }                   
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TOKEN");
            HttpContext.Session.Remove("USERID");
            HttpContext.Session.Remove("EMAIL");
            HttpContext.Session.Remove("ROLE");
            return RedirectToAction("Index", "Login");
        }

        public IActionResult FortgotPassword()
        {
            return View();
        }

        public async Task<List<SelectListItem>> ListCity()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            ApiManager apiManager = new ApiManager(_configuration["ServiceUrl"].ToString().Trim() + "/api/City");
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                var cities = JsonConvert.DeserializeObject<List<City>>(res.Item2);
                list = cities.Where(c => c.IsActive == true).Select(c => new SelectListItem
                {
                    Text = c.CityName,
                    Value = c.CityId.ToString()
                }).ToList();
            }
            return list;
        }
    }
}
