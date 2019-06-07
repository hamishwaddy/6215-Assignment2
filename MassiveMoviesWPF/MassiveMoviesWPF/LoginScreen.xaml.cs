using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using api.Models;
using Newtonsoft.Json;

namespace MassiveMoviesWPF
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private static Users User = new Users();

        private bool LoginUser(string uiboxuser, string uiboxpass)
        {
            // Get API Data
            using (WebClient wc = new WebClient())
            {
                string data = wc.DownloadString("http://10.0.2.2:5000/api/users");
                List<Users> users = JsonConvert.DeserializeObject <List<Users>>(data);

                foreach (var i in users)
                {
                    // Check the user name
                    if (uiboxuser == i.Uname)
                    {
                        // successful

                        // Check the user password
                        if (uiboxpass == i.Passwrd)
                        {
                            // successful
                            User = i;
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        //private void LoginScreen_OnMouseDown(object sender, RoutedEventArgs e)
        //{
        //    if (e. == MouseButtonState.Pressed)
        //    {
        //        DragMove();
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (LoginUser(UserName.Text, UserPassword.Password.ToString()))
            {
                
                //Open the new window
                MainWindow mainWindow = new MainWindow(User);
                mainWindow.Show();

            }
        }
    }
}