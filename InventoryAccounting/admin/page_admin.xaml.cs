using InventoryAccounting.employee;
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

namespace InventoryAccounting.admin
{
    /// <summary>
    /// Логика взаимодействия для page_admin.xaml
    /// </summary>
    public partial class page_admin : Page
    {
        public page_admin()
        {
            InitializeComponent();
            name_tb.Text = "Welcome, admin";
        }

        private void btn_receipt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new page_receipt(0));
        }

        private void btn_expen_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new employee.Expenditure_invoice.page_expen(0));
        }

        private void btn_exit_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new page_login());
        }

        private void btn_card_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new employee.Card.page_card());
        }

        private void btn_inventory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new admin.inventory.page_inventory());
        }

        private void btn_storage_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new admin.storage.page_storage());
        }
    }
}
