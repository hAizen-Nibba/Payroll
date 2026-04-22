namespace Payroll__C__
{
    partial class adminMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminMain));
            pictureBox4 = new PictureBox();
            label2 = new Label();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panelContent = new Panel();
            btnBack = new Button();
            btnAddEmplyee = new Button();
            btnAttendance = new Button();
            panel1 = new Panel();
            btnSQL = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.FromArgb(128, 128, 255);
            pictureBox4.Location = new Point(0, 116);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(240, 568);
            pictureBox4.TabIndex = 32;
            pictureBox4.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(0, 0, 192);
            label2.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(152, 60);
            label2.Name = "label2";
            label2.Size = new Size(372, 40);
            label2.TabIndex = 31;
            label2.Text = "TCP Employee Payroll Slip";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(0, 0, 192);
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(8, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(120, 104);
            pictureBox2.TabIndex = 30;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(0, 0, 192);
            label1.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(144, 12);
            label1.Name = "label1";
            label1.Size = new Size(312, 47);
            label1.TabIndex = 29;
            label1.Text = "Welcome, Admin!";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(0, 0, 192);
            pictureBox1.Location = new Point(0, -4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1272, 120);
            pictureBox1.TabIndex = 28;
            pictureBox1.TabStop = false;
            // 
            // panelContent
            // 
            panelContent.Location = new Point(248, 120);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1008, 560);
            panelContent.TabIndex = 34;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(64, 632);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(120, 40);
            btnBack.TabIndex = 35;
            btnBack.Text = "Log Out";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnAddEmplyee
            // 
            btnAddEmplyee.Location = new Point(0, 16);
            btnAddEmplyee.Name = "btnAddEmplyee";
            btnAddEmplyee.Size = new Size(224, 64);
            btnAddEmplyee.TabIndex = 36;
            btnAddEmplyee.Text = "Add Employee";
            btnAddEmplyee.UseVisualStyleBackColor = true;
            // 
            // btnAttendance
            // 
            btnAttendance.Location = new Point(0, 88);
            btnAttendance.Name = "btnAttendance";
            btnAttendance.Size = new Size(224, 64);
            btnAttendance.TabIndex = 37;
            btnAttendance.Text = "Attendance/Overtime\r\n/Deductions/Payroll";
            btnAttendance.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(128, 128, 255);
            panel1.Controls.Add(btnSQL);
            panel1.Controls.Add(btnAddEmplyee);
            panel1.Controls.Add(btnAttendance);
            panel1.Location = new Point(8, 120);
            panel1.Name = "panel1";
            panel1.Size = new Size(224, 504);
            panel1.TabIndex = 0;
            // 
            // btnSQL
            // 
            btnSQL.Location = new Point(0, 200);
            btnSQL.Name = "btnSQL";
            btnSQL.Size = new Size(224, 64);
            btnSQL.TabIndex = 38;
            btnSQL.Text = "Access to MySQL\r\n";
            btnSQL.UseVisualStyleBackColor = true;
            // 
            // adminMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(panel1);
            Controls.Add(btnBack);
            Controls.Add(panelContent);
            Controls.Add(pictureBox4);
            Controls.Add(label2);
            Controls.Add(pictureBox2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "adminMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "admin";
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox4;
        private Label label2;
        private PictureBox pictureBox2;
        private Label label1;
        private PictureBox pictureBox1;
        private Panel panelContent;
        private Button btnBack;
        private Button btnAddEmplyee;
        private Button btnAttendance;
        private Panel panel1;
        private Button btnSQL;
    }
}