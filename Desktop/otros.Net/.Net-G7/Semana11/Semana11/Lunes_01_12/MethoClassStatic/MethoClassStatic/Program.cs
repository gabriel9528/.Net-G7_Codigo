
using MethoClassStatic;

class Program
{
    static void Main(string[] args)
    {
        IEnumerable<string> resultsDesserts = ExplicitClass.ListFounds.ToList();

        IEnumerable<int> resultEven = ExplicitClass.GetNumbersEvenDefered().ToList();

        IEnumerable<int> resultsOdd = ExplicitClass.GetNumbersOddExecuted();
        
    }
}