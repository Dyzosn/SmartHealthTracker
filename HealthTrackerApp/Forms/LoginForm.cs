using System;
using System.Linq;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;

namespace HealthTrackerApp.Forms
{
    // User authentication form for login and registration
    // Validates credentials against database and opens dashboard on success
    public partial class LoginForm : Form
    {
        private readonly HealthTrackerContext _context;

        public LoginForm()
        {
            InitializeComponent();
            _context = new HealthTrackerContext();

            // Set form properties for better UX
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Wire up button click events
            btnLogin.Click += btnLogin_Click;
            btnRegister.Click += btnRegister_Click;

            // Allow Enter key to submit login
            this.AcceptButton = btnLogin;
        }

        // Validate credentials and open dashboard
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validate username input
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter username", "Validation Error");
                txtUsername.Focus();
                return;
            }

            // Validate password input
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter password", "Validation Error");
                txtPassword.Focus();
                return;
            }

            try
            {
                // Check credentials using LINQ query
                var user = _context.Users
                    .FirstOrDefault(u => u.Username == txtUsername.Text &&
                                        u.Password == txtPassword.Text);

                if (user != null)
                {
                    // Login successful, show welcome message
                    MessageBox.Show($"Welcome back, {user.Username}!", "Login Successful");

                    // Open dashboard with authenticated user
                    // Pass reference to this LoginForm so dashboard can return here on logout
                    DashboardForm dashboard = new DashboardForm(user, this);
                    dashboard.Show();

                    // Hide login form but keep it alive in memory for later use
                    this.Hide();
                }
                else
                {
                    // Invalid credentials, show error and clear password
                    MessageBox.Show("Invalid username or password", "Login Failed");
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error");
            }
        }

        // Open registration form for new user
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Validate username before opening registration
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter username", "Validation Error");
                txtUsername.Focus();
                return;
            }

            // Validate password before opening registration
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter password", "Validation Error");
                txtPassword.Focus();
                return;
            }

            try
            {
                // Check if username already exists in database
                var existingUser = _context.Users
                    .FirstOrDefault(u => u.Username == txtUsername.Text);

                if (existingUser != null)
                {
                    MessageBox.Show("Username already exists. Please choose another.",
                        "Registration Failed");
                    return;
                }

                // Open registration form to collect full user details
                RegistrationForm registrationForm = new RegistrationForm(
                    txtUsername.Text,
                    txtPassword.Text
                );

                if (registrationForm.ShowDialog() == DialogResult.OK)
                {
                    // Registration completed successfully, clear inputs
                    txtPassword.Clear();
                    txtUsername.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration error: {ex.Message}", "Error");
            }
        }

        // Show login form again after user logs out from dashboard
        public void ShowLoginAgain()
        {
            // Clear password field for security
            txtPassword.Clear();
            txtUsername.Clear();
            txtUsername.Focus();

            // Make form visible again
            this.Show();
        }

        // Clean up database context when form closes
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}