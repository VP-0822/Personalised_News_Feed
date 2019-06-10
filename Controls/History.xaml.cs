using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Personalised_News_Feed.Classes.Core;
using Personalised_News_Feed.Core.Converters;

namespace Personalised_News_Feed.Controls
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {
        public UserHistory userHistory { get; set; }

        public ObservableCollection<TopicHistory> todayHistory { get; set; }

        public ObservableCollection<TopicHistory> weekHistory { get; set; }

        public ObservableCollection<TopicHistory> restHistory { get; set; }

        public History()
        {
            InitializeComponent();
            loadHistory();
            filterHistoryByTime();

            Itc_Today_History.ItemsSource = todayHistory;
            Itc_Week_History.ItemsSource = weekHistory;
            Itc_All_History.ItemsSource = restHistory;
        }


        private void loadHistory()
        {
            AllUserHistory allUserHistory = XMLSerializerWrapper.ReadXML<AllUserHistory>("Data\\UserHistory.xml");
            if (allUserHistory != null)
            {
                userHistory = (from n in allUserHistory.UsersHistory where n.UserId == 1 select n).ToList()[0];
            }
        }
        private void filterHistoryByTime()
        {
            DateTime todayDateTime = DateTime.Now;
            int todayWeekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(todayDateTime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int todayDayNumber = CultureInfo.InvariantCulture.Calendar.GetDayOfYear(todayDateTime);
            if (userHistory != null && userHistory.Histories != null)
            {
                todayHistory = new ObservableCollection<TopicHistory>(from n in userHistory.Histories where CultureInfo.InvariantCulture.Calendar.GetDayOfYear(n.BrowsedDate) == todayDayNumber select n);

                weekHistory = new ObservableCollection<TopicHistory>(from n in userHistory.Histories where CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(n.BrowsedDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) == todayWeekNumber select n);

                restHistory = new ObservableCollection<TopicHistory>(from n in userHistory.Histories where CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(n.BrowsedDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) != todayWeekNumber select n);
            }
        }

        private void Tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
