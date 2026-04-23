namespace Payroll__C__
{
    partial class Employee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Employee));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            lblName = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            txtbxDept = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtbPos = new TextBox();
            dgvEarningItems = new DataGridView();
            label7 = new Label();
            label8 = new Label();
            dgvDeductions = new DataGridView();
            pictureBox4 = new PictureBox();
            dgvAttendance = new DataGridView();
            cmbPeriods = new ComboBox();
            label9 = new Label();
            btnBack = new Button();
            dgvOvertime = new DataGridView();
            btnPreviewPayslip = new Button();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEarningItems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDeductions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOvertime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(0, 0, 192);
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1272, 120);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(0, 0, 192);
            label1.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(144, 16);
            label1.Name = "label1";
            label1.Size = new Size(191, 47);
            label1.TabIndex = 1;
            label1.Text = "Welcome, ";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.FromArgb(0, 0, 192);
            lblName.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(320, 16);
            lblName.Name = "lblName";
            lblName.Size = new Size(111, 47);
            lblName.TabIndex = 2;
            lblName.Text = "name";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(0, 0, 192);
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(8, 8);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(120, 104);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(0, 0, 192);
            label2.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(152, 64);
            label2.Name = "label2";
            label2.Size = new Size(372, 40);
            label2.TabIndex = 5;
            label2.Text = "TCP Employee Payroll Slip";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(272, 136);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 11;
            label3.Text = "Department";
            // 
            // txtbxDept
            // 
            txtbxDept.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtbxDept.Location = new Point(272, 160);
            txtbxDept.Name = "txtbxDept";
            txtbxDept.Size = new Size(336, 35);
            txtbxDept.TabIndex = 10;
            txtbxDept.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(272, 216);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 13;
            label4.Text = "Attendance";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(272, 448);
            label5.Name = "label5";
            label5.Size = new Size(56, 15);
            label5.TabIndex = 15;
            label5.Text = "Overtime";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(648, 136);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 18;
            label6.Text = "Position";
            // 
            // txtbPos
            // 
            txtbPos.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtbPos.Location = new Point(648, 160);
            txtbPos.Name = "txtbPos";
            txtbPos.Size = new Size(336, 35);
            txtbPos.TabIndex = 17;
            txtbPos.TextAlign = HorizontalAlignment.Center;
            // 
            // dgvEarningItems
            // 
            dgvEarningItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEarningItems.Location = new Point(648, 240);
            dgvEarningItems.Name = "dgvEarningItems";
            dgvEarningItems.Size = new Size(336, 200);
            dgvEarningItems.TabIndex = 20;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(648, 216);
            label7.Name = "label7";
            label7.Size = new Size(79, 15);
            label7.TabIndex = 19;
            label7.Text = "Earning Items";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(648, 448);
            label8.Name = "label8";
            label8.Size = new Size(67, 15);
            label8.TabIndex = 21;
            label8.Text = "Deductions";
            // 
            // dgvDeductions
            // 
            dgvDeductions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDeductions.Location = new Point(648, 472);
            dgvDeductions.Name = "dgvDeductions";
            dgvDeductions.Size = new Size(336, 200);
            dgvDeductions.TabIndex = 25;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.FromArgb(128, 128, 255);
            pictureBox4.Location = new Point(0, 120);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(240, 568);
            pictureBox4.TabIndex = 27;
            pictureBox4.TabStop = false;
            // 
            // dgvAttendance
            // 
            dgvAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAttendance.Location = new Point(272, 240);
            dgvAttendance.Name = "dgvAttendance";
            dgvAttendance.Size = new Size(336, 200);
            dgvAttendance.TabIndex = 28;
            // 
            // cmbPeriods
            // 
            cmbPeriods.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            cmbPeriods.FormattingEnabled = true;
            cmbPeriods.Location = new Point(8, 224);
            cmbPeriods.Name = "cmbPeriods";
            cmbPeriods.Size = new Size(224, 29);
            cmbPeriods.TabIndex = 29;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(128, 128, 255);
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.Location = new Point(8, 192);
            label9.Name = "label9";
            label9.Size = new Size(67, 21);
            label9.TabIndex = 30;
            label9.Text = "Periods";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(56, 624);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(120, 40);
            btnBack.TabIndex = 31;
            btnBack.Text = "Log Out";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // dgvOvertime
            // 
            dgvOvertime.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOvertime.Location = new Point(272, 472);
            dgvOvertime.Name = "dgvOvertime";
            dgvOvertime.Size = new Size(336, 200);
            dgvOvertime.TabIndex = 32;
            // 
            // btnPreviewPayslip
            // 
            btnPreviewPayslip.Location = new Point(1064, 376);
            btnPreviewPayslip.Name = "btnPreviewPayslip";
            btnPreviewPayslip.Size = new Size(120, 40);
            btnPreviewPayslip.TabIndex = 33;
            btnPreviewPayslip.Text = "Print";
            btnPreviewPayslip.UseVisualStyleBackColor = true;
            btnPreviewPayslip.Click += btnPreviewPayslip_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(128, 128, 255);
            pictureBox3.Location = new Point(1000, 336);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(248, 112);
            pictureBox3.TabIndex = 34;
            pictureBox3.TabStop = false;
            // 
            // Employee
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(btnPreviewPayslip);
            Controls.Add(dgvOvertime);
            Controls.Add(btnBack);
            Controls.Add(label9);
            Controls.Add(cmbPeriods);
            Controls.Add(dgvAttendance);
            Controls.Add(pictureBox4);
            Controls.Add(dgvDeductions);
            Controls.Add(label8);
            Controls.Add(dgvEarningItems);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(txtbPos);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtbxDept);
            Controls.Add(label2);
            Controls.Add(pictureBox2);
            Controls.Add(lblName);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox3);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Employee";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Employee";
            Load += Employee_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEarningItems).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDeductions).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOvertime).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private Label label1;
        private Label lblName;
        private PictureBox pictureBox2;
        private Label label2;
        private Label label3;
        private TextBox txtbxDept;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtbPos;
        private DataGridView dgvEarningItems;
        private Label label7;
        private Label label8;
        private DataGridView dgvDeductions;
        private PictureBox pictureBox4;
        private DataGridView dgvAttendance;
        private ComboBox cmbPeriods;
        private Label label9;
        private Button btnBack;
        private DataGridView dgvOvertime;
        private Button btnPreviewPayslip;
        private PictureBox pictureBox3;
    }
}