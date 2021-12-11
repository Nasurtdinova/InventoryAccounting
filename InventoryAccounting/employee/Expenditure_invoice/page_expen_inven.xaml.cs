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
    /// Логика взаимодействия для page_expen_inven.xaml
    /// </summary>
    public partial class page_expen_inven : Page
    {
        public static ObservableCollection<dbo.Expenditure_Invoice> expen { get; set; }
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public static ObservableCollection<dbo.Expenditure_Inventory> expInven { get; set; }
        public static InvoiceInventory path;
        public static IEnumerable<InvoiceInventory> result { get; set; }
        public static int idInvoice { get; set; }
        public static int idEmployee2 { get; set; }
        public page_expen_inven(int id, int idEmployee)
        {
            InitializeComponent();
            idInvoice = id;
            idEmployee2 = idEmployee;
            expen = new ObservableCollection<dbo.Expenditure_Invoice>(Connection.connection.Expenditure_Invoice.ToList());
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            expInven = new ObservableCollection<dbo.Expenditure_Inventory>(Connection.connection.Expenditure_Inventory.ToList());
            result = from f in expInven
                     join t in inventory on f.ID_Inventory equals t.ID_Inventory
                     join k in expen on f.ID_Expenditure_Invoice equals k.ID_Expenditure_Invoice
                     where idInvoice == f.ID_Expenditure_Invoice
                     select new InvoiceInventory { ID = f.ID_Expenditure_Invoice, NameInventory = t.Name, Count = Convert.ToInt32(f.Count), NameInvoice=k.Name };
            this.DataContext = this;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            path = (sender as ListView).SelectedItem as InvoiceInventory;
            NavigationService.Navigate(new page_redak_expen_inventory(idEmployee2));
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            var recInv = expen.Where(c => c.ID_Expenditure_Invoice == idInvoice).FirstOrDefault();
            if (idEmployee2 == recInv.ID_Employee || idEmployee2 == 0)
            {
                NavigationService.Navigate(new page_create_expeninven(idInvoice));
            }

            else
            {
                MessageBox.Show("Недостаточно прав пользователя!!!");
            }
        }
    }
}
