using Personalised_News_Feed.Core;
using System;
using System.Collections.Generic;
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

namespace Personalised_News_Feed.Controls
{
    /// <summary>
    /// Interaction logic for ViewTopic.xaml
    /// </summary>
    public partial class ViewTopic : UserControl
    {
        private Topics topics;

        public Entry globalSelectedEntry { get; set; }

        public bool isDropDownChanged = false;

        public string tagHeader
        {
            get
            {
                return (globalSelectedEntry.title.Length > 20) ? globalSelectedEntry.title.Substring(0, 20) : globalSelectedEntry.title;
            }
        }

        public ViewTopic()
        {
            InitializeComponent();
        }

        private void Img_Add_To_Favorites_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Topics topicsUC = (Topics)((TextBlock)sender).DataContext;

            if (topicsUC.selectedTopic.isFavorite)
            {
                topicsUC.userFavoriteTopics.Remove(topicsUC.selectedTopic);
                topicsUC.userGeneralTopics.Add(topicsUC.selectedTopic);
                topicsUC.selectedTopic.isFavorite = !topicsUC.selectedTopic.isFavorite;
            }
            else
            {
                topicsUC.userGeneralTopics.Remove(topicsUC.selectedTopic);
                topicsUC.userFavoriteTopics.Add(topicsUC.selectedTopic);
                topicsUC.selectedTopic.isFavorite = !topicsUC.selectedTopic.isFavorite;
            }
        }

        private void Grd_FeedEntry_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid selectedGrid = (Grid)sender;
            Entry selectedEntry = (Entry)selectedGrid.DataContext;
            globalSelectedEntry = selectedEntry;
            if(Tct_Topic_Tabs.Items != null)
            {
                int index = 0;
                foreach(TabItem tabItem in Tct_Topic_Tabs.Items)
                {
                    if((string)tabItem.Tag == selectedEntry.id)
                    {
                        Tct_Topic_Tabs.SelectedItem = tabItem;
                        tabItem.IsSelected = true;
                        Dispatcher.BeginInvoke((Action)(() => Tct_Topic_Tabs.SelectedIndex = index));
                        return;
                    }
                    index++;
                }
            }
            TabItem newTab = new TabItem { Header = new TabHeaderControl() { DataContext = this}, DataContext = selectedEntry, Tag = selectedEntry.id};
            newTab.Content = new TabItemBrowserControl(topics);

            Tct_Topic_Tabs.Items.Add(newTab);
            newTab.IsSelected = true;
            topics.writeHistoryToFile(selectedEntry);
            Dispatcher.BeginInvoke((Action)(() => Tct_Topic_Tabs.SelectedIndex = (Tct_Topic_Tabs.Items.Count - 1)));
        }

        private void UC_ViewTopic_Loaded(object sender, RoutedEventArgs e)
        {
            topics = (Topics)((UserControl)sender).DataContext;
        }

        private void Cmb_HowOften_Selected(object sender, RoutedEventArgs e)
        {
            Topics topicsUC = (Topics)((ComboBox)sender).DataContext;
            if(topicsUC.selectedTopic == null)
            {
                return;
            }

            string howOften = topicsUC.selectedTopic.howOften;
            DateTime todayDateTime = DateTime.Now;
            int todayWeekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(todayDateTime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int todayDayNumber = CultureInfo.InvariantCulture.Calendar.GetDayOfYear(todayDateTime);

            List<Entry> filteredEntities = new List<Entry>();
            if (howOften == "This week")
            {
                filteredEntities = (from n in topicsUC.selectedTopic.topicFeed.entities where CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(n.updatedOnDatetime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) == todayWeekNumber select n).ToList();
                Itc_Feed_Entries.ItemsSource = filteredEntities;
            }
            //else
            //{
            //    filteredEntities = (from n in topicsUC.selectedTopic.topicFeed.entities where CultureInfo.InvariantCulture.Calendar.GetDayOfYear(n.updatedOnDatetime) == todayDayNumber select n).ToList();
            //}
            
        }

        private void Cmb_HowOften_Selected(object sender, SelectionChangedEventArgs e)
        {
            if(isDropDownChanged && topics != null)
            {
                topics.AddNewTopic(topics.selectedTopic, topics.selectedTopic.topicDetails.name);
            }
            isDropDownChanged = true;
        }
    }
}
