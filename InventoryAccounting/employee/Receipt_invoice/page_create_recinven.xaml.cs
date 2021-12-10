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

namespace InventoryAccounting.employee
{
    /// <summary>
    /// Логика взаимодействия для page_create_recinven.xaml
    /// </summary>
    public partial class page_create_recinven : Page
    {
        dbo.Receipt_Inventory a = null;
        dbo.Accounting_Card_Receipt c = null;
        public static int idInvoice { get; set; }
        public static int idInventory { get; set; }
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public static ObservableCollection<dbo.Receipt_Invoice> receipt { get; set; }
        public page_create_recinven(int idInv)
        {
            InitializeComponent();
            idInvoice = idInv;
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            receipt = new ObservableCollection<dbo.Receipt_Invoice>(Connection.connection.Receipt_Invoice.ToList());
            c = new dbo.Accounting_Card_Receipt();
            a = new dbo.Receipt_Inventory();
            this.DataContext = this;
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            var recInv = receipt.Where(c => c.ID_Receipt_Invoice == idInvoice).FirstOrDefault();
            a.ID_Receipt_Invoice = idInvoice;
            a.ID_Inventory = idInventory;
            a.Count = count.Text;
            Connection.connection.Receipt_Inventory.Add(a);
            Connection.connection.SaveChanges();
            MessageBox.Show("done");

            c.ID_Inventory = idInventory;
            c.Date = DateTime.Now;
            c.ID_Storage = recInv.ID_Storage;
            c.ID_Receipt_Invoice = idInvoice;
            Connection.connection.Accounting_Card_Receipt.Add(c);
            Connection.connection.SaveChanges();
            MessageBox.Show("done");

            NavigationService.Navigate(new page_recinven(idInvoice, Convert.ToInt32(recInv.ID_Employee)));
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
