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

namespace InventoryAccounting.employee
{
    /// <summary>
    /// Логика взаимодействия для page_employee.xaml
    /// </summary>
    public partial class page_employee : Page
    {
        int id { get; set; }
        public page_employee(string name, int idEmployee)
        {
            InitializeComponent();
            id = idEmployee;
            name_tb.Text = $"Welcome, {name}!";
        }

        private void btn_receipt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new page_receipt(id));
        }

        private void btn_expen_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Expenditure_invoice.page_expen(id));
        }

        private void btn_exit_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new page_login());
        }

        private void btn_card_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Card.page_card());
        }

        private void btn_inventory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new page_inventory());
        }

        private void btn_storage_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new page_storage());
        }
    }
}
