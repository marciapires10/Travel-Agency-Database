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
    public partial class Form3 : Form
    {
        private SqlConnection cn;

        public Form3(string CustID)
        {
            InitializeComponent();
            cn = getSGBDConnection();
            textBox1.Text = CustID;
            textBox1.ReadOnly = true;
            loadBookHistoric(CustID);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
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


        private void loadBookHistoric(string custID)
        {
            
            SqlCommand cmd = new SqlCommand("select * from TravelAgency.CustomerHistoric ('" + custID + "')", cn);
            
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
                    string ID = reader["ID"].ToString();
                    string paid = reader["Paid"].ToString();
                    string bookDate = reader["bookDate"].ToString().Split(' ')[0].ToString(); ;
                    string details = reader["Details"].ToString();
                    string pack_id = reader["Pack_ID"].ToString();
                    string ag_id = reader["Ag_ID"].ToString();
                    var row = new string[] { ID, paid, bookDate, details, pack_id, ag_id };
                    var list = new ListViewItem(row);
                    listView1.View = View.Details;
                    listView1.Items.Add(list);
                }
            }

        }
    }
}
