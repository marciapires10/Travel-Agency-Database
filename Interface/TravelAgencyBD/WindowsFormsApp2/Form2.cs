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
    public partial class Form2 : Form
    {

        private SqlConnection cn;
        private int currentCustomer = 0;
        private int currentAcc = 0;

        public Form2()
        {

            this.AutoSize = true;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            loadCustomers();
            loadAccommodation();

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

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 addCustomer = new Form3();
            addCustomer.ShowDialog();
        }

        // load Customers
        private void loadCustomers()
        {
            if (!verifySGBDConnection())
            {
                return;
            }


            SqlCommand cmd = new SqlCommand("SELECT * FROM TravelAgency.Person", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBox1.Items.Clear();

            while (reader.Read())
            {
                Person p = new Person();
                p.Email = reader["Email"].ToString();
                p.Fname = reader["Fname"].ToString();
                p.Lname = reader["Lname"].ToString();
                p.PhoneNo = reader["phoneNo"].ToString();
                listBox1.Items.Add(p);
            }

            cn.Close();

            currentCustomer++;
            //showCustomer();
        }

        public void showCustomer()
        {
            if(listBox1.Items.Count == 0 | currentCustomer < 0)
            {
                return;
            }

            Person p = new Person();
            p = (Person)listBox1.Items[currentCustomer];
            lbFname.Text = p.Fname;
            lbLname.Text = p.Lname;
            lbEmail.Text = p.Email;
            lbPhone.Text = p.PhoneNo;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex >= 0)
            {
                currentCustomer = listBox1.SelectedIndex;
                showCustomer();
            }
        }

        // load accommodations
       private void loadAccommodation()
        {
            if (!verifySGBDConnection())
            {
                return;
            }


            SqlCommand cmd = new SqlCommand("SELECT * FROM TravelAgency.Accommodation", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBox2.Items.Clear();

            while (reader.Read())
            {
                Accommodation ac = new Accommodation();
                ac.ID = (int)reader["ID"];
                ac.Name = reader["Name"].ToString();
                ac.Price = (decimal)reader["Price"];
                ac.Location = reader["CC_Location"].ToString();
                listBox2.Items.Add(ac);
            }

            cn.Close();

            currentAcc++;
            //showCustomer();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void searchAcc()
        {
            if (!verifySGBDConnection())
                return;

            string search = textBox1.Text;

            SqlCommand cmd = new SqlCommand("SELECT * FROM TravelAgency.Accommodation WHERE CC_Location= @CC_Location", cn);

            SqlParameter _location = new SqlParameter("@CC_Location", SqlDbType.VarChar);

            cmd.Parameters.Add(_location);

            cmd.Parameters["@CC_Location"].Value = search;

            cmd.Connection = cn;

            SqlDataReader reader = cmd.ExecuteReader();
            listBox2.Items.Clear();

            if (reader.Read())
            {
                Accommodation ac = new Accommodation();
                ac.ID = (int)reader["ID"];
                ac.Name = reader["Name"].ToString();
                ac.Price = (decimal)reader["Price"];
                ac.Location = reader["CC_Location"].ToString();
                listBox2.Items.Add(ac);
            }

            else
            {
                MessageBox.Show("Could not found destination", "Accommodation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            searchAcc();
        }
    }
}
