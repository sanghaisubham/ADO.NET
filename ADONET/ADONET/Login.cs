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

namespace ADONET
{
    public partial class Form5 : Form
    {
        SqlConnection connection;//Maintains connection between DB and Application
        SqlCommand command; //Insert,update,delete
        SqlParameter password, username;//parameter for command
        public Form5()
        {
            InitializeComponent();
            connection = new SqlConnection();
            connection.ConnectionString = "server=localhost\\sqlexpress;database=UHGDB;trusted_connection=yes";
        }
        public string myusername;
        public string mypassword;

        private void button2_Click(object sender, EventArgs e)
        {
          

            command = new SqlCommand();
            command.Connection = connection;
            string query = "select * from [User] where Username=@username and Password=@password";

            username = new SqlParameter("@username", SqlDbType.VarChar);//SqlDbType is an ennumeration
            password = new SqlParameter("@password", SqlDbType.VarChar);


            myusername = Convert.ToString(textBox1.Text);
            mypassword = Convert.ToString(textBox2.Text);

            username.Value = myusername;
            password.Value = mypassword;

            command.Parameters.Add(username);
            command.Parameters.Add(password);

            command.CommandText = query;

            connection.Open();
            SqlDataReader rd = command.ExecuteReader();

            if (rd.HasRows)
                button1.Enabled = true;
            else
            {
                MessageBox.Show("Sorry , Wrong Credentials");
                button1.Enabled = false;
            }
                

          
               
            connection.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Months month = Months.March;
            //int month = 3;
            MessageBox.Show(month.ToString());
            int m = (int)Months.April;
            MessageBox.Show(m.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.ShowDialog();

        }
    }
}
