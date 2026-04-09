namespace Payroll__C__
{
    partial class adminPayrollForm
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
            label3 = new Label();
            label1 = new Label();
            cmbDept = new ComboBox();
            cmbPos = new ComboBox();
            cmbName = new ComboBox();
            label2 = new Label();
            cmPeriods = new ComboBox();
            label4 = new Label();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(208, 8);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 13;
            label3.Text = "Position";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 8);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 15;
            label1.Text = "Department";
            // 
            // cmbDept
            // 
            cmbDept.Font = new Font("Segoe UI", 12F);
            cmbDept.FormattingEnabled = true;
            cmbDept.Location = new Point(8, 32);
            cmbDept.Name = "cmbDept";
            cmbDept.Size = new Size(184, 29);
            cmbDept.TabIndex = 16;
            // 
            // cmbPos
            // 
            cmbPos.Font = new Font("Segoe UI", 12F);
            cmbPos.FormattingEnabled = true;
            cmbPos.Location = new Point(208, 32);
            cmbPos.Name = "cmbPos";
            cmbPos.Size = new Size(184, 29);
            cmbPos.TabIndex = 17;
            // 
            // cmbName
            // 
            cmbName.Font = new Font("Segoe UI", 12F);
            cmbName.FormattingEnabled = true;
            cmbName.Location = new Point(408, 32);
            cmbName.Name = "cmbName";
            cmbName.Size = new Size(184, 29);
            cmbName.TabIndex = 19;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(408, 8);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 18;
            label2.Text = "Name";
            // 
            // cmPeriods
            // 
            cmPeriods.Font = new Font("Segoe UI", 12F);
            cmPeriods.FormattingEnabled = true;
            cmPeriods.Location = new Point(792, 32);
            cmPeriods.Name = "cmPeriods";
            cmPeriods.Size = new Size(184, 29);
            cmPeriods.TabIndex = 21;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(792, 8);
            label4.Name = "label4";
            label4.Size = new Size(46, 15);
            label4.TabIndex = 20;
            label4.Text = "Periods";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(24, 120);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(288, 112);
            dataGridView1.TabIndex = 22;
            // 
            // adminPayrollForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(992, 521);
            Controls.Add(dataGridView1);
            Controls.Add(cmPeriods);
            Controls.Add(label4);
            Controls.Add(cmbName);
            Controls.Add(label2);
            Controls.Add(cmbPos);
            Controls.Add(cmbDept);
            Controls.Add(label1);
            Controls.Add(label3);
            Name = "adminPayrollForm";
            Text = "adminPayrollForm";
            Load += adminPayrollForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Label label1;
        private ComboBox cmbDept;
        private ComboBox cmbPos;
        private ComboBox cmbName;
        private Label label2;
        private ComboBox cmPeriods;
        private Label label4;
        private DataGridView dataGridView1;
    }
}