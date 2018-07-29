/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  DATASET  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 
 * Collection of data tables containing data.
 * Fetch Data without Interacting with Data Source(Disconnected Data Access Method).
 * In Memory Data Store that can hold more than one table at the same time .
 * Also can be used to read and write data as XML Document.
 */

/*~~~~~~~~~~~~~~~~~~~~Presentation Layer~~~~~~~~~~~~~~~~~~~~~~~~*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//In Disconnected Table we use DataAdapter
namespace ADONET
{
    public partial class Form2 : Form
    {
        SqlConnection connection;
        DataSet dataset;
        //Dataset has a Tables Collection(Table has objects of DataTable)
        //DataTable CLass has DataRow and DataColumn
        SqlDataAdapter adapter;//Open query, send query to SQL,receive result and add in dataset
        
        /*DataAdapter acts as s bridge between DataSet and DataSource to retrieve data.It represents
         set of SQL Commands and Database connection.Used to fill dataset and update data source*/

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["cstring"].ConnectionString;
            //First add reference from References(System COnfiguration)

        }
        public void GetData()
        {
            //Adapter is used for Disconnected Components
            string query = "select * from employee";
            adapter = new SqlDataAdapter(query, connection);//Only for select
            dataset = new DataSet();//Local Copy of DB(Data in XML)
            adapter.Fill(dataset);

        }
        public void FillGrid()
        {
            GetData();
            dataGridView1.DataSource = dataset.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillGrid();

        }
    }
}
