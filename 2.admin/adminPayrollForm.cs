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

        private int activePayrollId = 0;
        private DateTime activePeriodStart;
        private DateTime activePeriodEnd;
        private string activePeriodLabel = "";

        private TabPage attendanceTab;
        private TabPage overtimeTab;
        private TabPage deductionTab;
        private TabPage payrollSlipTab;
        private List<TabPage> lockedTabs = new List<TabPage>();

        private const decimal OVERTIME_RATE_PER_HOUR = 250m;

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

            dgvPeriods.SelectionChanged += dgvPeriods_SelectionChanged;
            tabControl.Selecting += tabControl_Selecting;

            lockedTabs = new List<TabPage>();

            foreach (TabPage tab in tabControl.TabPages)
            {
                if (tab != tabPeriods)
                    lockedTabs.Add(tab);
            }
        }

        private void UpdateTabAccess()
        {
            if (!HasActivePeriod)
            {
                foreach (TabPage tab in lockedTabs)
                {
                    if (tabControl.TabPages.Contains(tab))
                        tabControl.TabPages.Remove(tab);
                }

                if (tabControl.SelectedTab != tabPeriods)
                    tabControl.SelectedTab = tabPeriods;
            }
            else
            {
                foreach (TabPage tab in lockedTabs)
                {
                    if (!tabControl.TabPages.Contains(tab))
                        tabControl.TabPages.Add(tab);
                }
            }
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
                UpdatePeriodTabCaption();
                UpdateTabAccess();
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

        #endregion

        private bool HasActivePeriod => activePayrollId > 0;

        private void LoadPeriods()
        {
            string query = @"
        SELECT 
            payroll_id AS 'Payroll ID',
            period_start AS 'Period Start',
            period_end AS 'Period End',
            pay_date AS 'Pay Date',
            status AS 'Status'
        FROM payroll_periods
        ORDER BY period_start DESC";

            dgvPeriods.DataSource = ExecuteQuery(query);
            FormatGrid(dgvPeriods);

            if (dgvPeriods.Rows.Count > 0)
            {
                dgvPeriods.ClearSelection();
            }
        }

        private void ApplySelectedPeriodFromGrid()
        {
            if (dgvPeriods.CurrentRow == null)
                return;

            object payrollObj = dgvPeriods.CurrentRow.Cells["Payroll ID"].Value;
            object startObj = dgvPeriods.CurrentRow.Cells["Period Start"].Value;
            object endObj = dgvPeriods.CurrentRow.Cells["Period End"].Value;

            if (payrollObj == null || startObj == null || endObj == null)
                return;

            activePayrollId = Convert.ToInt32(payrollObj);
            activePeriodStart = Convert.ToDateTime(startObj).Date;
            activePeriodEnd = Convert.ToDateTime(endObj).Date;
            activePeriodLabel = $"{activePeriodStart:yyyy-MM-dd} to {activePeriodEnd:yyyy-MM-dd}";

            UpdatePeriodTabCaption();
            UpdateTabAccess();
            LoadAllGrids();
        }

        private void ClearActivePeriod()
        {
            activePayrollId = 0;
            activePeriodStart = DateTime.MinValue;
            activePeriodEnd = DateTime.MinValue;
            activePeriodLabel = "";
            UpdatePeriodTabCaption();
            UpdateTabAccess();
            LoadAllGrids();
        }

        private void UpdatePeriodTabCaption()
        {
            if (HasActivePeriod)
            {
                tabPeriods.Text = $"Periods ({activePeriodLabel})";
                this.Text = $"adminPayrollForm - Active Period: {activePeriodLabel}";
            }
            else
            {
                tabPeriods.Text = "Periods";
                this.Text = "adminPayrollForm";
            }
        }

        private bool IsDateWithinActivePeriod(DateTime date)
        {
            if (!HasActivePeriod)
                return false;

            DateTime d = date.Date;
            return d >= activePeriodStart && d <= activePeriodEnd;
        }

        private void dgvPeriods_SelectionChanged(object? sender, EventArgs e)
        {
            try
            {
                if (dgvPeriods.CurrentRow != null)
                    ApplySelectedPeriodFromGrid();
            }
            catch
            {
            }
        }

        private void tabControl_Selecting(object? sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPeriods)
                return;

            if (!HasActivePeriod)
            {
                e.Cancel = true;
                MessageBox.Show("Please select a payroll period first in the Periods tab.",
                    "No Period Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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
          AND (@hasPeriod = 0 OR a.work_date BETWEEN @periodStart AND @periodEnd)
        ORDER BY a.work_date DESC, e.f_name, e.l_name";

            dgvAttendance.DataSource = ExecuteQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@deptId", SelectedDeptId),
                new MySqlParameter("@posId", SelectedPosId),
                new MySqlParameter("@hasPeriod", HasActivePeriod ? 1 : 0),
                new MySqlParameter("@periodStart", HasActivePeriod ? activePeriodStart : DBNull.Value),
                new MySqlParameter("@periodEnd", HasActivePeriod ? activePeriodEnd : DBNull.Value));
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
          AND (@hasPeriod = 0 OR o.ot_date BETWEEN @periodStart AND @periodEnd)
        ORDER BY o.ot_date DESC, e.f_name, e.l_name";

            dgvOvertime.DataSource = ExecuteQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@deptId", SelectedDeptId),
                new MySqlParameter("@posId", SelectedPosId),
                new MySqlParameter("@hasPeriod", HasActivePeriod ? 1 : 0),
                new MySqlParameter("@periodStart", HasActivePeriod ? activePeriodStart : DBNull.Value),
                new MySqlParameter("@periodEnd", HasActivePeriod ? activePeriodEnd : DBNull.Value));
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
        WHERE (@empId = 0 OR e.emp_id = @empId)
          AND (@deptId = 0 OR e.dept_id = @deptId)
          AND (@posId = 0 OR e.pos_id = @posId)
          AND (@payrollId = 0 OR psr.payroll_id = @payrollId)
        ORDER BY pp.period_start DESC, e.f_name, e.l_name";

            dgvDeduction.DataSource = ExecuteQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@deptId", SelectedDeptId),
                new MySqlParameter("@posId", SelectedPosId),
                new MySqlParameter("@payrollId", activePayrollId));
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
        WHERE (@empId = 0 OR psr.emp_id = @empId)
          AND (@deptId = 0 OR e.dept_id = @deptId)
          AND (@posId = 0 OR e.pos_id = @posId)
          AND (@payrollId = 0 OR psr.payroll_id = @payrollId)
        ORDER BY psr.payroll_id DESC, e.f_name, e.l_name";

            dgvPayrollSlipRecord.DataSource = ExecuteQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@deptId", SelectedDeptId),
                new MySqlParameter("@posId", SelectedPosId),
                new MySqlParameter("@payrollId", activePayrollId));
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

                dgvPeriods.ClearSelection();
                ClearActivePeriod();
                tabControl.SelectedTab = tabPeriods;
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
                ClearActivePeriod();
                tabControl.SelectedTab = tabPeriods;
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

        private int GetSelectedPayrollRecordId()
        {
            if (dgvPayrollSlipRecord.CurrentRow == null)
                return 0;

            return Convert.ToInt32(dgvPayrollSlipRecord.CurrentRow.Cells["Record ID"].Value);
        }

        private string PromptTime(string label, string caption, string defaultValue = "")
        {
            return Prompt.ShowDialog(label + " (HH:mm or HH:mm:ss):", caption, defaultValue);
        }

        private bool TryParseTime(string input, out TimeSpan time)
        {
            return TimeSpan.TryParse(input, out time);
        }

        private void RecalculatePayrollRecord(int payRecId)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                decimal totalEarnings = 0m;
                decimal totalDeductions = 0m;

                string earningsQuery = @"
            SELECT COALESCE(SUM(amount), 0)
            FROM payroll_earning_items
            WHERE pay_rec_id = @payRecId";

                using (MySqlCommand cmd = new MySqlCommand(earningsQuery, con))
                {
                    cmd.Parameters.AddWithValue("@payRecId", payRecId);
                    object result = cmd.ExecuteScalar();
                    totalEarnings = result != null && result != DBNull.Value
                        ? Convert.ToDecimal(result)
                        : 0m;
                }

                string deductionsQuery = @"
            SELECT COALESCE(SUM(amount), 0)
            FROM payroll_deduction_items
            WHERE pay_rec_id = @payRecId";

                using (MySqlCommand cmd = new MySqlCommand(deductionsQuery, con))
                {
                    cmd.Parameters.AddWithValue("@payRecId", payRecId);
                    object result = cmd.ExecuteScalar();
                    totalDeductions = result != null && result != DBNull.Value
                        ? Convert.ToDecimal(result)
                        : 0m;
                }

                decimal netPay = totalEarnings - totalDeductions;

                string updateQuery = @"
            UPDATE payroll_slip_record
            SET gross_pay = @grossPay,
                total_deduction = @totalDeduction,
                net_pay = @netPay
            WHERE pay_rec_id = @payRecId";

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@grossPay", totalEarnings);
                    cmd.Parameters.AddWithValue("@totalDeduction", totalDeductions);
                    cmd.Parameters.AddWithValue("@netPay", netPay);
                    cmd.Parameters.AddWithValue("@payRecId", payRecId);
                    cmd.ExecuteNonQuery();
                }
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

            string timeInText = PromptTime("Enter time in", "Add Attendance");
            if (string.IsNullOrWhiteSpace(timeInText)) return;

            string timeOutText = PromptTime("Enter time out", "Add Attendance");
            if (string.IsNullOrWhiteSpace(timeOutText)) return;

            if (!DateTime.TryParse(dateText, out DateTime workDate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }

            if (!HasActivePeriod)
            {
                MessageBox.Show("Please select a payroll period first.");
                return;
            }

            if (!IsDateWithinActivePeriod(workDate))
            {
                MessageBox.Show($"Attendance date must be within the active period:\n{activePeriodLabel}");
                return;
            }

            if (!HasActivePeriod)
            {
                MessageBox.Show("Please select a payroll period first.");
                return;
            }

            if (!IsDateWithinActivePeriod(workDate))
            {
                MessageBox.Show($"Attendance date must be within the active period:\n{activePeriodLabel}");
                return;
            }

            if (!TryParseTime(timeInText, out TimeSpan timeIn))
            {
                MessageBox.Show("Invalid time in format.");
                return;
            }

            if (!TryParseTime(timeOutText, out TimeSpan timeOut))
            {
                MessageBox.Show("Invalid time out format.");
                return;
            }

            string query = @"
        INSERT INTO attendance (emp_id, work_date, time_in, time_out)
        VALUES (@empId, @workDate, @timeIn, @timeOut)";

            ExecuteNonQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@workDate", workDate.Date),
                new MySqlParameter("@timeIn", timeIn),
                new MySqlParameter("@timeOut", timeOut));

            RebuildPayrollSlipForEmployee(activePayrollId, SelectedEmpId);
            LoadAttendance();
            LoadPayrollSlipRecord();
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
            string currentDate = dgvAttendance.CurrentRow.Cells["Work Date"].Value?.ToString() ?? "";
            string currentTimeIn = dgvAttendance.CurrentRow.Cells["Time In"].Value?.ToString() ?? "";
            string currentTimeOut = dgvAttendance.CurrentRow.Cells["Time Out"].Value?.ToString() ?? "";

            string newDate = Prompt.ShowDialog("Edit attendance date (yyyy-MM-dd):", "Edit Attendance", currentDate);
            if (string.IsNullOrWhiteSpace(newDate)) return;

            string newTimeIn = PromptTime("Edit time in", "Edit Attendance", currentTimeIn);
            if (string.IsNullOrWhiteSpace(newTimeIn)) return;

            string newTimeOut = PromptTime("Edit time out", "Edit Attendance", currentTimeOut);
            if (string.IsNullOrWhiteSpace(newTimeOut)) return;

            if (!DateTime.TryParse(newDate, out DateTime workDate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }

            if (!TryParseTime(newTimeIn, out TimeSpan timeIn))
            {
                MessageBox.Show("Invalid time in format.");
                return;
            }

            if (!TryParseTime(newTimeOut, out TimeSpan timeOut))
            {
                MessageBox.Show("Invalid time out format.");
                return;
            }

            string query = @"
        UPDATE attendance
        SET work_date = @workDate,
            time_in = @timeIn,
            time_out = @timeOut
        WHERE att_id = @attId";

            ExecuteNonQuery(query,
                new MySqlParameter("@workDate", workDate.Date),
                new MySqlParameter("@timeIn", timeIn),
                new MySqlParameter("@timeOut", timeOut),
                new MySqlParameter("@attId", attId));

            LoadAttendance();
            MessageBox.Show("Attendance updated successfully.");

            int empId = Convert.ToInt32(dgvAttendance.CurrentRow.Cells["Employee ID"].Value);
            RebuildPayrollSlipForEmployee(activePayrollId, empId);
            LoadAttendance();
            LoadPayrollSlipRecord();
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

            int empId = Convert.ToInt32(dgvAttendance.CurrentRow.Cells["Employee ID"].Value);
            RebuildPayrollSlipForEmployee(activePayrollId, empId);
            LoadAttendance();
            LoadPayrollSlipRecord();
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

            if (!DateTime.TryParse(dateText, out DateTime otDate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }

            if (!HasActivePeriod)
{
                MessageBox.Show("Please select a payroll period first.");
                return;
            }

            if (!IsDateWithinActivePeriod(otDate))
            {
                MessageBox.Show($"OT date must be within the active period:\n{activePeriodLabel}");
                return;
            }

            if (!HasActivePeriod)
            {
                MessageBox.Show("Please select a payroll period first.");
                return;
            }

            if (!IsDateWithinActivePeriod(otDate))
            {
                MessageBox.Show($"OT date must be within the active period:\n{activePeriodLabel}");
                return;
            }

            if (!decimal.TryParse(hoursText, out decimal hours))
            {
                MessageBox.Show("Invalid hours value.");
                return;
            }

            string query = @"
        INSERT INTO overtime (emp_id, ot_date, hours)
        VALUES (@empId, @otDate, @hours)";

            ExecuteNonQuery(query,
                new MySqlParameter("@empId", SelectedEmpId),
                new MySqlParameter("@otDate", otDate.Date),
                new MySqlParameter("@hours", hours));

            RebuildPayrollSlipForEmployee(activePayrollId, SelectedEmpId);
            LoadOvertime();
            LoadPayrollSlipRecord();
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
            string currentDate = dgvOvertime.CurrentRow.Cells["OT Date"].Value?.ToString() ?? "";
            string currentHours = dgvOvertime.CurrentRow.Cells["Hours"].Value?.ToString() ?? "";

            string newDate = Prompt.ShowDialog("Edit OT date (yyyy-MM-dd):", "Edit Overtime", currentDate);
            if (string.IsNullOrWhiteSpace(newDate)) return;

            string newHours = Prompt.ShowDialog("Edit OT hours:", "Edit Overtime", currentHours);
            if (string.IsNullOrWhiteSpace(newHours)) return;

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

            string query = @"
        UPDATE overtime
        SET ot_date = @otDate,
            hours = @hours
        WHERE ot_id = @otId";

            ExecuteNonQuery(query,
                new MySqlParameter("@otDate", otDate.Date),
                new MySqlParameter("@hours", hours),
                new MySqlParameter("@otId", otId));

            LoadOvertime();
            MessageBox.Show("Overtime updated successfully.");

            int empId = Convert.ToInt32(dgvOvertime.CurrentRow.Cells["Employee ID"].Value);
            RebuildPayrollSlipForEmployee(activePayrollId, empId);
            LoadOvertime();
            LoadPayrollSlipRecord();
        }

        private void btnDeleteOT_Click(object sender, EventArgs e)
        {
            if (dgvOvertime.CurrentRow == null)
            {
                MessageBox.Show("Please select an overtime record.");
                return;
            }

            int empId = Convert.ToInt32(dgvOvertime.CurrentRow.Cells["Employee ID"].Value);
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

            RebuildPayrollSlipForEmployee(activePayrollId, empId);
            LoadOvertime();
            LoadPayrollSlipRecord();
        }

        #endregion

        #region Deduction CRUD

        private void btnAddDeduction_Click(object sender, EventArgs e)
        {
            if (!HasActivePeriod)
            {
                MessageBox.Show("Please select a payroll period first.",
                    "No Period Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SelectedEmpId == 0)
            {
                MessageBox.Show("Please select an employee first.",
                    "No Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int payRecId = EnsurePayrollSlipRecord(activePayrollId, SelectedEmpId);

            string dedType = Prompt.ShowDialog("Enter deduction type:", "Add Deduction");
            if (string.IsNullOrWhiteSpace(dedType)) return;

            string amountText = Prompt.ShowDialog("Enter deduction amount:", "Add Deduction");
            if (string.IsNullOrWhiteSpace(amountText)) return;

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                MessageBox.Show("Invalid amount value.");
                return;
            }

            string query = @"
        INSERT INTO payroll_deduction_items (pay_rec_id, ded_type, amount)
        VALUES (@payRecId, @dedType, @amount)";

            ExecuteNonQuery(query,
                new MySqlParameter("@payRecId", payRecId),
                new MySqlParameter("@dedType", dedType),
                new MySqlParameter("@amount", amount));

            RecalculatePayrollRecord(payRecId);
            LoadDeductions();
            LoadPayrollSlipRecord();

            MessageBox.Show("Deduction added successfully.");
        }

        private void btnEditDeduction_Click(object sender, EventArgs e)
        {
            if (dgvDeduction.CurrentRow == null)
            {
                MessageBox.Show("Please select a deduction record.");
                return;
            }

            int dedId = Convert.ToInt32(dgvDeduction.CurrentRow.Cells["ID"].Value);
            string currentType = dgvDeduction.CurrentRow.Cells["Deduction"].Value?.ToString() ?? "";
            string currentAmount = dgvDeduction.CurrentRow.Cells["Amount"].Value?.ToString() ?? "";

            string newType = Prompt.ShowDialog("Edit deduction type:", "Edit Deduction", currentType);
            if (string.IsNullOrWhiteSpace(newType)) return;

            string newAmount = Prompt.ShowDialog("Edit deduction amount:", "Edit Deduction", currentAmount);
            if (string.IsNullOrWhiteSpace(newAmount)) return;

            if (!decimal.TryParse(newAmount, out decimal amount))
            {
                MessageBox.Show("Invalid amount value.");
                return;
            }

            int payRecId = Convert.ToInt32(
                ExecuteScalar("SELECT pay_rec_id FROM payroll_deduction_items WHERE ded_id = @dedId",
                    new MySqlParameter("@dedId", dedId))
            );

            string query = @"
        UPDATE payroll_deduction_items
        SET ded_type = @dedType,
            amount = @amount
        WHERE ded_id = @dedId";

            ExecuteNonQuery(query,
                new MySqlParameter("@dedType", newType),
                new MySqlParameter("@amount", amount),
                new MySqlParameter("@dedId", dedId));

            RecalculatePayrollRecord(payRecId);
            LoadDeductions();
            LoadPayrollSlipRecord();

            MessageBox.Show("Deduction updated successfully.");
        }

        private void btnDeleteDeduction_Click(object sender, EventArgs e)
        {
            if (dgvDeduction.CurrentRow == null)
            {
                MessageBox.Show("Please select a deduction record.");
                return;
            }

            int dedId = Convert.ToInt32(dgvDeduction.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this deduction record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            int payRecId = Convert.ToInt32(
                ExecuteScalar("SELECT pay_rec_id FROM payroll_deduction_items WHERE ded_id = @dedId",
                    new MySqlParameter("@dedId", dedId))
            );

            string query = "DELETE FROM payroll_deduction_items WHERE ded_id = @dedId";

            ExecuteNonQuery(query, new MySqlParameter("@dedId", dedId));

            RecalculatePayrollRecord(payRecId);
            LoadDeductions();
            LoadPayrollSlipRecord();

            MessageBox.Show("Deduction deleted successfully.");
        }

        #endregion

        #region EnsurePayrollPeriod
        private int EnsurePayrollSlipRecord(int payrollId, int empId)
        {
            object existing = ExecuteScalar(@"
        SELECT pay_rec_id
        FROM payroll_slip_record
        WHERE payroll_id = @payrollId
          AND emp_id = @empId
        LIMIT 1",
                new MySqlParameter("@payrollId", payrollId),
                new MySqlParameter("@empId", empId));

            if (existing != null && existing != DBNull.Value)
                return Convert.ToInt32(existing);

            using (MySqlConnection con = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(@"
        INSERT INTO payroll_slip_record (payroll_id, emp_id, gross_pay, total_deduction, net_pay)
        VALUES (@payrollId, @empId, 0, 0, 0);
        SELECT LAST_INSERT_ID();", con))
            {
                cmd.Parameters.AddWithValue("@payrollId", payrollId);
                cmd.Parameters.AddWithValue("@empId", empId);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int EnsureEarningItem(int payRecId, string earnType)
        {
            object existing = ExecuteScalar(@"
        SELECT earning_id
        FROM payroll_earning_items
        WHERE pay_rec_id = @payRecId
          AND earn_type = @earnType
        LIMIT 1",
                new MySqlParameter("@payRecId", payRecId),
                new MySqlParameter("@earnType", earnType));

            if (existing != null && existing != DBNull.Value)
                return Convert.ToInt32(existing);

            using (MySqlConnection con = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(@"
        INSERT INTO payroll_earning_items (pay_rec_id, earn_type, amount)
        VALUES (@payRecId, @earnType, 0);
        SELECT LAST_INSERT_ID();", con))
            {
                cmd.Parameters.AddWithValue("@payRecId", payRecId);
                cmd.Parameters.AddWithValue("@earnType", earnType);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        private void EnsureDefaultPayrollItems(int payrollId, int empId)
        {
            int payRecId = EnsurePayrollSlipRecord(payrollId, empId);
            EnsureEarningItem(payRecId, "Basic Pay");
            EnsureEarningItem(payRecId, "Overtime Pay");
        }

        #endregion

        private decimal GetEmployeeSemiMonthlyBasicPay(int empId)
        {
            object result = ExecuteScalar(@"
        SELECT COALESCE(p.basic_salary, 0)
        FROM employees e
        INNER JOIN positions p ON e.pos_id = p.pos_id
        WHERE e.emp_id = @empId
        LIMIT 1",
                new MySqlParameter("@empId", empId));

            decimal monthlySalary = result != null && result != DBNull.Value
                ? Convert.ToDecimal(result)
                : 0m;

            return monthlySalary / 2m;
        }

        private decimal GetEmployeeOvertimeHoursForActivePeriod(int empId)
        {
            if (!HasActivePeriod)
                return 0m;

            object result = ExecuteScalar(@"
        SELECT COALESCE(SUM(hours), 0)
        FROM overtime
        WHERE emp_id = @empId
          AND ot_date BETWEEN @periodStart AND @periodEnd",
                new MySqlParameter("@empId", empId),
                new MySqlParameter("@periodStart", activePeriodStart),
                new MySqlParameter("@periodEnd", activePeriodEnd));

            return result != null && result != DBNull.Value
                ? Convert.ToDecimal(result)
                : 0m;
        }

        private void UpsertEarningItemAmount(int payRecId, string earnType, decimal amount)
        {
            object existing = ExecuteScalar(@"
        SELECT earning_id
        FROM payroll_earning_items
        WHERE pay_rec_id = @payRecId
          AND earn_type = @earnType
        LIMIT 1",
                new MySqlParameter("@payRecId", payRecId),
                new MySqlParameter("@earnType", earnType));

            if (existing != null && existing != DBNull.Value)
            {
                ExecuteNonQuery(@"
            UPDATE payroll_earning_items
            SET amount = @amount
            WHERE earning_id = @earningId",
                    new MySqlParameter("@amount", amount),
                    new MySqlParameter("@earningId", Convert.ToInt32(existing)));
            }
            else
            {
                ExecuteNonQuery(@"
            INSERT INTO payroll_earning_items (pay_rec_id, earn_type, amount)
            VALUES (@payRecId, @earnType, @amount)",
                    new MySqlParameter("@payRecId", payRecId),
                    new MySqlParameter("@earnType", earnType),
                    new MySqlParameter("@amount", amount));
            }
        }
        private void RebuildPayrollSlipForEmployee(int payrollId, int empId)
        {
            if (payrollId <= 0 || empId <= 0)
                return;

            int payRecId = EnsurePayrollSlipRecord(payrollId, empId);

            decimal basicPay = GetEmployeeSemiMonthlyBasicPay(empId);
            decimal otHours = GetEmployeeOvertimeHoursForActivePeriod(empId);
            decimal overtimePay = otHours * OVERTIME_RATE_PER_HOUR;

            UpsertEarningItemAmount(payRecId, "Basic Pay", basicPay);
            UpsertEarningItemAmount(payRecId, "Overtime Pay", overtimePay);

            RecalculatePayrollRecord(payRecId);
        }
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