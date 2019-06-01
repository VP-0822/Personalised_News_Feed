using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Personalised_News_Feed.Core
{
    [XmlRoot("rssFeed")]
    public class RSSFeedConfig
    {
        [XmlElement("topic")]
        public List<RSSTopic> topics { get; set; }
    }

    public class RSSTopic
    {
        [XmlElement("topicId")]
        public int topicId { get; set; }
        [XmlElement("name")]
        public string name { get; set; }
        [XmlElement("url")]
        public string url { get; set; }
    }
}
