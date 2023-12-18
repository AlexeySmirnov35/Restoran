using Restoran.Pages;
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

namespace Restoran.Page
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage
    {
        public EditPage()
        {
            InitializeComponent();
            //DtGrid.ItemsSource=RestoranEntities.GetContext().User.ToList();
            listView.ItemsSource = RestoranEntities.GetContext().Rooms.ToList();
            UpdateRooms();
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditRoomPage((sender as Button).DataContext as Rooms));
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditRoomPage(null));
        }

        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        {
            var roomsToDelete = listView.SelectedItems.Cast<Rooms>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить эти {roomsToDelete.Count()} элемента!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                var dbContext = RestoranEntities.GetContext();

                foreach (var room in roomsToDelete)
                {
                    if (!dbContext.Reservations.Any(item => item.RoomID == room.RoomID))
                    {
                        dbContext.Rooms.Remove(room);
                    }
                    else
                    {
                        MessageBox.Show($"Комната {room.NameRoom} используется в других таблицах и не может быть удалена.");
                    }
                }

                dbContext.SaveChanges();
                MessageBox.Show("Удаление прошло успешно");
                listView.ItemsSource = dbContext.Rooms.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении комнаты: {ex.Message}");
            }
        }
        private void UpdateRooms()
        {
            string searchText = TboxSerch.Text.ToLower();
            var allRooms = RestoranEntities.GetContext().Rooms.ToList();

            allRooms = allRooms
                .Where(room => room.NameRoom.ToLower().Contains(searchText))
                .ToList();

            switch (ComboFilter.SelectedIndex)
            {
                case 1:
                    allRooms = allRooms.OrderBy(room => room.NameRoom).ToList();
                    break;
                case 2:
                    allRooms = allRooms.OrderByDescending(room => room.NameRoom).ToList();
                    break;
            }

            listView.ItemsSource = allRooms;
        }

        private void Tbox_Search(object sender, TextChangedEventArgs e)
        {
            UpdateRooms();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRooms();
        }


        private void Page_IsVis(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                RestoranEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                listView.ItemsSource = RestoranEntities.GetContext().Rooms.ToList();
            }
        }
        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
