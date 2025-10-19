using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Models.Enums;
using HealthTrackerApp.Services;
using HealthTrackerApp.BusinessLogic.Managers;

namespace HealthTrackerApp.Forms
{
    // Form for setting and tracking health and fitness goals
    // Demonstrates delegates and events pattern with goal achievement notifications
    public partial class GoalsProgressForm : Form
    {
        private readonly GoalService _goalService;
        private readonly GoalManager _goalManager;
        private readonly User _currentUser;
        private int? _selectedGoalId;

        public GoalsProgressForm(User user)
        {
            InitializeComponent();
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));

            using (var context = new HealthTrackerContext())
            {
                _goalService = new GoalService(context);
            }

            _goalManager = new GoalManager();

            // Subscribe to goal events - demonstrates delegates and events pattern
            _goalManager.OnGoalAchieved += GoalManager_OnGoalAchieved;
            _goalManager.OnGoalProgress += GoalManager_OnGoalProgress;

            InitialiseFormControls();
            LoadGoals();
        }

        // Event handler for goal achievement - fires when goal is completed
        // Demonstrates event subscription pattern
        private void GoalManager_OnGoalAchieved(Goal goal, string message)
        {
            MessageBox.Show(message, "Goal Achieved!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadGoals(); // Refresh the display
        }

        // Event handler for goal progress updates
        // Demonstrates event subscription pattern
        private void GoalManager_OnGoalProgress(Goal goal, double progressPercent)
        {
            lblMotivation.Text = _goalManager.GetMotivationMessage(goal);

            if (progressPercent >= 0 && progressPercent <= 100)
            {
                progressBarGoal.Value = (int)progressPercent;
            }
        }

        // Sets up dropdown values and date picker defaults
        private void InitialiseFormControls()
        {
            // Populate goal type dropdown
            cboGoalType.DataSource = Enum.GetValues(typeof(GoalType));
            cboGoalType.SelectedIndex = 0;

            // Set default dates
            dtpTargetDate.Value = DateTime.Now.AddMonths(3); // Default 3 months goal
            dtpTargetDate.MinDate = DateTime.Now.AddDays(1); // Must be future date

            // Configure numeric controls
            nudTargetValue.Minimum = 0;
            nudTargetValue.Maximum = 1000;
            nudTargetValue.DecimalPlaces = 1;
            nudTargetValue.Value = 70; // Default target

            nudCurrentValue.Minimum = 0;
            nudCurrentValue.Maximum = 1000;
            nudCurrentValue.DecimalPlaces = 1;

            // Get user's latest weight for initial current value
            double latestWeight = GetLatestUserWeight();
            nudCurrentValue.Value = (decimal)latestWeight;

            progressBarGoal.Minimum = 0;
            progressBarGoal.Maximum = 100;
            progressBarGoal.Value = 0;

            // Subscribe to goal type change to update current value automatically
            cboGoalType.SelectedIndexChanged += CboGoalType_SelectedIndexChanged;
        }

        // Gets user's latest weight from health metrics
        // Returns latest weight or 70kg default if no weight recorded
        private double GetLatestUserWeight()
        {
            using (var context = new HealthTrackerContext())
            {
                var latestWeight = context.HealthMetrics
                    .Where(hm => hm.UserId == _currentUser.UserId && hm.WeightKg.HasValue)
                    .OrderByDescending(hm => hm.RecordedDate)
                    .FirstOrDefault();

                return latestWeight?.WeightKg ?? 70.0; // Default 70kg if no weight recorded
            }
        }

        // Auto-update current value when goal type changes
        // For weight-related goals, fetch latest weight from health metrics
        private void CboGoalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GoalType selectedType = (GoalType)cboGoalType.SelectedItem;

            switch (selectedType)
            {
                case GoalType.WeightLoss:
                case GoalType.WeightGain:
                case GoalType.MuscleGain:
                    // Auto-populate current value with latest weight
                    double latestWeight = GetLatestUserWeight();
                    nudCurrentValue.Value = (decimal)latestWeight;

                    // Set reasonable target based on type
                    if (selectedType == GoalType.WeightLoss)
                    {
                        nudTargetValue.Value = nudCurrentValue.Value - 5; // Suggest 5kg loss
                    }
                    else
                    {
                        nudTargetValue.Value = nudCurrentValue.Value + 5; // Suggest 5kg gain
                    }
                    break;

                case GoalType.CalorieTarget:
                    // Set typical calorie target
                    nudTargetValue.Value = 2000;
                    nudCurrentValue.Value = 2200; // Start above target
                    break;

                case GoalType.ExerciseFrequency:
                    // Set typical exercise frequency
                    nudTargetValue.Value = 20; // 20 workouts per month
                    nudCurrentValue.Value = 0; // Starting from zero
                    break;
            }
        }

        // Loads active and completed goals into DataGridViews
        private void LoadGoals()
        {
            using (var context = new HealthTrackerContext())
            {
                var service = new GoalService(context);

                // Load active goals
                var activeGoals = service.GetActiveGoals(_currentUser.UserId);
                dgvActiveGoals.DataSource = activeGoals.Select(g => new
                {
                    g.GoalId,
                    Type = g.GoalType.ToString(),
                    g.Description,
                    Target = $"{g.TargetValue:F1}",
                    Current = $"{g.CurrentValue:F1}",
                    Progress = $"{CalculateProgressPercent(g):F0}%",
                    Deadline = g.TargetDate.ToString("dd/MM/yyyy"),
                    DaysLeft = (g.TargetDate - DateTime.Now).Days
                }).ToList();

                FormatActiveGoalsDataGridView();

                // Load completed goals
                var completedGoals = service.GetCompletedGoals(_currentUser.UserId);
                dgvCompletedGoals.DataSource = completedGoals.Select(g => new
                {
                    g.GoalId,
                    Type = g.GoalType.ToString(),
                    g.Description,
                    Target = $"{g.TargetValue:F1}",
                    Achieved = $"{g.CurrentValue:F1}",
                    Completed = g.CompletedDate?.ToString("dd/MM/yyyy") ?? "N/A"
                }).ToList();

                FormatCompletedGoalsDataGridView();
            }
        }

        // Calculates goal progress percentage using GoalManager
        private double CalculateProgressPercent(Goal goal)
        {
            return _goalManager.CalculateProgress(goal);
        }

        // Formats the active goals DataGridView
        private void FormatActiveGoalsDataGridView()
        {
            if (dgvActiveGoals.Columns.Count == 0) return;

            dgvActiveGoals.Columns["GoalId"].Visible = false;
            dgvActiveGoals.Columns["Type"].Width = 120;
            dgvActiveGoals.Columns["Description"].Width = 200;
            dgvActiveGoals.Columns["Target"].Width = 80;
            dgvActiveGoals.Columns["Current"].Width = 80;
            dgvActiveGoals.Columns["Progress"].Width = 80;
            dgvActiveGoals.Columns["Deadline"].Width = 100;
            dgvActiveGoals.Columns["DaysLeft"].Width = 80;

            dgvActiveGoals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvActiveGoals.AllowUserToAddRows = false;
            dgvActiveGoals.ReadOnly = true;
            dgvActiveGoals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Formats the completed goals DataGridView
        private void FormatCompletedGoalsDataGridView()
        {
            if (dgvCompletedGoals.Columns.Count == 0) return;

            dgvCompletedGoals.Columns["GoalId"].Visible = false;
            dgvCompletedGoals.Columns["Type"].Width = 120;
            dgvCompletedGoals.Columns["Description"].Width = 250;
            dgvCompletedGoals.Columns["Target"].Width = 80;
            dgvCompletedGoals.Columns["Achieved"].Width = 80;
            dgvCompletedGoals.Columns["Completed"].Width = 100;

            dgvCompletedGoals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvCompletedGoals.AllowUserToAddRows = false;
            dgvCompletedGoals.ReadOnly = true;
            dgvCompletedGoals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Creates a new goal in the database
        private void btnCreateGoal_Click(object sender, EventArgs e)
        {
            if (!ValidateGoalInput())
                return;

            try
            {
                using (var context = new HealthTrackerContext())
                {
                    var service = new GoalService(context);

                    var goal = service.CreateGoal(
                        _currentUser.UserId,
                        (GoalType)cboGoalType.SelectedItem,
                        txtDescription.Text.Trim(),
                        (double)nudTargetValue.Value,
                        (double)nudCurrentValue.Value,
                        dtpTargetDate.Value
                    );

                    MessageBox.Show("Goal created successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadGoals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating goal: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Updates the progress of a selected goal
        // This triggers the GoalManager events if goal is achieved
        private void btnUpdateProgress_Click(object sender, EventArgs e)
        {
            if (dgvActiveGoals.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a goal to update.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int goalId = Convert.ToInt32(dgvActiveGoals.SelectedRows[0].Cells["GoalId"].Value);

                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter new current value:",
                    "Update Progress",
                    "",
                    -1, -1);

                if (string.IsNullOrWhiteSpace(input))
                    return;

                if (!double.TryParse(input, out double newValue))
                {
                    MessageBox.Show("Please enter a valid number.", "Invalid Input",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var context = new HealthTrackerContext())
                {
                    var service = new GoalService(context);
                    var goal = service.GetGoalById(goalId);

                    if (goal != null)
                    {
                        // Use GoalManager to update progress - this fires events
                        _goalManager.UpdateProgress(goal, newValue);

                        // Calculate progress for display update
                        double progressPercent = _goalManager.CalculateProgress(goal);

                        // Update UI immediately BEFORE saving
                        progressBarGoal.Value = (int)Math.Min(progressPercent, 100);
                        lblMotivation.Text = _goalManager.GetMotivationMessage(goal);

                        // Save to database
                        service.UpdateGoal(goal);

                        // Check if goal should be marked complete
                        if (_goalManager.IsGoalAchieved(goal) && !goal.IsCompleted)
                        {
                            service.CompleteGoal(goalId);

                            // Show achievement message (in addition to event handler)
                            MessageBox.Show(
                                $"Congratulations! You've achieved your goal: {goal.Description}\n\n" +
                                $"Target: {goal.TargetValue:F1}\n" +
                                $"Achieved: {goal.CurrentValue:F1}",
                                "Goal Achieved!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Show progress update confirmation
                            MessageBox.Show(
                                $"Progress updated successfully!\n\n" +
                                $"Current: {goal.CurrentValue:F1}\n" +
                                $"Target: {goal.TargetValue:F1}\n" +
                                $"Progress: {progressPercent:F0}%",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }

                        // Refresh the goals display
                        LoadGoals();

                        // Auto-reselect the goal if still active
                        if (!goal.IsCompleted)
                        {
                            SelectGoalInGrid(goalId);
                        }
                        else
                        {
                            // Goal completed - select it in completed goals grid
                            SelectCompletedGoalInGrid(goalId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating progress: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to select a goal in active goals grid after update
        private void SelectGoalInGrid(int goalId)
        {
            foreach (DataGridViewRow row in dgvActiveGoals.Rows)
            {
                if (Convert.ToInt32(row.Cells["GoalId"].Value) == goalId)
                {
                    row.Selected = true;
                    dgvActiveGoals.FirstDisplayedScrollingRowIndex = row.Index;

                    // Update progress display for this goal
                    UpdateProgressDisplay(goalId);
                    break;
                }
            }
        }

        // Helper method to select a goal in completed goals grid
        private void SelectCompletedGoalInGrid(int goalId)
        {
            foreach (DataGridViewRow row in dgvCompletedGoals.Rows)
            {
                if (Convert.ToInt32(row.Cells["GoalId"].Value) == goalId)
                {
                    row.Selected = true;
                    dgvCompletedGoals.FirstDisplayedScrollingRowIndex = row.Index;

                    // Show 100% progress for completed goal
                    progressBarGoal.Value = 100;
                    lblMotivation.Text = "Goal achieved! Congratulations!";
                    break;
                }
            }
        }

        // Helper method to update progress bar and motivation for a goal
        private void UpdateProgressDisplay(int goalId)
        {
            using (var context = new HealthTrackerContext())
            {
                var service = new GoalService(context);
                var goal = service.GetGoalById(goalId);

                if (goal != null)
                {
                    double progress = _goalManager.CalculateProgress(goal);
                    progressBarGoal.Value = (int)Math.Min(progress, 100);
                    lblMotivation.Text = _goalManager.GetMotivationMessage(goal);
                }
            }
        }

        // Deletes the selected goal
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvActiveGoals.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a goal to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this goal?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int goalId = Convert.ToInt32(dgvActiveGoals.SelectedRows[0].Cells["GoalId"].Value);

                    using (var context = new HealthTrackerContext())
                    {
                        var service = new GoalService(context);
                        service.DeleteGoal(goalId);
                    }

                    MessageBox.Show("Goal deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadGoals();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting goal: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Validates user input before creating a goal
        private bool ValidateGoalInput()
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a goal description.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }

            if (nudTargetValue.Value <= 0)
            {
                MessageBox.Show("Target value must be greater than zero.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpTargetDate.Value <= DateTime.Now)
            {
                MessageBox.Show("Target date must be in the future.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Clears all input fields
        private void ClearForm()
        {
            cboGoalType.SelectedIndex = 0;
            txtDescription.Clear();
            nudTargetValue.Value = 70;

            // Reset current value to latest weight
            double latestWeight = GetLatestUserWeight();
            nudCurrentValue.Value = (decimal)latestWeight;

            dtpTargetDate.Value = DateTime.Now.AddMonths(3);
            progressBarGoal.Value = 0;
            lblMotivation.Text = "Set your goals and track your progress!";
        }

        // Loads selected goal data into form for viewing
        // Updates progress bar and motivation message
        private void dgvActiveGoals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int goalId = Convert.ToInt32(dgvActiveGoals.Rows[e.RowIndex].Cells["GoalId"].Value);
                _selectedGoalId = goalId;

                // Update progress display for selected goal
                UpdateProgressDisplay(goalId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading goal details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Also update progress when completed goal is clicked
        private void dgvCompletedGoals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int goalId = Convert.ToInt32(dgvCompletedGoals.Rows[e.RowIndex].Cells["GoalId"].Value);

                // Show 100% progress for completed goals
                progressBarGoal.Value = 100;
                lblMotivation.Text = "Goal achieved! Congratulations!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading goal details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Closes the form and unsubscribes from events
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Unsubscribe from events to prevent memory leaks
            _goalManager.OnGoalAchieved -= GoalManager_OnGoalAchieved;
            _goalManager.OnGoalProgress -= GoalManager_OnGoalProgress;

            this.Close();
        }
    }
}