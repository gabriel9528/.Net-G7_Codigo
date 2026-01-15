using Microsoft.AspNetCore.Mvc;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Models;

namespace ProyectoCapas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SlidersController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Create(Slider slider)
        {
            string mainRoute = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count() > 0)
            {
                string nameFile = Guid.NewGuid().ToString();
                string upload = Path.Combine(mainRoute, @"images\sliders");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(upload, nameFile + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                slider.UrlImage = @"\images\sliders\" + nameFile + extension;

                _unitOfWork.ISliderRepository.Add(slider);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(slider);
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
                Slider slider = new Slider();
                slider = _unitOfWork.ISliderRepository.GetById(id);
                if (slider == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(slider);
                }
            }
        }

        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            string mainRoute = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var sliderFromDb = _unitOfWork.ISliderRepository.GetById(slider.Id);

            if (files.Count() > 0)
            {
                string nameFile = Guid.NewGuid().ToString();
                string upload = Path.Combine(mainRoute, @"images\sliders");
                var extension = Path.GetExtension(files[0].FileName);

                //comprobar si la imagen existe
                var oldPath = Path.Combine(mainRoute, sliderFromDb.UrlImage.TrimStart('\\'));
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                //NUevamente subimos la imagen
                using (var fileStreams = new FileStream(Path.Combine(upload, nameFile + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                slider.UrlImage = @"\images\sliders\" + nameFile + extension;

                _unitOfWork.ISliderRepository.Update(slider);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                slider.UrlImage = sliderFromDb.UrlImage;
            }

            _unitOfWork.ISliderRepository.Update(slider);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Call Apis

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.ISliderRepository.GetAll(x => x.IsActive) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sliderFromDb = _unitOfWork.ISliderRepository.GetById(id);

            string mainRoute = _webHostEnvironment.WebRootPath;
            var pathOldImage = Path.Combine(mainRoute, sliderFromDb.UrlImage.TrimStart('\\'));

            //Comprobamos si la imagen existe
            if (System.IO.File.Exists(pathOldImage))
            {
                System.IO.File.Delete(pathOldImage);
            }


            if (sliderFromDb == null)
            {
                return Json(new { success = false, message = "El slider no existe" });
            }
            _unitOfWork.ISliderRepository.Delete(id);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Slider eliminado exitosamente" });
        }

        #endregion
    }
}
