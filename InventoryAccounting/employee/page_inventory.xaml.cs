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
    public partial class page_inventory : Page
    {
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public page_inventory()
        {
            InitializeComponent();
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            this.DataContext = this;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
