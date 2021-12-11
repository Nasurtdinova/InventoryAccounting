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

namespace InventoryAccounting.employee.Expenditure_invoice
{
    public partial class page_expen : Page
    {
        public static ObservableCollection<dbo.Expenditure_Invoice> expen { get; set; }
        public static ObservableCollection<dbo.Employee> employee { get; set; }
        public static int idEmployee { get; set; }
        public page_expen(int id)
        {
            InitializeComponent();
            idEmployee = id;
            expen = new ObservableCollection<dbo.Expenditure_Invoice>(Connection.connection.Expenditure_Invoice.ToList());
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
                NavigationService.Navigate(new admin.page_create_admin_expen());
            }
            else
            {
                NavigationService.Navigate(new page_create_expen(idEmployee));
            }
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ListView).SelectedItem as dbo.Expenditure_Invoice;
            NavigationService.Navigate(new page_expen_inven(a.ID_Expenditure_Invoice, idEmployee));
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel listViewItem = (StackPanel)button.Parent;
            TextBlock label = (TextBlock)listViewItem.Children[0];

            string text = label.Text;
            var recInv = expen.Where(c => c.Name == text).FirstOrDefault();
            if (idEmployee == 0)
            {
                if (MessageBox.Show($"Remove {text}?", "question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Connection.connection.Expenditure_Invoice.Remove(recInv);
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
            var recInv = expen.Where(c => c.Name == text).FirstOrDefault();
            if (idEmployee == recInv.ID_Employee)
            {
                if (MessageBox.Show($"Edit {text}?", "question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new page_redak_expen(idEmployee, recInv.ID_Expenditure_Invoice));
                }
            }
            else if (idEmployee == 0)
            {
                NavigationService.Navigate(new admin.page_admin_redak_expen(recInv.ID_Expenditure_Invoice));
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
