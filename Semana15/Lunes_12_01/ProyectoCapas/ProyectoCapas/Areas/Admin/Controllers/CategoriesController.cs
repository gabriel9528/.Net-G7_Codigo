using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Models;

namespace ProyectoCapas.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrador")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ICategoryRepository.Add(category);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(category);
        }
        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            else
            {
                Category category = new Category();
                category = _unitOfWork.ICategoryRepository.GetById(id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(category);
                }
            }
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ICategoryRepository.Update(category);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(category);
        }
        #endregion

        #region Call Apis

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.ICategoryRepository.GetAll(x => x.IsActive) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.ICategoryRepository.GetFirstOrDefault(x => x.Id == id && x.IsActive);
            if (category == null)
            {
                return Json(new { success = false, message = "La categoria no existe" });
            }
            _unitOfWork.ICategoryRepository.Delete(id);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Categoria eliminada exitosamente" });
        }

        #endregion
    }
}
