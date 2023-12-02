using System;
using System.Threading.Tasks;
using Npgsql;

namespace CourseBD
{
  public class BaseInteraction
  {
    private string connectData_;
    private NpgsqlConnection connection_;

    public void ConnectToBase()
    {
      NpgsqlConnectionStringBuilder tempBuilder = new NpgsqlConnectionStringBuilder();
      tempBuilder.Database = "task1";
      tempBuilder.Username = "postgres";
      tempBuilder.Password = "astralia7800";
      tempBuilder.Port = 5432;
      tempBuilder.Host = "localhost";
      tempBuilder.SslMode = SslMode.Prefer;
      tempBuilder.TrustServerCertificate = true;

      connectData_ = tempBuilder.ToString();

      connection_ = new NpgsqlConnection(connectData_);
      connection_.Open();
    }

    public void CloseConnection()
    {
      connection_.Close();
    }

    public NpgsqlConnection GetConnection()
    {
      return connection_;
    }

    private async Task<string> CheckVersionAsync()
    {
      Commands.Command command = new Commands.CheckVersion();
      return (await command.ExecuteAsync(connection_)).String;
    }

    public async void PrintBaseVersion(BaseInteraction dataBase)
    {
      Console.WriteLine($"PostgreSQL version: {await dataBase.CheckVersionAsync()}");
    }
  }
}
