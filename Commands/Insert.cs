using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace CourseBD.Commands
{
  public class Insert : Command
  {
    private string fromFile_ware_ = $"SELECT course_insert_ware(@name, @quantity, @amount)";
    private string fromFile_exp_ = $"SELECT course_insert_exp(@name)";

    private string sql_;

    public override async Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData())
    {
      CommandResult result = new CommandResult();

      commandData.SqlForWare = fromFile_ware_;
      commandData.SqlForExp = fromFile_exp_;
      sql_ = SelectTable(commandData);

      using (NpgsqlTransaction transaction = connection.BeginTransaction())
      {
        using (NpgsqlCommand cmd = new NpgsqlCommand(sql_, connection))
        {
          cmd.Parameters.AddWithValue("name", commandData.Name != string.Empty ? commandData.Name : "");

          if (commandData.Table == Enums.Tables.WAREHOUSES)
          {
            cmd.Parameters.AddWithValue("quantity", commandData.Quantity != null ? commandData.Quantity : 0);
            cmd.Parameters.AddWithValue("amount", commandData.Amount != null ? commandData.Amount : 0f);
          }

          try
          {
            await cmd.ExecuteNonQueryAsync();
          }

          catch (Npgsql.PostgresException ex)
          {
            MessageBox.Show(ex.MessageText.ToString());
          }
        }

        await transaction.CommitAsync();
      }

      result.Int = 1;

      return result;
    }
  }
}
