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

        public string showButton { get; set; } = "Visible";

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

        //public ManageTopics(Topics parentUC, Topic selectedTopic)
        //{
        //    InitializeComponent();
        //    Cmb_How_Often.SelectedValue = "Today";
        //    topicUserControl = parentUC;

        //    if (selectedTopic != null)
        //    {
        //        topicContext = selectedTopic;
        //        Tbx_Topic_Name.IsEnabled = false;
        //        header = "Manage topic";
        //        showButton = "Hidden";
        //    }
        //    this.DataContext = this;
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ManageTopics manageTopicsUC = (ManageTopics)sender;
            Cmb_How_Often.SelectedValue = "Today";
            topicUserControl = (Topics)manageTopicsUC.DataContext;
            this.DataContext = this;
            if (topicUserControl.selectedTopic != null)
            {
                topicContext = topicUserControl.selectedTopic;
                Tbx_Topic_Name.IsEnabled = false;
                header = "Manage topic";
                showButton = "Hidden";
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
            newTopic.howOften = (string)Cmb_How_Often.SelectedValue;
            
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
    }
}
