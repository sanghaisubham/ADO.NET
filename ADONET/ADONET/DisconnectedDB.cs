using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADONET
{
    public partial class Form3 : Form
    {
        Database dbase;
        public Form3()
        {
            InitializeComponent();
            dbase = new Database();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            FillGrid();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee()
            {
                Code = Convert.ToInt32(textBox1.Text),
                Name = textBox2.Text,
                Salary = Convert.ToSingle(textBox3.Text)
        };

        bool result =dbase.InsertRecord(emp);
            if (result == true)
            {
                FillGrid();
                MessageBox.Show("Record Added");
            }
            else
                MessageBox.Show("A Record with code already exists");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int code = Convert.ToInt32(textBox1.Text);
            bool result = dbase.DeleteRecord(code);
            if (result)
            {
                FillGrid();
                MessageBox.Show("Record Deleted..");
            }
            else
                MessageBox.Show("No Match Found");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee()
            {
                Code = Convert.ToInt32(textBox1.Text),
                Name = textBox2.Text,
                Salary = Convert.ToSingle(textBox3.Text)
            };
            bool result = dbase.UpdateRecord(emp);
            if (result)
            {
                FillGrid();
                MessageBox.Show("Record Updated");
            }
            else
                MessageBox.Show("No Record Updated");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        public void FillGrid()
        {
            dbase.GetData();
            dataGridView1.DataSource = dbase.dataset.Tables[0];


        }
    }
}
