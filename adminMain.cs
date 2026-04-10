using System;
using System.Windows.Forms;

namespace Payroll__C__
{
    public partial class adminMain : Form
    {
        private Form activeForm = null;
        private bool isExiting = false;

        public adminMain()
        {
            InitializeComponent();

            this.FormClosing += adminMain_FormClosing;

            // Wire button events
            btnAttendance.Click += btnAttendance_Click;
            btnBack.Click += btnBack_Click;
            btnSQL.Click += btnSQL_Click;
            btnAddEmplyee.Click += btnAddEmplyee_Click;
        }

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContent.Controls.Clear();
            panelContent.Controls.Add(childForm);
            panelContent.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        private void btnAttendance_Click(object sender, EventArgs e) 
        {
            OpenChildForm(new adminPayrollForm());
        }

        private void btnAddEmplyee_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Create your AddEmployeeForm first, then load it here.");
            // Example later:
            // OpenChildForm(new AddEmployeeForm());
        }

        private void btnSQL_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This button is for MySQL access. Add your logic here.");
        }

        private bool ConfirmExit()
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (ConfirmExit())
            {
                isExiting = true;
                Application.Exit();
            }
        }

        private void adminMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExiting)
                return;

            if (!ConfirmExit())
            {
                e.Cancel = true;
                Application.Exit();
            }
        }
    }
}