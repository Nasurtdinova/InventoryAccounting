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
    public partial class page_redak_Invoice_Inventory : Page
    {
        public static InvoiceInventory a;
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public static int idEmployee { get; set; }
        public static int idInventory { get; set; }
        public page_redak_Invoice_Inventory(int id)
        {
            InitializeComponent();
            a = page_recinven.path;
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            this.DataContext = this;
            idEmployee = id;
            name.Text = a.NameInventory;
            count.Text = a.Count.ToString();
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<dbo.Receipt_Inventory> recInvs = new ObservableCollection<dbo.Receipt_Inventory>(Connection.connection.Receipt_Inventory.ToList());
            var recInv = recInvs.Where(c => c.ID_Receipt_Invoice == a.ID).FirstOrDefault();

            if (idEmployee == recInv.Receipt_Invoice.ID_Employee || idEmployee == 0)
            {
                if (MessageBox.Show($"Remove {a.ID}?", "question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Connection.connection.Receipt_Inventory.Remove(recInv);
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
            ObservableCollection<dbo.Receipt_Inventory> recInvs = new ObservableCollection<dbo.Receipt_Inventory>(Connection.connection.Receipt_Inventory.ToList());
            var recInv = recInvs.Where(c => c.ID_Receipt_Invoice == a.ID).FirstOrDefault();
            this.DataContext = this;
            if (idEmployee == recInv.Receipt_Invoice.ID_Employee || idEmployee == 0)
            {
                recInv.ID_Receipt_Invoice = a.ID;
                recInv.Count = count.Text;
                //recInv.ID_Inventory = idInventory;
                Connection.connection.SaveChanges();
                MessageBox.Show("Done");
            }
            else
            {
                MessageBox.Show("Недостаточно прав чтобы изменить!!!");
            }
        }

        private void inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Inventory;
            idInventory = a.ID_Inventory;
        }
    }
}
