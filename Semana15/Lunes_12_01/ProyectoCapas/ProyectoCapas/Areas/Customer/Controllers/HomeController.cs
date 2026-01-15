using Microsoft.AspNetCore.Mvc;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Models;
using ProyectoCapas.Models.ViewModel;
using System.Diagnostics;

namespace ProyectoCapas.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult ResultSearch(string searchString, int page = 1, int pageSize = 6)
        {
            var articles = _unitOfWork.IArticleRepository.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(x => x.Name.ToLower().Contains(searchString.ToLower())
                || x.Description.ToLower().Contains(searchString.ToLower()));
            }

            //Contar los elementos despues de aplicar el filtro
            var totalArticles = articles.Count();

            //Paginar los resultados
            var paginatedEntries = articles.Skip((page-1)*pageSize).Take(pageSize);

            //Crear modelo de la vista
            PaginatedList<Article> articlesPaginated =
                new PaginatedList<Article>(paginatedEntries.ToList(), totalArticles, page, pageSize, searchString);

            return View(articlesPaginated);
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    HomeViewModel homeViewModel = new HomeViewModel()
        //    {
        //        listArticles = _unitOfWork.IArticleRepository.GetAll(includeProperties: "Category"),
        //        listSliders = _unitOfWork.ISliderRepository.GetAll()
        //    };

        //    ViewBag.IsHome = true;
        //    return View(homeViewModel);
        //}

        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            var articles = _unitOfWork.IArticleRepository.AsQueryable();
            var paginatedEntries = articles.Skip((page - 1) * pageSize).Take(pageSize);

            HomeViewModel homeViewModel = new HomeViewModel()
            {
                listArticles = paginatedEntries.ToList(),
                listSliders = _unitOfWork.ISliderRepository.GetAll(),
                PageIndex = page,
                TotalPages = (int)Math.Ceiling(articles.Count() / (double)pageSize)
            };

            ViewBag.IsHome = true;
            return View(homeViewModel);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var articleFromDb = _unitOfWork.IArticleRepository.
                GetFirstOrDefault(includeProperties: "Category", filter: x => x.Id == id);

            return View(articleFromDb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
