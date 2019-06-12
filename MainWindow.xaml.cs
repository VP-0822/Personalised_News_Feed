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
using System.ComponentModel;
using Personalised_News_Feed.Controls;
using System.Collections.ObjectModel;
using System.Resources;
using Personalised_News_Feed.Properties;
using System.Globalization;
using System.Reflection;

namespace Personalised_News_Feed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private UserControl _DisplayUserControl;
        public ObservableCollection<SideBarItem> sideBarItems;
        public List<string> cultures = new List<string> { "en english", "ar arabic" };
        //Use GetAvailableCultures()

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

            string language = Properties.Settings.Default.language;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(language);

            InitializeComponent();

            sideBarItems = new ObservableCollection<SideBarItem>();
            SideBarItem topics = new SideBarItem { ItemId = "1", ItemTitle = "Topics", IsActive = "Visible", IconPath = @"\Resources\Images\Icons\documentW.png" };
            SideBarItem history = new SideBarItem { ItemId = "2", ItemTitle = "History", IsActive = "Hidden", IconPath = @"\Resources\Images\Icons\historyW.png" };
            SideBarItem bookmarks = new SideBarItem { ItemId = "3", ItemTitle = "Bookmarks", IsActive = "Hidden", IconPath = @"\Resources\Images\Icons\bookmarkW.png" };

            sideBarItems.Add(topics);
            sideBarItems.Add(history);
            sideBarItems.Add(bookmarks);
            Itc_Side_Items.ItemsSource = sideBarItems;

            loadLanguageCombobox();
            this.DataContext = this;
            
        }

        private void loadLanguageCombobox()
        {
            List<ComboBoxItem> cultureItems = new List<ComboBoxItem>();
            foreach(string culture in cultures)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = culture;
                cultureItems.Add(item);
            }

            Cmb_Languages.ItemsSource = cultureItems;
        }

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

        private void Grd_SideBarItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Grid selectedGrid = (Grid)sender;
            SideBarItem selectedSideBarItem = (SideBarItem)selectedGrid.DataContext;
            foreach(SideBarItem sideBarItem in sideBarItems)
            {
                if(sideBarItem.ItemId.Equals(selectedSideBarItem.ItemId))
                {
                    sideBarItem.IsActive = "Visible";
                }
                else
                {
                    sideBarItem.IsActive = "Hidden";
                }
            }

            switch(selectedSideBarItem.ItemId)
            {
                case "1":
                    DisplayUserControl = new Topics();
                    break;
                case "2":
                    DisplayUserControl = new History();
                    break;
                case "3":
                    DisplayUserControl = new Bookmarks();
                    break;
                default:
                    break;
            }
        }

        private void Cmb_Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cultureComboBox = (ComboBox)sender;
            var cultureText = ((ComboBoxItem)cultureComboBox.SelectedItem).Content.ToString().Split(' ')[0];
            
            MessageBoxResult result = MessageBox.Show("Application will be closed to effect language change. Do you want to continue", "Language changed",MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(MessageBoxResult.Yes == result)
            {
                Properties.Settings.Default.language = cultureText;
                Properties.Settings.Default.Save();
                Close();
            }
        }

        //private IEnumerable<string> GetAvailableCultures()
        //{
        //    List<string> results = new List<string>();
        //    ResourceManager rm = new ResourceManager(typeof(Resources));
        //    CultureInfo[] cultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures);
        //    foreach(CultureInfo cultureSelected in cultureInfos)
        //    {

        //    }
        //}
    }
}
