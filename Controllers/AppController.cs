using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repository;

        //replace DutchContext with IDutchRepository
        public AppController(IMailService mailService, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        //action
        public IActionResult Index() // match the cshtml file name in ViewsApp
        {
            //var results = _context.Products.ToList();
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
                _mailService.SendMessage("ross@smith.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About() // match the cshtml file name in ViewsApp
        {
            ViewBag.Title = "About Us";
            return View();
        }

        
        public IActionResult Shop()
        {
            // this goes to the database, gets all products and returns them
            //var result = _context.Products
            //    .OrderBy(p => p.Category)
            //    .ToList();

            /*another way using LINQ query:
             var result = from p in _context.Products
                          orderby p.Category
                          select p;
             return View(result.ToList())
             */

            // it shows some products
            return View();
        }
    }
}