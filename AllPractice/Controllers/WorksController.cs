using AllPractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace AllPractice.Controllers
{
    public class WorksController : Controller
    {
        private readonly W1Context _w1Context;
        public WorksController(W1Context w1Context)
        {
            _w1Context = w1Context;
        }
        public ActionResult Index()
        {
            var Works = _w1Context.Works.ToList();
            return View(Works);
        }
        public ActionResult Create()
        {
            return View(new Work());
        }
        [HttpPost]
        public ActionResult Create(Work work)
        {
            _w1Context.Works.Add(work);
            _w1Context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Work work = _w1Context.Works.Where(x => x.Id == id).SingleOrDefault() ?? new Work();
            return View(work);
        }
        [HttpPost]
        public ActionResult Edit(Work model)
        {
            Work work = _w1Context.Works.Where(x => x.Id == model.Id).SingleOrDefault() ?? new Work();
            if (work != null)
            {
                _w1Context.Entry(work).CurrentValues.SetValues(model);
                _w1Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            Work work = _w1Context.Works.Find(id) ?? new Work();

            return View(work);
        }
        [HttpPost]
        public ActionResult Delete(int id, Work model)
        {
            var teacher = _w1Context.Works.Where(x => x.Id == id).SingleOrDefault();
            if (teacher != null)
            {
                _w1Context.Works.Remove(teacher);
                _w1Context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult ToHome()
        {
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ToWorks()
        {
            return RedirectToAction("Index", "Works");
        }
        public ActionResult ToCompanies()
        {
            return RedirectToAction("Index", "Companies");
        }
    }
}
