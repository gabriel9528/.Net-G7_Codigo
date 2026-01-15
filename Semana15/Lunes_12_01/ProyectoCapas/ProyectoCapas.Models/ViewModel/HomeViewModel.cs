namespace ProyectoCapas.Models.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Slider> listSliders { get; set; }
        public IEnumerable<Article> listArticles { get; set; }

        //Paginacion
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
