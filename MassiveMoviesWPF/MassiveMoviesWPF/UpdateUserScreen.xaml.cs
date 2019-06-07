using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for UpdateUserInfo.xaml
    /// </summary>
    public partial class UpdateUserScreen : Window
    {
        private static string Apiroot = "http://10.0.2.2:5000/api";
        private static Users _user;

        public UpdateUserScreen(Users user)
        {
            InitializeComponent();

            _user = user;
        }


        public string UpdateUserDetails(int id, string fName, string lName, string email, string passwrd, string confirmPasswrd)
        {
 
            _user.Id = id;
            _user.Fname = fName;
            _user.Lname = lName;
            _user.Email = email;
            _user.Passwrd = passwrd;


            string json = JsonConvert.SerializeObject(_user);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{Apiroot}/users/{id}");
            req.Method = "PUT";
            req.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            };

            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        private void UpdateUser(object sender, RoutedEventArgs e)
        {
            if (ConfirmPassword.Password == Password.Password)
            {
                MessageBox.Show(UpdateUserDetails(_user.Id, FirstName.Text, LastName.Text, Email.Text, Password.Password, ConfirmPassword.Password));
            }
            else
            {
                MessageBox.Show("Passwords didn't match. Try again");
            }

            FirstName.Text = $"";
            LastName.Text = $"";
            Email.Text = $"";

        }
    }
}
