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

namespace Restoran.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReservInfoPage.xaml
    /// </summary>
    public partial class ReservInfoPage
    {
        public ReservInfoPage()
        {
            InitializeComponent();
            listView.ItemsSource = RestoranEntities.GetContext().Reservations.ToList();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
