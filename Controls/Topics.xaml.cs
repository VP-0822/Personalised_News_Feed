﻿using HtmlAgilityPack;
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
        private RSSFeedConfig rssFeedConfig;
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
            Lbx_FavoriteTopics.ItemsSource = userFavoriteTopics;
            Lbx_GeneralTopics.ItemsSource = userGeneralTopics;
            Lbx_FavoriteTopics.SelectedItem = userFavoriteTopics[0];
            selectedTopic = (Topic)Lbx_FavoriteTopics.SelectedItem;

            this.DataContext = this;
        }

        private void ReadRSSConfig()
        {
            rssFeedConfig = XMLSerializerWrapper.ReadXML<RSSFeedConfig>("config\\RSSFeedConfig.xml");
        }

        private void ReadTopics()
        {
            UserTopics allUsers = XMLSerializerWrapper.ReadXML<UserTopics>("data\\UserTopicSelection.xml");
            UserTopic forUser1 = (from n in allUsers.topics where n.userId == 1 select n).ToList()[0];

            userFavoriteTopics = new ObservableCollection<Topic>((from n in forUser1.topics where n.isFavorite == true select n));
            userGeneralTopics = new ObservableCollection<Topic>((from n in forUser1.topics where n.isFavorite == false select n));
        }

        private void loadTopicData()
        {
            foreach(Topic eachTopic in userFavoriteTopics)
            {
                eachTopic.topicDetails = (from n in rssFeedConfig.topics where n.topicId == eachTopic.topicId select n).ToList()[0];
                eachTopic.topicFeed = XMLSerializerWrapper.ReadXML<TopicFeed>("data\\TopicId" + eachTopic.topicId+".xml");
            }

            foreach (Topic eachTopic in userGeneralTopics)
            {
                eachTopic.topicDetails = (from n in rssFeedConfig.topics where n.topicId == eachTopic.topicId select n).ToList()[0];
                eachTopic.topicFeed = XMLSerializerWrapper.ReadXML<TopicFeed>("data\\TopicId" + eachTopic.topicId+".xml");
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

        private void Grd_FeedEntry_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid selectedGrid = (Grid)sender;
            Entry selectedEntry = (Entry)selectedGrid.DataContext;

            string tabHeader = selectedEntry.title.Substring(0,20);
            TabItem newTab = new TabItem { Header = tabHeader, DataContext = selectedEntry };
            newTab.Content = new TabItemBrowserControl();
            //Tct_Topic_Tabs.Items.Add(new TabItem { Header= "Hello"});

            Tct_Topic_Tabs.Items.Add(newTab);
            Tct_Topic_Tabs.SelectedItem = newTab;
        }

        private void Img_Add_To_Favorites_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(selectedTopic.isFavorite)
            {
                userFavoriteTopics.Remove(selectedTopic);
                userGeneralTopics.Add(selectedTopic);
                selectedTopic.isFavorite = !selectedTopic.isFavorite;
            }
            else
            {
                userGeneralTopics.Remove(selectedTopic);
                userFavoriteTopics.Add(selectedTopic);
                selectedTopic.isFavorite = !selectedTopic.isFavorite;
            }
        }

        private void Btn_Add_Topic_Click(object sender, RoutedEventArgs e)
        {


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
    }
}
