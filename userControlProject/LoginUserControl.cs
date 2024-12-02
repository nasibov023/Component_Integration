using System;
using System.Windows.Forms;
using HashingLibrary; // Ensure you have the correct namespace for the hashing library

namespace UserControlProject
{
    public class LoginUserControl : UserControl
    {
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Label resultLabel;

        public LoginUserControl()
        {
            // Username Field
            var usernameLabel = new Label { Text = "Username:", Left = 10, Top = 10, Width = 100 };
            usernameTextBox = new TextBox { Left = 120, Top = 10, Width = 200 };

            // Password Field
            var passwordLabel = new Label { Text = "Password:", Left = 10, Top = 50, Width = 100 };
            passwordTextBox = new TextBox { Left = 120, Top = 50, Width = 200, PasswordChar = '*' };

            // Login Button
            loginButton = new Button { Text = "Login", Left = 120, Top = 90, Width = 100 };
            loginButton.Click += LoginButton_Click;

            // Result Label
            resultLabel = new Label { Left = 10, Top = 130, Width = 300 };

            // Add Controls to UserControl
            Controls.Add(usernameLabel);
            Controls.Add(usernameTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(passwordTextBox);
            Controls.Add(loginButton);
            Controls.Add(resultLabel);
        }

        private void LoginButton_Click(object? sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            // Hash the password
            string hashedPassword = HashingEncryption.ComputeSHA256(password);

            // Simple validation
            if (username == "admin" && hashedPassword == HashingEncryption.ComputeSHA256("password"))
            {
                resultLabel.Text = "Login Successful!";
            }
            else
            {
                resultLabel.Text = "Invalid Username or Password.";
            }
        }
    }
}
