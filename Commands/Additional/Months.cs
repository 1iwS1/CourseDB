﻿using System.Collections.Generic;

namespace CourseBD.Commands
{
  public class Months
  {
    public static Dictionary<string, List<string>> months = new()
    {
      { "Январь", new() { "31.12.22", "1.02.23" } },
      { "Февраль", new() { "31.01.23", "1.03.23" } },
      { "Март", new() { "28.02.23", "1.04.23" } },
      { "Апрель", new() { "31.03.23", "1.05.23" } },
      { "Май", new() { "30.04.23", "1.06.23" } },
      { "Июнь", new() { "31.05.23", "1.07.23" } },
      { "Июль", new() { "30.06.23", "1.08.23" } },
      { "Август", new() { "31.07.23", "1.09.23" } },
      { "Сентябрь", new() { "31.08.23", "1.10.23" } },
      { "Октябрь", new() { "30.09.23", "1.11.23" } },
      { "Ноябрь", new() { "31.10.23", "1.12.23" } },
      { "Декабрь", new() { "30.11.23", "1.01.24" } },
    };
  }
}
