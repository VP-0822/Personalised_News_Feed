using Personalised_News_Feed.Core;
using System;
using System.Collections.Generic;
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

    public class Topic
    {
        [XmlElement("topicId")]
        public int topicId { get; set; }

        [XmlElement("isFavorite")]
        public bool isFavorite { get; set; }

        [XmlIgnore]
        public RSSTopic topicDetails { get; set; }

        [XmlIgnore]
        public TopicFeed topicFeed { get; set; }
    }
}
