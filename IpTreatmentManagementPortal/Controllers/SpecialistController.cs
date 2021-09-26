using IPTreatmentOfferingMicroservices.Models;
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
    public class SpecialistController : Controller
    {
        private readonly IConfiguration _configuration;

        public SpecialistController(IConfiguration configuration)
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
                var response = await httpClient.GetAsync($"{_configuration["IPTreatmentOfferingBaseUri"]}/IPTreatmentPackages/SpecialistDetail");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var specialists = JsonConvert.DeserializeObject<List<SpecialistDetail>>(jsonString);
                    return View(specialists);
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
