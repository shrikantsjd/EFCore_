using DAL;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class DepartmentController : Controller
    {

        AppDBContext _db;

        public DepartmentController(AppDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var department = _db.Departments.ToList();
            return View(department);
        }

        public IActionResult Create()
        {           
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department model)
        {
            ModelState.Remove("DepartmentId");
            if (ModelState.IsValid)
            {
                _db.Departments.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            Department model = _db.Departments.Find(id);

            return View("Create",model);
        }

        [HttpPost]
        public IActionResult Edit(Department model)
        {
            ModelState.Remove("DepartmentId");
            if (ModelState.IsValid)
            {
                _db.Departments.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            Department model = _db.Departments.Find(id);
            if (model != null)
            {
                _db.Departments.Remove(model);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }





    }
}
