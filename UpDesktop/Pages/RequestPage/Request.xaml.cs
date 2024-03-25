using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UpDesktop.Core.Persistence;

namespace UpDesktop.Pages.RequestPage
{
    /// <summary>
    /// Логика взаимодействия для Request.xaml
    /// </summary>
    public partial class Request : Page
    {
        MainWindow mainWindow;
        MainContext mainContext;
        public Request(MainWindow mainWindow)
        {
            InitializeComponent();
            mainContext = new MainContext();
            this.mainWindow = mainWindow;

            dtRequests.ItemsSource = mainContext.Requests.ToList();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.auth);
        }

        private void GoEdit(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.changingReq);
        }

        private void NumberSearch(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text != "")
            {
                dtRequests.ItemsSource = mainContext.Requests.Where(c => c.Number == tbSearch.Text).ToList();
            }
            else MessageBox.Show("Введите текст в поиск");
        }

        private void EquipmentSearch(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text != "")
            {
                dtRequests.ItemsSource = mainContext.Requests.Where(c => c.Equipment == tbSearch.Text).ToList();
            }
            else MessageBox.Show("Введите текст в поиск");
        }

        private void StateSearch(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text != "")
            {
                dtRequests.ItemsSource = mainContext.Requests.Where(c => c.State == tbSearch.Text).ToList();
            }
            else MessageBox.Show("Введите текст в поиск");
        }
    }
}
