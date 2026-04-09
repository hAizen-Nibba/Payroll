using MySql.Data.MySqlClient;
using System;
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
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            string inputName = txtbxName.Text.Trim();
            string inputPassword = txtbxPass.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputName) || string.IsNullOrWhiteSpace(inputPassword))
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
                WHERE CONCAT(e.f_name, ' ', e.l_name) = @full_name
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

    }
}