using System;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using Multi_Login;
namespace Multi_Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        private const string ConnectionStringName = "Multi_Login.Properties.Settings.Setting";
        public MainWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName]?.ConnectionString;
            if (string.IsNullOrEmpty(con.ConnectionString))
            {
                MessageBox.Show($"Connection string '{ConnectionStringName}' not found in the configuration file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                if (VerifyUser(txtUsername.Text, txtPassword.Password))
                {
          
                    // Open the DashboardScreen window
                    DashboardScreen dashboardScreen = new DashboardScreen();
                    dashboardScreen.Show();

                    // Minimize the current window
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Username or password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                    
        }
        private bool VerifyUser(string username, string password)
        {
            try
            {
                using (con)
                {
                    con.Open();
                    using (com)
                    {
                        com.Connection = con;
                        com.CommandText = "SELECT Status FROM Users WHERE Username=@username AND Password=@password";
                        com.Parameters.AddWithValue("@username", username);
                        com.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return Convert.ToBoolean(dr["Status"]);
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
