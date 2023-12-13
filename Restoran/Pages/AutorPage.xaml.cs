
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
    /// Логика взаимодействия для AutorPage.xaml
    /// </summary>
    public partial class AutorPage
    {
        public AutorPage()
        {
            InitializeComponent();
        }

        private void Auto_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = RestoranEntities.GetContext().User.FirstOrDefault(x => x.login == tbLog.Text && x.passwod == tbPas.Password);
                if (userObj == null)
                {
                   MessageBox.Show("Пользователя нет! " ,"Ошибка при авторизации!",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                else
                {
                    switch (userObj.RoleID)
                    { 
                        case 1:
                            MessageBox.Show("Приветсвуем Вас, " + userObj.Name + "!", "Успешная авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.Navigate(new AdminPage());
                            break;

                        case 2:
                            MessageBox.Show("Приветсвуем Вас " + userObj.RoleID + "!", "Вы вошли как соотрудник", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.Navigate(new RoomsPage());
                            break;
                        default: MessageBox.Show("Не обнужерен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning); break;

                    }

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Нет данных" + Ex.Message.ToString() + "Критическая ошибка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        

        private void btn_cli(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/EditPage.xaml",UriKind.Relative));
        }
    }
}
