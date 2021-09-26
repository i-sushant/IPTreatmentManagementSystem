using IpTreatmentManagementPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IpTreatmentManagementPortal.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public LoginController(IConfiguration configuration, ILogger<LoginController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{_configuration["AuthenticationBaseUri"]}/auth/", content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    var stringJWT = await response.Content.ReadAsStringAsync();
                    JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);
                    _logger.LogInformation($"Token is {jwt.Token}");
                    HttpContext.Session.SetString("token", jwt.Token);
                    return RedirectToAction("Index", "ServiceOffering");
                }
                else
                {
                    ViewBag.Error = "Patient could not be marked as treated";
                    return RedirectToAction("Index");
                }
            }
        }
        //[HttpPost]
        //public IActionResult Login(User user)
        //{
        //    HttpClient client = new HttpClient();

        //    var token = string.Empty;
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        //    string endpoint = $"{_configuration["AuthenticationBaseUri"]}/auth/";
        //    client.BaseAddress = new Uri(endpoint);
        //    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        //    client.DefaultRequestHeaders.Accept.Add(contentType);
        //    var Response = client.PostAsync(endpoint, content);
        //    var result = Response.Result;


        //    if (result.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        _logger.LogInformation("Got the token");
        //        var stringJWT = result.Content.ReadAsStringAsync().Result;
        //        JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);
        //        _logger.LogInformation($"Token is {jwt.Token}");
        //        HttpContext.Session.SetString("token", jwt.Token);
                
        //        ViewBag.Message = "User logged in successfully!";
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //        return View();
        //}
    }
}
