using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Payroll__C__
{
    public partial class PreviewSlip : Form
    {
        private readonly string connectionString =
            "server=localhost;database=payrolldb_test;uid=root;pwd=;";

        private readonly int empId;
        private readonly int payrollId;
        private readonly string selectedPeriod;

        private PrintDocument printDocument1 = new PrintDocument();
        private PrintPreviewControl previewControl1 = new PrintPreviewControl();
        private Button btnPrint = new Button();

        private string employeeName = "";
        private string department = "";
        private string position = "";
        private string payDate = "";
        private decimal monthlySalary = 0m;
        private decimal basicPay = 0m;
        private decimal additionalEarnings = 0m;
        private decimal grossPay = 0m;
        private decimal totalDeductions = 0m;
        private decimal netPay = 0m;

        public PreviewSlip(int empId, string employeeName, int payrollId, string selectedPeriod)
        {
            InitializeComponent();

            this.empId = empId;
            this.employeeName = employeeName;
            this.payrollId = payrollId;
            this.selectedPeriod = selectedPeriod;

            SetupPreviewForm();
            LoadPayslipData();
        }

        private void SetupPreviewForm()
        {
            this.Text = "Payroll Slip Preview";
            this.WindowState = FormWindowState.Maximized;

            printDocument1.PrintPage += PrintDocument1_PrintPage;

            previewControl1.Document = printDocument1;
            previewControl1.Dock = DockStyle.Fill;
            previewControl1.Zoom = 1.0;
            previewControl1.AutoZoom = true;

            btnPrint.Text = "Print";
            btnPrint.Height = 40;
            btnPrint.Dock = DockStyle.Top;
            btnPrint.Click += BtnPrint_Click;

            Controls.Add(previewControl1);
            Controls.Add(btnPrint);
        }

        private void LoadPayslipData()
        {
            LoadEmployeeInfo();
            LoadPayrollAmounts();

            // Refresh preview after data is loaded
            previewControl1.InvalidatePreview();
        }

        private void LoadEmployeeInfo()
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
                cmd.Parameters.AddWithValue("@emp_id", empId);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        department = reader["dept_name"]?.ToString() ?? "";
                        position = reader["pos_name"]?.ToString() ?? "";
                    }
                }
            }
        }

        private void LoadPayrollAmounts()
        {
            payDate = selectedPeriod;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // 1) Monthly salary from position
                string monthlySalaryQuery = @"
            SELECT COALESCE(p.basic_salary, 0)
            FROM employees e
            INNER JOIN positions p ON e.pos_id = p.pos_id
            WHERE e.emp_id = @emp_id
            LIMIT 1;";

                using (MySqlCommand cmd = new MySqlCommand(monthlySalaryQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@emp_id", empId);
                    object result = cmd.ExecuteScalar();
                    monthlySalary = result != null && result != DBNull.Value
                        ? Convert.ToDecimal(result)
                        : 0m;
                }

                // 2) Basic Pay only
                string basicPayQuery = @"
            SELECT COALESCE(SUM(pei.amount), 0)
            FROM payroll_earning_items pei
            INNER JOIN payroll_slip_record psr ON pei.pay_rec_id = psr.pay_rec_id
            WHERE psr.emp_id = @emp_id
              AND psr.payroll_id = @payroll_id
              AND pei.earn_type = 'Basic Pay';";

                using (MySqlCommand cmd = new MySqlCommand(basicPayQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@emp_id", empId);
                    cmd.Parameters.AddWithValue("@payroll_id", payrollId);
                    object result = cmd.ExecuteScalar();
                    basicPay = result != null && result != DBNull.Value
                        ? Convert.ToDecimal(result)
                        : 0m;
                }

                // 3) Additional earnings only (exclude Basic Pay)
                string additionalEarningsQuery = @"
            SELECT COALESCE(SUM(pei.amount), 0)
            FROM payroll_earning_items pei
            INNER JOIN payroll_slip_record psr ON pei.pay_rec_id = psr.pay_rec_id
            WHERE psr.emp_id = @emp_id
              AND psr.payroll_id = @payroll_id
              AND pei.earn_type <> 'Basic Pay';";

                using (MySqlCommand cmd = new MySqlCommand(additionalEarningsQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@emp_id", empId);
                    cmd.Parameters.AddWithValue("@payroll_id", payrollId);
                    object result = cmd.ExecuteScalar();
                    additionalEarnings = result != null && result != DBNull.Value
                        ? Convert.ToDecimal(result)
                        : 0m;
                }

                // 4) Gross pay, deductions, net pay from payroll_slip_record
                string payrollSummaryQuery = @"
            SELECT 
                COALESCE(psr.gross_pay, 0),
                COALESCE(psr.total_deduction, 0),
                COALESCE(psr.net_pay, 0)
            FROM payroll_slip_record psr
            WHERE psr.emp_id = @emp_id
              AND psr.payroll_id = @payroll_id
            LIMIT 1;";

                using (MySqlCommand cmd = new MySqlCommand(payrollSummaryQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@emp_id", empId);
                    cmd.Parameters.AddWithValue("@payroll_id", payrollId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            grossPay = Convert.ToDecimal(reader[0]);
                            totalDeductions = Convert.ToDecimal(reader[1]);
                            netPay = Convert.ToDecimal(reader[2]);
                        }
                    }
                }
            }
        }

        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            using PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void PrintDocument1_PrintPage(object? sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            using Font titleFont = new Font("Arial", 18, FontStyle.Bold);
            using Font sectionFont = new Font("Arial", 12, FontStyle.Bold);
            using Font headerFont = new Font("Arial", 11, FontStyle.Bold);
            using Font bodyFont = new Font("Arial", 11, FontStyle.Regular);
            using Font netPayFont = new Font("Arial", 13, FontStyle.Bold);

            float left = 60;
            float right = 750;
            float top = 60;
            float y = top;

            g.DrawString("PAYROLL SLIP", titleFont, Brushes.Black, left + 220, y);
            y += 45;

            g.DrawLine(Pens.Black, left, y, right, y);
            y += 25;

            g.DrawString("Employee Name:", headerFont, Brushes.Black, left, y);
            g.DrawString(employeeName, bodyFont, Brushes.Black, left + 160, y);
            y += 28;

            g.DrawString("Department:", headerFont, Brushes.Black, left, y);
            g.DrawString(department, bodyFont, Brushes.Black, left + 160, y);
            y += 28;

            g.DrawString("Position:", headerFont, Brushes.Black, left, y);
            g.DrawString(position, bodyFont, Brushes.Black, left + 160, y);
            y += 28;

            g.DrawString("Payroll Period:", headerFont, Brushes.Black, left, y);
            g.DrawString(payDate, bodyFont, Brushes.Black, left + 160, y);
            y += 40;

            g.DrawLine(Pens.Black, left, y, right, y);
            y += 20;

            g.DrawString("Salary Details", sectionFont, Brushes.Black, left, y);
            y += 30;

            g.DrawString("Monthly Salary:", headerFont, Brushes.Black, left, y);
            g.DrawString("₱" + monthlySalary.ToString("N2"), bodyFont, Brushes.Black, left + 160, y);
            y += 28;

            g.DrawString("Basic Pay:", headerFont, Brushes.Black, left, y);
            g.DrawString("₱" + basicPay.ToString("N2"), bodyFont, Brushes.Black, left + 160, y);
            y += 28;

            g.DrawString("Additional Earnings:", headerFont, Brushes.Black, left, y);
            g.DrawString("₱" + additionalEarnings.ToString("N2"), bodyFont, Brushes.Black, left + 160, y);
            y += 28;

            g.DrawString("Gross Pay:", headerFont, Brushes.Black, left, y);
            g.DrawString("₱" + grossPay.ToString("N2"), bodyFont, Brushes.Black, left + 160, y);
            y += 28;

            g.DrawString("Total Deductions:", headerFont, Brushes.Black, left, y);
            g.DrawString("₱" + totalDeductions.ToString("N2"), bodyFont, Brushes.Black, left + 160, y);
            y += 35;

            g.DrawLine(Pens.Black, left, y, right, y);
            y += 20;

            g.DrawString("Net Pay:", headerFont, Brushes.Black, left, y);
            g.DrawString("₱" + netPay.ToString("N2"), netPayFont, Brushes.Black, left + 160, y);
            y += 45;

            g.DrawLine(Pens.Black, left, y, right, y);
            y += 30;

            g.DrawString("This is a system-generated payroll slip.", bodyFont, Brushes.Black, left, y);
        }
    }
}