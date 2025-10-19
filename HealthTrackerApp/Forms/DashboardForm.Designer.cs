namespace HealthTrackerApp.Forms
{
    partial class DashboardForm
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblBMIValue = new System.Windows.Forms.Label();
            this.lblBMICategory = new System.Windows.Forms.Label();
            this.lblCurrentWeight = new System.Windows.Forms.Label();
            this.lblCaloriesConsumed = new System.Windows.Forms.Label();
            this.lblCaloriesBurned = new System.Windows.Forms.Label();
            this.lblNetCalories = new System.Windows.Forms.Label();
            this.lblProtein = new System.Windows.Forms.Label();
            this.lblCarbs = new System.Windows.Forms.Label();
            this.lblFats = new System.Windows.Forms.Label();
            this.lblCalorieProgress = new System.Windows.Forms.Label();
            this.lblWeeklyMeals = new System.Windows.Forms.Label();
            this.lblWeeklyWorkouts = new System.Windows.Forms.Label();
            this.lblAvgCalories = new System.Windows.Forms.Label();
            this.progressBarCalories = new System.Windows.Forms.ProgressBar();
            this.btnMealTracker = new System.Windows.Forms.Button();
            this.btnExerciseLogger = new System.Windows.Forms.Button();
            this.btnHealthMetrics = new System.Windows.Forms.Button();
            this.btnGoals = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(200, 30);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome!";

            // lblBMIValue
            this.lblBMIValue.AutoSize = true;
            this.lblBMIValue.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblBMIValue.Location = new System.Drawing.Point(20, 70);
            this.lblBMIValue.Name = "lblBMIValue";
            this.lblBMIValue.Size = new System.Drawing.Size(100, 21);
            this.lblBMIValue.TabIndex = 1;
            this.lblBMIValue.Text = "BMI: N/A";

            // lblBMICategory
            this.lblBMICategory.AutoSize = true;
            this.lblBMICategory.Location = new System.Drawing.Point(20, 100);
            this.lblBMICategory.Name = "lblBMICategory";
            this.lblBMICategory.Size = new System.Drawing.Size(100, 15);
            this.lblBMICategory.TabIndex = 2;
            this.lblBMICategory.Text = "Category";

            // lblCurrentWeight
            this.lblCurrentWeight.AutoSize = true;
            this.lblCurrentWeight.Location = new System.Drawing.Point(20, 120);
            this.lblCurrentWeight.Name = "lblCurrentWeight";
            this.lblCurrentWeight.Size = new System.Drawing.Size(100, 15);
            this.lblCurrentWeight.TabIndex = 3;
            this.lblCurrentWeight.Text = "Weight";

            // lblCaloriesConsumed
            this.lblCaloriesConsumed.AutoSize = true;
            this.lblCaloriesConsumed.Location = new System.Drawing.Point(20, 160);
            this.lblCaloriesConsumed.Name = "lblCaloriesConsumed";
            this.lblCaloriesConsumed.Size = new System.Drawing.Size(150, 15);
            this.lblCaloriesConsumed.TabIndex = 4;
            this.lblCaloriesConsumed.Text = "Calories Consumed: 0";

            // lblCaloriesBurned
            this.lblCaloriesBurned.AutoSize = true;
            this.lblCaloriesBurned.Location = new System.Drawing.Point(20, 180);
            this.lblCaloriesBurned.Name = "lblCaloriesBurned";
            this.lblCaloriesBurned.Size = new System.Drawing.Size(150, 15);
            this.lblCaloriesBurned.TabIndex = 5;
            this.lblCaloriesBurned.Text = "Calories Burned: 0";

            // lblNetCalories
            this.lblNetCalories.AutoSize = true;
            this.lblNetCalories.Location = new System.Drawing.Point(20, 200);
            this.lblNetCalories.Name = "lblNetCalories";
            this.lblNetCalories.Size = new System.Drawing.Size(150, 15);
            this.lblNetCalories.TabIndex = 6;
            this.lblNetCalories.Text = "Net Calories: 0";

            // lblProtein
            this.lblProtein.AutoSize = true;
            this.lblProtein.Location = new System.Drawing.Point(20, 230);
            this.lblProtein.Name = "lblProtein";
            this.lblProtein.Size = new System.Drawing.Size(100, 15);
            this.lblProtein.TabIndex = 7;
            this.lblProtein.Text = "Protein: 0g";

            // lblCarbs
            this.lblCarbs.AutoSize = true;
            this.lblCarbs.Location = new System.Drawing.Point(20, 250);
            this.lblCarbs.Name = "lblCarbs";
            this.lblCarbs.Size = new System.Drawing.Size(100, 15);
            this.lblCarbs.TabIndex = 8;
            this.lblCarbs.Text = "Carbs: 0g";

            // lblFats
            this.lblFats.AutoSize = true;
            this.lblFats.Location = new System.Drawing.Point(20, 270);
            this.lblFats.Name = "lblFats";
            this.lblFats.Size = new System.Drawing.Size(100, 15);
            this.lblFats.TabIndex = 9;
            this.lblFats.Text = "Fats: 0g";

            // progressBarCalories
            this.progressBarCalories.Location = new System.Drawing.Point(20, 300);
            this.progressBarCalories.Name = "progressBarCalories";
            this.progressBarCalories.Size = new System.Drawing.Size(300, 23);
            this.progressBarCalories.TabIndex = 10;

            // lblCalorieProgress
            this.lblCalorieProgress.AutoSize = true;
            this.lblCalorieProgress.Location = new System.Drawing.Point(20, 330);
            this.lblCalorieProgress.Name = "lblCalorieProgress";
            this.lblCalorieProgress.Size = new System.Drawing.Size(150, 15);
            this.lblCalorieProgress.TabIndex = 11;
            this.lblCalorieProgress.Text = "0% of daily target";

            // lblWeeklyMeals
            this.lblWeeklyMeals.AutoSize = true;
            this.lblWeeklyMeals.Location = new System.Drawing.Point(20, 370);
            this.lblWeeklyMeals.Name = "lblWeeklyMeals";
            this.lblWeeklyMeals.Size = new System.Drawing.Size(150, 15);
            this.lblWeeklyMeals.TabIndex = 12;
            this.lblWeeklyMeals.Text = "Meals Logged: 0";

            // lblWeeklyWorkouts
            this.lblWeeklyWorkouts.AutoSize = true;
            this.lblWeeklyWorkouts.Location = new System.Drawing.Point(20, 390);
            this.lblWeeklyWorkouts.Name = "lblWeeklyWorkouts";
            this.lblWeeklyWorkouts.Size = new System.Drawing.Size(150, 15);
            this.lblWeeklyWorkouts.TabIndex = 13;
            this.lblWeeklyWorkouts.Text = "Workouts: 0";

            // lblAvgCalories
            this.lblAvgCalories.AutoSize = true;
            this.lblAvgCalories.Location = new System.Drawing.Point(20, 410);
            this.lblAvgCalories.Name = "lblAvgCalories";
            this.lblAvgCalories.Size = new System.Drawing.Size(200, 15);
            this.lblAvgCalories.TabIndex = 14;
            this.lblAvgCalories.Text = "Avg Calories: 0 kcal/day";

            // btnMealTracker
            this.btnMealTracker.Location = new System.Drawing.Point(350, 70);
            this.btnMealTracker.Name = "btnMealTracker";
            this.btnMealTracker.Size = new System.Drawing.Size(150, 40);
            this.btnMealTracker.TabIndex = 15;
            this.btnMealTracker.Text = "Meal Tracker";
            this.btnMealTracker.UseVisualStyleBackColor = true;
            this.btnMealTracker.Click += new System.EventHandler(this.btnMealTracker_Click);

            // btnExerciseLogger
            this.btnExerciseLogger.Location = new System.Drawing.Point(350, 120);
            this.btnExerciseLogger.Name = "btnExerciseLogger";
            this.btnExerciseLogger.Size = new System.Drawing.Size(150, 40);
            this.btnExerciseLogger.TabIndex = 16;
            this.btnExerciseLogger.Text = "Exercise Logger";
            this.btnExerciseLogger.UseVisualStyleBackColor = true;
            this.btnExerciseLogger.Click += new System.EventHandler(this.btnExerciseLogger_Click);

            // btnHealthMetrics
            this.btnHealthMetrics.Location = new System.Drawing.Point(350, 170);
            this.btnHealthMetrics.Name = "btnHealthMetrics";
            this.btnHealthMetrics.Size = new System.Drawing.Size(150, 40);
            this.btnHealthMetrics.TabIndex = 17;
            this.btnHealthMetrics.Text = "Health Metrics";
            this.btnHealthMetrics.UseVisualStyleBackColor = true;
            this.btnHealthMetrics.Click += new System.EventHandler(this.btnHealthMetrics_Click);

            // btnGoals
            this.btnGoals.Location = new System.Drawing.Point(350, 220);
            this.btnGoals.Name = "btnGoals";
            this.btnGoals.Size = new System.Drawing.Size(150, 40);
            this.btnGoals.TabIndex = 18;
            this.btnGoals.Text = "Goals";
            this.btnGoals.UseVisualStyleBackColor = true;
            this.btnGoals.Click += new System.EventHandler(this.btnGoals_Click);

            // btnReports
            this.btnReports.Location = new System.Drawing.Point(350, 270);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(150, 40);
            this.btnReports.TabIndex = 19;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);

            // btnLogout
            this.btnLogout.Location = new System.Drawing.Point(350, 320);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(150, 40);
            this.btnLogout.TabIndex = 20;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // DashboardForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 500);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnGoals);
            this.Controls.Add(this.btnHealthMetrics);
            this.Controls.Add(this.btnExerciseLogger);
            this.Controls.Add(this.btnMealTracker);
            this.Controls.Add(this.lblAvgCalories);
            this.Controls.Add(this.lblWeeklyWorkouts);
            this.Controls.Add(this.lblWeeklyMeals);
            this.Controls.Add(this.lblCalorieProgress);
            this.Controls.Add(this.progressBarCalories);
            this.Controls.Add(this.lblFats);
            this.Controls.Add(this.lblCarbs);
            this.Controls.Add(this.lblProtein);
            this.Controls.Add(this.lblNetCalories);
            this.Controls.Add(this.lblCaloriesBurned);
            this.Controls.Add(this.lblCaloriesConsumed);
            this.Controls.Add(this.lblCurrentWeight);
            this.Controls.Add(this.lblBMICategory);
            this.Controls.Add(this.lblBMIValue);
            this.Controls.Add(this.lblWelcome);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Health Tracker - Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Control declarations
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblBMIValue;
        private System.Windows.Forms.Label lblBMICategory;
        private System.Windows.Forms.Label lblCurrentWeight;
        private System.Windows.Forms.Label lblCaloriesConsumed;
        private System.Windows.Forms.Label lblCaloriesBurned;
        private System.Windows.Forms.Label lblNetCalories;
        private System.Windows.Forms.Label lblProtein;
        private System.Windows.Forms.Label lblCarbs;
        private System.Windows.Forms.Label lblFats;
        private System.Windows.Forms.Label lblCalorieProgress;
        private System.Windows.Forms.Label lblWeeklyMeals;
        private System.Windows.Forms.Label lblWeeklyWorkouts;
        private System.Windows.Forms.Label lblAvgCalories;
        private System.Windows.Forms.ProgressBar progressBarCalories;
        private System.Windows.Forms.Button btnMealTracker;
        private System.Windows.Forms.Button btnExerciseLogger;
        private System.Windows.Forms.Button btnHealthMetrics;
        private System.Windows.Forms.Button btnGoals;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnLogout;
    }
}