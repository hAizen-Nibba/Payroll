using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Payroll__C__
{
    public partial class adminPayrollForm : Form
    {
        private readonly string connectionString =
            "server=localhost;database=payrolldb_test;uid=root;pwd=;";

        public adminPayrollForm()
        {
            InitializeComponent();

            // Form events
            this.Load += adminPayrollForm_Load;

            // Filter button events
            btnFilter.Click += btnFilter_Click;
            btnClear.Click += btnClear_Click;
            btnRefresh.Click += btnRefresh_Click;

            // ComboBox change events
            cmbDept.SelectedIndexChanged += cmbDept_SelectedIndexChanged;
            cmbPos.SelectedIndexChanged += cmbPos_SelectedIndexChanged;
            cmbName.SelectedIndexChanged += cmbName_SelectedIndexChanged;
            cmPeriods.SelectedIndexChanged += cmPeriods_SelectedIndexChanged;

            // Attendance buttons
            btnAddAttendance.Click += btnAddAttendance_Click;
            btnEditAttendance.Click += btnEditAttendance_Click;
            btnDeleteAttendance.Click += btnDeleteAttendance_Click;

            // Overtime buttons
            btnAddOT.Click += btnAddOT_Click;
            btnEditOT.Click += btnEditOT_Click;
            btnDeleteOT.Click += btnDeleteOT_Click;

            // Deduction buttons
            btnAddDeduction.Click += btnAddDeduction_Click;
            btnEditDeduction.Click += btnEditDeduction_Click;
            btnDeleteDeduction.Click += btnDeleteDeduction_Click;
        }

        #region Form Load

        private void adminPayrollForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDepartments();
                LoadPositions();
                LoadEmployees();
                LoadPeriods();

                LoadAttendance();
                LoadOvertime();
                LoadDeductions();
                LoadPayrollSlipRecord();

                FormatGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading payroll form:\n" + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Database Helpers

        private DataTable ExecuteQuery(string query, params MySqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                con.Open();
                da.Fill(dt);
            }

            return dt;
        }

        private int ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        private object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        #endregion

        #region Load ComboBoxes

        private void LoadDepartments()
        {
            string query = @"SELECT dept_id, dept_name 
                             FROM department
                             ORDER BY dept_name";

            DataTable dt = ExecuteQuery(query);

            DataRow allRow = dt.NewRow();
            allRow["dept_id"] = 0;
            allRow["dept_name"] = "All Departments";
            dt.Rows.InsertAt(allRow, 0);

            cmbDept.DataSource = dt;
            cmbDept.DisplayMember = "dept_name";
            cmbDept.ValueMember = "dept_id";
            cmbDept.SelectedIndex = 0;
        }

        private void LoadPositions(int deptId = 0)
        {
            string query = @"
                SELECT DISTINCT p.pos_id, p.pos_name
                FROM positions p
                LEFT JOIN employees e ON e.pos_id = p.pos_id
                WHERE (@deptId = 0 OR e.dept_id = @deptId)
                ORDER BY p.pos_name";

            DataTable dt = ExecuteQuery(query,
                new MySqlParameter("@deptId", deptId));

            DataRow allRow = dt.NewRow();
            allRow["pos_id"] = 0;
            allRow["pos_name"] = "All Positions";
            dt.Rows.InsertAt(allRow, 0);

            cmbPos.DataSource = dt;
            cmbPos.DisplayMember = "pos_name";
            cmbPos.ValueMember = "pos_id";
            cmbPos.SelectedIndex = 0;
        }

        private void LoadEmployees(int deptId = 0, int posId = 0)
        {
            string query = @"
                SELECT emp_id, CONCAT(f_name, ' ', l_name) AS full_name
                FROM employees
                WHERE (@deptId = 0 OR dept_id = @deptId)
                  AND (@posId = 0 OR pos_id = @posId)
                ORDER BY f_name, l_name";

            DataTable dt = ExecuteQuery(query,
                new MySqlParameter("@deptId", deptId),
                new MySqlParameter("@posId", posId));

            DataRow allRow = dt.NewRow();
            allRow["emp_id"] = 0;
            allRow["full_name"] = "All Employees";
            dt.Rows.InsertAt(allRow, 0);

            cmbName.DataSource = dt;
            cmbName.DisplayMember = "full_name";
            cmbName.ValueMember = "emp_id";
            cmbName.SelectedIndex = 0;
        }

        private void LoadPeriods()
        {
            string query = @"
        SELECT 
            payroll_id,
            CONCAT(DATE_FORMAT(period_start, '%Y-%m-%d'), ' to ', DATE_FORMAT(period_end, '%Y-%m-%d')) AS period_name
        FROM payroll_periods
        ORDER BY payroll_id DESC";

            DataTable dt = ExecuteQuery(query);

            DataRow allRow = dt.NewRow();
            allRow["payroll_id"] = 0;
            allRow["period_name"] = "All Periods";
            dt.Rows.InsertAt(allRow, 0);

            cmPeriods.DataSource = dt;
            cmPeriods.DisplayMember = "period_name";
            cmPeriods.ValueMember = "payroll_id";
            cmPeriods.SelectedIndex = 0;
        }

        #endregion

        #region Filter Helpers

        private int GetSelectedComboValue(ComboBox comboBox)
        {
            if (comboBox.SelectedValue == null)
                return 0;

            if (int.TryParse(comboBox.SelectedValue.ToString(), out int value))
                return value;

            return 0;
        }

        private int SelectedDeptId => GetSelectedComboValue(cmbDept);
        private int SelectedPosId => GetSelectedComboValue(cmbPos);
        private int SelectedEmpId => GetSelectedComboValue(cmbName);
        private int SelectedPayrollId => GetSelectedComboValue(cmPeriods);

        #endregion

        #region Load Grids

        private void LoadAttendance()
        {
            string query = @"
        SELECT 
            a.att_id AS 'ID',
            e.emp_id AS 'Employee ID',
            CONCAT(e.f_name, ' ', e.l_name) AS 'Employee Name',
            a.work_date AS 'Work Date',
            a.time_in AS 'Time In',
            a.time_out AS 'Time Out'
        FROM attendance a
        INNER JOIN employees e ON a.emp_id = e.emp_id
        WHERE (@empId = 0 OR a.emp_id = @empId)
          AND (@deptId = 0 OR e.dept_id = @deptId)
          AND (@posId = 0 OR e.pos_id = @posId)
        ORDER BY a.work_date DESC";

            dgvAttendance.DataSource = ExecuteQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@deptId", SelectedDeptId),
                new MySqlParameter("@posId", SelectedPosId));
        }

        private void LoadOvertime()
        {
            string query = @"
        SELECT 
            o.ot_id AS 'ID',
            e.emp_id AS 'Employee ID',
            CONCAT(e.f_name, ' ', e.l_name) AS 'Employee Name',
            o.ot_date AS 'OT Date',
            o.hours AS 'Hours'
        FROM overtime o
        INNER JOIN employees e ON o.emp_id = e.emp_id
        WHERE (@empId = 0 OR o.emp_id = @empId)
          AND (@deptId = 0 OR e.dept_id = @deptId)
          AND (@posId = 0 OR e.pos_id = @posId)
        ORDER BY o.ot_date DESC";

            dgvOvertime.DataSource = ExecuteQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@deptId", SelectedDeptId),
                new MySqlParameter("@posId", SelectedPosId));
        }

        private void LoadDeductions()
        {
            string query = @"
        SELECT 
            pdi.ded_id AS 'ID',
            e.emp_id AS 'Employee ID',
            CONCAT(e.f_name, ' ', e.l_name) AS 'Employee Name',
            pdi.ded_type AS 'Deduction',
            pdi.amount AS 'Amount',
            CONCAT(DATE_FORMAT(pp.period_start, '%Y-%m-%d'), ' to ', DATE_FORMAT(pp.period_end, '%Y-%m-%d')) AS 'Payroll Period'
        FROM payroll_deduction_items pdi
        INNER JOIN payroll_slip_record psr ON pdi.pay_rec_id = psr.pay_rec_id
        INNER JOIN employees e ON psr.emp_id = e.emp_id
        INNER JOIN payroll_periods pp ON psr.payroll_id = pp.payroll_id
        WHERE (@payrollId = 0 OR psr.payroll_id = @payrollId)
          AND (@empId = 0 OR e.emp_id = @empId)
          AND (@deptId = 0 OR e.dept_id = @deptId)
          AND (@posId = 0 OR e.pos_id = @posId)
        ORDER BY pp.period_start DESC, e.f_name, e.l_name";

            dgvDeduction.DataSource = ExecuteQuery(query,
                new MySqlParameter("@payrollId", SelectedPayrollId),
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@deptId", SelectedDeptId),
                new MySqlParameter("@posId", SelectedPosId));
        }

        private void LoadPayrollSlipRecord()
        {
            string query = @"
        SELECT 
            psr.pay_rec_id AS 'Record ID',
            psr.payroll_id AS 'Payroll ID',
            e.emp_id AS 'Employee ID',
            CONCAT(e.f_name, ' ', e.l_name) AS 'Employee Name',
            CONCAT(
                DATE_FORMAT(pp.period_start, '%Y-%m-%d'),
                ' to ',
                DATE_FORMAT(pp.period_end, '%Y-%m-%d')
            ) AS 'Payroll Period',
            psr.gross_pay AS 'Gross Pay',
            psr.total_deduction AS 'Total Deduction',
            psr.net_pay AS 'Net Pay'
        FROM payroll_slip_record psr
        INNER JOIN employees e ON psr.emp_id = e.emp_id
        INNER JOIN payroll_periods pp ON psr.payroll_id = pp.payroll_id
        WHERE (@payrollId = 0 OR psr.payroll_id = @payrollId)
          AND (@empId = 0 OR psr.emp_id = @empId)
          AND (@deptId = 0 OR e.dept_id = @deptId)
          AND (@posId = 0 OR e.pos_id = @posId)
        ORDER BY psr.payroll_id DESC, e.f_name, e.l_name";

            dgvPayrollSlipRecord.DataSource = ExecuteQuery(query,
                new MySql.Data.MySqlClient.MySqlParameter("@payrollId", SelectedPayrollId),
                new MySql.Data.MySqlClient.MySqlParameter("@empId", SelectedEmpId),
                new MySql.Data.MySqlClient.MySqlParameter("@deptId", SelectedDeptId),
                new MySql.Data.MySqlClient.MySqlParameter("@posId", SelectedPosId));
        }

        private void LoadAllGrids()
        {
            LoadAttendance();
            LoadOvertime();
            LoadDeductions();
            LoadPayrollSlipRecord();
            FormatGrids();
        }

        #endregion

        #region Formatting

        private void FormatGrids()
        {
            FormatGrid(dgvAttendance);
            FormatGrid(dgvOvertime);
            FormatGrid(dgvDeduction);
            FormatGrid(dgvPayrollSlipRecord);
        }

        private void FormatGrid(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
        }

        #endregion

        #region Filter Events

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAllGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filter error:\n" + ex.Message,
                    "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                cmbDept.SelectedIndex = 0;
                cmbPos.SelectedIndex = 0;
                cmbName.SelectedIndex = 0;
                cmPeriods.SelectedIndex = 0;

                LoadAllGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clear error:\n" + ex.Message,
                    "Clear Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDepartments();
                LoadPositions();
                LoadEmployees();
                LoadPeriods();
                LoadAllGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Refresh error:\n" + ex.Message,
                    "Refresh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int deptId = SelectedDeptId;
                LoadPositions(deptId);
                LoadEmployees(deptId, GetSelectedComboValue(cmbPos));
            }
            catch
            {
                // Prevent startup binding errors from showing
            }
        }

        private void cmbPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadEmployees(SelectedDeptId, SelectedPosId);
            }
            catch
            {
            }
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAllGrids();
            }
            catch
            {
            }
        }

        private void cmPeriods_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadPayrollSlipRecord();
            }
            catch
            {
            }
        }

        #endregion

        #region Attendance CRUD

        private void btnAddAttendance_Click(object sender, EventArgs e)
        {
            if (SelectedEmpId == 0)
            {
                MessageBox.Show("Please select an employee first.",
                    "No Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dateText = Prompt.ShowDialog("Enter attendance date (yyyy-MM-dd):", "Add Attendance");
            if (string.IsNullOrWhiteSpace(dateText)) return;

            string daysText = Prompt.ShowDialog("Enter days present:", "Add Attendance");
            if (string.IsNullOrWhiteSpace(daysText)) return;

            if (!DateTime.TryParse(dateText, out DateTime workDate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }

            if (!decimal.TryParse(daysText, out decimal daysPresent))
            {
                MessageBox.Show("Invalid days present value.");
                return;
            }

            string query = @"
                INSERT INTO attendance (emp_id, work_date, days_present)
                VALUES (@empId, @workDate, @daysPresent)";

            ExecuteNonQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@workDate", workDate),
                new MySqlParameter("@daysPresent", daysPresent));

            LoadAttendance();
            MessageBox.Show("Attendance added successfully.");
        }

        private void btnEditAttendance_Click(object sender, EventArgs e)
        {
            if (dgvAttendance.CurrentRow == null)
            {
                MessageBox.Show("Please select an attendance record.");
                return;
            }

            int attId = Convert.ToInt32(dgvAttendance.CurrentRow.Cells["ID"].Value);
            string currentDate = dgvAttendance.CurrentRow.Cells["Work Date"].Value.ToString();
            string currentDays = dgvAttendance.CurrentRow.Cells["Days Present"].Value.ToString();

            string newDate = Prompt.ShowDialog("Edit attendance date (yyyy-MM-dd):", "Edit Attendance", currentDate);
            if (string.IsNullOrWhiteSpace(newDate)) return;

            string newDays = Prompt.ShowDialog("Edit days present:", "Edit Attendance", currentDays);
            if (string.IsNullOrWhiteSpace(newDays)) return;

            if (!DateTime.TryParse(newDate, out DateTime workDate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }

            if (!decimal.TryParse(newDays, out decimal daysPresent))
            {
                MessageBox.Show("Invalid days present value.");
                return;
            }

            string query = @"
                UPDATE attendance
                SET work_date = @workDate,
                    days_present = @daysPresent
                WHERE att_id = @attId";

            ExecuteNonQuery(query,
                new MySqlParameter("@workDate", workDate),
                new MySqlParameter("@daysPresent", daysPresent),
                new MySqlParameter("@attId", attId));

            LoadAttendance();
            MessageBox.Show("Attendance updated successfully.");
        }

        private void btnDeleteAttendance_Click(object sender, EventArgs e)
        {
            if (dgvAttendance.CurrentRow == null)
            {
                MessageBox.Show("Please select an attendance record.");
                return;
            }

            int attId = Convert.ToInt32(dgvAttendance.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this attendance record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            string query = "DELETE FROM attendance WHERE att_id = @attId";

            ExecuteNonQuery(query, new MySqlParameter("@attId", attId));

            LoadAttendance();
            MessageBox.Show("Attendance deleted successfully.");
        }

        #endregion

        #region Overtime CRUD

        private void btnAddOT_Click(object sender, EventArgs e)
        {
            if (SelectedEmpId == 0)
            {
                MessageBox.Show("Please select an employee first.",
                    "No Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dateText = Prompt.ShowDialog("Enter OT date (yyyy-MM-dd):", "Add Overtime");
            if (string.IsNullOrWhiteSpace(dateText)) return;

            string hoursText = Prompt.ShowDialog("Enter OT hours:", "Add Overtime");
            if (string.IsNullOrWhiteSpace(hoursText)) return;

            string amountText = Prompt.ShowDialog("Enter OT amount:", "Add Overtime");
            if (string.IsNullOrWhiteSpace(amountText)) return;

            if (!DateTime.TryParse(dateText, out DateTime otDate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }

            if (!decimal.TryParse(hoursText, out decimal hours))
            {
                MessageBox.Show("Invalid hours value.");
                return;
            }

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                MessageBox.Show("Invalid amount value.");
                return;
            }

            string query = @"
                INSERT INTO overtime (emp_id, ot_date, hours, amount)
                VALUES (@empId, @otDate, @hours, @amount)";

            ExecuteNonQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@otDate", otDate),
                new MySqlParameter("@hours", hours),
                new MySqlParameter("@amount", amount));

            LoadOvertime();
            MessageBox.Show("Overtime added successfully.");
        }

        private void btnEditOT_Click(object sender, EventArgs e)
        {
            if (dgvOvertime.CurrentRow == null)
            {
                MessageBox.Show("Please select an overtime record.");
                return;
            }

            int otId = Convert.ToInt32(dgvOvertime.CurrentRow.Cells["ID"].Value);
            string currentDate = dgvOvertime.CurrentRow.Cells["OT Date"].Value.ToString();
            string currentHours = dgvOvertime.CurrentRow.Cells["Hours"].Value.ToString();
            string currentAmount = dgvOvertime.CurrentRow.Cells["Amount"].Value.ToString();

            string newDate = Prompt.ShowDialog("Edit OT date (yyyy-MM-dd):", "Edit Overtime", currentDate);
            if (string.IsNullOrWhiteSpace(newDate)) return;

            string newHours = Prompt.ShowDialog("Edit OT hours:", "Edit Overtime", currentHours);
            if (string.IsNullOrWhiteSpace(newHours)) return;

            string newAmount = Prompt.ShowDialog("Edit OT amount:", "Edit Overtime", currentAmount);
            if (string.IsNullOrWhiteSpace(newAmount)) return;

            if (!DateTime.TryParse(newDate, out DateTime otDate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }

            if (!decimal.TryParse(newHours, out decimal hours))
            {
                MessageBox.Show("Invalid hours value.");
                return;
            }

            if (!decimal.TryParse(newAmount, out decimal amount))
            {
                MessageBox.Show("Invalid amount value.");
                return;
            }

            string query = @"
                UPDATE overtime
                SET ot_date = @otDate,
                    hours = @hours,
                    amount = @amount
                WHERE ot_id = @otId";

            ExecuteNonQuery(query,
                new MySqlParameter("@otDate", otDate),
                new MySqlParameter("@hours", hours),
                new MySqlParameter("@amount", amount),
                new MySqlParameter("@otId", otId));

            LoadOvertime();
            MessageBox.Show("Overtime updated successfully.");
        }

        private void btnDeleteOT_Click(object sender, EventArgs e)
        {
            if (dgvOvertime.CurrentRow == null)
            {
                MessageBox.Show("Please select an overtime record.");
                return;
            }

            int otId = Convert.ToInt32(dgvOvertime.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this overtime record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            string query = "DELETE FROM overtime WHERE ot_id = @otId";

            ExecuteNonQuery(query, new MySqlParameter("@otId", otId));

            LoadOvertime();
            MessageBox.Show("Overtime deleted successfully.");
        }

        #endregion

        #region Deduction CRUD

        private void btnAddDeduction_Click(object sender, EventArgs e)
        {
            if (SelectedEmpId == 0)
            {
                MessageBox.Show("Please select an employee first.",
                    "No Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = Prompt.ShowDialog("Enter deduction name:", "Add Deduction");
            if (string.IsNullOrWhiteSpace(name)) return;

            string amountText = Prompt.ShowDialog("Enter deduction amount:", "Add Deduction");
            if (string.IsNullOrWhiteSpace(amountText)) return;

            string dateText = Prompt.ShowDialog("Enter deduction date (yyyy-MM-dd):", "Add Deduction");
            if (string.IsNullOrWhiteSpace(dateText)) return;

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                MessageBox.Show("Invalid amount value.");
                return;
            }

            if (!DateTime.TryParse(dateText, out DateTime deductionDate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }

            string query = @"
                INSERT INTO deductions (emp_id, deduction_name, amount, deduction_date)
                VALUES (@empId, @name, @amount, @deductionDate)";

            ExecuteNonQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@name", name),
                new MySqlParameter("@amount", amount),
                new MySqlParameter("@deductionDate", deductionDate));

            LoadDeductions();
            MessageBox.Show("Deduction added successfully.");
        }

        private void btnEditDeduction_Click(object sender, EventArgs e)
        {
            if (dgvDeduction.CurrentRow == null)
            {
                MessageBox.Show("Please select a deduction record.");
                return;
            }

            int deductionId = Convert.ToInt32(dgvDeduction.CurrentRow.Cells["ID"].Value);
            string currentName = dgvDeduction.CurrentRow.Cells["Deduction"].Value.ToString();
            string currentAmount = dgvDeduction.CurrentRow.Cells["Amount"].Value.ToString();
            string currentDate = dgvDeduction.CurrentRow.Cells["Date"].Value.ToString();

            string newName = Prompt.ShowDialog("Edit deduction name:", "Edit Deduction", currentName);
            if (string.IsNullOrWhiteSpace(newName)) return;

            string newAmount = Prompt.ShowDialog("Edit deduction amount:", "Edit Deduction", currentAmount);
            if (string.IsNullOrWhiteSpace(newAmount)) return;

            string newDate = Prompt.ShowDialog("Edit deduction date (yyyy-MM-dd):", "Edit Deduction", currentDate);
            if (string.IsNullOrWhiteSpace(newDate)) return;

            if (!decimal.TryParse(newAmount, out decimal amount))
            {
                MessageBox.Show("Invalid amount value.");
                return;
            }

            if (!DateTime.TryParse(newDate, out DateTime deductionDate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }

            string query = @"
                UPDATE deductions
                SET deduction_name = @name,
                    amount = @amount,
                    deduction_date = @deductionDate
                WHERE deduction_id = @deductionId";

            ExecuteNonQuery(query,
                new MySqlParameter("@name", newName),
                new MySqlParameter("@amount", amount),
                new MySqlParameter("@deductionDate", deductionDate),
                new MySqlParameter("@deductionId", deductionId));

            LoadDeductions();
            MessageBox.Show("Deduction updated successfully.");
        }

        private void btnDeleteDeduction_Click(object sender, EventArgs e)
        {
            if (dgvDeduction.CurrentRow == null)
            {
                MessageBox.Show("Please select a deduction record.");
                return;
            }

            int deductionId = Convert.ToInt32(dgvDeduction.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this deduction record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            string query = "DELETE FROM deductions WHERE deduction_id = @deductionId";

            ExecuteNonQuery(query, new MySqlParameter("@deductionId", deductionId));

            LoadDeductions();
            MessageBox.Show("Deduction deleted successfully.");
        }

        #endregion
    }

    // Simple reusable input box for WinForms
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 380,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label lblText = new Label()
            {
                Left = 20,
                Top = 20,
                Width = 320,
                Text = text
            };

            TextBox txtInput = new TextBox()
            {
                Left = 20,
                Top = 50,
                Width = 320,
                Text = defaultValue
            };

            Button btnOk = new Button()
            {
                Text = "OK",
                Left = 180,
                Width = 75,
                Top = 90,
                DialogResult = DialogResult.OK
            };

            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Left = 265,
                Width = 75,
                Top = 90,
                DialogResult = DialogResult.Cancel
            };

            prompt.Controls.Add(lblText);
            prompt.Controls.Add(txtInput);
            prompt.Controls.Add(btnOk);
            prompt.Controls.Add(btnCancel);

            prompt.AcceptButton = btnOk;
            prompt.CancelButton = btnCancel;

            return prompt.ShowDialog() == DialogResult.OK ? txtInput.Text : "";
        }
    }
}