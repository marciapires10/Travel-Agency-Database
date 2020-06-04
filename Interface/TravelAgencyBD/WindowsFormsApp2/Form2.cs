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


            SqlCommand cmd = new SqlCommand("Select Person.Email, Person.Fname, Person.Lname, Person.phoneNo, CustID, NIF from TravelAgency.Customer Join TravelAgency.Person ON TravelAgency.Customer.Email = TravelAgency.Person.Email;", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBox1.Items.Clear();

            while (reader.Read())
            {
                Customer p = new Customer();
                p.Email = reader["Email"].ToString();
                p.Fname = reader["Fname"].ToString();
                p.Lname = reader["Lname"].ToString();
                p.PhoneNo = reader["phoneNo"].ToString();
                p.ID = reader["CustID"].ToString();
                p.NIF = reader["NIF"].ToString();
                listBox1.Items.Add(p);
            }

            cn.Close();

            currentCustomer++;
            showButtonsCustomer();
            //showCustomer();
        }

        public void showCustomer()
        {
            if(listBox1.Items.Count == 0 | currentCustomer < 0)
            {
                return;
            }

            Customer p = new Customer();
            p = (Customer)listBox1.Items[currentCustomer];
            textBox2.Text = p.Fname;
            textBox3.Text = p.Lname;
            textBox4.Text = p.Email;
            textBox5.Text = p.NIF;
            textBox6.Text = p.PhoneNo;

            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;


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

        private void addCustomer()
        {
            string fname = textBox2.Text;
            string lname = textBox3.Text;
            string email = textBox4.Text;
            int NIF = Int32.Parse(textBox5.Text);
            int phoneNo = Int32.Parse(textBox6.Text);

            int custID = 100;

            if (string.IsNullOrEmpty(fname))
            {
                MessageBox.Show("First name has to be defined!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrEmpty(lname))
            {
                MessageBox.Show("Last name has to be defined!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email has to be defined!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "TravelAgency.spAddCustomer"
                };

                cmd.Parameters.Add(new SqlParameter("@Fname", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@Lname", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@NIF", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PhoneNo", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CustID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
                cmd.Parameters["@Fname"].Value = fname;
                cmd.Parameters["@Lname"].Value = lname;
                cmd.Parameters["@Email"].Value = email;
                cmd.Parameters["@NIF"].Value = NIF;
                cmd.Parameters["@PhoneNo"].Value = phoneNo;
                cmd.Parameters["@CustID"].Value = custID;
                cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

                if (!verifySGBDConnection())
                {
                    return;
                }

                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                cn.Close();
            }
        }

        private void removeCustomer()
        {
            string email = textBox4.Text;
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Username has to be defined.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            else
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "TravelAgency.spDeleteCustomer"
                };

                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
                cmd.Parameters["@Email"].Value = email;
                cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

                if (!verifySGBDConnection())
                {
                    return;
                }

                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
                {
                    MessageBox.Show("" + cmd.Parameters["@responseMsg"].Value);
                }
                else
                {
                    MessageBox.Show("" + cmd.Parameters["@responseMsg"].Value, "Remove User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                textBox4.Text = "";
                cn.Close();
            }
        }


        public void showButtonsCustomer()
        {
            btn_Add.Visible = true;
            btn_Edit.Visible = true;
            btn_Remove.Visible = true;
            btn_OK.Visible = false;
            btn_Cancel.Visible = false;
        }


        public void unlockControls()
        {
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox5.ReadOnly = false;
            textBox6.ReadOnly = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            searchAcc();
        }

        // want to add new Customer
        private void btn_Add_Click(object sender, EventArgs e)
        {
            addCustomer();
            loadCustomers();
        }

        // want to edit a Customer
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            unlockControls();
        }

        // want to delete a Customer
        private void btn_Remove_Click(object sender, EventArgs e)
        {
            removeCustomer();
            loadCustomers();
            

        }

        // confirm
        private void btn_OK_Click(object sender, EventArgs e)
        {

        }

        // cancel
        private void btn_Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
