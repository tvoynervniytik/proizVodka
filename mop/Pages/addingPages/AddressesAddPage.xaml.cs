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
    /// Логика взаимодействия для BrigadesAddPage.xaml
    /// </summary>
    public partial class AddressesAddPage : Page
    {
        public static List<Clients> clients {  get; set; }
        public AddressesAddPage()
        {
            InitializeComponent();
            clients = new List<Clients>(DBConnection.mop.Clients.ToList());
            this.DataContext = this;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        { 
            
            Address address = new Address();
            var client = clientsCb.SelectedItem as Clients;
            if (client == null || roomTb.Text == "" || streetTb.Text == "" || houseNumberTb.Text == "")
                MessageBox.Show("Заполните все данные!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                address.ClientID = client.ID;
                address.Street = streetTb.Text;
                address.HouseNumber = address.HouseCalc(houseNumberTb.Text.Trim(), houseNameTb.Text.Trim());
                address.RoomNumber = roomTb.Text;
                DBConnection.mop.Address.Add(address);
                DBConnection.mop.SaveChanges();

                MessageBox.Show("Данные сохранены!");
                NavigationService.Navigate(new AddressesPage());
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddressesPage());
        }

        private void houseNumberTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = new string(houseNumberTb.Text.Where(c => char.IsDigit(c)).ToArray());

            if (newText != houseNumberTb.Text)
            {
                int cursorPosition = houseNumberTb.SelectionStart;
                houseNumberTb.Text = newText;
                houseNumberTb.SelectionStart = cursorPosition > 0 ? cursorPosition - 1 : 0;
            }
            
        }
        private void houseNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = new string(houseNameTb.Text.Where(c => char.IsLetter(c) || c == ' ').ToArray());

            if (newText != houseNameTb.Text)
            {
                int cursorPosition = houseNameTb.SelectionStart;
                houseNameTb.Text = newText;
                houseNameTb.SelectionStart = cursorPosition > 0 ? cursorPosition - 1 : 0;
            }
        }

        private void roomTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = new string(roomTb.Text.Where(c => char.IsDigit(c)).ToArray());

            if (newText != roomTb.Text)
            {
                int cursorPosition = houseNumberTb.SelectionStart;
                roomTb.Text = newText;
                roomTb.SelectionStart = cursorPosition > 0 ? cursorPosition - 1 : 0;
            }
        }
    }
}
