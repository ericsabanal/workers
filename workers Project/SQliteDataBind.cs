using Sqlite_project_tutorial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace sqlite_project_tutorial
{
    public partial class SQliteDataBind : Form
    {
        public SQliteDataBind()
        {
            InitializeComponent();
            grdEmployeeList.BackgroundColor = this.BackColor;
            grdEmployeeList.BorderStyle = BorderStyle.None;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SQliteDataBind_Load(object sender, EventArgs e)
        {
            string sql = "SELECT id, firstname, lastname, cast(idno as string) as idno FROM employee";
            DataTable dt = DataAccess.GetDataTable(sql);
            grdEmployeeList.DataSource = dt;
        }

        private void searchEmployee()
        {
            DataTable dt = DataAccess.GetDataTable(new StringBuilder()
                .Append("select id, firstname, lastname, ")
                    .Append("cast(idno as string) as idno ")
                .Append("from employee ")
                .Append("where ")
                    .Append(string.Format("firstname like '%{0}%' ", txtsearch.Text))
                    .Append(string.Format("or lastname like '%{0}%' ", txtsearch.Text))
                    .Append(string.Format("or idno like '%{0}%' ", txtsearch.Text))
                .ToString());
            grdEmployeeList.DataSource = dt;
        }

        private void searchEmployeeParameterized()
        {
            var cmd = DataAccess.CreateCommand();
            cmd.CommandText = new StringBuilder()
                .Append("select id, firstname, lastname, ")
                    .Append("cast(idno as string) as idno ")
                .Append("from employee ")
                .Append("where ")
                    .Append("firstname like @firstname ")
                    .Append("or lastname like @lastname ")
                    .Append("or idno like @idno ")
                .ToString();
            cmd.Parameters.AddWithValue("@firstname", string.Format("%{0}%", txtsearch.Text));
            cmd.Parameters.AddWithValue("@lastname", string.Format("%{0}%", txtsearch.Text));
            cmd.Parameters.AddWithValue("@idno", string.Format("%{0}%", txtsearch.Text));
            cmd.Prepare();
            var data = new DataSet();
            var adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(data, "employee");
            grdEmployeeList.DataSource = data.Tables["employee"];
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            // searchEmployee();
            searchEmployeeParameterized();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
