using HtmlAgilityPack;
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
        public string id { get; set; }

        [XmlElement("title")]
        public string title { get; set; }

        [XmlElement("link")]
        public Link link { get; set; }

        [XmlElement("updated")]
        public string updatedOn { get; set; }

        [XmlElement("entry")]
        public List<Entry> entities { get; set; }

    }

    public class Entry
    {
        [XmlElement("id")]
        public string id { get; set; }
        [XmlElement("title")]
        public string title_ { get; set; }

        [XmlIgnore]
        public string title
        {
            get
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(title_);
                title_ = doc.DocumentNode.InnerText.Replace("&quot;", "").Replace("&nbsp;", "");
                return title_;
            }
            set
            {
                title_ = value;
            }
        }

        [XmlElement("link")]
        public Link link { get; set; }

        [XmlElement("published")]
        public string published { get; set; }
        [XmlElement("updated")]
        public string updated { get; set; }

        [XmlIgnore]
        public DateTime updatedOnDatetime
        {
            get
            {
                return getUpdatedOnDateTime(updated);
            }
        }

        [XmlElement("content")]
        public string content_ { get; set; }
        [XmlIgnore]
        public string content
        {
            get
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(content_);
                content_ = doc.DocumentNode.InnerText.Replace("&quot;", "").Replace("&nbsp;", "");
                return content_;
            }
            set {
                content_ = value;
            }
        }
        [XmlElement("author")]
        public Author author { get; set; }

        private DateTime getUpdatedOnDateTime(string dateString)
        {
            string[] items = dateString.Substring(0, dateString.IndexOf('T')).Split('-');
            return new DateTime(Int32.Parse(items[0]), Int32.Parse(items[1]), Int32.Parse(items[2]));
        }
    }

    public class Author
    {
        [XmlElement("name")]
        public string name { get; set; }
    }

    public class Link
    {
        [XmlAttribute("href")]
        public string href_ { get; set; }

        [XmlIgnore]
        public string href
        {
            get
            {
                if(href_ != null)
                {
                    if(href_.Contains("url="))
                    {
                        int urlindex = href_.IndexOf("url=");
                        string originalSource = href_.Substring(urlindex + 4);
                        int endUrlIndex = originalSource.IndexOf("&");
                        return originalSource.Substring(0, endUrlIndex);
                    }
                    return href_;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                href_ = value;
            }
        }
    }
}
