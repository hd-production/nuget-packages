using Npgsql;

namespace HdProduction.Npgsql.Orm
{
  public interface IDatabaseConnector
  {
    NpgsqlConnection NewConnection();
    PostgresDataContext NewDataContext();
  }

  public class DatabaseConnector : IDatabaseConnector
  {
    private readonly string _connection;

    public DatabaseConnector(string connection)
    {
      _connection = connection;
    }

    public NpgsqlConnection NewConnection()
    {
      return new NpgsqlConnection(_connection);
    }

    public PostgresDataContext NewDataContext()
    {
      return new PostgresDataContext(_connection);
    }
  }
}