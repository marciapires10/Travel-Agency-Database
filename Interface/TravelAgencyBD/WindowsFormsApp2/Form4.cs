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
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        private SqlConnection cn;

        public Form4()
        {
            InitializeComponent();
            cn = getSGBDConnection();
            load_CC();
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

        private void newAcc()
        {
            string name = textBox1.Text;
            string imagePath = textBox3.Text;
            string image = "";
            string descr = richTextBox1.Text;
            int price = Int32.Parse(textBox2.Text);
            string locationComplete = comboBox1.SelectedItem.ToString();

            string location = locationComplete.Split(',')[0];

            if (!(string.IsNullOrEmpty(imagePath)))
            {
                using (Image imageUp = Image.FromFile(imagePath))
                {
                    using(MemoryStream mem = new MemoryStream())
                    {
                        imageUp.Save(mem, imageUp.RawFormat);
                        byte[] imagBytes = mem.ToArray();

                        image = Convert.ToBase64String(imagBytes);
                    }
                }
            }

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TravelAgency.spAddAcc"
            };

            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@image", SqlDbType.VarBinary));
            cmd.Parameters.Add(new SqlParameter("@description", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@price", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@location", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@responseMsg", SqlDbType.NVarChar, 250));

            cmd.Parameters["@name"].Value = name;
            cmd.Parameters["@image"].Value = image;
            cmd.Parameters["@description"].Value = descr;
            cmd.Parameters["@price"].Value = price;
            cmd.Parameters["@location"].Value = location;
            cmd.Parameters["@responseMsg"].Direction = ParameterDirection.Output;

            if (!verifySGBDConnection())
            {
                return;
            }

            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if ("" + cmd.Parameters["@responseMsg"].Value == "Success")
            {
                MessageBox.Show("Successfully registered new accommodation");
            }
            else
            {
                MessageBox.Show("Error registering new accommodation", "Adding Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.Close();


        }

        private void btn_Image_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string imageName = openFile.FileName;
                textBox3.Text = imageName;

            }
        }

        private void load_CC()
        {
            if (!verifySGBDConnection())
            {
                return;
            }

            comboBox1.Items.Clear();
            
            SqlCommand cmd = new SqlCommand("SELECT City+', '+Country AS Location FROM TravelAgency.CC", cn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader["Location"].ToString());
            }

            cn.Close();


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newAcc();
        }
    }
}
