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
    /// Логика взаимодействия для page_inventory.xaml
    /// </summary>
    public partial class page_inventory : Page
    {
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public page_inventory()
        {
            InitializeComponent();
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            this.DataContext = this;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel listViewItem = (StackPanel)button.Parent;
            TextBlock label = (TextBlock)listViewItem.Children[0];
            string text = label.Text;
            var inv = inventory.Where(c => c.ID_Inventory == Convert.ToInt32(text)).FirstOrDefault();
            if (MessageBox.Show($"Remove {text}?", "question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Connection.connection.Inventory.Remove(inv);
                Connection.connection.SaveChanges();
                MessageBox.Show("Done");
            }
            NavigationService.Navigate(new page_inventory());
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel listViewItem = (StackPanel)button.Parent;
            TextBlock label = (TextBlock)listViewItem.Children[0];

            string text = label.Text;
            var inv = inventory.Where(c => c.ID_Inventory == Convert.ToInt32(text)).FirstOrDefault();
            if (MessageBox.Show($"Edit {text}?", "question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new page_redak_inventory(inv.ID_Inventory));
            }

        }
        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new page_create_inventory());
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btn_main_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new page_admin());
        }
    }
}
