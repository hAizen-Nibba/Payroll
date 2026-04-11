using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Payroll__C__
{
    public partial class adminNewEmployee : Form
    {
        public static class DatabaseConnection
        {
            private static readonly string connectionString =
                "server=localhost;database=payrolldb_test;uid=root;pwd=;";

            public static MySqlConnection GetConnection()
            {
                return new MySqlConnection(connectionString);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }

        public adminNewEmployee()
        {
            InitializeComponent();

            this.Load += adminNewEmployee_Load;
            btnAdd.Click += btnAdd_Click;
            radShowPass.CheckedChanged += radShowPass_CheckedChanged;
        }

        private void adminNewEmployee_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            LoadPositions();

            txtbPassword.UseSystemPasswordChar = true;
        }
        private void radShowPass_CheckedChanged(object? sender, EventArgs e)
        {
            txtbPassword.UseSystemPasswordChar = !radShowPass.Checked;
        }

        private void LoadDepartments()
        {
            try
            {
                using (MySqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT dept_id, dept_name FROM department ORDER BY dept_name ASC";
                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cmbDept.DataSource = dt;
                        cmbDept.DisplayMember = "dept_name";
                        cmbDept.ValueMember = "dept_id";
                        cmbDept.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load departments.\n\n" + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPositions()
        {
            try
            {
                using (MySqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT pos_id, pos_name FROM positions ORDER BY pos_name ASC";
                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cmbPosition.DataSource = dt;
                        cmbPosition.DisplayMember = "pos_name";
                        cmbPosition.ValueMember = "pos_id";
                        cmbPosition.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load positions.\n\n" + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string lastName = txtbL_name.Text.Trim();
            string firstName = txtbf_name.Text.Trim();
            string password = txtbPassword.Text.Trim();
            string secQuestion = txtbSecQues.Text.Trim();
            string secAnswer = txtbSecAns.Text.Trim();

            if (string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(secQuestion) ||
                string.IsNullOrWhiteSpace(secAnswer) ||
                cmbDept.SelectedIndex == -1 ||
                cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Please complete all fields first.",
                    "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int deptId = Convert.ToInt32(cmbDept.SelectedValue);
            int posId = Convert.ToInt32(cmbPosition.SelectedValue);

            string hashedPassword = HashPassword(password);

            try
            {
                using (MySqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string employeeQuery = @"
                        INSERT INTO employees (f_name, l_name, dept_id, pos_id)
                        VALUES (@f_name, @l_name, @dept_id, @pos_id);
                        SELECT LAST_INSERT_ID();";

                            int newEmpId;

                            using (MySqlCommand cmd = new MySqlCommand(employeeQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@f_name", firstName);
                                cmd.Parameters.AddWithValue("@l_name", lastName);
                                cmd.Parameters.AddWithValue("@dept_id", deptId);
                                cmd.Parameters.AddWithValue("@pos_id", posId);

                                newEmpId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            string securityQuery = @"
                        INSERT INTO security (emp_id, password, sec_quest, sec_ans)
                        VALUES (@emp_id, @password, @sec_quest, @sec_ans)";

                            using (MySqlCommand cmd = new MySqlCommand(securityQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@emp_id", newEmpId);
                                cmd.Parameters.AddWithValue("@password", hashedPassword);
                                cmd.Parameters.AddWithValue("@sec_quest", secQuestion);
                                cmd.Parameters.AddWithValue("@sec_ans", secAnswer);

                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            MessageBox.Show($"Employee added successfully.\nEmployee ID: {newEmpId}",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearFields();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Failed to save employee.\n\n" + ex.Message,
                                "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error.\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtbL_name.Clear();
            txtbf_name.Clear();
            txtbPassword.Clear();
            txtbSecQues.Clear();
            txtbSecAns.Clear();

            cmbDept.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;

            txtbL_name.Focus();
        }
    }
}