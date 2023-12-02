using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CourseBD.Commands
{
  public class ShowProfit : Command
  {
    private string sql_ = @"SELECT * FROM course_func1(@from, @to)";

    public override async Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData())
    {
      CommandResult result = new CommandResult();
      DataTable profit = new DataTable();

      using (NpgsqlCommand cmd = new NpgsqlCommand(sql_, connection))
      {
        List<string> temp = new();
        temp = Months.months[commandData.Month];
        cmd.Parameters.AddWithValue("from", temp[0]);
        cmd.Parameters.AddWithValue("to", temp[1]);

        profit = await CreateTableWithCmdAsync(cmd);

        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            profit.Rows.Add(reader[0]);
          }
        }
      }

      result.DataTable = profit;

      return result;
    }
  }
}
