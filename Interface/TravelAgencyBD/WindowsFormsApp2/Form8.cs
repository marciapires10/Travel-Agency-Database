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
    public partial class Form8 : Form
    {
        private SqlConnection cn;
        private int pack_ID = 0;

        public Form8(int packID)
        {
            InitializeComponent();
            cn = getSGBDConnection();
            pack_ID = packID;
            loadReviews();
            avgScore();
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


        private string getCustName(int custID)
        {
            if (!verifySGBDConnection())
            {
                return "";
            }

            SqlCommand cmd;
            cmd = new SqlCommand("Select TravelAgency.GetCustName('" + custID + "')", cn);

            string name = (string)cmd.ExecuteScalar();

            cn.Close();

            return name;

        }

        private void loadReviews()
        {
            SqlCommand cmd = new SqlCommand("select * from TravelAgency.ReviewsHistoric ('" + pack_ID + "')", cn);

            List<string> descr = new List<string>();
            List<int> cust = new List<int>();

            if (!verifySGBDConnection())
            {
                return;
            }

            cmd.Connection = cn;

            listView1.Items.Clear();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    descr.Add(reader["Description"].ToString());
                    cust.Add(Int32.Parse(reader["Cust_ID"].ToString()));
                }
            }

            cn.Close();

            for (int i = 0; i < descr.Count - 1; i++) {
                string nameComplete = getCustName(cust[i]);
                string fname = nameComplete.Split(' ')[0];
                string lname = nameComplete.Split(' ')[1];
                var row = new string[] { descr[i], fname + " " + lname };
                var list = new ListViewItem(row);
                listView1.View = View.Details;
                listView1.Items.Add(list);
            }


        }

        private void avgScore()
        {

            SqlCommand cmd = new SqlCommand("select TravelAgency.AverageScore ('" + pack_ID + "')", cn);

            if (!verifySGBDConnection())
            {
                return;
            }

            cmd.Connection = cn;
            decimal avgscore = decimal.Parse(cmd.ExecuteScalar().ToString());
            if (avgscore < 0) textBox1.Text = "No Reviews";
            else textBox1.Text = avgscore.ToString();

            cn.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
