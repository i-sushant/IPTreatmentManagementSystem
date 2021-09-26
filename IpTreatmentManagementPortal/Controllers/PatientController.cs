using IpTreatmentManagementPortal.Models;
using IpTreatmentManagementPortal.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

    public class PatientController : Controller
    {
        private readonly IConfiguration _configuration;

        public PatientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        [HttpGet]
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
                var response = await httpClient.GetAsync($"{_configuration["IPTreatmentBaseUri"]}/Patient/");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var patients = JsonConvert.DeserializeObject<List<PatientDetail>>(jsonString);
                    return View(patients);
                }
                else
                {
                    ViewBag.msg = response.StatusCode;
                    return View();
                }
            }
        }

        

        [HttpGet]
        public async Task<IActionResult> MarkAsComplete(int id )
        {
            var token = HttpContext.Session.GetString("token");
            if(token is null)
            {
                return RedirectToAction("Index", "Login");
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                StringContent content = new StringContent(JsonConvert.SerializeObject(new { }), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.PatchAsync($"{_configuration["IPTreatmentBaseUri"]}/Patient/MarkTreatmentComplete/{id}", content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Patient could not be marked as treated";
                    return RedirectToAction("Index");
                }
            }
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientDetail patientDetail)
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
                StringContent content = new StringContent(JsonConvert.SerializeObject(patientDetail), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.PostAsync($"{_configuration["IPTreatmentBaseUri"]}/Patient/FormulateTreatmentTimeTable", content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Patient could not be registered";
                    return RedirectToAction("Index");
                }
            }
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (token is null)
            {
                return RedirectToAction("Index", "Login");
            }
            PatientDetail patient = await GetPatientDetail(id, token);
            if (patient is null)
            {
                ViewBag.Error = "Patient not found";
                return RedirectToAction("Index");
            }
            TreatmentPlan treatmentPlan = await GetTreatmentPlan(id, token);
            if (treatmentPlan is null)
            {
                ViewBag.Error = "Treatment plan not found";
                return RedirectToAction("Index");
            }
            InitiateClaim claim = await GetClaim(id, token);

            return View(new PatientViewModel { PatientDetail = patient, TreatmentPlan = treatmentPlan, Claim = claim });
        }
        private async Task<InitiateClaim> GetClaim(int patientId, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"{_configuration["InsuranceBaseUri"]}/AuditSeverity/Claim/?patientId={patientId}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var claim = JsonConvert.DeserializeObject<InitiateClaim>(result);
                    return claim;
                }
                else
                {
                    return null;
                }
            }
        }
        private async Task<PatientDetail> GetPatientDetail(int patientId, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"{_configuration["IPTreatmentBaseUri"]}/Patient/{patientId}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var patient = JsonConvert.DeserializeObject<PatientDetail>(jsonString);
                    return patient;

                }
                else
                {
                    return null;
                }
            }
        }
        private async Task<TreatmentPlan> GetTreatmentPlan(int patientId, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"{_configuration["IPTreatmentBaseUri"]}/Patient/GetTimeTableByPatientId/{patientId}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var treatmentPlan = JsonConvert.DeserializeObject<TreatmentPlan>(jsonString);
                    return treatmentPlan;
                }
                else
                {
                    return null;
                    
                }
            }
        }
        
    }
}
