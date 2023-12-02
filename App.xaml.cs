using System;
using System.Windows;

using CourseBD.Views;

namespace CourseBD
{
  public partial class App : Application
  {
    [STAThread]
    public static void Main()
    {
      App app = new App();
      app.ShutdownMode = ShutdownMode.OnLastWindowClose;

      BaseInteraction dataBase = new BaseInteraction();

      SecondWindow window = new SecondWindow(dataBase);

      app.StartSetup(app, dataBase);
      app.Run(window);
      app.EndSetup(dataBase);
    }

    private void StartSetup(App app, BaseInteraction dataBase)
    {
      dataBase.ConnectToBase();
      dataBase.PrintBaseVersion(dataBase);
    }

    private void EndSetup(BaseInteraction dataBase)
    {
      dataBase.CloseConnection();
    }
  }
}
