namespace AppointmentScheduler
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Username = new TextBox();
            Password = new TextBox();
            LoginButton = new Button();
            labelUsername = new Label();
            labelPassword = new Label();
            SuspendLayout();
            // 
            // Username
            // 
            Username.Location = new Point(176, 68);
            Username.Name = "Username";
            Username.Size = new Size(192, 23);
            Username.TabIndex = 0;
            // 
            // Password
            // 
            Password.Location = new Point(176, 113);
            Password.Name = "Password";
            Password.Size = new Size(192, 23);
            Password.TabIndex = 1;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(198, 166);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(75, 23);
            LoginButton.TabIndex = 2;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Location = new Point(90, 71);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(60, 15);
            labelUsername.TabIndex = 3;
            labelUsername.Text = "Username";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(93, 116);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(57, 15);
            labelPassword.TabIndex = 4;
            labelPassword.Text = "Password";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(465, 201);
            Controls.Add(labelPassword);
            Controls.Add(labelUsername);
            Controls.Add(LoginButton);
            Controls.Add(Password);
            Controls.Add(Username);
            Name = "LoginForm";
            Text = "Form1";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Username;
        private TextBox Password;
        private Button LoginButton;
        private Label labelUsername;
        private Label labelPassword;
    }
}
