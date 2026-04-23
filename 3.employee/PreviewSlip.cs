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
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Fonts
            using Font titleFont = new Font("Segoe UI", 20, FontStyle.Bold);
            using Font subtitleFont = new Font("Segoe UI", 10, FontStyle.Regular);
            using Font sectionTitleFont = new Font("Segoe UI", 11, FontStyle.Bold);
            using Font labelFont = new Font("Segoe UI", 10, FontStyle.Bold);
            using Font valueFont = new Font("Segoe UI", 10, FontStyle.Regular);
            using Font netPayLabelFont = new Font("Segoe UI", 11, FontStyle.Bold);
            using Font netPayValueFont = new Font("Segoe UI", 18, FontStyle.Bold);
            using Font footerFont = new Font("Segoe UI", 9, FontStyle.Italic);

            // Brushes / Pens
            using Brush blueBrush = new SolidBrush(Color.FromArgb(33, 150, 243));
            using Brush darkBrush = new SolidBrush(Color.FromArgb(45, 45, 45));
            using Brush grayBrush = new SolidBrush(Color.FromArgb(110, 110, 110));
            using Brush lightGrayBrush = new SolidBrush(Color.FromArgb(245, 247, 250));
            using Brush whiteBrush = new SolidBrush(Color.White);
            using Brush greenBrush = new SolidBrush(Color.FromArgb(46, 125, 50));

            using Pen borderPen = new Pen(Color.FromArgb(210, 210, 210), 1);
            using Pen lightPen = new Pen(Color.FromArgb(225, 225, 225), 1);

            // Page/card bounds
            float pageLeft = 80;
            float pageTop = 50;
            float pageWidth = 650;
            float pageHeight = 900;

            RectangleF cardRect = new RectangleF(pageLeft, pageTop, pageWidth, pageHeight);
            RectangleF headerRect = new RectangleF(pageLeft, pageTop, pageWidth, 110);

            // Card background
            g.FillRectangle(whiteBrush, cardRect);
            g.DrawRectangle(borderPen, cardRect.X, cardRect.Y, cardRect.Width, cardRect.Height);

            // Header
            g.FillRectangle(blueBrush, headerRect);
            g.DrawString("PAYROLL SLIP", titleFont, whiteBrush, pageLeft + 25, pageTop + 20);
            g.DrawString("Employee payroll summary", subtitleFont, whiteBrush, pageLeft + 27, pageTop + 60);

            // Net pay box at top-right
            RectangleF netTopBox = new RectangleF(pageLeft + pageWidth - 200, pageTop + 20, 160, 60);
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), netTopBox);
            g.DrawRectangle(lightPen, netTopBox.X, netTopBox.Y, netTopBox.Width, netTopBox.Height);
            g.DrawString("NET PAY", new Font("Segoe UI", 9, FontStyle.Bold), darkBrush, netTopBox.X + 12, netTopBox.Y + 8);
            g.DrawString("₱" + netPay.ToString("N2"), new Font("Segoe UI", 14, FontStyle.Bold), greenBrush, netTopBox.X + 12, netTopBox.Y + 28);

            float contentLeft = pageLeft + 25;
            float contentRight = pageLeft + pageWidth - 25;
            float y = pageTop + 135;

            // Employee info section
            g.DrawString("Employee Information", sectionTitleFont, darkBrush, contentLeft, y);
            y += 18;
            g.DrawLine(lightPen, contentLeft, y, contentRight, y);
            y += 15;

            DrawInfoRow(g, "Employee Name", employeeName, contentLeft, y, labelFont, valueFont, darkBrush);
            y += 28;
            DrawInfoRow(g, "Department", department, contentLeft, y, labelFont, valueFont, darkBrush);
            y += 28;
            DrawInfoRow(g, "Position", position, contentLeft, y, labelFont, valueFont, darkBrush);
            y += 28;
            DrawInfoRow(g, "Payroll Period", payDate, contentLeft, y, labelFont, valueFont, darkBrush);
            y += 45;

            // Salary details box
            RectangleF salaryBox = new RectangleF(contentLeft, y, pageWidth - 50, 220);
            g.FillRectangle(lightGrayBrush, salaryBox);
            g.DrawRectangle(borderPen, salaryBox.X, salaryBox.Y, salaryBox.Width, salaryBox.Height);

            float sy = y + 15;
            g.DrawString("Salary Details", sectionTitleFont, darkBrush, contentLeft + 15, sy);
            sy += 20;
            g.DrawLine(lightPen, contentLeft + 15, sy, contentRight - 15, sy);
            sy += 15;

            DrawAmountRow(g, "Monthly Salary", monthlySalary, contentLeft + 15, contentRight - 20, sy, labelFont, valueFont, darkBrush);
            sy += 28;
            DrawAmountRow(g, "Basic Pay", basicPay, contentLeft + 15, contentRight - 20, sy, labelFont, valueFont, darkBrush);
            sy += 28;
            DrawAmountRow(g, "Additional Earnings", additionalEarnings, contentLeft + 15, contentRight - 20, sy, labelFont, valueFont, darkBrush);
            sy += 28;
            DrawAmountRow(g, "Gross Pay", grossPay, contentLeft + 15, contentRight - 20, sy, labelFont, valueFont, darkBrush);
            sy += 28;
            DrawAmountRow(g, "Total Deductions", totalDeductions, contentLeft + 15, contentRight - 20, sy, labelFont, valueFont, darkBrush);

            y += 250;

            // Final net pay highlight
            RectangleF netBox = new RectangleF(contentLeft, y, pageWidth - 50, 80);
            g.FillRectangle(new SolidBrush(Color.FromArgb(232, 245, 233)), netBox);
            g.DrawRectangle(new Pen(Color.FromArgb(165, 214, 167), 1), netBox.X, netBox.Y, netBox.Width, netBox.Height);

            g.DrawString("Net Pay", netPayLabelFont, darkBrush, netBox.X + 18, netBox.Y + 16);
            SizeF netSize = g.MeasureString("₱" + netPay.ToString("N2"), netPayValueFont);
            g.DrawString("₱" + netPay.ToString("N2"), netPayValueFont, greenBrush,
                netBox.Right - netSize.Width - 18, netBox.Y + 14);

            y += 110;

            // Footer
            g.DrawString("This is a system-generated payroll slip.", footerFont, grayBrush, contentLeft, y);
        }

        private void DrawInfoRow(Graphics g, string label, string value, float x, float y,
            Font labelFont, Font valueFont, Brush textBrush)
        {
            g.DrawString(label + ":", labelFont, textBrush, x, y);
            g.DrawString(value, valueFont, textBrush, x + 150, y);
        }

        private void DrawAmountRow(Graphics g, string label, decimal amount, float left, float right, float y,
            Font labelFont, Font valueFont, Brush textBrush)
        {
            g.DrawString(label, labelFont, textBrush, left, y);

            string amountText = "₱" + amount.ToString("N2");
            SizeF size = g.MeasureString(amountText, valueFont);
            g.DrawString(amountText, valueFont, textBrush, right - size.Width, y);
        }
    }
}