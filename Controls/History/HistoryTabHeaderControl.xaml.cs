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
    /// Interaction logic for HistoryTabHeaderControl.xaml
    /// </summary>
    public partial class HistoryTabHeaderControl : UserControl
    {
        TopicHistory currentEntry { get; set; }

        private History historyUC;
        public HistoryTabHeaderControl()
        {
            InitializeComponent();
        }

        private void UC_History_Tab_Loaded(object sender, RoutedEventArgs e)
        {
            HistoryTabHeaderControl tabHeaderUC = (HistoryTabHeaderControl)sender;
            historyUC = (History)tabHeaderUC.DataContext;
            currentEntry = historyUC.globalSelectedEntry;
        }

        private void Btn_Close_X_MouseUp(object sender, RoutedEventArgs e)
        {
            TabItem selectedItem = null;
            foreach (TabItem ti in historyUC.Tct_Topic_Tabs.Items)
            {
                if (!(ti.DataContext is TopicHistory))
                {
                    continue;
                }
                if (((TopicHistory)ti.DataContext).Link.Equals(currentEntry.Link))
                {
                    selectedItem = ti;
                    break;
                }
            }

            if (selectedItem != null)
            {
                historyUC.Tct_Topic_Tabs.Items.Remove(selectedItem);
            }

            if(historyUC.Tct_Topic_Tabs.Items.Count == 0)
            {
                historyUC.IsNoTabVisible = true;
            }
        }
    }
}
