using System.Collections.Generic;
using System.Windows;

using CourseBD.Users;
using CourseBD.Enums;
using CourseBD.Commands;

namespace CourseBD.Views
{
  public partial class SecondWindow : Window
  {
    private BaseInteraction dataBase_;
    private SecondWindow window_;

    private readonly Dictionary<string, ActiveUser> userName_ = new()
    {
      { "admin", ActiveUser.ADMIN },
      { "seller", ActiveUser.SELLER }
    };

    public SecondWindow(BaseInteraction dataBase)
    {
      dataBase_ = dataBase;
      window_ = this;

      InitializeComponent();
    }

    private async void ButtonPassword(object sender, RoutedEventArgs e)
    {
      User user = new();
      user = window_.FillDataUser(user);

      string newHash = user.PasswordHashing();
      Command command = new PasswordComparison();
      CommandData data = new();
      data = window_.FillDataCommand(data);

      if (newHash.Equals((await command.ExecuteAsync(dataBase_.GetConnection(), data)).String))
      {
        MainWindow mainWindow = new(dataBase_, user);
        mainWindow.Show();

        Close();
      }

      else
      {
        MessageBox.Show("Пароль неверный");
      }
    }

    private User FillDataUser(User user)
    {
      user.UserName = userName_[UserName.Text];
      user.UserPassword = Password.Text;

      return user;
    }

    private CommandData FillDataCommand(CommandData data)
    {
      data.UserName = UserName.Text;

      return data;
    }
  }
}