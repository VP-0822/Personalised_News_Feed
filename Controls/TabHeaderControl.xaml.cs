using Personalised_News_Feed.Core;
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
    /// Interaction logic for TabHeaderControl.xaml
    /// </summary>
    public partial class TabHeaderControl : UserControl
    {
        public Entry currentEntry { get; set; }

        private ViewTopic viewTopicUC;
        public TabHeaderControl()
        {
            InitializeComponent();
        }

        private void Tbk_Close_X_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TabItem selectedItem = null;
            foreach(TabItem ti in viewTopicUC.Tct_Topic_Tabs.Items)
            {
                if(!(ti.DataContext is Entry))
                {
                    continue;
                }
                if(((Entry)ti.DataContext).id.Equals(currentEntry.id))
                {
                    selectedItem = ti;
                    break;
                }
            }

            if(selectedItem != null)
            {
                viewTopicUC.Tct_Topic_Tabs.Items.Remove(selectedItem);
            }
        }

        private void UC_Tab_Header_Loaded(object sender, RoutedEventArgs e)
        {
            TabHeaderControl tabHeaderUC = (TabHeaderControl)sender;
            viewTopicUC = (ViewTopic)tabHeaderUC.DataContext;
            currentEntry = viewTopicUC.globalSelectedEntry;
        }
    }
}
