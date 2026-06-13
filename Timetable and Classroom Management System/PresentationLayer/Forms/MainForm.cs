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

        private int selectedBranchId;

        private int selectedStudyYearId;

        private int selectedClassroomId;

        private int selectedFacultyMemberId;

        private int selectedSubjectId;

        private int selectedSectionId;

        private int selectedTimeSlotId;

        private int selectedAssignmentFacultyId;
        private int selectedAssignmentSubjectId;

        private int selectedScheduleId;
        private bool isClearingScheduleInputs;

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
            WireDesignerPages();

        }

        private void WireDesignerPages()
        {
            btnRefresh.Click += (_, _) => TryLoadAllData();

            btnBranchAdd.Click += (_, _) =>
                RunCommand(() => _branchService.AddBranch(txtBranchName.Text), "Branch added successfully.", ClearBranchInputs);
            btnBranchUpdate.Click += (_, _) =>
                RunCommand(() => _branchService.UpdateBranch(selectedBranchId, txtBranchName.Text), "Branch updated successfully.", ClearBranchInputs);
            btnBranchDelete.Click += (_, _) =>
                ConfirmAndRun("Delete selected branch?", () => _branchService.DeleteBranch(selectedBranchId), "Branch deleted successfully.", ClearBranchInputs);
            btnBranchClear.Click += (_, _) => ClearBranchInputs();
            dgvBranches.CellClick += DgvBranches_CellClick;

            btnStudyYearAdd.Click += (_, _) =>
                RunCommand(() => _studyYearService.AddStudyYear(txtStudyYearName.Text), "Study year added successfully.", ClearStudyYearInputs);
            btnStudyYearUpdate.Click += (_, _) =>
                RunCommand(() => _studyYearService.UpdateStudyYear(selectedStudyYearId, txtStudyYearName.Text), "Study year updated successfully.", ClearStudyYearInputs);
            btnStudyYearDelete.Click += (_, _) =>
                ConfirmAndRun("Delete selected study year?", () => _studyYearService.DeleteStudyYear(selectedStudyYearId), "Study year deleted successfully.", ClearStudyYearInputs);
            dgvStudyYears.CellClick += DgvStudyYears_CellClick;

            btnClassroomAdd.Click += (_, _) =>
                RunCommand(() => _classroomService.AddClassroom(txtClassroomNumber.Text, (int)numClassroomCapacity.Value, SelectedRoomType()), "Classroom added successfully.", ClearClassroomInputs);
            btnClassroomUpdate.Click += (_, _) =>
                RunCommand(() => _classroomService.UpdateClassroom(selectedClassroomId, txtClassroomNumber.Text, (int)numClassroomCapacity.Value, SelectedRoomType()), "Classroom updated successfully.", ClearClassroomInputs);
            btnClassroomDelete.Click += (_, _) =>
                ConfirmAndRun("Delete selected classroom?", () => _classroomService.DeleteClassroom(selectedClassroomId), "Classroom deleted successfully.", ClearClassroomInputs);
            dgvClassrooms.CellClick += DgvClassrooms_CellClick;

            btnFacultyAdd.Click += (_, _) =>
                RunCommand(() => _facultyMemberService.AddFacultyMember(txtFacultyName.Text, SelectedAcademicTitle()), "Faculty member added successfully.", ClearFacultyInputs);
            btnFacultyUpdate.Click += (_, _) =>
                RunCommand(() => _facultyMemberService.UpdateFacultyMember(selectedFacultyMemberId, txtFacultyName.Text, SelectedAcademicTitle()), "Faculty member updated successfully.", ClearFacultyInputs);
            btnFacultyDelete.Click += (_, _) =>
                ConfirmAndRun("Delete selected faculty member?", () => _facultyMemberService.DeleteFacultyMember(selectedFacultyMemberId), "Faculty member deleted successfully.", ClearFacultyInputs);
            dgvFacultyMembers.CellClick += DgvFacultyMembers_CellClick;

            cmbSubjectFilterYear.SelectedIndexChanged += (_, _) => BindSubjectsGrid();
            cmbSubjectFilterBranch.SelectedIndexChanged += (_, _) => BindSubjectsGrid();
            cmbSubjectFilterSemester.SelectedIndexChanged += (_, _) => BindSubjectsGrid();
            btnSubjectAdd.Click += (_, _) =>
                RunCommand(() => _subjectService.AddSubject(txtSubjectName.Text, SelectedId(cmbSubjectYear), (int)numSemester.Value, (double)numTheoreticalHours.Value, (double)numPracticalHours.Value, (double)numCreditUnits.Value, SelectedOptionalId(cmbSubjectBranch)), "Subject added successfully.", ClearSubjectInputs);
            btnSubjectUpdate.Click += (_, _) =>
                RunCommand(() => _subjectService.UpdateSubject(selectedSubjectId, txtSubjectName.Text, SelectedId(cmbSubjectYear), (int)numSemester.Value, (double)numTheoreticalHours.Value, (double)numPracticalHours.Value, (double)numCreditUnits.Value, SelectedOptionalId(cmbSubjectBranch)), "Subject updated successfully.", ClearSubjectInputs);
            btnSubjectDelete.Click += (_, _) =>
                ConfirmAndRun("Delete selected subject?", () => _subjectService.DeleteSubject(selectedSubjectId), "Subject deleted successfully.", ClearSubjectInputs);
            dgvSubjects.CellClick += DgvSubjects_CellClick;

            cmbSectionYear.SelectedIndexChanged += (_, _) => RefreshSectionBranchOptions();
            btnSectionAdd.Click += (_, _) =>
                RunCommand(() => _sectionService.AddSection(txtSectionName.Text, SelectedId(cmbSectionYear), SelectedOptionalId(cmbSectionBranch), (int)numStudentCount.Value), "Section added successfully.", ClearSectionInputs);
            btnSectionUpdate.Click += (_, _) =>
                RunCommand(() => _sectionService.UpdateSection(selectedSectionId, txtSectionName.Text, SelectedId(cmbSectionYear), SelectedOptionalId(cmbSectionBranch), (int)numStudentCount.Value), "Section updated successfully.", ClearSectionInputs);
            btnSectionDelete.Click += (_, _) =>
                ConfirmAndRun("Delete selected section?", () => _sectionService.DeleteSection(selectedSectionId), "Section deleted successfully.", ClearSectionInputs);
            dgvSections.CellClick += DgvSections_CellClick;

            btnTimeSlotAdd.Click += (_, _) =>
                RunCommand(() => _timeSlotService.AddTimeSlot(ReadTime(txtStartTime, "Start time"), ReadTime(txtEndTime, "End time"), chkIsBreak.Checked), "Time slot added successfully.", ClearTimeSlotInputs);
            btnTimeSlotUpdate.Click += (_, _) =>
                RunCommand(() => _timeSlotService.UpdateTimeSlot(selectedTimeSlotId, ReadTime(txtStartTime, "Start time"), ReadTime(txtEndTime, "End time"), chkIsBreak.Checked), "Time slot updated successfully.", ClearTimeSlotInputs);
            btnTimeSlotDelete.Click += (_, _) =>
                ConfirmAndRun("Delete selected time slot?", () => _timeSlotService.DeleteTimeSlot(selectedTimeSlotId), "Time slot deleted successfully.", ClearTimeSlotInputs);
            btnTimeSlotClear.Click += (_, _) => ClearTimeSlotInputs();
            dgvTimeSlots.CellClick += DgvTimeSlots_CellClick;

            btnAssignmentAdd.Click += (_, _) =>
                RunCommand(() => _assignmentService.AssignSubject(SelectedId(cmbAssignFaculty), SelectedId(cmbAssignSubject)), "Subject assigned successfully.", ClearAssignmentInputs);
            btnAssignmentRemove.Click += (_, _) =>
                ConfirmAndRun("Remove selected teaching assignment?", () => _assignmentService.RemoveAssignment(selectedAssignmentFacultyId, selectedAssignmentSubjectId), "Assignment removed successfully.", ClearAssignmentInputs);
            btnAssignmentClear.Click += (_, _) => ClearAssignmentInputs();
            dgvAssignments.CellClick += DgvAssignments_CellClick;

            cmbScheduleYear.SelectedIndexChanged += (_, _) => RefreshScheduleSubjectOptions();
            cmbScheduleBranch.SelectedIndexChanged += (_, _) => RefreshScheduleSubjectOptions();
            cmbScheduleSection.SelectedIndexChanged += (_, _) => SyncScheduleSectionContext();
            cmbScheduleFilterFaculty.SelectedIndexChanged += (_, _) => BindSchedulesGrid();
            cmbScheduleFilterSection.SelectedIndexChanged += (_, _) => BindSchedulesGrid();
            cmbScheduleFilterYear.SelectedIndexChanged += (_, _) =>
            {
                RefreshScheduleFilterSectionOptions();
                BindSchedulesGrid();
            };
            cmbScheduleFilterDay.SelectedIndexChanged += (_, _) => BindSchedulesGrid();
            btnScheduleGenerate.Click += (_, _) => GenerateAutomaticSchedule();
            btnScheduleAdd.Click += (_, _) =>
                RunCommand(() => _scheduleService.AddSchedule(SelectedId(cmbScheduleSubject), SelectedId(cmbScheduleFaculty), SelectedId(cmbScheduleClassroom), SelectedId(cmbScheduleTimeSlot), ReadScheduleDay(), SelectedOptionalId(cmbScheduleYear), SelectedOptionalId(cmbScheduleBranch), SelectedOptionalId(cmbScheduleSection)), "Schedule entry added successfully.", ClearScheduleInputs);
            btnScheduleUpdate.Click += (_, _) =>
                RunCommand(() => _scheduleService.UpdateSchedule(selectedScheduleId, SelectedId(cmbScheduleSubject), SelectedId(cmbScheduleFaculty), SelectedId(cmbScheduleClassroom), SelectedId(cmbScheduleTimeSlot), ReadScheduleDay(), SelectedOptionalId(cmbScheduleYear), SelectedOptionalId(cmbScheduleBranch), SelectedOptionalId(cmbScheduleSection)), "Schedule entry updated successfully.", ClearScheduleInputs);
            btnScheduleDelete.Click += (_, _) =>
                ConfirmAndRun("Delete selected schedule entry?", () => _scheduleService.DeleteSchedule(selectedScheduleId), "Schedule entry deleted successfully.", ClearScheduleInputs);
            btnScheduleClear.Click += (_, _) => ClearScheduleInputs();
            btnScheduleExport.Click += (_, _) => ExportSchedulesToPdf();
            dgvSchedules.CellClick += DgvSchedules_CellClick;
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
            SetComboItems(cmbSubjectFilterYear, _studyYears.Select(y => new LookupItem(y.StudyYearID, y.YearName)), true, "All study years");
            SetComboItems(cmbSubjectFilterBranch, _branches.Select(b => new LookupItem(b.BranchID, b.BranchName)), true, "All branches");
            SetComboItems(cmbSubjectFilterSemester, new[]
            {
                new LookupItem(0, "All semesters"),
                new LookupItem(1, "Semester 1"),
                new LookupItem(2, "Semester 2")
            }, false);

            SetComboItems(cmbSectionYear, _studyYears.Select(y => new LookupItem(y.StudyYearID, y.YearName)), false);
            RefreshSectionBranchOptions();

            SetComboItems(cmbAssignFaculty, _facultyMembers.Select(f => new LookupItem(f.FacultyMemberID, f.FullName)), false);
            SetComboItems(cmbAssignSubject, _subjects.Select(s => new LookupItem(s.SubjectID, s.SubjectName)), false);

            SetComboItems(cmbScheduleFaculty, _facultyMembers.Select(f => new LookupItem(f.FacultyMemberID, f.FullName)), true, "Select faculty");
            SetComboItems(cmbScheduleClassroom, _classrooms.Select(c => new LookupItem(c.ClassroomID, $"{c.ClassroomNumber} ({c.Capacity})")), true, "Select classroom");
            SetComboItems(cmbScheduleTimeSlot, _timeSlots.Where(t => !t.IsBreak).Select(t => new LookupItem(t.TimeSlotID, $"{FormatTime(t.StartTime)} - {FormatTime(t.EndTime)}")), true, "Select time slot");
            SetComboItems(cmbScheduleYear, _studyYears.Select(y => new LookupItem(y.StudyYearID, y.YearName)), true, "Select study year");
            SetComboItems(cmbScheduleBranch, _branches.Select(b => new LookupItem(b.BranchID, b.BranchName)), true);
            SetComboItems(cmbScheduleSection, _sections.Select(s => new LookupItem(s.SectionID, FormatSectionDisplayName(s))), true, "Select section");
            RefreshScheduleSubjectOptions();
            SetComboItems(cmbScheduleFilterFaculty, _facultyMembers.Select(f => new LookupItem(f.FacultyMemberID, f.FullName)), true, "All faculty");
            SetComboItems(cmbScheduleFilterYear, _studyYears.Select(y => new LookupItem(y.StudyYearID, y.YearName)), true, "All study years");
            RefreshScheduleFilterSectionOptions();

            if (cmbScheduleDay.Items.Count > 0 && cmbScheduleDay.SelectedIndex < 0)
            {
                cmbScheduleDay.SelectedIndex = 0;
            }
        }

        private void RefreshSectionBranchOptions(int preferredBranchId = 0)
        {
            int studyYearId = SelectedId(cmbSectionYear);
            List<LookupItem> branchItems = GetSectionBranchItems(studyYearId).ToList();
            bool hasBranchOptions = branchItems.Count > 0;

            SetComboItems(cmbSectionBranch, branchItems, true, hasBranchOptions ? "Select branch" : "None");
            cmbSectionBranch.Enabled = hasBranchOptions;

            if (preferredBranchId > 0)
            {
                SetComboValue(cmbSectionBranch, preferredBranchId);
            }
        }

        private IEnumerable<LookupItem> GetSectionBranchItems(int studyYearId)
        {
            if (studyYearId <= 0)
            {
                return Enumerable.Empty<LookupItem>();
            }

            HashSet<int> validBranchIds = _subjects
                .Where(s => s.StudyYearID == studyYearId && s.BranchID.HasValue)
                .Select(s => s.BranchID!.Value)
                .ToHashSet();

            return _branches
                .Where(b => validBranchIds.Contains(b.BranchID))
                .Select(b => new LookupItem(b.BranchID, b.BranchName));
        }

        private void RefreshScheduleFilterSectionOptions()
        {
            int selectedStudyYearId = SelectedId(cmbScheduleFilterYear);
            List<Section> filterSections = _sections
                .OrderBy(s => s.StudyYearID)
                .ThenBy(s => s.BranchID ?? 0)
                .ThenBy(s => s.SectionName)
                .ToList();

            if (selectedStudyYearId > 0)
            {
                filterSections = filterSections
                    .Where(s => s.StudyYearID == selectedStudyYearId)
                    .ToList();
            }

            string emptyLabel = selectedStudyYearId > 0 ? "All sections in year" : "All sections";

            SetComboItems(cmbScheduleFilterSection, filterSections.Select(s => new LookupItem(s.SectionID, FormatSectionDisplayName(s))), true, emptyLabel);
            cmbScheduleFilterSection.Enabled = true;
        }

        private void RefreshScheduleSubjectOptions(int preferredSubjectId = 0)
        {
            if (cmbScheduleSubject == null)
            {
                return;
            }

            int studyYearId = SelectedId(cmbScheduleYear);
            int branchId = SelectedId(cmbScheduleBranch);

            IEnumerable<Subject> subjects = _subjects;

            if (studyYearId > 0)
            {
                subjects = subjects.Where(s => s.StudyYearID == studyYearId);
            }

            if (branchId > 0)
            {
                subjects = subjects.Where(s => !s.BranchID.HasValue || s.BranchID == branchId);
            }
            else if (studyYearId > 0)
            {
                subjects = subjects.Where(s => !s.BranchID.HasValue);
            }

            SetComboItems(
                cmbScheduleSubject,
                subjects
                    .OrderBy(s => s.SemesterNumber)
                    .ThenBy(s => s.SubjectName)
                    .Select(s => new LookupItem(s.SubjectID, s.SubjectName)),
                true,
                "Select subject");

            if (preferredSubjectId > 0)
            {
                SetComboValue(cmbScheduleSubject, preferredSubjectId);
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

            BindSubjectsGrid();

            dgvSections.DataSource = _sections
                .Select((s, index) => new
                {
                    No = index + 1,
                    s.SectionID,
                    s.SectionName,
                    s.StudentCount,
                    s.StudyYearID,
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
                    Duration = FormatDuration(t.EndTime - t.StartTime),
                    Type = t.IsBreak ? "Break Period" : "Teaching Period",
                    t.IsBreak
                })
                .ToList();
            FinishGrid(dgvTimeSlots, "TimeSlotID", "IsBreak");
            StyleTimeSlotRows();

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

            BindSchedulesGrid();
        }

        private void StyleTimeSlotRows()
        {
            if (dgvTimeSlots.Columns.Contains("Type"))
            {
                dgvTimeSlots.Columns["Type"].FillWeight = 145;
            }

            if (dgvTimeSlots.Columns.Contains("Duration"))
            {
                dgvTimeSlots.Columns["Duration"].FillWeight = 95;
            }

            foreach (DataGridViewRow row in dgvTimeSlots.Rows)
            {
                if (!GetGridBool(row, "IsBreak"))
                {
                    continue;
                }

                row.DefaultCellStyle.BackColor = Color.FromArgb(239, 253, 244);
                row.DefaultCellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 247, 208);
                row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(20, 83, 45);
            }
        }

        private void BindSchedulesGrid()
        {
            if (dgvSchedules == null)
            {
                return;
            }

            int facultyId = SelectedId(cmbScheduleFilterFaculty);
            int sectionId = SelectedId(cmbScheduleFilterSection);
            int studyYearId = SelectedId(cmbScheduleFilterYear);
            string day = cmbScheduleFilterDay.Text.Trim();

            IEnumerable<Schedule> schedules = _schedules;

            if (facultyId > 0)
            {
                schedules = schedules.Where(s => s.FacultyMemberID == facultyId);
            }

            if (sectionId > 0)
            {
                schedules = schedules.Where(s => s.SectionID == sectionId);
            }

            if (studyYearId > 0)
            {
                schedules = schedules.Where(s => s.StudyYearID == studyYearId);
            }

            if (!string.IsNullOrWhiteSpace(day) && day != "All days")
            {
                schedules = schedules.Where(s => s.DayOfWeek.Equals(day, StringComparison.OrdinalIgnoreCase));
            }

            dgvSchedules.DataSource = schedules
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
                    Section = s.Section == null ? "-" : FormatSectionDisplayName(s.Section)
                })
                .ToList();
            FinishGrid(dgvSchedules, "ScheduleID", "SubjectID", "FacultyMemberID", "ClassroomID", "TimeSlotID", "StudyYearID", "BranchID", "Branch", "SectionID");
        }

        private void BindSubjectsGrid()
        {
            int studyYearId = SelectedId(cmbSubjectFilterYear);
            int branchId = SelectedId(cmbSubjectFilterBranch);
            int semesterNumber = SelectedId(cmbSubjectFilterSemester);

            IEnumerable<Subject> subjects = _subjects;

            if (studyYearId > 0)
            {
                subjects = subjects.Where(s => s.StudyYearID == studyYearId);
            }

            if (branchId > 0)
            {
                subjects = subjects.Where(s => s.BranchID == branchId);
            }

            if (semesterNumber > 0)
            {
                subjects = subjects.Where(s => s.SemesterNumber == semesterNumber);
            }

            dgvSubjects.DataSource = subjects
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
                    s.BranchID,
                    Branch = GetBranchName(s.BranchID)
                })
                .ToList();
            FinishGrid(dgvSubjects, "SubjectID", "StudyYearID", "BranchID");
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

        private void ExportSchedulesToPdf()
        {
            List<string[]> rows = GetScheduleExportRows();

            if (rows.Count == 0)
            {
                ShowInfo("No schedule entries to export.");
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Title = "Export Schedule to PDF",
                Filter = "PDF files (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                FileName = $"Schedule_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                SchedulePdfExporter.Export(dialog.FileName, BuildScheduleExportTitle(), rows);
                ShowInfo("Schedule PDF exported successfully.");
            }
            catch (Exception ex)
            {
                ShowError(ex, "Could not export schedule PDF.");
            }
        }

        private List<string[]> GetScheduleExportRows()
        {
            var rows = new List<string[]>();

            foreach (DataGridViewRow row in dgvSchedules.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                rows.Add(new[]
                {
                    GetGridText(row, "DayOfWeek"),
                    GetGridText(row, "Time"),
                    GetGridText(row, "Subject"),
                    GetGridText(row, "Faculty"),
                    GetGridText(row, "Classroom"),
                    GetGridText(row, "StudyYear"),
                    GetGridText(row, "Section")
                });
            }

            return rows;
        }

        private string BuildScheduleExportTitle()
        {
            var filters = new List<string>();
            AddSelectedFilter(filters, "Faculty", cmbScheduleFilterFaculty);
            AddSelectedFilter(filters, "Section", cmbScheduleFilterSection);
            AddSelectedFilter(filters, "Study Year", cmbScheduleFilterYear);

            string day = cmbScheduleFilterDay.Text.Trim();
            if (!string.IsNullOrWhiteSpace(day) && day != "All days")
            {
                filters.Add($"Day: {day}");
            }

            return filters.Count == 0
                ? "Schedule Report"
                : "Schedule Report - " + string.Join(", ", filters);
        }

        private static void AddSelectedFilter(List<string> filters, string label, Guna2ComboBox comboBox)
        {
            if (SelectedId(comboBox) <= 0)
            {
                return;
            }

            filters.Add($"{label}: {comboBox.Text}");
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
            SetSelectedAcademicTitle(GetGridText(row, "AcademicTitle"));
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
            RefreshSectionBranchOptions(GetGridNullableInt(row, "BranchID") ?? 0);
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
            int subjectId = GetGridInt(row, "SubjectID");
            SetComboValue(cmbScheduleFaculty, GetGridInt(row, "FacultyMemberID"));
            SetComboValue(cmbScheduleClassroom, GetGridInt(row, "ClassroomID"));
            SetComboValue(cmbScheduleTimeSlot, GetGridInt(row, "TimeSlotID"));
            cmbScheduleDay.SelectedItem = GetGridText(row, "DayOfWeek");
            SetComboValue(cmbScheduleYear, GetGridNullableInt(row, "StudyYearID") ?? 0);
            SetComboValue(cmbScheduleBranch, GetGridNullableInt(row, "BranchID") ?? 0);
            SetComboValue(cmbScheduleSection, GetGridNullableInt(row, "SectionID") ?? 0);
            RefreshScheduleSubjectOptions(subjectId);
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
            SetSelectedAcademicTitle("Assistant Professor");
            dgvFacultyMembers.ClearSelection();
        }

        private void ClearSubjectInputs()
        {
            selectedSubjectId = 0;
            txtSubjectName.Clear();
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
            isClearingScheduleInputs = true;

            try
            {
                selectedScheduleId = 0;
                SetComboValue(cmbScheduleSubject, 0);
                SetComboValue(cmbScheduleFaculty, 0);
                SetComboValue(cmbScheduleClassroom, 0);
                SetComboValue(cmbScheduleTimeSlot, 0);
                cmbScheduleDay.SelectedIndex = cmbScheduleDay.Items.Count > 0 ? 0 : -1;
                SetComboValue(cmbScheduleYear, 0);
                SetComboValue(cmbScheduleBranch, 0);
                SetComboValue(cmbScheduleSection, 0);
                SetComboValue(cmbScheduleFilterFaculty, 0);
                SetComboValue(cmbScheduleFilterSection, 0);
                SetComboValue(cmbScheduleFilterYear, 0);
                cmbScheduleFilterDay.SelectedIndex = cmbScheduleFilterDay.Items.Count > 0 ? 0 : -1;
                BindSchedulesGrid();
                ClearGridSelection(dgvSchedules);
            }
            finally
            {
                isClearingScheduleInputs = false;
            }
        }

        private void SyncScheduleSectionContext()
        {
            if (isClearingScheduleInputs)
            {
                return;
            }

            int sectionId = SelectedId(cmbScheduleSection);
            Section? section = _sections.FirstOrDefault(s => s.SectionID == sectionId);

            if (section == null)
            {
                return;
            }

            SetComboValue(cmbScheduleYear, section.StudyYearID);
            SetComboValue(cmbScheduleBranch, section.BranchID ?? 0);
            RefreshScheduleSubjectOptions();
        }

        private string ReadScheduleDay()
        {
            string day = cmbScheduleDay.Text.Trim();

            if (string.IsNullOrWhiteSpace(day) || day == "Select day")
            {
                throw new Exception("Please select a day.");
            }

            return day;
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

        private string FormatSectionDisplayName(Section section)
        {
            string yearName = GetStudyYearName(section.StudyYearID);
            string branchName = GetBranchName(section.BranchID);
            string sectionCode = section.SectionName.Trim();

            sectionCode = RemoveDisplayPart(sectionCode, yearName);
            sectionCode = RemoveDisplayPart(sectionCode, yearName.Replace(" Year", string.Empty, StringComparison.OrdinalIgnoreCase));

            if (section.BranchID.HasValue)
            {
                sectionCode = RemoveDisplayPart(sectionCode, branchName);
                sectionCode = RemoveDisplayPart(sectionCode, CompactBranchName(branchName));
            }

            sectionCode = NormalizeDisplayText(sectionCode.Trim('-', ' ', '/', '\\', '|'));

            if (string.IsNullOrWhiteSpace(sectionCode))
            {
                sectionCode = section.SectionName.Trim();
            }

            if (section.BranchID.HasValue)
            {
                return $"{yearName} - {CompactBranchName(branchName)} - {sectionCode}";
            }

            return $"{yearName} - {sectionCode}";
        }

        private static string CompactBranchName(string branchName)
        {
            return branchName switch
            {
                "Computer Science" => "CS",
                "Cyber Security" => "Cyber",
                "Information Technology" => "IT",
                _ => branchName
            };
        }

        private static string RemoveDisplayPart(string value, string part)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(part))
            {
                return value;
            }

            int index;
            while ((index = value.IndexOf(part, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                value = value.Remove(index, part.Length);
            }

            return value;
        }

        private static string NormalizeDisplayText(string value)
        {
            return string.Join(" ", value.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries));
        }

        private string SelectedRoomType()
        {
            string roomType = cmbRoomType.Text.Trim();
            return string.IsNullOrWhiteSpace(roomType) ? "Lecture" : roomType;
        }

        private void SetSelectedRoomType(string? roomType)
        {
            string value = string.Equals(roomType?.Trim(), "Lab", StringComparison.OrdinalIgnoreCase)
                ? "Lab"
                : "Lecture";
            cmbRoomType.SelectedItem = value;

            if (cmbRoomType.SelectedIndex < 0)
            {
                cmbRoomType.SelectedIndex = 0;
            }
        }

        private string SelectedAcademicTitle()
        {
            string academicTitle = cmbAcademicTitle.Text.Trim();
            return string.IsNullOrWhiteSpace(academicTitle) ? "Assistant Professor" : academicTitle;
        }

        private void SetSelectedAcademicTitle(string? academicTitle)
        {
            string title = NormalizeAcademicTitle(academicTitle);
            cmbAcademicTitle.SelectedItem = title;

            if (cmbAcademicTitle.SelectedIndex < 0)
            {
                cmbAcademicTitle.SelectedItem = "Assistant Professor";
            }
        }

        private static string NormalizeAcademicTitle(string? academicTitle)
        {
            string title = academicTitle?.Trim() ?? string.Empty;

            if (string.Equals(title, "Professor", StringComparison.OrdinalIgnoreCase))
            {
                return "Professor";
            }

            if (string.Equals(title, "Lecturer", StringComparison.OrdinalIgnoreCase))
            {
                return "Lecturer";
            }

            if (string.Equals(title, "Assistant Lecturer", StringComparison.OrdinalIgnoreCase))
            {
                return "Assistant Lecturer";
            }

            return "Assistant Professor";
        }

        private static string FormatTime(TimeSpan time)
        {
            return time.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
        }

        private static string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalMinutes < 60)
            {
                return $"{(int)duration.TotalMinutes} min";
            }

            int hours = (int)duration.TotalHours;
            int minutes = duration.Minutes;

            return minutes == 0 ? $"{hours} h" : $"{hours} h {minutes} min";
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
            SetHeader(grid, "SectionName", "Section");
            SetHeader(grid, "StudentCount", "Students");
            SetHeader(grid, "StartTime", "Start Time");
            SetHeader(grid, "EndTime", "End Time");
            SetHeader(grid, "Duration", "Duration");
            SetHeader(grid, "Type", "Type");
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

                if (SelectedId(comboBox) != id)
                {
                    SelectFirst(comboBox);
                }
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

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }

        private void lblAppTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblAppSubtitle_Click(object sender, EventArgs e)
        {

        }
    }
}
