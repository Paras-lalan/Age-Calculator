using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Birthday
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void birth()
        {
            //int D = Convert.ToInt32(DateTime.Today.Day);
            //int M = DateTime.Today.Month;
            DataTable dt = new DataTable();
            string Query = "Select * From PData WHERE (DATEPART(DAY,DOB) = @Day) AND (DATEPART(MONTH,DOB)=@Month)";
            SqlConnection sc = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\Birthday\Birthday\Database1.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(Query,sc);
            cmd.Parameters.AddWithValue("@Day", DateTime.Today.Day);
            cmd.Parameters.AddWithValue("@Month", DateTime.Today.Month);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sc.Open();
            sda.Fill(dt);
            dataGridView1.DataSource=dt;
            sc.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sc = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\Birthday\Birthday\Database1.mdf;Integrated Security=True");
                SqlCommand cmdw = new SqlCommand("Insert into PData (Name,DOB) values ('" + this.textBox1.Text + "','" + this.dateTimePicker1.Text + "')", sc);
                sc.Open();
                cmdw.ExecuteNonQuery();
                textBox1.Clear();
                MessageBox.Show("Record Saved Sucessfully");
                textBox1.Enabled = true;
                textBox1.Focus();
                birth();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           birth();
        }
    }
}
