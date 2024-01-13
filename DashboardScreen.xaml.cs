using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace Multi_Login
{
    /// <summary>
    /// Interaction logic for DashboardScreen.xaml
    /// </summary>
    /// 

    public partial class DashboardScreen : Window
    {
        private DispatcherTimer timer;
        public DashboardScreen()
        {
            InitializeComponent();

            //this for the database...please check method and place the right link
            FetchUserData();

            // Set up a timer to update the date every second
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            // Initial update of the date
            UpdateDate();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the date at each tick
            UpdateDate();
        }

        private void UpdateDate()
        {
            // Set the Label content with the current date
            currentDateLabel.Content = DateTime.Now.ToString("MMMM d, yyyy");
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void FetchUserData()
        {
            try
            {
                //DataBase connect appropriate table and columns
                using (SqlConnection connection = new SqlConnection("your_connection_string"))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SELECT first_name, last_name FROM Users WHERE your_condition", connection))
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            // Update TextBlocks with data from SQL Server
                            string firstName = reader["first_name"].ToString();
                            string lastName = reader["last_name"].ToString();

                            FirstLast2.Text = GetInitials(firstName, lastName);
                            FirstLast1.Text = GetInitials(firstName, lastName);
                            First.Text = firstName;
                            Last.Text = lastName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetInitials(string firstName, string lastName)
        {
            // Combine the first letter of the first name and the first letter of the last name
            return $"{firstName?.FirstOrDefault()}{lastName?.FirstOrDefault()}";
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            // Handle the Click event for Dashboard button
            ResetButtonBackgrounds();
            // Set the color to #000225
            Color customColor = (Color)ColorConverter.ConvertFromString("#000225");
            Dashboardbtn.Background = new SolidColorBrush(customColor);


            // Add your Dashboard button logic here...put link/connection to another window
            // NOTE: Height="700" Width="980"
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            // Handle the Click event for Profile button
            ResetButtonBackgrounds();
            // Set the color to #000225
            Color customColor = (Color)ColorConverter.ConvertFromString("#000225");
            Profilebtn.Background = new SolidColorBrush(customColor);


            // Add your Profile button logic here...put link/connection to another window
            // NOTE: Height="700" Width="980"
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            // Handle the Click event for Study Plan button
            ResetButtonBackgrounds();
            // Set the color to #000225
            Color customColor = (Color)ColorConverter.ConvertFromString("#000225");
            StudyPlnbtn.Background = new SolidColorBrush(customColor);


            // Add your Study Plan button logic here...put link/connection to another window
            // Height="700" Width="980"
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            // Handle the Click event for Checklist button
            ResetButtonBackgrounds();
            // Set the color to #000225
            Color customColor = (Color)ColorConverter.ConvertFromString("#000225");
            Checklistbtn.Background = new SolidColorBrush(customColor);


            // Add your Checklist button logic here...put link/connection to another window
            // NOTE: Height="700" Width="980"
        }

        private void ResetButtonBackgrounds()
        {
            // Reset the background color of all buttons to the normal color
            Dashboardbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0D2043"));
            Profilebtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0D2043"));
            StudyPlnbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0D2043"));
            Checklistbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0D2043"));
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Ask for confirmation
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit the program?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Check the user's response
            if (result == MessageBoxResult.Yes)
            {
                // User clicked Yes, exit the program
                Application.Current.Shutdown();
            }
            // If the result is No, do nothing (the program won't exit)
        }

        private void LogoutButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Set the color to #000225
            Color customColor = (Color)ColorConverter.ConvertFromString("#000225");
            LogoutButton.Background = new SolidColorBrush(customColor);
        }

        private void LogoutButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Restore the original background color when the button is released
            LogoutButton.Background = new SolidColorBrush(Color.FromArgb(255, 13, 32, 67)); // Original color
        }
    }
}
