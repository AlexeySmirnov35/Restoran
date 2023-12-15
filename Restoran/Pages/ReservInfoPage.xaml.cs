using Restoran.Page;
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
        private int _role;
        public ReservInfoPage(int role)
        {
            InitializeComponent();
            listView.ItemsSource = RestoranEntities.GetContext().Reservations.ToList();
            _role= role;
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new FormPage(null,_role));
           
        }

        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
                NavigationService.Navigate(new FormPage((sender as Button).DataContext as Reservations,_role));
        }
    }
}
