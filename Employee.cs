using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Payroll__C__
{
    public partial class Employee : Form
    {
        // Change this
        private readonly string connectionString =
            "server=localhost;database=payrolldb_test;uid=root;pwd=;";

        private int loggedInEmpId;
        private string loggedInEmployeeName = string.Empty;

        public Employee(int empId, string employeeName)
        {
            InitializeComponent();
            loggedInEmpId = empId;
            loggedInEmployeeName = employeeName;
        }

        public Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object? sender, EventArgs e)
        {
            lblName.Text = loggedInEmployeeName;

            txtbxDept.ReadOnly = true;
            txtbPos.ReadOnly = true;

            SetupGrids();

            btnPreviewPayslip.Enabled = false;

            LoadPayrollPeriods();

            cmbPeriods.SelectedIndexChanged += cmbPeriods_SelectedIndexChanged;
            btnBack.Click += btnBack_Click;
        }

        private void SetupGrids()
        {
            SetupSingleGrid(dgvAttendance);
            SetupSingleGrid(dgvOvertime);
            SetupSingleGrid(dgvEarningItems);
            SetupSingleGrid(dgvDeductions);
        }

        private void SetupSingleGrid(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
        }

        private void LoadPayrollPeriods()
        {
            string query = @"
                SELECT DISTINCT
                    pp.payroll_id,
                    CONCAT(
                        DATE_FORMAT(pp.period_start, '%Y-%m-%d'),
                        ' to ',
                        DATE_FORMAT(pp.period_end, '%Y-%m-%d')
                    ) AS period_label
                FROM payroll_periods pp
                INNER JOIN payroll_slip_record psr
                    ON pp.payroll_id = psr.payroll_id
                WHERE psr.emp_id = @emp_id
                ORDER BY pp.period_start DESC;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@emp_id", loggedInEmpId);

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                cmbPeriods.DataSource = null;
                cmbPeriods.DisplayMember = "period_label";
                cmbPeriods.ValueMember = "payroll_id";
                cmbPeriods.DataSource = dt;
            }

            if (cmbPeriods.Items.Count > 0)
            {
                cmbPeriods.SelectedIndex = -1;
            }

            btnPreviewPayslip.Enabled = false;
        }

        private void cmbPeriods_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbPeriods.SelectedValue == null ||
                !int.TryParse(cmbPeriods.SelectedValue.ToString(), out int payrollId))
            {
                btnPreviewPayslip.Enabled = false;
                return;
            }

            btnPreviewPayslip.Enabled = true;

            LoadEmployeeBasicInfo();
            LoadAttendance(payrollId);
            LoadOvertime(payrollId);
            LoadEarningItems(payrollId);
            LoadDeductions(payrollId);
        }

        private void LoadEmployeeBasicInfo()
        {
            string query = @"
                SELECT
                    d.dept_name,
                    p.pos_name
                FROM employees e
                INNER JOIN department d ON e.dept_id = d.dept_id
                INNER JOIN positions p ON e.pos_id = p.pos_id
                WHERE e.emp_id = @emp_id;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@emp_id", loggedInEmpId);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtbxDept.Text = reader["dept_name"].ToString();
                        txtbPos.Text = reader["pos_name"].ToString();
                    }
                }
            }
        }

        private void LoadAttendance(int payrollId)
        {
            string query = @"
                SELECT
                    a.work_date AS 'Work Date',
                    a.time_in AS 'Time In',
                    a.time_out AS 'Time Out'
                FROM attendance a
                INNER JOIN payroll_periods pp
                    ON a.work_date BETWEEN pp.period_start AND pp.period_end
                WHERE a.emp_id = @emp_id
                  AND pp.payroll_id = @payroll_id
                ORDER BY a.work_date;";

            dgvAttendance.DataSource = GetData(query, payrollId);
        }

        private void LoadOvertime(int payrollId)
        {
            string query = @"
                SELECT
                    o.ot_date AS 'OT Date',
                    o.hours AS 'Hours'
                FROM overtime o
                INNER JOIN payroll_periods pp
                    ON o.ot_date BETWEEN pp.period_start AND pp.period_end
                WHERE o.emp_id = @emp_id
                  AND pp.payroll_id = @payroll_id
                ORDER BY o.ot_date;";

            dgvOvertime.DataSource = GetData(query, payrollId);
        }

        private void LoadEarningItems(int payrollId)
        {
            string query = @"
                SELECT
                    pei.earn_type AS 'Earning Type',
                    pei.amount AS 'Amount'
                FROM payroll_earning_items pei
                INNER JOIN payroll_slip_record psr
                    ON pei.pay_rec_id = psr.pay_rec_id
                WHERE psr.emp_id = @emp_id
                  AND psr.payroll_id = @payroll_id;";

            dgvEarningItems.DataSource = GetData(query, payrollId);
        }

        private void LoadDeductions(int payrollId)
        {
            string query = @"
                SELECT
                    pdi.ded_type AS 'Deduction Type',
                    pdi.amount AS 'Amount'
                FROM payroll_deduction_items pdi
                INNER JOIN payroll_slip_record psr
                    ON pdi.pay_rec_id = psr.pay_rec_id
                WHERE psr.emp_id = @emp_id
                  AND psr.payroll_id = @payroll_id;";

            dgvDeductions.DataSource = GetData(query, payrollId);
        }

        private DataTable GetData(string query, int payrollId)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@emp_id", loggedInEmpId);
                cmd.Parameters.AddWithValue("@payroll_id", payrollId);
                da.Fill(dt);
            }

            return dt;
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit the application?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnPreviewPayslip_Click(object sender, EventArgs e)
        {
            if (cmbPeriods.SelectedValue == null)
            {
                MessageBox.Show("Please select a payroll period first.", "No Period Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(cmbPeriods.SelectedValue.ToString(), out int payrollId))
            {
                MessageBox.Show("Invalid payroll period selected.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedPeriod = cmbPeriods.Text;

            PreviewSlip preview = new PreviewSlip(loggedInEmpId, loggedInEmployeeName, payrollId, selectedPeriod);
            preview.ShowDialog();
        }
    }
}