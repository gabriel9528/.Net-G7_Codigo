using System.Data;

namespace DAL.DataContext
{
    public interface IDapperConnectionHelper
    {
        IDbConnection GetDapperContextHelper();
    }
}
