using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulkyweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            List<Category> objFromDb = _db.Categories.ToList();
            return View(objFromDb);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)

        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order Cannot exactly match the same.");
            }
            if (obj.DisplayOrder == 0)
            {
                ModelState.AddModelError("DisplayOrder", "DisplayOrder cannot be Zero.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Added";
                return RedirectToAction("Index");
            }
            return View();


        }
        public IActionResult Edit(int? id)
        {
            if (id == 0|| id== null)
            {
                return NotFound();
            }
            Category category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
            
        }
        [HttpPost]
        public IActionResult Edit(Category obj)

        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "The Display Order Cannot exactly match the same.");
            //}
            //if (obj.DisplayOrder == 0)
            //{
            //    ModelState.AddModelError("DisplayOrder", "DisplayOrder cannot be Zero.");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated Successfully";
                return RedirectToAction("Index");
            }
            return View();


        }
        public IActionResult DeleteShow(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category category = _db.Categories.FirstOrDefault(item=>item.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        public IActionResult Deletecategory(Category category)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
