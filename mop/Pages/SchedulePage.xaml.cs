using mop.DB;
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
using static mop.Pages.RequestsPage;

namespace mop.Pages
{
    /// <summary>
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public static List<Orders> orders { get; set; }
        public static List<Brigades> brigades { get; set; }    
        public SchedulePage()
        {
            InitializeComponent();
            if (AuthorizationFunc.loggedUser.PostID == 1)
            {
            orders = new List<Orders>(DBConnection.mop.Orders.
                    Where(i => i.BrigadeID == AuthorizationFunc.loggedUser.BrigadeID).ToList());
                brigadesCb.Visibility = Visibility.Hidden;
                brTb.Visibility = Visibility.Hidden;
                delBtn.Visibility = Visibility.Hidden;
            }
            else
                orders = new List<Orders>(DBConnection.mop.Orders.ToList());
            brigades = new List<Brigades>(DBConnection.mop.Brigades.ToList());
            brigades.Sort((b1, b2)=>b1.ID.CompareTo(b2.ID));
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuFirstPage());
        }

        private void dateDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
        public void Refresh()
        {
            var itemssource = new List<Orders>(DBConnection.mop.Orders.ToList());

            if (dateDp.SelectedDate == null) 
            {
                if (AuthorizationFunc.loggedUser.PostID == 1)
                    itemssource = itemssource.Where(i => i.BrigadeID == AuthorizationFunc.loggedUser.BrigadeID).ToList();
            }
            else //dateDp
                itemssource = itemssource.Where(i => i.Date == dateDp.SelectedDate).ToList();
            if (brigadesCb.SelectedItem == null)
            {
                
            }
            else //brigadesCb
            {
                var b = brigadesCb.SelectedItem as Brigades;
                itemssource = itemssource.Where(i=> i.BrigadeID == b.ID).ToList();
            }
            employeesLv.ItemsSource = itemssource;
        }

        private void brigadesCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            brigadesCb.SelectedItem = null;
            Refresh();
        }
    }
}
