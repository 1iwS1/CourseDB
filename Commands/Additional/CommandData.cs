using System;
using System.Collections.Generic;

namespace CourseBD
{
  public struct CommandData
  {
    public Enums.Tables Table { get; set; }
    public Int32? Id { get; set; }
    public string Name { get; set; }
    public Int32? Quantity { get; set; }
    public Single? Amount { get; set; }
    public string Condition { get; set; }
    
    public string Month { get; set; }
    public string DayFrom { get; set; }
    public string DayTo { get; set; }

    public string SqlForWare { get; set; }
    public string SqlForExp { get; set; }

    public string UserName { get; set; }
  }
}
