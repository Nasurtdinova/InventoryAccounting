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
    /// Логика взаимодействия для page_recinven.xaml
    /// </summary>
    public partial class page_recinven : Page
    {
        public static ObservableCollection<dbo.Receipt_Invoice> receipt { get; set; }
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public static ObservableCollection<dbo.Receipt_Inventory> invInven { get; set; }
        public static InvoiceInventory path;
        public static IEnumerable<InvoiceInventory> result { get; set; }
        public static int idInvoice { get; set; }
        public static int idEmployee2 { get; set; }
        public page_recinven(int id, int idEmployee)
        {
            InitializeComponent();
            idInvoice = id;
            idEmployee2 = idEmployee;
            receipt = new ObservableCollection<dbo.Receipt_Invoice>(Connection.connection.Receipt_Invoice.ToList());
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            invInven = new ObservableCollection<dbo.Receipt_Inventory>(Connection.connection.Receipt_Inventory.ToList());
            result = from f in invInven
                     join t in inventory on f.ID_Inventory equals t.ID_Inventory
                     join k in receipt on f.ID_Receipt_Invoice equals k.ID_Receipt_Invoice
                     where idInvoice == f.ID_Receipt_Invoice
                     select new InvoiceInventory { ID = f.ID_Receipt_Invoice, NameInventory = t.Name, Count = Convert.ToInt32(f.Count), NameInvoice = k.Name };
            this.DataContext = this;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            path = (sender as ListView).SelectedItem as InvoiceInventory;
            NavigationService.Navigate(new page_redak_Invoice_Inventory(idEmployee2));
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            var recInv = receipt.Where(c => c.ID_Receipt_Invoice == idInvoice).FirstOrDefault();
            if (idEmployee2 == recInv.ID_Employee || idEmployee2 == 0)
            {
                NavigationService.Navigate(new page_create_recinven(idInvoice));
            }

            else
            {
                MessageBox.Show("Недостаточно прав пользователя!!!");
            }
        }
    }
}
