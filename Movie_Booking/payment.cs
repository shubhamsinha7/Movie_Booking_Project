using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;


namespace Movie_Booking
{
    public partial class payment : Form
    {
        public payment()
        {
            InitializeComponent();
        }
        SqlCommand command;
        SqlConnection con;
        SqlDataReader datareader;
        string userid;
        string[] no_ofseat;
        string theater_name;


        public void opendata()
        {
            string connection = "Data Source=DESKTOP-NEBRBJS\\MSSQLSERVER01;Initial Catalog=TestDB;Integrated Security=True";
            con = new SqlConnection(connection);
            con.Open();
        }


        public void insert_bookinginformation()
        {
            opendata();
            string user_id = userid; 
            string Moviename = label12.Text;
            string price = label5.Text;
            string s_date =label11.Text;
            string s_time = label10.Text;
            string address = label13.Text;
            string seat_no = label3.Text;
            string inquary = "insert into booking_information(Email_id,Movie_name,s_date,s_time,a_ddress,seat_no,price) values('" + user_id + "','" + Moviename+ "','" + s_date + "','"+s_time+"','"+address+"','"+seat_no+"','"+price+"')";
            command = new SqlCommand(inquary, con);
            command.ExecuteReader();
            command.Dispose();
            con.Close();
            MessageBox.Show("Booking succfully");
        }
        private void payment_Load(object sender, EventArgs e)
        {
            userid = Movie_Details.user_id;
            label3.Text = seat_booking.s_no;
            label5.Text = seat_booking.totalprice.ToString();
            label12.Text = seat_booking.movie_name;
            label11.Text = booking.showdate;
            label10.Text = booking.showtiming;
            label13.Text = booking.theater +"  "+booking.statename;
            theater_name= booking.theater + booking.statename;
            splitstring();
            
        }

        public void splitstring()
        {
            string s = label3.Text;
            no_ofseat = s.Split(' ');
        }

        private void button1_Click(object sender, EventArgs e)
        {
            seat_booking obofseatbooking = new seat_booking();
            obofseatbooking.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if(comboBox1.SelectedItem==null)
            {
                MessageBox.Show("select payment option");
            }
           else
            {
                updateseat();
                insert_bookinginformation();
                earning();
                send_detail_by_mail();
                Movie_Details obofmovie = new Movie_Details();
                obofmovie.Show();
                this.Dispose();
            }
            
        }
        public void updateseat()
        {
            opendata();
            string s = "B";
            int i = 1;
            while (i < no_ofseat.Length)
            {
                string q = "UPDATE  " +theater_name+"  SET  "+booking.slot+ "=@b where lable=@l";
                command = new SqlCommand(q, con);
                command.Parameters.AddWithValue("@b",s);
                command.Parameters.AddWithValue("@l",no_ofseat[i]);
                command.ExecuteNonQuery();
                i++;
            }
            command.Dispose();
            con.Close();
        }
        int cost;
        int seatno;
        string price,seat;

        private void label13_Click(object sender, EventArgs e)
        {

        }

        public void earning()
        {
            int noofseat=no_ofseat.Length-1;
            opendata();
            string q = "select  "+booking.slot+"Earning  ,"+booking.slot+"SeatBooked from  "+theater_name+"Income where d_ate=@date";
            command = new SqlCommand(q, con);
            command.Parameters.AddWithValue("@date", label11.Text);
            datareader = command.ExecuteReader();
            if (datareader.HasRows == true)
            {
                while (datareader.Read())
                {
                    price = (datareader[booking.slot+"Earning"]).ToString();
                    seat = datareader[booking.slot + "SeatBooked"].ToString();
                    if (price == null && seat==null)
                    {
                         cost= 0;
                        seatno = 0;
                    }
                    else
                    {
                        cost = Convert.ToInt32(price);
                        seatno = Convert.ToInt32(seat);
                    }
                    cost = cost+Convert.ToInt32( label5.Text);
                    seatno = seatno + noofseat;
                }
                datareader.Close();
                string q2 = "UPDATE  "+ theater_name+"Income SET  "+booking.slot+"Earning=@cost ,"+booking.slot+"SeatBooked=@cost where d_ate=@date";
                command = new SqlCommand(q2, con);
                command.Parameters.AddWithValue("@cost", cost);
                //command.Parameters.AddWithValue("@seat", seatno);
                command.Parameters.AddWithValue("@date", label11.Text);
                command.ExecuteNonQuery();

            }
            else
            {
                datareader.Close();
                string q1 = "insert into  "+ theater_name+"Income (d_ate,"+booking.slot+"Earning , "+booking.slot+"SeatBooked) values('" + label11.Text + "','" + label5.Text + "','"+noofseat+"')";
                command = new SqlCommand(q1, con);
                command.ExecuteReader();
            }
            datareader.Close();
            command.Dispose();
            con.Close();
        }
        
        public void send_detail_by_mail()
        {
            string email_id = "movietime24x07@gmail.com";
            string message="";
            message = "Movie : " + label12.Text + " \n  Show Time : " + label10.Text + " \n Show Date : " + label11.Text + " \n " +
                "Seat No :" + label13.Text + " \n Price : " + label5.Text;
            
            string subject = " Booking Detail of Movie";
            MailMessage msg = new MailMessage(email_id, userid, subject,message);
            msg.IsBodyHtml = true;
            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
            sc.UseDefaultCredentials = false;
            NetworkCredential cre = new NetworkCredential(email_id, "110046110046s");//your mail password
            sc.Credentials = cre;
            sc.EnableSsl = true;
            sc.Send(msg);
            MessageBox.Show("Booking Detail is send to a user id " + userid);
        }
        

    }
}
