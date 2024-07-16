using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowPersons
{
    public partial class Form1 : Form
    {
        DBContext dbcontext;
        public Form1()
        {
            InitializeComponent();
            dbcontext = new DBContext();
            string query = "select * from persons";
            dataGridView1.DataSource = dbcontext.MakeQuery(query);
        }
    }
}
