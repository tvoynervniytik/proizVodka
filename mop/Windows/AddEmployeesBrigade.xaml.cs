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
using System.Windows.Shapes;

namespace mop.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeesBrigade.xaml
    /// </summary>
    public partial class AddEmployeesBrigade : Window
    {
        public static List<Employees> employees {  get; set; }
        private static Brigades brigades;
        public AddEmployeesBrigade(Brigades brigade)
        {
            InitializeComponent();
            brigades = brigade;
            Refresh();
            this.DataContext = this;
        }
        public void Refresh()
        {
            employees = new List<Employees>(DBConnection.mop.Employees.Where(i => i.PostID == 1 & i.BrigadeID == null).ToList());
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var empl = emplCb.SelectedItem as Employees;
            empl.BrigadeID = brigades.ID;
            DBConnection.mop.SaveChanges();
            this.Close();
            AddEmployeesBrigade addEmployeesBrigade = new AddEmployeesBrigade(brigades);
            addEmployeesBrigade.Show();
            Refresh();
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
