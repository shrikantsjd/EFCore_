using DAL;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        AppDBContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public EmployeeController(AppDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var emp = _db.Employees.ToList();
            return View(emp);
        }

        public IActionResult Create()
        {
            ViewBag.Departments  = _db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            ModelState.Remove("EmployeeId");
            if (ModelState.IsValid)
            {
                string filename = UploadedFile(model);
                Employee emp = new Employee
                {
                    Name = model.Name,
                    Address = model.Address,
                    DepartmentId = model.DepartmentId,
                    ImagePath = filename

                };

                _db.Employees.Add(emp);
                _db.SaveChanges();              

                return RedirectToAction("Index");
            }
            ViewBag.Departments = _db.Departments.ToList();
            return View();
        }


        public IActionResult Edit(int id)
        {
            ViewBag.Departments = _db.Departments.ToList();
            // Product model = _db.Products.Find(id);
            EmployeeViewModel model = null;
            //var data =  _db.Procedures.usp_getproductAsync(id).Result.FirstOrDefault();

            var result =  _db.Employees.Find(id);
            if (result != null)
            {
                var data = result;
                model = new EmployeeViewModel
                {
                    EmployeeId = data.EmployeeId,
                    DepartmentId = data.DepartmentId,
                    Name = data.Name,
                    Address = data.Address,

                    ExistingImagepath = data.ImagePath
                };
            }
            return View("Create", model);
        }
        [HttpPost]
        public IActionResult Edit(int id, EmployeeViewModel model)
        {
            ViewBag.Departments = _db.Departments.ToList();
            ModelState.Remove("ExistingImagepath");
            if (ModelState.IsValid)
            {
                var emp = _db.Employees.Find(id);
                emp.EmployeeId = model.EmployeeId;
                emp.DepartmentId = model.DepartmentId;
                emp.Name = model.Name;
                emp.Address = model.Address;

                   
                if (model.photo != null)
                {
                   
                    emp.ImagePath = UploadedFile(model);
                }
                _db.Update(emp);
                _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Create", model);
        }
        private string UploadedFile(EmployeeViewModel model)
        {
            string uniqueFileName = null;

            if (model.photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "ImagePath");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.photo.CopyTo(fileStream);
                }
                uniqueFileName = "~/ImagePath/" + uniqueFileName;
            }

            return uniqueFileName;
        }


        public IActionResult Delete(int id)
        {
            Employee model = _db.Employees.Find(id);
            if (model != null)
            {
                if (model.ImagePath != null)
                {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, model.ImagePath);
                        System.IO.File.Delete(filePath.Replace("~", "").Replace("/", "\\"));
                    

                 }

                _db.Employees.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
