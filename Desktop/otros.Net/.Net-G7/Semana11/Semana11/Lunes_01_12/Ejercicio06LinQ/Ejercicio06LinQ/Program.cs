using Ejercicio06LinQ;

class Program
{
    public static void Main(string[] args)
    {
        List<Student> students = new List<Student>
        {
            new Student("Gabriel", "A100", EnumCourse.Mercadotecnia, 10.0),
            new Student("Luis", "S250", EnumCourse.Orientado_A_Objetos, 9.0),
            new Student("Juan", "S875", EnumCourse.Programacion_Basica, 5.0),
            new Student("Susana", "A432", EnumCourse.Mercadotecnia, 8.7),
            new Student("Pablo", "A156", EnumCourse.Mercadotecnia, 4.3),
            new Student("Alberto", "S456", EnumCourse.Orientado_A_Objetos, 8.3),
        };

        IEnumerable<Student> rejects = from n in students
                                       where n.Average <= 5.0
                                       select n;

        IEnumerable<Student> rejects2 = students.Where(x => x.Average <= 5.0);

        foreach(Student s in rejects)
        {
            Console.WriteLine(s);
        }

        IEnumerable<Student> listMarketing = students.Where(x => x.Course == EnumCourse.Mercadotecnia);

        IEnumerable<string> listMarketingMethod = students.Where(x => x.Course == EnumCourse.Mercadotecnia)
                                                      .Select(x => x.Name);

        IEnumerable<string> listMarketingQuery = from n in students
                                             where n.Course == EnumCourse.Mercadotecnia
                                             select n.Name;

        Console.WriteLine("Syntax Method");
        foreach(string s in listMarketingMethod)
        {
            Console.WriteLine(s);
        }

        Console.WriteLine("Syntax Query");
        foreach (string s in listMarketingQuery)
        {
            Console.WriteLine(s);
        }

    }
}