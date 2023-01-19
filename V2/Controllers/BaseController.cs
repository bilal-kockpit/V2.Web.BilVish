using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using V2.Models.Master;
using V2.Utility;

namespace V2.Controllers
{
    public class BaseController : Controller
    {
        public IConfiguration _baseConfig { get; set; }
        public string ServiceUrl { get { return _baseConfig["ServiceUrl"].ToString().Trim(); } }

        public bool SESSIONEXISTS
        {
            get { return (HttpContext.Session.GetString("TOKEN") == null) ? false : true; }
        }
        public bool ISSUPERADMIN
        {
            get
            {
                return (HttpContext.Session.GetString("ROLE") == "SUPERADMIN") ? true : false;
            }
        }
        public bool ISADMIN
        {
            get
            {
                return (HttpContext.Session.GetString("ROLE") == "ADMIN") ? true : false;
            }
        }
        public bool ISVENDOR
        {
            get
            {
                return (HttpContext.Session.GetString("ROLE") == "VENDOR") ? true : false;
            }
        }

        public int USERID
        {
            get
            {
                return (HttpContext.Session.GetInt32("USERID") == null) ? 0
                    : HttpContext.Session.GetInt32("USERID").Value;
            }
        }
        public string AUTHTOKEN
        {
            get
            {
                return (HttpContext.Session.GetString("TOKEN") == null) ? ""
                    : HttpContext.Session.GetString("TOKEN");
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!SESSIONEXISTS)
            {
                filterContext.Result = new RedirectResult("~/Login/Logout");
                return;
            }
            ViewBag.ImageDomain = ServiceUrl;
            ViewBag.Role = HttpContext.Session.GetString("ROLE");
            base.OnActionExecuting(filterContext);
        }


        #region [List of Masters]
        public async Task<List<SelectListItem>> ListCity() 
        {
            List<SelectListItem> list = new List<SelectListItem>();
            ApiManager apiManager = new ApiManager(ServiceUrl + "/api/City");
            var res = await apiManager.Get();
            if(res.Item1 == System.Net.HttpStatusCode.OK)
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
        public async Task<List<SelectListItem>> ListDivision()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            ApiManager apiManager = new ApiManager(ServiceUrl + "/api/Division");
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                var cities = JsonConvert.DeserializeObject<List<Division>>(res.Item2);
                list = cities.Where(c => c.IsActive == true).Select(c => new SelectListItem
                {
                    Text = c.DivisionName,
                    Value = c.DivisionId.ToString()
                }).ToList();
            }
            return list;
        }
        public async Task<List<SelectListItem>> ListSubDivision(int DivisionId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            ApiManager apiManager = new ApiManager(ServiceUrl + $"/api/SubDivision/SubDivbyDivId/{DivisionId}");
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                var cities = JsonConvert.DeserializeObject<List<SubDivisions>>(res.Item2);
                list = cities.Where(c => c.IsActive == true).Select(c => new SelectListItem
                {
                    Text = c.SubDivisionName,
                    Value = c.SubDivisionId.ToString()
                }).ToList();
            }
            return list;
        }
        public async Task<List<SelectListItem>> ListMajorCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            ApiManager apiManager = new ApiManager(ServiceUrl + "/api/MajorCategory");
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                var cities = JsonConvert.DeserializeObject<List<MajorCategory>>(res.Item2);
                list = cities.Where(c => c.IsActive == true).Select(c => new SelectListItem
                {
                    Text = c.MajorCategoryName,
                    Value = c.MajorCategoryId.ToString()
                }).ToList();
            }
            return list;
        }
        public async Task<Tuple<List<SelectListItem>, List<Size>>> ListSize()
        {
            List<Size> sizes = new List<Size>();
            List<SelectListItem> list = new List<SelectListItem>();
            ApiManager apiManager = new ApiManager(ServiceUrl + "/api/Size");
            var res = await apiManager.Get();
            if (res.Item1 == System.Net.HttpStatusCode.OK)
            {
                sizes = JsonConvert.DeserializeObject<List<Size>>(res.Item2);
                list = sizes.Where(c => c.IsActive == true).Select(c => new SelectListItem
                {
                    Text = c.SizeName,
                    Value = c.SizeId.ToString()
                }).ToList();
            }
            return new Tuple<List<SelectListItem>, List<Size>>(list, sizes);
        }
        #endregion
    }
}
