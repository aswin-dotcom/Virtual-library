using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _category;
        public CategoryController(ICategoryRepository db)
        {
            _category = db;

        }
        public IActionResult Index()
        {
            IEnumerable<Category> objFromDb = _category.GetAll();
            return View(objFromDb);
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
                _category.Add(obj);
                _category.Save();
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
            Category category = _category.Get(item => item.Id == id);
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
                _category.Update(obj);
                _category.Save();
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
            Category category = _category.Get(item => item.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        public IActionResult Deletecategory(Category category)
        {
             _category.Remove(category);
            _category.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
