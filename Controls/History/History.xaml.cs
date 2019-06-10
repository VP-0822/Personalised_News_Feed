using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class History : UserControl, INotifyPropertyChanged
    {
        public UserHistory userHistory { get; set; }

        public ObservableCollection<TopicHistory> todayHistory { get; set; }

        public ObservableCollection<TopicHistory> weekHistory { get; set; }

        public ObservableCollection<TopicHistory> restHistory { get; set; }

        private string NoDataTodayVisibility_ { get; set; } = "Visible";

        public TopicHistory globalSelectedEntry { get; set; }

        public string tagHeader
        {
            get
            {
                return (globalSelectedEntry.LinkTitle.Length > 20) ? globalSelectedEntry.LinkTitle.Substring(0, 20) : globalSelectedEntry.LinkTitle;
            }
        }

        public string NoDataTodayVisibility
        {
            get { return NoDataTodayVisibility_; }
            set
            {
                NoDataTodayVisibility_ = value;
                OnPropertyChanged("NoDataTodayVisibility");
            }
        }

        private string NoDataWeekVisibility_ { get; set; } = "Visible";
        public string NoDataWeekVisibility
        {
            get { return NoDataWeekVisibility_; }
            set
            {
                NoDataWeekVisibility_ = value;
                OnPropertyChanged("NoDataWeekVisibility");
            }
        }

        public string NoDataAllVisibility_ { get; set; } = "Visible";
        public string NoDataAllVisibility
        {
            get { return NoDataAllVisibility_; }
            set
            {
                NoDataAllVisibility_ = value;
                OnPropertyChanged("NoDataAllVisibility");
            }
        }

        public string NoTabVisibility_ { get; set; } = "Visible";
        public string NoTabVisibility
        {
            get
            {
                return NoTabVisibility_;
            }
            set
            {
                NoTabVisibility_ = value;
                OnPropertyChanged("NoTabVisibility");
            }
        }

        public History()
        {
            InitializeComponent();
            loadHistory();
            filterHistoryByTime(userHistory.Histories);
            bindItemsToUI();
            this.DataContext = this;
        }


        private void loadHistory()
        {
            AllUserHistory allUserHistory = XMLSerializerWrapper.ReadXML<AllUserHistory>("Data\\UserHistory.xml");
            if (allUserHistory != null)
            {
                userHistory = (from n in allUserHistory.UsersHistory where n.UserId == 1 select n).ToList()[0];
            }
        }
        private void filterHistoryByTime(List<TopicHistory> histories)
        {
            DateTime todayDateTime = DateTime.Now;
            int todayWeekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(todayDateTime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int todayDayNumber = CultureInfo.InvariantCulture.Calendar.GetDayOfYear(todayDateTime);
            if (histories != null)
            {
                todayHistory = new ObservableCollection<TopicHistory>((from n in histories where CultureInfo.InvariantCulture.Calendar.GetDayOfYear(n.BrowsedDate) == todayDayNumber select n).OrderByDescending(x=> x.BrowsedDate));

                weekHistory = new ObservableCollection<TopicHistory>((from n in histories where CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(n.BrowsedDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) == todayWeekNumber && CultureInfo.InvariantCulture.Calendar.GetDayOfYear(n.BrowsedDate) != todayDayNumber select n).OrderByDescending(x => x.BrowsedDate));

                restHistory = new ObservableCollection<TopicHistory>((from n in histories where CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(n.BrowsedDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) != todayWeekNumber select n).OrderByDescending(x => x.BrowsedDate));
            }
        }

        private void Tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox filterBox = (TextBox)sender;
            string filterString = filterBox.Text;

            if (string.IsNullOrEmpty(filterString))
            {
                filterHistoryByTime(userHistory.Histories);
                bindItemsToUI();
                return;
            }

            List<TopicHistory> filteredHistory = (from n in userHistory.Histories where n.TopicName.ToLower().Contains(filterString.ToLower()) select n).ToList();

            filterHistoryByTime(filteredHistory);
            bindItemsToUI();
        }

        private void Grd_HistoryEntry_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NoTabVisibility = "Hidden";
            Grid selectedGrid = (Grid)sender;
            TopicHistory selectedEntry = (TopicHistory)selectedGrid.DataContext;
            globalSelectedEntry = selectedEntry;
            if (Tct_Topic_Tabs.Items != null)
            {
                int index = 0;
                foreach (TabItem tabItem in Tct_Topic_Tabs.Items)
                {
                    if ((string)tabItem.Tag == selectedEntry.Link)
                    {
                        Tct_Topic_Tabs.SelectedItem = tabItem;
                        tabItem.IsSelected = true;
                        Dispatcher.BeginInvoke((Action)(() => Tct_Topic_Tabs.SelectedIndex = index));
                        return;
                    }
                    index++;
                }
            }
            TabItem newTab = new TabItem { Header = new HistoryTabHeaderControl() { DataContext = this }, DataContext = selectedEntry , Tag = selectedEntry.Link};
            newTab.Content = new TabItemHistoryBrowserControl();

            Tct_Topic_Tabs.Items.Add(newTab);
            newTab.IsSelected = true;
            Dispatcher.BeginInvoke((Action)(() => Tct_Topic_Tabs.SelectedIndex = (Tct_Topic_Tabs.Items.Count - 1)));
        }

        private void bindItemsToUI()
        {
            Itc_Today_History.ItemsSource = todayHistory;
            Itc_Week_History.ItemsSource = weekHistory;
            Itc_All_History.ItemsSource = restHistory;
            if (todayHistory.Count > 0)
            {
                NoDataTodayVisibility = "Hidden";
            }
            else
            {
                NoDataTodayVisibility = "Visible";
            }

            if (weekHistory.Count > 0)
            {
                NoDataWeekVisibility = "Hidden";
            }
            else
            {
                NoDataWeekVisibility = "Visible";
            }

            if (restHistory.Count > 0)
            {
                NoDataAllVisibility = "Hidden";
            }
            else
            {
                NoDataAllVisibility = "Visible";
            }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
