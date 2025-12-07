namespace DapperMVCDemo.Data.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> GetDataAsync<T, P>(string spName, P parameters,
            string connectionId = "DefaultConnection");

        Task SaveDataAsync<T>(string spName, T parameters,
            string connectionId = "DefaultConnection");
    }
}
