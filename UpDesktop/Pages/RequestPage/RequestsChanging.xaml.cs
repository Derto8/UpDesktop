using Microsoft.EntityFrameworkCore;
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
using UpDesktop.Core.Entities;
using UpDesktop.Core.Persistence;

namespace UpDesktop.Pages.RequestPage
{
    /// <summary>
    /// Логика взаимодействия для RequestsChanging.xaml
    /// </summary>
    public partial class RequestsChanging : Page
    {
        MainWindow mainWindow;
        MainContext mainContext;
        public RequestsChanging(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainContext = new MainContext();

            combIds.ItemsSource = mainContext.Requests.Select(c => c.Number).ToList();
            combUsers.ItemsSource = mainContext.Users.Where(c => c.Role == "User").Select(c => c.Login).ToList();
        }

        private void Auth(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.requests);
        }

        private async void Add(object sender, RoutedEventArgs e)
        {
            if (tbnumberreq.Text != "")
            {
                if (tbDate.Text != "")
                {
                    if (tbEquipment.Text != "")
                    {
                        if (tbDesc.Text != "")
                        {
                            if (tbTypePromlem.Text != "")
                            {
                                if (tbState.Text != "")
                                {
                                    if (combUsers.Text != "")
                                    {
                                        var date = DateTime.TryParse(tbDate.Text, out DateTime time) ? time : DateTime.MinValue;
                                        if (date == DateTime.MinValue)
                                        {
                                            MessageBox.Show("Введите правильно дату!");
                                            return;
                                        }

                                        var userId = mainContext.Users.FirstOrDefault(c => c.Login == combUsers.Text);

                                        var request = new Requests
                                        {
                                            Date = date,
                                            Number = tbnumberreq.Text,
                                            ProblemDescription = tbDesc.Text,
                                            ProblemType = tbTypePromlem.Text,
                                            State = tbState.Text,
                                            UserId = userId.Id,
                                            Equipment = tbEquipment.Text,
                                        };

                                        await mainContext.Requests.AddAsync(request);
                                        await mainContext.SaveChangesAsync();

                                        mainWindow.OpenPage(MainWindow.pages.requests);
                                    }
                                    else MessageBox.Show("Выберите клиента, подающего заявку");
                                }
                                else MessageBox.Show("Введите статус");
                            }
                            else MessageBox.Show("Введите тип проблемы");
                        }
                        else MessageBox.Show("Введите описание");
                    }
                    else MessageBox.Show("Введите оборудование");
                }
                else MessageBox.Show("Введите дату");
            }
            else MessageBox.Show("Введите номер заявки");
        }

        private async void Change(object sender, RoutedEventArgs e)
        {
            if (combIds.Text != "")
            {
                if (tbnumberreq.Text != "")
                {
                    if (tbDate.Text != "")
                    {
                        if (tbEquipment.Text != "")
                        {
                            if (tbDesc.Text != "")
                            {
                                if (tbTypePromlem.Text != "")
                                {
                                    if (tbState.Text != "")
                                    {
                                        if (combUsers.Text != "")
                                        {
                                            var date = DateTime.TryParse(tbDate.Text, out DateTime time) ? time : DateTime.MinValue;
                                            if (date == DateTime.MinValue)
                                            {
                                                MessageBox.Show("Введите правильно дату!");
                                                return;
                                            }

                                            var requestId = mainContext.Requests.FirstOrDefault(c => c.Number == combIds.Text);

                                            var userId = mainContext.Users.FirstOrDefault(c => c.Login == combUsers.Text);

                                            requestId.State = tbState.Text;
                                            requestId.Date = date;
                                            requestId.Number = tbnumberreq.Text;
                                            requestId.ProblemDescription = tbDesc.Text;
                                            requestId.ProblemType = tbTypePromlem.Text;
                                            requestId.UserId = userId.Id;
                                            requestId.Equipment = tbEquipment.Text;

                                            mainContext.Update(requestId);
                                            await mainContext.SaveChangesAsync();

                                            mainWindow.OpenPage(MainWindow.pages.requests);
                                        }
                                        else MessageBox.Show("Выберите клиента, подающего заявку");
                                    }
                                    else MessageBox.Show("Введите статус");
                                }
                                else MessageBox.Show("Введите тип проблемы");
                            }
                            else MessageBox.Show("Введите описание");
                        }
                        else MessageBox.Show("Введите оборудование");
                    }
                    else MessageBox.Show("Введите дату");
                }
                else MessageBox.Show("Введите номер заявки");
            }
            else MessageBox.Show("Выберите номера заявки");
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            if (combIds.Text != "")
            {
                var requestId = mainContext.Requests.FirstOrDefault(c => c.Number == combIds.Text);
                mainContext.Requests.Remove(requestId);
                await mainContext.SaveChangesAsync();
                mainWindow.OpenPage(MainWindow.pages.requests);
            }
            else MessageBox.Show("Выберите номера заявки");
        }

        private async void combIds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var request = await mainContext.Requests.FirstOrDefaultAsync(c => c.Number == combIds.SelectedValue.ToString());
            tbDate.Text = request.Date.ToString();
            tbDesc.Text = request.ProblemDescription.ToString();
            tbEquipment.Text = request.Equipment;
            tbState.Text = request.State;
            tbnumberreq.Text = request.Number;
            tbTypePromlem.Text = request.ProblemType;
        }
    }
}

