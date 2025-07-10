using mop.Functions;
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
    /// Логика взаимодействия для MenuSecondPage.xaml
    /// </summary>
    public partial class MenuSecondPage : Page
    {
        public MenuSecondPage()
        {
            InitializeComponent();
            if (AuthorizationFunc.loggedUser.PostID == 2) //clients meneger
            {
                emplBrd.Visibility = Visibility.Hidden;
                emplBrd.Height = 0;
                requestBr.Visibility = Visibility.Hidden;
                requestBr.Height = 0;
            }
            if (AuthorizationFunc.loggedUser.PostID == 3) //employees meneger
            {
                clientsBrd.Visibility = Visibility.Hidden;
                clientsBrd.Height = 0;
                servicesBrd.Visibility = Visibility.Hidden;
                servicesBrd.Height = 0;
            }
            if (AuthorizationFunc.loggedUser.PostID == 1) //worker
            {
                MngStP.Visibility = Visibility.Hidden;
                MngStP.Height = 0;
            }
            if (AuthorizationFunc.loggedUser.PostID == 4) //admin
            {
                profileBr.Visibility = Visibility.Hidden;
                profileBr.Height = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorizationFunc.loggedUser.PostID == 3)
            {
                var result = MessageBox.Show("Вы уверены, что хотите выйти из профиля???", "Подтверждение выхода",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    AuthorizationFunc.Question = false;
                    NavigationService.Navigate(new AuthorizationPage());
                }
            }
            else
                NavigationService.Navigate(new MenuFirstPage());
        }

        private void brigadeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BrigadesPage());
        }

        private void emplBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeesPage());
        }

        private void clientsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientsPage());
        }

        private void servicessBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesPage());
        }

        private void addressesBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddressesPage());
        }

        private void profileBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage());
           
        }

        private void requestBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestsPage()); 
        }
    }
}
