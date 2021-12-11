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
    /// <summary>
    /// Логика взаимодействия для page_receipt.xaml
    /// </summary>
    public partial class page_receipt : Page
    {
        public static ObservableCollection<dbo.Receipt_Invoice> receipt { get; set; }
        public static ObservableCollection<dbo.Employee> employee { get; set; }
        public static int idEmployee { get; set; }
        public page_receipt(int id)
        {
            InitializeComponent();
            idEmployee = id;
            receipt = new ObservableCollection<dbo.Receipt_Invoice>(Connection.connection.Receipt_Invoice.ToList());
            employee = new ObservableCollection<dbo.Employee>(Connection.connection.Employee.ToList());
            this.DataContext = this;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            if (idEmployee == 0)
            {
                NavigationService.Navigate(new admin.page_create_receipt());
            }
            else
            {
                NavigationService.Navigate(new page_create_receipt(idEmployee));
            }
            
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ListView).SelectedItem as dbo.Receipt_Invoice;
            NavigationService.Navigate(new page_recinven(a.ID_Receipt_Invoice, idEmployee));
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel listViewItem = (StackPanel)button.Parent;
            TextBlock label = (TextBlock)listViewItem.Children[0];

            string text = label.Text;
            var recInv = receipt.Where(c => c.Name == text).FirstOrDefault();
            if (idEmployee == 0)
            {
                if (MessageBox.Show($"Remove {text}?", "question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Connection.connection.Receipt_Invoice.Remove(recInv);
                    Connection.connection.SaveChanges();
                    MessageBox.Show("Done");
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("Недостаточно прав пользователя!!!");
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel listViewItem = (StackPanel)button.Parent;
            TextBlock label = (TextBlock)listViewItem.Children[0];

            string text = label.Text;
            var recInv = receipt.Where(c => c.Name == text).FirstOrDefault();
            if (idEmployee == recInv.ID_Employee)
            {
                if (MessageBox.Show($"Edit {text}?", "question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new page_redak_receipt(idEmployee, recInv.ID_Receipt_Invoice));
                }
            }
            else if (idEmployee == 0)
            {
                NavigationService.Navigate(new admin.page_admin_redak_receipt(recInv.ID_Receipt_Invoice));
            }
            else
            {
                MessageBox.Show("Недостаточно прав пользователя!!!");
            }
        }

        private void btn_main_Click(object sender, RoutedEventArgs e)
        {
            var empl = employee.Where(c => c.ID_Employee == idEmployee).FirstOrDefault();
            if (idEmployee == 0)
            {
                NavigationService.Navigate(new admin.page_admin());
            }
            else
            {
                NavigationService.Navigate(new page_employee(empl.Name, idEmployee));
            }
            
        }
    }
}
