﻿using Personalised_News_Feed.Classes.Core;
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
    /// Interaction logic for TabItemBookmarkBrowserControl.xaml
    /// </summary>
    public partial class TabItemBookmarkBrowserControl : UserControl
    {
        public TabItemBookmarkBrowserControl()
        {
            InitializeComponent();
        }

        private void TbIBC_NewBookmarkTab_Loaded(object sender, RoutedEventArgs e)
        {
            TabItemBookmarkBrowserControl control = (TabItemBookmarkBrowserControl)sender;
            BookmarkItem entry = (BookmarkItem)control.DataContext;
            Wbs_TabBrowser.Navigate(entry.Link);
        }

        private void Wbs_TabBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            dynamic activeX = this.Wbs_TabBrowser.GetType().InvokeMember("ActiveXInstance",
                BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, this.Wbs_TabBrowser, new object[] { });

            activeX.Silent = true;
        }

        private void Tbk_Refresh_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BookmarkItem entry = (BookmarkItem)this.DataContext;
            Wbs_TabBrowser.Navigate(entry.Link);
        }
    }
}
