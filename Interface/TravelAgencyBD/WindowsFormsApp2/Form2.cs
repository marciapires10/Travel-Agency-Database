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
    public partial class Form2 : Form
    {

        private SqlConnection cn;
        private int currentCustomer = 0;
        private string acc_name;
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();

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
            dataGridView1.DataSource = bindingSource1;
            GetData();
            loadFlights();

            initialFieldsPack();
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
            
        }

        // CUSTOMER TAB

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

        // showCustomer
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        // add new Customer
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

        // edit Customer
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

        // remove Customer
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

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flight_add_button_Click(object sender, EventArgs e)
        {
            add_flight add_new_flight = new add_flight();
            add_new_flight.ShowDialog();
            GetData();
        }

        private void GetData()
        {
            try
            {
                if (!verifySGBDConnection())
                {
                    return;
                }

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter("Select * from TravelAgency.Flight", cn);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();

                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView1.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("Error");
            }
        }



        // ACCOMMODATION TAB

        private void pack_selected(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Text = "None";
            comboBox1.Items.Add("None");
            comboBox1.Items.Add("PriceAsc");
            comboBox1.Items.Add("PriceDesc");

            comboBox2.Items.Clear();
            comboBox2.Text = "None";
            comboBox2.Items.Add("None");
            comboBox2.Items.Add("DiscountAsc");
            comboBox2.Items.Add("DiscountDesc");
            comboBox2.Items.Add("Active");
            comboBox2.Items.Add("Not available");

            textBox12.ReadOnly = true;

            comboBox3.Items.Clear();
            comboBox3.Items.Add("True");
            comboBox3.Items.Add("False");

            loadAccommodation();
            loadPromo();
        }

        private void loadAccommodation()
        {
            int nAcc = 0;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.RowCount = nAcc;
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
                    accImage accimg = new accImage();
                    accimg.ID = (int)reader["ID"];
                    Panel x = new Panel();

                    tableLayoutPanel1.RowCount++;
                    x.Location = new System.Drawing.Point(25, 110);
                    x.Size = new System.Drawing.Size(1010, 205);
                    x.TabIndex = 3;

                    try
                    {
                        byte[] bytes = System.Convert.FromBase64String(reader["Image"].ToString());
                        var imageBytes = new System.IO.MemoryStream(bytes);
                        Image imgStream = Image.FromStream(imageBytes);
                        accimg.Image = imgStream;
                        accimg.SizeMode = PictureBoxSizeMode.StretchImage;
                        accimg.Location = new System.Drawing.Point(15, 25);
                        accimg.Size = new System.Drawing.Size(160, 150);
                        accimg.Name = "accimg_" + tableLayoutPanel2.RowCount;
                        accimg.TabIndex = 3;
                        //accimg.TabStop = false;
                    }
                    catch
                    {
                        accimg.Image = null;
                        accimg.SizeMode = PictureBoxSizeMode.StretchImage;
                        accimg.Location = new System.Drawing.Point(15, 25);
                        accimg.Size = new System.Drawing.Size(160, 150);
                        accimg.Name = "accimg_" + tableLayoutPanel2.RowCount;
                        accimg.TabIndex = 3;
                        //accimg.TabStop = false;
                    }
                    

                    

                    Label lb2 = new Label();
                    Label lb3 = new Label();
                    Label lb4 = new Label();
                    Label lb5 = new Label();
                    Label lb6 = new Label();
                    Label lb7 = new Label();
                    Button btn_Conf = new Button();

                    btn_Conf.Location = new System.Drawing.Point(305, 150);
                    btn_Conf.TabIndex = 3;
                    btn_Conf.Text = "SELECT";
                    btn_Conf.Click += new EventHandler(btn_Conf_Click);

                    Debug.WriteLine("load 3???");

                    lb7.AutoSize = true;
                    lb7.Location = new System.Drawing.Point(305, 130);
                    lb7.TabIndex = 3;
                    lb7.MaximumSize = new System.Drawing.Size(100, 20);

                    lb6.AutoSize = true;
                    lb6.Location = new System.Drawing.Point(180, 130);
                    lb6.TabIndex = 3;
                    lb6.MaximumSize = new System.Drawing.Size(100, 20);

                    lb5.AutoSize = true;
                    lb5.Location = new System.Drawing.Point(305, 100);
                    lb5.TabIndex = 3;
                    lb5.MaximumSize = new System.Drawing.Size(100, 20);

                    lb4.AutoSize = true;
                    lb4.Location = new System.Drawing.Point(180, 100);
                    lb4.TabIndex = 3;
                    lb4.MaximumSize = new System.Drawing.Size(100, 20);

                    lb3.AutoSize = true;
                    lb3.Location = new System.Drawing.Point(180, 53);
                    lb3.TabIndex = 2;
                    lb3.MaximumSize = new System.Drawing.Size(425, 80);

                    lb2.AutoSize = true;
                    lb2.Font = new Font(lb2.Font, FontStyle.Bold);
                    lb2.Location = new System.Drawing.Point(180, 28);
                    lb2.MaximumSize = new System.Drawing.Size(200, 15);
                    lb2.TabIndex = 1;

                    lb2.Name = "label2_" + nAcc;
                    lb3.Name = "label3_" + nAcc;
                    lb4.Name = "label4_" + nAcc;
                    lb5.Name = "label5_" + nAcc;
                    lb6.Name = "label6_" + nAcc;
                    lb7.Name = "label7_" + nAcc;
                    

                    Debug.WriteLine(btn_Conf.Name);

                    lb2.Text = reader["Name"].ToString();
                    lb3.Text = reader["Description"].ToString();
                    lb4.Font = new Font(lb4.Font, FontStyle.Bold);
                    lb4.Text = "Location:";
                    lb5.Text = reader["CC_Location"].ToString();
                    lb6.Font = new Font(lb6.Font, FontStyle.Bold);
                    lb6.Text = "Price per night:";
                    lb7.Text = reader["Price"].ToString() + " €";

                    btn_Conf.Name = lb2.Text;


                    x.Controls.Add(accimg);
                    x.Controls.Add(lb2);
                    x.Controls.Add(lb3);
                    x.Controls.Add(lb4);
                    x.Controls.Add(lb5);
                    x.Controls.Add(lb6);
                    x.Controls.Add(lb7);
                    x.Controls.Add(btn_Conf);

                    
                    tableLayoutPanel1.Controls.Add(x, 0, nAcc);
                    
                    nAcc++;
                }
            }
            cn.Close();

        }

        private void btn_Conf_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            acc_name = button.Name;
            textBox12.Text = acc_name;
            textBox9.Text = acc_name;
            textBox9.ReadOnly = true;

        }


        private void filterAcc(string option, string dest)
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
                CommandText = "TravelAgency.spFilterAcc"
            };

            cmd.Parameters.Add(new SqlParameter("@option", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@dest", SqlDbType.VarChar));
            cmd.Parameters["@option"].Value = option;
            cmd.Parameters["@dest"].Value = dest;


            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;

            
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 206));
                    accImage accimg = new accImage();
                    accimg.ID = (int)reader["ID"];
                    Panel x = new Panel();

                    tableLayoutPanel1.RowCount++;
                    x.Location = new System.Drawing.Point(25, 110);
                    x.Size = new System.Drawing.Size(1010, 205);
                    x.TabIndex = 3;

                    try
                    {
                        byte[] bytes = System.Convert.FromBase64String(reader["Image"].ToString());
                        var imageBytes = new System.IO.MemoryStream(bytes);
                        Image imgStream = Image.FromStream(imageBytes);
                        accimg.Image = imgStream;
                        accimg.SizeMode = PictureBoxSizeMode.StretchImage;
                        accimg.Location = new System.Drawing.Point(15, 25);
                        accimg.Size = new System.Drawing.Size(160, 150);
                        accimg.Name = "accimg_" + tableLayoutPanel2.RowCount;
                        accimg.TabIndex = 3;
                    }
                    catch
                    {
                        accimg.Image = null;
                        accimg.SizeMode = PictureBoxSizeMode.StretchImage;
                        accimg.Location = new System.Drawing.Point(15, 25);
                        accimg.Size = new System.Drawing.Size(160, 150);
                        accimg.Name = "accimg_" + tableLayoutPanel2.RowCount;
                        accimg.TabIndex = 3;
                    }




                    Label lb2 = new Label();
                    Label lb3 = new Label();
                    Label lb4 = new Label();
                    Label lb5 = new Label();
                    Label lb6 = new Label();
                    Label lb7 = new Label();
                    Button btn_Conf = new Button();

                    btn_Conf.Location = new System.Drawing.Point(305, 150);
                    btn_Conf.TabIndex = 3;
                    btn_Conf.Text = "SELECT";
                    btn_Conf.Click += new EventHandler(btn_Conf_Click);

                    lb7.AutoSize = true;
                    lb7.Location = new System.Drawing.Point(305, 130);
                    lb7.TabIndex = 3;
                    lb7.MaximumSize = new System.Drawing.Size(100, 20);

                    lb6.AutoSize = true;
                    lb6.Location = new System.Drawing.Point(180, 130);
                    lb6.TabIndex = 3;
                    lb6.MaximumSize = new System.Drawing.Size(100, 20);

                    lb5.AutoSize = true;
                    lb5.Location = new System.Drawing.Point(305, 100);
                    lb5.TabIndex = 3;
                    lb5.MaximumSize = new System.Drawing.Size(100, 20);

                    lb4.AutoSize = true;
                    lb4.Location = new System.Drawing.Point(180, 100);
                    lb4.TabIndex = 3;
                    lb4.MaximumSize = new System.Drawing.Size(100, 20);

                    lb3.AutoSize = true;
                    lb3.Location = new System.Drawing.Point(180, 53);
                    lb3.TabIndex = 2;
                    lb3.MaximumSize = new System.Drawing.Size(425, 80);

                    lb2.AutoSize = true;
                    lb2.Font = new Font(lb2.Font, FontStyle.Bold);
                    lb2.Location = new System.Drawing.Point(180, 28);
                    lb2.MaximumSize = new System.Drawing.Size(200, 15);
                    lb2.TabIndex = 1;

                    lb2.Name = "label2_" + nAcc;
                    lb3.Name = "label3_" + nAcc;
                    lb4.Name = "label4_" + nAcc;
                    lb5.Name = "label5_" + nAcc;
                    lb6.Name = "label6_" + nAcc;
                    lb7.Name = "label7_" + nAcc;


                    lb2.Text = reader["Name"].ToString();
                    lb3.Text = reader["Description"].ToString();
                    lb4.Font = new Font(lb4.Font, FontStyle.Bold);
                    lb4.Text = "Location:";
                    lb5.Text = reader["CC_Location"].ToString();
                    lb6.Font = new Font(lb6.Font, FontStyle.Bold);
                    lb6.Text = "Price per night:";
                    lb7.Text = reader["Price"].ToString() + " €";

                    btn_Conf.Name = lb2.Text;


                    x.Controls.Add(accimg);
                    x.Controls.Add(lb2);
                    x.Controls.Add(lb3);
                    x.Controls.Add(lb4);
                    x.Controls.Add(lb5);
                    x.Controls.Add(lb6);
                    x.Controls.Add(lb7);
                    x.Controls.Add(btn_Conf);


                    tableLayoutPanel1.Controls.Add(x, 0, nAcc);

                    nAcc++;
                }
            }
            cn.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string dest = textBox1.Text;
            string option = comboBox1.Text;
            if (string.IsNullOrEmpty(dest))
            {
                dest = "None";
            }
            
            filterAcc(option, dest);
        }

        private void btn_addNew_Click(object sender, EventArgs e)
        {
            Form4 newAcc = new Form4();
            newAcc.ShowDialog();
        }

        // PROMO TAB
        private void loadPromo()
        {
            if (!verifySGBDConnection())
                return;
            

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spLoadPromo"
            };

            cmd.Connection = cn;
            SqlDataReader reader = cmd.ExecuteReader();
            listBox2.Items.Clear();

            while (reader.Read())
            {
                listBox2.Items.Add(reader["ID"] + " | " + reader["Active"] + " | " + reader["Discount"] + "%");
            }

            cn.Close();
        }

        private void addPromo()
        {
            string active = comboBox3.Text;
            int active_bin;

            if(active == "True")
            {
                active_bin = 1;
            }
            else
            {
                active_bin = 0;
            }

            int discount = Int32.Parse(textBox13.Text);

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spAddPromo"
            };

            cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit));
            cmd.Parameters.Add(new SqlParameter("@Discount", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
            cmd.Parameters["@Active"].Value = active_bin;
            cmd.Parameters["@Discount"].Value = discount;
            cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

            if (!verifySGBDConnection())
            {
                return;
            }

            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
            {
                MessageBox.Show("Promo added");

            }
            else
            {
                MessageBox.Show("Promo already exists", "Promo Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.Close();

        }

        private void filterPromo(string option)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spFilterPromo"
            };

            cmd.Parameters.Add(new SqlParameter("@option", SqlDbType.VarChar));
            cmd.Parameters["@option"].Value = option;


            if (!verifySGBDConnection())
                return;

            cmd.Connection = cn;
            SqlDataReader reader = cmd.ExecuteReader();
            listBox2.Items.Clear();

            while (reader.Read())
            {
                listBox2.Items.Add(reader["ID"] + " | " + reader["Active"] + " | " + reader["Discount"] + "%");
            }

            cn.Close();
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {
            string option = comboBox2.Text;
            filterPromo(option);
        }

        private void btn_Enable_Click(object sender, EventArgs e)
        {
            string promo = listBox2.SelectedItem.ToString();
            string active = promo.Split('|')[0];

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spEnablePromo"
            };

            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
            cmd.Parameters["@ID"].Value = active;
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

            loadPromo();
            cn.Close();

            
        }

        private void btn_Disable(object sender, EventArgs e)
        {
            string promo = listBox2.SelectedItem.ToString();
            string active = promo.Split('|')[0];

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spDisablePromo"
            };

            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
            cmd.Parameters["@ID"].Value = active;
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

            loadPromo();
            cn.Close();
        }

        private void btn_ApplyPromo_Click(object sender, EventArgs e)
        {
            string chosenPromo = listBox2.SelectedItem.ToString();
            string active = chosenPromo.Split('|')[1];
            Debug.WriteLine(active);

            //isto ainda não funciona, calma que a esta hora sou burra
            if(active == "False")
            {
                MessageBox.Show("You cannot apply a promo if it isn't enabled", "Promo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("You have applied the promo " + chosenPromo + " to the actual package.", "Promo", MessageBoxButtons.OK);
                textBox10.Text = chosenPromo.Split('|')[0];
                textBox10.ReadOnly = true;
            }



        }

        private void btn_newPromo_Click(object sender, EventArgs e)
        {
            addPromo();
            loadPromo();
        }

        // help functions
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

        public void initialFieldsPack()
        {
            label9.Hide();
            label10.Hide();
            label11.Hide();
            label12.Hide();
            label13.Hide();
            label14.Hide();
            label18.Hide();
            textBox7.Hide();
            textBox8.Hide();
            textBox9.Hide();
            textBox10.Hide();
            textBox11.Hide();
            textBox14.Hide();

            btn_NewPack.Show();
            btn_CustPack.Show();
        }

        // PACKAGE TAB

        private void btn_NewPack_Click(object sender, EventArgs e)
        {
            btn_NewPack.Hide();
            btn_CustPack.Hide();

            label9.Show();
            label10.Show();
            label11.Show();
            label12.Show();
            label13.Show();
            label14.Show();
            label18.Show();
            textBox7.Show();
            textBox8.Show();
            textBox9.Show();
            textBox10.Show();
            textBox11.Show();
            textBox14.Show();
        }

        private void btn_cancelPack_Click(object sender, EventArgs e)
        {
            initialFieldsPack();
            btn_cancelPack.Hide();
        }

        private void btn_Historic_Click(object sender, EventArgs e)
        {
            string selCustomer = listBox1.SelectedItem.ToString();
            Form3 historic = new Form3(selCustomer);
            historic.ShowDialog();

        }

        private void flight_modify_button_Click(object sender, EventArgs e)
        {
            int flight_id = (int) dataGridView1.CurrentRow.Cells["ID"].Value;
            System.Diagnostics.Debug.WriteLine(flight_id);
            mod_flight edit_flight = new mod_flight(flight_id);
            edit_flight.ShowDialog();
            GetData();
        }

        private void flight_remove_button_Click(object sender, EventArgs e)
        {
            int flight_id = (int) dataGridView1.CurrentRow.Cells["ID"].Value;
            Debug.WriteLine(flight_id);
            DialogResult dr = MessageBox.Show("This flight will be removed", "Remove Flight", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No) return;

            if (!verifySGBDConnection())
            {
                return;
            }


            SqlCommand cmd = new SqlCommand("DELETE FROM [TravelAgency].[Flight] WHERE ID = @ID", cn);

            SqlParameter _id = new SqlParameter("@ID", SqlDbType.Int);

            cmd.Parameters.Add(_id);
            cmd.Parameters["@ID"].Value = flight_id;

            cmd.ExecuteNonQuery();

            cn.Close();

            GetData();
        }

        private void flight_search_button_Click(object sender, EventArgs e)
        {
            GetData();
            String search = flight_search_textBox.Text;
            search = search.ToLower();
            if(search == "")
            {
                return;
            }

            for(int i = dataGridView1.Rows.Count - 1; i>=0; i--)
            {
                DataGridViewRow r = dataGridView1.Rows[i];
                String s = "";
                foreach (DataGridViewCell c in r.Cells)
                {
                    s += c.Value.ToString();
                }
                s = s.ToLower();
                if (!s.Contains(search))
                {
                    dataGridView1.Rows.Remove(r);
                    
                }
                Debug.WriteLine(s);

            }
        }

        private void flight_classtype_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();

            if (flight_classtype_combobox == null)
            {
                GetData();
                return;
            }
            String class_type = flight_classtype_combobox.Text;


            try
            {
                if (!verifySGBDConnection())
                {
                    return;
                }

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter("Select * from TravelAgency.Flight WHERE classType = @ClassType", cn);

                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@ClassType", SqlDbType.NVarChar,250));
                dataAdapter.SelectCommand.Parameters["@ClassType"].Value = class_type;

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();

                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView1.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("Error");
            }

        }

        private void flight_airline_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();

            if (flight_airline_combobox == null)
            {
                GetData();
                return;
            }
            String airline = flight_airline_combobox.Text;
            airline = airline.Split(' ')[0];

            try
            {
                if (!verifySGBDConnection())
                {
                    return;
                }

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter("Select * from TravelAgency.Flight WHERE Airline = @Airline", cn);

                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Airline", SqlDbType.NVarChar,250));
                dataAdapter.SelectCommand.Parameters["@Airline"].Value = airline;

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();

                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView1.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("Error");
            }

        }

        private void loadFlights()
        {
            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd = new SqlCommand("SELECT ICAO + ' - ' + Name AS Airline FROM TravelAgency.Airline", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            flight_airline_combobox.Items.Clear();


            while (reader.Read())
            {
                flight_airline_combobox.Items.Add(reader["Airline"].ToString());
            }

            cn.Close();

            if (!verifySGBDConnection())
            {
                return;
            }


            flight_country_combobox.Items.Clear();

            cmd = new SqlCommand("SELECT City+', '+Country AS Location FROM TravelAgency.CC", cn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                flight_country_combobox.Items.Add(reader["Location"].ToString());
            }

            cn.Close();


        }

        private void flight_country_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();


            if (flight_country_combobox == null)
            {
                GetData();
                return;
            }
            String city = flight_country_combobox.Text;
            city = city.Split(',')[0];

            try
            {
                if (!verifySGBDConnection())
                {
                    return;
                }

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter("Select * from TravelAgency.Flight WHERE CC_Depart = @city or CC_Arriv = @city", cn);

                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@city", SqlDbType.NVarChar,250));
                dataAdapter.SelectCommand.Parameters["@city"].Value = city;

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();

                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView1.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("Error");
            }

        }
    }
}
