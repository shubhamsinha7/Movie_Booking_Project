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
    public partial class Movie_Details : Form
    {
        int panalwidth;
        SqlCommand command;
        SqlDataReader datareader;
        SqlConnection con;
        /// <summary>
        /// varianle for sending data from onre form to another
        /// </summary>
        public static string locationname;
        public static string moviename;
        public static string imagename;
        public static string user_id;
        string mlanguage;
        string userid;// to store userid local commimg from login form
        //string img_path;
        string[] pathname = new string[20];
        

        public Movie_Details()
        {
            InitializeComponent();
            panalwidth = panel1.Width;
                
        }

        
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// check for user login or not
        /// </summary>
        public void check_for_user()
        {
            booking ob_of_booking = new booking();
            Form1 obofform1 = new Form1();
            string uname = label8.Text;
            if (string.IsNullOrEmpty(uname) == false)
            {
                user_id = userid;
                if (string.IsNullOrEmpty(locationname) == false)
                {
                    ob_of_booking.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("select location", "location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                obofform1.Show();
                this.Hide();
            }
        }


        public void opendata()
        {
            string connection = "Data Source=DESKTOP-NEBRBJS\\MSSQLSERVER01;Initial Catalog=TestDB;Integrated Security=True";
            con = new SqlConnection(connection);
            con.Open();
        }


        /// <summary>
        /// this function is to select name ,path from movie table
        /// </summary>
        public void readdata()
        {

            opendata();
            string quari = "select Name,paths  from Moviename";
            command = new SqlCommand(quari, con);
            int i = 0;
            PictureBox[] pb = {pictureBox2, pictureBox3,pictureBox4,pictureBox5,pictureBox6,pictureBox7,
                pictureBox8,pictureBox9,pictureBox11,pictureBox12,pictureBox13,pictureBox14,pictureBox15,
                pictureBox16,pictureBox17,pictureBox18,pictureBox19,pictureBox20,pictureBox21,pictureBox22};
            Label[] lb = {label2,label3,label4,label5,label6,label7,label9,label10,label13,label14,label15,
                          label16,label17,label18,label19,label20,label21,label22,label23,label24};
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                if (datareader["Name"] != DBNull.Value && datareader["paths"]!=DBNull.Value)
                {
                    lb[i].Text = datareader.GetValue(0).ToString();
                    pb[i].ImageLocation = datareader.GetValue(1).ToString();
                    pathname[i]= pb[i].ImageLocation;//to show image on another form;
                }
                i++;
            }
            

            datareader.Close();
            command.Dispose();
            con.Close();

        }
        /// <summary>
        /// this method is use for get state name from data base
        /// </summary>
        public void read_statename()
        {
            HashSet<string> mystate = new HashSet<string>();
            opendata();
            string quari = "select * from Cinama_hall_name";
            command = new SqlCommand(quari, con);
            datareader = command.ExecuteReader();
            while(datareader.Read())
            {
                string statename = datareader["s_tate"].ToString();
                //comboBox1.Items.Add(statename);
                mystate.Add(statename);
            }
            foreach(string i in mystate)
            {
                comboBox1.Items.Add(i);
            }

        }
        private void Movie_Details_Load(object sender, EventArgs e)
        {
            readdata();
            label8.Text =Form1.name;
            userid = Form1.username;
            pictureBox10.ImageLocation = Form1.imp_path;
            buttoncheck();
            read_statename();
            groupBox1.Hide();
            groupBox2.Hide();


        }
        /// <summary>
        /// login or profile button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            movesidepenal(button2);
            Form1 obofform1 = new Form1();
            booking ob_of_booking = new booking();
            if (button2.Text=="LOGIN")
            {
                obofform1.Show();
                this.Hide();
            }
            else
            {
                user_id = userid;
                profile obofprofil = new profile();
                obofprofil.Show();
                this.Hide();
                
            }
        }
        public void buttoncheck()
        {
            string uname = label8.Text;
            if (string.IsNullOrEmpty(uname) == false)
            {
                button2.Text = "PROFIL";
                button1.Enabled = false;//for admin button
            }
            else
            {
                button2.Text = "LOGIN"; 
            }
        }

        /*private void pictureBox9_Click(object sender, EventArgs e)
        {

        }*/
        /// <summary>
        /// this function is for moving white button
        /// </summary>
        /// <param name="p"></param>
        private void movesidepenal(Control p)
        {
            panalside.Top = p.Top;
            panalside.Height = p.Height;
        }

        private void homebutton_Click(object sender, EventArgs e)
        {
            movesidepenal(homebutton);
            readdata();
        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            imagename = pathname[6];
            moviename = label9.Text;
            locationname = comboBox1.Text;
            check_for_user();
        }

/// <summary>
/// select movies on basic of language
/// </summary>
        private void languagemvovie()
        {
            opendata();
            string quari = "select Name,paths  from Moviename where language1=@language";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@language",mlanguage);
            int i = 0;
            PictureBox[] pb = { pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9 };
            Label[] lb = { label2, label3, label4, label5, label6, label7, label9, label10 };
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                if (datareader["Name"] != DBNull.Value && datareader["paths"] != DBNull.Value)
                {
                    lb[i].Text = datareader.GetValue(0).ToString();
                    pb[i].ImageLocation = datareader.GetValue(1).ToString();
                    pathname[i] = pb[i].ImageLocation;//to show image on another form;
                }
                i++;
            }
            datareader.Close();
            command.Dispose();
            con.Close();

        }
        private void english_Click(object sender, EventArgs e)
        {
            movesidepenal(english);//this is for wight vertical line;
            mlanguage = "English";
            languagemvovie();
        }

        private void hindibutton_Click(object sender, EventArgs e)
        {
            movesidepenal(hindibutton);
            mlanguage ="Hindi";
            languagemvovie();
        }
        
        
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            imagename = pathname[0];
            moviename = label2.Text;
            locationname = comboBox1.Text;
            check_for_user();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            imagename = pathname[1];
            moviename = label3.Text;
            locationname = comboBox1.Text;
            check_for_user();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            imagename = pathname[2];
            moviename = label4.Text;
            locationname = comboBox1.Text;
            check_for_user();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            imagename = pathname[3];
            moviename = label5.Text;
            locationname = comboBox1.Text;
            check_for_user();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            imagename = pathname[4];
            moviename = label6.Text;
            locationname = comboBox1.Text;
            check_for_user();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            imagename = pathname[5];
            moviename = label7.Text;
            locationname = comboBox1.Text;
            check_for_user();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            
            imagename = pathname[7];
            moviename = label10.Text;
            locationname = comboBox1.Text;
            check_for_user();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin obofadmin = new Admin();
            obofadmin.Show();
            this.Hide();
        }
    }
            
}
