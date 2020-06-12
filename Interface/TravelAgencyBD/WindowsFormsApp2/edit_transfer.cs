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
    public partial class edit_transfer : Form
    {
        private SqlConnection cn;
        private int ID;
        public edit_transfer(int transfer_id)
        {
            InitializeComponent();
            this.ID = transfer_id;
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
            if (!verifySGBDConnection())
            {
                return;
            }
            
            cmd = new SqlCommand("SELECT * FROM TravelAgency.Transfer WHERE ID = @ID", cn);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            cmd.Parameters["@ID"].Value = this.ID;
            
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Debug.WriteLine(reader["Company"].ToString());
                company.Text = reader["Company"].ToString();

                price_num.Value = (decimal) reader["Price"];

                foreach (String s in dep_city_combobox.Items)
                {
                    if (s.Contains(reader["CC_Depart"].ToString())){
                        dep_city_combobox.SelectedIndex = dep_city_combobox.Items.IndexOf(s);
                    }
                }

                foreach (String s in arr_city_combobox.Items)
                {
                    if (s.Contains(reader["CC_Arriv"].ToString()))
                    {
                        arr_city_combobox.SelectedIndex = arr_city_combobox.Items.IndexOf(s);
                    }
                }

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

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void edit_Click(object sender, EventArgs e)
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
                    CommandText = "TravelAgency.spEditTransfer"
            };

            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@Company", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Price", SqlDbType.SmallMoney));
            cmd.Parameters.Add(new SqlParameter("@departLoc", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@arrivalLoc", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar,250));
            cmd.Parameters["@ID"].Value = this.ID;
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
    }
}
