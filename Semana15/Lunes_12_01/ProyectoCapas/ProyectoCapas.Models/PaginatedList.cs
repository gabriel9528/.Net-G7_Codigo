namespace ProyectoCapas.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; } // Pagina actual
        public int TotalPages { get; private set; } //Total de paginas
        public string SearchString { get; private set; } //Texto de busqueda opcional

        //items:  la lista de elementos en la pagina actual
        //count: Numero total de elementos en la colleccion original
        //pageSize: cantidad de elementos por pagina
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, string searchString)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SearchString = searchString;
            //this.AddRange -> agrega los elementos de items a la lista paginada, que extiende de List<T>
            this.AddRange(items);
        }

        //Retorna true si hay una pagina anterior
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        //Retorna true si hay una pagina siguiente, es decir si el usuario no esta en la ultima pagina
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
