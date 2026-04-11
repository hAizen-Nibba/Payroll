using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

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

            txtbxPass.UseSystemPasswordChar = true;

            radShowPass.CheckedChanged += radShowPass_CheckedChanged;
        }

        private void radShowPass_CheckedChanged(object? sender, EventArgs e)
        {
            txtbxPass.UseSystemPasswordChar = !radShowPass.Checked;
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
            string inputPassword = HashPassword(rawPassword);

            if (string.IsNullOrWhiteSpace(inputName) || string.IsNullOrWhiteSpace(rawPassword))
            {
                MessageBox.Show("Please enter your name and password.", "Missing Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            MessageBox.Show("Forgot Password feature not implemented yet.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminMain adminForm = new adminMain();
            adminForm.Show();
            this.Hide();
        }
    }
}