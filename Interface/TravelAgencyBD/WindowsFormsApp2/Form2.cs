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
        private bool add = false;
        private bool edit = false;
        private bool remove = false;
        private int size = 12;
        private int noPage = 1;
        private string curr_agent = "";
        private int packID = 0;
        private int custID = 0;
        private int bookID = 0;
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private BindingSource bindingSource2 = new BindingSource();

        public Form2(string agent_mail)
        {
            curr_agent = agent_mail;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();

            loadProfile();

            dataGridView1.DataSource = bindingSource1;
            dataGridView2.DataSource = bindingSource2;
            

            textBox12.ReadOnly = true;

            comboBox5.Items.Clear();
            comboBox5.Items.Add("Yes");
            comboBox5.Items.Add("No");
            comboBox5.SelectedItem = "No";

            
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

        // ----------------------- CUSTOMER TAB -------------------------------

        // load Customers
        private void loadCustomers(int page)
        {
            int nCustomer = 0;

            if (page == 1)
            {
                btn_NextC.Enabled = true;
            }

            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spLoadCustomer"
            };

            cmd.Parameters.Add(new SqlParameter("@size", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@noPage", SqlDbType.Int));
            cmd.Parameters["@size"].Value = size;
            cmd.Parameters["@noPage"].Value = page;

            cmd.Connection = cn;
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

                nCustomer++;
            }

            if (nCustomer < size)
            {
                btn_BackC.Enabled = false;
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
            if (!verifySGBDConnection())
            {
                return;
            }


        }

        private void filterCustomer(int page, string fname, string lname)
        {
            int nCustomer = 0;

            if (page == 1)
            {
                btn_NextC.Enabled = true;
            }

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spFilterCustomer"
            };

            cmd.Parameters.Add(new SqlParameter("@Fname", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Lname", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@size", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@noPage", SqlDbType.Int));
            cmd.Parameters["@size"].Value = size;
            cmd.Parameters["@noPage"].Value = page;
            cmd.Parameters["@fname"].Value = fname;
            cmd.Parameters["@lname"].Value = lname;

            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;

            listBox1.Items.Clear();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Customer p = new Customer();
                    p.Email = reader["Email"].ToString();
                    p.Fname = reader["Fname"].ToString();
                    p.Lname = reader["Lname"].ToString();
                    p.PhoneNo = reader["phoneNo"].ToString();
                    p.NIF = reader["NIF"].ToString();
                    listBox1.Items.Add(p);

                    nCustomer++;
                }
            }

            if (nCustomer < size)
            {
                btn_BackC.Enabled = false;
            }

            cn.Close();
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
            btn_Historic.Visible = false;
            btn_OK.Visible = true;
            btn_Cancel.Visible = true;
            

            add = true;
        }

        // want to edit a Customer
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            unlockControls();
            btn_OK.Visible = true;
            btn_Cancel.Visible = true;
            btn_Add.Visible = false;
            btn_Edit.Visible = false;
            btn_Historic.Visible = false;
            btn_Remove.Visible = false;
            edit = true;
            
        }

        // want to delete a Customer
        private void btn_Remove_Click(object sender, EventArgs e)
        {
            btn_OK.Visible = true;
            btn_Cancel.Visible = true;
            btn_Add.Visible = false;
            btn_Edit.Visible = false;
            btn_Remove.Visible = false;
            btn_Historic.Visible = false;
            clearFields();
            remove = true;

        }

        // confirm
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (add)
            {
                addCustomer();
                add = false;
            }
            else if (edit)
            {
                editCustomer();
                edit = false;
            }
            else if (remove)
            {
                removeCustomer();
                remove = false;
            }
            loadCustomers(noPage);
        }

        // cancel
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            showButtonsCustomer();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string name = textSearch.Text;
            string fname = name.Split(' ')[0];
            string lname = name.Split(' ')[1];

            if (string.IsNullOrEmpty(fname))
            {
               fname = "None";
            }

            filterCustomer(noPage, fname, lname);
        }


        private void btn_NextC_Click(object sender, EventArgs e)
        {
            noPage++;
            string cust = textSearch.Text;
            string fname = cust.Split(' ')[0];
            string lname = "";

            if (string.IsNullOrEmpty(fname))
            {
                fname = "None";
            }

            filterCustomer(noPage, fname, lname);

            if (noPage > 1)
            {
                btn_BackC.Enabled = true;
            }
        }


        private void btn_BackC_Click(object sender, EventArgs e)
        {
            noPage--;
            string cust = textSearch.Text;
            string fname = cust.Split(' ')[0];
            string lname = "";

            if (string.IsNullOrEmpty(fname))
            {
                fname = "None";
            }

            filterCustomer(noPage, fname, lname);

            if (noPage == 1)
            {
                btn_BackC.Enabled = false;
            }
        }


        private int getCustID(string email)
        {
            if (!verifySGBDConnection())
            {
                return 0;
            }

            SqlCommand cmd;
            cmd = new SqlCommand("Select TravelAgency.GetCustID('" + email + "')", cn);

            int id = (int)cmd.ExecuteScalar();

            return id;
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
            get_flight_data();
        }

        private void get_flight_data()
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



        // --------------------------- ACCOMMODATION TAB ------------------------------

        private void loadAccommodation(int page)
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

            cmd.Parameters.Add(new SqlParameter("@size", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@noPage", SqlDbType.Int));
            cmd.Parameters["@size"].Value = size;
            cmd.Parameters["@noPage"].Value = page;

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
                    lb5.Text = reader["City"].ToString() + ", " + reader["Country"].ToString();
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

            if (nAcc < size)
            {
                btn_Next.Enabled = false;
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
            MessageBox.Show(acc_name + " is now on your package!");

        }


        private void filterAcc(int page, string option, string dest)
        {
            int nAcc = 0;

            if (page == 1)
            {
                btn_Next.Enabled = true;
            }

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
            cmd.Parameters.Add(new SqlParameter("@size", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@noPage", SqlDbType.Int));
            cmd.Parameters["@size"].Value = size;
            cmd.Parameters["@noPage"].Value = page;
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

            if (nAcc < size)
            {
                btn_Back.Enabled = false;
            }

            cn.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.btn_Back.Enabled = false;
            string dest = textBox1.Text;
            string option = comboBox1.Text;

            if (string.IsNullOrEmpty(dest))
            {
                dest = "None";
            }


            filterAcc(noPage, option, dest);
        }

        private void btn_addNew_Click(object sender, EventArgs e)
        {
            Form4 newAcc = new Form4();
            newAcc.ShowDialog();
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            noPage++;
            string dest = textBox1.Text;
            string opt = comboBox1.Text;

            if (string.IsNullOrEmpty(dest))
            {
                dest = "None";
            }

            filterAcc(noPage, opt, dest);

            if (noPage > 1)
            {
                btn_Back.Enabled = true;
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            noPage--;

            string dest = textBox1.Text;
            string opt = comboBox1.Text;

            if (string.IsNullOrEmpty(dest))
            {
                dest = "None";
            }

            filterAcc(noPage, opt, dest);

            if (noPage == 1)
            {
                btn_Back.Enabled = false;
            }
        }

        // ----------------------------- PROMO TAB -----------------------------------
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

            listView1.Items.Clear();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    string ID = reader["ID"].ToString();
                    string active = reader["Active"].ToString();
                    string discount = reader["Discount"].ToString();
                    var row = new string[] { ID, active, discount };
                    var list = new ListViewItem(row);
                    listView1.View = View.Details;
                    listView1.Items.Add(list);
                }
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

            listView1.Items.Clear();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    string ID = reader["ID"].ToString();
                    string active = reader["Active"].ToString();
                    string discount = reader["Discount"].ToString();
                    var row = new string[] { ID, active, discount };
                    var list = new ListViewItem(row);
                    listView1.View = View.Details;
                    listView1.Items.Add(list);
                }
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

            string active = listView1.SelectedItems[0].SubItems[1].Text;
            Debug.WriteLine(active);

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
            string active = listView1.SelectedItems[0].SubItems[1].Text;
            Debug.WriteLine(active);

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
            string chosenPromo = listView1.SelectedItems[0].SubItems[0].Text;
            string active = listView1.SelectedItems[0].SubItems[1].Text;
            string discount = listView1.SelectedItems[0].SubItems[2].Text;
            Debug.WriteLine(active);

            if(active == "False")
            {
                MessageBox.Show("You cannot apply a promo if it isn't enabled", "Promo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("You have applied the promo " + chosenPromo + " to the actual package.", "Promo", MessageBoxButtons.OK);
                textBox10.Text = chosenPromo + " - Discount: " + discount + "%";
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

        // --------------------------------- PACKAGE TAB ------------------------------

        private int getAccID(string acc)
        {
            if (!verifySGBDConnection())
            {
                return 0;
            }

            SqlCommand cmd;
            cmd = new SqlCommand("Select TravelAgency.GetAccID('" + acc + "')", cn);

            int id = (int)cmd.ExecuteScalar();
            return id;
        }


        private void createPackage()
        {
            
            string title = textBox15.Text;
            string descr = richTextBox1.Text;
            int duration = Int32.Parse(textBox19.Text);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            DateTime startDate = dateTimePicker1.Value;
            dateTimePicker2.Format = DateTimePickerFormat.Short;
            DateTime endDate = dateTimePicker2.Value;
            int noPersons = Int32.Parse(textBox8.Text);
            //int totalPrice = Int32.Parse(textBox11.Text);
            int accID = getAccID(textBox9.Text);
            string promoComplete = textBox10.Text;
            int promoID = Int32.Parse(promoComplete.Split('-')[0]);
            string flight1 = textBox16.Text;
            int flightID1 = Int32.Parse(flight1.Split('|')[0]);
            string flight2 = textBox17.Text;
            int flightID2 = Int32.Parse(flight2.Split('|')[0]);
            string opt = comboBox5.SelectedItem.ToString();
            string transfer = textBox18.Text;
            int transfID = Int32.Parse(transfer.Split('|')[0]);


            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spAddPackage"
            };


            cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Duration", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@startDate", SqlDbType.Date));
            cmd.Parameters.Add(new SqlParameter("@endDate", SqlDbType.Date));
            cmd.Parameters.Add(new SqlParameter("@noPersons", SqlDbType.Int));
            //cmd.Parameters.Add(new SqlParameter("@totalPrice", SqlDbType.SmallMoney));
            cmd.Parameters.Add(new SqlParameter("@Acomm_ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@Promo_ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@Flight_ID1", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@Flight_ID2", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@Transf_ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@opt1", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
            cmd.Parameters.Add(new SqlParameter("@totalPrice", SqlDbType.SmallMoney, 250));


            cmd.Parameters["@Title"].Value = title;
            cmd.Parameters["@Description"].Value = descr;
            cmd.Parameters["@Duration"].Value = duration;
            cmd.Parameters["@startDate"].Value = startDate;
            cmd.Parameters["@endDate"].Value = endDate;
            cmd.Parameters["@noPersons"].Value = noPersons;
            //cmd.Parameters["@totalPrice"].Value = totalPrice;
            cmd.Parameters["@Acomm_ID"].Value = accID;
            cmd.Parameters["@Promo_ID"].Value = promoID;
            cmd.Parameters["@Flight_ID1"].Value = flightID1;
            cmd.Parameters["@Flight_ID2"].Value = flightID2;
            cmd.Parameters["@Transf_ID"].Value = transfID;
            cmd.Parameters["@opt1"].Value = opt;
            cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;
            cmd.Parameters["@totalPrice"].Direction = ParameterDirection.Output;


            if (!verifySGBDConnection())
            {
                return;
            }

            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
            {
                MessageBox.Show("You have createad a package with the final price of: " + cmd.Parameters["@totalPrice"].Value + "€");
            }
            else
            {
                MessageBox.Show("Error");
            }


            cn.Close();
        }

        private void btn_crtPack_Click(object sender, EventArgs e)
        {
            createPackage();
        }

        private int getPackID(string title)
        {
            if (!verifySGBDConnection())
            {
                return 0;
            }

            SqlCommand cmd;
            cmd = new SqlCommand("Select TravelAgency.GetPackID('" + title + "')", cn);

            int id = (int)cmd.ExecuteScalar();
            return id;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            textBox12.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox10.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox16.Text = "";
            textBox26.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox17.Text = "";
            textBox27.Text = "";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox18.Text = "";
            textBox7.Text = "";
        }

        // ----------------------------- FLIGHT TAB ---------------------------------
        private void flight_modify_button_Click(object sender, EventArgs e)
        {
            int flight_id = (int) dataGridView1.CurrentRow.Cells["ID"].Value;
            System.Diagnostics.Debug.WriteLine(flight_id);
            mod_flight edit_flight = new mod_flight(flight_id);
            edit_flight.ShowDialog();
            get_flight_data();
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

            get_flight_data();
        }

        private void flight_search_button_Click(object sender, EventArgs e)
        {
            get_flight_data();
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
            get_flight_data();

            if (flight_classtype_combobox == null)
            {
                get_flight_data();
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
            get_flight_data();

            if (flight_airline_combobox == null)
            {
                get_flight_data();
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
            get_flight_data();


            if (flight_country_combobox == null)
            {
                get_flight_data();
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

        private void btn_FlightsSel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You have added the selected departure flight to your package!");
            string id_flight1 = Convert.ToString(dataGridView1.CurrentRow.Cells["ID"].Value) + " | " + Convert.ToString(dataGridView1.CurrentRow.Cells["CC_Depart"].Value) + " - " + Convert.ToString(dataGridView1.CurrentRow.Cells["CC_Arriv"].Value);
            textBox26.Text = id_flight1;
            textBox16.Text = id_flight1;
        }

        private void btn_Flight2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You have added the selected arrival flight to your package!");
            string id_flight2 = Convert.ToString(dataGridView1.CurrentRow.Cells["ID"].Value) + " | " + Convert.ToString(dataGridView1.CurrentRow.Cells["CC_Depart"].Value) + " - " + Convert.ToString(dataGridView1.CurrentRow.Cells["CC_Arriv"].Value);
            textBox27.Text = id_flight2;
            textBox17.Text = id_flight2;
        }

        // ------------------------ TRANSFER TAB -------------------------------

        private void transfer_add_button_Click(object sender, EventArgs e)
        {
            add_transfer add_new_transfer = new add_transfer();
            add_new_transfer.ShowDialog();
            get_transfer_data();
            load_transfer();

        }

        private void transfer_edit_button_Click(object sender, EventArgs e)
        {
            int transfer_id = (int) dataGridView2.CurrentRow.Cells["ID"].Value;
            edit_transfer transfer_edit = new edit_transfer(transfer_id);
            transfer_edit.ShowDialog();
            load_transfer();
            get_transfer_data();
        }

        private void transfer_remove_button_Click(object sender, EventArgs e)
        {
            int transfer_id = (int) dataGridView2.CurrentRow.Cells["ID"].Value;
            Debug.WriteLine(transfer_id);
            DialogResult dr = MessageBox.Show("This Transfer will be removed", "Remove Transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No) return;

            if (!verifySGBDConnection())
            {
                return;
            }


            SqlCommand cmd = new SqlCommand("DELETE FROM [TravelAgency].[Transfer] WHERE ID = @ID", cn);

            SqlParameter _id = new SqlParameter("@ID", SqlDbType.Int);

            cmd.Parameters.Add(_id);
            cmd.Parameters["@ID"].Value = transfer_id;

            cmd.ExecuteNonQuery();

            cn.Close();

            load_transfer();
            get_transfer_data();

        }

        private void transfer_search_button_Click(object sender, EventArgs e)
        {
            get_transfer_data();
            String search = transfer_search_textbox.Text;
            search = search.ToLower();
            if(search == "")
            {
                return;
            }

            for(int i = dataGridView2.Rows.Count - 1; i>=0; i--)
            {
                DataGridViewRow r = dataGridView2.Rows[i];
                String s = "";
                foreach (DataGridViewCell c in r.Cells)
                {
                    s += c.Value.ToString();
                }
                s = s.ToLower();
                if (!s.Contains(search))
                {
                    dataGridView2.Rows.Remove(r);
                    
                }
                Debug.WriteLine(s);

            }


        }

        private void transfer_company_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_transfer_data();

            if (transfer_company_combobox == null)
            {
                return;
            }
            String company = transfer_company_combobox.Text;

            try
            {
                if (!verifySGBDConnection())
                {
                    return;
                }

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter("Select * from TravelAgency.Transfer WHERE Company = @company", cn);

                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@company", SqlDbType.NVarChar,250));
                dataAdapter.SelectCommand.Parameters["@company"].Value = company;

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();

                dataAdapter.Fill(table);
                bindingSource2.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView2.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("Error");
            }

        }

        private void transfer_city_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_transfer_data();


            if (transfer_city_combobox == null)
            {
                return;
            }
            String city = transfer_city_combobox.Text;
            city = city.Split(',')[0];

            try
            {
                if (!verifySGBDConnection())
                {
                    return;
                }

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter("Select * from TravelAgency.Transfer WHERE CC_Depart = @city or CC_Arriv = @city", cn);

                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@city", SqlDbType.NVarChar,250));
                dataAdapter.SelectCommand.Parameters["@city"].Value = city;

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();

                dataAdapter.Fill(table);
                bindingSource2.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView1.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("Error");
            }

        }

        private void load_transfer()
        {
            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd = new SqlCommand("SELECT Company FROM TravelAgency.Transfer", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            transfer_company_combobox.Items.Clear();


            while (reader.Read())
            {
                transfer_company_combobox.Items.Add(reader["Company"].ToString());
            }

            cn.Close();

            if (!verifySGBDConnection())
            {
                return;
            }


            transfer_city_combobox.Items.Clear();

            cmd = new SqlCommand("SELECT City+', '+Country AS Location FROM TravelAgency.CC", cn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                transfer_city_combobox.Items.Add(reader["Location"].ToString());
            }

            cn.Close();


        }

        private void get_transfer_data()
        {
            try
            {
                if (!verifySGBDConnection())
                {
                    return;
                }

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter("Select * from TravelAgency.Transfer", cn);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();

                dataAdapter.Fill(table);
                bindingSource2.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView2.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("Error");
            }
        }

        private void btn_SelectTransfer_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedItem.Equals("Yes"))
            {
                MessageBox.Show("You have added the selected transfer to your package!");
                string id_transf_selected = Convert.ToString(dataGridView2.CurrentRow.Cells["ID"].Value) + " | " + Convert.ToString(dataGridView2.CurrentRow.Cells["CC_Depart"].Value) + " - " + Convert.ToString(dataGridView2.CurrentRow.Cells["CC_Arriv"].Value);
                textBox7.Text = id_transf_selected;
                textBox18.Text = id_transf_selected;
                textBox7.ReadOnly = true;
                textBox18.ReadOnly = true;
            }

            else
            {
                MessageBox.Show("You have to change Transfer option to Yes");
            }
            
        }


        // ---------------------------- PROFILE TAB ------------------------------------
       
        private void loadProfile()
        {
            textBox23.Text = curr_agent;

            if (!verifySGBDConnection())
            {
                return;
            }

            SqlCommand cmd;
            cmd = new SqlCommand("select * from TravelAgency.AgentProf ('" + curr_agent + "')", cn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    textBox20.Text = reader["AgID"].ToString();
                    textBox21.Text = reader["Fname"].ToString();
                    textBox22.Text = reader["Lname"].ToString();
                    textBox25.Text = reader["PhoneNo"].ToString();

                }
            }

            fillChart();

            cn.Close();
        }
        
        private void btn_EditProfile_Click(object sender, EventArgs e)
        {
            string fname = textBox21.Text;
            string lname = textBox22.Text;
            string email = textBox23.Text;
            string password = textBox24.Text;
            string phoneNo = textBox25.Text;

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spEditAgent"
            };


            cmd.Parameters.Add(new SqlParameter("@Fname", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Lname", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@PhoneNo", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
            cmd.Parameters["@Fname"].Value = fname;
            cmd.Parameters["@Lname"].Value = lname;
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@Password"].Value = password;
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

        private void fillChart()
        {
            chart1.Series["Series1"].Points.AddXY("January", "1000");
            chart1.Series["Series1"].Points.AddXY("February", "1000");
            chart1.Series["Series1"].Points.AddXY("March", "1000");
            chart1.Series["Series1"].Points.AddXY("April", "1000");
            chart1.Series["Series1"].Points.AddXY("May", "1000");
            chart1.Series["Series1"].Points.AddXY("June", "1000");
            chart1.Series["Series1"].Points.AddXY("July", "1000");
            chart1.Series["Series1"].Points.AddXY("August", "1000");
            chart1.Series["Series1"].Points.AddXY("September", "1000");
            chart1.Series["Series1"].Points.AddXY("October", "1000");
            chart1.Series["Series1"].Points.AddXY("November", "1000");
            chart1.Series["Series1"].Points.AddXY("December", "1000");
            chart1.Titles.Add("Alguma coisa");
        }


        // -------------------------------- LOGOUT -----------------------------------
        private void btn_logout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Close();
        }


        // ----------------------------- BOOKING TAB ---------------------------------
        
        private void loadPackages()
        {
            if (!verifySGBDConnection())
                return;


            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spLoadPackage"
            };

            cmd.Connection = cn;
            SqlDataReader reader = cmd.ExecuteReader();
            listBox3.Items.Clear();

            while (reader.Read())
            {
                listBox3.Items.Add(reader["ID"] + " - " + reader["Title"]);
            }


            cn.Close();
        }

        private void btn_showDetails_Click(object sender, EventArgs e)
        {
            textBox31.Show();
            textBox35.Show();
            textBox14.Show();
            dateTimePicker4.Show();
            dateTimePicker3.Show();
            textBox34.Show();
            textBox30.Show();
            textBox29.Show();
            textBox28.Show();
            textBox33.Show();
            textBox32.Show();
            richTextBox2.Show();
            label40.Show();
            label45.Show();
            label42.Show();
            label38.Show();
            label37.Show();
            label44.Show();
            label36.Show();
            label35.Show();
            label34.Show();
            label43.Show();
            label39.Show();
            label41.Show();

            button10.Visible = true;
            panel1.Visible = false;

            if (!verifySGBDConnection())
                return;


            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spLoadPackage"
            };

            cmd.Connection = cn;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                /*textBox31.Text = reader["Title"].ToString();
                richTextBox2.Text = reader["Description"].ToString();
                textBox35.Text = reader["noPersons"].ToString();
                textBox14.Text = reader["Duration"].ToString();
                dateTimePicker4.Value = reader["startDate"].ToString();
                dateTimePicker3.Value = reader["endDate"].ToString();
                textBox34.Text = reader["Acomm_ID"].ToString();
                textBox30.Text = reader[""];
                textBox29.Text = reader[""];
                textBox28.Text = reader[""];
                textBox33.Text = reader["Promo_ID"].ToString();
                textBox32.Text = reader["totalPrice"].ToString();*/
            }

            cn.Close();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox31.Hide();
            textBox35.Hide();
            textBox14.Hide();
            dateTimePicker4.Hide();
            dateTimePicker3.Hide();
            textBox34.Hide();
            textBox30.Hide();
            textBox29.Hide();
            textBox28.Hide();
            textBox33.Hide();
            textBox32.Hide();
            richTextBox2.Hide();
            label40.Hide();
            label45.Hide();
            label42.Hide();
            label38.Hide();
            label37.Hide();
            label44.Hide();
            label36.Hide();
            label35.Hide();
            label34.Hide();
            label43.Hide();
            label39.Hide();
            label41.Hide();

            button10.Visible = false;
            panel1.Visible = true;
        }

        private void loadBookings()
        {
            if (!verifySGBDConnection())
                return;


            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spLoadBookings"
            };

            cmd.Connection = cn;
            SqlDataReader reader = cmd.ExecuteReader();
            listBox2.Items.Clear();

            while (reader.Read())
            {
                listBox2.Items.Add(reader["ID"] + " - " + reader["bookDate"]);
            }


            cn.Close();
        }

        private void filterBooking(string option)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spFilterBookings"
            };

            cmd.Parameters.Add(new SqlParameter("@option", SqlDbType.VarChar));
            cmd.Parameters["@option"].Value = option;


            if (!verifySGBDConnection())
                return;

            cmd.Connection = cn;

            listBox2.Items.Clear();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    listBox2.Items.Add(reader["ID"] + " - " + reader["bookDate"]);
                }
            }

            cn.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string option = comboBox4.Text;
            filterBooking(option);
        }


        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if(tabControl1.SelectedTab == tabPage2)
            {
                loadCustomers(noPage);
            }

            if (tabControl1.SelectedTab == tabPage4)
            {
                loadPackages();
                loadBookings();


                comboBox4.Items.Clear();
                comboBox4.Items.Add("Yes");
                comboBox4.Items.Add("No");
                comboBox4.Items.Add("None");
                comboBox4.SelectedItem = "None";

                textBox31.Hide();
                textBox35.Hide();
                textBox14.Hide();
                dateTimePicker4.Hide();
                dateTimePicker3.Hide();
                textBox34.Hide();
                textBox30.Hide();
                textBox29.Hide();
                textBox28.Hide();
                textBox33.Hide();
                textBox32.Hide();
                richTextBox2.Hide();
                label40.Hide();
                label45.Hide();
                label42.Hide();
                label38.Hide();
                label37.Hide();
                label44.Hide();
                label36.Hide();
                label35.Hide();
                label34.Hide();
                label43.Hide();
                label39.Hide();
                label41.Hide();
            }
        }

        private void btn_Book_Click(object sender, EventArgs e)
        {
            string pack = listBox3.SelectedItem.ToString();
            packID = Int32.Parse(pack.Split(' ')[0]);
            int agID = Int32.Parse(textBox20.Text);
            
            if(textBox4.Text == "")
            {
                MessageBox.Show("You have to assign a customer to book a package!");
            }
            else
            {
                custID = getCustID(textBox4.Text);
                Form6 book = new Form6(packID, agID, custID);
                book.ShowDialog();
            }
            

            
        }

        private void btn_Search_Click_1(object sender, EventArgs e)
        {
            custID = getCustID(textBox4.Text);
            Form3 listBooks = new Form3(custID);
            listBooks.ShowDialog();
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            if(noPage == 1)
            {
                btn_Back.Enabled = false;
                btn_BackC.Enabled = false;
            }


            if (tabControl2.SelectedTab == tabPage5)
            {
                noPage = 1;
                textBox1.Text = "";
                comboBox1.Items.Clear();
                comboBox1.Items.Add("None");
                comboBox1.Items.Add("PriceAsc");
                comboBox1.Items.Add("PriceDesc");
                comboBox1.SelectedItem = "None";

                loadAccommodation(noPage);
            }

            if (tabControl2.SelectedTab == tabPage6)
            {
                get_flight_data();
                loadFlights();
            }

            if (tabControl2.SelectedTab == tabPage7)
            {
                get_transfer_data();
                load_transfer();
            }

            if (tabControl2.SelectedTab == tabPage8)
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("None");
                comboBox2.Items.Add("DiscountAsc");
                comboBox2.Items.Add("DiscountDesc");
                comboBox2.Items.Add("Active");
                comboBox2.Items.Add("Not available");
                comboBox2.SelectedItem = "None";

                comboBox3.Items.Clear();
                comboBox3.Items.Add("True");
                comboBox3.Items.Add("False");

                loadPromo();
            }

            if (tabControl2.SelectedTab == tabPage9)
            {
                textBox9.ReadOnly = true;
                textBox10.ReadOnly = true;
                textBox16.ReadOnly = true;
                textBox17.ReadOnly = true;
                textBox18.ReadOnly = true;
            }
        }

        private void btn_addReview_Click(object sender, EventArgs e)
        {
            string pack = listBox3.SelectedItem.ToString();
            packID = Int32.Parse(pack.Split(' ')[0]);

            if (textBox4.Text == "")
            {
                MessageBox.Show("You have to assign a customer to review a package!");
            }
            else
            {
                custID = getCustID(textBox4.Text);
                Form7 review = new Form7(packID, custID);
                review.ShowDialog();
            }

            

        }

        private void btn_showReviews_Click(object sender, EventArgs e)
        {
            string pack = listBox3.SelectedItem.ToString();
            packID = Int32.Parse(pack.Split(' ')[0]);

            Form8 reviewHistoric = new Form8(packID);
            reviewHistoric.ShowDialog();

            cn.Close();
        }

        private void btn_EditBook_Click(object sender, EventArgs e)
        {
            string ID = listBox2.SelectedItem.ToString();

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spEnablePromo"
            };

            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));
            cmd.Parameters["@ID"].Value = ID;
            cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

            if (!verifySGBDConnection())
            {
                return;
            }

            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
            {
                MessageBox.Show("Book paid");
            }
            else
            {
                MessageBox.Show("Error");
            }

            loadBookings();
            cn.Close();
        }
    }
}
