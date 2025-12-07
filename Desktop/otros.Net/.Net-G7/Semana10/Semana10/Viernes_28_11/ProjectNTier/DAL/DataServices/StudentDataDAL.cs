using BOL.DataBaseEntities;
using DAL.DataContext;
using Dapper;
using System.Data;

namespace DAL.DataServices
{
    public class StudentDataDAL : IStudentDataDAL
    {
        public IDapperConnectionHelper _dapperConnectionHelper { get; set; }
        public StudentDataDAL(IDapperConnectionHelper dapperConnectionHelper)
        {
            _dapperConnectionHelper = dapperConnectionHelper;
        }

        public List<Student> GetSudentsData()
        {
            List<Student> listStudents = new();
            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    string query = "select * from Student";
                    listStudents = dbConnection.Query<Student>(query, commandType: CommandType.Text).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listStudents;
        }

        public string SaveStudentData(Student student)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    string query = "insert into Student (FirstName, LastName, Email) " +
                        "values (@firstName, @lastName, @email)";
                    dbConnection.Execute(query, new
                    {
                        firstName = student.FirstName,
                        lastName = student.LastName,
                        email = student.Email,
                    }, commandType: CommandType.Text);

                    result = "Estudiante guardado con exito";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public string UpdateStudentData(Student student)
        {
            string result = "";
            try
            {
                using(IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    string query = "update Student set FirstName = @firstName, LastName = @lastName, Email = @email where Id = @id";
                    dbConnection.Execute(query, new
                    {
                        id = student.Id,
                        firstName = student.FirstName,
                        lastName = student.LastName,
                        email = student.Email,
                    }, commandType: CommandType.Text);

                    result = "Estudiante actualizado con exito";

                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public string DeleteStudentData(int studentId)
        {
            string result = "";
            try
            {
                using(IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    string query = "delete fron Student where Id = @id";
                    dbConnection.Execute(query, new
                    {
                        id = studentId
                    }, commandType: CommandType.Text);

                    result = "Estudiante eliminado con exito";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}
