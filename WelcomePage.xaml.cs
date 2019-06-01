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

namespace Personalised_News_Feed
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void Btn_sign_in_Click(object sender, RoutedEventArgs e)
        {
            string emailAddress = Tbx_email_address.Text;
            string password = Tbx_password.Text;

            if (String.IsNullOrEmpty(emailAddress) || String.IsNullOrEmpty(password))
            {
                Lb_errors.Content = "Please enter valid login credentials";
                Lb_errors.Foreground = Brushes.Red;
                return;
            }
        }

        private void Tbx_password_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate();
            
        }
    }
}
