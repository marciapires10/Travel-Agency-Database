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
using System.Data.SqlClient;
using System.Data;

namespace TravelAgencyBD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private SqlConnection con;

        public MainWindow()
        {
            InitializeComponent();
        }

        private SqlConnection getTADBConnection()
        {
            return new SqlConnection("Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p1g9;Persist Security Info=True;User ID=p1g9;Password=4rmariO");
        }

        private bool verifyTADBConnection()
        {
            if(con == null)
            {
                con = getTADBConnection();
            }

            if(con.State != ConnectionState.Open)
            {
                con.Open();
            }

            return con.State == ConnectionState.Open;
        } 


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (!verifyTADBConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM TravelAgency.Agent", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (txtEmail.Text.Equals(reader["Email"]) && txtPassword.Password.Equals(reader["Password"]))
                {
                    MessageBox.Show("Login successfully!");
                    MainMenu page = new MainMenu();
                    var host = new Window();
                    host.Content = page;
                    host.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }


            con.Close();
        }
    }
}
