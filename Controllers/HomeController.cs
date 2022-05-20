using Done.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Done.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        TaskContext db = new TaskContext();
        public IActionResult Index()
        {
            ViewBag.Todos = db.Todos.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Index(Todo tt)
        {
            if (ModelState.IsValid)
            {
                db.Todos.Add(tt);
                db.SaveChanges();
                return RedirectToAction("Note");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var et =  db.Todos.Find(id);
            return View(et);
        }
        [HttpPost]
        public IActionResult Edit(Todo tt ,int id)
        {
            if (id != tt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(tt);
                     db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(tt.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Note));
            }
            return View(tt);
        }

		private bool TodoExists(int id)
		{
			throw new NotImplementedException();
		}

		public IActionResult Note()
        {
            var d = db.Todos.ToList();
            return View(d);
        }

        public IActionResult About()
        {
            return View();
        }
  
        public IActionResult Delete(int id)
        {
            Todo de = db.Todos.Where(e => e.Id ==id).FirstOrDefault();
            if(de != null)
            {
                db.Todos.Remove(de);
                db.SaveChanges();
            }
            return RedirectToAction("Note");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}