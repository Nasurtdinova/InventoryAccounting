using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для page_create_storage.xaml
    /// </summary>
    public partial class page_create_storage : Page
    {
        dbo.Storage a = null;
        public page_create_storage()
        {
            InitializeComponent();
            a = new dbo.Storage();
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            a.Phone = Convert.ToInt32(phone_txt.Text);
            a.Name = name_txt.Text;
            Connection.connection.Storage.Add(a);
            Connection.connection.SaveChanges();
            MessageBox.Show("done");

            NavigationService.Navigate(new page_storage());
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
