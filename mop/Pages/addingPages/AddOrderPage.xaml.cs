using mop.DB;
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

namespace mop.Pages.addingPages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        public static List<Clients> clients { get; set; }
        public static List<Brigades> brigades { get; set; }
        public static List<Services > services { get; set; }
        private Services serv;

        public AddOrderPage()
        {
            InitializeComponent();
            clients = new List<Clients>(DBConnection.mop.Clients.ToList());
            brigades = new List<Brigades>(DBConnection.mop.Brigades.ToList());
            services = new List<Services>(DBConnection.mop.Services.ToList());
            this.DataContext = this;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Orders order = new Orders();
            if (clients == null || brigades == null || services == null 
                || (squareTb.Text == "" && servicesCb.SelectedIndex == 0) || dateDp.SelectedDate == null)
            {
                MessageBox.Show("Заполните все данные!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var client = clientsCb.SelectedItem as Clients;
                var brigade = brigadesCb.SelectedItem as Brigades;
                var service = servicesCb.SelectedItem as Services;
                order.ClientID = client.ID;
                order.ServiceID = service.ID;
                order.BrigadeID = brigade.ID;
                if (servicesCb.SelectedIndex == 0)
                    order.CountPeople = int.Parse(squareTb.Text.Trim());
                try
                {
                    if (dateDp.SelectedDate < DateTime.Now)
                    {
                        MessageBox.Show("Дата не раньше и не сегодня!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        order.Date = dateDp.SelectedDate;
                        DBConnection.mop.Orders.Add(order);
                        DBConnection.mop.SaveChanges();

                        MessageBox.Show("Данные сохранены!");
                        NavigationService.Navigate(new OrdersPage());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                
                    
            }
        }

        private void servicesCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var service = servicesCb.SelectedItem as Services;
            serv = service;
            if (squareTb.Text == "")
                priceTb.Text = (serv.Price).ToString(); 
            else
                priceTb.Text = (serv.Price * int.Parse(squareTb.Text.Trim())).ToString();
            
        }

        private void squareTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (serv == null)
                { }
                else
                {
                    if (squareTb.Text == "")
                    { priceTb.Text = (serv.Price).ToString(); }
                    else
                        priceTb.Text = (serv.Price * int.Parse(squareTb.Text.Trim())).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
