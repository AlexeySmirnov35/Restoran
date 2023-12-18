using Restoran.Page;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage 
    {
        public UserPage()
        {
            InitializeComponent();
            listView.ItemsSource = RestoranEntities.GetContext().User.ToList();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage(null));
        }

        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        { 
            var dbContext = RestoranEntities.GetContext();
            var userToDelete = listView.SelectedItems.Cast<User>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить эти {userToDelete.Count()} пользователей и все связанные записи брони? " +
                $"Это также удалит записи о брони.", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
            try
            { 
                dbContext.User.RemoveRange(userToDelete);
                dbContext.SaveChanges();

                MessageBox.Show("Удаление прошло успешно");
                listView.ItemsSource = dbContext.User.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}");
            }
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage((sender as Button).DataContext as User));
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tbox_Search(object sender, TextChangedEventArgs e)
        {

        }
        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
