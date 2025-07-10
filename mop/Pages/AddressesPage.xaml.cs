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
    /// Логика взаимодействия для AddressesPage.xaml
    /// </summary>
    public partial class AddressesPage : Page
    {
        public static List<Address> addresses {  get; set; }
        public AddressesPage()
        {
            InitializeComponent();
            addresses = new List<Address>(DBConnection.mop.Address.ToList());
            if (AuthorizationFunc.loggedUser.PostID == 3) st.Visibility = Visibility.Hidden;
            this.DataContext = this;
        }
        public void Refresh()
        {
            addressesLv.ItemsSource = new List<Address>(DBConnection.mop.Address.ToList());
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuSecondPage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var del = addressesLv.SelectedItem as Address;
            if (del != null)
            {
                NavigationService.Navigate(new AddressesEditPage(addressesLv.SelectedItem as Address));
            }
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var del = addressesLv.SelectedItem as Address;
            if (del != null)
            {
                DBConnection.mop.Address.Remove(del);
                DBConnection.mop.SaveChanges();
                Refresh();
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddressesAddPage());
        }
    }
}
