using Npgsql;
using System.Threading.Tasks;

namespace CourseBD.Commands
{
  public class Delete : Command
  {
    private string fromFile_ware_ = $"SELECT course_delete_ware(@id);";
    private string fromFile_exp_ = $"SELECT course_delete_exp(@id)";

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
          cmd.Parameters.AddWithValue("id", commandData.Id);

          await cmd.ExecuteNonQueryAsync();
        }

        await transaction.CommitAsync();
      }

      result.Int = 1;

      return result;
    }
  }
}
