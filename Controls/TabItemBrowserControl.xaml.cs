using Personalised_News_Feed.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for TabItemBrowserControl.xaml
    /// </summary>
    public partial class TabItemBrowserControl : UserControl
    {
        public TabItemBrowserControl()
        {
            InitializeComponent();
            
        }

        private void TbIBC_NewTab_Loaded(object sender, RoutedEventArgs e)
        {
            TabItemBrowserControl control = (TabItemBrowserControl)sender;
            Entry entry = (Entry)control.DataContext;
            Wbs_TabBrowser.Navigate(entry.link.href);
            
        }

        private void Wbs_TabBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            dynamic activeX = this.Wbs_TabBrowser.GetType().InvokeMember("ActiveXInstance",
                BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, this.Wbs_TabBrowser, new object[] { });

            activeX.Silent = true;
        }
    }
}
