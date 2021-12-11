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
    public partial class page_redak_storage : Page
    {
        public static int idStorage { get; set; }
        public static ObservableCollection<dbo.Storage> storage { get; set; }
        public page_redak_storage(int id)
        {
            InitializeComponent();
            idStorage = id;
            storage = new ObservableCollection<dbo.Storage>(Connection.connection.Storage.ToList());
            var inv = storage.Where(c => c.ID_Storage == idStorage).FirstOrDefault();
            name_txt.Text = inv.Name;
            phone_txt.Text = inv.Phone.ToString();
            this.DataContext = this;
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var inv = storage.Where(c => c.ID_Storage == idStorage).FirstOrDefault();
            inv.Phone = Convert.ToInt32(phone_txt.Text);
            inv.Name = name_txt.Text;
            Connection.connection.SaveChanges();
            MessageBox.Show("Done");

            NavigationService.Navigate(new page_storage());
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
