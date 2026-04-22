namespace Payroll__C__
{
    partial class adminNewEmployee
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
            txtbL_name = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtbf_name = new TextBox();
            label3 = new Label();
            txtbPassword = new TextBox();
            pictureBox1 = new PictureBox();
            label4 = new Label();
            txtbSecQues = new TextBox();
            label5 = new Label();
            txtbSecAns = new TextBox();
            cmbDept = new ComboBox();
            cmbPosition = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            btnAdd = new Button();
            radShowPass = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtbL_name
            // 
            txtbL_name.Font = new Font("Segoe UI", 12F);
            txtbL_name.Location = new Point(120, 304);
            txtbL_name.Name = "txtbL_name";
            txtbL_name.Size = new Size(328, 29);
            txtbL_name.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ControlLight;
            label1.ForeColor = Color.Black;
            label1.Location = new Point(248, 288);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 16;
            label1.Text = "Last Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ControlLight;
            label2.ForeColor = Color.Black;
            label2.Location = new Point(248, 344);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 18;
            label2.Text = "First Name";
            // 
            // txtbf_name
            // 
            txtbf_name.Font = new Font("Segoe UI", 12F);
            txtbf_name.Location = new Point(120, 360);
            txtbf_name.Name = "txtbf_name";
            txtbf_name.Size = new Size(328, 29);
            txtbf_name.TabIndex = 17;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(680, 72);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 20;
            label3.Text = "Password";
            // 
            // txtbPassword
            // 
            txtbPassword.Font = new Font("Segoe UI", 12F);
            txtbPassword.Location = new Point(544, 88);
            txtbPassword.Name = "txtbPassword";
            txtbPassword.Size = new Size(328, 29);
            txtbPassword.TabIndex = 19;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ControlLight;
            pictureBox1.Location = new Point(104, 40);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(360, 424);
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(656, 168);
            label4.Name = "label4";
            label4.Size = new Size(100, 15);
            label4.TabIndex = 23;
            label4.Text = "Security Question";
            // 
            // txtbSecQues
            // 
            txtbSecQues.Font = new Font("Segoe UI", 12F);
            txtbSecQues.Location = new Point(544, 184);
            txtbSecQues.Multiline = true;
            txtbSecQues.Name = "txtbSecQues";
            txtbSecQues.Size = new Size(328, 152);
            txtbSecQues.TabIndex = 22;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(664, 352);
            label5.Name = "label5";
            label5.Size = new Size(91, 15);
            label5.TabIndex = 25;
            label5.Text = "Security Answer";
            // 
            // txtbSecAns
            // 
            txtbSecAns.Font = new Font("Segoe UI", 12F);
            txtbSecAns.Location = new Point(544, 368);
            txtbSecAns.Name = "txtbSecAns";
            txtbSecAns.Size = new Size(328, 29);
            txtbSecAns.TabIndex = 24;
            // 
            // cmbDept
            // 
            cmbDept.Font = new Font("Segoe UI", 12F);
            cmbDept.FormattingEnabled = true;
            cmbDept.Location = new Point(120, 128);
            cmbDept.Name = "cmbDept";
            cmbDept.Size = new Size(328, 29);
            cmbDept.TabIndex = 26;
            // 
            // cmbPosition
            // 
            cmbPosition.Font = new Font("Segoe UI", 12F);
            cmbPosition.FormattingEnabled = true;
            cmbPosition.Location = new Point(120, 184);
            cmbPosition.Name = "cmbPosition";
            cmbPosition.Size = new Size(328, 29);
            cmbPosition.TabIndex = 27;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ControlLight;
            label6.ForeColor = Color.Black;
            label6.Location = new Point(256, 168);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 29;
            label6.Text = "Position";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.ControlLight;
            label7.ForeColor = Color.Black;
            label7.Location = new Point(248, 112);
            label7.Name = "label7";
            label7.Size = new Size(70, 15);
            label7.TabIndex = 28;
            label7.Text = "Department";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(832, 448);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(144, 48);
            btnAdd.TabIndex = 30;
            btnAdd.Text = "Add to Database";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // radShowPass
            // 
            radShowPass.AutoSize = true;
            radShowPass.Location = new Point(544, 120);
            radShowPass.Name = "radShowPass";
            radShowPass.Size = new Size(108, 19);
            radShowPass.TabIndex = 31;
            radShowPass.Text = "Show Password";
            radShowPass.UseVisualStyleBackColor = true;
            // 
            // adminNewEmployee
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(992, 521);
            Controls.Add(radShowPass);
            Controls.Add(btnAdd);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(cmbPosition);
            Controls.Add(cmbDept);
            Controls.Add(label5);
            Controls.Add(txtbSecAns);
            Controls.Add(label4);
            Controls.Add(txtbSecQues);
            Controls.Add(label3);
            Controls.Add(txtbPassword);
            Controls.Add(label2);
            Controls.Add(txtbf_name);
            Controls.Add(label1);
            Controls.Add(txtbL_name);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "adminNewEmployee";
            Text = "adminNewEmployee";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtbL_name;
        private Label label1;
        private Label label2;
        private TextBox txtbf_name;
        private Label label3;
        private TextBox txtbPassword;
        private PictureBox pictureBox1;
        private Label label4;
        private TextBox txtbSecQues;
        private Label label5;
        private TextBox txtbSecAns;
        private ComboBox cmbDept;
        private ComboBox cmbPosition;
        private Label label6;
        private Label label7;
        private Button btnAdd;
        private CheckBox radShowPass;
    }
}