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
    public partial class booking : Form
    {
        SqlCommand command;
        SqlDataReader datareader;
        SqlConnection con;
        string locname;//for location or state
        public static string theater;
        // for sending name of cinama hall from one form to another 
        public static string statename;
        public static string showdate;
        public static string showtiming;
        public static string slot;
        seat_booking obofseatbooking = new seat_booking();
        public booking()
        {
            InitializeComponent();
        }
        Movie_Details obmovie = new Movie_Details();

        public void opendata()
        {
            string connection = "Data Source=DESKTOP-NEBRBJS\\MSSQLSERVER01;Initial Catalog=TestDB;Integrated Security=True";
            con = new SqlConnection(connection);
            con.Open();
            Console.WriteLine("connection is open");
        }
        /// <summary>
        /// tot in use right now
        /// </summary>
        public void readdata()//read theater name from data base
        {
            opendata();
            string quari = "select Name from Cinama_hall_name where S_tate=@s";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@s",locname);

            int i = 0;
            Label[] lb = { label2, label3,label4,label5 };
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                if (datareader["Name"] != DBNull.Value)
                {
                    lb[i].Text = datareader.GetValue(0).ToString();
                }
                i++;
            }
            datareader.Close();
            command.Dispose();
            con.Close();
        }
        public void read_statename()
        {
            HashSet<string> mystate = new HashSet<string>();
            opendata();
            string quari = "select * from Cinama_hall_name";
            command = new SqlCommand(quari, con);
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                string statename = datareader["s_tate"].ToString();
                //comboBox1.Items.Add(statename);
                mystate.Add(statename);
            }
            foreach (string i in mystate)
            {
                comboBox1.Items.Add(i);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            obmovie.Show();
            this.Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            obmovie.Show();
            this.Hide();
        }

        private void booking_Load(object sender, EventArgs e)
        {
            locname = Movie_Details.locationname;
            pictureBox1.ImageLocation = Movie_Details.imagename;
            comboBox1.Text = locname;
            readdata();
            timedesider();
            showdate = label6.Text;
            read_statename();
        }

        private void timedesider()
        {
            int i = 0;
            DateTime dt = DateTime.Now;
            label6.Text = dt.ToShortDateString();
            Button[] oofb = { button3, button4, button5, button6, button7, button8, button9, button10 };
            int t1 = Convert.ToInt32(dt.ToString("HH"));
            
            while (i<8)
            {
                int t2;
                if (i%2==0)
                {
                     t2 = Convert.ToInt32(oofb[i].Text.Replace(":00 AM", ""));
                }
                else
                {
                     t2 = Convert.ToInt32(oofb[i].Text.Replace(":00 PM", ""));
                    t2 = 17;
                }
                if (t2<=t1)
                {
                    oofb[i].BackColor = Color.Gray;
                    oofb[i].Enabled = false;
 
                }
                else
                {
                    oofb[i].BackColor = Color.Green;
                
                }
                i++;   
            }
        }
        
        /// <summary>
        /// This code for selecting theater from data base
        /// whene we select state from combbox
        /// </summary>
        public void selectedindexreaddata()
        {

            opendata();
            string quari = "select Name from Cinama_hall_name where S_tate=@s";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@s", comboBox1.Text);

            int i = 0;
            Label[] lb = { label2, label3,label4,label5 };
            datareader = command.ExecuteReader();

            while (datareader.Read())
            {
                if (datareader["Name"] != DBNull.Value)
                {
                    lb[i].Text = datareader.GetValue(0).ToString();
                }
                i++;
            }

            datareader.Close();
            command.Dispose();
            con.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedindexreaddata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            theater = label2.Text;
            showtiming = button3.Text;
            statename = comboBox1.Text;
            slot = "s_tatus1";
            obofseatbooking.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            theater = label2.Text;
            showtiming = button4.Text;
            statename = comboBox1.Text;
            slot = "s_tatus2";
            obofseatbooking.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            theater = label3.Text;
            showtiming = button5.Text;
            statename = comboBox1.Text;
            slot = "s_tatus1";
            obofseatbooking.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            theater = label3.Text;
            showtiming = button6.Text;
            statename = comboBox1.Text;
            slot = "s_tatus2";
            obofseatbooking.Show();
            this.Hide();
        }
        public void movinglable(int speed)
        {
            if (label7.Left >= 400)
            {
                label7.Left = 0;
            }
            else
            {
                label7.Left += speed;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            int t = Convert.ToInt32(dt.ToString("HH"));
            if (t >=17)
            {
                timer1.Enabled = true;
                movinglable(4);
            }
            else
            {
                timer1.Enabled = false;
                label7.Dispose();
            }
               
        }
    }
}
