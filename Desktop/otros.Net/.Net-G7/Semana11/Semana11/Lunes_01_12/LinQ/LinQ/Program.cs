public class Program
{
    public static void Main(string[] args)
    {
        //*-------------------Syntax Query------------------
        Console.WriteLine("Syntax Query");
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6 };

        var querySyntax = from n in numbers
                          where n > 2
                          select n;

        foreach(var item in querySyntax)
        {
            Console.WriteLine(item);
        }

        //*-----------------Syntax Method--------------------
        Console.WriteLine("Syntax Method");
        var methodSyntax = numbers.Where(x => x > 2);

        foreach(var item in methodSyntax)
        {
            Console.WriteLine(item);
        }

        //*----------------Syntax Mixed-----------------------
        Console.WriteLine("Syntax Mixed");
        var mixedSyntax = (from n in numbers
                           where n > 2
                           select n).Sum();

        Console.WriteLine("La suma de los numeros mayores a 2 es: " + mixedSyntax);
    }
}