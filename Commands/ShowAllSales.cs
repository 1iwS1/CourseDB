using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace CourseBD.Commands
{
  public class ShowAllSales : Command
  {
    private string sql_ = "SELECT * FROM course_show_sales()";

    public override async Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData())
    {
      CommandResult result = new CommandResult();

      DataTable sales = new DataTable();

      using (NpgsqlCommand cmd = new NpgsqlCommand(sql_, connection))
      {
        sales = await CreateTableWithCmdAsync(cmd);

        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            sales.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4]);
          }
        }
      }

      result.DataTable = sales;

      return result;
    }
  }
}
