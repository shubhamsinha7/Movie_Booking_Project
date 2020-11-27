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

namespace Movie_Booking
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        SqlCommand command;
        SqlDataReader datareader;
        SqlConnection con;

        public void opendata()
        {
            string connection = "Data Source=DESKTOP-NEBRBJS\\MSSQLSERVER01;Initial Catalog=TestDB;Integrated Security=True";
            con = new SqlConnection(connection);
            con.Open();
            Console.WriteLine("connection is open");
        }
        public void searchadmine()
        {
            opendata();
            string quari = "select * from admine where Email_id=@userid and Pass_word=@password";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@userid", textBox1.Text);
            command.Parameters.AddWithValue("@password", textBox2.Text);
            datareader = command.ExecuteReader();
            employeedetail obofemployeedetail = new employeedetail();
            if (datareader.HasRows == true)
            {
                obofemployeedetail.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("user_name password worng", "worng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchadmine();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Movie_Details obofmoviedetail = new Movie_Details();
            obofmoviedetail.Show();
            this.Dispose();
        }
    }
}
