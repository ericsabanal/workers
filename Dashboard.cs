using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sqlite_project_tutorial
{
    public partial class Dashboard : Form
    {
        public static Form instance;
        private AddnewUser UserManagementForm;
        private SQliteDataBind UserListForm;
        private Sqlite_project_tutorial.MasterDetailForm GatepassForm;

        public Dashboard()
        {
            InitializeComponent();
            Dashboard.instance = this;
        }
        //private void x()
        //{
        //    DisplayFormMaximized<SQliteDataBind>(UserListForm, y);
        //}

        //private SQliteDataBind y()
        //{
        //    return new SQliteDataBind();
        //}

        /*
         * ito ang mga methods na gusto ko kaninang pag-isahin na lang dito sa 
         * DisplayFormMaximized
         * 
         * showUserListForm()
         * showUserManagementForm()
         * showGatepassForm()
         */

        //private void DisplayFormMaximized<T>(Form form, Func<T> func)
        //{
        //    if (form == null || form.IsDisposed)
        //    {
        //        func();
        //    }
        //}

        private void showUserListForm()
        {
            if (UserListForm == null || UserListForm.IsDisposed)
            {
                UserListForm = new SQliteDataBind();
                UserListForm.WindowState = FormWindowState.Maximized;
                UserListForm.MdiParent = this;
                UserListForm.Show();

                _childform_maximization_workaround();
            }
            else
            {
                UserListForm.Activate();
            }
        }

        private void _childform_maximization_workaround()
        {
            // a workaround for cases where child forms does not maximize when
            // - parent form was maximized and
            // - it's alone (no sibling forms)
            var tmp = new Form();
            tmp.MdiParent = this;
            tmp.Height = 0;
            tmp.Width = 0;
            tmp.Show();
            if (!tmp.IsDisposed)
            {
                tmp.Dispose();
            }
        }

        private void showUserManagementForm()
        {
            if (UserManagementForm == null || UserManagementForm.IsDisposed)
            {
                UserManagementForm = new AddnewUser();
                UserManagementForm.WindowState = FormWindowState.Maximized;
                UserManagementForm.MdiParent = this;
                UserManagementForm.Show();
                
                _childform_maximization_workaround();
            }
            else
            {
                UserManagementForm.Activate();
            }
        }

        private void dataFromSqliteTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showUserListForm();
        }

        private void addnewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showUserManagementForm();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showGatepassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showGatepassForm();
        }

        private void showGatepassForm()
        {
            if (GatepassForm == null || GatepassForm.IsDisposed)
            {
                GatepassForm = new Sqlite_project_tutorial.MasterDetailForm();
                GatepassForm.MdiParent = this;
                GatepassForm.WindowState = FormWindowState.Maximized;
                GatepassForm.Show();
                _childform_maximization_workaround();
            }
            else
            {
                GatepassForm.Activate();
            }
        }
    }
}
