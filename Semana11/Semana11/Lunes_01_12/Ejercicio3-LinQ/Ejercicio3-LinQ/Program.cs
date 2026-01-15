//Ejercicio 3: Filtrar y Seleccionar Números
//Dado un arreglo de números enteros, filtra los números que están entre 6 y 16
//(sin incluir ambos extremos) y almacénalos en una secuencia de enteros
//(IEnumerable<int>). Luego, recorre la secuencia resultante y muestra cada
//número en la consola.

int[] numbers = { 1, 5, 7, 11, 15, 2, 13, 21, 9 };

IEnumerable<int> valuesInt = numbers.Where(x => x > 6 && x < 16);

foreach (int value in valuesInt)
{
    Console.WriteLine(value);
}


//Ejercicio 4: Filtrar y Ordenar Cadenas
//Dado un arreglo de cadenas que representan postres, filtra aquellos que
//contienen la palabra "manzana" y ordénalos alfabéticamente.
//Muestra los resultados en la consola

string[] desserts = { "pay de manzana", "pastel de chocolate", "manzana caramelizada", "fresas con crema" };
IEnumerable<string> valuesString = from n in desserts
                                   where n.Contains("manzana")
                                   orderby n
                                   select n;

foreach (string value in valuesString)
{
    Console.WriteLine($"{value}");
}

//Ejercicio 6: Filtrar Números Pares y Ejecución Diferida
//Filtra los números pares de un arreglo y muestra los resultados.
//Cambia un valor en el arreglo original después de la primera iteración y
//observa cómo la ejecución diferida afecta los resultados.
int[] numbersArray = { 1, 5, 7, 11, 15, 2, 13, 21, 9 };

IEnumerable<int> valuesIntArray = from x in numbersArray
                                  where x % 2 == 0
                                  select x;

Console.WriteLine("-------------------------");
Console.WriteLine("-------------------------");
Console.WriteLine("Ejecucion Diferida");
Console.WriteLine("-------------------------");
//foreach (int value in valuesIntArray)
//{
//    Console.WriteLine(value);
//}

Console.WriteLine("Vamos a hacer un cambio");

List<int> examplesInt = [];
foreach(var item in valuesIntArray)
{
    examplesInt.Add(item);
    Console.WriteLine(item);
}

numbersArray[1] = 8;

Console.WriteLine(examplesInt.Count);

//Ejercicio 7: Filtrar Números Pares y Ejecución Inmediata
//Filtra los números pares de un arreglo y almacénalos en un arreglo y una lista.
//Demuestra la ejecución inmediata cambiando un valor en el arreglo original
//después de la ejecución, y muestra los resultados.
Console.WriteLine("Ejecucion inmediata: ");
Console.WriteLine("----------------");

int[] numbersTest = { 1, 5, 7, 11, 15, 2, 13, 21, 9 };

int[] deferedValuesArray = (from n in numbersTest where n % 2 == 0 select n).ToArray();

List<int> deferedValuesInt = (from n in numbersTest where n % 2 == 0 select n).ToList();

Console.WriteLine("El areglo");

foreach (var item in deferedValuesArray)
{
    Console.WriteLine(item); // -> 2
}

numbersTest[0] = 8;

Console.WriteLine("Se realizo un cambio");

foreach(var item in deferedValuesArray)
{
    Console.WriteLine(item); // -> 2 y el 8 o solo el 2
}






