using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.DataContext
{
    public class DapperConnectionHelper : IDapperConnectionHelper
    {
        public readonly IConfiguration _configuration;

        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
        public DapperConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            ProviderName = "System.Data.SqlClient";
        }

        public IDbConnection GetDapperContextHelper()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
