using mop.DB;
using mop.Functions;
using mop.Windows;
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
    /// Логика взаимодействия для RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page
    {
        public static List<Requests> requests { get; set; }
       
        public partial class RequestsLv
        {
            public int ID { get; set; }
            public Nullable<int> EmployeeID { get; set; }
            public string EmplF { get; set; }
            public string EmplN { get; set; }
            public string EmplP { get; set; }
            public string AtributeName { get; set; }
            public string ItemEditted { get; set; }
            public Nullable<System.DateTime> Date { get; set; }
            public string Checking { get; set; }
            public string Done { get; set; }

            public virtual Employees Employees { get; set; }
        }
        public static List<RequestsLv> requests1 { get; set; }
        public RequestsPage()
        {
            InitializeComponent();
            Refresh();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorizationFunc.loggedUser.PostID == 3 || AuthorizationFunc.loggedUser.PostID == 4) //meneger or admin
                NavigationService.Navigate(new MenuSecondPage());
            else
                NavigationService.Navigate(new ProfilePage());
        }
        public void Refresh()
        {
            var ItemsRequestlv = new List<RequestsLv>();
            if (AuthorizationFunc.loggedUser.PostID == 3 || AuthorizationFunc.loggedUser.PostID == 4) //meneger or admin
                requests = new List<Requests>(DBConnection.mop.Requests.ToList());
            else
            {
                requests = new List<Requests>(DBConnection.mop.Requests.
                    Where(i => i.EmployeeID == AuthorizationFunc.loggedUser.ID).ToList());
                mngSp.Visibility = Visibility.Hidden;
                surSt.Visibility = Visibility.Hidden;
                surSt.Width = 0;
                surSt.Height = 0;
            }
            foreach (var request in requests)
            {
                RequestsLv requestsLv = new RequestsLv();
                requestsLv.ID = request.ID;
                requestsLv.EmplF = request.Employees.Surname;
                requestsLv.EmplN = request.Employees.Name;
                requestsLv.EmplP = request.Employees.Patronymic;
                requestsLv.AtributeName = request.AtributeName;
                requestsLv.ItemEditted = request.ItemEditted;
                requestsLv.Date = request.Date;
                requestsLv.EmployeeID = request.EmployeeID;
                if (request.Done == true)
                    requestsLv.Done = "изменено";
                else requestsLv.Done = "не изменено";
                if (request.Checking == true)
                    requestsLv.Checking = "проверено";
                else requestsLv.Checking = "не проверено";
                ItemsRequestlv.Add(requestsLv);
            }
            //requestsLv.ItemsSource = ItemsRequestlv.ToList();
            //this.DataContext = this;
            //var itemssource = new List<Requests>(DBConnection.mop.Requests.ToList());
            
            //условие
            if (surnameTb.Text == "") { }
            else
                requests = requests.Where(i => i.Employees.Surname.ToLower().StartsWith(surnameTb.Text.Trim().ToLower())).ToList();
            if (dateDp.SelectedDate == null) { }
            else //dateDp
                requests = requests.Where(i => i.Date == (DateTime)dateDp.SelectedDate).ToList();
            if (checkingCb.SelectedItem == null || checkingCb.SelectedIndex == 0) { }
            else //checkingCb
                if (checkingCb.SelectedIndex == 1)
                    requests = requests.Where(i => i.Checking == true).ToList();
                else
                    requests = requests.Where(i => i.Checking == false).ToList();
            if (doneCb.SelectedItem == null || doneCb.SelectedIndex == 0) { }
            else //doneCb
                if (doneCb.SelectedIndex == 1)
                    requests = requests.Where(i => i.Done == true).ToList();
                else
                    requests = requests.Where(i => i.Done == false).ToList();
            foreach (var request in requests)
            {
                RequestsLv requestsLv = new RequestsLv();
                requestsLv.ID = request.ID;
                requestsLv.EmplF = request.Employees.Surname;
                requestsLv.EmplN = request.Employees.Name;
                requestsLv.EmplP = request.Employees.Patronymic;
                requestsLv.AtributeName = request.AtributeName;
                requestsLv.ItemEditted = request.ItemEditted;
                requestsLv.Date = request.Date;
                requestsLv.EmployeeID = request.EmployeeID;
                if (request.Done == true)
                    requestsLv.Done = "изменено";
                else requestsLv.Done = "не изменено";
                if (request.Checking == true)
                    requestsLv.Checking = "проверено";
                else requestsLv.Checking = "не проверено";
                ItemsRequestlv.Add(requestsLv);
            }
            requestsLv.ItemsSource = ItemsRequestlv;
         }
        public static Requests requestSelected {  get; set; }
        private void checkedBtn_Click(object sender, RoutedEventArgs e)
        {
            if (requestsLv.SelectedItem != null)
            {
                var a = requestsLv.SelectedItem as RequestsLv;
                requestSelected = DBConnection.mop.Requests.First(i => i.ID == a.ID);
                requestSelected.Checking = true;
                Refresh();
            }
        }
        private void requestsLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthorizationFunc.loggedUser.PostID == 3)
            { 
                var a = requestsLv.SelectedItem as RequestsLv;
                if (a != null)
                {
                    if (a.Checking == "проверено")
                    {
                        if (a.Done == "изменено") MessageBox.Show
                            ("Запрос уже исполнен!", "done error", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                        {
                            var b = DBConnection.mop.Requests.First(i=>i.ID == a.ID);
                            EmployeeEditWindow employeeEditWindow = new EmployeeEditWindow(b);
                            employeeEditWindow.Show();
                        }
                    }
                    else MessageBox.Show
                            ("Проверка данного запроса не проведена, проверьте документы!", "checking error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void doneCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void checkingCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void surnameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void dateDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
