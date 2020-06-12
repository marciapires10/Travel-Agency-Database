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

        private void loadReviews()
        {
            SqlCommand cmd = new SqlCommand("select * from TravelAgency.ReviewsHistoric ('" + pack_ID + "')", cn);

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
                    string descr = reader["Description"].ToString();
                    string cust = reader["Cust_ID"].ToString();
                    var row = new string[] { descr, cust };
                    var list = new ListViewItem(row);
                    listView1.View = View.Details;
                    listView1.Items.Add(list);
                }
            }

            cn.Close();
        }

        private void avgScore()
        {
            SqlCommand cmd = new SqlCommand("select * from TravelAgency.AverageScore ('" + pack_ID + "')", cn);

            if (!verifySGBDConnection())
            {
                return;
            }

            cmd.Connection = cn;
            int avgscore = (int)cmd.ExecuteScalar();

            textBox1.Text = avgscore.ToString();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
