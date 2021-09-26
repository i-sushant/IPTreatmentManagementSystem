using IpTreatmentManagementPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IpTreatmentManagementPortal.Controllers
{
    public class ServiceOfferingController : Controller
    {
        private readonly IConfiguration _configuration;

        public ServiceOfferingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {

            var token = HttpContext.Session.GetString("token");
            if (token is null)
            {
                return RedirectToAction("Index", "Login");
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"{_configuration["IPTreatmentOfferingBaseUri"]}/IPTreatmentPackages/IPTreatmentPackages");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var patients = JsonConvert.DeserializeObject<List<IPTreatmentPackage>>(jsonString);
                    return View(patients);
                }
                else
                {
                    ViewBag.msg = response.StatusCode;
                    return View();
                }
            }
        }


    }
}
