using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Personalised_News_Feed.Core
{
    [XmlRoot("feed")]
    public class TopicFeed
    {
        [XmlElement("id")]
        public int id { get; set; }
        [XmlElement("title")]
        public string title { get; set; }
        [XmlElement("link")]
        public string link { get; set; }
        [XmlElement("updated")]
        public string updatedOn { get; set; }
        [XmlElement("entry")]
        public List<Entry> entities { get; set; }
    }

    public class Entry
    {
        [XmlElement("id")]
        public int id { get; set; }
        [XmlElement("title")]
        public string title { get; set; }
        [XmlElement("link")]
        public string link { get; set; }
        [XmlElement("published")]
        public string published { get; set; }
        [XmlElement("updated")]
        public string updated { get; set; }
        [XmlElement("content")]
        public string content { get; set; }
        [XmlElement("author")]
        public Author author { get; set; }

    }

    public class Author
    {
        [XmlElement("name")]
        public string name { get; set; }
    }
}
