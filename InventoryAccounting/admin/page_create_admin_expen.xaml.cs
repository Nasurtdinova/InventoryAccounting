using InventoryAccounting.employee.Expenditure_invoice;
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

namespace InventoryAccounting.admin
{
    public partial class page_create_admin_expen : Page
    {
        public static ObservableCollection<dbo.Storage> storage { get; set; }
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public static ObservableCollection<dbo.Employee> employee { get; set; }
        public static int idStorage { get; set; }
        public static int idInventory { get; set; }
        public static int idEmployee { get; set; }
        dbo.Expenditure_Inventory a = null;
        dbo.Expenditure_Invoice b = null;
        dbo.Accounting_Card_Expenditure c = null;
        public page_create_admin_expen()
        {
            InitializeComponent();
            storage = new ObservableCollection<dbo.Storage>(Connection.connection.Storage.ToList());
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            employee = new ObservableCollection<dbo.Employee>(Connection.connection.Employee.ToList());
            a = new dbo.Expenditure_Inventory();
            b = new dbo.Expenditure_Invoice();
            c = new dbo.Accounting_Card_Expenditure();
            this.DataContext = this;
        }

        private void storage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Storage;
            idStorage = a.ID_Storage;
        }

        private void inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Inventory;
            idInventory = a.ID_Inventory;
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            b.Name = name_txt.Text;
            b.Date = DateTime.Now;
            b.ID_Employee = idEmployee;
            b.ID_Storage = idStorage;
            Connection.connection.Expenditure_Invoice.Add(b);
            Connection.connection.SaveChanges();
            MessageBox.Show("done");

            a.ID_Inventory = idInventory;
            a.ID_Expenditure_Invoice = b.ID_Expenditure_Invoice;
            a.Count = count.Text;
            Connection.connection.Expenditure_Inventory.Add(a);
            Connection.connection.SaveChanges();
            MessageBox.Show("done");

            c.ID_Inventory = idInventory;
            c.Date = DateTime.Now;
            c.ID_Storage = idStorage;
            c.ID_Expenditure_Invoice = b.ID_Expenditure_Invoice;
            Connection.connection.Accounting_Card_Expenditure.Add(c);
            Connection.connection.SaveChanges();
            MessageBox.Show("done");

            NavigationService.Navigate(new page_expen_inven(b.ID_Expenditure_Invoice, idEmployee));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void employee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Employee;
            idEmployee = a.ID_Employee;
        }
    }
}
