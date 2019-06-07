﻿using Personalised_News_Feed.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Personalised_News_Feed.Classes.Core
{

    [XmlRoot("userSelection")]
    public class UserTopics
    {
        [XmlElement("userTopic")]
        public List<UserTopic> topics { get; set; }
    }

    public class UserTopic
    {
        [XmlElement("userId")]
        public int userId { get; set; }

        [XmlElement("topic")]
        public List<Topic> topics { get; set; }
    }

    public class Topic : INotifyPropertyChanged
    {
        [XmlElement("topicId")]
        public int topicId { get; set; }

        [XmlElement("isFavorite")]
        public bool isFavorite
        {
            get { return isFavorite_; }
            set
            {
                isFavorite_ = value;
                OnPropertyChanged("isFavorite");
                OnPropertyChanged("favoriteIcon");
            }
        }

        [XmlIgnore]
        private bool isFavorite_;

        [XmlIgnore]
        public RSSTopic topicDetails { get; set; }

        [XmlIgnore]
        public TopicFeed topicFeed { get; set; }

        [XmlIgnore]
        public string favoriteIcon
        {
            get
            {
                if(isFavorite)
                {
                    return @"..\Resources\Images\Icons\bookmark_done.png";
                }
                else
                {
                    return @"..\Resources\Images\Icons\bookmark_undone.png";
                }
            }
            set
            {
                favoriteIcon = value;
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
