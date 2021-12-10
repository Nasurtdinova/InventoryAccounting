using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryAccounting.employee.Expenditure_invoice
{
    /// <summary>
    /// Логика взаимодействия для page_create_expeninven.xaml
    /// </summary>
    public partial class page_create_expeninven : Page
    {
        dbo.Expenditure_Inventory a = null;
        public static int idInvoice { get; set; }
        public static int idInventory { get; set; }
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public static ObservableCollection<dbo.Expenditure_Invoice> expen { get; set; }
        dbo.Accounting_Card_Expenditure c = null;
        public page_create_expeninven(int idInv)
        {
            InitializeComponent();
            idInvoice = idInv;
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            expen = new ObservableCollection<dbo.Expenditure_Invoice>(Connection.connection.Expenditure_Invoice.ToList());
            a = new dbo.Expenditure_Inventory();
            c = new dbo.Accounting_Card_Expenditure();
            this.DataContext = this;
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            var recInv = expen.Where(c => c.ID_Expenditure_Invoice == idInvoice).FirstOrDefault();
            a.ID_Expenditure_Invoice = idInvoice;
            a.ID_Inventory = idInventory;
            a.Count = count.Text;
            Connection.connection.Expenditure_Inventory.Add(a);
            Connection.connection.SaveChanges();
            MessageBox.Show("done");

            c.ID_Inventory = idInventory;
            c.Date = DateTime.Now;
            c.ID_Storage = recInv.ID_Storage;
            c.ID_Expenditure_Invoice = idInvoice;
            Connection.connection.Accounting_Card_Expenditure.Add(c);
            Connection.connection.SaveChanges();
            MessageBox.Show("done");

            NavigationService.Navigate(new page_expen_inven(idInvoice, Convert.ToInt32(recInv.ID_Employee)));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Inventory;
            idInventory = a.ID_Inventory;
        }
    }
}
