namespace Timetable_and_Classroom_Management_System.PresentationLayer.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel rootLayout;
        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Label lblAppTitle;
        private Label lblAppSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2TabControl mainTabs;
        private TabPage tabDashboard;
        private TabPage tabBranches;
        private TabPage tabStudyYears;
        private TabPage tabClassrooms;
        private FlowLayoutPanel dashboardContentPanel;
        private Label lblBranchCount;
        private Label lblSubjectCount;
        private Label lblClassroomCount;
        private Label lblScheduleCount;
        private TableLayoutPanel branchesLayout;
        private Guna.UI2.WinForms.Guna2Panel branchesFormCard;
        private FlowLayoutPanel branchesFieldsPanel;
        private FlowLayoutPanel branchesButtonsPanel;
        private Guna.UI2.WinForms.Guna2TextBox txtBranchName;
        private Guna.UI2.WinForms.Guna2Button btnBranchAdd;
        private Guna.UI2.WinForms.Guna2Button btnBranchUpdate;
        private Guna.UI2.WinForms.Guna2Button btnBranchDelete;
        private Guna.UI2.WinForms.Guna2Button btnBranchClear;
        private Guna.UI2.WinForms.Guna2DataGridView dgvBranches;
        private TableLayoutPanel studyYearsLayout;
        private Guna.UI2.WinForms.Guna2Panel studyYearsFormCard;
        private FlowLayoutPanel studyYearsFieldsPanel;
        private FlowLayoutPanel studyYearsButtonsPanel;
        private Guna.UI2.WinForms.Guna2TextBox txtStudyYearName;
        private Guna.UI2.WinForms.Guna2Button btnStudyYearAdd;
        private Guna.UI2.WinForms.Guna2Button btnStudyYearUpdate;
        private Guna.UI2.WinForms.Guna2Button btnStudyYearDelete;
        private Guna.UI2.WinForms.Guna2DataGridView dgvStudyYears;
        private TableLayoutPanel classroomsLayout;
        private Guna.UI2.WinForms.Guna2Panel classroomsFormCard;
        private FlowLayoutPanel classroomsFieldsPanel;
        private FlowLayoutPanel classroomsButtonsPanel;
        private Guna.UI2.WinForms.Guna2TextBox txtClassroomNumber;
        private Guna.UI2.WinForms.Guna2NumericUpDown numClassroomCapacity;
        private Guna.UI2.WinForms.Guna2ComboBox cmbRoomType;
        private Guna.UI2.WinForms.Guna2Button btnClassroomAdd;
        private Guna.UI2.WinForms.Guna2Button btnClassroomUpdate;
        private Guna.UI2.WinForms.Guna2Button btnClassroomDelete;
        private Guna.UI2.WinForms.Guna2DataGridView dgvClassrooms;
        private TabPage tabFaculty;
        private TableLayoutPanel facultyLayout;
        private Guna.UI2.WinForms.Guna2Panel facultyFormCard;
        private FlowLayoutPanel facultyFieldsPanel;
        private FlowLayoutPanel facultyButtonsPanel;
        private Guna.UI2.WinForms.Guna2TextBox txtFacultyName;
        private Guna.UI2.WinForms.Guna2ComboBox cmbAcademicTitle;
        private Guna.UI2.WinForms.Guna2Button btnFacultyAdd;
        private Guna.UI2.WinForms.Guna2Button btnFacultyUpdate;
        private Guna.UI2.WinForms.Guna2Button btnFacultyDelete;
        private Guna.UI2.WinForms.Guna2DataGridView dgvFacultyMembers;
        private TabPage tabSubjects;
        private TableLayoutPanel subjectsLayout;
        private Guna.UI2.WinForms.Guna2Panel subjectsFormCard;
        private FlowLayoutPanel subjectsFieldsPanel;
        private FlowLayoutPanel subjectsButtonsPanel;
        private Guna.UI2.WinForms.Guna2TextBox txtSubjectName;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectYear;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectBranch;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectFilterYear;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectFilterBranch;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectFilterSemester;
        private Guna.UI2.WinForms.Guna2NumericUpDown numSemester;
        private Guna.UI2.WinForms.Guna2NumericUpDown numTheoreticalHours;
        private Guna.UI2.WinForms.Guna2NumericUpDown numPracticalHours;
        private Guna.UI2.WinForms.Guna2NumericUpDown numCreditUnits;
        private Guna.UI2.WinForms.Guna2Button btnSubjectAdd;
        private Guna.UI2.WinForms.Guna2Button btnSubjectUpdate;
        private Guna.UI2.WinForms.Guna2Button btnSubjectDelete;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSubjects;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges33 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges34 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges35 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges36 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges37 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges38 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges39 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges40 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            rootLayout = new TableLayoutPanel();
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblAppTitle = new Label();
            lblAppSubtitle = new Label();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            mainTabs = new Guna.UI2.WinForms.Guna2TabControl();
            tabDashboard = new TabPage();
            dashboardContentPanel = new FlowLayoutPanel();
            tabBranches = new TabPage();
            branchesLayout = new TableLayoutPanel();
            tabStudyYears = new TabPage();
            studyYearsLayout = new TableLayoutPanel();
            tabClassrooms = new TabPage();
            classroomsLayout = new TableLayoutPanel();
            branchesFormCard = new Guna.UI2.WinForms.Guna2Panel();
            branchesFieldsPanel = new FlowLayoutPanel();
            branchesButtonsPanel = new FlowLayoutPanel();
            txtBranchName = new Guna.UI2.WinForms.Guna2TextBox();
            btnBranchAdd = new Guna.UI2.WinForms.Guna2Button();
            btnBranchUpdate = new Guna.UI2.WinForms.Guna2Button();
            btnBranchDelete = new Guna.UI2.WinForms.Guna2Button();
            btnBranchClear = new Guna.UI2.WinForms.Guna2Button();
            dgvBranches = new Guna.UI2.WinForms.Guna2DataGridView();
            studyYearsFormCard = new Guna.UI2.WinForms.Guna2Panel();
            studyYearsFieldsPanel = new FlowLayoutPanel();
            studyYearsButtonsPanel = new FlowLayoutPanel();
            txtStudyYearName = new Guna.UI2.WinForms.Guna2TextBox();
            btnStudyYearAdd = new Guna.UI2.WinForms.Guna2Button();
            btnStudyYearUpdate = new Guna.UI2.WinForms.Guna2Button();
            btnStudyYearDelete = new Guna.UI2.WinForms.Guna2Button();
            dgvStudyYears = new Guna.UI2.WinForms.Guna2DataGridView();
            classroomsFormCard = new Guna.UI2.WinForms.Guna2Panel();
            classroomsFieldsPanel = new FlowLayoutPanel();
            classroomsButtonsPanel = new FlowLayoutPanel();
            txtClassroomNumber = new Guna.UI2.WinForms.Guna2TextBox();
            numClassroomCapacity = new Guna.UI2.WinForms.Guna2NumericUpDown();
            cmbRoomType = new Guna.UI2.WinForms.Guna2ComboBox();
            btnClassroomAdd = new Guna.UI2.WinForms.Guna2Button();
            btnClassroomUpdate = new Guna.UI2.WinForms.Guna2Button();
            btnClassroomDelete = new Guna.UI2.WinForms.Guna2Button();
            dgvClassrooms = new Guna.UI2.WinForms.Guna2DataGridView();
            tabFaculty = new TabPage();
            facultyLayout = new TableLayoutPanel();
            facultyFormCard = new Guna.UI2.WinForms.Guna2Panel();
            facultyFieldsPanel = new FlowLayoutPanel();
            facultyButtonsPanel = new FlowLayoutPanel();
            txtFacultyName = new Guna.UI2.WinForms.Guna2TextBox();
            cmbAcademicTitle = new Guna.UI2.WinForms.Guna2ComboBox();
            btnFacultyAdd = new Guna.UI2.WinForms.Guna2Button();
            btnFacultyUpdate = new Guna.UI2.WinForms.Guna2Button();
            btnFacultyDelete = new Guna.UI2.WinForms.Guna2Button();
            dgvFacultyMembers = new Guna.UI2.WinForms.Guna2DataGridView();
            tabSubjects = new TabPage();
            subjectsLayout = new TableLayoutPanel();
            subjectsFormCard = new Guna.UI2.WinForms.Guna2Panel();
            subjectsFieldsPanel = new FlowLayoutPanel();
            subjectsButtonsPanel = new FlowLayoutPanel();
            txtSubjectName = new Guna.UI2.WinForms.Guna2TextBox();
            cmbSubjectYear = new Guna.UI2.WinForms.Guna2ComboBox();
            cmbSubjectBranch = new Guna.UI2.WinForms.Guna2ComboBox();
            cmbSubjectFilterYear = new Guna.UI2.WinForms.Guna2ComboBox();
            cmbSubjectFilterBranch = new Guna.UI2.WinForms.Guna2ComboBox();
            cmbSubjectFilterSemester = new Guna.UI2.WinForms.Guna2ComboBox();
            numSemester = new Guna.UI2.WinForms.Guna2NumericUpDown();
            numTheoreticalHours = new Guna.UI2.WinForms.Guna2NumericUpDown();
            numPracticalHours = new Guna.UI2.WinForms.Guna2NumericUpDown();
            numCreditUnits = new Guna.UI2.WinForms.Guna2NumericUpDown();
            btnSubjectAdd = new Guna.UI2.WinForms.Guna2Button();
            btnSubjectUpdate = new Guna.UI2.WinForms.Guna2Button();
            btnSubjectDelete = new Guna.UI2.WinForms.Guna2Button();
            dgvSubjects = new Guna.UI2.WinForms.Guna2DataGridView();
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            mainTabs.SuspendLayout();
            tabDashboard.SuspendLayout();
            tabBranches.SuspendLayout();
            tabStudyYears.SuspendLayout();
            tabClassrooms.SuspendLayout();
            tabFaculty.SuspendLayout();
            tabSubjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBranches).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvStudyYears).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numClassroomCapacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvClassrooms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFacultyMembers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSemester).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTheoreticalHours).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPracticalHours).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCreditUnits).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSubjects).BeginInit();
            SuspendLayout();
            //
            // rootLayout
            //
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(mainTabs, 0, 1);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(16);
            rootLayout.RowCount = 2;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(1280, 820);
            rootLayout.TabIndex = 0;
            //
            // headerPanel
            //
            headerPanel.BorderRadius = 14;
            headerPanel.BorderThickness = 1;
            headerPanel.Controls.Add(lblAppTitle);
            headerPanel.Controls.Add(lblAppSubtitle);
            headerPanel.Controls.Add(btnRefresh);
            headerPanel.CustomizableEdges = customizableEdges3;
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(16, 16);
            headerPanel.Margin = new Padding(0, 0, 0, 12);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(24, 14, 24, 14);
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            headerPanel.Size = new Size(1248, 80);
            headerPanel.TabIndex = 0;
            //
            // lblAppTitle
            //
            lblAppTitle.AutoSize = true;
            lblAppTitle.BackColor = Color.Transparent;
            lblAppTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblAppTitle.Location = new Point(24, 12);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(260, 41);
            lblAppTitle.TabIndex = 0;
            lblAppTitle.Text = "Timetable Studio";
            lblAppTitle.Click += lblAppTitle_Click;
            //
            // lblAppSubtitle
            //
            lblAppSubtitle.AutoSize = true;
            lblAppSubtitle.BackColor = Color.Transparent;
            lblAppSubtitle.Font = new Font("Segoe UI", 10F);
            lblAppSubtitle.Location = new Point(28, 54);
            lblAppSubtitle.Name = "lblAppSubtitle";
            lblAppSubtitle.Size = new Size(392, 19);
            lblAppSubtitle.TabIndex = 1;
            lblAppSubtitle.Text = "Modern classroom, subject, faculty, and schedule management";
            lblAppSubtitle.Click += lblAppSubtitle_Click;
            //
            // btnRefresh
            //
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BorderRadius = 9;
            btnRefresh.CustomizableEdges = customizableEdges1;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1108, 22);
            btnRefresh.Margin = new Padding(0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnRefresh.Size = new Size(116, 40);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            //
            // mainTabs
            //
            mainTabs.Alignment = TabAlignment.Left;
            mainTabs.Controls.Add(tabDashboard);
            mainTabs.Controls.Add(tabBranches);
            mainTabs.Controls.Add(tabStudyYears);
            mainTabs.Controls.Add(tabClassrooms);
            mainTabs.Controls.Add(tabFaculty);
            mainTabs.Controls.Add(tabSubjects);
            mainTabs.Dock = DockStyle.Fill;
            mainTabs.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            mainTabs.ItemSize = new Size(150, 42);
            mainTabs.Location = new Point(16, 108);
            mainTabs.Margin = new Padding(0);
            mainTabs.Name = "mainTabs";
            mainTabs.Padding = new Point(12, 4);
            mainTabs.SelectedIndex = 0;
            mainTabs.Size = new Size(1248, 696);
            mainTabs.TabButtonHoverState.BorderColor = Color.Empty;
            mainTabs.TabButtonHoverState.FillColor = Color.FromArgb(40, 52, 70);
            mainTabs.TabButtonHoverState.Font = new Font("Segoe UI Semibold", 10F);
            mainTabs.TabButtonHoverState.ForeColor = Color.White;
            mainTabs.TabButtonHoverState.InnerColor = Color.FromArgb(40, 52, 70);
            mainTabs.TabButtonIdleState.BorderColor = Color.Empty;
            mainTabs.TabButtonIdleState.FillColor = Color.FromArgb(33, 42, 57);
            mainTabs.TabButtonIdleState.Font = new Font("Segoe UI Semibold", 10F);
            mainTabs.TabButtonIdleState.ForeColor = Color.FromArgb(203, 213, 225);
            mainTabs.TabButtonIdleState.InnerColor = Color.FromArgb(33, 42, 57);
            mainTabs.TabButtonSelectedState.BorderColor = Color.Empty;
            mainTabs.TabButtonSelectedState.FillColor = Color.FromArgb(29, 37, 49);
            mainTabs.TabButtonSelectedState.Font = new Font("Segoe UI Semibold", 10F);
            mainTabs.TabButtonSelectedState.ForeColor = Color.White;
            mainTabs.TabButtonSelectedState.InnerColor = Color.FromArgb(76, 132, 255);
            mainTabs.TabButtonSize = new Size(150, 42);
            mainTabs.TabIndex = 1;
            mainTabs.TabMenuBackColor = Color.FromArgb(33, 42, 57);
            //
            // tabDashboard
            //
            tabDashboard.Controls.Add(dashboardContentPanel);
            tabDashboard.Location = new Point(154, 4);
            tabDashboard.Name = "tabDashboard";
            tabDashboard.Size = new Size(1090, 688);
            tabDashboard.TabIndex = 0;
            tabDashboard.Text = "Dashboard";
            //
            // dashboardContentPanel
            //
            dashboardContentPanel.Dock = DockStyle.Fill;
            dashboardContentPanel.Location = new Point(0, 0);
            dashboardContentPanel.Name = "dashboardContentPanel";
            dashboardContentPanel.Padding = new Padding(10);
            dashboardContentPanel.Size = new Size(1090, 688);
            dashboardContentPanel.TabIndex = 0;
            //
            // tabBranches
            //
            tabBranches.Controls.Add(branchesLayout);
            tabBranches.Location = new Point(154, 4);
            tabBranches.Name = "tabBranches";
            tabBranches.Size = new Size(1090, 688);
            tabBranches.TabIndex = 1;
            tabBranches.Text = "Branches";
            //
            // branchesLayout
            //
            branchesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            branchesLayout.Location = new Point(0, 0);
            branchesLayout.Name = "branchesLayout";
            branchesLayout.Size = new Size(200, 100);
            branchesLayout.TabIndex = 0;
            //
            // tabStudyYears
            //
            tabStudyYears.Controls.Add(studyYearsLayout);
            tabStudyYears.Location = new Point(154, 4);
            tabStudyYears.Name = "tabStudyYears";
            tabStudyYears.Size = new Size(1090, 688);
            tabStudyYears.TabIndex = 2;
            tabStudyYears.Text = "Study Years";
            //
            // studyYearsLayout
            //
            studyYearsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            studyYearsLayout.Location = new Point(0, 0);
            studyYearsLayout.Name = "studyYearsLayout";
            studyYearsLayout.Size = new Size(200, 100);
            studyYearsLayout.TabIndex = 0;
            //
            // tabClassrooms
            //
            tabClassrooms.Controls.Add(classroomsLayout);
            tabClassrooms.Location = new Point(154, 4);
            tabClassrooms.Name = "tabClassrooms";
            tabClassrooms.Size = new Size(1090, 688);
            tabClassrooms.TabIndex = 3;
            tabClassrooms.Text = "Classrooms";
            //
            // classroomsLayout
            //
            classroomsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            classroomsLayout.Location = new Point(0, 0);
            classroomsLayout.Name = "classroomsLayout";
            classroomsLayout.Size = new Size(200, 100);
            classroomsLayout.TabIndex = 0;
            //
            // tabFaculty
            //
            tabFaculty.Controls.Add(facultyLayout);
            tabFaculty.Location = new Point(154, 4);
            tabFaculty.Name = "tabFaculty";
            tabFaculty.Size = new Size(1090, 688);
            tabFaculty.TabIndex = 4;
            tabFaculty.Text = "Faculty";
            //
            // facultyLayout
            //
            facultyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            facultyLayout.Location = new Point(0, 0);
            facultyLayout.Name = "facultyLayout";
            facultyLayout.Size = new Size(200, 100);
            facultyLayout.TabIndex = 0;
            //
            // tabSubjects
            //
            tabSubjects.Controls.Add(subjectsLayout);
            tabSubjects.Location = new Point(154, 4);
            tabSubjects.Name = "tabSubjects";
            tabSubjects.Size = new Size(1090, 688);
            tabSubjects.TabIndex = 5;
            tabSubjects.Text = "Subjects";
            //
            // subjectsLayout
            //
            subjectsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            subjectsLayout.Location = new Point(0, 0);
            subjectsLayout.Name = "subjectsLayout";
            subjectsLayout.Size = new Size(200, 100);
            subjectsLayout.TabIndex = 0;
            //
            // branchesFormCard
            //
            branchesFormCard.CustomizableEdges = customizableEdges5;
            branchesFormCard.Location = new Point(0, 0);
            branchesFormCard.Name = "branchesFormCard";
            branchesFormCard.ShadowDecoration.CustomizableEdges = customizableEdges6;
            branchesFormCard.Size = new Size(200, 100);
            branchesFormCard.TabIndex = 0;
            //
            // branchesFieldsPanel
            //
            branchesFieldsPanel.Location = new Point(0, 0);
            branchesFieldsPanel.Name = "branchesFieldsPanel";
            branchesFieldsPanel.Size = new Size(200, 100);
            branchesFieldsPanel.TabIndex = 0;
            //
            // branchesButtonsPanel
            //
            branchesButtonsPanel.Location = new Point(0, 0);
            branchesButtonsPanel.Name = "branchesButtonsPanel";
            branchesButtonsPanel.Size = new Size(200, 100);
            branchesButtonsPanel.TabIndex = 0;
            //
            // txtBranchName
            //
            txtBranchName.CustomizableEdges = customizableEdges7;
            txtBranchName.DefaultText = "";
            txtBranchName.Font = new Font("Segoe UI", 9F);
            txtBranchName.Location = new Point(0, 0);
            txtBranchName.Name = "txtBranchName";
            txtBranchName.PlaceholderText = "";
            txtBranchName.SelectedText = "";
            txtBranchName.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtBranchName.Size = new Size(200, 36);
            txtBranchName.TabIndex = 0;
            //
            // btnBranchAdd
            //
            btnBranchAdd.CustomizableEdges = customizableEdges9;
            btnBranchAdd.Font = new Font("Segoe UI", 9F);
            btnBranchAdd.ForeColor = Color.White;
            btnBranchAdd.Location = new Point(0, 0);
            btnBranchAdd.Name = "btnBranchAdd";
            btnBranchAdd.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnBranchAdd.Size = new Size(180, 45);
            btnBranchAdd.TabIndex = 0;
            //
            // btnBranchUpdate
            //
            btnBranchUpdate.CustomizableEdges = customizableEdges11;
            btnBranchUpdate.Font = new Font("Segoe UI", 9F);
            btnBranchUpdate.ForeColor = Color.White;
            btnBranchUpdate.Location = new Point(0, 0);
            btnBranchUpdate.Name = "btnBranchUpdate";
            btnBranchUpdate.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnBranchUpdate.Size = new Size(180, 45);
            btnBranchUpdate.TabIndex = 0;
            //
            // btnBranchDelete
            //
            btnBranchDelete.CustomizableEdges = customizableEdges13;
            btnBranchDelete.Font = new Font("Segoe UI", 9F);
            btnBranchDelete.ForeColor = Color.White;
            btnBranchDelete.Location = new Point(0, 0);
            btnBranchDelete.Name = "btnBranchDelete";
            btnBranchDelete.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnBranchDelete.Size = new Size(180, 45);
            btnBranchDelete.TabIndex = 0;
            //
            // btnBranchClear
            //
            btnBranchClear.CustomizableEdges = customizableEdges15;
            btnBranchClear.Font = new Font("Segoe UI", 9F);
            btnBranchClear.ForeColor = Color.White;
            btnBranchClear.Location = new Point(0, 0);
            btnBranchClear.Name = "btnBranchClear";
            btnBranchClear.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnBranchClear.Size = new Size(180, 45);
            btnBranchClear.TabIndex = 0;
            //
            // dgvBranches
            //
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11.25F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvBranches.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvBranches.ColumnHeadersHeight = 15;
            dgvBranches.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11.25F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvBranches.DefaultCellStyle = dataGridViewCellStyle2;
            dgvBranches.GridColor = Color.FromArgb(231, 229, 255);
            dgvBranches.Location = new Point(0, 0);
            dgvBranches.Name = "dgvBranches";
            dgvBranches.RowHeadersVisible = false;
            dgvBranches.Size = new Size(240, 150);
            dgvBranches.TabIndex = 0;
            dgvBranches.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11.25F);
            dgvBranches.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 11.25F);
            dgvBranches.ThemeStyle.RowsStyle.Height = 29;
            //
            // studyYearsFormCard
            //
            studyYearsFormCard.CustomizableEdges = customizableEdges17;
            studyYearsFormCard.Location = new Point(0, 0);
            studyYearsFormCard.Name = "studyYearsFormCard";
            studyYearsFormCard.ShadowDecoration.CustomizableEdges = customizableEdges18;
            studyYearsFormCard.Size = new Size(200, 100);
            studyYearsFormCard.TabIndex = 0;
            //
            // studyYearsFieldsPanel
            //
            studyYearsFieldsPanel.Location = new Point(0, 0);
            studyYearsFieldsPanel.Name = "studyYearsFieldsPanel";
            studyYearsFieldsPanel.Size = new Size(200, 100);
            studyYearsFieldsPanel.TabIndex = 0;
            //
            // studyYearsButtonsPanel
            //
            studyYearsButtonsPanel.Location = new Point(0, 0);
            studyYearsButtonsPanel.Name = "studyYearsButtonsPanel";
            studyYearsButtonsPanel.Size = new Size(200, 100);
            studyYearsButtonsPanel.TabIndex = 0;
            //
            // txtStudyYearName
            //
            txtStudyYearName.CustomizableEdges = customizableEdges19;
            txtStudyYearName.DefaultText = "";
            txtStudyYearName.Font = new Font("Segoe UI", 9F);
            txtStudyYearName.Location = new Point(0, 0);
            txtStudyYearName.Name = "txtStudyYearName";
            txtStudyYearName.PlaceholderText = "";
            txtStudyYearName.SelectedText = "";
            txtStudyYearName.ShadowDecoration.CustomizableEdges = customizableEdges20;
            txtStudyYearName.Size = new Size(200, 36);
            txtStudyYearName.TabIndex = 0;
            //
            // btnStudyYearAdd
            //
            btnStudyYearAdd.CustomizableEdges = customizableEdges21;
            btnStudyYearAdd.Font = new Font("Segoe UI", 9F);
            btnStudyYearAdd.ForeColor = Color.White;
            btnStudyYearAdd.Location = new Point(0, 0);
            btnStudyYearAdd.Name = "btnStudyYearAdd";
            btnStudyYearAdd.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnStudyYearAdd.Size = new Size(180, 45);
            btnStudyYearAdd.TabIndex = 0;
            //
            // btnStudyYearUpdate
            //
            btnStudyYearUpdate.CustomizableEdges = customizableEdges23;
            btnStudyYearUpdate.Font = new Font("Segoe UI", 9F);
            btnStudyYearUpdate.ForeColor = Color.White;
            btnStudyYearUpdate.Location = new Point(0, 0);
            btnStudyYearUpdate.Name = "btnStudyYearUpdate";
            btnStudyYearUpdate.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnStudyYearUpdate.Size = new Size(180, 45);
            btnStudyYearUpdate.TabIndex = 0;
            //
            // btnStudyYearDelete
            //
            btnStudyYearDelete.CustomizableEdges = customizableEdges25;
            btnStudyYearDelete.Font = new Font("Segoe UI", 9F);
            btnStudyYearDelete.ForeColor = Color.White;
            btnStudyYearDelete.Location = new Point(0, 0);
            btnStudyYearDelete.Name = "btnStudyYearDelete";
            btnStudyYearDelete.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnStudyYearDelete.Size = new Size(180, 45);
            btnStudyYearDelete.TabIndex = 0;
            //
            // dgvStudyYears
            //
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 11.25F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvStudyYears.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvStudyYears.ColumnHeadersHeight = 15;
            dgvStudyYears.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 11.25F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvStudyYears.DefaultCellStyle = dataGridViewCellStyle4;
            dgvStudyYears.GridColor = Color.FromArgb(231, 229, 255);
            dgvStudyYears.Location = new Point(0, 0);
            dgvStudyYears.Name = "dgvStudyYears";
            dgvStudyYears.RowHeadersVisible = false;
            dgvStudyYears.Size = new Size(240, 150);
            dgvStudyYears.TabIndex = 0;
            dgvStudyYears.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11.25F);
            dgvStudyYears.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 11.25F);
            dgvStudyYears.ThemeStyle.RowsStyle.Height = 29;
            //
            // classroomsFormCard
            //
            classroomsFormCard.CustomizableEdges = customizableEdges27;
            classroomsFormCard.Location = new Point(0, 0);
            classroomsFormCard.Name = "classroomsFormCard";
            classroomsFormCard.ShadowDecoration.CustomizableEdges = customizableEdges28;
            classroomsFormCard.Size = new Size(200, 100);
            classroomsFormCard.TabIndex = 0;
            //
            // classroomsFieldsPanel
            //
            classroomsFieldsPanel.Location = new Point(0, 0);
            classroomsFieldsPanel.Name = "classroomsFieldsPanel";
            classroomsFieldsPanel.Size = new Size(200, 100);
            classroomsFieldsPanel.TabIndex = 0;
            //
            // classroomsButtonsPanel
            //
            classroomsButtonsPanel.Location = new Point(0, 0);
            classroomsButtonsPanel.Name = "classroomsButtonsPanel";
            classroomsButtonsPanel.Size = new Size(200, 100);
            classroomsButtonsPanel.TabIndex = 0;
            //
            // txtClassroomNumber
            //
            txtClassroomNumber.CustomizableEdges = customizableEdges29;
            txtClassroomNumber.DefaultText = "";
            txtClassroomNumber.Font = new Font("Segoe UI", 9F);
            txtClassroomNumber.Location = new Point(0, 0);
            txtClassroomNumber.Name = "txtClassroomNumber";
            txtClassroomNumber.PlaceholderText = "";
            txtClassroomNumber.SelectedText = "";
            txtClassroomNumber.ShadowDecoration.CustomizableEdges = customizableEdges30;
            txtClassroomNumber.Size = new Size(200, 36);
            txtClassroomNumber.TabIndex = 0;
            //
            // numClassroomCapacity
            //
            numClassroomCapacity.BackColor = Color.Transparent;
            numClassroomCapacity.CustomizableEdges = customizableEdges31;
            numClassroomCapacity.Font = new Font("Segoe UI", 9F);
            numClassroomCapacity.Location = new Point(0, 0);
            numClassroomCapacity.Name = "numClassroomCapacity";
            numClassroomCapacity.ShadowDecoration.CustomizableEdges = customizableEdges32;
            numClassroomCapacity.Size = new Size(100, 36);
            numClassroomCapacity.TabIndex = 0;
            //
            // cmbRoomType
            //
            cmbRoomType.BackColor = Color.Transparent;
            cmbRoomType.CustomizableEdges = customizableEdges33;
            cmbRoomType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRoomType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRoomType.FocusedColor = Color.Empty;
            cmbRoomType.Font = new Font("Segoe UI", 10F);
            cmbRoomType.ForeColor = Color.FromArgb(68, 88, 112);
            cmbRoomType.ItemHeight = 30;
            cmbRoomType.Location = new Point(0, 0);
            cmbRoomType.Name = "cmbRoomType";
            cmbRoomType.ShadowDecoration.CustomizableEdges = customizableEdges34;
            cmbRoomType.Size = new Size(140, 36);
            cmbRoomType.TabIndex = 0;
            //
            // btnClassroomAdd
            //
            btnClassroomAdd.CustomizableEdges = customizableEdges35;
            btnClassroomAdd.Font = new Font("Segoe UI", 9F);
            btnClassroomAdd.ForeColor = Color.White;
            btnClassroomAdd.Location = new Point(0, 0);
            btnClassroomAdd.Name = "btnClassroomAdd";
            btnClassroomAdd.ShadowDecoration.CustomizableEdges = customizableEdges36;
            btnClassroomAdd.Size = new Size(180, 45);
            btnClassroomAdd.TabIndex = 0;
            //
            // btnClassroomUpdate
            //
            btnClassroomUpdate.CustomizableEdges = customizableEdges37;
            btnClassroomUpdate.Font = new Font("Segoe UI", 9F);
            btnClassroomUpdate.ForeColor = Color.White;
            btnClassroomUpdate.Location = new Point(0, 0);
            btnClassroomUpdate.Name = "btnClassroomUpdate";
            btnClassroomUpdate.ShadowDecoration.CustomizableEdges = customizableEdges38;
            btnClassroomUpdate.Size = new Size(180, 45);
            btnClassroomUpdate.TabIndex = 0;
            //
            // btnClassroomDelete
            //
            btnClassroomDelete.CustomizableEdges = customizableEdges39;
            btnClassroomDelete.Font = new Font("Segoe UI", 9F);
            btnClassroomDelete.ForeColor = Color.White;
            btnClassroomDelete.Location = new Point(0, 0);
            btnClassroomDelete.Name = "btnClassroomDelete";
            btnClassroomDelete.ShadowDecoration.CustomizableEdges = customizableEdges40;
            btnClassroomDelete.Size = new Size(180, 45);
            btnClassroomDelete.TabIndex = 0;
            //
            // dgvClassrooms
            //
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 11.25F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvClassrooms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvClassrooms.ColumnHeadersHeight = 15;
            dgvClassrooms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 11.25F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvClassrooms.DefaultCellStyle = dataGridViewCellStyle6;
            dgvClassrooms.GridColor = Color.FromArgb(231, 229, 255);
            dgvClassrooms.Location = new Point(0, 0);
            dgvClassrooms.Name = "dgvClassrooms";
            dgvClassrooms.RowHeadersVisible = false;
            dgvClassrooms.Size = new Size(240, 150);
            dgvClassrooms.TabIndex = 0;
            dgvClassrooms.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11.25F);
            dgvClassrooms.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 11.25F);
            dgvClassrooms.ThemeStyle.RowsStyle.Height = 29;
            ConfigureDesignerOwnedPages();
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 820);
            Controls.Add(rootLayout);
            MinimumSize = new Size(1080, 720);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Timetable and Classroom Management System";
            Load += MainForm_Load_1;
            rootLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            mainTabs.ResumeLayout(false);
            tabDashboard.ResumeLayout(false);
            tabBranches.ResumeLayout(false);
            tabStudyYears.ResumeLayout(false);
            tabClassrooms.ResumeLayout(false);
            tabFaculty.ResumeLayout(false);
            tabSubjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBranches).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvStudyYears).EndInit();
            ((System.ComponentModel.ISupportInitialize)numClassroomCapacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvClassrooms).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFacultyMembers).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSemester).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTheoreticalHours).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPracticalHours).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCreditUnits).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSubjects).EndInit();
            ResumeLayout(false);
        }

        private void ConfigureDesignerOwnedPages()
        {
            dashboardContentPanel.FlowDirection = FlowDirection.LeftToRight;
            dashboardContentPanel.WrapContents = true;
            dashboardContentPanel.BackColor = AppBackground;
            CreateDashboardDesignerCards();

            CreateDataDesignerPage(
                branchesLayout,
                branchesFormCard,
                branchesFieldsPanel,
                branchesButtonsPanel,
                dgvBranches,
                "Branches",
                "Manage academic branches.");
            AddDesignerTextField(branchesFieldsPanel, "Branch name", txtBranchName, "Computer Science");
            AddDesignerButton(branchesButtonsPanel, btnBranchAdd, "Add", SuccessColor);
            AddDesignerButton(branchesButtonsPanel, btnBranchUpdate, "Update", PrimaryColor);
            AddDesignerButton(branchesButtonsPanel, btnBranchDelete, "Delete", DangerColor);
            AddDesignerButton(branchesButtonsPanel, btnBranchClear, "Clear", MutedColor);

            CreateDataDesignerPage(
                studyYearsLayout,
                studyYearsFormCard,
                studyYearsFieldsPanel,
                studyYearsButtonsPanel,
                dgvStudyYears,
                "Study Years",
                "Create and update academic year levels.");
            AddDesignerTextField(studyYearsFieldsPanel, "Study year name", txtStudyYearName, "First Year");
            AddDesignerButton(studyYearsButtonsPanel, btnStudyYearAdd, "Add", SuccessColor);
            AddDesignerButton(studyYearsButtonsPanel, btnStudyYearUpdate, "Update", PrimaryColor);
            AddDesignerButton(studyYearsButtonsPanel, btnStudyYearDelete, "Delete", DangerColor);

            CreateDataDesignerPage(
                classroomsLayout,
                classroomsFormCard,
                classroomsFieldsPanel,
                classroomsButtonsPanel,
                dgvClassrooms,
                "Classrooms",
                "Manage rooms, labs, and their capacity.");
            AddDesignerTextField(classroomsFieldsPanel, "Room number", txtClassroomNumber, "A-101");
            AddDesignerNumberField(classroomsFieldsPanel, "Capacity", numClassroomCapacity, 150, 1, 1000, 30);
            AddDesignerComboField(classroomsFieldsPanel, "Room type", cmbRoomType, new object[] { "Lecture", "Lab" }, 0);
            AddDesignerButton(classroomsButtonsPanel, btnClassroomAdd, "Add", SuccessColor);
            AddDesignerButton(classroomsButtonsPanel, btnClassroomUpdate, "Update", PrimaryColor);
            AddDesignerButton(classroomsButtonsPanel, btnClassroomDelete, "Delete", DangerColor);

            CreateDataDesignerPage(
                facultyLayout,
                facultyFormCard,
                facultyFieldsPanel,
                facultyButtonsPanel,
                dgvFacultyMembers,
                "Faculty",
                "Manage instructors and academic titles.");
            AddDesignerTextField(facultyFieldsPanel, "Full name", txtFacultyName, "Dr. Sara Ahmed");
            AddDesignerComboField(
                facultyFieldsPanel,
                "Academic title",
                cmbAcademicTitle,
                new object[] { "Professor", "Assistant Professor", "Lecturer", "Assistant Lecturer" },
                1);
            AddDesignerButton(facultyButtonsPanel, btnFacultyAdd, "Add", SuccessColor);
            AddDesignerButton(facultyButtonsPanel, btnFacultyUpdate, "Update", PrimaryColor);
            AddDesignerButton(facultyButtonsPanel, btnFacultyDelete, "Delete", DangerColor);

            CreateDataDesignerPage(
                subjectsLayout,
                subjectsFormCard,
                subjectsFieldsPanel,
                subjectsButtonsPanel,
                dgvSubjects,
                "Subjects",
                "Manage course information and hours.");
            AddDesignerTextField(subjectsFieldsPanel, "Subject name", txtSubjectName, "Algorithms");
            AddDesignerLookupComboField(subjectsFieldsPanel, "Study year", cmbSubjectYear);
            AddDesignerLookupComboField(subjectsFieldsPanel, "Branch", cmbSubjectBranch);
            AddDesignerNumberField(subjectsFieldsPanel, "Semester", numSemester, 130, 1, 2, 1);
            AddDesignerNumberField(subjectsFieldsPanel, "Theory hours", numTheoreticalHours, 150, 0, 20, 2, 2, 0.5M);
            AddDesignerNumberField(subjectsFieldsPanel, "Practical hours", numPracticalHours, 150, 0, 20, 0, 2, 0.5M);
            AddDesignerNumberField(subjectsFieldsPanel, "Credit units", numCreditUnits, 150, 0.5M, 20, 3, 2, 0.5M);
            ConfigureDesignerFilterCombo(cmbSubjectFilterYear, 170);
            ConfigureDesignerFilterCombo(cmbSubjectFilterBranch, 170);
            ConfigureDesignerFilterCombo(cmbSubjectFilterSemester, 150);
            subjectsButtonsPanel.Controls.Add(cmbSubjectFilterYear);
            subjectsButtonsPanel.Controls.Add(cmbSubjectFilterBranch);
            subjectsButtonsPanel.Controls.Add(cmbSubjectFilterSemester);
            AddDesignerButton(subjectsButtonsPanel, btnSubjectAdd, "Add", SuccessColor);
            AddDesignerButton(subjectsButtonsPanel, btnSubjectUpdate, "Update", PrimaryColor);
            AddDesignerButton(subjectsButtonsPanel, btnSubjectDelete, "Delete", DangerColor);
        }

        private void CreateDashboardDesignerCards()
        {
            lblBranchCount = CreateDesignerStatValueLabel();
            lblSubjectCount = CreateDesignerStatValueLabel();
            lblClassroomCount = CreateDesignerStatValueLabel();
            lblScheduleCount = CreateDesignerStatValueLabel();

            dashboardContentPanel.Controls.Add(CreateDesignerStatCard("Branches", lblBranchCount, "Academic departments and tracks"));
            dashboardContentPanel.Controls.Add(CreateDesignerStatCard("Subjects", lblSubjectCount, "Courses ready for scheduling"));
            dashboardContentPanel.Controls.Add(CreateDesignerStatCard("Classrooms", lblClassroomCount, "Rooms with capacity data"));
            dashboardContentPanel.Controls.Add(CreateDesignerStatCard("Schedules", lblScheduleCount, "Created timetable entries"));

            var note = new Guna.UI2.WinForms.Guna2Panel
            {
                Width = 960,
                Height = 130,
                BorderRadius = 12,
                FillColor = CardBackground,
                Margin = new Padding(0, 18, 0, 0),
                Padding = new Padding(22)
            };

            note.Controls.Add(new Label
            {
                Text = "System overview",
                AutoSize = true,
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = HeaderColor,
                BackColor = Color.Transparent,
                Location = new Point(22, 20)
            });
            note.Controls.Add(new Label
            {
                Text = "Use the sidebar to manage branches, study years, classrooms, faculty, subjects, sections, time slots, teaching assignments, and schedules. Conflict checks stay active while adding or updating schedule entries.",
                AutoSize = false,
                Width = 880,
                Height = 48,
                Font = new Font("Segoe UI", 10F),
                ForeColor = MutedColor,
                BackColor = Color.Transparent,
                Location = new Point(24, 62)
            });

            dashboardContentPanel.Controls.Add(note);
        }

        private Label CreateDesignerStatValueLabel()
        {
            return new Label
            {
                Text = "0",
                AutoSize = true,
                Font = new Font("Segoe UI", 26F, FontStyle.Bold),
                ForeColor = HeaderColor,
                BackColor = Color.Transparent
            };
        }

        private Guna.UI2.WinForms.Guna2Panel CreateDesignerStatCard(string title, Label valueLabel, string subtitle)
        {
            var card = new Guna.UI2.WinForms.Guna2Panel
            {
                Width = 220,
                Height = 140,
                BorderRadius = 12,
                FillColor = CardBackground,
                Margin = new Padding(0, 0, 18, 18),
                Padding = new Padding(18)
            };

            card.Controls.Add(new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = MutedColor,
                BackColor = Color.Transparent,
                Location = new Point(18, 18)
            });

            valueLabel.Location = new Point(18, 46);
            card.Controls.Add(valueLabel);

            card.Controls.Add(new Label
            {
                Text = subtitle,
                AutoSize = false,
                Width = 178,
                Height = 40,
                Font = new Font("Segoe UI", 9F),
                ForeColor = MutedColor,
                BackColor = Color.Transparent,
                Location = new Point(18, 94)
            });

            return card;
        }

        private void CreateDataDesignerPage(
            TableLayoutPanel pageLayout,
            Guna.UI2.WinForms.Guna2Panel formCard,
            FlowLayoutPanel fieldsPanel,
            FlowLayoutPanel buttonsPanel,
            Guna.UI2.WinForms.Guna2DataGridView grid,
            string title,
            string subtitle)
        {
            var heading = new Panel { Dock = DockStyle.Fill, BackColor = AppBackground };

            heading.Controls.Add(new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = HeaderColor,
                BackColor = Color.Transparent,
                Location = new Point(0, 4)
            });
            heading.Controls.Add(new Label
            {
                Text = subtitle,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = MutedColor,
                BackColor = Color.Transparent,
                Location = new Point(2, 46)
            });

            pageLayout.BackColor = AppBackground;
            pageLayout.ColumnCount = 1;
            pageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pageLayout.Dock = DockStyle.Fill;
            pageLayout.Location = new Point(0, 0);
            pageLayout.Padding = new Padding(10);
            pageLayout.RowCount = 3;
            pageLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            pageLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 250F));
            pageLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            formCard.BorderRadius = 12;
            formCard.Dock = DockStyle.Fill;
            formCard.FillColor = CardBackground;
            formCard.Padding = new Padding(18);

            fieldsPanel.BackColor = CardBackground;
            fieldsPanel.Dock = DockStyle.Top;
            fieldsPanel.FlowDirection = FlowDirection.LeftToRight;
            fieldsPanel.Height = 164;
            fieldsPanel.WrapContents = true;

            buttonsPanel.BackColor = CardBackground;
            buttonsPanel.Dock = DockStyle.Bottom;
            buttonsPanel.FlowDirection = FlowDirection.LeftToRight;
            buttonsPanel.Height = 50;
            buttonsPanel.WrapContents = false;

            ConfigureDesignerGrid(grid);

            formCard.Controls.Add(buttonsPanel);
            formCard.Controls.Add(fieldsPanel);
            pageLayout.Controls.Add(heading, 0, 0);
            pageLayout.Controls.Add(formCard, 0, 1);
            pageLayout.Controls.Add(grid, 0, 2);
        }

        private void AddDesignerTextField(FlowLayoutPanel fieldsPanel, string labelText, Guna.UI2.WinForms.Guna2TextBox input, string placeholder)
        {
            input.BorderColor = BorderColor;
            input.BorderRadius = 8;
            input.FillColor = CardBackground;
            input.Font = new Font("Segoe UI", 10F);
            input.ForeColor = HeaderColor;
            input.Height = 40;
            input.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            input.PlaceholderText = placeholder;
            input.Width = 220;

            fieldsPanel.Controls.Add(CreateDesignerFieldPanel(labelText, input, 220));
        }

        private void AddDesignerNumberField(
            FlowLayoutPanel fieldsPanel,
            string labelText,
            Guna.UI2.WinForms.Guna2NumericUpDown input,
            int width,
            decimal minimum,
            decimal maximum,
            decimal value,
            int decimalPlaces = 0,
            decimal increment = 1)
        {
            input.BorderColor = BorderColor;
            input.BorderRadius = 8;
            input.FillColor = CardBackground;
            input.Font = new Font("Segoe UI", 10F);
            input.ForeColor = HeaderColor;
            input.Height = 40;
            input.Minimum = minimum;
            input.Maximum = maximum;
            input.Value = value;
            input.DecimalPlaces = decimalPlaces;
            input.Increment = increment;
            input.Width = width;

            fieldsPanel.Controls.Add(CreateDesignerFieldPanel(labelText, input, width));
        }

        private void AddDesignerLookupComboField(
            FlowLayoutPanel fieldsPanel,
            string labelText,
            Guna.UI2.WinForms.Guna2ComboBox input,
            int width = 220)
        {
            ConfigureDesignerCombo(input, width);
            fieldsPanel.Controls.Add(CreateDesignerFieldPanel(labelText, input, width));
        }

        private void AddDesignerComboField(
            FlowLayoutPanel fieldsPanel,
            string labelText,
            Guna.UI2.WinForms.Guna2ComboBox input,
            object[] items,
            int selectedIndex)
        {
            ConfigureDesignerCombo(input, 220);
            input.Items.AddRange(items);
            input.SelectedIndex = items.Length > selectedIndex ? selectedIndex : 0;

            fieldsPanel.Controls.Add(CreateDesignerFieldPanel(labelText, input, 220));
        }

        private void ConfigureDesignerFilterCombo(Guna.UI2.WinForms.Guna2ComboBox input, int width)
        {
            ConfigureDesignerCombo(input, width);
            input.Margin = new Padding(0, 6, 10, 0);
        }

        private void ConfigureDesignerCombo(Guna.UI2.WinForms.Guna2ComboBox input, int width)
        {
            input.BorderColor = BorderColor;
            input.BorderRadius = 8;
            input.DropDownStyle = ComboBoxStyle.DropDownList;
            input.FillColor = CardBackground;
            input.Font = new Font("Segoe UI", 10F);
            input.ForeColor = HeaderColor;
            input.Height = 40;
            input.Width = width;
        }

        private Panel CreateDesignerFieldPanel(string labelText, Control input, int width)
        {
            var panel = new Panel
            {
                Width = width,
                Height = 74,
                Margin = new Padding(0, 0, 16, 10),
                BackColor = CardBackground
            };

            panel.Controls.Add(new Label
            {
                Text = labelText,
                AutoSize = false,
                Width = width,
                Height = 24,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = HeaderColor,
                BackColor = Color.Transparent,
                Location = new Point(0, 0)
            });

            input.Location = new Point(0, 28);
            panel.Controls.Add(input);

            return panel;
        }

        private void AddDesignerButton(FlowLayoutPanel buttonsPanel, Guna.UI2.WinForms.Guna2Button button, string text, Color fillColor)
        {
            button.BorderRadius = 9;
            button.FillColor = fillColor;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.ForeColor = Color.White;
            button.Height = 40;
            button.Margin = new Padding(0, 6, 10, 0);
            button.Text = text;
            button.Width = 112;
            buttonsPanel.Controls.Add(button);
        }

        private void ConfigureDesignerGrid(Guna.UI2.WinForms.Guna2DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = CardBackground;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersHeight = 44;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.Dock = DockStyle.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = BorderColor;
            grid.Margin = new Padding(0, 14, 0, 0);
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                SelectionBackColor = PrimaryColor,
                SelectionForeColor = Color.White
            };
            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = CardBackground,
                ForeColor = HeaderColor,
                Font = new Font("Segoe UI", 9F),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = HeaderColor
            };
            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = SurfaceColor
            };
            grid.RowTemplate.Height = 38;
        }
    }
}
