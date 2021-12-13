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
    /// Логика взаимодействия для page_redak_inventory.xaml
    /// </summary>
    public partial class page_redak_inventory : Page
    {
        public static ObservableCollection<dbo.Type_Inventory> typeInventory { get; set; }
        public static ObservableCollection<dbo.Image_Inventory> Image { get; set; }
        public static string image { get; set; }
        public static int idTypeInventory { get; set; }
        public static int idInventory { get; set; }
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public page_redak_inventory(int id)
        {
            InitializeComponent();
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            typeInventory = new ObservableCollection<dbo.Type_Inventory>(Connection.connection.Type_Inventory.ToList());
            Image = new ObservableCollection<dbo.Image_Inventory>(Connection.connection.Image_Inventory.ToList());
            idInventory = id;
            var inv = inventory.Where(c => c.ID_Inventory == idInventory).FirstOrDefault();
            image = inv.Image;
            name_txt.Text = inv.Name;
            idTypeInventory = Convert.ToInt32(inv.ID_Type_Inventory);
            image_txt.Text = inv.Image;
            inventory_txt.Text = inv.Type_Inventory.Name;
            this.DataContext = this;
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var inv = inventory.Where(c => c.ID_Inventory == idInventory).FirstOrDefault();
            inv.ID_Type_Inventory = idTypeInventory;
            inv.Name = name_txt.Text;
            inv.Image = image;
            Connection.connection.SaveChanges();
            MessageBox.Show("Done");

            NavigationService.Navigate(new page_inventory());
        }

        private void inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Type_Inventory;
            idTypeInventory = a.ID_Type_Inventory;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void image_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Image_Inventory;
            image = a.Name;
        }
    }
}
