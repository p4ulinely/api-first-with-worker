using System.Data;

namespace Infra;

public class SqlDbConnectionFactory : IDbConnectionFactory
{
    public IDbConnection Create(string connectionString)
    {
        // return new SqlConnection(connectionString);
        throw new NotImplementedException();
    }
}
