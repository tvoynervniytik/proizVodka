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
    /// Логика взаимодействия для BrigadesPage.xaml
    /// </summary>
    public partial class BrigadesPage : Page
    {
        public static List<Employees> employees {  get; set; }
        public static List<Brigades> brigades {  get; set; }
        public BrigadesPage()
        {
            InitializeComponent();
            employees = new List<Employees>(DBConnection.mop.Employees.Where(i => i.PostID == 1 & i.BrigadeID != null).ToList());
            brigades = new List<Brigades>(DBConnection.mop.Brigades.ToList());
            if (AuthorizationFunc.loggedUser.PostID == 2) st.Visibility = Visibility.Hidden;
            this.DataContext = this;
        }
        public void Refresh()
        {
            brigadesLv.ItemsSource = DBConnection.mop.Brigades.ToList();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddBrigade addBrigade = new AddBrigade();
            addBrigade.Show();
        }


        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var br = brigadesLv.SelectedItem as Brigades;
            if (br == null) { }
            else
            {
                EditBrigadeWindow editBrigadeWindow = new EditBrigadeWindow(br);
                editBrigadeWindow.Show();                
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
