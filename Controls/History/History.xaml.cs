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

        public TopicHistory globalSelectedEntry { get; set; }

        public string tagHeader
        {
            get
            {
                return (globalSelectedEntry.LinkTitle.Length > 20) ? globalSelectedEntry.LinkTitle.Substring(0, 20) : globalSelectedEntry.LinkTitle;
            }
        }

        private bool IsNoDataTodayVisible_ { get; set; }

        public bool IsNoDataTodayVisible
        {
            get { return IsNoDataTodayVisible_; }
            set
            {
                IsNoDataTodayVisible_ = value;
                OnPropertyChanged("IsNoDataTodayVisible");
            }
        }

        private bool IsNoDataWeekVisible_ { get; set; }

        public bool IsNoDataWeekVisible
        {
            get { return IsNoDataWeekVisible_; }
            set
            {
                IsNoDataWeekVisible_ = value;
                OnPropertyChanged("IsNoDataWeekVisible");
            }
        }

        private bool IsNoDataAllVisible_ { get; set; }

        public bool IsNoDataAllVisible
        {
            get { return IsNoDataAllVisible_; }
            set
            {
                IsNoDataAllVisible_ = value;
                OnPropertyChanged("IsNoDataAllVisible");
            }
        }

        private bool IsNoTabVisible_ { get; set; }

        public bool IsNoTabVisible
        {
            get { return IsNoTabVisible_; }
            set
            {
                IsNoTabVisible_ = value;
                OnPropertyChanged("IsNoTabVisible");
            }
        }

        public History()
        {
            InitializeComponent();
            loadHistory();
            filterHistoryByTime(userHistory.Histories);
            bindItemsToUI();
            this.DataContext = this;
            IsNoTabVisible = true;
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
            IsNoTabVisible = false;
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
                IsNoDataTodayVisible = false;
            }
            else
            {
                IsNoDataTodayVisible = true;
            }

            if (weekHistory.Count > 0)
            {
                IsNoDataWeekVisible = false;
            }
            else
            {
                IsNoDataWeekVisible = true;
            }

            if (restHistory.Count > 0)
            {
                IsNoDataAllVisible = false;
            }
            else
            {
                IsNoDataAllVisible = true;
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
