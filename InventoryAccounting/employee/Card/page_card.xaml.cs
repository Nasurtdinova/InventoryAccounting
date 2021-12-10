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

namespace InventoryAccounting.employee.Card
{
    /// <summary>
    /// Логика взаимодействия для page_card.xaml
    /// </summary>
    public partial class page_card : Page
    {
        public static ObservableCollection<dbo.Inventory> inventory { get; set; }
        public static ObservableCollection<dbo.Accounting_Card_Receipt> cardReceipt { get; set; }
        public static ObservableCollection<dbo.Accounting_Card_Expenditure> cardExpenditure { get; set; }
        public static ObservableCollection<dbo.Receipt_Inventory> recInventory { get; set; }
        public static ObservableCollection<dbo.Expenditure_Inventory> expenInventory { get; set; }
        public static IEnumerable<InventoryCard> resultReceipt { get; set; }
        public static IEnumerable<InventoryCard> resultExpenditure { get; set; }
        public static int idInventory { get; set; }
        public page_card()
        {
            InitializeComponent();
            inventory = new ObservableCollection<dbo.Inventory>(Connection.connection.Inventory.ToList());
            cardReceipt = new ObservableCollection<dbo.Accounting_Card_Receipt>(Connection.connection.Accounting_Card_Receipt.ToList());
            cardExpenditure = new ObservableCollection<dbo.Accounting_Card_Expenditure>(Connection.connection.Accounting_Card_Expenditure.ToList());
            recInventory = new ObservableCollection<dbo.Receipt_Inventory>(Connection.connection.Receipt_Inventory.ToList());
            expenInventory = new ObservableCollection<dbo.Expenditure_Inventory>(Connection.connection.Expenditure_Inventory.ToList());
            resultReceipt = from f in cardReceipt
                            join t in inventory on f.ID_Inventory equals t.ID_Inventory
                            join s in recInventory on f.ID_Receipt_Invoice equals s.ID_Receipt_Invoice where f.ID_Inventory == s.ID_Inventory
                            select new InventoryCard { ID = f.ID_Accounting_Card, NameInventory = t.Name, Date = Convert.ToDateTime(f.Date), NameStorage = f.Storage.Name, count = Convert.ToInt32(s.Count),isWho="Приход" };
            listReceipt.ItemsSource = resultReceipt;

            resultExpenditure = from f in cardExpenditure
                                join t in inventory on f.ID_Inventory equals t.ID_Inventory
                                join s in expenInventory on f.ID_Expenditure_Invoice equals s.ID_Expenditure_Invoice where f.ID_Inventory == s.ID_Inventory
                                select new InventoryCard { ID = f.ID_Accounting_Card, NameInventory = t.Name, Date = Convert.ToDateTime(f.Date), NameStorage = f.Storage.Name, count = Convert.ToInt32(s.Count), isWho = "Расход" };
            listExpenditure.ItemsSource = resultExpenditure;
            this.DataContext = this;
        }

        private void inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as dbo.Inventory;
            idInventory = a.ID_Inventory;
            resultReceipt = from f in cardReceipt
                     join t in inventory on f.ID_Inventory equals t.ID_Inventory
                     where idInventory == f.ID_Inventory
                     join s in recInventory on f.ID_Receipt_Invoice equals s.ID_Receipt_Invoice
                     where f.ID_Inventory == s.ID_Inventory
                            select new InventoryCard { ID = f.ID_Accounting_Card, NameInventory = t.Name, Date = Convert.ToDateTime(f.Date), NameStorage = f.Storage.Name, count = Convert.ToInt32(s.Count), isWho = "Приход" };
            listReceipt.ItemsSource = resultReceipt;

            resultExpenditure = from f in cardExpenditure
                                join t in inventory on f.ID_Inventory equals t.ID_Inventory
                            where idInventory == f.ID_Inventory
                                join s in expenInventory on f.ID_Expenditure_Invoice equals s.ID_Expenditure_Invoice
                                where f.ID_Inventory == s.ID_Inventory
                                select new InventoryCard { ID = f.ID_Accounting_Card, NameInventory = t.Name, Date = Convert.ToDateTime(f.Date), NameStorage = f.Storage.Name, count = Convert.ToInt32(s.Count), isWho = "Расход" };
            listExpenditure.ItemsSource = resultExpenditure;

            this.DataContext = this;
        }

        public class InventoryCard
        {
            public int ID { get; set; }
            public string NameInventory { get; set; }
            public string NameStorage { get; set; }
            public DateTime Date { get; set; }
            public int count { get; set; }
            public string isWho { get; set; }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
