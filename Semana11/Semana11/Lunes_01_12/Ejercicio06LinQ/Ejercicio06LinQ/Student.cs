namespace Ejercicio06LinQ
{
    internal class Student
    {
        private string _name;
        private string _id;
        private EnumCourse _course;
        private double _average;

        public string Name { get { return _name; } set { _name = value; } }
        public string Id { get { return _id; } set { _id = value; } }
        public EnumCourse Course { get { return _course; } set { _course = value; } }
        public double Average { get { return _average; } set { _average = value; } }

        public Student(string Name, string Id, EnumCourse Course, double Average)
        {
            _name = Name;
            _id = Id;
            _course = Course;
            _average = Average;
        }

        public override string ToString()
        {
            return string.Format("Nombre {0}, {1}, curso: {2}, promedio {3}", _name, _id, _course, _average);
        }
    }
}
