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
    /// Interaction logic for SideBarItemControl.xaml
    /// </summary>
    public partial class SideBarItemControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SideBarItemControl));
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(SideBarItemControl));
        public static readonly DependencyProperty ItemIconProperty = DependencyProperty.Register("IconPath", typeof(string), typeof(SideBarItemControl));

        public static readonly DependencyProperty ItemIdProperty = DependencyProperty.Register("Id", typeof(string), typeof(SideBarItemControl));

        public string Title { get { return (string)GetValue(TitleProperty); } set { SetValue(TitleProperty, value); } }

        public bool IsActive { get { return (bool)GetValue(IsActiveProperty); } set { SetValue(IsActiveProperty, value); } }

        public string IconPath { get { return (string)GetValue(ItemIconProperty); } set { SetValue(ItemIconProperty, value); } }

        public string Id { get { return (string)GetValue(ItemIdProperty); } set { SetValue(ItemIdProperty, value); } }

        public string IsSelectedVisibility { get { if(!IsActive) { return "Hidden"; } else { return ""; }
                    } }

        public SideBarItemControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
