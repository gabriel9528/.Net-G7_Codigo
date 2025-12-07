using BOL.DataBaseEntities;

namespace DAL.DataServices
{
    public interface IStudentDataDAL
    {
        List<Student> GetSudentsData();
        string SaveStudentData(Student student);
        string UpdateStudentData(Student student);
        string DeleteStudentData(int studentId);
    }
}
