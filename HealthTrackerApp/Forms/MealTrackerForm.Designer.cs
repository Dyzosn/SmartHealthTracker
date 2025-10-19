namespace HealthTrackerApp.Forms
{
    partial class MealTrackerForm
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
            this.txtFoodSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.dgvSearchResults = new System.Windows.Forms.DataGridView();
            this.lblSelectedFood = new System.Windows.Forms.Label();
            this.txtPortionSize = new System.Windows.Forms.TextBox();
            this.lblPortionSize = new System.Windows.Forms.Label();
            this.btnAddFood = new System.Windows.Forms.Button();
            this.cboMealType = new System.Windows.Forms.ComboBox();
            this.lblMealType = new System.Windows.Forms.Label();
            this.dtpMealDate = new System.Windows.Forms.DateTimePicker();
            this.lblMealDate = new System.Windows.Forms.Label();
            this.txtMealName = new System.Windows.Forms.TextBox();
            this.lblMealName = new System.Windows.Forms.Label();
            this.dgvCurrentMeal = new System.Windows.Forms.DataGridView();
            this.lblMealSummary = new System.Windows.Forms.Label();
            this.btnSaveMeal = new System.Windows.Forms.Button();
            this.lblTodaySummary = new System.Windows.Forms.Label();
            this.dgvMealHistory = new System.Windows.Forms.DataGridView();
            this.btnDeleteMeal = new System.Windows.Forms.Button();
            this.lblMealDetails = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentMeal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMealHistory)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Meal Tracker";

            // txtFoodSearch
            this.txtFoodSearch.Location = new System.Drawing.Point(20, 70);
            this.txtFoodSearch.Name = "txtFoodSearch";
            this.txtFoodSearch.Size = new System.Drawing.Size(300, 23);
            this.txtFoodSearch.TabIndex = 1;
            this.txtFoodSearch.PlaceholderText = "Search foods...";

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(330, 70);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 25);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;

            // lblSearchStatus
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Location = new System.Drawing.Point(440, 75);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(100, 15);
            this.lblSearchStatus.TabIndex = 3;
            this.lblSearchStatus.Text = "";

            // dgvSearchResults
            this.dgvSearchResults.AllowUserToAddRows = false;
            this.dgvSearchResults.AllowUserToDeleteRows = false;
            this.dgvSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchResults.Location = new System.Drawing.Point(20, 100);
            this.dgvSearchResults.MultiSelect = false;
            this.dgvSearchResults.Name = "dgvSearchResults";
            this.dgvSearchResults.ReadOnly = true;
            this.dgvSearchResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchResults.Size = new System.Drawing.Size(600, 150);
            this.dgvSearchResults.TabIndex = 4;

            // lblSelectedFood
            this.lblSelectedFood.AutoSize = true;
            this.lblSelectedFood.Location = new System.Drawing.Point(20, 260);
            this.lblSelectedFood.Name = "lblSelectedFood";
            this.lblSelectedFood.Size = new System.Drawing.Size(100, 15);
            this.lblSelectedFood.TabIndex = 5;
            this.lblSelectedFood.Text = "Selected: None";

            // lblPortionSize
            this.lblPortionSize.AutoSize = true;
            this.lblPortionSize.Location = new System.Drawing.Point(20, 285);
            this.lblPortionSize.Name = "lblPortionSize";
            this.lblPortionSize.Size = new System.Drawing.Size(100, 15);
            this.lblPortionSize.TabIndex = 6;
            this.lblPortionSize.Text = "Portion Size (g):";

            // txtPortionSize
            this.txtPortionSize.Location = new System.Drawing.Point(120, 282);
            this.txtPortionSize.Name = "txtPortionSize";
            this.txtPortionSize.Size = new System.Drawing.Size(100, 23);
            this.txtPortionSize.TabIndex = 7;

            // btnAddFood
            this.btnAddFood.Location = new System.Drawing.Point(230, 280);
            this.btnAddFood.Name = "btnAddFood";
            this.btnAddFood.Size = new System.Drawing.Size(100, 25);
            this.btnAddFood.TabIndex = 8;
            this.btnAddFood.Text = "Add to Meal";
            this.btnAddFood.UseVisualStyleBackColor = true;

            // lblMealType
            this.lblMealType.AutoSize = true;
            this.lblMealType.Location = new System.Drawing.Point(650, 75);
            this.lblMealType.Name = "lblMealType";
            this.lblMealType.Size = new System.Drawing.Size(65, 15);
            this.lblMealType.TabIndex = 9;
            this.lblMealType.Text = "Meal Type:";

            // cboMealType
            this.cboMealType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMealType.FormattingEnabled = true;
            this.cboMealType.Location = new System.Drawing.Point(720, 72);
            this.cboMealType.Name = "cboMealType";
            this.cboMealType.Size = new System.Drawing.Size(150, 23);
            this.cboMealType.TabIndex = 10;

            // lblMealDate
            this.lblMealDate.AutoSize = true;
            this.lblMealDate.Location = new System.Drawing.Point(650, 105);
            this.lblMealDate.Name = "lblMealDate";
            this.lblMealDate.Size = new System.Drawing.Size(35, 15);
            this.lblMealDate.TabIndex = 11;
            this.lblMealDate.Text = "Date:";

            // dtpMealDate
            this.dtpMealDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpMealDate.Location = new System.Drawing.Point(720, 102);
            this.dtpMealDate.Name = "dtpMealDate";
            this.dtpMealDate.Size = new System.Drawing.Size(150, 23);
            this.dtpMealDate.TabIndex = 12;

            // lblMealName
            this.lblMealName.AutoSize = true;
            this.lblMealName.Location = new System.Drawing.Point(650, 135);
            this.lblMealName.Name = "lblMealName";
            this.lblMealName.Size = new System.Drawing.Size(70, 15);
            this.lblMealName.TabIndex = 13;
            this.lblMealName.Text = "Meal Name:";

            // txtMealName
            this.txtMealName.Location = new System.Drawing.Point(720, 132);
            this.txtMealName.Name = "txtMealName";
            this.txtMealName.Size = new System.Drawing.Size(150, 23);
            this.txtMealName.TabIndex = 14;

            // dgvCurrentMeal
            this.dgvCurrentMeal.AllowUserToAddRows = false;
            this.dgvCurrentMeal.AllowUserToDeleteRows = false;
            this.dgvCurrentMeal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrentMeal.Location = new System.Drawing.Point(20, 320);
            this.dgvCurrentMeal.Name = "dgvCurrentMeal";
            this.dgvCurrentMeal.ReadOnly = true;
            this.dgvCurrentMeal.Size = new System.Drawing.Size(850, 120);
            this.dgvCurrentMeal.TabIndex = 15;

            // lblMealSummary
            this.lblMealSummary.AutoSize = true;
            this.lblMealSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMealSummary.Location = new System.Drawing.Point(20, 450);
            this.lblMealSummary.Name = "lblMealSummary";
            this.lblMealSummary.Size = new System.Drawing.Size(100, 19);
            this.lblMealSummary.TabIndex = 16;
            this.lblMealSummary.Text = "Total: 0 kcal";

            // btnSaveMeal
            this.btnSaveMeal.Location = new System.Drawing.Point(770, 447);
            this.btnSaveMeal.Name = "btnSaveMeal";
            this.btnSaveMeal.Size = new System.Drawing.Size(100, 30);
            this.btnSaveMeal.TabIndex = 17;
            this.btnSaveMeal.Text = "Save Meal";
            this.btnSaveMeal.UseVisualStyleBackColor = true;

            // lblTodaySummary
            this.lblTodaySummary.AutoSize = true;
            this.lblTodaySummary.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTodaySummary.Location = new System.Drawing.Point(20, 490);
            this.lblTodaySummary.Name = "lblTodaySummary";
            this.lblTodaySummary.Size = new System.Drawing.Size(150, 19);
            this.lblTodaySummary.TabIndex = 18;
            this.lblTodaySummary.Text = "Today's Total: 0 kcal";

            // dgvMealHistory
            this.dgvMealHistory.AllowUserToAddRows = false;
            this.dgvMealHistory.AllowUserToDeleteRows = false;
            this.dgvMealHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMealHistory.Location = new System.Drawing.Point(20, 520);
            this.dgvMealHistory.MultiSelect = false;
            this.dgvMealHistory.Name = "dgvMealHistory";
            this.dgvMealHistory.ReadOnly = true;
            this.dgvMealHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMealHistory.Size = new System.Drawing.Size(750, 150);
            this.dgvMealHistory.TabIndex = 19;

            // btnDeleteMeal
            this.btnDeleteMeal.Location = new System.Drawing.Point(780, 520);
            this.btnDeleteMeal.Name = "btnDeleteMeal";
            this.btnDeleteMeal.Size = new System.Drawing.Size(90, 30);
            this.btnDeleteMeal.TabIndex = 20;
            this.btnDeleteMeal.Text = "Delete";
            this.btnDeleteMeal.UseVisualStyleBackColor = true;

            // lblMealDetails
            this.lblMealDetails.AutoSize = true;
            this.lblMealDetails.Location = new System.Drawing.Point(20, 680);
            this.lblMealDetails.Name = "lblMealDetails";
            this.lblMealDetails.Size = new System.Drawing.Size(100, 15);
            this.lblMealDetails.TabIndex = 21;
            this.lblMealDetails.Text = "";

            // MealTrackerForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 720);
            this.Controls.Add(this.lblMealDetails);
            this.Controls.Add(this.btnDeleteMeal);
            this.Controls.Add(this.dgvMealHistory);
            this.Controls.Add(this.lblTodaySummary);
            this.Controls.Add(this.btnSaveMeal);
            this.Controls.Add(this.lblMealSummary);
            this.Controls.Add(this.dgvCurrentMeal);
            this.Controls.Add(this.txtMealName);
            this.Controls.Add(this.lblMealName);
            this.Controls.Add(this.dtpMealDate);
            this.Controls.Add(this.lblMealDate);
            this.Controls.Add(this.cboMealType);
            this.Controls.Add(this.lblMealType);
            this.Controls.Add(this.btnAddFood);
            this.Controls.Add(this.txtPortionSize);
            this.Controls.Add(this.lblPortionSize);
            this.Controls.Add(this.lblSelectedFood);
            this.Controls.Add(this.dgvSearchResults);
            this.Controls.Add(this.lblSearchStatus);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtFoodSearch);
            this.Controls.Add(this.lblTitle);
            this.Name = "MealTrackerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Meal Tracker - Health Tracker";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentMeal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMealHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtFoodSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSearchStatus;
        private System.Windows.Forms.DataGridView dgvSearchResults;
        private System.Windows.Forms.Label lblSelectedFood;
        private System.Windows.Forms.TextBox txtPortionSize;
        private System.Windows.Forms.Label lblPortionSize;
        private System.Windows.Forms.Button btnAddFood;
        private System.Windows.Forms.ComboBox cboMealType;
        private System.Windows.Forms.Label lblMealType;
        private System.Windows.Forms.DateTimePicker dtpMealDate;
        private System.Windows.Forms.Label lblMealDate;
        private System.Windows.Forms.TextBox txtMealName;
        private System.Windows.Forms.Label lblMealName;
        private System.Windows.Forms.DataGridView dgvCurrentMeal;
        private System.Windows.Forms.Label lblMealSummary;
        private System.Windows.Forms.Button btnSaveMeal;
        private System.Windows.Forms.Label lblTodaySummary;
        private System.Windows.Forms.DataGridView dgvMealHistory;
        private System.Windows.Forms.Button btnDeleteMeal;
        private System.Windows.Forms.Label lblMealDetails;
    }
}