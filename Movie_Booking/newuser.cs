using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;


namespace Movie_Booking
{
    public partial class newuser : Form
    {



        string ppattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
        string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        string imp_path=null;//to store image paths
        string otp_no;
        public newuser()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------
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
        public void getvalue()
        {
            opendata();
            string name = textBox1.Text;
            string gender = comboBox1.Text;
            string dob = dateTimePicker1.Text;
            string mobile_no = textBox3.Text;
            string email_id = textBox4.Text;
            string password = textBox5.Text;

            string inquary = "insert into user_detail(Name,DOB,Gender,Mobile_no,Email_id,passwords,image_path) values('" +name + "','" + dob + "','" + gender + "','"+mobile_no +"','"+email_id+"','"+password+"','"+imp_path+"')";
            command = new SqlCommand(inquary, con);
            command.ExecuteReader();
            command.Dispose();
            con.Close();
            MessageBox.Show("Registeration Sucessfully!!","sucessfully",MessageBoxButtons.OK,MessageBoxIcon.Information);
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Text = "";
            textBox6.Text = "";
            imp_path = null;
            textBox2.Text = "";

        }
        //---------------------------------------------------------  




        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text)==true)
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "fill it");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
        /*
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                textBox2.Focus();
                errorProvider2.SetError(this.textBox2, "fill it");
            }
            else
            {
                errorProvider2.Clear();
            }

        }*/

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem==null)
            {
                comboBox1.Focus();
                errorProvider3.SetError(this.comboBox1, "fill it");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Length!=10)
            {
                textBox3.Focus();
                errorProvider4.SetError(this.textBox3, "not valid number");
            }
            else
            {
                errorProvider4.Clear();
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox4.Text,pattern)==false)
            {
                textBox4.Focus();
                errorProvider5.SetError(this.textBox4, "ivalid");
            }
            else
            {
                errorProvider5.Clear();
                opendata();
                string q2 = "select * from User_detail where Email_id=@e_id";
                SqlCommand command2 = new SqlCommand(q2, con);
                command2.Parameters.AddWithValue("@e_id", textBox4.Text);
                datareader = command2.ExecuteReader();
                if (datareader.HasRows == true)
                {
                    textBox4.Focus();
                    errorProvider5.SetError(this.textBox4, "user id alredy exist");
                }

                else
                {
                    datareader.Close();
                    command2.Dispose();
                    con.Close();
                    getOTP();
                }
                
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox5.Text,ppattern)==false)
            {
                textBox5.Focus();
                errorProvider6.SetError(this.textBox5, "invalid");
            }
            else
            {
                errorProvider6.Clear();
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text!=textBox6.Text)
            {
                textBox6.Focus();
                errorProvider7.SetError(this.textBox6, "worng password");
            }
            else
            {
                
                errorProvider7.Clear();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(char.IsLetter(ch)==true)
            {
                e.Handled = false;
            }
            else if(ch==8)
            {
                e.Handled = false;
            }
            else if(ch==32)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            imp_path = null;
            Form1 ob = new Form1();
            this.Dispose();
            ob.Show();
        }

        private void signupbutton_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != textBox6.Text)
            {
                textBox6.Focus();
                errorProvider7.SetError(this.textBox6, "worng password");
            }
            if(textBox2.Text==null ||otp_no!=textBox2.Text)
            {
                textBox2.Focus();
                errorProvider8.SetError(this.textBox2, "wrong OTP");
            }
            else
            {
                getvalue();
                errorProvider7.Clear();
                errorProvider8.Clear();
                Form1 ob = new Form1();
                this.Dispose();
                ob.Show();

            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            webcame obj_webcam = new webcame();
            obj_webcam.Show();
            this.Hide();
            //this.Dispose();
            /*
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "IMAGE FILE(*.jpg;*.jpeg)|*.jpg;*.jpeg";
            if (op.ShowDialog() == DialogResult.OK)
            {
                string imagelocation = op.FileName;
                pictureBox2.ImageLocation = imagelocation;
                imp_path = imagelocation;
            }*/

        }

        private void button2_Click(object sender, EventArgs e)
        {
            getOTP();
        }

        private void newuser_Load(object sender, EventArgs e)
        {
           // webcame obwebcame = new webcame();
            imp_path = webcame.imp_path;
            pictureBox2.ImageLocation = imp_path;
        }
        public void getOTP()
        {
            string email_id = "movietime24x07@gmail.com";
            string firsttext = "Hi "+textBox1.Text +" your OTP is  ";
            string lasttext = " for Movie Time";
            string subject = "Verification of email id";
            Random randomno = new Random();
            otp_no = randomno.Next(1000, 9999).ToString();
            MailMessage msg = new MailMessage(email_id, textBox4.Text, subject, firsttext + otp_no + lasttext);
            msg.IsBodyHtml = true;
            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
            sc.UseDefaultCredentials = false;
            NetworkCredential cre = new NetworkCredential(email_id,"110046110046s");//your mail password
            sc.Credentials = cre;
            sc.EnableSsl = true;
            sc.Send(msg);
            MessageBox.Show("OTP is send to your email id "+textBox4.Text);
        }
    }
}
