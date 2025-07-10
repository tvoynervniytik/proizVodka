using mop.DB;
using mop.Pages.addingPages;
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
using mop.Pages.editingPages;

namespace mop.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public static List<Orders> orders {  get; set; }
        public OrdersPage()
        {
            InitializeComponent();
            
            this.DataContext = this;
            Refresh();
        }
        public void Refresh()
        {
            orders = new List<Orders>(DBConnection.mop.Orders.ToList());
            
           
            ordersLv.ItemsSource = new List<Orders>(DBConnection.mop.Orders.ToList());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuFirstPage());
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderPage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ordersLv.SelectedItem != null)
            NavigationService.Navigate(new EditOrderPage(ordersLv.SelectedItem as Orders));
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var a = ordersLv.SelectedItem as Orders;
            if (ordersLv.SelectedItem != null)
            {
                DBConnection.mop.Orders.Remove(a);
                DBConnection.mop.SaveChanges();
                Refresh();
            }
        }
    }
}
