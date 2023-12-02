using Npgsql;
using System.Threading.Tasks;

namespace CourseBD.Commands
{
  public class Update : Command
  {
    private string fromFile_ware_name_ = @"SELECT course_update_ware_name(@id, @name)";
    private string fromFile_ware_quantity_ = @"SELECT course_update_ware_quantity(@id, @quantity)";
    private string fromFile_ware_amount_ = @"SELECT course_update_ware_amount(@id, @amount)";

    private string fromFile_exp_ = @"SELECT course_update_exp(@id, @name)";

    private string sql_;

    public override async Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData())
    {
      CommandResult result = new CommandResult();

      if (commandData.Table == Enums.Tables.EXPENSE_ITEM)
      {
        sql_ = fromFile_exp_;

        using (NpgsqlTransaction transaction = connection.BeginTransaction())
        {
          using (NpgsqlCommand cmd = new NpgsqlCommand(sql_, connection))
          {
            cmd.Parameters.AddWithValue("id", commandData.Id);
            cmd.Parameters.AddWithValue("name", commandData.Name);

            await cmd.ExecuteNonQueryAsync();
          }

          await transaction.CommitAsync();
        }
      }

      else if (commandData.Table == Enums.Tables.WAREHOUSES)
      {
        if (commandData.Name != string.Empty)
        {
          using (NpgsqlTransaction transaction = connection.BeginTransaction())
          {
            using (NpgsqlCommand cmd = new NpgsqlCommand(fromFile_ware_name_, connection))
            {
              cmd.Parameters.AddWithValue("id", commandData.Id);
              cmd.Parameters.AddWithValue("name", commandData.Name);

              await cmd.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
          }
        }

        if (commandData.Quantity != null)
        {
          using (NpgsqlTransaction transaction = connection.BeginTransaction())
          {
            using (NpgsqlCommand cmd = new NpgsqlCommand(fromFile_ware_quantity_, connection))
            {
              cmd.Parameters.AddWithValue("id", commandData.Id);
              cmd.Parameters.AddWithValue("quantity", commandData.Quantity);

              await cmd.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
          }
        }

        if (commandData.Amount != null)
        {
          using (NpgsqlTransaction transaction = connection.BeginTransaction())
          {
            using (NpgsqlCommand cmd = new NpgsqlCommand(fromFile_ware_amount_, connection))
            {
              cmd.Parameters.AddWithValue("id", commandData.Id);
              cmd.Parameters.AddWithValue("amount", commandData.Amount);

              await cmd.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
          }
        }
      }

      result.Int = 1;

      return result;
    }
  }
}
