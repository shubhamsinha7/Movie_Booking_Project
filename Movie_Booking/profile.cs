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
    public partial class profile : Form
    {
        public profile()
        {
            InitializeComponent();
        }
        SqlCommand command;
        SqlDataReader datareader;
        SqlConnection con;
        string user_id;

        public void opendata()
        {
            string connection = "Data Source=DESKTOP-NEBRBJS\\MSSQLSERVER01;Initial Catalog=TestDB;Integrated Security=True";
            con = new SqlConnection(connection);
            con.Open();
        }
        Movie_Details obofmoviedetail = new Movie_Details();
        public void readinformation()
        {
            Movie_Details obofmoviedetail = new Movie_Details();
            user_id = Movie_Details.user_id;
            opendata();
            string quari = "select * from user_detail where Email_id=@email ";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@email",user_id);
            datareader = command.ExecuteReader();

            if (datareader.HasRows == true)
            {
                while (datareader.Read())
                {
                    label1.Text = datareader["Name"].ToString();
                    label2.Text = datareader["DOB"].ToString();
                    label3.Text = datareader["Gender"].ToString();
                    label4.Text = datareader["Mobile_no"].ToString();
                    label5.Text = datareader["Email_id"].ToString();
                    pictureBox1.ImageLocation = datareader["image_path"].ToString();
                    
                }
            }
            else
            {
                MessageBox.Show("user_name password worng", "worng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void profile_Load(object sender, EventArgs e)
        {
            readinformation();
            dataGridView1.Hide();
        }
        public void read_ordersinformation()
        {
            opendata();
            string q = "select Movie_name,s_date,s_time,a_ddress,seat_no from booking_information where Email_id =@id ";
            SqlCommand command = new SqlCommand(q, con);
            command.Parameters.AddWithValue("@id", user_id);

            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            con.Close();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            read_ordersinformation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            obofmoviedetail.Show();
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
