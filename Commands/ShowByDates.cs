using Npgsql;
using System.Data;
using System.Threading.Tasks;

namespace CourseBD.Commands
{
  public class ShowByDates : Command
  {
    private string sql_ = @"SELECT * FROM course_func2(@from, @to)";

    public override async Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData())
    {
      CommandResult result = new CommandResult();
      DataTable profitProducts = new DataTable();

      using (NpgsqlCommand cmd = new NpgsqlCommand(sql_, connection))
      {
        cmd.Parameters.AddWithValue("from", commandData.DayFrom);
        cmd.Parameters.AddWithValue("to", commandData.DayTo);

        profitProducts = await CreateTableWithCmdAsync(cmd);

        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            profitProducts.Rows.Add(reader[0], reader[1]);
          }
        }
      }

      result.DataTable = profitProducts;

      return result;
    }
  }
}
