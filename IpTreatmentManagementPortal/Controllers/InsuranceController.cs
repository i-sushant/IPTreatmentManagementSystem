using IpTreatmentManagementPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IpTreatmentManagementPortal.Controllers
{
    public class InsuranceController : Controller
    {
        private readonly IConfiguration _configuration;

        public InsuranceController(IConfiguration configuration)
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
                var response = await httpClient.GetAsync($"{_configuration["InsuranceBaseUri"]}/AuditSeverity/GetAllInsurerDetail");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var insurers = JsonConvert.DeserializeObject<List<InsurerDetail>>(jsonString);
                    return View(insurers);
                }
                else
                {
                    ViewBag.Error = "Error";
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Claim(string patientId)
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
                var response = await httpClient.GetAsync($"{_configuration["InsuranceBaseUri"]}/AuditSeverity/Claim/{patientId}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var claim = JsonConvert.DeserializeObject<InitiateClaim>(result);
                    return View(claim);
                }
                else
                {
                    ViewBag.Error = "Claim not found";
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        public IActionResult SettleClaim()
        {

            var token = HttpContext.Session.GetString("token");
            if (token is null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SettleClaim(InitiateClaim claim)
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
                StringContent content = new StringContent(JsonConvert.SerializeObject(claim), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.PostAsync($"{_configuration["InsuranceBaseUri"]}/AuditSeverity/InitiateClaim", content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var errorMessage = JsonConvert.DeserializeObject<string>(result);
                    if (errorMessage is null)
                        TempData["Error"] = "Claim could not be settled! Please try again";
                    else TempData["Error"] = errorMessage;
                    return RedirectToAction("SettleClaim");
                }
            }
        }
    }
}
