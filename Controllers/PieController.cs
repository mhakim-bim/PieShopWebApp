using Microsoft.AspNetCore.Mvc;
using PieShop.Models;

namespace PieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public PieController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "The List of Pies";
            return View(_pieRepository.GetAllPies());
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            return View(pie);
        }
    }
}
