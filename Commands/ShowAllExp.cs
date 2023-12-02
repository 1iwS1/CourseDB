using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace CourseBD.Commands
{
  public class ShowAllExp : Command
  {
    private string sql_ = "SELECT * FROM course_show_exp()";

    public override async Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData())
    {
      CommandResult result = new CommandResult();
      DataTable expense_items = new DataTable();

      using (NpgsqlCommand cmd = new NpgsqlCommand(sql_, connection))
      {
        expense_items = await CreateTableWithCmdAsync(cmd);

        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            expense_items.Rows.Add(reader[0], reader[1]);
          }
        }
      }

      result.DataTable = expense_items;

      return result;
    }
  }
}
