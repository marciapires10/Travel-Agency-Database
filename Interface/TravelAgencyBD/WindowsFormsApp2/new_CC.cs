using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    public partial class new_CC : Form
    {
        private SqlConnection cn;
        public new_CC()
        {
            InitializeComponent();
            cn = getSGBDConnection();

        }

        private void cc_cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cc_addNew_button_Click(object sender, EventArgs e)
        {
            if (city_textbox.TextLength == 0 || country_textbox.TextLength == 0)
            {
                MessageBox.Show("Please insert City and Country","Error: No City or Country",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            String city = city_textbox.Text;
            String country = country_textbox.Text;

            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd = new SqlCommand
            {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "TravelAgency.spAddNewCC"
            };

            cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar,250));
            cmd.Parameters["@City"].Value = city;
            cmd.Parameters["@Country"].Value = country;
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

    }
}
