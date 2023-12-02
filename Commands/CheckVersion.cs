using System.Threading.Tasks;
using Npgsql;

namespace CourseBD.Commands
{
  public class CheckVersion : Command
  {
    private string sql_ = "SELECT version()";

    public override async Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData())
    {
      CommandResult result = new CommandResult();

      using (NpgsqlCommand cmd = new NpgsqlCommand(sql_, connection))
      {
        object table = await cmd.ExecuteScalarAsync();

        result.String = table.ToString();

        return result;
      }
    }
  }
}
