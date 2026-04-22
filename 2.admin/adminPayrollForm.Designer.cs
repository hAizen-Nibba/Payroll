namespace Payroll__C__
{
    partial class adminPayrollForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label3 = new Label();
            label1 = new Label();
            cmbDept = new ComboBox();
            cmbPos = new ComboBox();
            cmbName = new ComboBox();
            label2 = new Label();
            dgvOvertime = new DataGridView();
            dgvDeduction = new DataGridView();
            btnFilter = new Button();
            btnClear = new Button();
            btnRefresh = new Button();
            tabControl = new TabControl();
            tabPeriods = new TabPage();
            tabAttendance = new TabPage();
            btnDeleteAttendance = new Button();
            btnEditAttendance = new Button();
            btnAddAttendance = new Button();
            dgvAttendance = new DataGridView();
            tabOvertime = new TabPage();
            btnDeleteOT = new Button();
            btnEditOT = new Button();
            btnAddOT = new Button();
            tabDeductions = new TabPage();
            btnDeleteDeduction = new Button();
            btnEditDeduction = new Button();
            btnAddDeduction = new Button();
            tabPayrollSlipRecord = new TabPage();
            dgvPayrollSlipRecord = new DataGridView();
            dgvPeriods = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvOvertime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDeduction).BeginInit();
            tabControl.SuspendLayout();
            tabPeriods.SuspendLayout();
            tabAttendance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
            tabOvertime.SuspendLayout();
            tabDeductions.SuspendLayout();
            tabPayrollSlipRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayrollSlipRecord).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPeriods).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(208, 8);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 13;
            label3.Text = "Position";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 8);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 15;
            label1.Text = "Department";
            // 
            // cmbDept
            // 
            cmbDept.Font = new Font("Segoe UI", 12F);
            cmbDept.FormattingEnabled = true;
            cmbDept.Location = new Point(8, 32);
            cmbDept.Name = "cmbDept";
            cmbDept.Size = new Size(184, 29);
            cmbDept.TabIndex = 16;
            // 
            // cmbPos
            // 
            cmbPos.Font = new Font("Segoe UI", 12F);
            cmbPos.FormattingEnabled = true;
            cmbPos.Location = new Point(208, 32);
            cmbPos.Name = "cmbPos";
            cmbPos.Size = new Size(184, 29);
            cmbPos.TabIndex = 17;
            // 
            // cmbName
            // 
            cmbName.Font = new Font("Segoe UI", 12F);
            cmbName.FormattingEnabled = true;
            cmbName.Location = new Point(408, 32);
            cmbName.Name = "cmbName";
            cmbName.Size = new Size(184, 29);
            cmbName.TabIndex = 19;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(408, 8);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 18;
            label2.Text = "Name";
            // 
            // dgvOvertime
            // 
            dgvOvertime.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOvertime.Location = new Point(8, 8);
            dgvOvertime.Name = "dgvOvertime";
            dgvOvertime.Size = new Size(952, 280);
            dgvOvertime.TabIndex = 24;
            // 
            // dgvDeduction
            // 
            dgvDeduction.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDeduction.Location = new Point(8, 8);
            dgvDeduction.Name = "dgvDeduction";
            dgvDeduction.Size = new Size(952, 280);
            dgvDeduction.TabIndex = 26;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(808, 16);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(64, 48);
            btnFilter.TabIndex = 30;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(880, 40);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(104, 24);
            btnClear.TabIndex = 31;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(880, 16);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(104, 24);
            btnRefresh.TabIndex = 32;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPeriods);
            tabControl.Controls.Add(tabAttendance);
            tabControl.Controls.Add(tabOvertime);
            tabControl.Controls.Add(tabDeductions);
            tabControl.Controls.Add(tabPayrollSlipRecord);
            tabControl.Location = new Point(8, 72);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(976, 440);
            tabControl.TabIndex = 34;
            // 
            // tabPeriods
            // 
            tabPeriods.Controls.Add(dgvPeriods);
            tabPeriods.Location = new Point(4, 24);
            tabPeriods.Name = "tabPeriods";
            tabPeriods.Padding = new Padding(3);
            tabPeriods.Size = new Size(968, 412);
            tabPeriods.TabIndex = 4;
            tabPeriods.Text = "Periods";
            tabPeriods.UseVisualStyleBackColor = true;
            // 
            // tabAttendance
            // 
            tabAttendance.Controls.Add(btnDeleteAttendance);
            tabAttendance.Controls.Add(btnEditAttendance);
            tabAttendance.Controls.Add(btnAddAttendance);
            tabAttendance.Controls.Add(dgvAttendance);
            tabAttendance.Location = new Point(4, 24);
            tabAttendance.Name = "tabAttendance";
            tabAttendance.Padding = new Padding(3);
            tabAttendance.Size = new Size(968, 412);
            tabAttendance.TabIndex = 0;
            tabAttendance.Text = "Attendance";
            tabAttendance.UseVisualStyleBackColor = true;
            // 
            // btnDeleteAttendance
            // 
            btnDeleteAttendance.Location = new Point(816, 360);
            btnDeleteAttendance.Name = "btnDeleteAttendance";
            btnDeleteAttendance.Size = new Size(144, 40);
            btnDeleteAttendance.TabIndex = 25;
            btnDeleteAttendance.Text = "Delete";
            btnDeleteAttendance.UseVisualStyleBackColor = true;
            // 
            // btnEditAttendance
            // 
            btnEditAttendance.Location = new Point(160, 360);
            btnEditAttendance.Name = "btnEditAttendance";
            btnEditAttendance.Size = new Size(144, 40);
            btnEditAttendance.TabIndex = 24;
            btnEditAttendance.Text = "Edit";
            btnEditAttendance.UseVisualStyleBackColor = true;
            // 
            // btnAddAttendance
            // 
            btnAddAttendance.Location = new Point(8, 360);
            btnAddAttendance.Name = "btnAddAttendance";
            btnAddAttendance.Size = new Size(144, 40);
            btnAddAttendance.TabIndex = 23;
            btnAddAttendance.Text = "Add";
            btnAddAttendance.UseVisualStyleBackColor = true;
            // 
            // dgvAttendance
            // 
            dgvAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAttendance.Location = new Point(8, 8);
            dgvAttendance.Name = "dgvAttendance";
            dgvAttendance.Size = new Size(952, 280);
            dgvAttendance.TabIndex = 22;
            // 
            // tabOvertime
            // 
            tabOvertime.Controls.Add(btnDeleteOT);
            tabOvertime.Controls.Add(btnEditOT);
            tabOvertime.Controls.Add(btnAddOT);
            tabOvertime.Controls.Add(dgvOvertime);
            tabOvertime.Location = new Point(4, 24);
            tabOvertime.Name = "tabOvertime";
            tabOvertime.Padding = new Padding(3);
            tabOvertime.Size = new Size(968, 412);
            tabOvertime.TabIndex = 1;
            tabOvertime.Text = "Overtime";
            tabOvertime.UseVisualStyleBackColor = true;
            // 
            // btnDeleteOT
            // 
            btnDeleteOT.Location = new Point(816, 360);
            btnDeleteOT.Name = "btnDeleteOT";
            btnDeleteOT.Size = new Size(144, 40);
            btnDeleteOT.TabIndex = 28;
            btnDeleteOT.Text = "Delete";
            btnDeleteOT.UseVisualStyleBackColor = true;
            // 
            // btnEditOT
            // 
            btnEditOT.Location = new Point(160, 360);
            btnEditOT.Name = "btnEditOT";
            btnEditOT.Size = new Size(144, 40);
            btnEditOT.TabIndex = 27;
            btnEditOT.Text = "Edit";
            btnEditOT.UseVisualStyleBackColor = true;
            // 
            // btnAddOT
            // 
            btnAddOT.Location = new Point(8, 360);
            btnAddOT.Name = "btnAddOT";
            btnAddOT.Size = new Size(144, 40);
            btnAddOT.TabIndex = 26;
            btnAddOT.Text = "Add";
            btnAddOT.UseVisualStyleBackColor = true;
            // 
            // tabDeductions
            // 
            tabDeductions.Controls.Add(btnDeleteDeduction);
            tabDeductions.Controls.Add(btnEditDeduction);
            tabDeductions.Controls.Add(btnAddDeduction);
            tabDeductions.Controls.Add(dgvDeduction);
            tabDeductions.Location = new Point(4, 24);
            tabDeductions.Name = "tabDeductions";
            tabDeductions.Padding = new Padding(3);
            tabDeductions.Size = new Size(968, 412);
            tabDeductions.TabIndex = 2;
            tabDeductions.Text = "Deductions";
            tabDeductions.UseVisualStyleBackColor = true;
            // 
            // btnDeleteDeduction
            // 
            btnDeleteDeduction.Location = new Point(816, 360);
            btnDeleteDeduction.Name = "btnDeleteDeduction";
            btnDeleteDeduction.Size = new Size(144, 40);
            btnDeleteDeduction.TabIndex = 29;
            btnDeleteDeduction.Text = "Delete";
            btnDeleteDeduction.UseVisualStyleBackColor = true;
            // 
            // btnEditDeduction
            // 
            btnEditDeduction.Location = new Point(160, 360);
            btnEditDeduction.Name = "btnEditDeduction";
            btnEditDeduction.Size = new Size(144, 40);
            btnEditDeduction.TabIndex = 28;
            btnEditDeduction.Text = "Edit";
            btnEditDeduction.UseVisualStyleBackColor = true;
            // 
            // btnAddDeduction
            // 
            btnAddDeduction.Location = new Point(8, 360);
            btnAddDeduction.Name = "btnAddDeduction";
            btnAddDeduction.Size = new Size(144, 40);
            btnAddDeduction.TabIndex = 27;
            btnAddDeduction.Text = "Add";
            btnAddDeduction.UseVisualStyleBackColor = true;
            // 
            // tabPayrollSlipRecord
            // 
            tabPayrollSlipRecord.Controls.Add(dgvPayrollSlipRecord);
            tabPayrollSlipRecord.Location = new Point(4, 24);
            tabPayrollSlipRecord.Name = "tabPayrollSlipRecord";
            tabPayrollSlipRecord.Padding = new Padding(3);
            tabPayrollSlipRecord.Size = new Size(968, 412);
            tabPayrollSlipRecord.TabIndex = 3;
            tabPayrollSlipRecord.Text = "Payroll Slip Record";
            tabPayrollSlipRecord.UseVisualStyleBackColor = true;
            // 
            // dgvPayrollSlipRecord
            // 
            dgvPayrollSlipRecord.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPayrollSlipRecord.Location = new Point(8, 8);
            dgvPayrollSlipRecord.Name = "dgvPayrollSlipRecord";
            dgvPayrollSlipRecord.Size = new Size(952, 392);
            dgvPayrollSlipRecord.TabIndex = 0;
            // 
            // dgvPeriods
            // 
            dgvPeriods.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPeriods.Location = new Point(8, 8);
            dgvPeriods.Name = "dgvPeriods";
            dgvPeriods.Size = new Size(952, 392);
            dgvPeriods.TabIndex = 0;
            // 
            // adminPayrollForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(992, 521);
            Controls.Add(tabControl);
            Controls.Add(btnRefresh);
            Controls.Add(btnClear);
            Controls.Add(btnFilter);
            Controls.Add(cmbName);
            Controls.Add(label2);
            Controls.Add(cmbPos);
            Controls.Add(cmbDept);
            Controls.Add(label1);
            Controls.Add(label3);
            FormBorderStyle = FormBorderStyle.None;
            Name = "adminPayrollForm";
            Text = "adminPayrollForm";
            ((System.ComponentModel.ISupportInitialize)dgvOvertime).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDeduction).EndInit();
            tabControl.ResumeLayout(false);
            tabPeriods.ResumeLayout(false);
            tabAttendance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
            tabOvertime.ResumeLayout(false);
            tabDeductions.ResumeLayout(false);
            tabPayrollSlipRecord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPayrollSlipRecord).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPeriods).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Label label1;
        private ComboBox cmbDept;
        private ComboBox cmbPos;
        private ComboBox cmbName;
        private Label label2;
        private DataGridView dgvOvertime;
        private DataGridView dgvDeduction;
        private Button btnFilter;
        private Button btnClear;
        private Button btnRefresh;
        private TabControl tabControl;
        private TabPage tabPeriods;
        private TabPage tabAttendance;
        private TabPage tabOvertime;
        private TabPage tabDeductions;
        private Button btnEditAttendance;
        private Button btnAddAttendance;
        private Button btnDeleteAttendance;
        private Button btnDeleteOT;
        private Button btnEditOT;
        private Button btnAddOT;
        private Button btnDeleteDeduction;
        private Button btnEditDeduction;
        private Button btnAddDeduction;
        private TabPage tabPayrollSlipRecord;
        private DataGridView dgvAttendance;
        private DataGridView dgvPayrollSlipRecord;
        private DataGridView dgvPeriods;
    }
}