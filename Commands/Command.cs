using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace CourseBD.Commands
{
  public abstract class Command
  {
    public abstract Task<CommandResult> ExecuteAsync(NpgsqlConnection connection, CommandData commandData = new CommandData());

    protected string SelectTable(CommandData commandData)
    {
      switch (commandData.Table)
      {
        case Enums.Tables.WAREHOUSES:
          return commandData.SqlForWare;

        case Enums.Tables.EXPENSE_ITEM:
          return commandData.SqlForExp;

        default:
          return string.Empty;
      }
    }

    protected async Task<DataTable> CreateTableWithCmdAsync(NpgsqlCommand cmd)
    {
      DataTable result = new DataTable();

      await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
      {
        int i = 0;
        while (await reader.ReadAsync())
        {
          if (i < reader.FieldCount)
          {
            result.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
            i++;
          }
        }
      }

      return result;
    }
  }
}
