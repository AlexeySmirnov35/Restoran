using Restoran.Page;
using System;
using System.Collections;
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
        private void UpdateRooms()
        {
            string searchText = TboxSerch.Text.ToLower();
            var allRooms = RestoranEntities.GetContext().Reservations.ToList();

            var filteredRooms = allRooms
                .Where(room =>
                    room.Rooms.NameRoom.ToLower().Contains(searchText)).ToList();
            switch (ComboFilter.SelectedIndex)
            {

                case 1:

                    filteredRooms = filteredRooms.OrderBy(resr=>resr.DateTimeReserv).ToList();
                    break;
                case 2:

                    filteredRooms = filteredRooms.OrderByDescending(room => room.DateTimeReserv).ToList();
                    break;
                case 3:

                    filteredRooms = filteredRooms.OrderBy(room => room.Rooms.NameRoom).ToList();
                    break;
            }
            listView.ItemsSource = filteredRooms;
        }
        private void Tbox_Search(object sender, TextChangedEventArgs e)
        {
            UpdateRooms();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRooms();
        }
        private void Page_IsVis(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Visibility == Visibility.Visible)
            {
                RestoranEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                listView.ItemsSource = RestoranEntities.GetContext().Reservations.ToList();
            }

        }
        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
