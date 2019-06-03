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
using System.Threading;
using Personalised_News_Feed.Delete;
using System.ComponentModel;
using Personalised_News_Feed.Controls;

namespace Personalised_News_Feed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private UserControl _DisplayUserControl;

        public UserControl DisplayUserControl
        {
            get
            {
                return _DisplayUserControl;
            }
            set
            {
                _DisplayUserControl = value;
                OnPropertyChanged("DisplayUserControl");
            }
        }

        //List<SideBarItem> sideBarItems;
        public MainWindow()
        {

            //InitializeUIComponents();
            //WelcomePage welcome = new WelcomePage();
            //frm_main.NavigationService.Navigate(welcome);
            string language = "en";
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(language);

            InitializeComponent();
            this.DataContext = this;
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

        private void OnPropertyChanged(string v)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void W_Main_Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayUserControl = new Topics();
        }

        private void Sbic_topics_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DisplayUserControl = new Topics();
            Sbic_history.IsActive = false;
            Sbic_bookmarks.IsActive = false;
            Sbic_topics.IsActive = true;
        }

        private void Sbic_history_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DisplayUserControl = new History();
            Sbic_topics.IsActive = false;
            Sbic_bookmarks.IsActive = false;
            Sbic_history.IsActive = true;
        }
    }
}
