namespace Timetable_and_Classroom_Management_System.PresentationLayer.Forms
{

    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvBranches = new DataGridView();
            BranchName = new Label();
            txtBranchName = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            Clear = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBranches).BeginInit();
            SuspendLayout();
            // 
            // dgvBranches
            // 
            dgvBranches.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBranches.Location = new Point(62, 123);
            dgvBranches.Name = "dgvBranches";
            dgvBranches.Size = new Size(637, 315);
            dgvBranches.TabIndex = 0;
            dgvBranches.CellClick += dgvBranches_CellClick;
            dgvBranches.CellContentClick += dgvBranches_CellContentClick;
            // 
            // BranchName
            // 
            BranchName.AutoSize = true;
            BranchName.Location = new Point(107, 19);
            BranchName.Name = "BranchName";
            BranchName.Size = new Size(98, 20);
            BranchName.TabIndex = 1;
            BranchName.Text = "Branch Name";
            // 
            // txtBranchName
            // 
            txtBranchName.Location = new Point(244, 16);
            txtBranchName.Name = "txtBranchName";
            txtBranchName.Size = new Size(201, 27);
            txtBranchName.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(126, 76);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(310, 76);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(451, 76);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // Clear
            // 
            Clear.Location = new Point(624, 76);
            Clear.Name = "Clear";
            Clear.Size = new Size(75, 23);
            Clear.TabIndex = 6;
            Clear.Text = "Clear";
            Clear.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Clear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(txtBranchName);
            Controls.Add(BranchName);
            Controls.Add(dgvBranches);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBranches).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvBranches;
        private Label BranchName;
        private TextBox txtBranchName;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button Clear;
    }
}
