using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;

namespace Movie_Booking
{
    public partial class seat_booking : Form
    {
        
        public seat_booking()
        {
            InitializeComponent();
        }
        SqlCommand command;
        SqlDataReader datareader;
        SqlConnection con;
        public static string s_no;
        public static string movie_name;
        string tablename;
        

        public void opendata()
        {
            string connection = "Data Source=DESKTOP-NEBRBJS\\MSSQLSERVER01;Initial Catalog=TestDB;Integrated Security=True";
            con = new SqlConnection(connection);
            con.Open();
            Console.WriteLine("connection is open");
        }

        public void seatreaddata()
        {
            opendata();
            string quari = "select "+booking.slot+ " from "+tablename;
            command = new SqlCommand(quari, con);
            Button[] bob = { button3, button4,button5,button6,button7,button8,button9,button10,button11,button12,
            button13,button14,button15,button16,button17,button18,button19,button20,button21,
            button22,button23,button24,button25,button26,button27,button28,button29,button30,button31,
            button32};
            datareader = command.ExecuteReader();
            int j = 0;
            while (datareader.Read())
            {
                if (datareader[booking.slot] != DBNull.Value)
                {
                    bob[j].Text = datareader.GetValue(0).ToString();

                    if (bob[j].Text == "B")
                    {
                        bob[j].BackColor = Color.Red;
                        bob[j].Enabled = false;
                    }
                    else
                    {
                        bob[j].BackColor = Color.Gray;
                       
                    }
                }
                j++;
            }
            datareader.Close();
            command.Dispose();
            con.Close();
        }
        public void seat_price()
        {
            opendata();
            string quari = "select price from "+tablename+"seat_price";
            command = new SqlCommand(quari, con);
            Label[] obofpricelabel = { label20, label22,label26, label24 ,label28};
            datareader = command.ExecuteReader();
            int j = 0;
            while (datareader.Read())
            {
                if (datareader["price"] != DBNull.Value)
                {
                    obofpricelabel[j].Text = datareader.GetValue(0).ToString();
                }
                j++;
            }
            datareader.Close();
            command.Dispose();
            con.Close();
        }
        private void seat_booking_Load(object sender, EventArgs e)
        {
            Movie_Details ob_of_moviedetail = new Movie_Details();
            label2.Text = Movie_Details.moviename;
            movie_name = label2.Text;
            label1.Text = booking.theater + "  " + booking.statename;
            tablename = booking.theater + booking.statename;
            label1.Text=booking.theater+"  "+ booking.statename;
            tablename = booking.theater + booking.statename;
            label4.Text = "Date :" + booking.showdate;
            label3.Text = "Show Timing :" + booking.showtiming;
            seatreaddata();
            seat_price();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            booking obofbooking = new booking();
            obofbooking.Show();
            this.Hide();
        }
        int c1 = 0;
        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            c1++;
            button3.BackColor = Color.Green;
            if(c1==2)
            {
                button3.BackColor = Color.Gray;
                c1 = 0;
            }
        }
        int c2 = 0;
        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            c2++;
            button4.BackColor = Color.Green;
            if (c2 == 2)
            {
                button4.BackColor = Color.Gray;
                c2 = 0;
            }
        }
        int c3 = 0;
        private void button5_MouseClick(object sender, MouseEventArgs e)
        {
            c3++;
            button5.BackColor = Color.Green;
            if (c3 == 2)
            {
                button5.BackColor = Color.Gray;
                c3 = 0;
            }
        }
        int c4 = 0;
        private void button6_MouseClick(object sender, MouseEventArgs e)
        {
            c4++;
            button6.BackColor = Color.Green;
            if (c4 == 2)
            {
                button6.BackColor = Color.Gray;
                c4 = 0;
            }
        }
        private void button7_MouseEnter(object sender, EventArgs e)
        {
        }
        int c6 = 0;
        private void button8_MouseClick(object sender, MouseEventArgs e)
        {
            c6++;
            button8.BackColor = Color.Green;
            if (c6 == 2)
            {
                button8.BackColor = Color.Gray;
                c6 = 0;
            }
        }
        int c7 = 0;
        private void button9_MouseClick(object sender, MouseEventArgs e)
        {
            c7++;
            button9.BackColor = Color.Green;
            if (c7 == 2)
            {
                button9.BackColor = Color.Gray;
                c7 = 0;
            }
        }
        private void button10_MouseEnter(object sender, EventArgs e)
        {
            
        }
        int c9 = 0;
        private void button11_MouseClick(object sender, MouseEventArgs e)
        {
            c9++;
            button11.BackColor = Color.Green;
            if (c9 == 2)
            {
                button11.BackColor = Color.Gray;
                c9 = 0;
            }
        }
        int c10 = 0;
        private void button12_MouseClick(object sender, MouseEventArgs e)
        {
            c10++;
            button12.BackColor = Color.Green;
            if (c10 == 2)
            {
                button12.BackColor = Color.Gray;
                c10 = 0;
            }
        }
        int c11 = 0;
        private void button13_MouseClick(object sender, MouseEventArgs e)
        {
            c11++;
            button13.BackColor = Color.Green;
            if (c11 == 2)
            {
                button13.BackColor = Color.Gray;
                c11 = 0;
            }
        }
        int c12 = 0;
        private void button14_MouseClick(object sender, MouseEventArgs e)
        {
            c12++;
            button14.BackColor = Color.Green;
            if (c12 == 2)
            {
                button14.BackColor = Color.Gray;
                c12 = 0;
            }
        }
        int c13 = 0;
        private void button15_Click(object sender, EventArgs e)
        {
            c13++;
            button15.BackColor = Color.Green;


            if (c13 == 2)
            {
                button15.BackColor = Color.Gray;
                c13 = 0;
            }

        }
        int c14 = 0;
        private void button16_Click(object sender, EventArgs e)
        {
            c14++;
            button16.BackColor = Color.Green;


            if (c14 == 2)
            {
                button16.BackColor = Color.Gray;
                c14 = 0;
            }
        }
        int c15 = 0;
        private void button17_Click(object sender, EventArgs e)
        {
            c15++;
            button17.BackColor = Color.Green;


            if (c15 == 2)
            {
                button17.BackColor = Color.Gray;
                c15 = 0;
            }
        }
        int c16 = 0;
        private void button18_Click(object sender, EventArgs e)
        {
            c16++;
            button18.BackColor = Color.Green;


            if (c16 == 2)
            {
                button18.BackColor = Color.Gray;
                c16 = 0;
            }
        }
        int c17 = 0;
        private void button19_Click(object sender, EventArgs e)
        {
            c17++;
            button19.BackColor = Color.Green;
            if (c17 == 2)
            {
                button19.BackColor = Color.Gray;
                c17 = 0;
            }
        }
        int c20 = 0;
        private void button20_Click(object sender, EventArgs e)
        {
            c20++;
            button20.BackColor = Color.Green;
            if (c20 == 2)
            {
                button20.BackColor = Color.Gray;
                c20 = 0;
            }
        }
        int c21 = 0;
        private void button21_Click(object sender, EventArgs e)
        {
            c21++;
            button21.BackColor = Color.Green;
            if (c21 == 2)
            {
                button21.BackColor = Color.Gray;
                c21 = 0;
            }
        }
        int c22 = 0;
        private void button22_Click(object sender, EventArgs e)
        {
            c22++;
            button22.BackColor = Color.Green;


            if (c22 == 2)
            {
                button22.BackColor = Color.Gray;
                c22 = 0;
            }
        }
        int c23 = 0;
        private void button23_Click(object sender, EventArgs e)
        {
            c23++;
            button23.BackColor = Color.Green;
            if (c23 == 2)
            {
                button23.BackColor = Color.Gray;
                c23 = 0;
            }
        }
        int c24 = 0;
        private void button24_Click(object sender, EventArgs e)
        {
            c24++;
            button24.BackColor = Color.Green;
            if (c24 == 2)
            {
                button24.BackColor = Color.Gray;
                c24 = 0;
            }
        }
        int c25 = 0;
        private void button25_Click(object sender, EventArgs e)
        {
            c25++;
            button25.BackColor = Color.Green;


            if (c25==2)
            {
                button25.BackColor = Color.Gray;
                c25= 0;
            }
        }
        int c26 = 0;
        private void button26_Click(object sender, EventArgs e)
        {
            c26++;
            button3.BackColor = Color.Green;


            if (c26== 2)
            {
                button3.BackColor = Color.Gray;
                c26 = 0;
            }
        }
        int c27 = 0;
        private void button27_Click(object sender, EventArgs e)
        {
            c27++;
            button27.BackColor = Color.Green;


            if (c27== 2)
            {
                button27.BackColor = Color.Gray;
                c27= 0;
            }
        }
        int c28 = 0;
        private void button28_Click(object sender, EventArgs e)
        {
            c28++;
            button28.BackColor = Color.Green;
            if (c28== 2)
            {
                button28.BackColor = Color.Gray;
                c28= 0;
            }
        }
        int c29 = 0;
        private void button29_Click(object sender, EventArgs e)
        {
            c29++;
            button29.BackColor = Color.Green;


            if (c29== 2)
            {
                button29.BackColor = Color.Gray;
                c29= 0;
            }
        }
        int c30 = 0;
        private void button30_Click(object sender, EventArgs e)
        {
            c30++;
            button30.BackColor = Color.Green;


            if (c30== 2)
            {
                button30.BackColor = Color.Gray;
                c30= 0;
            }
        }
        int c31 = 0;
        private void button31_Click(object sender, EventArgs e)
        {
            c31++;
            button31.BackColor = Color.Green;
            if (c31 == 2)
            {
                button31.BackColor = Color.Gray;
                c31 = 0;
            }
        }
        int c32 = 0;
        private void button32_Click(object sender, EventArgs e)
        {
            c32++;
            button32.BackColor = Color.Green;
            if (c32 == 2)
            {
                button3.BackColor = Color.Gray;
                c32 = 0;
            }
        }
        int c5 = 0;
        private void button7_Click(object sender, EventArgs e)
        {
            c5++;
            button7.BackColor = Color.Green;
            if (c5 == 2)
            {
                button7.BackColor = Color.Gray;
                c5 = 0;
            }
        }
        int c8 = 0;
        private void button10_Click(object sender, EventArgs e)
        {
            c8++;
            button10.BackColor = Color.Green;
            if (c8 == 2)
            {
                button10.BackColor = Color.Gray;
                c8 = 0;
            }
        }


        ArrayList seatno = new ArrayList();
        int price1 = 0;
        int price2 = 0;
        int price3 = 0;
        public static double totalprice;

        private void conditionchack()
        {
            if (c1 == 1)
            {
                price1 = price1 + int.Parse(label20.Text);
                seatno.Add("H1");
            }
            if (c2 == 1)
            {
                price1 = price1 + int.Parse(label20.Text);
                seatno.Add("H2");
            }
            if (c3 == 1)
            {
                price1 = price1 + int.Parse(label20.Text);
                seatno.Add("H3");
            }
            if (c4 == 1)
            {
                price1 = price1 + int.Parse(label20.Text);
                seatno.Add("H4");
            }
            if (c5 == 1)
            {
                price1 = price1 + int.Parse(label20.Text);
                seatno.Add("H5");
            }
            if (c6 == 1)
            {
                price1 = price1 + int.Parse(label20.Text);
                seatno.Add("H6");
            }
            if (c7 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("I1");
            }
            if (c8 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("I2");
            }
            if (c9 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("I3");
            }
            if (c10 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("I4");
            }
            if (c11 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("I5");
            }
            if (c12 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("I6");
            }
            if (c13 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("J1");
            }
            if (c14 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("J2");
            }
            if (c15 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("J3");
            }
            if (c16 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("J4");
            }
            if (c17 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("J5");
            }
            if (c20 == 1)
            {
                price2 = price2 + int.Parse(label22.Text);
                seatno.Add("J6");
            }
            if (c21 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("K1");
            }
            if (c22 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("K2");
            }
            if (c23 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("K 3");
            }
            if (c24 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("K4");
            }
            if (c25 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("K5");
            }
            if (c26 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("K6");
            }
            if (c27 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("L1");
            }
            if (c28 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("L 2");
            }
            if (c29 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("L3");
            }
            if (c30 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("L4");
            }
            if (c31 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("L5");
            }
            if (c32 == 1)
            {
                price3 = price3 + int.Parse(label22.Text);
                seatno.Add("L6");
            }
            int tcost = price1 + price2 + price3;
            totalprice = tcost + tcost * .18;
            string a=null ;
            foreach(var i in seatno)
            {
                a =a+" "+i;//concatinate two string from list;
            }
            s_no = a;//sending seat no to next form;

        }
        private void button33_Click(object sender, EventArgs e)
        {
            conditionchack();
            payment obofpayment = new payment();
            obofpayment.Show();
            this.Hide();
            
           
            
        }
    }
}
