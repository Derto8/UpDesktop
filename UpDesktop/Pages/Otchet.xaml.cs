using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Otchet.xaml
    /// </summary>
    public partial class Otchet : Page
    {
        MainWindow mainWindow;
        MainContext mainContext;
        public Otchet(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            mainContext = new MainContext();
            combIds.ItemsSource = mainContext.Requests.Select(c => c.Number).ToList();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (combIds.Text != "")
            {
                if (tbTime.Text != "")
                {
                    if (tbMaterials.Text != "")
                    {
                        if (tbCost.Text != "")
                        {
                            if (tbProblem.Text != "")
                            {
                                if (tbHelp.Text != "")
                                {
                                    var cost = double.TryParse(tbCost.Text, out double coost) ? coost : 0;
                                    if (coost == 0)
                                    {
                                        MessageBox.Show("Введите цену цифрами!");
                                        return;
                                    }

                                    var ttime = TimeOnly.TryParse(tbTime.Text, out TimeOnly time) ? time : TimeOnly.MinValue;
                                    if (time == TimeOnly.MinValue)
                                    {
                                        MessageBox.Show("Введите правильно затраченное время!");
                                        return;
                                    }

                                    iTextSharp.text.Document document = new iTextSharp.text.Document();
                                    PdfWriter.GetInstance(document, new FileStream("Otchet.pdf", FileMode.Create));
                                    document.Open();
                                    document.Add(new iTextSharp.text.Paragraph($"Application report: {combIds.Text}"));
                                    document.Add(new iTextSharp.text.Paragraph($"Time spent: {tbTime.Text}"));
                                    document.Add(new iTextSharp.text.Paragraph($"Materials used: {tbMaterials.Text}"));
                                    document.Add(new iTextSharp.text.Paragraph($"Cost: {tbCost.Text}"));
                                    document.Add(new iTextSharp.text.Paragraph($"Cause of malfunction: {tbProblem.Text}"));
                                    document.Add(new iTextSharp.text.Paragraph($"Assistance provided: {tbHelp.Text}"));
                                    document.Close();

                                    MessageBox.Show("Отчёт был сгенерирован");
                                }
                                else MessageBox.Show("Введите оказанную помощь");
                            }
                            else MessageBox.Show("Введите причину неисправности");
                        }
                        else MessageBox.Show("Введите стоимость");
                    }
                    else MessageBox.Show("Введите затраченные материалы");
                }
                else MessageBox.Show("Введите затраченное время");
            }
            else MessageBox.Show("Выберите номер заявки");
        }

        private void Auth(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.workerReq);
        }
    }
}
