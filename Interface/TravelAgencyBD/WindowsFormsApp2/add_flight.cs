using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class add_flight : Form
    {

        private SqlConnection cn;
        public add_flight()
        {
            InitializeComponent();
            cn = getSGBDConnection();
            load_flights();

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addflight_departure_addLoc_Click(object sender, EventArgs e)
        {
            new_CC add_cc = new new_CC();
            add_cc.ShowDialog();
            load_flights();
        }

        private void addflight_addnew_button_Click(object sender, EventArgs e)
        {

            if (addflight_airline_combobox.SelectedItem == null || 
                addflight_classtype_combobox.SelectedItem == null||
                addflight_price_textbox.Value <= 0 ||
                addflight_depLoc.SelectedItem == null ||
                addflight_depTime.Value.CompareTo(DateTime.Today) < 0 ||
                addflight_arrLoc.SelectedItem == null ||
                addflight_depTime.Value.CompareTo(DateTime.Today) < 0 )
            {
                MessageBox.Show("All camps must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String airline = addflight_airline_combobox.SelectedItem.ToString();
            String classtype = addflight_classtype_combobox.SelectedItem.ToString();
            decimal price = addflight_price_textbox.Value;
            String city_dep = addflight_depLoc.SelectedItem.ToString();
            String city_arr = addflight_arrLoc.SelectedItem.ToString();
            DateTime date_dep = addflight_depTime.Value;
            DateTime date_arr = addflight_arrTime.Value;

            if(date_dep.CompareTo(date_arr) >= 0)
            {
                MessageBox.Show("Arrival Date and Time must be later thans Departure Data and Time", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else if (city_dep.Equals(city_arr))
            {
                MessageBox.Show("Departure Local and Arrival Local must be different", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            airline = airline.Split(' ')[0];
            city_dep = city_dep.Split(',')[0];
            city_arr = city_arr.Split(',')[0];


            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd = new SqlCommand
            {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "TravelAgency.spAddFlight"
            };

            cmd.Parameters.Add(new SqlParameter("@departTime", SqlDbType.SmallDateTime));
            cmd.Parameters.Add(new SqlParameter("@arrivalTime", SqlDbType.SmallDateTime));
            cmd.Parameters.Add(new SqlParameter("@Airline", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@classType", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Price", SqlDbType.SmallMoney));
            cmd.Parameters.Add(new SqlParameter("@departLoc", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@arrivalLoc", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar,250));
            cmd.Parameters["@departTime"].Value = date_dep;
            cmd.Parameters["@arrivalTime"].Value = date_arr;
            cmd.Parameters["@Airline"].Value = airline;
            cmd.Parameters["@classType"].Value = classtype;
            cmd.Parameters["@Price"].Value = price;
            cmd.Parameters["@departLoc"].Value = city_dep;
            cmd.Parameters["@arrivalLoc"].Value = city_arr;
            cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
            {
                MessageBox.Show("" + cmd.Parameters["@responseMsg"].Value);
            } else {
                MessageBox.Show("" + cmd.Parameters["@responseMsg"].Value, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cn.Close();

            this.Close();



        }

        private void addflight_arrival_addLoc_Click(object sender, EventArgs e)
        {
            new_CC add_cc = new new_CC();
            add_cc.ShowDialog();
            load_flights();
        }

        private void load_flights()
        {
            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd = new SqlCommand("SELECT ICAO + ' - ' + Name AS Airline FROM TravelAgency.Airline", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            addflight_airline_combobox.Items.Clear();


            while (reader.Read())
            {
                addflight_airline_combobox.Items.Add(reader["Airline"].ToString());
            }

            cn.Close();
            
            if (!verifySGBDConnection())
            {
                return;
            }


            addflight_depLoc.Items.Clear();
            addflight_arrLoc.Items.Clear();

            cmd = new SqlCommand("SELECT City+', '+Country AS Location FROM TravelAgency.CC", cn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                addflight_depLoc.Items.Add(reader["Location"].ToString());
                addflight_arrLoc.Items.Add(reader["Location"].ToString());
            }

            cn.Close();


        }
        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p1g9;Persist Security Info=True;User ID=p1g9;Password=4rmariO");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
            {
                cn = getSGBDConnection();
            }

            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }

            return cn.State == ConnectionState.Open;
        }

        private void addflight_cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
