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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        Database dbase;
        private void button1_Click(object sender, EventArgs e)
        {
            dbase = new Database();
            DataTable table,newTable;
            dbase.GetData();
            table = dbase.dataset.Tables[0];//employee table

            newTable = new DataTable("manager");
            newTable.Columns.Add("Id",typeof(int)); // Method 1

            DataColumn name = new DataColumn("Name", typeof(string));//Method 2
            newTable.Columns.Add(name);

            newTable.Columns.Add("Salary", typeof(float));

            DataRow newrow;
            foreach(DataRow empRow in table.Rows)
            {
                newrow = newTable.NewRow();//new Row is created of type of Table(sees structure of the Table)
                newrow[0] = empRow[0];
                newrow[1] = empRow[1];
                newrow[2] = empRow[2];

                newTable.Rows.Add(newrow);

            }
            dataGridView1.DataSource = newTable;



        }

        private void button2_Click(object sender, EventArgs e)
        {
            //In webService we exchange data using XML or JSON
            dbase = new Database();
            dbase.GetData();
            dbase.dataset.WriteXml("C:\\Users\\Subham\\source\\repos\\ADONET\\emp.xml");
            dbase.dataset.WriteXmlSchema("C:\\Users\\Subham\\source\\repos\\ADONET\\empSchema.xsd");


        }
    }
}
