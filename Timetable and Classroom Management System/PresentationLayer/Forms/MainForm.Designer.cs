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
            branchesFormCard = new Guna.UI2.WinForms.Guna2Panel();
            branchesFieldsPanel = new FlowLayoutPanel();
            branchesButtonsPanel = new FlowLayoutPanel();
            txtBranchName = new Guna.UI2.WinForms.Guna2TextBox();
            btnBranchAdd = new Guna.UI2.WinForms.Guna2Button();
            btnBranchUpdate = new Guna.UI2.WinForms.Guna2Button();
            btnBranchDelete = new Guna.UI2.WinForms.Guna2Button();
            btnBranchClear = new Guna.UI2.WinForms.Guna2Button();
            dgvBranches = new Guna.UI2.WinForms.Guna2DataGridView();
            tabStudyYears = new TabPage();
            studyYearsLayout = new TableLayoutPanel();
            studyYearsFormCard = new Guna.UI2.WinForms.Guna2Panel();
            studyYearsFieldsPanel = new FlowLayoutPanel();
            studyYearsButtonsPanel = new FlowLayoutPanel();
            txtStudyYearName = new Guna.UI2.WinForms.Guna2TextBox();
            btnStudyYearAdd = new Guna.UI2.WinForms.Guna2Button();
            btnStudyYearUpdate = new Guna.UI2.WinForms.Guna2Button();
            btnStudyYearDelete = new Guna.UI2.WinForms.Guna2Button();
            dgvStudyYears = new Guna.UI2.WinForms.Guna2DataGridView();
            tabClassrooms = new TabPage();
            classroomsLayout = new TableLayoutPanel();
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
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            mainTabs.SuspendLayout();
            tabDashboard.SuspendLayout();
            tabBranches.SuspendLayout();
            branchesLayout.SuspendLayout();
            branchesFormCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBranches).BeginInit();
            tabStudyYears.SuspendLayout();
            studyYearsLayout.SuspendLayout();
            studyYearsFormCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudyYears).BeginInit();
            tabClassrooms.SuspendLayout();
            classroomsLayout.SuspendLayout();
            classroomsFormCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numClassroomCapacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvClassrooms).BeginInit();
            SuspendLayout();

            //
            // rootLayout
            //
            rootLayout.BackColor = AppBackground;
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
            headerPanel.BorderColor = BorderColor;
            headerPanel.BorderRadius = 14;
            headerPanel.BorderThickness = 1;
            headerPanel.Controls.Add(lblAppTitle);
            headerPanel.Controls.Add(lblAppSubtitle);
            headerPanel.Controls.Add(btnRefresh);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.FillColor = CardBackground;
            headerPanel.Location = new Point(16, 16);
            headerPanel.Margin = new Padding(0, 0, 0, 12);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(24, 14, 24, 14);
            headerPanel.Size = new Size(1248, 80);
            headerPanel.TabIndex = 0;

            //
            // lblAppTitle
            //
            lblAppTitle.AutoSize = true;
            lblAppTitle.BackColor = Color.Transparent;
            lblAppTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblAppTitle.ForeColor = HeaderColor;
            lblAppTitle.Location = new Point(24, 12);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(271, 50);
            lblAppTitle.TabIndex = 0;
            lblAppTitle.Text = "Timetable Studio";

            //
            // lblAppSubtitle
            //
            lblAppSubtitle.AutoSize = true;
            lblAppSubtitle.BackColor = Color.Transparent;
            lblAppSubtitle.Font = new Font("Segoe UI", 10F);
            lblAppSubtitle.ForeColor = MutedColor;
            lblAppSubtitle.Location = new Point(28, 54);
            lblAppSubtitle.Name = "lblAppSubtitle";
            lblAppSubtitle.Size = new Size(481, 23);
            lblAppSubtitle.TabIndex = 1;
            lblAppSubtitle.Text = "Modern classroom, subject, faculty, and schedule management";

            //
            // btnRefresh
            //
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BorderRadius = 9;
            btnRefresh.FillColor = PrimaryColor;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1108, 22);
            btnRefresh.Margin = new Padding(0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(116, 40);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";

            //
            // mainTabs
            //
            mainTabs.Controls.Add(tabDashboard);
            mainTabs.Controls.Add(tabBranches);
            mainTabs.Controls.Add(tabStudyYears);
            mainTabs.Controls.Add(tabClassrooms);
            mainTabs.Dock = DockStyle.Fill;
            mainTabs.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            mainTabs.ItemSize = new Size(150, 42);
            mainTabs.Location = new Point(16, 108);
            mainTabs.Margin = new Padding(0);
            mainTabs.Name = "mainTabs";
            mainTabs.Padding = new Point(12, 4);
            mainTabs.SelectedIndex = 0;
            mainTabs.Size = new Size(1248, 696);
            mainTabs.SizeMode = TabSizeMode.Fixed;
            mainTabs.TabButtonHoverState.FillColor = SidebarHoverColor;
            mainTabs.TabButtonHoverState.ForeColor = Color.White;
            mainTabs.TabButtonHoverState.InnerColor = PrimaryColor;
            mainTabs.TabButtonIdleState.FillColor = SidebarColor;
            mainTabs.TabButtonIdleState.ForeColor = Color.FromArgb(203, 213, 225);
            mainTabs.TabButtonIdleState.InnerColor = SidebarColor;
            mainTabs.TabButtonSelectedState.FillColor = HeaderColor;
            mainTabs.TabButtonSelectedState.ForeColor = Color.White;
            mainTabs.TabButtonSelectedState.InnerColor = PrimaryColor;
            mainTabs.TabIndex = 1;
            mainTabs.TabMenuBackColor = SidebarColor;

            //
            // tabDashboard
            //
            tabDashboard.BackColor = AppBackground;
            tabDashboard.Controls.Add(dashboardContentPanel);
            tabDashboard.Location = new Point(154, 4);
            tabDashboard.Name = "tabDashboard";
            tabDashboard.Padding = new Padding(0);
            tabDashboard.Size = new Size(1090, 688);
            tabDashboard.TabIndex = 0;
            tabDashboard.Text = "Dashboard";

            //
            // dashboardContentPanel
            //
            dashboardContentPanel.BackColor = AppBackground;
            dashboardContentPanel.Dock = DockStyle.Fill;
            dashboardContentPanel.FlowDirection = FlowDirection.LeftToRight;
            dashboardContentPanel.Location = new Point(0, 0);
            dashboardContentPanel.Name = "dashboardContentPanel";
            dashboardContentPanel.Padding = new Padding(10);
            dashboardContentPanel.Size = new Size(1090, 688);
            dashboardContentPanel.TabIndex = 0;
            dashboardContentPanel.WrapContents = true;
            CreateDashboardDesignerCards();

            //
            // tabBranches
            //
            tabBranches.BackColor = AppBackground;
            tabBranches.Controls.Add(branchesLayout);
            tabBranches.Location = new Point(154, 4);
            tabBranches.Name = "tabBranches";
            tabBranches.Padding = new Padding(0);
            tabBranches.Size = new Size(1090, 688);
            tabBranches.TabIndex = 1;
            tabBranches.Text = "Branches";
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

            //
            // tabStudyYears
            //
            tabStudyYears.BackColor = AppBackground;
            tabStudyYears.Controls.Add(studyYearsLayout);
            tabStudyYears.Location = new Point(154, 4);
            tabStudyYears.Name = "tabStudyYears";
            tabStudyYears.Padding = new Padding(0);
            tabStudyYears.Size = new Size(1090, 688);
            tabStudyYears.TabIndex = 2;
            tabStudyYears.Text = "Study Years";
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

            //
            // tabClassrooms
            //
            tabClassrooms.BackColor = AppBackground;
            tabClassrooms.Controls.Add(classroomsLayout);
            tabClassrooms.Location = new Point(154, 4);
            tabClassrooms.Name = "tabClassrooms";
            tabClassrooms.Padding = new Padding(0);
            tabClassrooms.Size = new Size(1090, 688);
            tabClassrooms.TabIndex = 3;
            tabClassrooms.Text = "Classrooms";
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
            AddDesignerComboField(classroomsFieldsPanel, "Room type", cmbRoomType);
            AddDesignerButton(classroomsButtonsPanel, btnClassroomAdd, "Add", SuccessColor);
            AddDesignerButton(classroomsButtonsPanel, btnClassroomUpdate, "Update", PrimaryColor);
            AddDesignerButton(classroomsButtonsPanel, btnClassroomDelete, "Delete", DangerColor);

            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = AppBackground;
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
            branchesLayout.ResumeLayout(false);
            branchesFormCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBranches).EndInit();
            tabStudyYears.ResumeLayout(false);
            studyYearsLayout.ResumeLayout(false);
            studyYearsFormCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStudyYears).EndInit();
            tabClassrooms.ResumeLayout(false);
            classroomsLayout.ResumeLayout(false);
            classroomsFormCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numClassroomCapacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvClassrooms).EndInit();
            ResumeLayout(false);
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
            decimal value)
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
            input.Width = width;

            fieldsPanel.Controls.Add(CreateDesignerFieldPanel(labelText, input, width));
        }

        private void AddDesignerComboField(FlowLayoutPanel fieldsPanel, string labelText, Guna.UI2.WinForms.Guna2ComboBox input)
        {
            input.BorderColor = BorderColor;
            input.BorderRadius = 8;
            input.DropDownStyle = ComboBoxStyle.DropDownList;
            input.FillColor = CardBackground;
            input.Font = new Font("Segoe UI", 10F);
            input.ForeColor = HeaderColor;
            input.Height = 40;
            input.Items.AddRange(new object[] { "Lecture", "Lab" });
            input.SelectedIndex = 0;
            input.Width = 220;

            fieldsPanel.Controls.Add(CreateDesignerFieldPanel(labelText, input, 220));
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
