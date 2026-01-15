using Microsoft.AspNetCore.Mvc;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using System.Security.Claims;

namespace ProyectoCapas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var userCurrent = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var list = _unitOfWork.IUserRepository.GetAll(x => x.Id != userCurrent.Value);

            return View(_unitOfWork.IUserRepository.GetAll(x => x.Id != userCurrent.Value));
        }

        [HttpGet]
        public IActionResult Block(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            _unitOfWork.IUserRepository.BlockUser(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UnBlock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _unitOfWork.IUserRepository.UnBlockUser(id);

            return RedirectToAction("Index");
        }
    }
}
