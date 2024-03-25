using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using UpDesktop.Core.Entities;
using UpDesktop.Core.Persistence;

namespace UpDesktop.Pages.RequestPage
{
    /// <summary>
    /// Логика взаимодействия для WorkerRequest.xaml
    /// </summary>
    public partial class WorkerRequest : Page
    {
        MainWindow mainWindow;
        MainContext mainContext;
        public WorkerRequest(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainContext = new MainContext();

            dtRequests.ItemsSource = mainContext.Requests.ToList();
            combWorkers.ItemsSource = mainContext.Users.Where(c => c.Role == "Worker").Select(c => c.Login).ToList();
            combIds.ItemsSource = mainContext.Requests.Select(c => c.Number).ToList();
        }

        private void GoEdit(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.requests);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.auth);
        }

        private void Otchet(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.otchet);
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

        private async void Comment(object sender, RoutedEventArgs e)
        {
            if (combIds.Text != "")
            {
                if (combWorkers.Text != "")
                {
                    if (tbHistory.Text != "")
                    {
                        if (tbTime.Text != "")
                        {
                            var ttime = Time.TryParse(tbTime.Text, out TimeOnly time) ? time : TimeOnly.MinValue;
                            if (time == TimeOnly.MinValue)
                            {
                                MessageBox.Show("Введите правильно затраченное время!");
                                return;
                            }

                            History history = new History
                            {
                                Comment = tbHistory.Text,
                                RequestId = (await mainContext.Requests.FirstOrDefaultAsync(c => c.Number == combIds.Text)).Id,
                                UserId = (await mainContext.Users.FirstOrDefaultAsync(c => c.Login == combWorkers.Text)).Id,
                                TimeSpent = time.ToString()
                            };

                            await mainContext.AddAsync(history);
                            await mainContext.SaveChangesAsync();

                            mainWindow.OpenPage(MainWindow.pages.histories);
                        }
                        else MessageBox.Show("Введите потраченное время");
                    }
                    else MessageBox.Show("Введите комментарий");
                }
                else MessageBox.Show("Выберите работника");
            }
            else MessageBox.Show("Выберите заявку");
        }
    }
}
