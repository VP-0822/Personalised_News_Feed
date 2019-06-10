using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personalised_News_Feed.Classes.Core
{
    public class AllUserHistory
    {
        public List<UserHistory> UsersHistory { get; set; }
    }

    public class UserHistory
    {
        public int UserId { get; set; }

        public List<TopicHistory> Histories { get; set; }
    }

    public class TopicHistory
    {
        public string TopicName { get; set; }

        public string LinkTitle { get; set; }

        public string Link { get; set; }

        public DateTime BrowsedDate { get; set; }
        
    }
}
