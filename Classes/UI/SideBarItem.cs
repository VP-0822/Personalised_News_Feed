using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personalised_News_Feed.Classes.UI
{
    public class SideBarItem
    {
        public string ItemTitle { get; set; }

        public bool IsActive { get; set; }

        public string IconPath { get; set; }

        public string ItemId { get; set; }
    }
}
