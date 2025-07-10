using mop.DB;
using mop.Pages.addingPages;
using mop.Pages.editingPages;
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

namespace mop.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public static List<Clients> clients {  get; set; }
        public ClientsPage()
        {
            InitializeComponent();
            clients = new List<Clients>(DBConnection.mop.Clients.ToList());
            
            this.DataContext = this;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuSecondPage());
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientsAddPage());
        }
        public void Refresh()
        {
            if (surnameTb.Text == "")
            {
                clientsLv.ItemsSource = new List<Clients>(DBConnection.mop.Clients.ToList());
            }
            else 
            { 
                clientsLv.ItemsSource = 
                    new List<Clients>(DBConnection.mop.Clients.Where(i=>i.Surname.ToLower().StartsWith(surnameTb.Text.Trim().ToLower()) ).ToList());
            }
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var del = clientsLv.SelectedItem as Clients;
            if (del != null)
            {
                DBConnection.mop.Clients.Remove(del);
                DBConnection.mop.SaveChanges();
                Refresh();
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (clientsLv.SelectedItem as Clients != null)
                NavigationService.Navigate(new ClientsEditPage(clientsLv.SelectedItem as Clients));
        }

        private void surnameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
