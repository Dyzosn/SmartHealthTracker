using System.Runtime.ConstrainedExecution;
using static System.Windows.Forms.LinkLabel;

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
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.lblFoodSearch = new System.Windows.Forms.Label();
            this.txtFoodSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.dgvSearchResults = new System.Windows.Forms.DataGridView();
            this.grpAddFood = new System.Windows.Forms.GroupBox();
            this.lblSelectedFood = new System.Windows.Forms.Label();
            this.lblPortionSize = new System.Windows.Forms.Label();
            this.txtPortionSize = new System.Windows.Forms.TextBox();
            this.lblMealType = new System.Windows.Forms.Label();
            this.cboMealType = new System.Windows.Forms.ComboBox();
            this.lblMealName = new System.Windows.Forms.Label();
            this.txtMealName = new System.Windows.Forms.TextBox();
            this.btnAddFood = new System.Windows.Forms.Button();
            this.grpCurrentMeal = new System.Windows.Forms.GroupBox();
            this.lblTodaySummary = new System.Windows.Forms.Label();
            this.lblCurrentMeal = new System.Windows.Forms.Label();
            this.dgvCurrentMealFoods = new System.Windows.Forms.DataGridView();
            this.btnSaveMeal = new System.Windows.Forms.Button();
            this.grpHistory = new System.Windows.Forms.GroupBox();
            this.dgvMealHistory = new System.Windows.Forms.DataGridView();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).BeginInit();
            this.grpAddFood.SuspendLayout();
            this.grpCurrentMeal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentMealFoods)).BeginInit();
            this.grpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMealHistory)).BeginInit();
            this.SuspendLayout();

            // grpSearch
            this.grpSearch.Controls.Add(this.dgvSearchResults);
            this.grpSearch.Controls.Add(this.lblSearchStatus);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.txtFoodSearch);
            this.grpSearch.Controls.Add(this.lblFoodSearch);
            this.grpSearch.Location = new System.Drawing.Point(12, 12);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(450, 320);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search Foods";

            // lblFoodSearch
            this.lblFoodSearch.AutoSize = true;
            this.lblFoodSearch.Location = new System.Drawing.Point(15, 25);
            this.lblFoodSearch.Name = "lblFoodSearch";
            this.lblFoodSearch.Size = new System.Drawing.Size(75, 15);
            this.lblFoodSearch.TabIndex = 0;
            this.lblFoodSearch.Text = "Search Food:";

            // txtFoodSearch
            this.txtFoodSearch.Location = new System.Drawing.Point(15, 45);
            this.txtFoodSearch.Name = "txtFoodSearch";
            this.txtFoodSearch.Size = new System.Drawing.Size(300, 23);
            this.txtFoodSearch.TabIndex = 1;

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(325, 43);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(110, 27);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;

            // lblSearchStatus
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Location = new System.Drawing.Point(15, 75);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(120, 15);
            this.lblSearchStatus.TabIndex = 3;
            this.lblSearchStatus.Text = "Enter food to search";

            // dgvSearchResults
            this.dgvSearchResults.AllowUserToAddRows = false;
            this.dgvSearchResults.AllowUserToDeleteRows = false;
            this.dgvSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchResults.Location = new System.Drawing.Point(15, 95);
            this.dgvSearchResults.MultiSelect = false;
            this.dgvSearchResults.Name = "dgvSearchResults";
            this.dgvSearchResults.ReadOnly = true;
            this.dgvSearchResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchResults.Size = new System.Drawing.Size(420, 210);
            this.dgvSearchResults.TabIndex = 4;

            // grpAddFood
            this.grpAddFood.Controls.Add(this.btnAddFood);
            this.grpAddFood.Controls.Add(this.txtMealName);
            this.grpAddFood.Controls.Add(this.lblMealName);
            this.grpAddFood.Controls.Add(this.cboMealType);
            this.grpAddFood.Controls.Add(this.lblMealType);
            this.grpAddFood.Controls.Add(this.txtPortionSize);
            this.grpAddFood.Controls.Add(this.lblPortionSize);
            this.grpAddFood.Controls.Add(this.lblSelectedFood);
            this.grpAddFood.Location = new System.Drawing.Point(480, 12);
            this.grpAddFood.Name = "grpAddFood";
            this.grpAddFood.Size = new System.Drawing.Size(320, 320);
            this.grpAddFood.TabIndex = 1;
            this.grpAddFood.TabStop = false;
            this.grpAddFood.Text = "Add Food to Meal";

            // lblSelectedFood
            this.lblSelectedFood.Location = new System.Drawing.Point(15, 25);
            this.lblSelectedFood.Name = "lblSelectedFood";
            this.lblSelectedFood.Size = new System.Drawing.Size(290, 40);
            this.lblSelectedFood.TabIndex = 0;
            this.lblSelectedFood.Text = "Select a food from search results";

            // lblMealType
            this.lblMealType.AutoSize = true;
            this.lblMealType.Location = new System.Drawing.Point(15, 75);
            this.lblMealType.Name = "lblMealType";
            this.lblMealType.Size = new System.Drawing.Size(63, 15);
            this.lblMealType.TabIndex = 1;
            this.lblMealType.Text = "Meal Type:";

            // cboMealType
            this.cboMealType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMealType.FormattingEnabled = true;
            this.cboMealType.Location = new System.Drawing.Point(15, 95);
            this.cboMealType.Name = "cboMealType";
            this.cboMealType.Size = new System.Drawing.Size(290, 23);
            this.cboMealType.TabIndex = 2;

            // lblMealName
            this.lblMealName.AutoSize = true;
            this.lblMealName.Location = new System.Drawing.Point(15, 130);
            this.lblMealName.Name = "lblMealName";
            this.lblMealName.Size = new System.Drawing.Size(70, 15);
            this.lblMealName.TabIndex = 3;
            this.lblMealName.Text = "Meal Name:";

            // txtMealName
            this.txtMealName.Location = new System.Drawing.Point(15, 150);
            this.txtMealName.Name = "txtMealName";
            this.txtMealName.Size = new System.Drawing.Size(290, 23);
            this.txtMealName.TabIndex = 4;

            // lblPortionSize
            this.lblPortionSize.AutoSize = true;
            this.lblPortionSize.Location = new System.Drawing.Point(15, 185);
            this.lblPortionSize.Name = "lblPortionSize";
            this.lblPortionSize.Size = new System.Drawing.Size(100, 15);
            this.lblPortionSize.TabIndex = 5;
            this.lblPortionSize.Text = "Portion Size (g):";

            // txtPortionSize
            this.txtPortionSize.Location = new System.Drawing.Point(15, 205);
            this.txtPortionSize.Name = "txtPortionSize";
            this.txtPortionSize.Size = new System.Drawing.Size(290, 23);
            this.txtPortionSize.TabIndex = 6;

            // btnAddFood
            this.btnAddFood.Location = new System.Drawing.Point(15, 245);
            this.btnAddFood.Name = "btnAddFood";
            this.btnAddFood.Size = new System.Drawing.Size(290, 35);
            this.btnAddFood.TabIndex = 7;
            this.btnAddFood.Text = "Add Food to Meal";
            this.btnAddFood.UseVisualStyleBackColor = true;

            // grpCurrentMeal
            this.grpCurrentMeal.Controls.Add(this.btnSaveMeal);
            this.grpCurrentMeal.Controls.Add(this.dgvCurrentMealFoods);
            this.grpCurrentMeal.Controls.Add(this.lblCurrentMeal);
            this.grpCurrentMeal.Controls.Add(this.lblTodaySummary);
            this.grpCurrentMeal.Location = new System.Drawing.Point(12, 345);
            this.grpCurrentMeal.Name = "grpCurrentMeal";
            this.grpCurrentMeal.Size = new System.Drawing.Size(788, 220);
            this.grpCurrentMeal.TabIndex = 2;
            this.grpCurrentMeal.TabStop = false;
            this.grpCurrentMeal.Text = "Today's Meals";

            // lblTodaySummary
            this.lblTodaySummary.AutoSize = true;
            this.lblTodaySummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTodaySummary.Location = new System.Drawing.Point(15, 25);
            this.lblTodaySummary.Name = "lblTodaySummary";
            this.lblTodaySummary.Size = new System.Drawing.Size(200, 19);
            this.lblTodaySummary.TabIndex = 0;
            this.lblTodaySummary.Text = "Today's Total: 0 kcal";

            // lblCurrentMeal
            this.lblCurrentMeal.AutoSize = true;
            this.lblCurrentMeal.Location = new System.Drawing.Point(15, 50);
            this.lblCurrentMeal.Name = "lblCurrentMeal";
            this.lblCurrentMeal.Size = new System.Drawing.Size(150, 15);
            this.lblCurrentMeal.TabIndex = 1;
            this.lblCurrentMeal.Text = "Current Meal: 0 kcal";

            // dgvCurrentMealFoods
            this.dgvCurrentMealFoods.AllowUserToAddRows = false;
            this.dgvCurrentMealFoods.AllowUserToDeleteRows = false;
            this.dgvCurrentMealFoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrentMealFoods.Location = new System.Drawing.Point(15, 75);
            this.dgvCurrentMealFoods.Name = "dgvCurrentMealFoods";
            this.dgvCurrentMealFoods.ReadOnly = true;
            this.dgvCurrentMealFoods.Size = new System.Drawing.Size(620, 130);
            this.dgvCurrentMealFoods.TabIndex = 2;

            // btnSaveMeal
            this.btnSaveMeal.Location = new System.Drawing.Point(650, 75);
            this.btnSaveMeal.Name = "btnSaveMeal";
            this.btnSaveMeal.Size = new System.Drawing.Size(120, 35);
            this.btnSaveMeal.TabIndex = 3;
            this.btnSaveMeal.Text = "Save Meal";
            this.btnSaveMeal.UseVisualStyleBackColor = true;

            // grpHistory
            this.grpHistory.Controls.Add(this.dgvMealHistory);
            this.grpHistory.Location = new System.Drawing.Point(12, 575);
            this.grpHistory.Name = "grpHistory";
            this.grpHistory.Size = new System.Drawing.Size(788, 200);
            this.grpHistory.TabIndex = 3;
            this.grpHistory.TabStop = false;
            this.grpHistory.Text = "Meal History (Last 20)";

            // dgvMealHistory
            this.dgvMealHistory.AllowUserToAddRows = false;
            this.dgvMealHistory.AllowUserToDeleteRows = false;
            this.dgvMealHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMealHistory.Location = new System.Drawing.Point(15, 25);
            this.dgvMealHistory.Name = "dgvMealHistory";
            this.dgvMealHistory.ReadOnly = true;
            this.dgvMealHistory.Size = new System.Drawing.Size(755, 160);
            this.dgvMealHistory.TabIndex = 0;

            // MealTrackerForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 790);
            this.Controls.Add(this.grpHistory);
            this.Controls.Add(this.grpCurrentMeal);
            this.Controls.Add(this.grpAddFood);
            this.Controls.Add(this.grpSearch);
            this.Name = "MealTrackerForm";
            this.Text = "Meal Tracker - Health Tracker";
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).EndInit();
            this.grpAddFood.ResumeLayout(false);
            this.grpAddFood.PerformLayout();
            this.grpCurrentMeal.ResumeLayout(false);
            this.grpCurrentMeal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentMealFoods)).EndInit();
            this.grpHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMealHistory)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Label lblFoodSearch;
        private System.Windows.Forms.TextBox txtFoodSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSearchStatus;
        private System.Windows.Forms.DataGridView dgvSearchResults;
        private System.Windows.Forms.GroupBox grpAddFood;
        private System.Windows.Forms.Label lblSelectedFood;
        private System.Windows.Forms.Label lblMealType;
        private System.Windows.Forms.ComboBox cboMealType;
        private System.Windows.Forms.Label lblMealName;
        private System.Windows.Forms.TextBox txtMealName;
        private System.Windows.Forms.Label lblPortionSize;
        private System.Windows.Forms.TextBox txtPortionSize;
        private System.Windows.Forms.Button btnAddFood;
        private System.Windows.Forms.GroupBox grpCurrentMeal;
        private System.Windows.Forms.Label lblTodaySummary;
        private System.Windows.Forms.Label lblCurrentMeal;
        private System.Windows.Forms.DataGridView dgvCurrentMealFoods;
        private System.Windows.Forms.Button btnSaveMeal;
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.DataGridView dgvMealHistory;
    }
}