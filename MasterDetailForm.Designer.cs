namespace Sqlite_project_tutorial
{
    partial class MasterDetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdGp = new System.Windows.Forms.DataGridView();
            this.grdOut = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblChildTitle = new System.Windows.Forms.Label();
            this.btnNoMdi = new System.Windows.Forms.Button();
            this.btnAttachToParent = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdGp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOut)).BeginInit();
            this.SuspendLayout();
            // 
            // grdGp
            // 
            this.grdGp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdGp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdGp.Location = new System.Drawing.Point(12, 59);
            this.grdGp.Name = "grdGp";
            this.grdGp.Size = new System.Drawing.Size(334, 320);
            this.grdGp.TabIndex = 0;
            // 
            // grdOut
            // 
            this.grdOut.AllowUserToAddRows = false;
            this.grdOut.AllowUserToDeleteRows = false;
            this.grdOut.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdOut.Location = new System.Drawing.Point(352, 59);
            this.grdOut.Name = "grdOut";
            this.grdOut.ReadOnly = true;
            this.grdOut.Size = new System.Drawing.Size(670, 320);
            this.grdOut.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 27);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(104, 25);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Gatepass";
            // 
            // lblChildTitle
            // 
            this.lblChildTitle.AutoSize = true;
            this.lblChildTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChildTitle.Location = new System.Drawing.Point(347, 27);
            this.lblChildTitle.Name = "lblChildTitle";
            this.lblChildTitle.Size = new System.Drawing.Size(176, 25);
            this.lblChildTitle.TabIndex = 3;
            this.lblChildTitle.Text = "Gatepass Details";
            // 
            // btnNoMdi
            // 
            this.btnNoMdi.AutoSize = true;
            this.btnNoMdi.Location = new System.Drawing.Point(170, 27);
            this.btnNoMdi.Name = "btnNoMdi";
            this.btnNoMdi.Size = new System.Drawing.Size(130, 23);
            this.btnNoMdi.TabIndex = 4;
            this.btnNoMdi.Text = "Leave Parent MDI Form";
            this.btnNoMdi.UseVisualStyleBackColor = true;
            this.btnNoMdi.Click += new System.EventHandler(this.btnNoMdi_Click);
            // 
            // btnAttachToParent
            // 
            this.btnAttachToParent.AutoSize = true;
            this.btnAttachToParent.Location = new System.Drawing.Point(170, -2);
            this.btnAttachToParent.Name = "btnAttachToParent";
            this.btnAttachToParent.Size = new System.Drawing.Size(155, 23);
            this.btnAttachToParent.TabIndex = 5;
            this.btnAttachToParent.Text = "Attach to Original MDI Parent";
            this.btnAttachToParent.UseVisualStyleBackColor = true;
            this.btnAttachToParent.Click += new System.EventHandler(this.btnAttachToParent_Click);
            // 
            // MasterDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 382);
            this.Controls.Add(this.btnAttachToParent);
            this.Controls.Add(this.btnNoMdi);
            this.Controls.Add(this.lblChildTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grdOut);
            this.Controls.Add(this.grdGp);
            this.Name = "MasterDetailForm";
            this.Text = "MasterDetailForm";
            this.Load += new System.EventHandler(this.MasterDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdGp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdGp;
        private System.Windows.Forms.DataGridView grdOut;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblChildTitle;
        private System.Windows.Forms.Button btnNoMdi;
        private System.Windows.Forms.Button btnAttachToParent;
    }
}