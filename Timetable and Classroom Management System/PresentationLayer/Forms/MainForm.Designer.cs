namespace Timetable_and_Classroom_Management_System.PresentationLayer.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2Button btnRefresh = null!;
        private Label lblBranchCount = null!;
        private Label lblSubjectCount = null!;
        private Label lblClassroomCount = null!;
        private Label lblScheduleCount = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtBranchName = null!;
        private Guna.UI2.WinForms.Guna2Button btnBranchAdd = null!;
        private Guna.UI2.WinForms.Guna2Button btnBranchUpdate = null!;
        private Guna.UI2.WinForms.Guna2Button btnBranchDelete = null!;
        private Guna.UI2.WinForms.Guna2Button btnBranchClear = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvBranches = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtStudyYearName = null!;
        private Guna.UI2.WinForms.Guna2Button btnStudyYearAdd = null!;
        private Guna.UI2.WinForms.Guna2Button btnStudyYearUpdate = null!;
        private Guna.UI2.WinForms.Guna2Button btnStudyYearDelete = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvStudyYears = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtClassroomNumber = null!;
        private Guna.UI2.WinForms.Guna2NumericUpDown numClassroomCapacity = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbRoomType = null!;
        private Guna.UI2.WinForms.Guna2Button btnClassroomAdd = null!;
        private Guna.UI2.WinForms.Guna2Button btnClassroomUpdate = null!;
        private Guna.UI2.WinForms.Guna2Button btnClassroomDelete = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvClassrooms = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtFacultyName = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbAcademicTitle = null!;
        private Guna.UI2.WinForms.Guna2Button btnFacultyAdd = null!;
        private Guna.UI2.WinForms.Guna2Button btnFacultyUpdate = null!;
        private Guna.UI2.WinForms.Guna2Button btnFacultyDelete = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvFacultyMembers = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtSubjectName = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectYear = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectBranch = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectFilterYear = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectFilterBranch = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectFilterSemester = null!;
        private Guna.UI2.WinForms.Guna2NumericUpDown numSemester = null!;
        private Guna.UI2.WinForms.Guna2NumericUpDown numTheoreticalHours = null!;
        private Guna.UI2.WinForms.Guna2NumericUpDown numPracticalHours = null!;
        private Guna.UI2.WinForms.Guna2NumericUpDown numCreditUnits = null!;
        private Guna.UI2.WinForms.Guna2Button btnSubjectAdd = null!;
        private Guna.UI2.WinForms.Guna2Button btnSubjectUpdate = null!;
        private Guna.UI2.WinForms.Guna2Button btnSubjectDelete = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSubjects = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtSectionName = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSectionYear = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSectionBranch = null!;
        private Guna.UI2.WinForms.Guna2NumericUpDown numStudentCount = null!;
        private Guna.UI2.WinForms.Guna2Button btnSectionAdd = null!;
        private Guna.UI2.WinForms.Guna2Button btnSectionUpdate = null!;
        private Guna.UI2.WinForms.Guna2Button btnSectionDelete = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSections = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtStartTime = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtEndTime = null!;
        private Guna.UI2.WinForms.Guna2CheckBox chkIsBreak = null!;
        private Guna.UI2.WinForms.Guna2Button btnTimeSlotAdd = null!;
        private Guna.UI2.WinForms.Guna2Button btnTimeSlotUpdate = null!;
        private Guna.UI2.WinForms.Guna2Button btnTimeSlotDelete = null!;
        private Guna.UI2.WinForms.Guna2Button btnTimeSlotClear = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvTimeSlots = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbAssignFaculty = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbAssignSubject = null!;
        private Guna.UI2.WinForms.Guna2Button btnAssignmentAdd = null!;
        private Guna.UI2.WinForms.Guna2Button btnAssignmentRemove = null!;
        private Guna.UI2.WinForms.Guna2Button btnAssignmentClear = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvAssignments = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleSubject = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleFaculty = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleClassroom = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleTimeSlot = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleDay = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleYear = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleBranch = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleSection = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleFilterFaculty = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleFilterSection = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleFilterYear = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbScheduleFilterDay = null!;
        private Guna.UI2.WinForms.Guna2Button btnScheduleGenerate = null!;
        private Guna.UI2.WinForms.Guna2Button btnScheduleAdd = null!;
        private Guna.UI2.WinForms.Guna2Button btnScheduleUpdate = null!;
        private Guna.UI2.WinForms.Guna2Button btnScheduleDelete = null!;
        private Guna.UI2.WinForms.Guna2Button btnScheduleClear = null!;
        private Guna.UI2.WinForms.Guna2Button btnScheduleExport = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSchedules = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 820);
            MinimumSize = new Size(1080, 720);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Timetable and Classroom Management System";
            Load += MainForm_Load_1;
            ResumeLayout(false);
        }
    }
}