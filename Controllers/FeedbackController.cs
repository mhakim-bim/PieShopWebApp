using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;

namespace PieShop.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

      
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult Index(Feedback feedback)
        {

            if (ModelState.IsValid)
            {
                _feedbackRepository.AddNewFeedback(feedback);
                return RedirectToAction("Index", "Pie");
            }

            return View(feedback);
        }
    }
}
