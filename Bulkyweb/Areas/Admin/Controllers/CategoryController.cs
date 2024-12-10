//using Book.DataAccess.Data;
//using Book.DataAccess.Repository.IRepository;
//using Book.Models;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulkyweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public CategoryController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;

        }
        public IActionResult Index()
        {
            IEnumerable<Category> objFromDb = _unitofwork.Category.GetAll();
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
                _unitofwork.Category.Add(obj);
                _unitofwork.Save();
                TempData["success"] = "Category Added";
                return RedirectToAction("Index");
            }
            return View();


        }
        public IActionResult create()
        {
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category category = _unitofwork.Category.Get(item => item.Id == id);
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
                _unitofwork.Category.Update(obj);
                _unitofwork.Save();
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
            Category category = _unitofwork.Category.Get(item => item.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        public IActionResult Deletecategory(Category category)
        {
            _unitofwork.Category.Remove(category);
            _unitofwork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
