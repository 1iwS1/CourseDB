using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace CourseBD.Commands
{
  public class ShowAllCharges : Command
  {
    private string sql_ = "SELECT * FROM course_show_charges()";

    public override async Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData())
    {
      CommandResult result = new CommandResult();
      DataTable charges = new DataTable();

      using (NpgsqlCommand cmd = new NpgsqlCommand(sql_, connection))
      {
        charges = await CreateTableWithCmdAsync(cmd);

        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            charges.Rows.Add(reader[0], reader[1], reader[2], reader[3]);
          }
        }
      }

      result.DataTable = charges;

      return result;
    }
  }
}
