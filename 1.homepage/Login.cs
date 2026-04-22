using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Payroll__C__
{
    public partial class Login : Form
    {
        private readonly string connectionString =
            "server=localhost;database=payrolldb_test;uid=root;pwd=;";

        public Login()
        {
            InitializeComponent();

            btnLogin.Click += btnLogin_Click;
            btnForgotPass.Click += btnForgotPass_Click;

            radShowPass.CheckedChanged += radShowPass_CheckedChanged;
            radShowPassForPass.CheckedChanged += radShowPassForPass_CheckedChanged;

            button2.Click += button2_Click; // Load security question
            btnSecAnsConfirmed.Click += btnSecAnsConfirmed_Click; // Confirm security answer
            button1.Click += button1_Click; // Reset password

            txtbxPass.UseSystemPasswordChar = true;
            txtbNewPass.UseSystemPasswordChar = true;
            txtbConfNewPass.UseSystemPasswordChar = true;

            txtbSecQues.ReadOnly = true;
            txtbSecQues.TabStop = false;

            panelForgotPass.Visible = false;
            SetForgotPasswordInitialState();
        }

        private void radShowPass_CheckedChanged(object? sender, EventArgs e)
        {
            txtbxPass.UseSystemPasswordChar = !radShowPass.Checked;
        }

        private void radShowPassForPass_CheckedChanged(object? sender, EventArgs e)
        {
            bool hidePassword = !radShowPassForPass.Checked;
            txtbNewPass.UseSystemPasswordChar = hidePassword;
            txtbConfNewPass.UseSystemPasswordChar = hidePassword;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();

                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            string inputName = txtbxName.Text.Trim();
            string rawPassword = txtbxPass.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputName) || string.IsNullOrWhiteSpace(rawPassword))
            {
                MessageBox.Show("Please enter your name and password.", "Missing Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Admin login
            if (inputName.Equals("admin", StringComparison.OrdinalIgnoreCase) &&
                rawPassword == "teamcoapal")
            {
                adminMain adminForm = new adminMain();
                adminForm.Show();
                this.Hide();
                return;
            }

            string inputPassword = HashPassword(rawPassword);

            string query = @"
                SELECT 
                    e.emp_id,
                    CONCAT(e.f_name, ' ', e.l_name) AS full_name
                FROM employees e
                INNER JOIN security s ON e.emp_id = s.emp_id
                WHERE LOWER(TRIM(CONCAT(e.f_name, ' ', e.l_name))) = LOWER(TRIM(@full_name))
                  AND s.password = @password
                LIMIT 1;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@full_name", inputName);
                    cmd.Parameters.AddWithValue("@password", inputPassword);

                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int empId = Convert.ToInt32(reader["emp_id"]);
                            string fullName = reader["full_name"]?.ToString() ?? string.Empty;

                            Employee employeeForm = new Employee(empId, fullName);
                            employeeForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid name or password.", "Login Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnForgotPass_Click(object? sender, EventArgs e)
        {
            panelForgotPass.Visible = true;
            panelForgotPass.BringToFront();
            ClearForgotPasswordFields();
            SetForgotPasswordInitialState();
            txtbxNameForPass.Focus();
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            string inputName = txtbxNameForPass.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputName))
            {
                MessageBox.Show("Please enter your name first.", "Missing Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
                SELECT s.sec_quest
                FROM employees e
                INNER JOIN security s ON e.emp_id = s.emp_id
                WHERE LOWER(TRIM(CONCAT(e.f_name, ' ', e.l_name))) = LOWER(TRIM(@full_name))
                LIMIT 1;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@full_name", inputName);

                    conn.Open();
                    object? result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        txtbSecQues.Text = result.ToString();
                        SetForgotPasswordQuestionState();
                        txtbxSecAns.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Employee name not found.", "Not Found",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtbSecQues.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSecAnsConfirmed_Click(object? sender, EventArgs e)
        {
            string inputName = txtbxNameForPass.Text.Trim();
            string inputAnswer = txtbxSecAns.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputName) || string.IsNullOrWhiteSpace(inputAnswer))
            {
                MessageBox.Show("Please enter your security answer.", "Missing Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
                SELECT e.emp_id
                FROM employees e
                INNER JOIN security s ON e.emp_id = s.emp_id
                WHERE LOWER(TRIM(CONCAT(e.f_name, ' ', e.l_name))) = LOWER(TRIM(@full_name))
                  AND LOWER(TRIM(s.sec_ans)) = LOWER(TRIM(@sec_answer))
                LIMIT 1;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@full_name", inputName);
                    cmd.Parameters.AddWithValue("@sec_answer", inputAnswer);

                    conn.Open();
                    object? result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        SetForgotPasswordResetState();
                        txtbNewPass.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect security answer.", "Verification Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtbxSecAns.Focus();
                        txtbxSecAns.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            string inputName = txtbxNameForPass.Text.Trim();
            string newPassword = txtbNewPass.Text.Trim();
            string confirmPassword = txtbConfNewPass.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputName) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please complete the new password fields.", "Missing Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.", "Mismatch",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Weak Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string getEmpQuery = @"
                SELECT e.emp_id
                FROM employees e
                INNER JOIN security s ON e.emp_id = s.emp_id
                WHERE LOWER(TRIM(CONCAT(e.f_name, ' ', e.l_name))) = LOWER(TRIM(@full_name))
                LIMIT 1;";

            string updateQuery = @"
                UPDATE security
                SET password = @password
                WHERE emp_id = @emp_id;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand getEmpCmd = new MySqlCommand(getEmpQuery, conn))
                {
                    getEmpCmd.Parameters.AddWithValue("@full_name", inputName);

                    conn.Open();
                    object? result = getEmpCmd.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("Employee not found.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int empId = Convert.ToInt32(result);
                    string hashedPassword = HashPassword(newPassword);

                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@password", hashedPassword);
                        updateCmd.Parameters.AddWithValue("@emp_id", empId);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password reset successful.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            panelForgotPass.Visible = false;
                            ClearForgotPasswordFields();
                            SetForgotPasswordInitialState();
                        }
                        else
                        {
                            MessageBox.Show("Password reset failed.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetForgotPasswordInitialState()
        {
            txtbxNameForPass.Enabled = true;
            button2.Enabled = true;

            txtbSecQues.Enabled = false;
            txtbxSecAns.Enabled = false;
            btnSecAnsConfirmed.Enabled = false;

            txtbNewPass.Enabled = false;
            txtbConfNewPass.Enabled = false;
            radShowPassForPass.Enabled = false;
            button1.Enabled = false;

            txtbSecQues.ReadOnly = true;
        }

        private void SetForgotPasswordQuestionState()
        {
            txtbxNameForPass.Enabled = false;
            button2.Enabled = false;

            txtbSecQues.Enabled = true;
            txtbxSecAns.Enabled = true;
            btnSecAnsConfirmed.Enabled = true;

            txtbNewPass.Enabled = false;
            txtbConfNewPass.Enabled = false;
            radShowPassForPass.Enabled = false;
            button1.Enabled = false;

            txtbSecQues.ReadOnly = true;
        }

        private void SetForgotPasswordResetState()
        {
            txtbSecQues.Enabled = false;
            txtbxSecAns.Enabled = false;
            btnSecAnsConfirmed.Enabled = false;

            txtbNewPass.Enabled = true;
            txtbConfNewPass.Enabled = true;
            radShowPassForPass.Enabled = true;
            button1.Enabled = true;
        }

        private void ClearForgotPasswordFields()
        {
            txtbxNameForPass.Clear();
            txtbSecQues.Clear();
            txtbxSecAns.Clear();
            txtbNewPass.Clear();
            txtbConfNewPass.Clear();
            radShowPassForPass.Checked = false;
        }

        private void btnCancelForgot_Click(object sender, EventArgs e)
        {
            panelForgotPass.Visible = false;
            ClearForgotPasswordFields();
            SetForgotPasswordInitialState();
        }
    }
}