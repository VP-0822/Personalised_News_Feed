using HtmlAgilityPack;
using Personalised_News_Feed.Classes.Core;
using Personalised_News_Feed.Core;
using Personalised_News_Feed.Core.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Topics.xaml
    /// </summary>
    public partial class Topics : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Topic> userFavoriteTopics;
        public ObservableCollection<Topic> userGeneralTopics;
        public RSSFeedConfig rssFeedConfig;

        private AllUserHistory allUserHistory;
        private UserHistory userHistory { get; set; }

        private AllUserBookmark allUserBookmark;
        private UserBookmark userBookmark { get; set; }

        private UserTopics allUsers;
        private UserTopic selectedUser;

        public bool isManageTopics { get; set; } = false;

        private UserControl _TopicUserControl;

        public UserControl TopicUserControl
        {
            get
            {
                return _TopicUserControl;
            }
            set
            {
                _TopicUserControl = value;
                OnPropertyChanged("TopicUserControl");
            }
        }

        public Topic selectedTopic {
            get { return selectedTopic_; }
            set
            {
                selectedTopic_ = value;
                OnPropertyChanged("selectedTopic");
            }
        }
        private Topic selectedTopic_;

        public ObservableCollection<Topic> removedTopics = new ObservableCollection<Topic>();
        public Topics()
        {
            InitializeComponent();
            //WriteContent();
            ReadRSSConfig();
            ReadTopics();
            loadTopicData();
            loadHistory();
            loadBookmarks();
            Lbx_FavoriteTopics.ItemsSource = userFavoriteTopics;
            Lbx_GeneralTopics.ItemsSource = userGeneralTopics;
            Lbx_FavoriteTopics.SelectedItem = userFavoriteTopics[0];
            selectedTopic = (Topic)Lbx_FavoriteTopics.SelectedItem;
            //Cmb_How_Often.SelectedValue = "Today";
            this.DataContext = this;
            if (isManageTopics)
            {
                //TopicUserControl = new ManageTopics(this, selectedTopic);
                TopicUserControl = new ManageTopics();
            }
            else
            {
                TopicUserControl = new ViewTopic();
            }
        }

        private void ReadRSSConfig()
        {
            rssFeedConfig = XMLSerializerWrapper.ReadXML<RSSFeedConfig>("config\\RSSFeedConfig.xml");

            int highestTopicId = (from n in rssFeedConfig.topics select n).OrderByDescending(x => x.topicId).ToList()[0].topicId;
            Properties.Settings.Default.Next_Topic_Id = highestTopicId + 1;
            Properties.Settings.Default.Save();
        }

        private void ReadTopics()
        {
            allUsers = XMLSerializerWrapper.ReadXML<UserTopics>("Data\\UserTopicSelection.xml");
            selectedUser = (from n in allUsers.topics where n.userId == 1 select n).ToList()[0];

            userFavoriteTopics = new ObservableCollection<Topic>((from n in selectedUser.topics where n.isFavorite == true select n));
            userGeneralTopics = new ObservableCollection<Topic>((from n in selectedUser.topics where n.isFavorite == false select n));


        }

        private void loadTopicData()
        {
            foreach (Topic eachTopic in userFavoriteTopics)
            {
                eachTopic.topicDetails = (from n in rssFeedConfig.topics where n.topicId == eachTopic.topicId select n).ToList()[0];
                eachTopic.topicFeed = XMLSerializerWrapper.ReadXML<TopicFeed>("Data\\TopicId" + eachTopic.topicId + ".xml");
            }

            foreach (Topic eachTopic in userGeneralTopics)
            {
                eachTopic.topicDetails = (from n in rssFeedConfig.topics where n.topicId == eachTopic.topicId select n).ToList()[0];
                eachTopic.topicFeed = XMLSerializerWrapper.ReadXML<TopicFeed>("Data\\TopicId" + eachTopic.topicId + ".xml");
            }
        }

        private void loadHistory()
        {
            allUserHistory = XMLSerializerWrapper.ReadXML<AllUserHistory>("Data\\UserHistory.xml");
            if (allUserHistory != null)
            {
                userHistory = (from n in allUserHistory.UsersHistory where n.UserId == 1 select n).ToList()[0];
            }
        }

        private void loadBookmarks()
        {
            allUserBookmark = XMLSerializerWrapper.ReadXML<AllUserBookmark>("Data\\UserBookmarks.xml");
            if(allUserBookmark != null)
            {
                userBookmark = (from n in allUserBookmark.UserBookmarks where n.UserId == 1 select n).ToList()[0];
            }
        }

        private void Lbx_FavoriteTopics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lbx_FavoriteTopics.SelectedItem == null)
            {
                return;
            }

            if (Lbx_GeneralTopics.SelectedItem != null)
            {
                Lbx_GeneralTopics.SelectedItem = null;
            }

            selectedTopic = (Topic)Lbx_FavoriteTopics.SelectedItem;
            if (isManageTopics)
            {
                //TopicUserControl = new ManageTopics(this, selectedTopic);
                TopicUserControl = new ManageTopics();
            }
            else
            {
                TopicUserControl = new ViewTopic();
            }
        }

        private void Lbx_GeneralTopics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lbx_GeneralTopics.SelectedItem == null)
            {
                return;
            }

            if (Lbx_FavoriteTopics.SelectedItem != null)
            {
                Lbx_FavoriteTopics.SelectedItem = null;
            }

            selectedTopic = (Topic)Lbx_GeneralTopics.SelectedItem;
            if (isManageTopics)
            {
                //TopicUserControl = new ManageTopics(this, selectedTopic);
                TopicUserControl = new ManageTopics();
            }
            else
            {
                TopicUserControl = new ViewTopic();
            }
        }

        //private void WriteContent()
        //{
        //    UserTopics uts = new UserTopics();
        //    UserTopic userTopic = new UserTopic();
        //    userTopic.userId = 1;
        //    Topic topic = new Topic { topicId = 1, isFavorite = true };
        //    Topic topic1 = new Topic { topicId = 2, isFavorite = false };
        //    userTopic.topics = new List<Topic> { topic, topic1 };

        //    UserTopic userTopic1 = new UserTopic();
        //    userTopic1.userId = 2;
        //    Topic topic2 = new Topic { topicId = 1, isFavorite = false };
        //    Topic topic3 = new Topic { topicId = 3, isFavorite = true };
        //    userTopic1.topics = new List<Topic> { topic2, topic3 };

        //    uts.topics = new List<UserTopic> { userTopic, userTopic1 };

        //    XMLSerializerWrapper.WriteXML<UserTopics>(uts, "UserTopicSelection.xml");
        //}

        private void OnPropertyChanged(string v)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddNewTopic(Topic newTopic, string newTopicName)
        {
            List<RSSTopic> newTopics = (from n in rssFeedConfig.topics where n.name == newTopicName select n).ToList();
            if (newTopics != null && newTopics.Count > 0)
            {
                RSSTopic newTopicDetails = newTopics[0];
                newTopic.topicId = newTopicDetails.topicId;
                newTopic.topicDetails = newTopicDetails;
                newTopic.topicFeed = XMLSerializerWrapper.ReadXML<TopicFeed>("Data\\TopicId" + newTopic.topicId + ".xml");
            }

            if (newTopic.isFavorite == true)
            {
                userFavoriteTopics.Add(newTopic);
            }
            else
            {
                userGeneralTopics.Add(newTopic);
            }
            selectedTopic = newTopic;
            TopicUserControl = new ManageTopics();
            writeToFile();
        }

        private void Btn_Add_Topic_Click(object sender, RoutedEventArgs e)
        {
            isManageTopics = true;
            //TopicUserControl = new ManageTopics(this, null);
            selectedTopic = null;
            TopicUserControl = new ManageTopics();
        }

        private void Tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = Tbx_filter.Text;

            if (string.IsNullOrEmpty(query))
            {
                Lbx_FavoriteTopics.ItemsSource = userFavoriteTopics;
                Lbx_GeneralTopics.ItemsSource = userGeneralTopics;
                return;
            }

            var favResults = from n in userFavoriteTopics where n.topicDetails.name.ToLower().Contains(query.ToLower()) select n;
            var genResults = from n in userGeneralTopics where n.topicDetails.name.ToLower().Contains(query.ToLower()) select n;

            Lbx_FavoriteTopics.ItemsSource = favResults;
            Lbx_GeneralTopics.ItemsSource = genResults;
        }

        private void Btn_Remove_Item_Click(object sender, RoutedEventArgs e)
        {
            Button removeTopic = (Button)sender;
            int selectedTopicId = (int)removeTopic.Tag;
            List<Topic> selectedTopicsForRemove = (List<Topic>)(from n in userFavoriteTopics where n.topicId == selectedTopicId select n).ToList();
            Topic selectedTopicForRemove = null;
            if (selectedTopicsForRemove != null && selectedTopicsForRemove.Count > 0)
            {
                selectedTopicForRemove = selectedTopicsForRemove[0];
            }
            if (selectedTopicForRemove != null)
            {
                userFavoriteTopics.Remove(selectedTopicForRemove);
                removedTopics.Add(selectedTopicForRemove);
                return;
            }

            selectedTopicsForRemove = (List<Topic>)(from n in userGeneralTopics where n.topicId == selectedTopicId select n).ToList();

            if (selectedTopicsForRemove != null && selectedTopicsForRemove.Count > 0)
            {
                selectedTopicForRemove = selectedTopicsForRemove[0];
            }
            if (selectedTopicForRemove != null)
            {
                userGeneralTopics.Remove(selectedTopicForRemove);
                removedTopics.Add(selectedTopicForRemove);
                return;
            }

        }

        private void Btn_Manage_Item_Click(object sender, RoutedEventArgs e)
        {
            isManageTopics = true;
            Button removeTopic = (Button)sender;
            int selectedTopicId = (int)removeTopic.Tag;
            List<Topic> selectedTopicsForManage = (List<Topic>)(from n in userFavoriteTopics where n.topicId == selectedTopicId select n).ToList();
            Topic selectedTopicForManage = null;
            if (selectedTopicsForManage != null && selectedTopicsForManage.Count > 0)
            {
                selectedTopicForManage = selectedTopicsForManage[0];
            }
            if (selectedTopicForManage != null)
            {
                Lbx_FavoriteTopics.SelectedItem = selectedTopicForManage;
                return;
            }

            selectedTopicsForManage = (List<Topic>)(from n in userGeneralTopics where n.topicId == selectedTopicId select n).ToList();

            if (selectedTopicsForManage != null && selectedTopicsForManage.Count > 0)
            {
                selectedTopicForManage = selectedTopicsForManage[0];
            }
            if (selectedTopicForManage != null)
            {
                Lbx_GeneralTopics.SelectedItem = selectedTopicForManage;
                return;
            }
        }

        private void writeToFile()
        {
            List<Topic> collectionToWrite = new List<Topic>();
            collectionToWrite.AddRange(userFavoriteTopics);
            collectionToWrite.AddRange(userGeneralTopics);

            selectedUser.topics = collectionToWrite;

            XMLSerializerWrapper.WriteXML<UserTopics>(allUsers, "Data\\UserTopicSelection.xml");
        }

        public void writeHistoryToFile(Entry selectedEntry)
        {
            TopicHistory history = new TopicHistory();
            history.TopicName = selectedTopic.topicDetails.name;
            history.LinkTitle = selectedEntry.title;
            history.Link = selectedEntry.link.href;
            history.BrowsedDate = DateTime.Now;

            userHistory.Histories.Add(history);

            List<TopicHistory> sortedHistory = (from n in userHistory.Histories select n).OrderByDescending(x => x.BrowsedDate).ToList();
            userHistory.Histories = sortedHistory;

            XMLSerializerWrapper.WriteXML<AllUserHistory>(allUserHistory, "Data\\UserHistory.xml");

        }

        public void writeBookmarkToFile(Entry selectedEntry)
        {
            BookmarkItem bookmarkItem = new BookmarkItem();
            bookmarkItem.BookmarkedDate = DateTime.Now;
            bookmarkItem.Link = selectedEntry.link.href;
            bookmarkItem.LinkTitle = selectedEntry.title;

            foreach(TopicWiseBookmark twBookmark in userBookmark.TopicWiseBookmarks)
            {
                if(twBookmark.TopicName.ToLower().Equals(selectedTopic.topicDetails.name.ToLower()))
                {
                    twBookmark.Bookmarks.Add(bookmarkItem);
                    List<BookmarkItem> filteredBookmarkItems = (from n in twBookmark.Bookmarks select n).OrderByDescending(x => x.BookmarkedDate).ToList();
                    twBookmark.Bookmarks = filteredBookmarkItems;
                }
            }

            XMLSerializerWrapper.WriteXML<AllUserBookmark>(allUserBookmark, "Data\\UserBookmarks.xml");
        }

    }
}
