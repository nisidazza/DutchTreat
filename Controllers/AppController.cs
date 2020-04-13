using DutchTreat.Services;
using DutchTreat.ViewModels;
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
        private readonly IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }
        //action
        public IActionResult Index() // match the cshtml file name in ViewsApp
        {
            //Razor
            return View();
        }

        [HttpGet("contact")] //specify a route that is specific to Contact
        public IActionResult Contact() // match the cshtml file name in ViewsApp
        {
            //throw new InvalidOperationException("Bad things happen");
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send the email via Service
                _mailService.SendMessage("nisida@azzalini.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
            }
            
            return View();
        }

        public IActionResult About() // match the cshtml file name in ViewsApp
        {
            ViewBag.Title = "About Us";
            return View();
        }
    }
}
