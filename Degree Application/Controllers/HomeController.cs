using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Degree_Application.Models;

namespace Degree_Application.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //<img src="data:image;base64,@System.Convert.ToBase64String(Model.Image)" />
            return RedirectToAction("Index", "Item");
        }

            public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
