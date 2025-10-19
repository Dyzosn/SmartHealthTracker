namespace HealthTrackerApp.Forms
{
    partial class GoalsProgressForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblGoalType = new System.Windows.Forms.Label();
            this.cboGoalType = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblTargetValue = new System.Windows.Forms.Label();
            this.nudTargetValue = new System.Windows.Forms.NumericUpDown();
            this.lblCurrentValue = new System.Windows.Forms.Label();
            this.nudCurrentValue = new System.Windows.Forms.NumericUpDown();
            this.lblTargetDate = new System.Windows.Forms.Label();
            this.dtpTargetDate = new System.Windows.Forms.DateTimePicker();
            this.btnCreateGoal = new System.Windows.Forms.Button();
            this.btnUpdateProgress = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblActiveGoals = new System.Windows.Forms.Label();
            this.dgvActiveGoals = new System.Windows.Forms.DataGridView();
            this.lblCompletedGoals = new System.Windows.Forms.Label();
            this.dgvCompletedGoals = new System.Windows.Forms.DataGridView();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBarGoal = new System.Windows.Forms.ProgressBar();
            this.lblMotivation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTargetValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCurrentValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveGoals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompletedGoals)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Goals & Progress";

            // lblGoalType
            this.lblGoalType.AutoSize = true;
            this.lblGoalType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGoalType.Location = new System.Drawing.Point(20, 70);
            this.lblGoalType.Name = "lblGoalType";
            this.lblGoalType.Size = new System.Drawing.Size(75, 19);
            this.lblGoalType.TabIndex = 1;
            this.lblGoalType.Text = "Goal Type:";

            // cboGoalType
            this.cboGoalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGoalType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGoalType.FormattingEnabled = true;
            this.cboGoalType.Location = new System.Drawing.Point(140, 67);
            this.cboGoalType.Name = "cboGoalType";
            this.cboGoalType.Size = new System.Drawing.Size(250, 25);
            this.cboGoalType.TabIndex = 2;

            // lblDescription
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.Location = new System.Drawing.Point(20, 110);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(83, 19);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description:";

            // txtDescription
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(140, 107);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(250, 50);
            this.txtDescription.TabIndex = 4;

            // lblTargetValue
            this.lblTargetValue.AutoSize = true;
            this.lblTargetValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTargetValue.Location = new System.Drawing.Point(20, 175);
            this.lblTargetValue.Name = "lblTargetValue";
            this.lblTargetValue.Size = new System.Drawing.Size(88, 19);
            this.lblTargetValue.TabIndex = 5;
            this.lblTargetValue.Text = "Target Value:";

            // nudTargetValue
            this.nudTargetValue.DecimalPlaces = 1;
            this.nudTargetValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudTargetValue.Location = new System.Drawing.Point(140, 172);
            this.nudTargetValue.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.nudTargetValue.Name = "nudTargetValue";
            this.nudTargetValue.Size = new System.Drawing.Size(120, 25);
            this.nudTargetValue.TabIndex = 6;
            this.nudTargetValue.Value = new decimal(new int[] { 70, 0, 0, 0 });

            // lblCurrentValue
            this.lblCurrentValue.AutoSize = true;
            this.lblCurrentValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrentValue.Location = new System.Drawing.Point(20, 215);
            this.lblCurrentValue.Name = "lblCurrentValue";
            this.lblCurrentValue.Size = new System.Drawing.Size(95, 19);
            this.lblCurrentValue.TabIndex = 7;
            this.lblCurrentValue.Text = "Current Value:";

            // nudCurrentValue
            this.nudCurrentValue.DecimalPlaces = 1;
            this.nudCurrentValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCurrentValue.Location = new System.Drawing.Point(140, 212);
            this.nudCurrentValue.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.nudCurrentValue.Name = "nudCurrentValue";
            this.nudCurrentValue.Size = new System.Drawing.Size(120, 25);
            this.nudCurrentValue.TabIndex = 8;
            this.nudCurrentValue.Value = new decimal(new int[] { 75, 0, 0, 0 });

            // lblTargetDate
            this.lblTargetDate.AutoSize = true;
            this.lblTargetDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTargetDate.Location = new System.Drawing.Point(20, 255);
            this.lblTargetDate.Name = "lblTargetDate";
            this.lblTargetDate.Size = new System.Drawing.Size(83, 19);
            this.lblTargetDate.TabIndex = 9;
            this.lblTargetDate.Text = "Target Date:";

            // dtpTargetDate
            this.dtpTargetDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTargetDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTargetDate.Location = new System.Drawing.Point(140, 252);
            this.dtpTargetDate.Name = "dtpTargetDate";
            this.dtpTargetDate.Size = new System.Drawing.Size(250, 25);
            this.dtpTargetDate.TabIndex = 10;

            // btnCreateGoal
            this.btnCreateGoal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnCreateGoal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateGoal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCreateGoal.ForeColor = System.Drawing.Color.White;
            this.btnCreateGoal.Location = new System.Drawing.Point(140, 295);
            this.btnCreateGoal.Name = "btnCreateGoal";
            this.btnCreateGoal.Size = new System.Drawing.Size(120, 35);
            this.btnCreateGoal.TabIndex = 11;
            this.btnCreateGoal.Text = "Create Goal";
            this.btnCreateGoal.UseVisualStyleBackColor = false;
            this.btnCreateGoal.Click += new System.EventHandler(this.btnCreateGoal_Click);

            // btnUpdateProgress
            this.btnUpdateProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnUpdateProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateProgress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdateProgress.ForeColor = System.Drawing.Color.White;
            this.btnUpdateProgress.Location = new System.Drawing.Point(270, 295);
            this.btnUpdateProgress.Name = "btnUpdateProgress";
            this.btnUpdateProgress.Size = new System.Drawing.Size(120, 35);
            this.btnUpdateProgress.TabIndex = 12;
            this.btnUpdateProgress.Text = "Update";
            this.btnUpdateProgress.UseVisualStyleBackColor = false;
            this.btnUpdateProgress.Click += new System.EventHandler(this.btnUpdateProgress_Click);

            // btnDelete
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(140, 340);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 35);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(270, 340);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 35);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // lblActiveGoals
            this.lblActiveGoals.AutoSize = true;
            this.lblActiveGoals.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblActiveGoals.Location = new System.Drawing.Point(450, 70);
            this.lblActiveGoals.Name = "lblActiveGoals";
            this.lblActiveGoals.Size = new System.Drawing.Size(100, 20);
            this.lblActiveGoals.TabIndex = 15;
            this.lblActiveGoals.Text = "Active Goals";

            // dgvActiveGoals
            this.dgvActiveGoals.AllowUserToAddRows = false;
            this.dgvActiveGoals.AllowUserToDeleteRows = false;
            this.dgvActiveGoals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActiveGoals.Location = new System.Drawing.Point(450, 100);
            this.dgvActiveGoals.Name = "dgvActiveGoals";
            this.dgvActiveGoals.ReadOnly = true;
            this.dgvActiveGoals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActiveGoals.Size = new System.Drawing.Size(550, 180);
            this.dgvActiveGoals.TabIndex = 16;
            this.dgvActiveGoals.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvActiveGoals_CellClick);

            // lblProgress
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProgress.Location = new System.Drawing.Point(450, 295);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(110, 19);
            this.lblProgress.TabIndex = 17;
            this.lblProgress.Text = "Goal Progress:";

            // progressBarGoal
            this.progressBarGoal.Location = new System.Drawing.Point(450, 320);
            this.progressBarGoal.Name = "progressBarGoal";
            this.progressBarGoal.Size = new System.Drawing.Size(550, 30);
            this.progressBarGoal.TabIndex = 18;

            // lblMotivation
            this.lblMotivation.AutoSize = true;
            this.lblMotivation.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblMotivation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblMotivation.Location = new System.Drawing.Point(450, 360);
            this.lblMotivation.Name = "lblMotivation";
            this.lblMotivation.Size = new System.Drawing.Size(280, 19);
            this.lblMotivation.TabIndex = 19;
            this.lblMotivation.Text = "Set your goals and track your progress!";

            // lblCompletedGoals
            this.lblCompletedGoals.AutoSize = true;
            this.lblCompletedGoals.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCompletedGoals.Location = new System.Drawing.Point(450, 395);
            this.lblCompletedGoals.Name = "lblCompletedGoals";
            this.lblCompletedGoals.Size = new System.Drawing.Size(135, 20);
            this.lblCompletedGoals.TabIndex = 20;
            this.lblCompletedGoals.Text = "Completed Goals";

            // dgvCompletedGoals
            this.dgvCompletedGoals.AllowUserToAddRows = false;
            this.dgvCompletedGoals.AllowUserToDeleteRows = false;
            this.dgvCompletedGoals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompletedGoals.Location = new System.Drawing.Point(450, 425);
            this.dgvCompletedGoals.Name = "dgvCompletedGoals";
            this.dgvCompletedGoals.ReadOnly = true;
            this.dgvCompletedGoals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompletedGoals.Size = new System.Drawing.Size(550, 150);
            this.dgvCompletedGoals.TabIndex = 21;
            this.dgvCompletedGoals.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompletedGoals_CellClick);

            // GoalsProgressForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.dgvCompletedGoals);
            this.Controls.Add(this.lblCompletedGoals);
            this.Controls.Add(this.lblMotivation);
            this.Controls.Add(this.progressBarGoal);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.dgvActiveGoals);
            this.Controls.Add(this.lblActiveGoals);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdateProgress);
            this.Controls.Add(this.btnCreateGoal);
            this.Controls.Add(this.dtpTargetDate);
            this.Controls.Add(this.lblTargetDate);
            this.Controls.Add(this.nudCurrentValue);
            this.Controls.Add(this.lblCurrentValue);
            this.Controls.Add(this.nudTargetValue);
            this.Controls.Add(this.lblTargetValue);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.cboGoalType);
            this.Controls.Add(this.lblGoalType);
            this.Controls.Add(this.lblTitle);
            this.Name = "GoalsProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Goals & Progress - Health Tracker";
            ((System.ComponentModel.ISupportInitialize)(this.nudTargetValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCurrentValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveGoals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompletedGoals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblGoalType;
        private System.Windows.Forms.ComboBox cboGoalType;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblTargetValue;
        private System.Windows.Forms.NumericUpDown nudTargetValue;
        private System.Windows.Forms.Label lblCurrentValue;
        private System.Windows.Forms.NumericUpDown nudCurrentValue;
        private System.Windows.Forms.Label lblTargetDate;
        private System.Windows.Forms.DateTimePicker dtpTargetDate;
        private System.Windows.Forms.Button btnCreateGoal;
        private System.Windows.Forms.Button btnUpdateProgress;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblActiveGoals;
        private System.Windows.Forms.DataGridView dgvActiveGoals;
        private System.Windows.Forms.Label lblCompletedGoals;
        private System.Windows.Forms.DataGridView dgvCompletedGoals;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar progressBarGoal;
        private System.Windows.Forms.Label lblMotivation;
    }
}