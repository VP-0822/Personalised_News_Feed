using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Personalised_News_Feed.Classes.UI;

namespace Personalised_News_Feed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List<SideBarItem> sideBarItems;
        public MainWindow()
        {
            InitializeComponent();

            //InitializeUIComponents();
            //WelcomePage welcome = new WelcomePage();
            //frm_main.NavigationService.Navigate(welcome);

        }

        //private void InitializeUIComponents()
        //{
        //    sideBarItems = new List<SideBarItem>();

        //    SideBarItem topics = new SideBarItem() { ItemTitle = "Topics", IsActive = true, IconPath= "&#xf086;", ItemId="1" };
        //    SideBarItem activities = new SideBarItem() { ItemTitle = "Activities", IsActive = false, IconPath = "&#xf086;", ItemId = "2" };
        //    SideBarItem bookMarks = new SideBarItem() { ItemTitle = "Bookmarks", IsActive = false, IconPath = "&#xf086;", ItemId = "3" };
        //    sideBarItems.Add(topics);
        //    sideBarItems.Add(activities);
        //    sideBarItems.Add(bookMarks);

        //}
    }
}
