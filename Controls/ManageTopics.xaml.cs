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
    /// Interaction logic for ManageTopics.xaml
    /// </summary>
    public partial class ManageTopics : UserControl
    {
        private Topics topicUserControl;

        public ManageTopics()
        {
            InitializeComponent();
        }

        public ManageTopics(Topics parentObject)
        {
            topicUserControl = parentObject;
            InitializeComponent();
        }


    }
}
