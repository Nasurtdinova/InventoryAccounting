using InventoryAccounting.employee;
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
    /// <summary>
    /// Логика взаимодействия для page_admin_redak_receipt.xaml
    /// </summary>
    public partial class page_admin_redak_receipt : Page
    {
        public static ObservableCollection<dbo.Storage> storage { get; set; }
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public static ObservableCollection<dbo.Receipt_Invoice> receipt { get; set; }
        public static ObservableCollection<dbo.Employee> employee { get; set; }
        public static int idStorage { get; set; }
        public static int idInventory { get; set; }
        public static int idEmployee { get; set; }
        public static int idInv { get; set; }
        public page_admin_redak_receipt(int idInvoice)
        {
            InitializeComponent();
            storage = new ObservableCollection<dbo.Storage>(Connection.connection.Storage.ToList());
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            receipt = new ObservableCollection<dbo.Receipt_Invoice>(Connection.connection.Receipt_Invoice.ToList());
            employee = new ObservableCollection<dbo.Employee>(Connection.connection.Employee.ToList());
            idInv = idInvoice;
            var recInv = receipt.Where(c => c.ID_Receipt_Invoice == idInv).FirstOrDefault();
            idStorage = Convert.ToInt32(recInv.ID_Storage);
            idEmployee = Convert.ToInt32(recInv.ID_Employee);
            name_txt.Text = recInv.Name;
            storage_txt.Text = recInv.Storage.Name;
            employee_txt.Text = recInv.Employee.Name;
            this.DataContext = this;
        }

        private void employee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Employee;
            idEmployee = a.ID_Employee;
        }

        private void storage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Storage;
            idStorage = a.ID_Storage;
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {

            var recInv = receipt.Where(c => c.ID_Receipt_Invoice == idInv).FirstOrDefault();
            recInv.ID_Employee = idEmployee;
            recInv.ID_Storage = idStorage;
            recInv.Name = name_txt.Text;
            recInv.Date = DateTime.Now;
            Connection.connection.SaveChanges();
            MessageBox.Show("Done");

            NavigationService.Navigate(new page_receipt(0));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
