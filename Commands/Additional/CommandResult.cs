using System.Data;

namespace CourseBD.Commands
{
  public class CommandResult
  {
    private DataTable dataTableReturn_;
    public DataTable DataTable
    {
      get => dataTableReturn_;

      set
      {
        dataTableReturn_ = value;
      }
    }

    private string stringReturn_;
    public string String
    {
      get => stringReturn_;

      set
      {
        stringReturn_ = value;
      }
    }

    private int intReturn_;
    public int Int
    {
      get => intReturn_;

      set
      {
        intReturn_ = value;
      }
    }
  }
}
