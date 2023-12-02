using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace CourseBD.Commands
{
  public class ShowAllWare : Command
  {
    private string sql_ = "SELECT * FROM course_show_ware()";

    public override async Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData())
    {
      CommandResult result = new CommandResult();

      DataTable warehouses = new DataTable();

      using (NpgsqlCommand cmd = new NpgsqlCommand(sql_, connection))
      {
        warehouses = await CreateTableWithCmdAsync(cmd);

        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            warehouses.Rows.Add(reader[0], reader[1], reader[2], reader[3]);
          }
        }
      }

      result.DataTable = warehouses;

      return result;
    }
  }
}
