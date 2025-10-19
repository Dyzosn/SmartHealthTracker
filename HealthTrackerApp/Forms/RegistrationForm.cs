using System;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;

namespace HealthTrackerApp.Forms
{
    // Complete registration form for new user account creation
    // Collects all required user profile information
    public partial class RegistrationForm : Form
    {
        private readonly HealthTrackerContext _context;
        private readonly string _username;
        private readonly string _password;

        public RegistrationForm(string username, string password)
        {
            InitializeComponent();
            _context = new HealthTrackerContext();
            _username = username;
            _password = password;

            this.Load += RegistrationForm_Load;
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            // Populate gender combo box
            cboGender.Items.AddRange(new string[] { "Male", "Female", "Other" });
            cboGender.SelectedIndex = 0;

            // Set default date of birth to 25 years ago
            dtpDateOfBirth.Value = DateTime.Today.AddYears(-25);
            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-10);

            btnComplete.Click += btnComplete_Click;
            btnCancel.Click += btnCancel_Click;
        }

        // Complete registration with full user details
        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter email address", "Validation Error");
                return;
            }

            if (!double.TryParse(txtHeight.Text, out double height) || height < 0.5 || height > 2.5)
            {
                MessageBox.Show("Please enter valid height between 0.5 and 2.5 metres", "Validation Error");
                return;
            }

            try
            {
                // Create new user with complete information
                var newUser = new User
                {
                    Username = _username,
                    Password = _password,
                    Email = txtEmail.Text.Trim(),
                    DateOfBirth = dtpDateOfBirth.Value,
                    Height = height,
                    Gender = cboGender.SelectedItem.ToString(),
                    CreatedDate = DateTime.Now
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                MessageBox.Show("Registration completed successfully!\nYou can now login.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration error: {ex.Message}", "Error");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}