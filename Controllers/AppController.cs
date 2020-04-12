using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        //action
        public IActionResult Index() // match th cshtml file name in ViewsApp
        {
            //throw new InvalidOperationException();
            //Razor
            return View();
        }

        public IActionResult Contact() // match th cshtml file name in ViewsApp
        {
            ViewBag.Title = "Contact Us";
            return View();
        }

        public IActionResult About() // match th cshtml file name in ViewsApp
        {
            ViewBag.Title = "About Us";
            return View();
        }
    }
}
