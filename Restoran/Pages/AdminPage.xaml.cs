﻿using Restoran.Page;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage 
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void Btn_Room_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditPage());
        }

        private void Btn_Resrev_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReservInfoPage());
        }
    }
}
