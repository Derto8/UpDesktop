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
using UpDesktop.Core.Persistence;

namespace UpDesktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для HistoriesPage.xaml
    /// </summary>
    public partial class HistoriesPage : Page
    {
        MainWindow mainWindow;
        MainContext mainContext;
        public HistoriesPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            mainContext = new MainContext();
            dtRequests.ItemsSource = mainContext.Histories.ToList();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.auth);
        }
    }
}
