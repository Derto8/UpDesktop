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
    /// Логика взаимодействия для Statistic.xaml
    /// </summary>
    public partial class Statistic : Page
    {
        MainWindow mainWindow;
        MainContext mainContext;
        public Statistic(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            mainContext = new MainContext();

            var avarageTime = CalculateAverageTime(mainContext.Histories.Select(c => TimeOnly.Parse(c.TimeSpent)).ToArray());
            lbAvarageTime.Content += avarageTime.ToString();
            lbCout.Content += mainContext.Requests.Where(c => c.State == "Выполнено").Count().ToString();

            var problemsText = "";
            var problems = mainContext.Requests.Select(c => c.ProblemType).Distinct().ToArray();
            foreach (var problem in problems)
            {
                problemsText += $", {problem}";
            }

            lbProblems.Content += problemsText;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.managerPage);
        }

        private TimeOnly CalculateAverageTime(TimeOnly[] times)
        {
            int totalHours = 0;
            int totalMinutes = 0;
            int totalSeconds = 0;

            foreach (TimeOnly time in times)
            {
                totalHours += time.Hour;
                totalMinutes += time.Minute;
                totalSeconds += time.Second;
            }

            int totalTime = times.Length;

            int averageHours = totalHours / totalTime;
            int averageMinutes = totalMinutes / totalTime;
            int averageSeconds = totalSeconds / totalTime;

            return new TimeOnly(averageHours, averageMinutes, averageSeconds);
        }
    }
}
