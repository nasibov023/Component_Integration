using System;
using System.Windows.Forms;

namespace UserControlProject
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create a Form
            Form mainForm = new Form
            {
                Text = "Login Example",
                Width = 400,
                Height = 300
            };

            // Add User Control
            LoginUserControl loginControl = new LoginUserControl
            {
                Dock = DockStyle.Fill
            };
            mainForm.Controls.Add(loginControl);

            Application.Run(mainForm);
        }
    }
}
