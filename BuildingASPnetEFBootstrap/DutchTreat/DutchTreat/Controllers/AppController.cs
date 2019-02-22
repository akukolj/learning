using System.Linq;
using DutchTreat.Data;
using DutchTreat.Data.Interfaces;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailServices _mailServices;
        private readonly IDutchRepository _repository;
        public AppController(IMailServices mailServices,IDutchRepository repository)
        {
            _mailServices = mailServices;
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailServices.SendMessage("sjsjsj@kdkdk.com", model.Subject,
                    $"From: {model.Name} - {model.Email}, Message : {model.Message}");
                ViewBag.UserMessage = "Mail sent";
                ModelState.Clear();
            }
            
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }

        public IActionResult Shop()
        {
            var result = _repository.GetAllProducts();

            return View(result);
        }
    }
}