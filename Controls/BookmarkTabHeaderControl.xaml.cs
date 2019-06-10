using Personalised_News_Feed.Classes.Core;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for BookmarkTabHeaderControl.xaml
    /// </summary>
    public partial class BookmarkTabHeaderControl : UserControl
    {
        BookmarkItem currentEntry { get; set; }

        private Bookmarks bookmarksUC;
        public BookmarkTabHeaderControl()
        {
            InitializeComponent();
        }

        private void UC_Browser_Header_Loaded(object sender, RoutedEventArgs e)
        {
            BookmarkTabHeaderControl tabHeaderUC = (BookmarkTabHeaderControl)sender;
            bookmarksUC = (Bookmarks)tabHeaderUC.DataContext;
            currentEntry = bookmarksUC.globalSelectedEntry;
        }

        private void Tbk_Close_X_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TabItem selectedItem = null;
            foreach (TabItem ti in bookmarksUC.Tct_Bookmark_Tabs.Items)
            {
                if (!(ti.DataContext is BookmarkItem))
                {
                    continue;
                }
                if (((BookmarkItem)ti.DataContext).Link.Equals(currentEntry.Link))
                {
                    selectedItem = ti;
                    break;
                }
            }

            if (selectedItem != null)
            {
                bookmarksUC.Tct_Bookmark_Tabs.Items.Remove(selectedItem);
            }

            if (bookmarksUC.Tct_Bookmark_Tabs.Items.Count == 0)
            {
                bookmarksUC.NoTabVisibility = "Visible";
            }
        }
    }
}
