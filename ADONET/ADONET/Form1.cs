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
    public partial class Form1 : Form
    {
        SqlConnection connection;//Maintains connection between DB and Application
        SqlCommand command; //Insert,update,delete
        SqlParameter code, name, salary;//parameter for command
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //command = new SqlCommand();
            //command.Connection = connection;
            string query= "delete from  employee where Name=(@name)";
            command = new SqlCommand(query, connection);
           
            name = new SqlParameter("@name", SqlDbType.VarChar);
       


            
            name.Value = textBox2.Text;

           
            command.Parameters.Add(name);
          

            connection.Open();
            int recordsAffected=command.ExecuteNonQuery();//Insert,Update and delete
            //Returns an integer of how many rows were affected
            connection.Close();
            if (recordsAffected > 0)
                MessageBox.Show("Record Deleted!!");
            else
                MessageBox.Show("No Record Found!!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "update employee set Salary=(@salary) where Name=(@name)";

            salary = new SqlParameter("@salary", SqlDbType.Money);//SqlDbType is an ennumeration
            name = new SqlParameter("@name", SqlDbType.VarChar);



            salary.Value = textBox3.Text;
            name.Value = textBox2.Text;

            command.Parameters.Add(salary);
            command.Parameters.Add(name);


            connection.Open();
            int recordsAffected=command.ExecuteNonQuery();//Insert,Update and delete
            connection.Close();
            if (recordsAffected > 0)
                MessageBox.Show("Row Updated");
            else
                MessageBox.Show("No Match Found");
            //Returns an integer of how many rows were affected
           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select name,salary from employee where code=@code";
            command.Parameters.AddWithValue("@code", textBox1.Text);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();//Provides a way of reading a forward only stream
            if(reader.HasRows==true)
            {
                reader.Read();
                textBox2.Text = reader["name"].ToString();
                textBox3.Text = reader[1].ToString();
            }
            else
            {
                MessageBox.Show("No record Found..");

            }
            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "insert into employee values(@code,@name,@salary)";
            code = new SqlParameter("@code",SqlDbType.Int);//SqlDbType is an ennumeration
            name = new SqlParameter("@name", SqlDbType.VarChar);
            salary = new SqlParameter("@salary", SqlDbType.Money);


            code.Value = textBox1.Text;
            name.Value = textBox2.Text;
            salary.Value = textBox3.Text;

            command.Parameters.Add(code);
            command.Parameters.Add(name);
            command.Parameters.Add(salary);

            connection.Open();
            command.ExecuteNonQuery();//Insert,Update and delete
            //Returns an integer of how many rows were affected
            connection.Close();

            MessageBox.Show("Row Inserted in Database");


        }

        private void TotalSalary_Click(object sender, EventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Select sum(salary) from employee";
            connection.Open();
            object result = command.ExecuteScalar();//returns only first column of first row
            connection.Close();
            MessageBox.Show("Total Salary: " + result);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection();
            connection.ConnectionString="server=localhost\\sqlexpress;database=UHGDB;trusted_connection=yes";//Windows Authentication
            //connection.ConnectionString = "server=lovalhost\\sqlexpress;database=UHGDB;uid=sa;pwd=123";//User Authentication
            FillList();
        }
        public void  FillList()
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select name from employee";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());

            }
            connection.Close();
        }
    }
}
