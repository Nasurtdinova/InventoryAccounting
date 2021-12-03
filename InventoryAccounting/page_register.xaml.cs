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
    /// Логика взаимодействия для page_register.xaml
    /// </summary>
    public partial class page_register : Page
    {
        public static ObservableCollection<dbo.Position> position { get; set; }
        int ID_position {get;set;}
        public page_register()
        {
            InitializeComponent();
            position = new ObservableCollection<dbo.Position>(Connection.connection.Position.ToList());
            this.DataContext = this;
        }

        private void registr_Click(object sender, RoutedEventArgs e)
        {
            var b = new dbo.Employee();
            b.Login = login_txt.Text;
            b.Password = password_txt.Text;
            b.Surname = surname_txt.Text;
            b.Name = name_txt.Text;
            b.ID_Position = ID_position;
            Connection.connection.Employee.Add(b);
            Connection.connection.SaveChanges();
            MessageBox.Show("Done");
        }

        private void position_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Position;
            ID_position = a.ID_Position;
        }

        private void revers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void registr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.MainWindow.Close();
        }
    }
}
