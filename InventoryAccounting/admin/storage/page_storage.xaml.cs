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

namespace InventoryAccounting.admin.storage
{
    /// <summary>
    /// Логика взаимодействия для page_storage.xaml
    /// </summary>
    public partial class page_storage : Page
    {
        public static ObservableCollection<dbo.Storage> storage { get; set; }
        public page_storage()
        {
            InitializeComponent();
            storage = new ObservableCollection<dbo.Storage>(Connection.connection.Storage.ToList());
            this.DataContext = this;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel listViewItem = (StackPanel)button.Parent;
            TextBlock label = (TextBlock)listViewItem.Children[0];
            string text = label.Text;
            var inv = storage.Where(c => c.ID_Storage == Convert.ToInt32(text)).FirstOrDefault();
            if (MessageBox.Show($"Remove {text}?", "question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Connection.connection.Storage.Remove(inv);
                Connection.connection.SaveChanges();
                MessageBox.Show("Done");
  
            }
            NavigationService.Navigate(new page_storage());
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel listViewItem = (StackPanel)button.Parent;
            TextBlock label = (TextBlock)listViewItem.Children[0];

            string text = label.Text;
            var inv = storage.Where(c => c.ID_Storage == Convert.ToInt32(text)).FirstOrDefault();
            if (MessageBox.Show($"Edit {text}?", "question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new page_redak_storage(inv.ID_Storage));
            }

        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new page_create_storage());
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
