namespace Payroll__C__
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            btnForgotPass = new Button();
            txtbxPass = new TextBox();
            txtbxName = new TextBox();
            btnLogin = new Button();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            radShowPass = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(0, 0, 192);
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(768, 688);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(0, 0, 192);
            label1.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(40, 320);
            label1.Name = "label1";
            label1.Size = new Size(665, 65);
            label1.TabIndex = 2;
            label1.Text = "TCP Employee Payroll Portal";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(0, 0, 192);
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(0, 96);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(304, 272);
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(0, 0, 192);
            label2.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(40, 280);
            label2.Name = "label2";
            label2.Size = new Size(231, 50);
            label2.TabIndex = 4;
            label2.Text = "Welcome to";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnForgotPass
            // 
            btnForgotPass.Font = new Font("Segoe UI", 12F);
            btnForgotPass.Location = new Point(1056, 408);
            btnForgotPass.Name = "btnForgotPass";
            btnForgotPass.Size = new Size(136, 40);
            btnForgotPass.TabIndex = 5;
            btnForgotPass.Text = "Forgot Password";
            btnForgotPass.UseVisualStyleBackColor = true;
            // 
            // txtbxPass
            // 
            txtbxPass.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtbxPass.Location = new Point(856, 336);
            txtbxPass.Name = "txtbxPass";
            txtbxPass.Size = new Size(336, 35);
            txtbxPass.TabIndex = 6;
            txtbxPass.TextAlign = HorizontalAlignment.Center;
            // 
            // txtbxName
            // 
            txtbxName.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtbxName.Location = new Point(856, 272);
            txtbxName.Name = "txtbxName";
            txtbxName.Size = new Size(336, 35);
            txtbxName.TabIndex = 7;
            txtbxName.TextAlign = HorizontalAlignment.Center;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.DodgerBlue;
            btnLogin.Font = new Font("Segoe UI", 12F);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(856, 408);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(200, 40);
            btnLogin.TabIndex = 8;
            btnLogin.Text = "Log in";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(856, 248);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 9;
            label3.Text = "Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(856, 312);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 10;
            label4.Text = "Password";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F);
            button1.Location = new Point(960, 464);
            button1.Name = "button1";
            button1.Size = new Size(136, 40);
            button1.TabIndex = 11;
            button1.Text = "Admin";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // radShowPass
            // 
            radShowPass.AutoSize = true;
            radShowPass.Location = new Point(1080, 376);
            radShowPass.Name = "radShowPass";
            radShowPass.Size = new Size(108, 19);
            radShowPass.TabIndex = 13;
            radShowPass.Text = "Show Password";
            radShowPass.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(radShowPass);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnLogin);
            Controls.Add(txtbxName);
            Controls.Add(txtbxPass);
            Controls.Add(btnForgotPass);
            Controls.Add(label2);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private Label label1;
        private PictureBox pictureBox2;
        private Label label2;
        private Button btnForgotPass;
        private TextBox txtbxPass;
        private TextBox txtbxName;
        private Button btnLogin;
        private Label label3;
        private Label label4;
        private Button button1;
        private CheckBox radShowPass;
    }
}
