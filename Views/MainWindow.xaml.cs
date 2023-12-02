using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Aspose.Cells;

using CourseBD.Enums;
using CourseBD.Users;

namespace CourseBD.Views
{
  public partial class MainWindow : Window
  {
    private BaseInteraction dataBase_;
    private MainWindow window_;

    private Tables table_;
    private ActiveCommand activeCommand_;

    private readonly string workFilesPath_ = "../../WorkFiles/";
    private readonly string workFilesFullPath_ = @"C:\Users\Максим\source\repos\CourseBD\CourseBD\WorkFiles\";

    private Dictionary<ActiveCommand, Commands.Command> commandDict_ = new()
    {
      { ActiveCommand.INSERT, new Commands.Insert() },
      { ActiveCommand.UPDATE, new Commands.Update() },
      { ActiveCommand.DELETE, new Commands.Delete() },
      { ActiveCommand.FUNC_1, new Commands.ShowProfit() },
      { ActiveCommand.FUNC_2, new Commands.ShowByDates() },
      { ActiveCommand.SHOW_SALES, new Commands.ShowAllSales() },
      { ActiveCommand.SHOW_CHARGES, new Commands.ShowAllCharges() },
      { ActiveCommand.SHOW_WARE, new Commands.ShowAllWare() },
      { ActiveCommand.SHOW_EXP, new Commands.ShowAllExp() }
    };

    private Dictionary<Tables, string> tableToNameDict_ = new()
    {
      { Tables.WAREHOUSES, "warehouses" },
      { Tables.EXPENSE_ITEM, "expense_items" },
      { Tables.SALES, "sales" },
      { Tables.CHARGES, "charges" }
    };

    private DataTable sales_;
    private DataTable charges_;
    private DataTable warehouses_;
    private DataTable expense_items_;

    public MainWindow(BaseInteraction dataBase, User user)
    {
      window_ = this;
      dataBase_ = dataBase;

      InitializeComponent();
      window_.HideAllModifyElements();
      FuncCommandsView.Visibility = Visibility.Hidden;

      window_.UserChecking(user);
    }

    private void UserChecking(User user)
    {
      if (user.UserName == ActiveUser.SELLER)
      {
        Insert.Visibility = Visibility.Hidden;
        Update.Visibility = Visibility.Hidden;
        Delete.Visibility = Visibility.Hidden;
      }
    }

    private void ButtonSales(object sender, RoutedEventArgs e)
    {
      activeCommand_ = ActiveCommand.SHOW_SALES;
      table_ = Tables.SALES;

      window_.ClearAllInputs();
      window_.HideAllModifyElements();
      FuncCommandsView.Visibility = Visibility.Hidden;
      FuncGrid.Visibility = Visibility.Hidden;

      Download.Visibility = Visibility.Visible;
      Browse.Visibility = Visibility.Visible;
      window_.ShowSales();
    }
    private void ButtonCharges(object sender, RoutedEventArgs e)
    {
      activeCommand_ = ActiveCommand.SHOW_CHARGES;
      table_ = Tables.CHARGES;

      window_.ClearAllInputs();
      window_.HideAllModifyElements();
      FuncCommandsView.Visibility = Visibility.Hidden;
      FuncGrid.Visibility = Visibility.Hidden;

      Download.Visibility = Visibility.Visible;
      Browse.Visibility = Visibility.Visible;
      window_.ShowCharges();
    }

    private void ButtonWare(object sender, RoutedEventArgs e)
    {
      activeCommand_ = ActiveCommand.SHOW_WARE;
      table_ = Tables.WAREHOUSES;

      window_.ClearAllInputs();
      window_.HideAllModifyElements();
      FuncCommandsView.Visibility = Visibility.Hidden;
      FuncGrid.Visibility = Visibility.Hidden;

      Download.Visibility = Visibility.Visible;
      Browse.Visibility = Visibility.Visible;
      window_.ShowWare();
    }

    private void ButtonExp(object sender, RoutedEventArgs e)
    {
      activeCommand_ = ActiveCommand.SHOW_EXP;
      table_ = Tables.EXPENSE_ITEM;

      window_.ClearAllInputs();
      window_.HideAllModifyElements();
      FuncCommandsView.Visibility = Visibility.Hidden;
      FuncGrid.Visibility = Visibility.Hidden;

      Download.Visibility = Visibility.Visible;
      Browse.Visibility = Visibility.Visible;
      window_.ShowExp();
    }

    private void ButtonInsert(object sender, RoutedEventArgs e)
    {
      activeCommand_ = ActiveCommand.INSERT;

      Download.Visibility = Visibility.Hidden;
      Browse.Visibility = Visibility.Hidden;
      MainGrid.Visibility = Visibility.Hidden;
      FuncCommandsView.Visibility = Visibility.Hidden;
      FuncGrid.Visibility = Visibility.Hidden;

      ChooseTable.SelectedItem = null;
      window_.WarehousesElementsArrangement();
      window_.ClearAllInputs();

      ModifyCommandsView.Visibility = Visibility.Visible;
      window_.IsDeleteView();
    }

    private void ButtonUpdate(object sender, RoutedEventArgs e)
    {
      activeCommand_ = ActiveCommand.UPDATE;

      Download.Visibility = Visibility.Hidden;
      Browse.Visibility = Visibility.Hidden;
      MainGrid.Visibility = Visibility.Hidden;
      FuncCommandsView.Visibility = Visibility.Hidden;
      FuncGrid.Visibility = Visibility.Hidden;

      ChooseTable.SelectedItem = null;
      window_.WarehousesElementsArrangement();
      window_.ClearAllInputs();

      ModifyCommandsView.Visibility = Visibility.Visible;
      window_.IsDeleteView();
    }

    private void ButtonDelete(object sender, RoutedEventArgs e)
    {
      activeCommand_ = ActiveCommand.DELETE;

      Download.Visibility = Visibility.Hidden;
      Browse.Visibility = Visibility.Hidden;
      MainGrid.Visibility = Visibility.Hidden;
      FuncCommandsView.Visibility = Visibility.Hidden;
      FuncGrid.Visibility = Visibility.Hidden;

      ChooseTable.SelectedItem = null;
      window_.WarehousesElementsArrangement();
      window_.ClearAllInputs();

      ModifyCommandsView.Visibility = Visibility.Visible;
      window_.IsDeleteView();
    }

    private void ButtonShowProfit(object sender, RoutedEventArgs e)
    {
      activeCommand_ = ActiveCommand.FUNC_1;

      window_.HideAllModifyElements();
      MainGrid.Visibility = Visibility.Hidden;
      window_.ClearAllInputs();

      ChooseMonth.SelectedItem = null;

      Download.Visibility = Visibility.Hidden;
      Browse.Visibility = Visibility.Hidden;
      DateFrom.Visibility = Visibility.Hidden;
      DateTo.Visibility = Visibility.Hidden;
      ExecuteDate.Visibility = Visibility.Hidden;
      FuncGrid.Visibility = Visibility.Hidden;

      ChooseMonth.Visibility = Visibility.Visible;
      FuncCommandsView.Visibility = Visibility.Visible;
    }

    private void ButtonShowByDates(object sender, RoutedEventArgs e)
    {
      activeCommand_ = ActiveCommand.FUNC_2;

      window_.HideAllModifyElements();
      MainGrid.Visibility = Visibility.Hidden;
      window_.ClearAllInputs();

      Download.Visibility = Visibility.Hidden;
      Browse.Visibility = Visibility.Hidden;
      ChooseMonth.Visibility = Visibility.Hidden;
      FuncGrid.Visibility = Visibility.Hidden;

      DateFrom.SelectedDate = null;
      DateTo.SelectedDate = null;
      DateFrom.Visibility = Visibility.Visible;
      DateTo.Visibility = Visibility.Visible;
      ExecuteDate.Visibility = Visibility.Visible;
      FuncCommandsView.Visibility = Visibility.Visible;
    }

    private void ButtonClear(object sender, RoutedEventArgs e)
    {
      window_.ClearAllInputs();
    }

    private async void ButtonExecute(object sender, RoutedEventArgs e)
    {
      CommandData commandData = FillCommandData(new CommandData());

      if (activeCommand_ == ActiveCommand.DELETE && IdText.Text == string.Empty)
      {
        MessageBox.Show("nothing to delete");
      }

      else if (activeCommand_ == ActiveCommand.UPDATE && (NameText.Text == string.Empty && QuantityText.Text == string.Empty && AmountText.Text == string.Empty))
      {
        MessageBox.Show("nothing to update");
      }

      else if (activeCommand_ == ActiveCommand.INSERT && (NameText.Text == string.Empty && QuantityText.Text == string.Empty && AmountText.Text == string.Empty))
      {
        MessageBox.Show("nothing to insert");
      }

      else
      {
        await commandDict_[activeCommand_].ExecuteAsync(dataBase_.GetConnection(), commandData);
      }
    }

    private void ButtonExecuteDate(object sender, RoutedEventArgs e)
    {
      CommandData commandData = FillCommandData(new CommandData());

      if (commandData.DayFrom == string.Empty || commandData.DayTo == string.Empty)
      {
        MessageBox.Show("nothing to show...");
      }

      else
      {
        window_.ShowByDates(commandData);
      }
    }

    private async void ButtonDownload(object sender, RoutedEventArgs e)
    {
      await Task.Run(() => DownloadExecute());
    }

    private void DownloadExecute()
    {
      DataTable temp = DefineTable();

      StringBuilder fileName = new(string.Format("{0} {1}.xlsx", tableToNameDict_[table_], DateTime.Now));
      fileName = fileName.Replace(":", ".");

      Workbook workbook = new(); // создание файла excel
      Worksheet sheet = workbook.Worksheets[0]; // создание страницы
      sheet.Name = tableToNameDict_[table_];

      Cells cells = sheet.Cells;

      for (int i = 0; i < temp.Columns.Count; i++)
      {
        cells[0, i].PutValue(temp.Columns[i].ColumnName);
      }

      for (int i = 0; i < temp.Rows.Count; i++)
      {
        for (int k = 0; k < temp.Columns.Count; k++)
        {
          if (temp.Rows[i][k].GetType() == typeof(DateTime))
          {
            cells[1 + i, k].PutValue(temp.Rows[i][k].ToString());
          }

          else
          {
            cells[1 + i, k].PutValue(temp.Rows[i][k]);
          }
        }
      }

      sheet.AutoFitColumns();
      sheet.AutoFitRows();

      workbook.Save(workFilesPath_ + fileName.ToString(), SaveFormat.Xlsx);
    }

    private async void ButtonBrowse(object sender, RoutedEventArgs e)
    {
      await Task.Run(() => System.Diagnostics.Process.Start("explorer.exe", workFilesFullPath_));
    }

    private DataTable DefineTable()
    {
      switch (table_)
      {
        case Tables.SALES:
          return sales_;

        case Tables.CHARGES:
          return charges_;

        case Tables.WAREHOUSES:
          return warehouses_;

        case Tables.EXPENSE_ITEM:
          return expense_items_;

        default:
          return new DataTable();
      }
    }

    private void ListBoxController(object sender, SelectionChangedEventArgs e)
    {
      ListBoxItem item = (sender as ListBox).SelectedItem as ListBoxItem;

      window_.ClearAllInputs();

      if (item != null)
      {
        if (item.Content.ToString() == "Товары")
        {
          window_.WarehousesElementsArrangement();
          table_ = Tables.WAREHOUSES;
        }

        else if (item.Content.ToString() == "Статьи расходов")
        {
          window_.ExpenseItemsElementsArrangement();
          table_ = Tables.EXPENSE_ITEM;
        }
      }
    }

    private void ComboxController(object sender, SelectionChangedEventArgs e)
    {
      ComboBoxItem month = (sender as ComboBox).SelectedItem as ComboBoxItem;

      if (month != null)
      {
        CommandData commandData = new CommandData
        {
          Month = month.Content.ToString()
        };

        window_.ShowProfit(commandData);
      }
    }

    private async void ShowSales()
    {
      sales_ = (await commandDict_[activeCommand_].ExecuteAsync(dataBase_.GetConnection())).DataTable;

      MainGrid.Visibility = Visibility.Visible;
      MainGrid.ItemsSource = sales_.DefaultView;
    }

    private async void ShowCharges()
    {
      charges_ = (await commandDict_[activeCommand_].ExecuteAsync(dataBase_.GetConnection())).DataTable;

      MainGrid.Visibility = Visibility.Visible;
      MainGrid.ItemsSource = charges_.DefaultView;
    }

    private async void ShowWare()
    {
      warehouses_ = (await commandDict_[activeCommand_].ExecuteAsync(dataBase_.GetConnection())).DataTable;

      MainGrid.Visibility = Visibility.Visible;
      MainGrid.ItemsSource = warehouses_.DefaultView;
    }

    private async void ShowExp()
    {
      expense_items_ = (await commandDict_[activeCommand_].ExecuteAsync(dataBase_.GetConnection())).DataTable;

      MainGrid.Visibility = Visibility.Visible;
      MainGrid.ItemsSource = expense_items_.DefaultView;
    }

    private async void ShowProfit(CommandData commandData)
    {
      DataTable profit = (await commandDict_[activeCommand_].ExecuteAsync(dataBase_.GetConnection(), commandData)).DataTable;

      FuncGrid.Visibility = Visibility.Visible;
      FuncGrid.ItemsSource = profit.DefaultView;
    }

    private async void ShowByDates(CommandData commandData)
    {
      DataTable profitProducts = (await commandDict_[activeCommand_].ExecuteAsync(dataBase_.GetConnection(), commandData)).DataTable;

      FuncGrid.Visibility = Visibility.Visible;
      FuncGrid.ItemsSource = profitProducts.DefaultView;
    }

    private void HideAllModifyElements()
    {
      ModifyCommandsView.Visibility = Visibility.Hidden;
    }

    private void WarehousesElementsArrangement()
    {
      if (activeCommand_ != ActiveCommand.DELETE)
      {
        Quantity.Visibility = Visibility.Visible;
        Amount.Visibility = Visibility.Visible;
      }
    }

    private void ExpenseItemsElementsArrangement()
    {
      if (activeCommand_ != ActiveCommand.DELETE)
      {
        Quantity.Visibility = Visibility.Hidden;
        Amount.Visibility = Visibility.Hidden;
      }
    }

    private void ClearAllInputs()
    {
      IdText.Clear();
      NameText.Clear();
      QuantityText.Clear();
      AmountText.Clear();
    }

    private void IsDeleteView()
    {
      if (activeCommand_ == ActiveCommand.DELETE)
      {
        Name.Visibility = Visibility.Hidden;
        Quantity.Visibility = Visibility.Hidden;
        Amount.Visibility = Visibility.Hidden;
      }

      else
      {
        Name.Visibility = Visibility.Visible;
        Quantity.Visibility = Visibility.Visible;
        Amount.Visibility = Visibility.Visible;
      }
    }

    private CommandData FillCommandData(CommandData commandData)
    {
      commandData.Table = table_;

      commandData.Id = IdText.Text != string.Empty ? Int32.Parse(IdText.Text) : null;

      commandData.Name = NameText.Text != string.Empty ? NameText.Text : string.Empty;

      commandData.Quantity = QuantityText.Text != string.Empty ? Int32.Parse(QuantityText.Text) : null;

      commandData.Amount = AmountText.Text != string.Empty ? Single.Parse(AmountText.Text) : null;

      commandData.DayFrom = DateFrom.SelectedDate != null ? DateFrom.SelectedDate.ToString() : string.Empty;
      commandData.DayTo = DateTo.SelectedDate != null ? DateTo.SelectedDate.ToString() : string.Empty;

      return commandData;
    }
  }
}
