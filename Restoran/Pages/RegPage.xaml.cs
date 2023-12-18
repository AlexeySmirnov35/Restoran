using Restoran.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage 
    {
        private int _id;
        User _user=new User();
        public RegPage(User selUser)
        {
            InitializeComponent();
            cbRole.ItemsSource=RestoranEntities.GetContext().Role.ToList();
            if (selUser != null)
            {
                _user = selUser;
            }

            DataContext = _user;
           

        }

        private void Reg_Btn_Click(object sender, RoutedEventArgs e)
        {
           var rol=cbRole.SelectedItem as Role;
            User newd = new User();
            
            if (RestoranEntities.GetContext().User.Count(x => x.PhoneNumber == tbNum.Text) > 0)
            {
                MessageBox.Show("Такой пользователь уже есть", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Close();
                return;
            }
            try
            {
                User sotrrud = new User()
                {
                    PhoneNumber= tbNum.Text,
                    passwod = tbPas.Text,
                    Name = tbName.Text,
                    Surname = tbSurname.Text,
                    RoleID = rol.RoleID,
                    Patranomic=tbPatr.Text
                    

                };
                RestoranEntities.GetContext().User.Add(sotrrud);
                RestoranEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно регистрация");
            }
            catch (Exception ex)
            {

            }
        }
        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
