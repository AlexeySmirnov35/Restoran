﻿using Restoran.Pages;
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
            bListView.ItemsSource = RestoranEntities.GetContext().Rooms.ToList();
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditRoomPage((sender as Button).DataContext as Rooms));
        }
    }
}
