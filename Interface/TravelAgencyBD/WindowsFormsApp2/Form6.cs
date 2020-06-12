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
    public partial class Form6 : Form
    {
        private SqlConnection cn;
        private int pack_ID = 0;
        private int ag_ID = 0;
        private int cust_ID = 0;

        public Form6(int packID, int agID, int cusID)
        {
            InitializeComponent();
            cn = getSGBDConnection();
            pack_ID = packID;
            textBox1.Text = pack_ID.ToString();
            ag_ID = agID;
            cust_ID = cusID;
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

        private void btn_cancelBook_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_confirmBook_Click(object sender, EventArgs e)
        {
            string paid = comboBox1.Text;
            int paid_bin;

            if (paid == "True")
            {
                paid_bin = 1;
            }
            else
            {
                paid_bin = 0;
            }

            DateTime bookDate = Convert.ToDateTime(textBox2.Text);
            string details = richTextBox1.Text;



            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spAddBook"
            };

            cmd.Parameters.Add(new SqlParameter("@Paid", SqlDbType.Bit));
            cmd.Parameters.Add(new SqlParameter("@bookDate", SqlDbType.Date));
            cmd.Parameters.Add(new SqlParameter("@Details", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Pack_ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@Ag_ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@Cust_ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
            cmd.Parameters["@Paid"].Value = paid_bin;
            cmd.Parameters["@bookDate"].Value = bookDate;
            cmd.Parameters["@Details"].Value = details;
            cmd.Parameters["@Pack_ID"].Value = pack_ID;
            cmd.Parameters["@Ag_ID"].Value = ag_ID;
            cmd.Parameters["@Cust_ID"].Value = cust_ID;
            cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

            if (!verifySGBDConnection())
            {
                return;
            }

            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
            {
                MessageBox.Show("Booked!");

            }
            else
            {
                MessageBox.Show("Error", "Booking Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
            cn.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("False");
            comboBox1.Items.Add("True");
            comboBox1.SelectedItem = "False";


            var dateAndTime = DateTime.Now;
            textBox2.Text = Convert.ToString(dateAndTime.ToShortDateString());

        }
    }
}
