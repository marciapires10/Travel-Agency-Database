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
    public partial class Form1 : Form
    {
        private SqlConnection cn;

        public Form1()
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p1g9;Persist Security Info=True;User ID=p1g9;Password=4rmariO");
        }

        private bool verifySGBDConnection()
        {
            if(cn == null)
            {
                cn = getSGBDConnection();
            }

            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }

            return cn.State == ConnectionState.Open;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!verifySGBDConnection())
                return;


            string email = textBox1.Text;
            string password = textBox2.Text;


            if (email == "" && password == "")
            {
                MessageBox.Show("Inputs cannot be left in blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (email == "")
            {
                MessageBox.Show("Please enter e-mail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (password == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("TravelAgency.VerifyAgent", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter aEmail = new SqlParameter("@Email", SqlDbType.VarChar);
                SqlParameter aPassword = new SqlParameter("@AccountPwd", SqlDbType.VarChar);
                SqlParameter aResult = new SqlParameter("@Result", SqlDbType.Int);

                cmd.Parameters.Add(aEmail);
                cmd.Parameters.Add(aPassword);
                cmd.Parameters.Add(aResult);

                cmd.Parameters["@Email"].Value = email;
                cmd.Parameters["@AccountPwd"].Value = password;
                cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                cmd.Connection = cn;

                cmd.ExecuteNonQuery();


                if ((int)cmd.Parameters["@Result"].Value == 1)
                {
                    MessageBox.Show("You have logged in successfully\n" + email);
                    this.Hide();
                    Form2 menu = new Form2();
                    menu.ShowDialog();
                }

                else
                {
                    MessageBox.Show("Login failed, try again", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
            cn.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
