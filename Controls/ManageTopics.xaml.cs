using Personalised_News_Feed.Classes.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ManageTopics.xaml
    /// </summary>
    public partial class ManageTopics : UserControl, INotifyPropertyChanged
    {
        private Topics topicUserControl;

        private Topic _topicContext { get; set; }

        private string showButton_ { get; set; } = "Visible";

        public string showButton
        {
            get
            {
                return showButton_;
            }
            set
            {
                showButton_ = value;
                OnPropertyChanged("showButton");
            }
        }

        private bool isDropdownChanged = false;

        private string _header { get; set; } = "Add topic"; 

        public string header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
                OnPropertyChanged("header");
            }
        }

        public Topic topicContext
        {
            get
            {
                return _topicContext;
            }
            set
            {
                _topicContext = value;
                OnPropertyChanged("topicContext");
            }
        } 

        public ManageTopics()
        {
            InitializeComponent();
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ManageTopics manageTopicsUC = (ManageTopics)sender;
            Cmb_How_Often.SelectedValue = "Day";
            topicUserControl = (Topics)manageTopicsUC.DataContext;
            this.DataContext = this;
            if (topicUserControl.selectedTopic != null)
            {
                topicContext = topicUserControl.selectedTopic;
                Tbx_Topic_Name.IsEnabled = false;
                header = "Manage topic";
                showButton = "Hidden";
                if (topicContext.howOften.Equals("Today"))
                {
                    Cmb_How_Often.SelectedValue = "Day";
                }
                else
                {
                    Cmb_How_Often.SelectedValue = "Week";
                }
            }
        }

        private void Btn_Add_Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            if(validateSubmit() == false)
            {
                return;
            }

            Topic newTopic = new Topic();
            newTopic.isFavorite = (bool)Chb_IsFavorite.IsChecked;
            if(!String.IsNullOrEmpty((string)Cmb_How_Often.SelectedValue))
            {
                if(((string)Cmb_How_Often.SelectedValue).Equals("Day"))
                {
                    newTopic.howOften = "Today";
                }
                else
                {
                    newTopic.howOften = "This week";
                }
            }
            
            
            topicUserControl.AddNewTopic(newTopic, Tbx_Topic_Name.Text);
            MessageBox.Show("Successfully added new topic");
        }

        private bool validateSubmit()
        {
            if(String.IsNullOrEmpty(Tbx_Topic_Name.Text))
            {
                MessageBox.Show("Please enter topic name.");
                return false;
            }

            if(Cmb_How_Often.SelectedItem == null)
            {
                MessageBox.Show("Please select duration for feed.");
                return false;
            }
            return true;
        }

        private void Btn_Back_To_Feed_Click(object sender, RoutedEventArgs e)
        {
            topicUserControl.TopicUserControl = new ViewTopic() { DataContext = topicUserControl.DataContext};
            topicUserControl.isManageTopics = false;
            topicUserControl.selectedTopic = ((Topics)topicUserControl.DataContext).userFavoriteTopics[0];
            ((Topics)topicUserControl.DataContext).Lbx_FavoriteTopics.SelectedItem = topicUserControl.selectedTopic;
        }

        private void Chb_IsFavorite_Checked(object sender, RoutedEventArgs e)
        {   if(topicUserControl.selectedTopic != null)
            {
                topicUserControl.AddNewTopic(topicContext, Tbx_Topic_Name.Text);
            }
        }

        private void Cmb_How_Often_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isDropdownChanged && topicContext != null)
            {
                if (((string)Cmb_How_Often.SelectedValue).Equals("Day"))
                {
                    topicContext.howOften = "Today";
                }
                else
                {
                    topicContext.howOften = "This week";
                }
                topicUserControl.AddNewTopic(topicContext, Tbx_Topic_Name.Text);
            }
            isDropdownChanged = true;
        }
    }
}
