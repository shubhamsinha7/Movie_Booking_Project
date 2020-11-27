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
    public partial class employeedetail : Form
    {
        public employeedetail()
        {
            InitializeComponent();
        }
        SqlCommand command;
        SqlDataReader datareader;
        SqlConnection con;
        string lablename;

        public void opendata()
        {
            string connection = "Data Source=DESKTOP-NEBRBJS\\MSSQLSERVER01;Initial Catalog=TestDB;Integrated Security=True";
            con = new SqlConnection(connection);
            con.Open();
            Console.WriteLine("connection is open");
        }
/// <summary>
/// this function is for insert movie detail in data base
/// </summary>
        public void getvaluemovie()
        {
            opendata();
            string imagepath = textBox2.Text;
            string Moviename =textBox1.Text ;
             string language = textBox3.Text;

            string inquary = "insert into MovieName(Name,language1,paths) values('"+Moviename+"','"+language+"','"+imagepath+"')";
            command = new SqlCommand(inquary, con);
            command.ExecuteReader();
            con.Close();
            MessageBox.Show("insert succfully");
            textBox3.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            pictureBox1.ImageLocation = "";

        }
        /// <summary>
        /// select image from folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "IMAGE FILE(*.jpg;*.jpeg)|*.jpg;*.jpeg";
            if (op.ShowDialog() == DialogResult.OK)
            {
                string imagelocation = op.FileName;
                pictureBox1.ImageLocation = imagelocation;
                textBox2.Text = imagelocation;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text)==false&&string.IsNullOrEmpty(textBox3.Text)==false)
            {
                getvaluemovie();//insert detail of movie in table
            }
            else
            {
                MessageBox.Show("fill name and language");
            }
            
        }
        /// <summary>
        /// this function is for inserting theater detail in table
        /// </summary>
        public void getvaluecinamahall()
        {
            opendata();
            string name = textBox4.Text; 
            string address = textBox5.Text;
            string state = textBox7.Text;

            string inquary = "insert into Cinama_hall_name(Name,A_ddress,S_tate) values('" + name + "','" +address + "','" + state + "')";
            command = new SqlCommand(inquary, con);
            command.ExecuteReader();
            con.Close();
            MessageBox.Show("Registeration Sucessfully!!", "sucessfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox4.Text = "";
            textBox5.Text = "";
            //comboBox1.Text = "";
            textBox7.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox4.Text)==false &&string.IsNullOrEmpty(textBox5.Text)==false&&string.IsNullOrEmpty(textBox7.Text)==false)
            {
                getvaluecinamahall();
            }
            else
            {
                MessageBox.Show("fill all details");
            }
           
        }
        public void update_price()
        {
            opendata();
            string quari = "update " + listBox1.SelectedItem + comboBox2.Text + "seat_price set price=@p_rice  where lable=@l_able";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@l_able", listBox2.SelectedItem);
            command.Parameters.AddWithValue("@p_rice", textBox6.Text);
            datareader = command.ExecuteReader();
            datareader.Close();
            command.Dispose();
            con.Close();
            MessageBox.Show("update succesfully");
            textBox6.Text = "";

        }
        private void employeedetail_Load(object sender, EventArgs e)
        {
            read_statename();
            resetbutton();//this is for enable and disable of reset button;
            
        }
        public void selectedindexreaddata()
        {

            opendata();
            string quari = "select Name from Cinama_hall_name where S_tate=@s";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@s", comboBox2.Text);
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                string theatername = datareader["Name"].ToString();
                listBox1.Items.Add(theatername);
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
                string statename = datareader["S_tate"].ToString();
                mystate.Add(statename);
                
            }
            foreach(string i in mystate)
            {
                comboBox2.Items.Add(i);
                comboBox3.Items.Add(i);
            }
            datareader.Close();
            command.Dispose();
            con.Close();

        }
        //combobox2 is for state name
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            selectedindexreaddata();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            opendata();
            string quari1 = "select lable from " + listBox1.SelectedItem + comboBox2.Text + "seat_price";
            command = new SqlCommand(quari1, con);
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                lablename = datareader["lable"].ToString();
                listBox2.Items.Add(lablename);
            }
            datareader.Close();
            command.Dispose();
            con.Close();

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            opendata();
            string quari1 = "select price from " + listBox1.SelectedItem + comboBox2.Text + "seat_price  where lable= @l_able";
            command = new SqlCommand(quari1, con);
            command.Parameters.AddWithValue("@l_able", listBox2.SelectedItem);
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                textBox6.Text = datareader["price"].ToString();
            }
            datareader.Close();
            command.Dispose();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox2.Text)==false && string.IsNullOrEmpty(listBox2.Text)==false)
            {
                update_price();
            }
            else
            {
                MessageBox.Show("select lable and fill price of seat");
            }
            
        }
        /// <summary>
        /// this function is for get movie name from movie table
        /// </summary>
        public void read_moviename()
        {
            opendata();
            string quari = "select Name from Moviename";
            command = new SqlCommand(quari, con);
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                string statename = datareader["Name"].ToString();
                listBox3.Items.Add(statename);
            }
            datareader.Close();
            command.Dispose();
            con.Close();
        }
        public void deletemovie()
        {
            opendata();
            string quari = "delete from Moviename where Name=@name";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@name",listBox3.SelectedItem);
            datareader = command.ExecuteReader();
            MessageBox.Show("successfuly");
            listBox3.Items.Clear();
            read_moviename();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(listBox3.Text)==false)
            {
                deletemovie();
            }
            else
            {
                MessageBox.Show(" first select movie");
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Movie_Details obofmoviedetail = new Movie_Details();
            obofmoviedetail.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            read_moviename();
        }
        public void resetseatstatus()
        {
            opendata();
            string s = "A";
            string quari = "update " + listBox1.SelectedItem + comboBox2.Text + "  set s_tatus1=@s ,s_tatus2=@s";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@s",s);
            datareader = command.ExecuteReader();
            datareader.Close();
            command.Dispose();
            con.Close();
            MessageBox.Show("Reset succesfully");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public void resetbutton()
        {
            DateTime dt = DateTime.Now;
            int t = Convert.ToInt32(dt.ToString("HH"));
            if (t > 17)
            {
                button8.Enabled = true;
            }
            else
            {
                button8.Enabled = false;
            }
        }
        //reseat button 
        private void button8_Click(object sender, EventArgs e)
        {
            string l1 = listBox1.Text;
            if(string.IsNullOrEmpty(comboBox2.Text)==false && string.IsNullOrEmpty(l1)==false)
            {
                resetseatstatus();
            }
            else
            {
                MessageBox.Show("first select state and theater");
            }
            
        }
        public void selectedindexreaddata3()
        {

            opendata();
            string quari = "select Name from Cinama_hall_name where S_tate=@s";
            command = new SqlCommand(quari, con);
            command.Parameters.AddWithValue("@s", comboBox3.Text);
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                string theatername = datareader["Name"].ToString();
                listBox4.Items.Add(theatername);
            }
            datareader.Close();
            command.Dispose();
            con.Close();
        }
        public void read_Incomeinformation()
        {
            opendata();
            string q = "select *" +
                "from "+listBox4.SelectedItem+ comboBox3.Text + "Income";
            SqlCommand command = new SqlCommand(q, con);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            con.Close();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
            selectedindexreaddata3();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBox3.Text)==false&&string.IsNullOrEmpty(listBox4.Text)==false)
            {
                dataGridView1.ClearSelection();
                read_Incomeinformation();
            }
            else
            {
                MessageBox.Show("select state and theater");
            }
        }
    }
}
