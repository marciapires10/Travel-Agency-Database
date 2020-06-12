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
using System.Diagnostics;

namespace WindowsFormsApp2
{
    public partial class Form5 : Form
    {
        private SqlConnection cn;

        public Form5()
        {
            InitializeComponent();
            cn = getSGBDConnection();
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

        private void createAccount()
        {
            if(textBox2.TextLength == 0|| textBox3.TextLength == 0 || textBox4.TextLength == 0 || textBox6.TextLength == 0 || textBox1.TextLength == 0)
            {
                MessageBox.Show("All camps must be filled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int phoneNo;
            if (!Int32.TryParse(textBox6.Text,out phoneNo))
            {
                Debug.WriteLine(phoneNo);
                MessageBox.Show("Phone Number must be a number with 9 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (phoneNo < 100000000 || phoneNo > 1000000000)
            {
                Debug.WriteLine(phoneNo+"dwdw");
                MessageBox.Show("Phone Number must be a number with 9 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fname = textBox2.Text;
            string lname = textBox3.Text;
            string email = textBox4.Text;
            string password = textBox1.Text;

            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.CreateAgent"
            };

            cmd.Parameters.Add(new SqlParameter("@Fname", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Lname", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@NewAccountPwd", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@PhoneNo", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));

            cmd.Parameters["@Fname"].Value = fname;
            cmd.Parameters["@Lname"].Value = lname;
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@NewAccountPwd"].Value = password;
            cmd.Parameters["@PhoneNo"].Value = phoneNo;
            cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
            {
                MessageBox.Show("Created account");
            }

            cn.Close();
            this.Close();

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            createAccount();
        }
    }
}
