using Npgsql;
using System.Threading.Tasks;

namespace CourseBD.Commands
{
  public class PasswordComparison : Command
  {
    private string sql_ = "SELECT password FROM users WHERE name=@name";

    public override async Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData())
    {
      CommandResult result = new CommandResult();

      using (NpgsqlCommand cmd = new NpgsqlCommand(sql_, connection))
      {
        cmd.Parameters.AddWithValue("name", commandData.UserName);

        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            result.String = reader[0].ToString();
          }
        }
      }

      return result;
    }
  }
}
