public class Program
{
    public static void Main(string[] args)
    {
        //*---------------------IEnumerable-----------------------
        List<Employee> employees = new List<Employee>()
        {
            new Employee(){ Id = 1, Name = "Pepito", Salary = 10000 },
            new Employee(){ Id = 2, Name = "Andrea", Salary = 15000 },
            new Employee(){ Id = 3, Name = "Jorge", Salary = 20000 },
        };

        IEnumerable<Employee> query = from n in employees
                                      where n.Salary > 12000
                                      select n;
        foreach(var n in query)
        {
            Console.WriteLine(n.Name);
        }

        //*---------------------IQueryable----------------------------
        IQueryable<Employee> queryable = employees.AsQueryable();
        foreach(var n in queryable)
        {
            Console.WriteLine(n.Name);
        }

    }
}

public class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Salary { get; set; }
}