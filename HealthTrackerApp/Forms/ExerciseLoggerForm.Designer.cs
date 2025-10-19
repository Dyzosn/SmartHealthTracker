namespace HealthTrackerApp.Forms
{
    partial class ExerciseLoggerForm
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
            this.lblExerciseName = new System.Windows.Forms.Label();
            this.txtExerciseName = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboExerciseCategory = new System.Windows.Forms.ComboBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            this.lblIntensity = new System.Windows.Forms.Label();
            this.cboIntensity = new System.Windows.Forms.ComboBox();
            this.lblExerciseDate = new System.Windows.Forms.Label();
            this.dtpExerciseDate = new System.Windows.Forms.DateTimePicker();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTodayBurned = new System.Windows.Forms.Label();
            this.lblExerciseHistory = new System.Windows.Forms.Label();
            this.dgvExercises = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExercises)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Exercise Logger";

            // lblExerciseName
            this.lblExerciseName.AutoSize = true;
            this.lblExerciseName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblExerciseName.Location = new System.Drawing.Point(20, 70);
            this.lblExerciseName.Name = "lblExerciseName";
            this.lblExerciseName.Size = new System.Drawing.Size(110, 19);
            this.lblExerciseName.TabIndex = 1;
            this.lblExerciseName.Text = "Exercise Name:";

            // txtExerciseName
            this.txtExerciseName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtExerciseName.Location = new System.Drawing.Point(150, 67);
            this.txtExerciseName.Name = "txtExerciseName";
            this.txtExerciseName.Size = new System.Drawing.Size(250, 25);
            this.txtExerciseName.TabIndex = 2;

            // lblCategory
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCategory.Location = new System.Drawing.Point(20, 110);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(70, 19);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "Category:";

            // cboExerciseCategory
            this.cboExerciseCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExerciseCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboExerciseCategory.FormattingEnabled = true;
            this.cboExerciseCategory.Location = new System.Drawing.Point(150, 107);
            this.cboExerciseCategory.Name = "cboExerciseCategory";
            this.cboExerciseCategory.Size = new System.Drawing.Size(250, 25);
            this.cboExerciseCategory.TabIndex = 4;

            // lblDuration
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDuration.Location = new System.Drawing.Point(20, 150);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(120, 19);
            this.lblDuration.TabIndex = 5;
            this.lblDuration.Text = "Duration (minutes):";

            // nudDuration
            this.nudDuration.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudDuration.Location = new System.Drawing.Point(150, 147);
            this.nudDuration.Maximum = new decimal(new int[] { 480, 0, 0, 0 });
            this.nudDuration.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.Size = new System.Drawing.Size(120, 25);
            this.nudDuration.TabIndex = 6;
            this.nudDuration.Value = new decimal(new int[] { 30, 0, 0, 0 });

            // lblIntensity
            this.lblIntensity.AutoSize = true;
            this.lblIntensity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIntensity.Location = new System.Drawing.Point(20, 190);
            this.lblIntensity.Name = "lblIntensity";
            this.lblIntensity.Size = new System.Drawing.Size(65, 19);
            this.lblIntensity.TabIndex = 7;
            this.lblIntensity.Text = "Intensity:";

            // cboIntensity
            this.cboIntensity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIntensity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboIntensity.FormattingEnabled = true;
            this.cboIntensity.Location = new System.Drawing.Point(150, 187);
            this.cboIntensity.Name = "cboIntensity";
            this.cboIntensity.Size = new System.Drawing.Size(250, 25);
            this.cboIntensity.TabIndex = 8;

            // lblExerciseDate
            this.lblExerciseDate.AutoSize = true;
            this.lblExerciseDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblExerciseDate.Location = new System.Drawing.Point(20, 230);
            this.lblExerciseDate.Name = "lblExerciseDate";
            this.lblExerciseDate.Size = new System.Drawing.Size(41, 19);
            this.lblExerciseDate.TabIndex = 9;
            this.lblExerciseDate.Text = "Date:";

            // dtpExerciseDate
            this.dtpExerciseDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpExerciseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExerciseDate.Location = new System.Drawing.Point(150, 227);
            this.dtpExerciseDate.Name = "dtpExerciseDate";
            this.dtpExerciseDate.Size = new System.Drawing.Size(250, 25);
            this.dtpExerciseDate.TabIndex = 10;

            // lblNotes
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNotes.Location = new System.Drawing.Point(20, 270);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(48, 19);
            this.lblNotes.TabIndex = 11;
            this.lblNotes.Text = "Notes:";

            // txtNotes
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.Location = new System.Drawing.Point(150, 267);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(250, 60);
            this.txtNotes.TabIndex = 12;

            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(150, 345);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save Exercise";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnDelete
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(280, 345);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 35);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(750, 345);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(860, 345);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // lblTodayBurned
            this.lblTodayBurned.AutoSize = true;
            this.lblTodayBurned.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTodayBurned.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblTodayBurned.Location = new System.Drawing.Point(450, 70);
            this.lblTodayBurned.Name = "lblTodayBurned";
            this.lblTodayBurned.Size = new System.Drawing.Size(280, 21);
            this.lblTodayBurned.TabIndex = 17;
            this.lblTodayBurned.Text = "Today's Total: 0 kcal burned (0 workouts)";

            // lblExerciseHistory
            this.lblExerciseHistory.AutoSize = true;
            this.lblExerciseHistory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblExerciseHistory.Location = new System.Drawing.Point(450, 110);
            this.lblExerciseHistory.Name = "lblExerciseHistory";
            this.lblExerciseHistory.Size = new System.Drawing.Size(130, 20);
            this.lblExerciseHistory.TabIndex = 18;
            this.lblExerciseHistory.Text = "Exercise History";

            // dgvExercises
            this.dgvExercises.AllowUserToAddRows = false;
            this.dgvExercises.AllowUserToDeleteRows = false;
            this.dgvExercises.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExercises.Location = new System.Drawing.Point(450, 140);
            this.dgvExercises.Name = "dgvExercises";
            this.dgvExercises.ReadOnly = true;
            this.dgvExercises.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExercises.Size = new System.Drawing.Size(510, 190);
            this.dgvExercises.TabIndex = 19;
            this.dgvExercises.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExercises_CellDoubleClick);

            // ExerciseLoggerForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 411);
            this.Controls.Add(this.dgvExercises);
            this.Controls.Add(this.lblExerciseHistory);
            this.Controls.Add(this.lblTodayBurned);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.dtpExerciseDate);
            this.Controls.Add(this.lblExerciseDate);
            this.Controls.Add(this.cboIntensity);
            this.Controls.Add(this.lblIntensity);
            this.Controls.Add(this.nudDuration);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.cboExerciseCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.txtExerciseName);
            this.Controls.Add(this.lblExerciseName);
            this.Controls.Add(this.lblTitle);
            this.Name = "ExerciseLoggerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exercise Logger - Health Tracker";
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExercises)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblExerciseName;
        private System.Windows.Forms.TextBox txtExerciseName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboExerciseCategory;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.NumericUpDown nudDuration;
        private System.Windows.Forms.Label lblIntensity;
        private System.Windows.Forms.ComboBox cboIntensity;
        private System.Windows.Forms.Label lblExerciseDate;
        private System.Windows.Forms.DateTimePicker dtpExerciseDate;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTodayBurned;
        private System.Windows.Forms.Label lblExerciseHistory;
        private System.Windows.Forms.DataGridView dgvExercises;
    }
}