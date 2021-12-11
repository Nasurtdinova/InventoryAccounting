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
    /// Логика взаимодействия для page_redak_expen_inventory.xaml
    /// </summary>
    public partial class page_redak_expen_inventory : Page
    {
        public static InvoiceInventory a;
        public static int idEmployee { get; set; }
        public static int idInventory { get; set; }
        public page_redak_expen_inventory(int id)
        {
            InitializeComponent();
            a = page_expen_inven.path;
            idEmployee = id;
            name.Text = a.NameInventory;
            invoice.Text = a.NameInvoice;
            count.Text = a.Count.ToString();
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<dbo.Expenditure_Inventory> recInvs = new ObservableCollection<dbo.Expenditure_Inventory>(Connection.connection.Expenditure_Inventory.ToList());
            var recInv = recInvs.Where(c => c.ID_Expenditure_Invoice == a.ID).FirstOrDefault();
            this.DataContext = this;
            if (idEmployee == 0)
            {
                if (MessageBox.Show($"Remove {a.ID}?", "question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Connection.connection.Expenditure_Inventory.Remove(recInv);
                    Connection.connection.SaveChanges();
                    MessageBox.Show("Done");
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("Недостаточно прав чтобы изменить!!!");
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<dbo.Expenditure_Inventory> recInvs = new ObservableCollection<dbo.Expenditure_Inventory>(Connection.connection.Expenditure_Inventory.ToList());
            var recInv = recInvs.Where(c => c.ID_Expenditure_Invoice == a.ID && c.Inventory.Name == a.NameInventory).FirstOrDefault();
            this.DataContext = this;
            if (idEmployee == recInv.Expenditure_Invoice.ID_Employee || idEmployee == 0)
            {
                recInv.ID_Expenditure_Invoice = a.ID;
                recInv.Count = count.Text;
                Connection.connection.SaveChanges();
                MessageBox.Show("Done");
            }
            else
            {
                MessageBox.Show("Недостаточно прав чтобы изменить!!!");
            }
        }
    }
}
