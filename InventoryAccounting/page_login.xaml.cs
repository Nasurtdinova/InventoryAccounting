using InventoryAccounting.admin;
using InventoryAccounting.employee;
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

namespace InventoryAccounting
{
    /// <summary>
    /// Логика взаимодействия для page_login.xaml
    /// </summary>
    public partial class page_login : Page
    {
        public static ObservableCollection<dbo.Employee> employee { get; set; }

        public page_login()
        {         
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            employee = new ObservableCollection<dbo.Employee>(Connection.connection.Employee.ToList());
            var k = employee.Where(a => a.Login == txt_login.Text && a.Password == txt_password.Password).FirstOrDefault();
            if (k != null)
            {
                NavigationService.Navigate(new page_employee(k.Name, Convert.ToInt32(k.ID_Employee)));
            }
            else if (txt_login.Text == "2002" && txt_password.Password == "2003")
            {
                NavigationService.Navigate(new page_admin());
            }
            else
            {
                MessageBox.Show("Login or password is not correct", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void registr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new page_register());
        }
    }
}
