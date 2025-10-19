namespace HealthTrackerApp.Forms
{
    partial class ReportsAnalyticsForm
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
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.lblSummaryStats = new System.Windows.Forms.Label();
            this.lblDailyBreakdown = new System.Windows.Forms.Label();
            this.dgvDailyBreakdown = new System.Windows.Forms.DataGridView();
            this.lblMealTypeAnalysis = new System.Windows.Forms.Label();
            this.dgvMealTypeAnalysis = new System.Windows.Forms.DataGridView();
            this.lblTopFoods = new System.Windows.Forms.Label();
            this.dgvTopFoods = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDailyBreakdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMealTypeAnalysis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopFoods)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(230, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reports & Analytics";

            // lblStartDate
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(20, 70);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(65, 15);
            this.lblStartDate.TabIndex = 1;
            this.lblStartDate.Text = "Start Date:";

            // dtpStartDate
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(90, 67);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(120, 23);
            this.dtpStartDate.TabIndex = 2;

            // lblEndDate
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(230, 70);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(58, 15);
            this.lblEndDate.TabIndex = 3;
            this.lblEndDate.Text = "End Date:";

            // dtpEndDate
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(295, 67);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(120, 23);
            this.dtpEndDate.TabIndex = 4;

            // btnGenerateReport
            this.btnGenerateReport.Location = new System.Drawing.Point(430, 65);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(120, 27);
            this.btnGenerateReport.TabIndex = 5;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;

            // lblSummaryStats
            this.lblSummaryStats.AutoSize = true;
            this.lblSummaryStats.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSummaryStats.Location = new System.Drawing.Point(20, 110);
            this.lblSummaryStats.MaximumSize = new System.Drawing.Size(530, 0);
            this.lblSummaryStats.Name = "lblSummaryStats";
            this.lblSummaryStats.TabIndex = 6;
            this.lblSummaryStats.Text = "Summary Statistics";

            // lblDailyBreakdown
            this.lblDailyBreakdown.AutoSize = true;
            this.lblDailyBreakdown.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDailyBreakdown.Location = new System.Drawing.Point(20, 250);
            this.lblDailyBreakdown.Name = "lblDailyBreakdown";
            this.lblDailyBreakdown.Size = new System.Drawing.Size(130, 20);
            this.lblDailyBreakdown.TabIndex = 7;
            this.lblDailyBreakdown.Text = "Daily Breakdown";

            // dgvDailyBreakdown
            this.dgvDailyBreakdown.AllowUserToAddRows = false;
            this.dgvDailyBreakdown.AllowUserToDeleteRows = false;
            this.dgvDailyBreakdown.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDailyBreakdown.Location = new System.Drawing.Point(20, 280);
            this.dgvDailyBreakdown.Name = "dgvDailyBreakdown";
            this.dgvDailyBreakdown.ReadOnly = true;
            this.dgvDailyBreakdown.Size = new System.Drawing.Size(530, 150);
            this.dgvDailyBreakdown.TabIndex = 8;

            // lblMealTypeAnalysis
            this.lblMealTypeAnalysis.AutoSize = true;
            this.lblMealTypeAnalysis.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMealTypeAnalysis.Location = new System.Drawing.Point(570, 250);
            this.lblMealTypeAnalysis.Name = "lblMealTypeAnalysis";
            this.lblMealTypeAnalysis.Size = new System.Drawing.Size(155, 20);
            this.lblMealTypeAnalysis.TabIndex = 9;
            this.lblMealTypeAnalysis.Text = "Meal Type Analysis";

            // dgvMealTypeAnalysis
            this.dgvMealTypeAnalysis.AllowUserToAddRows = false;
            this.dgvMealTypeAnalysis.AllowUserToDeleteRows = false;
            this.dgvMealTypeAnalysis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMealTypeAnalysis.Location = new System.Drawing.Point(570, 280);
            this.dgvMealTypeAnalysis.Name = "dgvMealTypeAnalysis";
            this.dgvMealTypeAnalysis.ReadOnly = true;
            this.dgvMealTypeAnalysis.Size = new System.Drawing.Size(380, 150);
            this.dgvMealTypeAnalysis.TabIndex = 10;

            // lblTopFoods
            this.lblTopFoods.AutoSize = true;
            this.lblTopFoods.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTopFoods.Location = new System.Drawing.Point(20, 450);
            this.lblTopFoods.Name = "lblTopFoods";
            this.lblTopFoods.Size = new System.Drawing.Size(180, 20);
            this.lblTopFoods.TabIndex = 11;
            this.lblTopFoods.Text = "Top 10 Foods Logged";

            // dgvTopFoods
            this.dgvTopFoods.AllowUserToAddRows = false;
            this.dgvTopFoods.AllowUserToDeleteRows = false;
            this.dgvTopFoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopFoods.Location = new System.Drawing.Point(20, 480);
            this.dgvTopFoods.Name = "dgvTopFoods";
            this.dgvTopFoods.ReadOnly = true;
            this.dgvTopFoods.Size = new System.Drawing.Size(530, 150);
            this.dgvTopFoods.TabIndex = 12;

            // ReportsAnalyticsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 660);
            this.Controls.Add(this.dgvTopFoods);
            this.Controls.Add(this.lblTopFoods);
            this.Controls.Add(this.dgvMealTypeAnalysis);
            this.Controls.Add(this.lblMealTypeAnalysis);
            this.Controls.Add(this.dgvDailyBreakdown);
            this.Controls.Add(this.lblDailyBreakdown);
            this.Controls.Add(this.lblSummaryStats);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.lblTitle);
            this.Name = "ReportsAnalyticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports & Analytics - Health Tracker";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDailyBreakdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMealTypeAnalysis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopFoods)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Label lblSummaryStats;
        private System.Windows.Forms.Label lblDailyBreakdown;
        private System.Windows.Forms.DataGridView dgvDailyBreakdown;
        private System.Windows.Forms.Label lblMealTypeAnalysis;
        private System.Windows.Forms.DataGridView dgvMealTypeAnalysis;
        private System.Windows.Forms.Label lblTopFoods;
        private System.Windows.Forms.DataGridView dgvTopFoods;
    }
}