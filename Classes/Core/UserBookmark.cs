using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personalised_News_Feed.Classes.Core
{
    public class AllUserBookmark
    {
        public List<UserBookmark> UserBookmarks { get; set; }
    }

    public class UserBookmark
    {
        public int UserId { get; set; }

        public List<TopicWiseBookmark> TopicWiseBookmarks{get; set;}

    }
    public class TopicWiseBookmark
    {
        public string TopicName { get; set; }

        public List<BookmarkItem> Bookmarks { get; set; }
    }

    public class BookmarkItem
    {
        public string LinkTitle { get; set; }

        public string Link { get; set; }

        public DateTime BookmarkedDate { get; set; }
    }
}
