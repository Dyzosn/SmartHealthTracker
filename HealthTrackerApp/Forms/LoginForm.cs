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

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            btnLogin.Click += btnLogin_Click;
            btnRegister.Click += btnRegister_Click;
            this.AcceptButton = btnLogin;
        }

        // Validate credentials and open dashboard
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter username", "Validation Error");
                txtUsername.Focus();
                return;
            }

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
                    MessageBox.Show($"Welcome back, {user.Username}!", "Login Successful");

                    // Open dashboard with authenticated user
                    DashboardForm dashboard = new DashboardForm(user);
                    dashboard.FormClosed += (s, args) => this.Close();
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
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
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter username", "Validation Error");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter password", "Validation Error");
                txtPassword.Focus();
                return;
            }

            try
            {
                // Check if username already exists
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
                    // Registration completed successfully
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}