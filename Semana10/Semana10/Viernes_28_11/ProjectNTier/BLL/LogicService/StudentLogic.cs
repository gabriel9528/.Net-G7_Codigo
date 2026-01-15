using BOL.DataBaseEntities;
using DAL.DataServices;

namespace BLL.LogicService
{
    public class StudentLogic : IStudentLogic
    {
        private readonly IStudentDataDAL _studentDataDAL;

        public StudentLogic(IStudentDataDAL studentDataDAL)
        {
            _studentDataDAL = studentDataDAL;
        }

        public List<Student> GetStudentLogic()
        {
            List<Student> students = new List<Student>();
            students = _studentDataDAL.GetSudentsData();
            return students;
        }

        public string SaveStudentLogic(Student student)
        {
            string result = "";
            if(string.IsNullOrEmpty(student.FirstName) 
                ||  string.IsNullOrEmpty(student.LastName) 
                || string.IsNullOrEmpty(student.Email))
            {
                result = "Es obligatorio llenar todos los campos";
            }

            result = _studentDataDAL.SaveStudentData(student);
            if(result == "Estudiante guardado con exito")
            {
                return result;
            }
            else
            {
                result = "Error al guardar el estudiante";
                return result;
            }
        }

        public string UpdateStudentLogic(Student student)
        {
            string result = "";
            if( student.Id <= 0
                || string.IsNullOrEmpty(student.FirstName) 
                || string.IsNullOrEmpty(student.LastName)
                || string.IsNullOrEmpty(student.Email))
            {
                result = "Es obligatorio llenar todos los campos";
                return result;
            }

            result = _studentDataDAL.UpdateStudentData(student);
            return result == "Estudiante actualizado con exito" 
                ? result 
                : "Error al actualizar el estudiante";
        }

        public string DeleteStudentLogic(int studentId)
        {
            if(studentId <= 0)
            {
                return "Id de estudiante no valido";
            }

            string result = "";
            result = _studentDataDAL.DeleteStudentData(studentId);
            return result == "Estudiante eliminado con exito" 
                ? result 
                : "Error al intentar eliminar el estudiante";
        }

        

        

        
    }
}
