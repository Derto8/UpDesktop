using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        MainWindow mainWindow;
        MainContext mainContext;
        public ManagerPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainContext = new MainContext();

            dtRequests.ItemsSource = mainContext.Users.Where(c => c.Role == "Worker").ToList();
            combWorkers.ItemsSource = mainContext.Users.Where(c => c.Role == "Worker").Select(c => c.Login).ToList();
            combIds.ItemsSource = mainContext.Requests.Select(c => c.Number).ToList();
        }

        private void GoEdit(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.requests);
        }

        private async void AddWorker(object sender, RoutedEventArgs e)
        {
            if (combIds.Text != "")
            {
                if (combWorkers.Text != "")
                {
                    var request = await mainContext.Requests.FirstOrDefaultAsync(c => c.Number == combIds.Text);

                    if (request != null)
                    {
                        var user = await mainContext.Users.FirstOrDefaultAsync(c => c.Login == combWorkers.Text);
                        if (user != null)
                        {
                            user.RequestId = request.Id;
                            mainContext.Update(user);
                            await mainContext.SaveChangesAsync();
                            dtRequests.ItemsSource = mainContext.Users.Where(c => c.Role == "Worker").ToList();
                            MessageBox.Show("Был назначен работник");
                        }
                    }
                }
                else MessageBox.Show("Выберите работника");
            }
            else MessageBox.Show("Выберите заявку");
        }

        private void Otchet(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.statistics);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.auth);
        }

        private async void ChangeTime(object sender, RoutedEventArgs e)
        {
            if (combIds.Text != "")
            {
                if (tbData.Text != "")
                {
                    var date = DateTime.TryParse(tbData.Text, out DateTime time) ? time : DateTime.MinValue;
                    if (date == DateTime.MinValue)
                    {
                        MessageBox.Show("Введите правильно дату!");
                        return;
                    }

                    var requestId = mainContext.Requests.FirstOrDefault(c => c.Number == combIds.Text);
                    requestId.Date = date;

                    mainContext.Update(requestId);
                    await mainContext.SaveChangesAsync();

                    MessageBox.Show("Время заявки было изменено!");
                }
                else MessageBox.Show("Введите дату!");
            }
            else MessageBox.Show("Выберите заявку");
        }
    }
}
