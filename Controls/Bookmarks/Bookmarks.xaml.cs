using Personalised_News_Feed.Classes.Core;
using Personalised_News_Feed.Core.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Bookmarks.xaml
    /// </summary>
    public partial class Bookmarks : UserControl, INotifyPropertyChanged
    {
        public UserBookmark userBookmark { get; set; }

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

        public BookmarkItem globalSelectedEntry { get; set; }

        public string tagHeader
        {
            get
            {
                return (globalSelectedEntry.LinkTitle.Length > 20) ? globalSelectedEntry.LinkTitle.Substring(0, 20) : globalSelectedEntry.LinkTitle;
            }
        }

        public Bookmarks()
        {
            InitializeComponent();
            //writeBookmark();
            loadBookmarks();
            Itc_Topic_Bookmarks.ItemsSource = userBookmark.TopicWiseBookmarks;
            this.DataContext = this;
            IsNoTabVisible = true;
        }

        public void loadBookmarks()
        {
            AllUserBookmark allUserBookmark = XMLSerializerWrapper.ReadXML<AllUserBookmark>("Data\\UserBookmarks.xml");
            if (allUserBookmark != null)
            {
                userBookmark = (from n in allUserBookmark.UserBookmarks where n.UserId == 1 select n).ToList()[0];
            }
        }

        

        public void writeBookmark()
        {
            AllUserBookmark allUserBookmark = new AllUserBookmark();
            UserBookmark ub = new UserBookmark { UserId = 1 };

            BookmarkItem bi = new BookmarkItem() { BookmarkedDate = DateTime.Now, Link = "https://www.msn.com", LinkTitle = "Something" };
            TopicWiseBookmark twb = new TopicWiseBookmark() { Bookmarks = new List<BookmarkItem> { bi }, TopicName = "Machine Learning" };

            ub.TopicWiseBookmarks = new List<TopicWiseBookmark>() { twb };
            allUserBookmark.UserBookmarks = new List<UserBookmark>() { ub };
            XMLSerializerWrapper.WriteXML<AllUserBookmark>(allUserBookmark, @"Data\\UserBookmarks.xml");
        }

        private void Tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox filterBox = (TextBox)sender;
            string filterString = filterBox.Text;

            if (string.IsNullOrEmpty(filterString))
            {
                Itc_Topic_Bookmarks.ItemsSource = userBookmark.TopicWiseBookmarks;
                return;
            }

            List<TopicWiseBookmark> topicBookmarks = (from n in userBookmark.TopicWiseBookmarks where n.TopicName.ToLower().Contains(filterString.ToLower()) select n).ToList();
            Itc_Topic_Bookmarks.ItemsSource = topicBookmarks;
        }


        private void Grd_BookmarkEntry_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            IsNoTabVisible = false;
            Grid selectedGrid = (Grid)sender;
            BookmarkItem selectedEntry = (BookmarkItem)selectedGrid.DataContext;
            globalSelectedEntry = selectedEntry;
            if (Tct_Bookmark_Tabs.Items != null)
            {
                int index = 0;
                foreach (TabItem tabItem in Tct_Bookmark_Tabs.Items)
                {
                    if ((string)tabItem.Tag == selectedEntry.Link)
                    {
                        Tct_Bookmark_Tabs.SelectedItem = tabItem;
                        tabItem.IsSelected = true;
                        Dispatcher.BeginInvoke((Action)(() => Tct_Bookmark_Tabs.SelectedIndex = index));
                        return;
                    }
                    index++;
                }
            }
            TabItem newTab = new TabItem { Header = new BookmarkTabHeaderControl() { DataContext = this}, DataContext = selectedEntry, Tag = selectedEntry.Link };
            newTab.Content = new TabItemBookmarkBrowserControl();

            Tct_Bookmark_Tabs.Items.Add(newTab);
            newTab.IsSelected = true;
            Dispatcher.BeginInvoke((Action)(() => Tct_Bookmark_Tabs.SelectedIndex = (Tct_Bookmark_Tabs.Items.Count - 1)));
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
