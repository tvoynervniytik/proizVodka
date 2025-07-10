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
    /// Логика взаимодействия для AddBrigade.xaml
    /// </summary>
    public partial class AddBrigade : Window
    {
        public static List<Employees> employees { get; set; }
        public AddBrigade()
        {
            InitializeComponent();
            employees = new List<Employees>(DBConnection.mop.Employees.Where(i=>i.PostID ==1 & i.BrigadeID == null).ToList());
            this.DataContext = this;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Brigades brigade = new Brigades();
            var a = emplCb.SelectedItem as Employees;
            if (a != null)
            {
                brigade.HeadID = a.ID;
                DBConnection.mop.Brigades.Add(brigade);
                DBConnection.mop.SaveChanges();
                a.BrigadeID = brigade.ID;
                DBConnection.mop.SaveChanges();
                this.Close();
                AddEmployeesBrigade addEmployeesBrigade = new AddEmployeesBrigade(brigade);
                addEmployeesBrigade.Show();
            }
            else MessageBox.Show("Заполните все данные!", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
