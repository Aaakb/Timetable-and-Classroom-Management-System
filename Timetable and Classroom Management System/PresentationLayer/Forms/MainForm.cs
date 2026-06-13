using System.Globalization;
using Guna.UI2.WinForms;
using Timetable_and_Classroom_Management_System.BusinessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.PresentationLayer.Forms
{
    public partial class MainForm : Form
    {
        private static readonly Color AppBackground = Color.FromArgb(245, 247, 251);
        private static readonly Color CardBackground = Color.White;
        private static readonly Color SurfaceColor = Color.FromArgb(248, 250, 252);
        private static readonly Color BorderColor = Color.FromArgb(226, 232, 240);
        private static readonly Color HeaderColor = Color.FromArgb(15, 23, 42);
        private static readonly Color SidebarColor = Color.FromArgb(30, 41, 59);
        private static readonly Color SidebarHoverColor = Color.FromArgb(51, 65, 85);
        private static readonly Color PrimaryColor = Color.FromArgb(37, 99, 235);
        private static readonly Color SuccessColor = Color.FromArgb(16, 185, 129);
        private static readonly Color DangerColor = Color.FromArgb(239, 68, 68);
        private static readonly Color MutedColor = Color.FromArgb(100, 116, 139);
        private const int AutomaticScheduleSemester = 2;

        private readonly BranchService _branchService = new BranchService();
        private readonly StudyYearService _studyYearService = new StudyYearService();
        private readonly ClassroomService _classroomService = new ClassroomService();
        private readonly FacultyMemberService _facultyMemberService = new FacultyMemberService();
        private readonly SubjectService _subjectService = new SubjectService();
        private readonly SectionService _sectionService = new SectionService();
        private readonly TimeSlotService _timeSlotService = new TimeSlotService();
        private readonly FacultyMemberSubjectService _assignmentService = new FacultyMemberSubjectService();
        private readonly ScheduleService _scheduleService = new ScheduleService();
        private readonly CurriculumImportService _curriculumImportService = new CurriculumImportService();
        private readonly WeeklyScheduleImportService _weeklyScheduleImportService = new WeeklyScheduleImportService();

        private List<Branch> _branches = new List<Branch>();
        private List<StudyYear> _studyYears = new List<StudyYear>();
        private List<Classroom> _classrooms = new List<Classroom>();
        private List<FacultyMember> _facultyMembers = new List<FacultyMember>();
        private List<Subject> _subjects = new List<Subject>();
        private List<Section> _sections = new List<Section>();
        private List<TimeSlot> _timeSlots = new List<TimeSlot>();
        private List<FacultyMemberSubject> _assignments = new List<FacultyMemberSubject>();
        private List<Schedule> _schedules = new List<Schedule>();

        private Label lblBranchCount = null!;
        private Label lblSubjectCount = null!;
        private Label lblClassroomCount = null!;
        private Label lblScheduleCount = null!;

        private Guna2DataGridView dgvBranches = null!;
        private Guna2TextBox txtBranchName = null!;
        private int selectedBranchId;

        private Guna2DataGridView dgvStudyYears = null!;
        private Guna2TextBox txtStudyYearName = null!;
        private int selectedStudyYearId;

        private Guna2DataGridView dgvClassrooms = null!;
        private Guna2TextBox txtClassroomNumber = null!;
        private Guna2NumericUpDown numClassroomCapacity = null!;
        private Guna2RadioButton rdoRoomLecture = null!;
        private Guna2RadioButton rdoRoomLab = null!;
        private int selectedClassroomId;

        private Guna2DataGridView dgvFacultyMembers = null!;
        private Guna2TextBox txtFacultyName = null!;
        private Guna2TextBox txtAcademicTitle = null!;
        private int selectedFacultyMemberId;

        private Guna2DataGridView dgvSubjects = null!;
        private Guna2TextBox txtSubjectName = null!;
        private Guna2ComboBox cmbSubjectYear = null!;
        private Guna2ComboBox cmbSubjectBranch = null!;
        private Guna2NumericUpDown numSemester = null!;
        private Guna2NumericUpDown numTheoreticalHours = null!;
        private Guna2NumericUpDown numPracticalHours = null!;
        private Guna2NumericUpDown numCreditUnits = null!;
        private Guna2TextBox txtRequirementType = null!;
        private int selectedSubjectId;

        private Guna2DataGridView dgvSections = null!;
        private Guna2TextBox txtSectionName = null!;
        private Guna2ComboBox cmbSectionYear = null!;
        private Guna2ComboBox cmbSectionBranch = null!;
        private Guna2NumericUpDown numStudentCount = null!;
        private int selectedSectionId;

        private Guna2DataGridView dgvTimeSlots = null!;
        private Guna2TextBox txtStartTime = null!;
        private Guna2TextBox txtEndTime = null!;
        private Guna2CheckBox chkIsBreak = null!;
        private int selectedTimeSlotId;

        private Guna2DataGridView dgvAssignments = null!;
        private Guna2ComboBox cmbAssignFaculty = null!;
        private Guna2ComboBox cmbAssignSubject = null!;
        private int selectedAssignmentFacultyId;
        private int selectedAssignmentSubjectId;

        private Guna2DataGridView dgvSchedules = null!;
        private Guna2ComboBox cmbScheduleSubject = null!;
        private Guna2ComboBox cmbScheduleFaculty = null!;
        private Guna2ComboBox cmbScheduleClassroom = null!;
        private Guna2ComboBox cmbScheduleTimeSlot = null!;
        private Guna2ComboBox cmbScheduleDay = null!;
        private Guna2ComboBox cmbScheduleYear = null!;
        private Guna2ComboBox cmbScheduleBranch = null!;
        private Guna2ComboBox cmbScheduleSection = null!;
        private int selectedScheduleId;

        public MainForm()
        {
            InitializeComponent();
            BuildInterface();
            Load += MainForm_Load;
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            TryLoadAllData();
        }

        private void BuildInterface()
        {
            BackColor = AppBackground;
            Controls.Clear();

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = AppBackground,
                Padding = new Padding(16)
            };

            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 92));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            root.Controls.Add(CreateHeader(), 0, 0);

            var tabs = new Guna2TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ItemSize = new Size(150, 42),
                SizeMode = TabSizeMode.Fixed,
                Padding = new Point(12, 4),
                TabMenuBackColor = SidebarColor
            };
            tabs.TabButtonIdleState.FillColor = SidebarColor;
            tabs.TabButtonIdleState.ForeColor = Color.FromArgb(203, 213, 225);
            tabs.TabButtonIdleState.InnerColor = SidebarColor;
            tabs.TabButtonHoverState.FillColor = SidebarHoverColor;
            tabs.TabButtonHoverState.ForeColor = Color.White;
            tabs.TabButtonHoverState.InnerColor = PrimaryColor;
            tabs.TabButtonSelectedState.FillColor = HeaderColor;
            tabs.TabButtonSelectedState.ForeColor = Color.White;
            tabs.TabButtonSelectedState.InnerColor = PrimaryColor;

            tabs.Controls.Add(CreateDashboardPage());
            tabs.Controls.Add(CreateBranchesPage());
            tabs.Controls.Add(CreateStudyYearsPage());
            tabs.Controls.Add(CreateClassroomsPage());
            tabs.Controls.Add(CreateFacultyPage());
            tabs.Controls.Add(CreateSubjectsPage());
            tabs.Controls.Add(CreateSectionsPage());
            tabs.Controls.Add(CreateTimeSlotsPage());
            tabs.Controls.Add(CreateAssignmentsPage());
            tabs.Controls.Add(CreateSchedulesPage());

            root.Controls.Add(tabs, 0, 1);
            Controls.Add(root);
        }

        private Control CreateHeader()
        {
            var header = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BorderRadius = 14,
                FillColor = CardBackground,
                BorderColor = BorderColor,
                BorderThickness = 1,
                Padding = new Padding(24, 14, 24, 14)
            };

            var title = new Label
            {
                Text = "Timetable Studio",
                AutoSize = true,
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = HeaderColor,
                BackColor = Color.Transparent,
                Location = new Point(24, 14)
            };

            var subtitle = new Label
            {
                Text = "Modern classroom, subject, faculty, and schedule management",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = MutedColor,
                BackColor = Color.Transparent,
                Location = new Point(28, 56)
            };

            var refreshButton = CreateActionButton("Refresh", PrimaryColor);
            refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            refreshButton.Location = new Point(Width - 170, 24);
            refreshButton.Size = new Size(116, 40);
            refreshButton.Click += (_, _) => TryLoadAllData();
            header.Resize += (_, _) => refreshButton.Location = new Point(header.Width - 148, 24);

            header.Controls.Add(title);
            header.Controls.Add(subtitle);
            header.Controls.Add(refreshButton);

            return header;
        }

        private TabPage CreateDashboardPage()
        {
            var page = CreateTab("Dashboard");

            var content = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(10),
                BackColor = AppBackground
            };

            lblBranchCount = CreateStatValueLabel();
            lblSubjectCount = CreateStatValueLabel();
            lblClassroomCount = CreateStatValueLabel();
            lblScheduleCount = CreateStatValueLabel();

            content.Controls.Add(CreateStatCard("Branches", lblBranchCount, "Academic departments and tracks"));
            content.Controls.Add(CreateStatCard("Subjects", lblSubjectCount, "Courses ready for scheduling"));
            content.Controls.Add(CreateStatCard("Classrooms", lblClassroomCount, "Rooms with capacity data"));
            content.Controls.Add(CreateStatCard("Schedules", lblScheduleCount, "Created timetable entries"));

            var note = new Guna2Panel
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
                Text = "Scheduling checks are active",
                AutoSize = true,
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = HeaderColor,
                BackColor = Color.Transparent,
                Location = new Point(22, 20)
            });
            note.Controls.Add(new Label
            {
                Text = "The app prevents faculty, classroom, and section conflicts in the same day and time slot.",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = MutedColor,
                BackColor = Color.Transparent,
                Location = new Point(24, 62)
            });
            var importButton = CreateActionButton("Import Curriculum", SuccessColor, (_, _) => ImportCurriculumFromPdf());
            importButton.Width = 170;
            importButton.Location = new Point(24, 88);
            var importScheduleButton = CreateActionButton("Import Weekly Schedule", PrimaryColor, (_, _) => ImportWeeklyScheduleFromPdf());
            importScheduleButton.Width = 190;
            importScheduleButton.Location = new Point(210, 88);
            note.Controls.Add(importButton);
            note.Controls.Add(importScheduleButton);

            content.Controls.Add(note);
            page.Controls.Add(content);
            return page;
        }

        private TabPage CreateBranchesPage()
        {
            var page = CreateDataPage("Branches", "Manage academic branches.", out var fields, out var buttons, out dgvBranches);

            txtBranchName = CreateTextBox("Computer Science");
            fields.Controls.Add(CreateField("Branch name", txtBranchName));

            buttons.Controls.Add(CreateActionButton("Add", SuccessColor, (_, _) =>
                RunCommand(() => _branchService.AddBranch(txtBranchName.Text), "Branch added successfully.", ClearBranchInputs)));
            buttons.Controls.Add(CreateActionButton("Update", PrimaryColor, (_, _) =>
                RunCommand(() => _branchService.UpdateBranch(selectedBranchId, txtBranchName.Text), "Branch updated successfully.", ClearBranchInputs)));
            buttons.Controls.Add(CreateActionButton("Delete", DangerColor, (_, _) =>
                ConfirmAndRun("Delete selected branch?", () => _branchService.DeleteBranch(selectedBranchId), "Branch deleted successfully.", ClearBranchInputs)));
            buttons.Controls.Add(CreateActionButton("Clear", MutedColor, (_, _) => ClearBranchInputs()));

            dgvBranches.CellClick += DgvBranches_CellClick;
            return page;
        }

        private TabPage CreateStudyYearsPage()
        {
            var page = CreateDataPage("Study Years", "Create and update academic year levels.", out var fields, out var buttons, out dgvStudyYears);

            txtStudyYearName = CreateTextBox("First Year");
            fields.Controls.Add(CreateField("Study year name", txtStudyYearName));

            buttons.Controls.Add(CreateActionButton("Add", SuccessColor, (_, _) =>
                RunCommand(() => _studyYearService.AddStudyYear(txtStudyYearName.Text), "Study year added successfully.", ClearStudyYearInputs)));
            buttons.Controls.Add(CreateActionButton("Update", PrimaryColor, (_, _) =>
                RunCommand(() => _studyYearService.UpdateStudyYear(selectedStudyYearId, txtStudyYearName.Text), "Study year updated successfully.", ClearStudyYearInputs)));
            buttons.Controls.Add(CreateActionButton("Delete", DangerColor, (_, _) =>
                ConfirmAndRun("Delete selected study year?", () => _studyYearService.DeleteStudyYear(selectedStudyYearId), "Study year deleted successfully.", ClearStudyYearInputs)));

            dgvStudyYears.CellClick += DgvStudyYears_CellClick;
            return page;
        }

        private TabPage CreateClassroomsPage()
        {
            var page = CreateDataPage("Classrooms", "Manage rooms, labs, and their capacity.", out var fields, out var buttons, out dgvClassrooms);

            txtClassroomNumber = CreateTextBox("A-101");
            numClassroomCapacity = CreateNumber(1, 1000, 30);
            Control roomTypeSelector = CreateRoomTypeSelector();

            fields.Controls.Add(CreateField("Room number", txtClassroomNumber));
            fields.Controls.Add(CreateField("Capacity", numClassroomCapacity, 150));
            fields.Controls.Add(CreateField("Room type", roomTypeSelector));

            buttons.Controls.Add(CreateActionButton("Add", SuccessColor, (_, _) =>
                RunCommand(() => _classroomService.AddClassroom(txtClassroomNumber.Text, (int)numClassroomCapacity.Value, SelectedRoomType()), "Classroom added successfully.", ClearClassroomInputs)));
            buttons.Controls.Add(CreateActionButton("Update", PrimaryColor, (_, _) =>
                RunCommand(() => _classroomService.UpdateClassroom(selectedClassroomId, txtClassroomNumber.Text, (int)numClassroomCapacity.Value, SelectedRoomType()), "Classroom updated successfully.", ClearClassroomInputs)));
            buttons.Controls.Add(CreateActionButton("Delete", DangerColor, (_, _) =>
                ConfirmAndRun("Delete selected classroom?", () => _classroomService.DeleteClassroom(selectedClassroomId), "Classroom deleted successfully.", ClearClassroomInputs)));

            dgvClassrooms.CellClick += DgvClassrooms_CellClick;
            return page;
        }

        private TabPage CreateFacultyPage()
        {
            var page = CreateDataPage("Faculty", "Manage instructors and academic titles.", out var fields, out var buttons, out dgvFacultyMembers);

            txtFacultyName = CreateTextBox("Dr. Sara Ahmed");
            txtAcademicTitle = CreateTextBox("Assistant Professor");

            fields.Controls.Add(CreateField("Full name", txtFacultyName));
            fields.Controls.Add(CreateField("Academic title", txtAcademicTitle));

            buttons.Controls.Add(CreateActionButton("Add", SuccessColor, (_, _) =>
                RunCommand(() => _facultyMemberService.AddFacultyMember(txtFacultyName.Text, NullIfWhiteSpace(txtAcademicTitle.Text)), "Faculty member added successfully.", ClearFacultyInputs)));
            buttons.Controls.Add(CreateActionButton("Update", PrimaryColor, (_, _) =>
                RunCommand(() => _facultyMemberService.UpdateFacultyMember(selectedFacultyMemberId, txtFacultyName.Text, NullIfWhiteSpace(txtAcademicTitle.Text)), "Faculty member updated successfully.", ClearFacultyInputs)));
            buttons.Controls.Add(CreateActionButton("Delete", DangerColor, (_, _) =>
                ConfirmAndRun("Delete selected faculty member?", () => _facultyMemberService.DeleteFacultyMember(selectedFacultyMemberId), "Faculty member deleted successfully.", ClearFacultyInputs)));
            buttons.Controls.Add(CreateActionButton("Clear", MutedColor, (_, _) => ClearFacultyInputs()));

            dgvFacultyMembers.CellClick += DgvFacultyMembers_CellClick;
            return page;
        }

        private TabPage CreateSubjectsPage()
        {
            var page = CreateDataPage("Subjects", "Manage course information and hours.", out var fields, out var buttons, out dgvSubjects);

            txtSubjectName = CreateTextBox("Algorithms");
            cmbSubjectYear = CreateCombo();
            cmbSubjectBranch = CreateCombo();
            numSemester = CreateNumber(1, 2, 1);
            numTheoreticalHours = CreateNumber(0, 20, 2);
            numPracticalHours = CreateNumber(0, 20, 0);
            numCreditUnits = CreateNumber(1, 20, 3);
            txtRequirementType = CreateTextBox("Core / Elective");

            fields.Controls.Add(CreateField("Subject name", txtSubjectName));
            fields.Controls.Add(CreateField("Study year", cmbSubjectYear));
            fields.Controls.Add(CreateField("Branch", cmbSubjectBranch));
            fields.Controls.Add(CreateField("Semester", numSemester, 130));
            fields.Controls.Add(CreateField("Theory hours", numTheoreticalHours, 150));
            fields.Controls.Add(CreateField("Practical hours", numPracticalHours, 150));
            fields.Controls.Add(CreateField("Credit units", numCreditUnits, 150));
            fields.Controls.Add(CreateField("Requirement", txtRequirementType));

            buttons.Controls.Add(CreateActionButton("Add", SuccessColor, (_, _) =>
                RunCommand(() => _subjectService.AddSubject(txtSubjectName.Text, SelectedId(cmbSubjectYear), (int)numSemester.Value, (int)numTheoreticalHours.Value, (int)numPracticalHours.Value, (int)numCreditUnits.Value, txtRequirementType.Text, SelectedOptionalId(cmbSubjectBranch)), "Subject added successfully.", ClearSubjectInputs)));
            buttons.Controls.Add(CreateActionButton("Update", PrimaryColor, (_, _) =>
                RunCommand(() => _subjectService.UpdateSubject(selectedSubjectId, txtSubjectName.Text, SelectedId(cmbSubjectYear), (int)numSemester.Value, (int)numTheoreticalHours.Value, (int)numPracticalHours.Value, (int)numCreditUnits.Value, txtRequirementType.Text, SelectedOptionalId(cmbSubjectBranch)), "Subject updated successfully.", ClearSubjectInputs)));
            buttons.Controls.Add(CreateActionButton("Delete", DangerColor, (_, _) =>
                ConfirmAndRun("Delete selected subject?", () => _subjectService.DeleteSubject(selectedSubjectId), "Subject deleted successfully.", ClearSubjectInputs)));
            buttons.Controls.Add(CreateActionButton("Clear", MutedColor, (_, _) => ClearSubjectInputs()));

            dgvSubjects.CellClick += DgvSubjects_CellClick;
            return page;
        }

        private TabPage CreateSectionsPage()
        {
            var page = CreateDataPage("Sections", "Manage student groups and their size.", out var fields, out var buttons, out dgvSections);

            txtSectionName = CreateTextBox("A");
            cmbSectionYear = CreateCombo();
            cmbSectionBranch = CreateCombo();
            numStudentCount = CreateNumber(1, 1000, 30);

            fields.Controls.Add(CreateField("Section name", txtSectionName));
            fields.Controls.Add(CreateField("Study year", cmbSectionYear));
            fields.Controls.Add(CreateField("Branch", cmbSectionBranch));
            fields.Controls.Add(CreateField("Students", numStudentCount, 150));

            buttons.Controls.Add(CreateActionButton("Add", SuccessColor, (_, _) =>
                RunCommand(() => _sectionService.AddSection(txtSectionName.Text, SelectedId(cmbSectionYear), SelectedOptionalId(cmbSectionBranch), (int)numStudentCount.Value), "Section added successfully.", ClearSectionInputs)));
            buttons.Controls.Add(CreateActionButton("Update", PrimaryColor, (_, _) =>
                RunCommand(() => _sectionService.UpdateSection(selectedSectionId, txtSectionName.Text, SelectedId(cmbSectionYear), SelectedOptionalId(cmbSectionBranch), (int)numStudentCount.Value), "Section updated successfully.", ClearSectionInputs)));
            buttons.Controls.Add(CreateActionButton("Delete", DangerColor, (_, _) =>
                ConfirmAndRun("Delete selected section?", () => _sectionService.DeleteSection(selectedSectionId), "Section deleted successfully.", ClearSectionInputs)));
            buttons.Controls.Add(CreateActionButton("Clear", MutedColor, (_, _) => ClearSectionInputs()));

            dgvSections.CellClick += DgvSections_CellClick;
            return page;
        }

        private TabPage CreateTimeSlotsPage()
        {
            var page = CreateDataPage("Time Slots", "Create teaching periods and break periods.", out var fields, out var buttons, out dgvTimeSlots);

            txtStartTime = CreateTextBox("08:30");
            txtEndTime = CreateTextBox("10:00");
            chkIsBreak = new Guna2CheckBox
            {
                Text = "Break period",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = HeaderColor,
                BackColor = Color.Transparent,
                CheckedState = { FillColor = PrimaryColor, BorderColor = PrimaryColor },
                UncheckedState = { FillColor = CardBackground, BorderColor = BorderColor },
                Margin = new Padding(0, 27, 16, 8)
            };

            fields.Controls.Add(CreateField("Start time", txtStartTime));
            fields.Controls.Add(CreateField("End time", txtEndTime));
            fields.Controls.Add(chkIsBreak);

            buttons.Controls.Add(CreateActionButton("Add", SuccessColor, (_, _) =>
                RunCommand(() => _timeSlotService.AddTimeSlot(ReadTime(txtStartTime, "Start time"), ReadTime(txtEndTime, "End time"), chkIsBreak.Checked), "Time slot added successfully.", ClearTimeSlotInputs)));
            buttons.Controls.Add(CreateActionButton("Update", PrimaryColor, (_, _) =>
                RunCommand(() => _timeSlotService.UpdateTimeSlot(selectedTimeSlotId, ReadTime(txtStartTime, "Start time"), ReadTime(txtEndTime, "End time"), chkIsBreak.Checked), "Time slot updated successfully.", ClearTimeSlotInputs)));
            buttons.Controls.Add(CreateActionButton("Delete", DangerColor, (_, _) =>
                ConfirmAndRun("Delete selected time slot?", () => _timeSlotService.DeleteTimeSlot(selectedTimeSlotId), "Time slot deleted successfully.", ClearTimeSlotInputs)));
            buttons.Controls.Add(CreateActionButton("Clear", MutedColor, (_, _) => ClearTimeSlotInputs()));

            dgvTimeSlots.CellClick += DgvTimeSlots_CellClick;
            return page;
        }

        private TabPage CreateAssignmentsPage()
        {
            var page = CreateDataPage("Teaching", "Assign instructors to subjects before scheduling.", out var fields, out var buttons, out dgvAssignments);

            cmbAssignFaculty = CreateCombo();
            cmbAssignSubject = CreateCombo();

            fields.Controls.Add(CreateField("Faculty member", cmbAssignFaculty));
            fields.Controls.Add(CreateField("Subject", cmbAssignSubject));

            buttons.Controls.Add(CreateActionButton("Assign", SuccessColor, (_, _) =>
                RunCommand(() => _assignmentService.AssignSubject(SelectedId(cmbAssignFaculty), SelectedId(cmbAssignSubject)), "Subject assigned successfully.", ClearAssignmentInputs)));
            buttons.Controls.Add(CreateActionButton("Remove", DangerColor, (_, _) =>
                ConfirmAndRun("Remove selected teaching assignment?", () => _assignmentService.RemoveAssignment(selectedAssignmentFacultyId, selectedAssignmentSubjectId), "Assignment removed successfully.", ClearAssignmentInputs)));
            buttons.Controls.Add(CreateActionButton("Clear", MutedColor, (_, _) => ClearAssignmentInputs()));

            dgvAssignments.CellClick += DgvAssignments_CellClick;
            return page;
        }

        private TabPage CreateSchedulesPage()
        {
            var page = CreateDataPage("Schedule", "Build the timetable with conflict checks.", out var fields, out var buttons, out dgvSchedules);

            cmbScheduleSubject = CreateCombo();
            cmbScheduleFaculty = CreateCombo();
            cmbScheduleClassroom = CreateCombo();
            cmbScheduleTimeSlot = CreateCombo();
            cmbScheduleDay = CreateCombo();
            cmbScheduleYear = CreateCombo();
            cmbScheduleBranch = CreateCombo();
            cmbScheduleSection = CreateCombo();

            cmbScheduleDay.Items.AddRange(new object[]
            {
                "Sunday",
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday"
            });
            cmbScheduleDay.SelectedIndex = 0;
            cmbScheduleSection.SelectedIndexChanged += (_, _) => SyncScheduleSectionContext();

            fields.Controls.Add(CreateField("Subject", cmbScheduleSubject));
            fields.Controls.Add(CreateField("Faculty", cmbScheduleFaculty));
            fields.Controls.Add(CreateField("Classroom", cmbScheduleClassroom));
            fields.Controls.Add(CreateField("Time slot", cmbScheduleTimeSlot));
            fields.Controls.Add(CreateField("Day", cmbScheduleDay));
            fields.Controls.Add(CreateField("Study year", cmbScheduleYear));
            fields.Controls.Add(CreateField("Branch", cmbScheduleBranch));
            fields.Controls.Add(CreateField("Section", cmbScheduleSection));

            var generateButton = CreateActionButton("Generate", HeaderColor, (_, _) => GenerateAutomaticSchedule());
            generateButton.Width = 132;
            buttons.Controls.Add(generateButton);
            buttons.Controls.Add(CreateActionButton("Add", SuccessColor, (_, _) =>
                RunCommand(() => _scheduleService.AddSchedule(SelectedId(cmbScheduleSubject), SelectedId(cmbScheduleFaculty), SelectedId(cmbScheduleClassroom), SelectedId(cmbScheduleTimeSlot), cmbScheduleDay.Text, SelectedOptionalId(cmbScheduleYear), SelectedOptionalId(cmbScheduleBranch), SelectedOptionalId(cmbScheduleSection)), "Schedule entry added successfully.", ClearScheduleInputs)));
            buttons.Controls.Add(CreateActionButton("Update", PrimaryColor, (_, _) =>
                RunCommand(() => _scheduleService.UpdateSchedule(selectedScheduleId, SelectedId(cmbScheduleSubject), SelectedId(cmbScheduleFaculty), SelectedId(cmbScheduleClassroom), SelectedId(cmbScheduleTimeSlot), cmbScheduleDay.Text, SelectedOptionalId(cmbScheduleYear), SelectedOptionalId(cmbScheduleBranch), SelectedOptionalId(cmbScheduleSection)), "Schedule entry updated successfully.", ClearScheduleInputs)));
            buttons.Controls.Add(CreateActionButton("Delete", DangerColor, (_, _) =>
                ConfirmAndRun("Delete selected schedule entry?", () => _scheduleService.DeleteSchedule(selectedScheduleId), "Schedule entry deleted successfully.", ClearScheduleInputs)));
            buttons.Controls.Add(CreateActionButton("Clear", MutedColor, (_, _) => ClearScheduleInputs()));

            dgvSchedules.CellClick += DgvSchedules_CellClick;
            return page;
        }

        private TabPage CreateDataPage(string title, string subtitle, out FlowLayoutPanel fields, out FlowLayoutPanel buttons, out Guna2DataGridView grid)
        {
            var page = CreateTab(title);
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                Padding = new Padding(10),
                BackColor = AppBackground
            };

            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 250));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

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

            var formCard = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BorderRadius = 12,
                FillColor = CardBackground,
                Padding = new Padding(18)
            };

            fields = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 164,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = CardBackground
            };

            buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = CardBackground
            };

            formCard.Controls.Add(buttons);
            formCard.Controls.Add(fields);

            grid = CreateGrid();

            root.Controls.Add(heading, 0, 0);
            root.Controls.Add(formCard, 0, 1);
            root.Controls.Add(grid, 0, 2);
            page.Controls.Add(root);

            return page;
        }

        private TabPage CreateTab(string title)
        {
            return new TabPage
            {
                Text = title,
                BackColor = AppBackground,
                Padding = new Padding(0)
            };
        }

        private Guna2Panel CreateStatCard(string title, Label valueLabel, string subtitle)
        {
            var card = new Guna2Panel
            {
                Width = 220,
                Height = 140,
                BorderRadius = 12,
                FillColor = CardBackground,
                Margin = new Padding(0, 0, 18, 18),
                Padding = new Padding(18)
            };

            var titleLabel = new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = MutedColor,
                BackColor = Color.Transparent,
                Location = new Point(18, 18)
            };

            valueLabel.Location = new Point(18, 46);

            var subtitleLabel = new Label
            {
                Text = subtitle,
                AutoSize = false,
                Width = 178,
                Height = 40,
                Font = new Font("Segoe UI", 9F),
                ForeColor = MutedColor,
                BackColor = Color.Transparent,
                Location = new Point(18, 94)
            };

            card.Controls.Add(titleLabel);
            card.Controls.Add(valueLabel);
            card.Controls.Add(subtitleLabel);
            return card;
        }

        private Label CreateStatValueLabel()
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

        private Guna2DataGridView CreateGrid()
        {
            var grid = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = CardBackground,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeight = 44,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                EnableHeadersVisualStyles = false,
                GridColor = BorderColor,
                MultiSelect = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Margin = new Padding(0, 14, 0, 0)
            };

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

            return grid;
        }

        private Guna2TextBox CreateTextBox(string placeholder)
        {
            return new Guna2TextBox
            {
                Width = 220,
                Height = 40,
                BorderRadius = 8,
                BorderColor = BorderColor,
                FillColor = CardBackground,
                ForeColor = HeaderColor,
                Font = new Font("Segoe UI", 10F),
                PlaceholderForeColor = Color.FromArgb(148, 163, 184),
                PlaceholderText = placeholder
            };
        }

        private Guna2ComboBox CreateCombo()
        {
            return new Guna2ComboBox
            {
                Width = 220,
                Height = 40,
                BorderRadius = 8,
                BorderColor = BorderColor,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FillColor = CardBackground,
                ForeColor = HeaderColor,
                Font = new Font("Segoe UI", 10F)
            };
        }

        private Guna2NumericUpDown CreateNumber(decimal minimum, decimal maximum, decimal value)
        {
            return new Guna2NumericUpDown
            {
                Width = 150,
                Height = 40,
                BorderRadius = 8,
                BorderColor = BorderColor,
                FillColor = CardBackground,
                ForeColor = HeaderColor,
                Font = new Font("Segoe UI", 10F),
                Minimum = minimum,
                Maximum = maximum,
                Value = value
            };
        }

        private Control CreateRoomTypeSelector()
        {
            rdoRoomLecture = CreateRoomTypeRadio("Lecture", true);
            rdoRoomLab = CreateRoomTypeRadio("Lab", false);

            var panel = new FlowLayoutPanel
            {
                Width = 220,
                Height = 40,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = CardBackground,
                Padding = new Padding(0)
            };

            panel.Controls.Add(rdoRoomLecture);
            panel.Controls.Add(rdoRoomLab);
            return panel;
        }

        private Guna2RadioButton CreateRoomTypeRadio(string text, bool isChecked)
        {
            return new Guna2RadioButton
            {
                Text = text,
                Width = 96,
                Height = 36,
                Checked = isChecked,
                Font = new Font("Segoe UI", 10F),
                ForeColor = HeaderColor,
                BackColor = Color.Transparent,
                CheckedState = { FillColor = PrimaryColor, BorderColor = PrimaryColor },
                UncheckedState = { FillColor = CardBackground, BorderColor = BorderColor },
                Margin = new Padding(0, 2, 12, 0)
            };
        }

        private Control CreateField(string labelText, Control input, int width = 220)
        {
            input.Width = width;
            input.Height = 40;
            input.Location = new Point(0, 28);

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
            panel.Controls.Add(input);

            return panel;
        }

        private Guna2Button CreateActionButton(string text, Color fillColor, EventHandler? onClick = null)
        {
            var button = new Guna2Button
            {
                Text = text,
                Width = 112,
                Height = 40,
                BorderRadius = 9,
                FillColor = fillColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(0, 6, 10, 0)
            };

            if (onClick != null)
            {
                button.Click += onClick;
            }

            return button;
        }

        private void TryLoadAllData()
        {
            try
            {
                RefreshAllData();
            }
            catch (Exception ex)
            {
                ShowError(ex, "Could not load data.");
            }
        }

        private void RefreshAllData()
        {
            _branches = _branchService.GetAllBranches();
            _studyYears = _studyYearService.GetAllStudyYears();
            _classrooms = _classroomService.GetAllClassrooms();
            _facultyMembers = _facultyMemberService.GetAllFacultyMembers();
            _subjects = _subjectService.GetAllSubjects();
            _sections = _sectionService.GetAllSections();
            _timeSlots = _timeSlotService.GetAllTimeSlots();
            _assignments = _assignmentService.GetAllAssignments();
            _schedules = _scheduleService.GetAllSchedules();

            RefreshLookupControls();
            BindGrids();
            UpdateDashboard();
        }

        private void RefreshLookupControls()
        {
            SetComboItems(cmbSubjectYear, _studyYears.Select(y => new LookupItem(y.StudyYearID, y.YearName)), false);
            SetComboItems(cmbSubjectBranch, _branches.Select(b => new LookupItem(b.BranchID, b.BranchName)), true);

            SetComboItems(cmbSectionYear, _studyYears.Select(y => new LookupItem(y.StudyYearID, y.YearName)), false);
            SetComboItems(cmbSectionBranch, _branches.Select(b => new LookupItem(b.BranchID, b.BranchName)), true);

            SetComboItems(cmbAssignFaculty, _facultyMembers.Select(f => new LookupItem(f.FacultyMemberID, f.FullName)), false);
            SetComboItems(cmbAssignSubject, _subjects.Select(s => new LookupItem(s.SubjectID, s.SubjectName)), false);

            SetComboItems(cmbScheduleSubject, _subjects.Select(s => new LookupItem(s.SubjectID, s.SubjectName)), true, "Select subject");
            SetComboItems(cmbScheduleFaculty, _facultyMembers.Select(f => new LookupItem(f.FacultyMemberID, f.FullName)), true, "Select faculty");
            SetComboItems(cmbScheduleClassroom, _classrooms.Select(c => new LookupItem(c.ClassroomID, $"{c.ClassroomNumber} ({c.Capacity})")), true, "Select classroom");
            SetComboItems(cmbScheduleTimeSlot, _timeSlots.Select(t => new LookupItem(t.TimeSlotID, $"{FormatTime(t.StartTime)} - {FormatTime(t.EndTime)}{(t.IsBreak ? " Break" : string.Empty)}")), true, "Select time slot");
            SetComboItems(cmbScheduleYear, _studyYears.Select(y => new LookupItem(y.StudyYearID, y.YearName)), true, "Select study year");
            SetComboItems(cmbScheduleBranch, _branches.Select(b => new LookupItem(b.BranchID, b.BranchName)), true);
            SetComboItems(cmbScheduleSection, _sections.Select(s => new LookupItem(s.SectionID, $"{s.SectionName} ({GetStudyYearName(s.StudyYearID)})")), true, "Select section");

            if (cmbScheduleDay.Items.Count > 0 && cmbScheduleDay.SelectedIndex < 0)
            {
                cmbScheduleDay.SelectedIndex = 0;
            }
        }

        private void BindGrids()
        {
            dgvBranches.DataSource = _branches
                .Select((b, index) => new { No = index + 1, b.BranchID, b.BranchName })
                .ToList();
            FinishGrid(dgvBranches, "BranchID");

            dgvStudyYears.DataSource = _studyYears
                .Select((y, index) => new { No = index + 1, y.StudyYearID, y.YearName })
                .ToList();
            FinishGrid(dgvStudyYears, "StudyYearID");

            dgvClassrooms.DataSource = _classrooms
                .Select((c, index) => new { No = index + 1, c.ClassroomID, c.ClassroomNumber, c.Capacity, c.RoomType })
                .ToList();
            FinishGrid(dgvClassrooms, "ClassroomID");

            dgvFacultyMembers.DataSource = _facultyMembers
                .Select((f, index) => new { No = index + 1, f.FacultyMemberID, f.FullName, f.AcademicTitle })
                .ToList();
            FinishGrid(dgvFacultyMembers, "FacultyMemberID");

            dgvSubjects.DataSource = _subjects
                .Select((s, index) => new
                {
                    No = index + 1,
                    s.SubjectID,
                    s.SubjectName,
                    s.StudyYearID,
                    StudyYear = GetStudyYearName(s.StudyYearID),
                    s.SemesterNumber,
                    s.TheoreticalHours,
                    s.PracticalHours,
                    s.CreditUnits,
                    s.RequirementType,
                    s.BranchID,
                    Branch = GetBranchName(s.BranchID)
                })
                .ToList();
            FinishGrid(dgvSubjects, "SubjectID", "StudyYearID", "BranchID");

            dgvSections.DataSource = _sections
                .Select((s, index) => new
                {
                    No = index + 1,
                    s.SectionID,
                    s.SectionName,
                    s.StudentCount,
                    s.StudyYearID,
                    StudyYear = GetStudyYearName(s.StudyYearID),
                    s.BranchID,
                    Branch = GetBranchName(s.BranchID)
                })
                .ToList();
            FinishGrid(dgvSections, "SectionID", "StudyYearID", "BranchID");

            dgvTimeSlots.DataSource = _timeSlots
                .Select((t, index) => new
                {
                    No = index + 1,
                    t.TimeSlotID,
                    StartTime = FormatTime(t.StartTime),
                    EndTime = FormatTime(t.EndTime),
                    t.IsBreak
                })
                .ToList();
            FinishGrid(dgvTimeSlots, "TimeSlotID");

            dgvAssignments.DataSource = _assignments
                .Select((a, index) => new
                {
                    No = index + 1,
                    a.FacultyMemberID,
                    FacultyMember = a.FacultyMember.FullName,
                    a.SubjectID,
                    Subject = a.Subject.SubjectName
                })
                .ToList();
            FinishGrid(dgvAssignments, "FacultyMemberID", "SubjectID");

            dgvSchedules.DataSource = _schedules
                .Select((s, index) => new
                {
                    No = index + 1,
                    s.ScheduleID,
                    s.DayOfWeek,
                    Time = $"{FormatTime(s.TimeSlot.StartTime)} - {FormatTime(s.TimeSlot.EndTime)}",
                    s.SubjectID,
                    Subject = s.Subject.SubjectName,
                    s.FacultyMemberID,
                    Faculty = s.FacultyMember.FullName,
                    s.ClassroomID,
                    Classroom = s.Classroom.ClassroomNumber,
                    s.TimeSlotID,
                    s.StudyYearID,
                    StudyYear = s.StudyYear?.YearName ?? "-",
                    s.BranchID,
                    Branch = s.Branch?.BranchName ?? "-",
                    s.SectionID,
                    Section = s.Section?.SectionName ?? "-"
                })
                .ToList();
            FinishGrid(dgvSchedules, "ScheduleID", "SubjectID", "FacultyMemberID", "ClassroomID", "TimeSlotID", "StudyYearID", "BranchID", "SectionID");
        }

        private void UpdateDashboard()
        {
            lblBranchCount.Text = _branches.Count.ToString(CultureInfo.InvariantCulture);
            lblSubjectCount.Text = _subjects.Count.ToString(CultureInfo.InvariantCulture);
            lblClassroomCount.Text = _classrooms.Count.ToString(CultureInfo.InvariantCulture);
            lblScheduleCount.Text = _schedules.Count.ToString(CultureInfo.InvariantCulture);
        }

        private void RunCommand(Action action, string successMessage, Action clearAction)
        {
            try
            {
                action();
                RefreshAllData();
                clearAction();
                ShowInfo(successMessage);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ConfirmAndRun(string question, Action action, string successMessage, Action clearAction)
        {
            DialogResult result = MessageBox.Show(question, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                RunCommand(action, successMessage, clearAction);
            }
        }

        private void ImportCurriculumFromPdf()
        {
            DialogResult confirmation = MessageBox.Show(
                "Import the curriculum subjects extracted from the PDF?",
                "Import Curriculum",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                CurriculumImportResult result = _curriculumImportService.ImportComputerScienceCurriculum();
                RefreshAllData();
                ShowInfo(BuildCurriculumImportMessage(result));
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ImportWeeklyScheduleFromPdf()
        {
            DialogResult confirmation = MessageBox.Show(
                "Import the weekly schedule extracted from the 2025-2026 second semester PDF?",
                "Import Weekly Schedule",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                WeeklyScheduleImportResult result = _weeklyScheduleImportService.ImportSecondSemester2026Schedule();
                RefreshAllData();
                ShowInfo(BuildWeeklyScheduleImportMessage(result));
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void GenerateAutomaticSchedule()
        {
            DialogResult confirmation = MessageBox.Show(
                $"Generate a new semester {AutomaticScheduleSemester} timetable and replace all existing schedule entries?",
                "Generate Schedule",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                ScheduleGenerationResult result = _scheduleService.GenerateAutomaticSchedule(replaceExistingSchedules: true, AutomaticScheduleSemester);
                RefreshAllData();
                ClearScheduleInputs();
                ShowInfo(BuildGenerationMessage(result));
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void DgvBranches_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (!TryGetRow(dgvBranches, e.RowIndex, out DataGridViewRow row))
            {
                return;
            }

            selectedBranchId = GetGridInt(row, "BranchID");
            txtBranchName.Text = GetGridText(row, "BranchName");
        }

        private void DgvStudyYears_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (!TryGetRow(dgvStudyYears, e.RowIndex, out DataGridViewRow row))
            {
                return;
            }

            selectedStudyYearId = GetGridInt(row, "StudyYearID");
            txtStudyYearName.Text = GetGridText(row, "YearName");
        }

        private void DgvClassrooms_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (!TryGetRow(dgvClassrooms, e.RowIndex, out DataGridViewRow row))
            {
                return;
            }

            selectedClassroomId = GetGridInt(row, "ClassroomID");
            txtClassroomNumber.Text = GetGridText(row, "ClassroomNumber");
            numClassroomCapacity.Value = GetGridDecimal(row, "Capacity");
            SetSelectedRoomType(GetGridText(row, "RoomType"));
        }

        private void DgvFacultyMembers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (!TryGetRow(dgvFacultyMembers, e.RowIndex, out DataGridViewRow row))
            {
                return;
            }

            selectedFacultyMemberId = GetGridInt(row, "FacultyMemberID");
            txtFacultyName.Text = GetGridText(row, "FullName");
            txtAcademicTitle.Text = GetGridText(row, "AcademicTitle");
        }

        private void DgvSubjects_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (!TryGetRow(dgvSubjects, e.RowIndex, out DataGridViewRow row))
            {
                return;
            }

            selectedSubjectId = GetGridInt(row, "SubjectID");
            txtSubjectName.Text = GetGridText(row, "SubjectName");
            SetComboValue(cmbSubjectYear, GetGridInt(row, "StudyYearID"));
            SetComboValue(cmbSubjectBranch, GetGridNullableInt(row, "BranchID") ?? 0);
            numSemester.Value = GetGridDecimal(row, "SemesterNumber");
            numTheoreticalHours.Value = GetGridDecimal(row, "TheoreticalHours");
            numPracticalHours.Value = GetGridDecimal(row, "PracticalHours");
            numCreditUnits.Value = GetGridDecimal(row, "CreditUnits");
            txtRequirementType.Text = GetGridText(row, "RequirementType");
        }

        private void DgvSections_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (!TryGetRow(dgvSections, e.RowIndex, out DataGridViewRow row))
            {
                return;
            }

            selectedSectionId = GetGridInt(row, "SectionID");
            txtSectionName.Text = GetGridText(row, "SectionName");
            numStudentCount.Value = GetGridDecimal(row, "StudentCount");
            SetComboValue(cmbSectionYear, GetGridInt(row, "StudyYearID"));
            SetComboValue(cmbSectionBranch, GetGridNullableInt(row, "BranchID") ?? 0);
        }

        private void DgvTimeSlots_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (!TryGetRow(dgvTimeSlots, e.RowIndex, out DataGridViewRow row))
            {
                return;
            }

            selectedTimeSlotId = GetGridInt(row, "TimeSlotID");
            txtStartTime.Text = GetGridText(row, "StartTime");
            txtEndTime.Text = GetGridText(row, "EndTime");
            chkIsBreak.Checked = GetGridBool(row, "IsBreak");
        }

        private void DgvAssignments_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (!TryGetRow(dgvAssignments, e.RowIndex, out DataGridViewRow row))
            {
                return;
            }

            selectedAssignmentFacultyId = GetGridInt(row, "FacultyMemberID");
            selectedAssignmentSubjectId = GetGridInt(row, "SubjectID");
            SetComboValue(cmbAssignFaculty, selectedAssignmentFacultyId);
            SetComboValue(cmbAssignSubject, selectedAssignmentSubjectId);
        }

        private void DgvSchedules_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (!TryGetRow(dgvSchedules, e.RowIndex, out DataGridViewRow row))
            {
                return;
            }

            selectedScheduleId = GetGridInt(row, "ScheduleID");
            SetComboValue(cmbScheduleSubject, GetGridInt(row, "SubjectID"));
            SetComboValue(cmbScheduleFaculty, GetGridInt(row, "FacultyMemberID"));
            SetComboValue(cmbScheduleClassroom, GetGridInt(row, "ClassroomID"));
            SetComboValue(cmbScheduleTimeSlot, GetGridInt(row, "TimeSlotID"));
            cmbScheduleDay.SelectedItem = GetGridText(row, "DayOfWeek");
            SetComboValue(cmbScheduleYear, GetGridNullableInt(row, "StudyYearID") ?? 0);
            SetComboValue(cmbScheduleBranch, GetGridNullableInt(row, "BranchID") ?? 0);
            SetComboValue(cmbScheduleSection, GetGridNullableInt(row, "SectionID") ?? 0);
        }

        private void ClearBranchInputs()
        {
            selectedBranchId = 0;
            txtBranchName.Clear();
            dgvBranches.ClearSelection();
        }

        private void ClearStudyYearInputs()
        {
            selectedStudyYearId = 0;
            txtStudyYearName.Clear();
            dgvStudyYears.ClearSelection();
        }

        private void ClearClassroomInputs()
        {
            selectedClassroomId = 0;
            txtClassroomNumber.Clear();
            numClassroomCapacity.Value = 30;
            SetSelectedRoomType("Lecture");
            dgvClassrooms.ClearSelection();
        }

        private void ClearFacultyInputs()
        {
            selectedFacultyMemberId = 0;
            txtFacultyName.Clear();
            txtAcademicTitle.Clear();
            dgvFacultyMembers.ClearSelection();
        }

        private void ClearSubjectInputs()
        {
            selectedSubjectId = 0;
            txtSubjectName.Clear();
            txtRequirementType.Clear();
            numSemester.Value = 1;
            numTheoreticalHours.Value = 2;
            numPracticalHours.Value = 0;
            numCreditUnits.Value = 3;
            SelectFirst(cmbSubjectYear);
            SetComboValue(cmbSubjectBranch, 0);
            dgvSubjects.ClearSelection();
        }

        private void ClearSectionInputs()
        {
            selectedSectionId = 0;
            txtSectionName.Clear();
            numStudentCount.Value = 30;
            SelectFirst(cmbSectionYear);
            SetComboValue(cmbSectionBranch, 0);
            dgvSections.ClearSelection();
        }

        private void ClearTimeSlotInputs()
        {
            selectedTimeSlotId = 0;
            txtStartTime.Text = "08:30";
            txtEndTime.Text = "10:00";
            chkIsBreak.Checked = false;
            dgvTimeSlots.ClearSelection();
        }

        private void ClearAssignmentInputs()
        {
            selectedAssignmentFacultyId = 0;
            selectedAssignmentSubjectId = 0;
            SelectFirst(cmbAssignFaculty);
            SelectFirst(cmbAssignSubject);
            dgvAssignments.ClearSelection();
        }

        private void ClearScheduleInputs()
        {
            selectedScheduleId = 0;
            SetComboValue(cmbScheduleSubject, 0);
            SetComboValue(cmbScheduleFaculty, 0);
            SetComboValue(cmbScheduleClassroom, 0);
            SetComboValue(cmbScheduleTimeSlot, 0);
            cmbScheduleDay.SelectedIndex = -1;
            SetComboValue(cmbScheduleYear, 0);
            SetComboValue(cmbScheduleBranch, 0);
            SetComboValue(cmbScheduleSection, 0);
            ClearGridSelection(dgvSchedules);
        }

        private void SyncScheduleSectionContext()
        {
            int sectionId = SelectedId(cmbScheduleSection);
            Section? section = _sections.FirstOrDefault(s => s.SectionID == sectionId);

            if (section == null)
            {
                return;
            }

            SetComboValue(cmbScheduleYear, section.StudyYearID);
            SetComboValue(cmbScheduleBranch, section.BranchID ?? 0);
        }

        private string GetBranchName(int? branchId)
        {
            return branchId.HasValue
                ? _branches.FirstOrDefault(b => b.BranchID == branchId.Value)?.BranchName ?? "-"
                : "-";
        }

        private string GetStudyYearName(int studyYearId)
        {
            return _studyYears.FirstOrDefault(y => y.StudyYearID == studyYearId)?.YearName ?? "-";
        }

        private string SelectedRoomType()
        {
            return rdoRoomLab.Checked ? "Lab" : "Lecture";
        }

        private void SetSelectedRoomType(string? roomType)
        {
            bool isLab = string.Equals(roomType?.Trim(), "Lab", StringComparison.OrdinalIgnoreCase);
            rdoRoomLab.Checked = isLab;
            rdoRoomLecture.Checked = !isLab;
        }

        private static string FormatTime(TimeSpan time)
        {
            return time.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
        }

        private static TimeSpan ReadTime(Guna2TextBox textBox, string label)
        {
            string value = textBox.Text.Trim();

            if (!TimeSpan.TryParseExact(value, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan time))
            {
                throw new Exception(label + " must use HH:mm format.");
            }

            return time;
        }

        private static void FinishGrid(DataGridView grid, params string[] hiddenColumns)
        {
            if (grid.Columns.Contains("No"))
            {
                grid.Columns["No"].HeaderText = "ID";
                grid.Columns["No"].DisplayIndex = 0;
                grid.Columns["No"].FillWeight = 45;
                grid.Columns["No"].MinimumWidth = 55;
            }

            ApplyFriendlyColumnHeaders(grid);

            foreach (string columnName in hiddenColumns)
            {
                if (grid.Columns.Contains(columnName))
                {
                    grid.Columns[columnName].Visible = false;
                }
            }

            ClearGridSelection(grid);
        }

        private static void ApplyFriendlyColumnHeaders(DataGridView grid)
        {
            SetHeader(grid, "BranchName", "Branch");
            SetHeader(grid, "YearName", "Study Year");
            SetHeader(grid, "ClassroomNumber", "Classroom");
            SetHeader(grid, "RoomType", "Room Type");
            SetHeader(grid, "FullName", "Full Name");
            SetHeader(grid, "AcademicTitle", "Academic Title");
            SetHeader(grid, "SubjectName", "Subject");
            SetHeader(grid, "SemesterNumber", "Semester");
            SetHeader(grid, "TheoreticalHours", "Theory Hours");
            SetHeader(grid, "PracticalHours", "Practical Hours");
            SetHeader(grid, "CreditUnits", "Credit Units");
            SetHeader(grid, "RequirementType", "Requirement");
            SetHeader(grid, "SectionName", "Section");
            SetHeader(grid, "StudentCount", "Students");
            SetHeader(grid, "StartTime", "Start Time");
            SetHeader(grid, "EndTime", "End Time");
            SetHeader(grid, "IsBreak", "Break");
            SetHeader(grid, "DayOfWeek", "Day");
        }

        private static void SetHeader(DataGridView grid, string columnName, string headerText)
        {
            if (grid.Columns.Contains(columnName))
            {
                grid.Columns[columnName].HeaderText = headerText;
            }
        }

        private static void ClearGridSelection(DataGridView grid)
        {
            grid.ClearSelection();

            try
            {
                grid.CurrentCell = null;
            }
            catch
            {
                // Some grid states briefly reject clearing CurrentCell during rebinding.
            }
        }

        private static void SetComboItems(Guna2ComboBox comboBox, IEnumerable<LookupItem> items, bool includeEmpty, string emptyLabel = "None")
        {
            var source = items.ToList();

            if (includeEmpty)
            {
                source.Insert(0, new LookupItem(0, emptyLabel));
            }

            comboBox.DisplayMember = nameof(LookupItem.Name);
            comboBox.ValueMember = nameof(LookupItem.Id);
            comboBox.DataSource = source;

            if (source.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private static int SelectedId(Guna2ComboBox comboBox)
        {
            if (comboBox.SelectedValue is int id)
            {
                return id;
            }

            if (comboBox.SelectedItem is LookupItem item)
            {
                return item.Id;
            }

            return 0;
        }

        private static int? SelectedOptionalId(Guna2ComboBox comboBox)
        {
            int id = SelectedId(comboBox);
            return id > 0 ? id : null;
        }

        private static void SetComboValue(Guna2ComboBox comboBox, int id)
        {
            try
            {
                comboBox.SelectedValue = id;
            }
            catch
            {
                SelectFirst(comboBox);
            }
        }

        private static void SelectFirst(Guna2ComboBox comboBox)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private static bool TryGetRow(DataGridView grid, int rowIndex, out DataGridViewRow row)
        {
            row = null!;

            if (rowIndex < 0 || rowIndex >= grid.Rows.Count)
            {
                return false;
            }

            row = grid.Rows[rowIndex];
            return true;
        }

        private static string GetGridText(DataGridViewRow row, string columnName)
        {
            object? value = row.Cells[columnName].Value;
            return value == null || value == DBNull.Value ? string.Empty : value.ToString() ?? string.Empty;
        }

        private static int GetGridInt(DataGridViewRow row, string columnName)
        {
            object? value = row.Cells[columnName].Value;
            return value == null || value == DBNull.Value ? 0 : Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        private static int? GetGridNullableInt(DataGridViewRow row, string columnName)
        {
            object? value = row.Cells[columnName].Value;
            return value == null || value == DBNull.Value ? null : Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        private static decimal GetGridDecimal(DataGridViewRow row, string columnName)
        {
            object? value = row.Cells[columnName].Value;
            return value == null || value == DBNull.Value ? 0 : Convert.ToDecimal(value, CultureInfo.InvariantCulture);
        }

        private static bool GetGridBool(DataGridViewRow row, string columnName)
        {
            object? value = row.Cells[columnName].Value;
            return value != null && value != DBNull.Value && Convert.ToBoolean(value, CultureInfo.InvariantCulture);
        }

        private static string? NullIfWhiteSpace(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private static void ShowInfo(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string BuildCurriculumImportMessage(CurriculumImportResult result)
        {
            return string.Join(
                Environment.NewLine,
                $"Study years ready: {result.StudyYearCount}",
                $"Branches ready: {result.BranchCount}",
                $"Subjects in PDF data: {result.TotalSubjectCount}",
                $"Added subjects: {result.AddedSubjectCount}",
                $"Updated subjects: {result.UpdatedSubjectCount}");
        }

        private static string BuildWeeklyScheduleImportMessage(WeeklyScheduleImportResult result)
        {
            var lines = new List<string>
            {
                $"Added schedule entries: {result.AddedScheduleCount}",
                $"Already existing entries: {result.ExistingScheduleCount}",
                $"Skipped conflicting entries: {result.SkippedScheduleCount}"
            };

            if (result.Warnings.Count > 0)
            {
                lines.Add(string.Empty);
                lines.Add("Notes:");
                lines.AddRange(result.Warnings.Take(8).Select(warning => "- " + warning));

                if (result.Warnings.Count > 8)
                {
                    lines.Add($"- Plus {result.Warnings.Count - 8} more notes.");
                }
            }

            return string.Join(Environment.NewLine, lines);
        }

        private static string BuildGenerationMessage(ScheduleGenerationResult result)
        {
            var lines = new List<string>
            {
                result.SemesterNumber.HasValue
                    ? $"Generated {result.CreatedCount} schedule entries for semester {result.SemesterNumber.Value}."
                    : $"Generated {result.CreatedCount} schedule entries."
            };

            if (result.SkippedCount > 0)
            {
                lines.Add($"Skipped {result.SkippedCount} entries that could not fit.");
            }

            if (result.Warnings.Count > 0)
            {
                lines.Add(string.Empty);
                lines.Add("Notes:");
                lines.AddRange(result.Warnings.Take(8).Select(warning => "- " + warning));

                if (result.Warnings.Count > 8)
                {
                    lines.Add($"- Plus {result.Warnings.Count - 8} more notes.");
                }
            }

            return string.Join(Environment.NewLine, lines);
        }

        private static void ShowError(Exception exception, string? prefix = null)
        {
            MessageBox.Show(BuildErrorMessage(exception, prefix), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static string BuildErrorMessage(Exception exception, string? prefix)
        {
            List<string> parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(prefix))
            {
                parts.Add(prefix);
            }

            parts.Add(exception.Message);

            Exception? innerException = exception.InnerException;

            while (innerException != null)
            {
                if (!string.IsNullOrWhiteSpace(innerException.Message))
                {
                    parts.Add(innerException.Message);
                }

                innerException = innerException.InnerException;
            }

            if (parts.Any(part => part.Contains("UQ_Section_Time", StringComparison.OrdinalIgnoreCase)))
            {
                return "This section already has a class at this day and time. Choose another day/time or edit the existing schedule entry.";
            }

            if (parts.Any(part => part.Contains("UQ_Year_Branch_Time", StringComparison.OrdinalIgnoreCase)))
            {
                return "The database still has the old year/branch schedule constraint. Restart the app so it can update the schedule schema, then try again.";
            }

            return string.Join(Environment.NewLine + Environment.NewLine, parts.Distinct());
        }

        private sealed class LookupItem
        {
            public LookupItem(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; }

            public string Name { get; }
        }
    }
}
