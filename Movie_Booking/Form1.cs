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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlCommand command;
        SqlDataReader datareader;
        SqlConnection con;

        public static string username; //static variable for send data from one form to another
        public static string imp_path;
        public static string name;
        public void opendata()
        {
            string connection = "Data Source=DESKTOP-NEBRBJS\\MSSQLSERVER01;Initial Catalog=TestDB;Integrated Security=True";
            con = new SqlConnection(connection);
            con.Open();
            Console.WriteLine("connection is open");
        }


        private void Form1_Load(object sender, EventArgs e)
        {   
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool check = checkBox1.Checked;
            switch(check)
            {
                case true:
                    textBox2.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox2.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "fill this fild!!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                textBox2.Focus();
                errorProvider2.SetError(this.textBox2, "fill this fild!!");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        Movie_Details ob_of_movie_Details = new Movie_Details();
        private void button2_Click(object sender, EventArgs e)
        {
            ob_of_movie_Details.Show();
            this.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            newuser ob_of_newuser = new newuser();
            ob_of_newuser.Show();
            this.Hide();
        }
        //------------------------------------------------
       
        public void searchdata()
        {

            opendata();
            string quari = "select * from user_detail where Email_id=@email and Passwords=@password";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@email", textBox1.Text);
            command.Parameters.AddWithValue("@password", textBox2.Text);
            datareader = command.ExecuteReader();
            
            if (datareader.HasRows == true)
            {
                while(datareader.Read())
                {
                    name = datareader["Name"].ToString();
                    imp_path = datareader["image_path"].ToString();
                }
                username = textBox1.Text;
                ob_of_movie_Details.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("user_name password worng","worng", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            textBox1.Text = "";
            textBox2.Text = "";
            datareader.Close();
            command.Dispose();
            con.Close();
        }
        //--------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            searchdata();
        }

        
    }
}
