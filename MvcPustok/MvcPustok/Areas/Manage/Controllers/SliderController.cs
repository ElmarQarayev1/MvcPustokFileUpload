using System;
using Microsoft.AspNetCore.Mvc;
using MvcPustok.Areas.Manage.ViewModels;
using MvcPustok.Data;
using MvcPustok.Models;

namespace MvcPustok.Areas.Manage.Controllers
{
	[Area("manage")]
	public class SliderController:Controller
	{
		private readonly AppDbContext _context;

		public SliderController(AppDbContext context)
		{
			_context = context;
		}
		public  IActionResult Index(int page=1)
		{
			var query = _context.Sliders;
			return View(PaginatedList<Slider>.Create(query,page,2));
		}
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
		{
            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            if (_context.Sliders.Any(x => x.Order == slider.Order))
			{
				ModelState.AddModelError("Order", "Order already exists!");
				return View(slider);
			}
			_context.Sliders.Add(slider);
			_context.SaveChanges();
			return RedirectToAction("index");
		}
	}
}

