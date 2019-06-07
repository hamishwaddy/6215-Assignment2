using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using api.Models;
using Newtonsoft.Json;

namespace MassiveMoviesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string Apiroot = "http://10.0.2.2:5000/api";
        private static Users _user = new Users();

        public MainWindow(Users users)
        {
            InitializeComponent();

            //MovieTitle.ItemSource = GetMoviesFromApi();
            //MoviesPanel.DataContext = GetMoviesList();
            UserName.Text = $"{users.Fname}";
            _user = users;
            EditUserDetails.Click += EditUserDetails_Click1;

        }

        private void EditUserDetails_Click1(object sender, RoutedEventArgs e)
        {
            UpdateUserScreen updateuserscreen = new UpdateUserScreen(_user);
            updateuserscreen.Show();
        }

        static List<Movies> movies = GetMoviesList();


        public Movies GetSelectedMovie(string selMovie)
        {

            Movies selectedMovie = new Movies();
            //var movies = GetMoviesList();

            // need to loop thru movies here
            foreach (Movies x in movies)
            {
                //selectedMovie = x;

                if (x.Title == selMovie)
                {
                    selectedMovie = x;
                    x.Picture = $"https://m.media-amazon.com/images/M/{x.Picture}.jpg";

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(x.Picture, UriKind.Absolute);
                    bitmap.EndInit();

                    MoviePicture.Source = bitmap;

                    MoviesPanel.DataContext = x;
                }

                AddToWishlist.Visibility = Visibility.Visible;
            }


            return selectedMovie;
        }

        public static List<Movies> GetMoviesList()
        {

            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString($"{Apiroot}/movies");

                List<Movies> movies = JsonConvert.DeserializeObject<List<Movies>>(json);

                return movies;

            }
        }

        public string UpdateMovies(int id, string title, string summary, string picture, string genre, double rating)
        {
            Movies movie = new Movies();
            movie.Id = id;
            movie.Title = title;
            movie.Summary = summary;
            movie.Picture = picture;
            movie.Genre = genre;
            movie.Rating = rating;

            string json = JsonConvert.SerializeObject(movie);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{Apiroot}/movies/{id}");
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

        public List<Users> GetUsersFromApi()
        {
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString($"{Apiroot}/users");

                List<Users> users = JsonConvert.DeserializeObject<List<Users>>(json);

                return users;
            }
        }

        
        private void MovieToSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            GetSelectedMovie(MovieToSearch.Text);
            //AddToWishlist.Visibility = Visibility.Visible;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            GetSelectedMovie(MovieToSearch.Text);
        }


        private void AddToWishlist_Click(object sender, RoutedEventArgs e)
        {
            //UpdateMovies(Wishlist.Text);

        }

        private void OpenWishlist(object sender, RoutedEventArgs e)
        {
            WishlistPanel.Visibility = Visibility.Visible;
        }

        //private void EditUserDetails_Click(object sender, RoutedEventArgs e)
        //{
            
        //}
    }
}
