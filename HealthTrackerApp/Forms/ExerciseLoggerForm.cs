using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Models.Enums;
using HealthTrackerApp.Services;

namespace HealthTrackerApp.Forms
{
    // Form for logging and tracking exercise activities
    // Calculates calories burned automatically using user's weight and exercise intensity
    public partial class ExerciseLoggerForm : Form
    {
        private readonly ExerciseService _exerciseService;
        private readonly User _currentUser;
        private int? _selectedExerciseId;

        public ExerciseLoggerForm(User user)
        {
            InitializeComponent();
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));

            using (var context = new HealthTrackerContext())
            {
                _exerciseService = new ExerciseService(context);
            }

            InitialiseFormControls();
            LoadExercises();
            UpdateTodaySummary();
        }

        // Sets up dropdown values and date picker defaults
        private void InitialiseFormControls()
        {
            // Populate exercise category dropdown
            cboExerciseCategory.DataSource = Enum.GetValues(typeof(ExerciseCategory));
            cboExerciseCategory.SelectedIndex = 0;

            // Populate intensity level dropdown
            cboIntensity.DataSource = Enum.GetValues(typeof(IntensityLevel));
            cboIntensity.SelectedIndex = 1; // Default to Moderate

            // Set date picker to today - use .Today to avoid time component issues
            dtpExerciseDate.MaxDate = DateTime.Today; // Can't log future exercises
            dtpExerciseDate.Value = DateTime.Today;

            // Configure numeric controls
            nudDuration.Minimum = 1;
            nudDuration.Maximum = 480; // 8 hours max
            nudDuration.Value = 30; // Default 30 minutes
        }

        // Loads all exercises for the current user into DataGridView
        private void LoadExercises()
        {
            using (var context = new HealthTrackerContext())
            {
                var service = new ExerciseService(context);
                var exercises = service.GetUserExercises(_currentUser.UserId);

                dgvExercises.DataSource = exercises.Select(e => new
                {
                    e.ExerciseId,
                    Date = e.ExerciseDate.ToString("dd/MM/yyyy"),
                    Exercise = e.ExerciseName,
                    Category = e.Category.ToString(),
                    Duration = $"{e.DurationMinutes} min",
                    Intensity = e.Intensity.ToString(),
                    Calories = $"{e.CaloriesBurned:F0} kcal",
                    e.Notes
                }).ToList();

                FormatExerciseDataGridView();
            }
        }

        // Formats the exercise history DataGridView columns
        private void FormatExerciseDataGridView()
        {
            if (dgvExercises.Columns.Count == 0) return;

            dgvExercises.Columns["ExerciseId"].Visible = false;
            dgvExercises.Columns["Date"].Width = 100;
            dgvExercises.Columns["Exercise"].Width = 150;
            dgvExercises.Columns["Category"].Width = 100;
            dgvExercises.Columns["Duration"].Width = 80;
            dgvExercises.Columns["Intensity"].Width = 100;
            dgvExercises.Columns["Calories"].Width = 100;
            dgvExercises.Columns["Notes"].Width = 200;

            dgvExercises.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvExercises.AllowUserToAddRows = false;
            dgvExercises.ReadOnly = true;
            dgvExercises.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Updates the today's calories burned summary label
        private void UpdateTodaySummary()
        {
            using (var context = new HealthTrackerContext())
            {
                var service = new ExerciseService(context);
                double todayBurned = service.GetTodaysCaloriesBurned(_currentUser.UserId);
                var todayExercises = service.GetTodaysExercises(_currentUser.UserId);

                lblTodayBurned.Text = $"Today's Total: {todayBurned:F0} kcal burned ({todayExercises.Count} workouts)";
            }
        }

        // Gets user's current weight from latest health metric record
        // Returns default 70kg if no weight records found
        private double GetUserWeight()
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

        // Saves a new exercise or updates existing one
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateExerciseInput())
                return;

            try
            {
                using (var context = new HealthTrackerContext())
                {
                    var service = new ExerciseService(context);

                    if (_selectedExerciseId.HasValue)
                    {
                        // Update existing exercise
                        var exercise = service.GetExerciseById(_selectedExerciseId.Value);
                        if (exercise != null)
                        {
                            exercise.ExerciseDate = dtpExerciseDate.Value.Date; // Use date only
                            exercise.ExerciseName = txtExerciseName.Text.Trim();
                            exercise.Category = (ExerciseCategory)cboExerciseCategory.SelectedItem;
                            exercise.DurationMinutes = (int)nudDuration.Value;
                            exercise.Intensity = (IntensityLevel)cboIntensity.SelectedItem;
                            exercise.Notes = txtNotes.Text.Trim();

                            service.UpdateExercise(exercise);
                            MessageBox.Show("Exercise updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Create new exercise - get user's current weight from health metrics
                        double userWeight = GetUserWeight();

                        service.CreateExercise(
                            _currentUser.UserId,
                            dtpExerciseDate.Value.Date, // Use date only, not time
                            txtExerciseName.Text.Trim(),
                            (ExerciseCategory)cboExerciseCategory.SelectedItem,
                            (int)nudDuration.Value,
                            (IntensityLevel)cboIntensity.SelectedItem,
                            userWeight,
                            txtNotes.Text.Trim()
                        );

                        MessageBox.Show("Exercise logged successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                ClearForm();
                LoadExercises();
                UpdateTodaySummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving exercise: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Validates user input before saving
        private bool ValidateExerciseInput()
        {
            if (string.IsNullOrWhiteSpace(txtExerciseName.Text))
            {
                MessageBox.Show("Please enter an exercise name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtExerciseName.Focus();
                return false;
            }

            if (nudDuration.Value <= 0)
            {
                MessageBox.Show("Duration must be greater than zero.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudDuration.Focus();
                return false;
            }

            if (dtpExerciseDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Cannot log exercises in the future.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Clears all input fields and resets form
        private void ClearForm()
        {
            txtExerciseName.Clear();
            cboExerciseCategory.SelectedIndex = 0;
            nudDuration.Value = 30;
            cboIntensity.SelectedIndex = 1;
            dtpExerciseDate.Value = DateTime.Today; // Use .Today to avoid time issues
            txtNotes.Clear();
            _selectedExerciseId = null;
            btnSave.Text = "Save Exercise";
        }

        // Deletes the selected exercise from database
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExercises.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an exercise to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this exercise?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int exerciseId = Convert.ToInt32(dgvExercises.SelectedRows[0].Cells["ExerciseId"].Value);

                    using (var context = new HealthTrackerContext())
                    {
                        var service = new ExerciseService(context);
                        service.DeleteExercise(exerciseId);
                    }

                    MessageBox.Show("Exercise deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadExercises();
                    UpdateTodaySummary();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting exercise: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Refreshes the exercise list
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadExercises();
            UpdateTodaySummary();
            MessageBox.Show("Exercise list refreshed!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Loads selected exercise data into form for editing
        private void dgvExercises_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int exerciseId = Convert.ToInt32(dgvExercises.Rows[e.RowIndex].Cells["ExerciseId"].Value);

                using (var context = new HealthTrackerContext())
                {
                    var service = new ExerciseService(context);
                    var exercise = service.GetExerciseById(exerciseId);

                    if (exercise != null)
                    {
                        _selectedExerciseId = exercise.ExerciseId;
                        txtExerciseName.Text = exercise.ExerciseName;
                        cboExerciseCategory.SelectedItem = exercise.Category;
                        nudDuration.Value = exercise.DurationMinutes;
                        cboIntensity.SelectedItem = exercise.Intensity;
                        txtNotes.Text = exercise.Notes ?? string.Empty;

                        // Temporarily remove MaxDate constraint to allow loading past exercises
                        // DateTimePicker max is 12/31/9998, not DateTime.MaxValue
                        dtpExerciseDate.MaxDate = new DateTime(9998, 12, 31);
                        dtpExerciseDate.Value = exercise.ExerciseDate.Date; // Use date only, not time
                        dtpExerciseDate.MaxDate = DateTime.Today; // Restore constraint using .Today

                        btnSave.Text = "Update Exercise";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading exercise: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Closes the form
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}