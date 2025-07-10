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

namespace mop.Pages.editingPages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class EditOrderPage : Page
    {
        public static List<Clients> clients { get; set; }
        public static List<Brigades> brigades { get; set; }
        public static List<Services> services { get; set; }
        private Services serv;
        private Orders ordr;
        public EditOrderPage(Orders order)
        {
            InitializeComponent();
            ordr = order;
            clients = new List<Clients>(DBConnection.mop.Clients.ToList());
            brigades = new List<Brigades>(DBConnection.mop.Brigades.ToList());
            brigades.Sort((b1, b2) => b1.ID.CompareTo(b2.ID));
            services = new List<Services>(DBConnection.mop.Services.ToList());

            Services curService = services.FirstOrDefault(i=>i.ID == ordr.ServiceID);
            servicesCb.SelectedItem = curService;

            Clients curClient = clients.FirstOrDefault(i=>i.ID==ordr.ClientID);
            clientsCb.SelectedItem = curClient;

            Brigades curBrigade = brigades.FirstOrDefault(i=>i.ID==ordr.BrigadeID);
            brigadesCb.SelectedItem = curBrigade;

            dateDp.SelectedDate = order.Date;
            priceTb.Text = (order.Price).ToString();
            this.DataContext = this;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Orders order = new Orders();
            if ((squareTb.Text == "" && servicesCb.SelectedIndex == 0)|| dateDp.SelectedDate == null)
            {
                MessageBox.Show("Заполните все данные!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    if (clientsCb.SelectedItem != null)
                    {
                        var client = clientsCb.SelectedItem as Clients;
                        ordr.ClientID = client.ID;
                    }
                    if (brigadesCb.SelectedItem != null)
                    {
                        var brigade = brigadesCb.SelectedItem as Brigades;
                        ordr.BrigadeID = brigade.ID;
                    }
                    if (servicesCb.SelectedItem != null)
                    {
                        var service = servicesCb.SelectedItem as Services;
                        ordr.ServiceID = service.ID;
                    }
                    if (priceTb.Text != "")
                        ordr.Price = int.Parse(priceTb.Text.Trim());
                    if (servicesCb.SelectedIndex == 0)
                        ordr.CountPeople = int.Parse(squareTb.Text.Trim());
                    ordr.Date = dateDp.SelectedDate;
                    DBConnection.mop.SaveChanges();

                    MessageBox.Show("Данные сохранены!");
                    NavigationService.Navigate(new OrdersPage());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message);  }
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
            if (servicesCb.SelectedIndex != 0)
            {
                squareTb.Text = "";
                squareTb.IsEnabled = false;
            }
            else squareTb.IsEnabled = true;
        
    }

        private void squareTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (serv == null)
                {
                    //priceTb.Text = (ordr.Price / ordr.Square * int.Parse(squareTb.Text.Trim())).ToString();
                }
                else
                {
                    if (squareTb.Text == "")
                    { priceTb.Text = (serv.Price).ToString(); }
                    else
                        priceTb.Text = (serv.Price * int.Parse(squareTb.Text.Trim())).ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
