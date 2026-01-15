using BOL.DataBaseEntities;

namespace BLL.LogicService
{
    public interface IStudentLogic
    {
        List<Student> GetStudentLogic();
        string SaveStudentLogic(Student student);
        string UpdateStudentLogic(Student student);
        string DeleteStudentLogic(int studentId);
    }
}
