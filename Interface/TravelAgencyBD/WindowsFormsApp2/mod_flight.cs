using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class mod_flight : Form
    {

        private SqlConnection cn;
        private int ID;
        public mod_flight(int flight_ID)
        {
            InitializeComponent();
            this.ID = flight_ID;
            cn = getSGBDConnection();
            load_flights();
        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p1g9;Persist Security Info=True;User ID=p1g9;Password=4rmariO");
        }

        private void load_flights()
        {
            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd = new SqlCommand("SELECT ICAO + ' - ' + Name AS Airline FROM TravelAgency.Airline", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            airline.Items.Clear();


            while (reader.Read())
            {
                airline.Items.Add(reader["Airline"].ToString());
            }

            cn.Close();
            
            if (!verifySGBDConnection())
            {
                return;
            }


            dep_city.Items.Clear();
            arr_city.Items.Clear();

            cmd = new SqlCommand("SELECT City+', '+Country AS Location FROM TravelAgency.CC", cn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dep_city.Items.Add(reader["Location"].ToString());
                arr_city.Items.Add(reader["Location"].ToString());
            }

            cn.Close();

            if (!verifySGBDConnection())
            {
                return;
            }


            cmd = new SqlCommand("SELECT * FROM TravelAgency.Flight WHERE ID = @ID", cn);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            cmd.Parameters["@ID"].Value = this.ID;
            
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Debug.WriteLine(reader["Airline"].ToString());
                foreach(String s in airline.Items){
                    if (s.Contains(reader["Airline"].ToString()))
                    {
                        Debug.WriteLine(airline.Items.IndexOf(s));
                        airline.SelectedIndex = airline.Items.IndexOf(s);
                    }
                }
                foreach (String s in classtype.Items)
                {
                    if (s.Contains(reader["classType"].ToString()))
                    {
                        classtype.SelectedIndex = classtype.Items.IndexOf(s);
                    }
                }

                price.Value = (decimal) reader["Price"];

                foreach (String s in dep_city.Items)
                {
                    if (s.Contains(reader["CC_Depart"].ToString())){
                        dep_city.SelectedIndex = dep_city.Items.IndexOf(s);
                    }
                }

                foreach (String s in arr_city.Items)
                {
                    if (s.Contains(reader["CC_Arriv"].ToString()))
                    {
                        arr_city.SelectedIndex = arr_city.Items.IndexOf(s);
                    }
                }

                dep_time.Value = (DateTime) reader["departTime"];
                arr_time.Value = (DateTime) reader["arrivalTime"];
            }

            cn.Close();
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


        private void modify_Click(object sender, EventArgs e)
        {
            if (airline.SelectedItem == null || 
                classtype.SelectedItem == null||
                price.Value <= 0 ||
                dep_city.SelectedItem == null ||
                dep_time.Value.CompareTo(DateTime.Today) < 0 ||
                arr_city.SelectedItem == null ||
                dep_time.Value.CompareTo(DateTime.Today) < 0 )
            {
                MessageBox.Show("All camps must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String _airline = airline.SelectedItem.ToString();
            String _classtype = classtype.SelectedItem.ToString();
            decimal _price = price.Value;
            String city_dep = dep_city.SelectedItem.ToString();
            String city_arr = arr_city.SelectedItem.ToString();
            DateTime date_dep = dep_time.Value;
            DateTime date_arr = arr_time.Value;

            if(date_dep.CompareTo(date_arr) >= 0)
            {
                MessageBox.Show("Arrival Date and Time must be later thans Departure Data and Time", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else if (city_dep.Equals(city_arr))
            {
                MessageBox.Show("Departure Local and Arrival Local must be different", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _airline = _airline.Split(' ')[0];
            city_dep = city_dep.Split(',')[0];
            city_arr = city_arr.Split(',')[0];


            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd = new SqlCommand
            {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "TravelAgency.spEditFlight"
            };

            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@departTime", SqlDbType.SmallDateTime));
            cmd.Parameters.Add(new SqlParameter("@arrivalTime", SqlDbType.SmallDateTime));
            cmd.Parameters.Add(new SqlParameter("@Airline", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@classType", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Price", SqlDbType.SmallMoney));
            cmd.Parameters.Add(new SqlParameter("@departLoc", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@arrivalLoc", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar,250));
            cmd.Parameters["@ID"].Value = this.ID;
            cmd.Parameters["@departTime"].Value = date_dep;
            cmd.Parameters["@arrivalTime"].Value = date_arr;
            cmd.Parameters["@Airline"].Value = _airline;
            cmd.Parameters["@classType"].Value = _classtype;
            cmd.Parameters["@Price"].Value = _price;
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

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mod_flight_Load(object sender, EventArgs e)
        {

        }
    }
}
