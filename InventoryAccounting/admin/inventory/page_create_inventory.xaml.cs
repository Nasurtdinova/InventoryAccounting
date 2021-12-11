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

namespace InventoryAccounting.admin.inventory
{
    /// <summary>
    /// Логика взаимодействия для page_create_inventory.xaml
    /// </summary>
    public partial class page_create_inventory : Page
    {
        public static ObservableCollection<dbo.Type_Inventory> typeInventory { get; set; }
        public static int idTypeInventory { get; set; }
        dbo.Inventory a = null;
        public page_create_inventory()
        {
            InitializeComponent();
            typeInventory = new ObservableCollection<dbo.Type_Inventory>(Connection.connection.Type_Inventory.ToList());
            a = new dbo.Inventory();
            this.DataContext = this;
        }

        private void inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Type_Inventory;
            idTypeInventory = a.ID_Type_Inventory;
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            a.ID_Type_Inventory = idTypeInventory;
            a.Name = name_txt.Text;
            Connection.connection.Inventory.Add(a);
            Connection.connection.SaveChanges();
            MessageBox.Show("done");

            NavigationService.Navigate(new page_inventory());
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
