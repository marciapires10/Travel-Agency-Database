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
    public partial class Form7 : Form
    {
        private SqlConnection cn;
        private int pack_ID = 0;
        private int cust_ID = 0;

        public Form7(int packID, int custID)
        {
            InitializeComponent();
            cn = getSGBDConnection();
            pack_ID = packID;
            cust_ID = custID;
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

        private void addReview()
        {
            string description = richTextBox1.Text;
            int score = trackBar1.Value;


            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spAddReview"
            };

            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Score", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@PackID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@CustID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));

            
            cmd.Parameters["@Description"].Value = description;
            cmd.Parameters["@Score"].Value = score;
            cmd.Parameters["@PackID"].Value = pack_ID;
            cmd.Parameters["@CustID"].Value = cust_ID;
            cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

            if (!verifySGBDConnection())
            {
                return;
            }

            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
            {
                MessageBox.Show("Thanks for your review!");
            }
            else
            {
                MessageBox.Show("Error adding review", "Review error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.Close();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            addReview();
        }
    }
}
