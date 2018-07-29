/*~~~~~~~~~~~~~~~~~~~~~~~~Data Link Layer~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~   DATATABLE   ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 
 * DataTable represents relational data into tabular form.
 * Provides DataTable CLass to create and use data Table Independently.
 * Initially we dont have any table schema,
 * We can create table schema by adding columns and constraints to the table .
 * After defining table schema we cam add rows to the table
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
/*Adapter does not automatically create command for insert,delete and update like it does for select command*/
namespace ADONET
{
    class Database
    {
        SqlConnection connection;
        public SqlDataAdapter adapter;
        public DataSet dataset;
        SqlCommandBuilder builder;
        public Database()
        {
            connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["cstring"].ConnectionString;
        }
        public void GetData()
        {
            string query = "select * from employee";
            dataset = new DataSet();
            adapter = new SqlDataAdapter(query, connection);
            builder = new SqlCommandBuilder(adapter);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;//Adding Constraints
            adapter.Fill(dataset,"employee");
        }
        //Dataset has collection of tables
        public DataRow FindRow(int code)
        {
            GetData();
            DataRow row = null;
            row=dataset.Tables["employee"].Rows.Find(code);
            return row;
        }
        public bool InsertRecord(Employee employee)
        {
            DataTable table;
            
            DataRow row = FindRow(employee.Code);
            if (row == null)
            {
                table = dataset.Tables["employee"];//declaring schema
                row = table.NewRow();
                row[0] = employee.Code;
                row[1] = employee.Name;
                row[2] = employee.Salary;

                table.Rows.Add(row);
                adapter.Update(dataset, "employee");//if we want to change main database
                return true;
            }
            else
               return false;
        }
        public bool DeleteRecord(int code)
        {
            DataRow row = FindRow(code);
            if (row == null)
                return false;
            else
            {
                row.Delete();
                adapter.Update(dataset, "employee");
                return true;
            }
        }
        public bool UpdateRecord(Employee employee)
        {
            DataRow row = FindRow(employee.Code);
            if (row == null)
                return false;
            else
            {
                row[1] = employee.Name;
                row[2] = employee.Salary;
                adapter.Update(dataset, "employee");
                return true;
            }

        }

    }
    class Employee
    {
        public int Code { get;set; }
        public string Name { get; set; }
        public float Salary { get; set; }

    }
}
