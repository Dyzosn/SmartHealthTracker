namespace HealthTrackerApp.Forms
{
    partial class HealthMetricsForm
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
            cboMetricType = new ComboBox();
            dtpRecordedDate = new DateTimePicker();
            lblWeight = new Label();
            txtWeight = new TextBox();
            lblSystolic = new Label();
            txtSystolic = new TextBox();
            lblDiastolic = new Label();
            txtDiastolic = new TextBox();
            lblHeartRate = new Label();
            txtHeartRate = new TextBox();
            lblBloodSugar = new Label();
            txtBloodSugar = new TextBox();
            txtNotes = new TextBox();
            btnSave = new Button();
            dgvHealthMetrics = new DataGridView();
            dgvTrendData = new DataGridView();
            lblTotalRecords = new Label();
            lblLatestWeight = new Label();
            lblLatestBP = new Label();
            lblStatistics = new Label();
            lblMetricTypeLabel = new Label();
            lblDateLabel = new Label();
            groupBoxInput = new GroupBox();
            groupBoxHistory = new GroupBox();
            groupBoxTrends = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvHealthMetrics).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTrendData).BeginInit();
            groupBoxInput.SuspendLayout();
            groupBoxHistory.SuspendLayout();
            groupBoxTrends.SuspendLayout();
            SuspendLayout();
            
            // cboMetricType
            cboMetricType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMetricType.FormattingEnabled = true;
            cboMetricType.Location = new Point(143, 37);
            cboMetricType.Margin = new Padding(4, 5, 4, 5);
            cboMetricType.Name = "cboMetricType";
            cboMetricType.Size = new Size(255, 33);
            cboMetricType.TabIndex = 1;
            
            // dtpRecordedDate
            dtpRecordedDate.Location = new Point(143, 95);
            dtpRecordedDate.Margin = new Padding(4, 5, 4, 5);
            dtpRecordedDate.Name = "dtpRecordedDate";
            dtpRecordedDate.Size = new Size(327, 31);
            dtpRecordedDate.TabIndex = 3;
            
            // lblWeight
            lblWeight.AutoSize = true;
            lblWeight.Location = new Point(21, 167);
            lblWeight.Margin = new Padding(4, 0, 4, 0);
            lblWeight.Name = "lblWeight";
            lblWeight.Size = new Size(107, 25);
            lblWeight.TabIndex = 4;
            lblWeight.Text = "Weight (kg):";
            
            // txtWeight
            txtWeight.Location = new Point(214, 162);
            txtWeight.Margin = new Padding(4, 5, 4, 5);
            txtWeight.Name = "txtWeight";
            txtWeight.Size = new Size(141, 31);
            txtWeight.TabIndex = 5;
            
            // lblSystolic
            lblSystolic.AutoSize = true;
            lblSystolic.Location = new Point(21, 167);
            lblSystolic.Margin = new Padding(4, 0, 4, 0);
            lblSystolic.Name = "lblSystolic";
            lblSystolic.Size = new Size(146, 25);
            lblSystolic.TabIndex = 6;
            lblSystolic.Text = "Systolic (mmHg):";
            
            // txtSystolic
            txtSystolic.Location = new Point(214, 162);
            txtSystolic.Margin = new Padding(4, 5, 4, 5);
            txtSystolic.Name = "txtSystolic";
            txtSystolic.Size = new Size(141, 31);
            txtSystolic.TabIndex = 7;
            
            // lblDiastolic
            lblDiastolic.AutoSize = true;
            lblDiastolic.Location = new Point(21, 225);
            lblDiastolic.Margin = new Padding(4, 0, 4, 0);
            lblDiastolic.Name = "lblDiastolic";
            lblDiastolic.Size = new Size(154, 25);
            lblDiastolic.TabIndex = 8;
            lblDiastolic.Text = "Diastolic (mmHg):";
            
            // txtDiastolic
            txtDiastolic.Location = new Point(214, 220);
            txtDiastolic.Margin = new Padding(4, 5, 4, 5);
            txtDiastolic.Name = "txtDiastolic";
            txtDiastolic.Size = new Size(141, 31);
            txtDiastolic.TabIndex = 9;
            
            // lblHeartRate
            lblHeartRate.AutoSize = true;
            lblHeartRate.Location = new Point(21, 167);
            lblHeartRate.Margin = new Padding(4, 0, 4, 0);
            lblHeartRate.Name = "lblHeartRate";
            lblHeartRate.Size = new Size(152, 25);
            lblHeartRate.TabIndex = 10;
            lblHeartRate.Text = "Heart Rate (bpm):";
            
            // txtHeartRate
            txtHeartRate.Location = new Point(214, 162);
            txtHeartRate.Margin = new Padding(4, 5, 4, 5);
            txtHeartRate.Name = "txtHeartRate";
            txtHeartRate.Size = new Size(141, 31);
            txtHeartRate.TabIndex = 11;
            
            // lblBloodSugar
            lblBloodSugar.AutoSize = true;
            lblBloodSugar.Location = new Point(21, 167);
            lblBloodSugar.Margin = new Padding(4, 0, 4, 0);
            lblBloodSugar.Name = "lblBloodSugar";
            lblBloodSugar.Size = new Size(191, 25);
            lblBloodSugar.TabIndex = 12;
            lblBloodSugar.Text = "Blood Sugar (mmol/L):";
            
            // txtBloodSugar
            txtBloodSugar.Location = new Point(214, 162);
            txtBloodSugar.Margin = new Padding(4, 5, 4, 5);
            txtBloodSugar.Name = "txtBloodSugar";
            txtBloodSugar.Size = new Size(141, 31);
            txtBloodSugar.TabIndex = 13;
            
            // txtNotes
            txtNotes.Location = new Point(21, 283);
            txtNotes.Margin = new Padding(4, 5, 4, 5);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.PlaceholderText = "Notes (optional)";
            txtNotes.Size = new Size(448, 97);
            txtNotes.TabIndex = 14;
            
            // btnSave
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.Location = new Point(21, 400);
            btnSave.Margin = new Padding(4, 5, 4, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(450, 67);
            btnSave.TabIndex = 15;
            btnSave.Text = "Save Health Metric";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            
            // dgvHealthMetrics
            dgvHealthMetrics.AllowUserToAddRows = false;
            dgvHealthMetrics.AllowUserToDeleteRows = false;
            dgvHealthMetrics.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHealthMetrics.Location = new Point(21, 42);
            dgvHealthMetrics.Margin = new Padding(4, 5, 4, 5);
            dgvHealthMetrics.Name = "dgvHealthMetrics";
            dgvHealthMetrics.ReadOnly = true;
            dgvHealthMetrics.RowHeadersWidth = 62;
            dgvHealthMetrics.Size = new Size(786, 333);
            dgvHealthMetrics.TabIndex = 17;
            
            // dgvTrendData
            dgvTrendData.AllowUserToAddRows = false;
            dgvTrendData.AllowUserToDeleteRows = false;
            dgvTrendData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTrendData.Location = new Point(21, 42);
            dgvTrendData.Margin = new Padding(4, 5, 4, 5);
            dgvTrendData.Name = "dgvTrendData";
            dgvTrendData.ReadOnly = true;
            dgvTrendData.RowHeadersWidth = 62;
            dgvTrendData.Size = new Size(743, 417);
            dgvTrendData.TabIndex = 22;
            
            // lblTotalRecords
            lblTotalRecords.AutoSize = true;
            lblTotalRecords.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalRecords.Location = new Point(21, 392);
            lblTotalRecords.Margin = new Padding(4, 0, 4, 0);
            lblTotalRecords.Name = "lblTotalRecords";
            lblTotalRecords.Size = new Size(147, 25);
            lblTotalRecords.TabIndex = 18;
            lblTotalRecords.Text = "Total Records: 0";
            
            // lblLatestWeight
            lblLatestWeight.AutoSize = true;
            lblLatestWeight.Location = new Point(21, 425);
            lblLatestWeight.Margin = new Padding(4, 0, 4, 0);
            lblLatestWeight.Name = "lblLatestWeight";
            lblLatestWeight.Size = new Size(160, 25);
            lblLatestWeight.TabIndex = 19;
            lblLatestWeight.Text = "Latest Weight: N/A";
            
            // lblLatestBP
            lblLatestBP.AutoSize = true;
            lblLatestBP.Location = new Point(21, 458);
            lblLatestBP.Margin = new Padding(4, 0, 4, 0);
            lblLatestBP.Name = "lblLatestBP";
            lblLatestBP.Size = new Size(124, 25);
            lblLatestBP.TabIndex = 20;
            lblLatestBP.Text = "Latest BP: N/A";
            
            // lblStatistics
            lblStatistics.Font = new Font("Consolas", 9F);
            lblStatistics.Location = new Point(21, 475);
            lblStatistics.Margin = new Padding(4, 0, 4, 0);
            lblStatistics.Name = "lblStatistics";
            lblStatistics.Size = new Size(743, 550);
            lblStatistics.TabIndex = 23;
            lblStatistics.Text = "Select a metric type to view statistics";
            
            // lblMetricTypeLabel
            lblMetricTypeLabel.AutoSize = true;
            lblMetricTypeLabel.Location = new Point(21, 42);
            lblMetricTypeLabel.Margin = new Padding(4, 0, 4, 0);
            lblMetricTypeLabel.Name = "lblMetricTypeLabel";
            lblMetricTypeLabel.Size = new Size(107, 25);
            lblMetricTypeLabel.TabIndex = 0;
            lblMetricTypeLabel.Text = "Metric Type:";
            
            // lblDateLabel
            lblDateLabel.AutoSize = true;
            lblDateLabel.Location = new Point(21, 100);
            lblDateLabel.Margin = new Padding(4, 0, 4, 0);
            lblDateLabel.Name = "lblDateLabel";
            lblDateLabel.Size = new Size(53, 25);
            lblDateLabel.TabIndex = 2;
            lblDateLabel.Text = "Date:";
            
            // groupBoxInput
            groupBoxInput.Controls.Add(lblMetricTypeLabel);
            groupBoxInput.Controls.Add(cboMetricType);
            groupBoxInput.Controls.Add(lblDateLabel);
            groupBoxInput.Controls.Add(dtpRecordedDate);
            groupBoxInput.Controls.Add(lblWeight);
            groupBoxInput.Controls.Add(txtWeight);
            groupBoxInput.Controls.Add(lblSystolic);
            groupBoxInput.Controls.Add(txtSystolic);
            groupBoxInput.Controls.Add(lblDiastolic);
            groupBoxInput.Controls.Add(txtDiastolic);
            groupBoxInput.Controls.Add(lblHeartRate);
            groupBoxInput.Controls.Add(txtHeartRate);
            groupBoxInput.Controls.Add(lblBloodSugar);
            groupBoxInput.Controls.Add(txtBloodSugar);
            groupBoxInput.Controls.Add(txtNotes);
            groupBoxInput.Controls.Add(btnSave);
            groupBoxInput.Location = new Point(29, 33);
            groupBoxInput.Margin = new Padding(4, 5, 4, 5);
            groupBoxInput.Name = "groupBoxInput";
            groupBoxInput.Padding = new Padding(4, 5, 4, 5);
            groupBoxInput.Size = new Size(500, 500);
            groupBoxInput.TabIndex = 16;
            groupBoxInput.TabStop = false;
            groupBoxInput.Text = "Log New Metric";
            
            // groupBoxHistory
            groupBoxHistory.Controls.Add(dgvHealthMetrics);
            groupBoxHistory.Controls.Add(lblTotalRecords);
            groupBoxHistory.Controls.Add(lblLatestWeight);
            groupBoxHistory.Controls.Add(lblLatestBP);
            groupBoxHistory.Location = new Point(29, 550);
            groupBoxHistory.Margin = new Padding(4, 5, 4, 5);
            groupBoxHistory.Name = "groupBoxHistory";
            groupBoxHistory.Padding = new Padding(4, 5, 4, 5);
            groupBoxHistory.Size = new Size(829, 508);
            groupBoxHistory.TabIndex = 21;
            groupBoxHistory.TabStop = false;
            groupBoxHistory.Text = "All Health Metrics";
            
            // groupBoxTrends
            groupBoxTrends.Controls.Add(dgvTrendData);
            groupBoxTrends.Controls.Add(lblStatistics);
            groupBoxTrends.Location = new Point(886, 33);
            groupBoxTrends.Margin = new Padding(4, 5, 4, 5);
            groupBoxTrends.Name = "groupBoxTrends";
            groupBoxTrends.Padding = new Padding(4, 5, 4, 5);
            groupBoxTrends.Size = new Size(786, 1025);
            groupBoxTrends.TabIndex = 24;
            groupBoxTrends.TabStop = false;
            groupBoxTrends.Text = "Trends & Statistics (Last 30 Days)";
            
            // HealthMetricsForm
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1700, 1083);
            Controls.Add(groupBoxTrends);
            Controls.Add(groupBoxHistory);
            Controls.Add(groupBoxInput);
            Margin = new Padding(4, 5, 4, 5);
            Name = "HealthMetricsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Health Metrics Tracker - Statistical View";
            ((System.ComponentModel.ISupportInitialize)dgvHealthMetrics).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTrendData).EndInit();
            groupBoxInput.ResumeLayout(false);
            groupBoxInput.PerformLayout();
            groupBoxHistory.ResumeLayout(false);
            groupBoxHistory.PerformLayout();
            groupBoxTrends.ResumeLayout(false);
            ResumeLayout(false);
        }

        // Control declarations
        private System.Windows.Forms.ComboBox cboMetricType;
        private System.Windows.Forms.DateTimePicker dtpRecordedDate;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label lblSystolic;
        private System.Windows.Forms.TextBox txtSystolic;
        private System.Windows.Forms.Label lblDiastolic;
        private System.Windows.Forms.TextBox txtDiastolic;
        private System.Windows.Forms.Label lblHeartRate;
        private System.Windows.Forms.TextBox txtHeartRate;
        private System.Windows.Forms.Label lblBloodSugar;
        private System.Windows.Forms.TextBox txtBloodSugar;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvHealthMetrics;
        private System.Windows.Forms.DataGridView dgvTrendData;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Label lblLatestWeight;
        private System.Windows.Forms.Label lblLatestBP;
        private System.Windows.Forms.Label lblStatistics;
        private System.Windows.Forms.Label lblMetricTypeLabel;
        private System.Windows.Forms.Label lblDateLabel;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.GroupBox groupBoxHistory;
        private System.Windows.Forms.GroupBox groupBoxTrends;
    }
}