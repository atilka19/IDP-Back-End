using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP_Back_End.Controllers
{
    public class Chat : Controller
    {
        // GET the Chat feed. This endpoint returns the connection for the chat.
        public ActionResult Index()
        {
            return View();
        }
    }
}
