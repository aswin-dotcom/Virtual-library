using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulkyweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public ProductController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;


        }



        public IActionResult Index()
        {
            IEnumerable<Product> objview =_unitofwork.Product.GetAll();
            return View(objview);
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)

        {

            if (ModelState.IsValid)
            {
                _unitofwork.Product.Add(obj);
                _unitofwork.Save();
                TempData["success"] = "Category Added";
                return RedirectToAction("Index");
            }
            return View();


        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Product product = _unitofwork.Product.Get(item => item.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }

        [HttpPost]
        public IActionResult Edit(Product obj)

        {

            if (ModelState.IsValid)
            {
                _unitofwork.Product.Update(obj);
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
            Product product  = _unitofwork.Product.Get(item => item.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        public IActionResult Deletecategory(Product product)
        {
            _unitofwork.Product.Remove(product);
            _unitofwork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
