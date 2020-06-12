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
    public partial class add_transfer : Form
    {
        
        private SqlConnection cn;
        public add_transfer()
        {
            InitializeComponent();
            cn = getSGBDConnection();
            load_transfer();
        }

        private void load_transfer()
        {
            if (!verifySGBDConnection())
            {
                return;
            }

            dep_city_combobox.Items.Clear();
            arr_city_combobox.Items.Clear();

            SqlCommand cmd = new SqlCommand("SELECT City+', '+Country AS Location FROM TravelAgency.CC", cn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dep_city_combobox.Items.Add(reader["Location"].ToString());
                arr_city_combobox.Items.Add(reader["Location"].ToString());
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

        private void new_dep_city_button_Click(object sender, EventArgs e)
        {
            new_CC add_cc = new new_CC();
            add_cc.ShowDialog();
            load_transfer();
        }

        private void new_arr_city_button_Click(object sender, EventArgs e)
        {
            new_CC add_cc = new new_CC();
            add_cc.ShowDialog();
            load_transfer();
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (company.Text == null ||
                price_num.Value <= 0 ||
                dep_city_combobox.SelectedItem == null ||
                arr_city_combobox.SelectedItem == null)
            {
                MessageBox.Show("All camps must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String _company = company.Text;
            decimal _price = price_num.Value;
            String city_dep = dep_city_combobox.SelectedItem.ToString();
            String city_arr = arr_city_combobox.SelectedItem.ToString();
            String country_dep = city_dep.Split(',')[1];
            String country_arr = city_arr.Split(',')[1];

            System.Diagnostics.Debug.WriteLine(country_dep + " " + country_arr);
            if(country_arr != country_dep)
            {
                MessageBox.Show("Both Cities must be in the same Country!", "Invalid Cities", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _company = _company.Split(' ')[0];
            city_dep = city_dep.Split(',')[0];
            city_arr = city_arr.Split(',')[0];


            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd = new SqlCommand
            {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "TravelAgency.spAddTransfer"
            };

            cmd.Parameters.Add(new SqlParameter("@Company", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Price", SqlDbType.SmallMoney));
            cmd.Parameters.Add(new SqlParameter("@departLoc", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@arrivalLoc", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar,250));
            cmd.Parameters["@Company"].Value = _company;
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
    }
}
