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
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
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
            if (listBox1.Items.Count == 0 | currentCustomer < 0)
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
            if (listBox1.SelectedIndex >= 0)
            {
                currentCustomer = listBox1.SelectedIndex;
                showCustomer();
            }
        }

        // load accommodations
        /*private void loadAccommodation()
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
         }*/

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
            //listBox2.Items.Clear();

            if (reader.Read())
            {
                Accommodation ac = new Accommodation();
                ac.ID = (int)reader["ID"];
                ac.Name = reader["Name"].ToString();
                ac.Price = (decimal)reader["Price"];
                ac.Location = reader["CC_Location"].ToString();
                //listBox2.Items.Add(ac);
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
                //cmd.Parameters.Add(new SqlParameter("@CustID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
                cmd.Parameters["@Fname"].Value = fname;
                cmd.Parameters["@Lname"].Value = lname;
                cmd.Parameters["@Email"].Value = email;
                cmd.Parameters["@NIF"].Value = NIF;
                cmd.Parameters["@PhoneNo"].Value = phoneNo;
                //cmd.Parameters["@CustID"].Value = custID;
                cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

                if (!verifySGBDConnection())
                {
                    return;
                }

                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
                {
                    MessageBox.Show("Customer added");

                }
                else
                {
                    MessageBox.Show("Customer already exists", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                cn.Close();
            }
        }

        private void editCustomer()
        {
            string fname = textBox2.Text;
            string lname = textBox3.Text;
            string email = textBox4.Text;
            int NIF = Int32.Parse(textBox5.Text);
            int phoneNo = Int32.Parse(textBox6.Text);

            if (string.IsNullOrEmpty(fname))
            {
                MessageBox.Show("First name has to be defined!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrEmpty(lname))
            {
                MessageBox.Show("Last name has to be defined!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "TravelAgency.spEditCustomer"
                };

                cmd.Parameters.Add(new SqlParameter("@Fname", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@Lname", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@NIF", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PhoneNo", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
                cmd.Parameters["@Fname"].Value = fname;
                cmd.Parameters["@Lname"].Value = lname;
                cmd.Parameters["@Email"].Value = email;
                cmd.Parameters["@NIF"].Value = NIF;
                cmd.Parameters["@PhoneNo"].Value = phoneNo;
                cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

                if (!verifySGBDConnection())
                {
                    return;
                }

                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Error");
                }

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

        public void clearFields()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            searchAcc();
        }

        // want to add new Customer
        private void btn_Add_Click(object sender, EventArgs e)
        {
            unlockControls();
            textBox4.ReadOnly = false;
            clearFields();
            btn_Add.Visible = false;
            btn_Edit.Visible = false;
            btn_Remove.Visible = false;
            btn_OK.Visible = true;
            btn_Cancel.Visible = true;
        }

        // want to edit a Customer
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            unlockControls();
            editCustomer();
            loadCustomers();
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
            addCustomer();
            loadCustomers();
        }

        // cancel
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            showButtonsCustomer();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void loadAccommodation()
        {
            int nAcc = 0;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.RowCount = 0;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spLoadAcc"
            };


            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 206));
                    Panel x = new Panel();

                    tableLayoutPanel1.RowCount++;
                    x.Location = new System.Drawing.Point(24, 111);
                    x.Size = new System.Drawing.Size(1010, 204);
                    x.TabIndex = 3;

                    Label lb1 = new Label();

                    lb1.Location = new System.Drawing.Point(20, 28);
                    lb1.Size = new System.Drawing.Size(180, 150);
                    lb1.Name = "pic_" + tableLayoutPanel1.RowCount;
                    lb1.TabIndex = 3;
                    lb1.TabStop = false;
                    lb1.Text = "OLA";


                    Label lb2 = new Label();
                    Label lb3 = new Label();
                    Label lb4 = new Label();
                    Label lb5 = new Label();

                    lb5.AutoSize = true;
                    lb5.Location = new System.Drawing.Point(305, 140);
                    lb5.TabIndex = 3;
                    lb5.MaximumSize = new System.Drawing.Size(100, 20);

                    lb4.AutoSize = true;
                    lb4.Location = new System.Drawing.Point(220, 140);
                    lb4.TabIndex = 3;
                    lb4.MaximumSize = new System.Drawing.Size(100, 20);

                    lb3.AutoSize = true;
                    lb3.Location = new System.Drawing.Point(220, 53);
                    lb3.TabIndex = 2;
                    lb3.MaximumSize = new System.Drawing.Size(425, 80);

                    lb2.AutoSize = true;
                    lb2.Font = new Font(lb2.Font, FontStyle.Bold);
                    lb2.Location = new System.Drawing.Point(220, 28);
                    lb2.MaximumSize = new System.Drawing.Size(200, 15);
                    lb2.TabIndex = 1;

                    lb2.Name = "label9_" + tableLayoutPanel1.RowCount;
                    lb3.Name = "label10_" + tableLayoutPanel1.RowCount;
                    lb4.Name = "label11_" + tableLayoutPanel1.RowCount;
                    lb5.Name = "label12_" + tableLayoutPanel1.RowCount;

                    lb2.Text = reader["Name"].ToString() + " - " + reader["Price"].ToString();
                    lb3.Text = reader["Description"].ToString();
                    lb4.Font = new Font(lb4.Font, FontStyle.Bold);
                    lb4.Text = "Location:";
                    lb5.Text = reader["CC_Location"].ToString();


                    x.Controls.Add(lb1);
                    x.Controls.Add(lb2);
                    x.Controls.Add(lb3);
                    x.Controls.Add(lb4);
                    x.Controls.Add(lb5);

                    tableLayoutPanel1.Controls.Add(x, 0, tableLayoutPanel1.RowCount - 1);
                    nAcc++;
                }
            }
            cn.Close();

        }
    }
}
