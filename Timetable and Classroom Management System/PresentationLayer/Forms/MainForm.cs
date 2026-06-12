using Timetable_and_Classroom_Management_System.BusinessLayer;

namespace Timetable_and_Classroom_Management_System.PresentationLayer.Forms
{
    public partial class MainForm : Form
    {
        private readonly BranchService _branchService = new BranchService();

        private int selectedBranchId = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadBranches();
        }
        private void ClearBranchInputs()
        {
            selectedBranchId = 0;
            txtBranchName.Clear();
            dgvBranches.ClearSelection();
            txtBranchName.Focus();
        }
        private void LoadBranches()
        {
            dgvBranches.DataSource = _branchService.GetAllBranches()
                .Select(b => new
                {
                    b.BranchID,
                    b.BranchName
                })
                .ToList();

            dgvBranches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBranches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBranches.ReadOnly = true;
            dgvBranches.ClearSelection();
        }

      

        private void dgvBranches_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            selectedBranchId = Convert.ToInt32(
                dgvBranches.Rows[e.RowIndex].Cells["BranchID"].Value
            );

            txtBranchName.Text =
                dgvBranches.Rows[e.RowIndex].Cells["BranchName"].Value?.ToString() ?? string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _branchService.AddBranch(txtBranchName.Text);

                MessageBox.Show("Branch added successfully.");

                LoadBranches();
                ClearBranchInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _branchService.UpdateBranch(selectedBranchId, txtBranchName.Text);

                MessageBox.Show("Branch updated successfully.");

                LoadBranches();
                ClearBranchInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this branch?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    _branchService.DeleteBranch(selectedBranchId);

                    MessageBox.Show("Branch deleted successfully.");

                    LoadBranches();
                    ClearBranchInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearBranchInputs();
        }

        private void dgvBranches_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}