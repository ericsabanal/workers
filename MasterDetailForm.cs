using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// https://learn.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/y8c0cxey(v=vs.100)?redirectedfrom=MSDN
namespace Sqlite_project_tutorial
{
    public partial class MasterDetailForm : Form
    {

        // 100_000 is sluggish
        // private static int MAX_GP_COUNT = 100000;
        // gp.dbf of lmf materials 2012 has 17,168 records.
        private static int MAX_GP_COUNT = 100000;
        private static int MAX_DETAIL_COUNT = 10;
        private DataSet data;
        private Random random = new Random();
        private Form originalParent;

        public MasterDetailForm()
        {
            InitializeComponent();
            insertBogusData();
            linkTables();
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
        private void MasterDetailForm_Load(object sender, EventArgs e)
        {

        }


        private void createTables()
        {
            using (var cmd = DataAccess.CreateCommand())
            {
                cmd.CommandText = "drop table if exists gp";
                cmd.ExecuteNonQuery();
                cmd.CommandText = new StringBuilder()
                    .Append("create table gp (")
                        .Append("id integer primary key autoincrement, ")
                        .Append("gpno varchar(10)")
                    .Append(")")
                    .ToString();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "drop table if exists out";
                cmd.ExecuteNonQuery();
                cmd.CommandText = new StringBuilder()
                    .Append("create table out (")
                        .Append("id integer primary key autoincrement, ")
                        .Append("gp_id integer, ")
                        .Append("qty integer, ")
                        .Append("descript varchar(50)")
                    .Append(")")
                    .ToString();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "drop index if exists idx_gp_id";
                cmd.ExecuteNonQuery();
                cmd.CommandText = new StringBuilder()
                    .Append("create index idx_gp_id on out (gp_id asc)")
                    .ToString();
                cmd.ExecuteNonQuery();
            }
        }

        private void insertGpData()
        {
            using (var cmd = DataAccess.CreateCommand())
            {
                cmd.CommandText = new StringBuilder()
                    .Append("insert into gp ")
                        .Append("(gpno) values (@gpno)")
                    .ToString();
                for (var i = 0; i < MAX_GP_COUNT; i++)
                {
                    cmd.Parameters.AddWithValue("gpno", "LA2300" + random.Next(100,999));
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void insertOutData()
        {
            using (var cmd = DataAccess.CreateCommand())
            {
                cmd.CommandText = new StringBuilder()
                .Append("insert into out (")
                    .Append("gp_id, qty, descript")
                .Append(") values (")
                    .Append("@gp_id, @qty, @descript")
                .Append(")")
                .ToString();
                for (var i = 1; i <= MAX_GP_COUNT; i++)
                {
                    var detailCount = random.Next(1, MAX_DETAIL_COUNT);
                    for (var j = 0; j < detailCount; j++)
                    {
                        cmd.Parameters.AddWithValue("@gp_id", i);
                        cmd.Parameters.AddWithValue("@qty", random.Next(1,10));
                        cmd.Parameters.AddWithValue("@descript",
                            "Item #" + random.Next(1,99));
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            
        }

        private void insertBogusData() {
            // very slow if not wrapped by a transaction
            //createTables();
            //insertGpData();
            //insertOutData();
            using (var transaction = DataAccess.Connection.BeginTransaction())
            {
                createTables();
                insertGpData();
                insertOutData();
                transaction.Commit();
            }
        }

        private void linkTables()
        {
            data = new DataSet();
            data.Locale = System.Globalization.CultureInfo.InvariantCulture;
            var gpAdapter = new SQLiteDataAdapter("select * from gp", DataAccess.Connection);
            gpAdapter.Fill(data, "gp");
            var outAdapter = new SQLiteDataAdapter("select * from out", DataAccess.Connection);
            outAdapter.Fill(data, "out");

            var relation = new DataRelation("gatepass",
                data.Tables["gp"].Columns["id"],
                data.Tables["out"].Columns["gp_id"]);
            data.Relations.Add(relation);
            var gpSource = new BindingSource();
            gpSource.DataSource = data;
            gpSource.DataMember = "gp";
            var outSource = new BindingSource();
            outSource.DataSource = gpSource;
            outSource.DataMember = "gatepass";

            grdGp.DataSource = gpSource;
            grdOut.DataSource = outSource;
        }

        private void btnNoMdi_Click(object sender, EventArgs e)
        {
            this.MdiParent = null;
            this.WindowState = FormWindowState.Normal;
        }

        private void btnAttachToParent_Click(object sender, EventArgs e)
        {
            this.MdiParent = sqlite_project_tutorial.Dashboard.instance;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
