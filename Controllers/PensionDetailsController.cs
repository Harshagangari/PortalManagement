using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortalManagement.Entities;
using System.Dynamic;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;

namespace PortalManagement.Controllers
{
    [Authorize]
    public class PensionDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        
        [HttpPost]
        
        public IActionResult Display(double AadharNo, string name, DateTime dob, int pensionType, string pan)
        {
            dynamic dataObj = new ExpandoObject();
            dataObj.aadharNo = AadharNo;
            dataObj.Name = name;
            dataObj.Dob = dob;
            dataObj.pensiontype = pensionType;
            dataObj.Pan = pan;

            
            using (var client = new HttpClient())
            {
                
               
                HttpResponseMessage result = null;
               // client.BaseAddress = new Uri("http://localhost:49650/");
                //try
                client.BaseAddress = new Uri("https://localhost:44380/");
                

                string ptype;
                    if (dataObj.pensiontype == 1)
                    {
                        ptype = "family";
                    }
                    else
                    {
                        ptype = "self";
                    }

                string url = "processpension/mod1?&dob=" + dataObj.Name + "&aadharNumber=" + dataObj.aadharNo + "&Name=" + dataObj.Name +
                    "&PAN=" + dataObj.Pan + "&pensionType=" + ptype;
                    var response = client.GetAsync(url);
                    response.Wait();
                    result = response.Result;
                    
                    if (result.IsSuccessStatusCode)
                    {
                    
                      ViewBag.details = dataObj;
                      ViewBag.result =(result.Content.ReadAsStringAsync().Result);
                   
                    }
                    else
                    {
                    ViewBag.details = dataObj;
                    ViewBag.result = "Please Check your details and try agin......!! THANK YOU";
                }

                
                //catch (Exception exception)
                //{
                //    ViewBag.wrongPan = "Pan Details are invalid";
                //    return View("Create");
                //}
                return View();
            }

        }


    }
}
