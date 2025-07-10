using mop.DB;
using mop.Functions;
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
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public static List<Services> services {  get; set; }
        public ServicesPage()
        {
            InitializeComponent();
            services = new List<Services>(DBConnection.mop.Services.ToList());
            if (AuthorizationFunc.loggedUser.PostID != 4) st.Visibility = Visibility.Hidden;
            this.DataContext = this;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuSecondPage());
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesAddPage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (servicesLv.SelectedItem as Services != null)
            {
                NavigationService.Navigate(new ServicesEditPage(servicesLv.SelectedItem as Services));
            }
        }
        private void Refresh()
        {
            servicesLv.ItemsSource = new List<Services>(DBConnection.mop.Services.ToList());
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            if (servicesLv.SelectedItem as Services != null)
            {
                var a = servicesLv.SelectedItem as Services;
                DBConnection.mop.Services.Remove(a);
                DBConnection.mop.SaveChanges();
                Refresh();
            }
        }
    }
}
