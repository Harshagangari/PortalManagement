using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortalManagement.Entities;
using System.Net.Http;

namespace PortalManagement.Controllers
{
    public class LoginController : Controller
    {
        List<Login> loginDetails = new List<Login>();
        public LoginController()
        {
            loginDetails.Add(new Login() {id=2, Username = "lavangam", Password = "alda@paris" });

            loginDetails.Add(new Login() { id=3,Username = "pampachek", Password = "ruchi@usa" });
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = loginDetails.Find(x => x.Username == username);
            if (user != null && user.Password.Equals(password))
            {
                ViewBag.message = "Login Success";
                //
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:61990/");
                    var responseTask = client.GetAsync(string.Format("api/auth/{0}", user.id));

                   

                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var result1 = result.Content.ReadAsStringAsync().Result;
                        //pd = JsonConvert.DeserializeObject<PensionerDetails>(result1);
                        Response.Cookies.Append("token", result1, new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            HttpOnly = true,

                        });
                    }

                }
                //
                return RedirectToAction("Create", "PensionDetails");
            }
            ViewBag.message = "Password Incorrect";
            return View();
        }
    }
}
