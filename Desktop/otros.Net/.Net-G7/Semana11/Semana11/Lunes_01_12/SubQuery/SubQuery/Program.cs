class Program
{
    static void Main(string[] args)
    {
        string[] desserts = { 
            "pay de manzana", 
            "pastel de chocolate", 
            "manzana caramelizada", 
            "fresas con crema" 
        };

        //Ordenar la cadena en orden ascendente segun la ultima palabra
        IEnumerable<string> queryDesserts = desserts.OrderBy(x => x.Split().Last());

        //Console.WriteLine("Ordenar la cadena en orden ascendente segun la ultima palabra");
        //foreach(string dessert in queryDesserts)
        //{
        //    Console.WriteLine(dessert);
        //}


        int[] numbers = { 19, 4, 56, 32, 11, 8, 45, 7, 18, 2, 17, 23 };
        //Filtrar aquellos numeros que sean menores al primer elemento de la lista

        IEnumerable<int> queryNumbers = numbers.Where(x => x < numbers.First());

        //foreach(var item in queryNumbers)
        //{
        //    Console.WriteLine(item);
        //}

        //Filtrar aquellos numeros que son menores al primer numero par de nuestra lista

        IEnumerable<int> evenNumbers = numbers.Where(x => x < (numbers.Where(n => n % 2 == 0).First())); 
        foreach(var item in evenNumbers)
        {
            Console.WriteLine(item);
        }
    }
}