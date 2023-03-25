using Sqlite_project_tutorial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sqlite_project_tutorial
{
    public partial class AddnewUser : Form
    {
        public AddnewUser()
        {
            InitializeComponent();
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

        public void DataBind() 
        {
            //string sql = "select * from employee";
            string sql = "select id, firstname, middlename, lastname, suffix, strftime('%Y-%m-%d', birthday) as birthday, civil_status, designation, designation_rank, payrollgroup_id,section_id, rate, percentage, cola, cast(idno as text) as idno from employee";
            DataAccess.ExecuteSQL(sql);
            DataTable dt = DataAccess.GetDataTable(sql);
            dataGridViewAddnewUser.DataSource = dt;


            //Eto yung nadiscover ko sa mga kulay ng datagridview
            //https://www.youtube.com/watch?v=6W7Xn8H2cqg&ab_channel=ProgrammingforEverybody
            //dataGridViewAddnewUser.EnableHeadersVisualStyles = false;
            //dataGridViewAddnewUser.ColumnHeadersDefaultCellStyle.BackColor = Color.GreenYellow;
            //foreach (var column in dataGridViewAddnewUser.Columns) 
            //{
            //}
            //dataGridViewAddnewUser.Columns[0].DefaultCellStyle.BackColor = Color.LimeGreen;
            //dataGridViewAddnewUser.Columns[1].DefaultCellStyle.BackColor = Color.LimeGreen;
            //dataGridViewAddnewUser.Columns[2].DefaultCellStyle.BackColor = Color.LimeGreen;
            //dataGridViewAddnewUser.Columns[3].DefaultCellStyle.BackColor = Color.LimeGreen;
            //dataGridViewAddnewUser.Columns[4].DefaultCellStyle.BackColor = Color.LimeGreen;
            //dataGridViewAddnewUser.Columns[5].DefaultCellStyle.BackColor = Color.LimeGreen;
            //dataGridViewAddnewUser.Columns[5].DefaultCellStyle.BackColor = Color.LimeGreen;
        }


        private bool areFieldsValid()
        {

            if (txtFirstname.Text == string.Empty || txtLastname.Text == string.Empty)
            {
                MessageBox.Show("Empty firstname or lastname is not allowed!");
                return false;
            }
            else if (txtIdno.Text == string.Empty)
            {
                MessageBox.Show("ID number must not be empty!");
                return false;
            }
            return true;
        }

        //private void createNewEmployee()
        //{
        //    DataAccess.ExecuteSQL(new StringBuilder()
        //        .Append("insert into employee (")
        //            .Append("firstname, middlename, lastname, idno, civil_status")
        //        .Append(") values (")
        //            .Append(string.Format("'{0}', '{1}', '{2}', '{3}', '{4}' ",
        //                txtFirstname.Text, txtmiddlename.Text, txtLastname.Text, txtIdno.Text, cbocivil_status.Text))
        //        .Append(")")
        //        .ToString());

        //}


        private bool createNewEmployeeParameterized()
        {
            using (var cmd = DataAccess.CreateCommand())
            {
                cmd.CommandText = new StringBuilder()
                    .Append("insert into employee (")
                    .Append("firstname, middlename,  lastname, birthday, suffix, civil_status, idno, designation")
                    .Append(") values (")
                    .Append("@firstname, @middlename, @lastname, @birthday, @civil_status, @suffix, @idno, @designation ")
                    .Append(")")
                    .ToString();
                cmd.Parameters.AddWithValue("@id", lblid.Text);
                cmd.Parameters.AddWithValue("@firstname", txtFirstname.Text);
                cmd.Parameters.AddWithValue("@middlename", txtmiddlename.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLastname.Text);
                cmd.Parameters.AddWithValue("@birthday", birthdayPicker.Value);
                cmd.Parameters.AddWithValue("@civil_status", cbocivil_stat.Text);
                cmd.Parameters.AddWithValue("@suffix", txtsuffix.Text);
                cmd.Parameters.AddWithValue("@idno", txtIdno.Text);
                cmd.Parameters.AddWithValue("@designation", txtdesig.Text);
             
                cmd.Prepare();
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    if (e.Message.Contains("idno"))
                    {
                        MessageBox.Show(string.Format(Properties.Resources.MESSAGE_DUPLICATE_IDNO, txtIdno.Text.Trim()));
                    }
                }
            }
            return false;
        }

        //private void updateEmployee()
        //{
        //    string sql = new StringBuilder()
        //        .Append("update employee ")
        //            .Append(string.Format("set firstname='{0}', ", txtFirstname.Text))
        //            .Append(string.Format("set middlename='{0}', ", txtmiddlename.Text))
        //            .Append(string.Format("lastname='{0}', ", txtLastname.Text))
        //            .Append(string.Format("suffix='{0}', ", txtsuffix.Text))
        //            .Append(string.Format("civil_status='{0}', ", cbocivil_stat.Text))
        //            .Append(string.Format("idno='{0}' ", txtIdno.Text))
        //            .Append(string.Format("designation='{0}' ", cbodesignation.Text))
        //            .Append(string.Format("where id={0}", lblid.Text))
        //        .ToString();
        //    DataAccess.ExecuteSQL(sql);
        //}


        private void updateEmployeeParameterized()
        {
            string sql = new StringBuilder()
                    .Append("update employee ")
                    .Append("set firstname=@firstname, ")
                    .Append("middlename=@middlename, ")
                    .Append("lastname=@lastname, ")
                    .Append("suffix =@suffix, ")
                    .Append("birthday =@birthday")
                    .Append("civil_status=@civil_status, ")
                    .Append("idno=@idno ")
                    .Append("designation =@designation, ")
                    .Append("where id=@id")
                    .ToString();
            using (var cmd = DataAccess.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", lblid.Text);
                cmd.Parameters.AddWithValue("@firstname", txtFirstname.Text);
                cmd.Parameters.AddWithValue("@middlename", txtmiddlename.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLastname.Text);
                cmd.Parameters.AddWithValue("@suffix", txtsuffix.Text);
                cmd.Parameters.AddWithValue("@birthday", txtbirthday.Text);
                cmd.Parameters.AddWithValue("@civil_status", cbocivil_stat.Text);
                cmd.Parameters.AddWithValue("@idno", txtIdno.Text);
                cmd.Parameters.AddWithValue("@designation", txtdesig.Text);
               
                cmd.ExecuteNonQuery();
            }
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
       
            // updateEmployee();
            updateEmployeeParameterized();
            MessageBox.Show(string.Format("Record of '{0}' has been successfully updated!", txtFirstname.Text));
            DataBind();
            //try
            //{
            //    //createNewEmployee();
            //    updateEmployee();
            //    MessageBox.Show(string.Format("Record of '{0}' has been successfully saved!", txtFirstname));
            //    DataBind();
            //}
            //catch (Exception error)
            //{
            //    // ignore for now
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddnewUser_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        //private void updateTextFieldValues()
        //{
        //    foreach (DataGridViewRow rowupdate in dataGridViewAddnewUser.SelectedRows)
        //    {
        //        lblid.Text =         rowupdate.Cells[0].Value.ToString();
        //        txtFirstname.Text =  rowupdate.Cells[1].Value.ToString();
        //        txtmiddlename.Text = rowupdate.Cells[2].Value.ToString();
        //        txtLastname.Text =   rowupdate.Cells[3].Value.ToString();
        //        txtIdno.Text =       rowupdate.Cells[4].Value.ToString();
        //        btnUpdate.Text = "Update";
        //    }
        //}

        private void updateTextFieldValues2()
        {
            if (dataGridViewAddnewUser.CurrentRow == null) return;
            var row = dataGridViewAddnewUser.CurrentRow;
            lblid.Text =         row.Cells[0].Value.ToString();
            txtFirstname.Text =  row.Cells[1].Value.ToString();
            txtmiddlename.Text = row.Cells[2].Value.ToString();
            txtLastname.Text =   row.Cells[3].Value.ToString();
            txtsuffix.Text =     row.Cells[4].Value.ToString();

            //txtbirthday.Text =   row.Cells[-estions/5366285/parse-string-to-datetime-in-c-sharp

            //txtbirthday.Text = row.Cells[-estions / 5366285 / parse - string - to - datetime -in -c - sharp];


            try
            {
                birthdayPicker.Value = DateTime.ParseExact(
                row.Cells[5].Value.ToString(),
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture);
            }
            catch (System.FormatException)
            {
                // ignore System.FormatException
                // throw;
                birthdayPicker.Value = DateTime.Now;
            }
            

            cbocivil_stat.Text = row.Cells[6].Value.ToString();
            txtdesig.Text =      row.Cells[7].Value.ToString();
            txtIdno.Text =       row.Cells[8].Value.ToString();
            


            btnUpdate.Text = "Update";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    string.Format("You are about to delete record of {0}. Do you wish to continue?", txtFirstname.Text),
                    "Yes or No",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if(result == DialogResult.Yes)
                {
                   
                    DataAccess.ExecuteSQL(new StringBuilder()
                        .Append("delete from employee where id=")
                        .Append(lblid.Text)
                        .ToString());
                    MessageBox.Show("Deleted successfully");
                    DataBind();
                }
            }
            catch 
            {
                
            }
        }


        private void dataGridViewAddnewUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            updateTextFieldValues2();
        }

        private void dataGridViewAddnewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updateTextFieldValues2();
        }

        private void dataGridViewAddnewUser_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            updateTextFieldValues2();
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            
            // updateEmployee();
            // createNewEmployee();


           
            if (createNewEmployeeParameterized())
            {
                MessageBox.Show(string.Format("Record of '{0}' has been successfully inserted!", txtFirstname.Text));
                DataBind();
            }
        }

        private bool DoesNameAlreadyExist()
        {
            using (var cmd = DataAccess.CreateCommand())
            {
                cmd.CommandText = new StringBuilder().Append("select ")
                    .Append("id, firstname, lastname ")
                    .Append("from employee ")
                    .Append("where ")
                    .Append("lower(firstname) = @firstname ")
                    .Append(" and lower(lastname) = @lastname ")
                    .ToString();
                cmd.Parameters.AddWithValue("@firstname", txtFirstname.Text.Trim().ToLower());
                cmd.Parameters.AddWithValue("@lastname", txtLastname.Text.Trim().ToLower());
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) return true;
                }
            }
                    return false;
        }

        private void dataGridViewAddnewUser_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridViewAddnewUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //foreach (DataGridViewRow r in dataGridViewAddnewUser.Rows) 
            //{
            //    bool is_Approved = Convert.ToBoolean(r.Cells[6].Value);
            //}
            //if (is_Approved == false)
            //{
            //ref.DefaultCellStyle.BackColor=Color
            //}
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtFirstname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
