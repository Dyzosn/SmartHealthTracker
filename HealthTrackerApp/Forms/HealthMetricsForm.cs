using System;
using System.Linq;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Services;
using HealthTrackerApp.BusinessLogic.HealthMetrics;

namespace HealthTrackerApp.Forms
{
    // Health metrics form for logging vital measurements
    // Allows users to track weight, blood pressure, heart rate, and blood sugar
    // Displays statistics and trends using DataGridView (no charts - .NET 9.0 compatible)
    public partial class HealthMetricsForm : Form
    {
        private readonly HealthTrackerContext _context;
        private readonly HealthMetricService _metricService;
        private readonly User _currentUser;
        private string _selectedMetricType = "Weight"; // Default metric type

        public HealthMetricsForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _context = new HealthTrackerContext();
            _metricService = new HealthMetricService(_context);

            // Setup form when it loads
            this.Load += HealthMetricsForm_Load;
        }

        private void HealthMetricsForm_Load(object sender, EventArgs e)
        {
            // Initialise metric type combo box
            cboMetricType.Items.AddRange(new string[] { "Weight", "BloodPressure", "HeartRate", "BloodSugar" });
            cboMetricType.SelectedIndex = 0;
            cboMetricType.SelectedIndexChanged += cboMetricType_SelectedIndexChanged;

            // Set default date to today
            dtpRecordedDate.Value = DateTime.Now;

            // Load existing metrics into data grid
            LoadHealthMetrics();

            // Show appropriate input controls for selected metric type
            UpdateInputControls();

            // Load trend statistics for weight by default
            LoadMetricStatistics("Weight");
        }

        // Update visibility of input controls based on selected metric type
        private void UpdateInputControls()
        {
            // Hide all metric-specific controls
            lblWeight.Visible = txtWeight.Visible = false;
            lblSystolic.Visible = txtSystolic.Visible = false;
            lblDiastolic.Visible = txtDiastolic.Visible = false;
            lblHeartRate.Visible = txtHeartRate.Visible = false;
            lblBloodSugar.Visible = txtBloodSugar.Visible = false;

            // Show relevant controls based on selected metric type
            switch (_selectedMetricType)
            {
                case "Weight":
                    lblWeight.Visible = txtWeight.Visible = true;
                    lblWeight.Text = "Weight (kg):";
                    break;

                case "BloodPressure":
                    lblSystolic.Visible = txtSystolic.Visible = true;
                    lblDiastolic.Visible = txtDiastolic.Visible = true;
                    lblSystolic.Text = "Systolic (mmHg):";
                    lblDiastolic.Text = "Diastolic (mmHg):";
                    break;

                case "HeartRate":
                    lblHeartRate.Visible = txtHeartRate.Visible = true;
                    lblHeartRate.Text = "Heart Rate (bpm):";
                    break;

                case "BloodSugar":
                    lblBloodSugar.Visible = txtBloodSugar.Visible = true;
                    lblBloodSugar.Text = "Blood Sugar (mmol/L):";
                    break;
            }
        }

        // Handle metric type selection change
        private void cboMetricType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedMetricType = cboMetricType.SelectedItem.ToString();
            UpdateInputControls();
            LoadMetricStatistics(_selectedMetricType);
        }

        // Load health metrics into main DataGridView
        private void LoadHealthMetrics()
        {
            var metrics = _metricService.GetHealthMetricsByUserId(_currentUser.UserId);

            // Clear existing rows
            dgvHealthMetrics.Rows.Clear();

            // Add columns if not already added
            if (dgvHealthMetrics.Columns.Count == 0)
            {
                dgvHealthMetrics.Columns.Add("Date", "Date");
                dgvHealthMetrics.Columns.Add("Type", "Type");
                dgvHealthMetrics.Columns.Add("Value", "Value");
                dgvHealthMetrics.Columns.Add("Notes", "Notes");
            }

            // Add metric rows
            foreach (var metric in metrics)
            {
                string displayValue = GetMetricDisplayValue(metric);
                dgvHealthMetrics.Rows.Add(
                    metric.RecordedDate.ToString("dd/MM/yyyy"),
                    metric.MetricType,
                    displayValue,
                    metric.Notes ?? ""
                );
            }

            // Update statistics label
            UpdateOverallStatistics();
        }

        // Get formatted display value for metric using polymorphism
        private string GetMetricDisplayValue(HealthMetricRecord metric)
        {
            // Use polymorphism to get display string
            var polymorphicMetric = _metricService.ConvertToPolymorphicMetric(metric);
            if (polymorphicMetric != null)
            {
                return polymorphicMetric.Display();
            }

            return "N/A";
        }

        // Load metric trend statistics into second DataGridView
        private void LoadMetricStatistics(string metricType)
        {
            // Get metrics for last 30 days for selected type
            var metrics = _metricService.GetHealthMetricsByType(_currentUser.UserId, metricType)
                .OrderBy(m => m.RecordedDate)
                .Take(30)
                .ToList();

            // Clear existing rows
            dgvTrendData.Rows.Clear();

            // Add columns if not already added
            if (dgvTrendData.Columns.Count == 0)
            {
                dgvTrendData.Columns.Add("Date", "Date");
                dgvTrendData.Columns.Add("Value", "Value");
                dgvTrendData.Columns.Add("Category", "Category/Status");
            }

            // Add trend data rows with category/status using polymorphism
            foreach (var metric in metrics)
            {
                var polymorphicMetric = _metricService.ConvertToPolymorphicMetric(metric);
                if (polymorphicMetric != null)
                {
                    string value = polymorphicMetric.Display();
                    string category = GetMetricCategory(polymorphicMetric);

                    dgvTrendData.Rows.Add(
                        metric.RecordedDate.ToString("dd/MM/yyyy"),
                        value,
                        category
                    );
                }
            }

            // Calculate and display statistics for this metric type
            CalculateAndDisplayStatistics(metrics, metricType);
        }

        // Get category/status for metric using polymorphic methods
        private string GetMetricCategory(HealthMetric metric)
        {
            // Use polymorphism to determine category
            if (metric is WeightMetric weightMetric)
            {
                // Calculate BMI and return category
                double bmi = weightMetric.CalculateBMI(_currentUser.Height);
                return weightMetric.GetBMICategory(bmi);
            }
            else if (metric is BloodPressureMetric bpMetric)
            {
                return bpMetric.GetBloodPressureCategory();
            }
            else if (metric is HeartRateMetric hrMetric)
            {
                return hrMetric.GetRestingHeartRateCategory();
            }
            else if (metric is BloodSugarMetric bsMetric)
            {
                return bsMetric.GetBloodSugarCategory();
            }

            return "N/A";
        }

        // Calculate and display statistics for selected metric type
        private void CalculateAndDisplayStatistics(System.Collections.Generic.List<HealthMetricRecord> metrics, string metricType)
        {
            if (metrics.Count == 0)
            {
                lblStatistics.Text = $"No {metricType} data available";
                return;
            }

            // Calculate statistics based on metric type
            string stats = "";

            switch (metricType)
            {
                case "Weight":
                    var weights = metrics.Where(m => m.WeightKg.HasValue).Select(m => m.WeightKg.Value).ToList();
                    if (weights.Any())
                    {
                        double avgWeight = weights.Average();
                        double minWeight = weights.Min();
                        double maxWeight = weights.Max();
                        double latestWeight = weights.Last();
                        double firstWeight = weights.First();
                        double change = latestWeight - firstWeight;
                        string trend = change > 0 ? "▲ Increasing" : change < 0 ? "▼ Decreasing" : "→ Stable";

                        stats = $"Weight Statistics (Last 30 days):\n" +
                                $"Average: {avgWeight:F1} kg\n" +
                                $"Minimum: {minWeight:F1} kg\n" +
                                $"Maximum: {maxWeight:F1} kg\n" +
                                $"Latest: {latestWeight:F1} kg\n" +
                                $"Change: {change:+0.0;-0.0;0.0} kg\n" +
                                $"Trend: {trend}";
                    }
                    break;

                case "BloodPressure":
                    var systolicValues = metrics.Where(m => m.Systolic.HasValue).Select(m => m.Systolic.Value).ToList();
                    var diastolicValues = metrics.Where(m => m.Diastolic.HasValue).Select(m => m.Diastolic.Value).ToList();
                    if (systolicValues.Any() && diastolicValues.Any())
                    {
                        double avgSystolic = systolicValues.Average();
                        double avgDiastolic = diastolicValues.Average();
                        int minSystolic = systolicValues.Min();
                        int maxSystolic = systolicValues.Max();
                        int latestSystolic = systolicValues.Last();
                        int latestDiastolic = diastolicValues.Last();

                        stats = $"Blood Pressure Statistics (Last 30 days):\n" +
                                $"Average: {avgSystolic:F0}/{avgDiastolic:F0} mmHg\n" +
                                $"Systolic Range: {minSystolic}-{maxSystolic} mmHg\n" +
                                $"Latest: {latestSystolic}/{latestDiastolic} mmHg\n" +
                                $"Readings: {systolicValues.Count}";
                    }
                    break;

                case "HeartRate":
                    var heartRates = metrics.Where(m => m.HeartRateBpm.HasValue).Select(m => m.HeartRateBpm.Value).ToList();
                    if (heartRates.Any())
                    {
                        double avgHR = heartRates.Average();
                        int minHR = heartRates.Min();
                        int maxHR = heartRates.Max();
                        int latestHR = heartRates.Last();

                        stats = $"Heart Rate Statistics (Last 30 days):\n" +
                                $"Average: {avgHR:F0} bpm\n" +
                                $"Minimum: {minHR} bpm\n" +
                                $"Maximum: {maxHR} bpm\n" +
                                $"Latest: {latestHR} bpm\n" +
                                $"Readings: {heartRates.Count}";
                    }
                    break;

                case "BloodSugar":
                    var bloodSugars = metrics.Where(m => m.BloodSugarMmol.HasValue).Select(m => m.BloodSugarMmol.Value).ToList();
                    if (bloodSugars.Any())
                    {
                        double avgBS = bloodSugars.Average();
                        double minBS = bloodSugars.Min();
                        double maxBS = bloodSugars.Max();
                        double latestBS = bloodSugars.Last();

                        stats = $"Blood Sugar Statistics (Last 30 days):\n" +
                                $"Average: {avgBS:F1} mmol/L\n" +
                                $"Minimum: {minBS:F1} mmol/L\n" +
                                $"Maximum: {maxBS:F1} mmol/L\n" +
                                $"Latest: {latestBS:F1} mmol/L\n" +
                                $"Readings: {bloodSugars.Count}";
                    }
                    break;
            }

            lblStatistics.Text = stats;
        }

        // Update overall statistics labels
        private void UpdateOverallStatistics()
        {
            int totalRecords = _context.HealthMetrics
                .Count(h => h.UserId == _currentUser.UserId);

            lblTotalRecords.Text = $"Total Records: {totalRecords}";

            // Show latest metric for each type
            var latestWeight = _metricService.GetLatestHealthMetric(_currentUser.UserId, "Weight");
            if (latestWeight != null && latestWeight.WeightKg.HasValue)
            {
                lblLatestWeight.Text = $"Latest Weight: {latestWeight.WeightKg:F1} kg";
            }
            else
            {
                lblLatestWeight.Text = "Latest Weight: N/A";
            }

            var latestBP = _metricService.GetLatestHealthMetric(_currentUser.UserId, "BloodPressure");
            if (latestBP != null && latestBP.Systolic.HasValue && latestBP.Diastolic.HasValue)
            {
                lblLatestBP.Text = $"Latest BP: {latestBP.Systolic}/{latestBP.Diastolic} mmHg";
            }
            else
            {
                lblLatestBP.Text = "Latest BP: N/A";
            }
        }

        // Save new health metric
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Create new metric record
                HealthMetricRecord metric = new HealthMetricRecord
                {
                    UserId = _currentUser.UserId,
                    RecordedDate = dtpRecordedDate.Value,
                    MetricType = _selectedMetricType,
                    Notes = txtNotes.Text.Trim()
                };

                // Populate metric-specific fields
                switch (_selectedMetricType)
                {
                    case "Weight":
                        if (!double.TryParse(txtWeight.Text, out double weight))
                        {
                            MessageBox.Show("Please enter a valid weight value.", "Invalid Input");
                            return;
                        }
                        metric.WeightKg = weight;
                        break;

                    case "BloodPressure":
                        if (!int.TryParse(txtSystolic.Text, out int systolic) ||
                            !int.TryParse(txtDiastolic.Text, out int diastolic))
                        {
                            MessageBox.Show("Please enter valid blood pressure values.", "Invalid Input");
                            return;
                        }
                        metric.Systolic = systolic;
                        metric.Diastolic = diastolic;
                        break;

                    case "HeartRate":
                        if (!int.TryParse(txtHeartRate.Text, out int heartRate))
                        {
                            MessageBox.Show("Please enter a valid heart rate value.", "Invalid Input");
                            return;
                        }
                        metric.HeartRateBpm = heartRate;
                        break;

                    case "BloodSugar":
                        if (!double.TryParse(txtBloodSugar.Text, out double bloodSugar))
                        {
                            MessageBox.Show("Please enter a valid blood sugar value.", "Invalid Input");
                            return;
                        }
                        metric.BloodSugarMmol = bloodSugar;
                        break;
                }

                // Validate using polymorphism
                var polymorphicMetric = _metricService.ConvertToPolymorphicMetric(metric);
                if (polymorphicMetric != null && !polymorphicMetric.Validate())
                {
                    MessageBox.Show("The entered value is outside the acceptable range. Please check your input.",
                        "Validation Error");
                    return;
                }

                // Save to database
                _metricService.AddHealthMetric(metric);

                MessageBox.Show("Health metric saved successfully!", "Success");

                // Refresh display
                LoadHealthMetrics();
                LoadMetricStatistics(_selectedMetricType);
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving health metric: {ex.Message}", "Error");
            }
        }

        // Clear input fields after save
        private void ClearInputFields()
        {
            txtWeight.Clear();
            txtSystolic.Clear();
            txtDiastolic.Clear();
            txtHeartRate.Clear();
            txtBloodSugar.Clear();
            txtNotes.Clear();
            dtpRecordedDate.Value = DateTime.Now;
        }

        // Clean up resources when form closes
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}