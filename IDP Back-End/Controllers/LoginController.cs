using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP_Back_End.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login Endpoint
        public ActionResult Index()
        {
            return View();
        }

        // POST: Register Endpoit
        public ActionResult Register()
        {
                return View("~/Views/Register/Index.cshtml");
        }
    }
}
