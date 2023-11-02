using System.Data;

namespace Infra;

public interface IDbConnectionFactory
{
    IDbConnection Create(string connectionString);
}