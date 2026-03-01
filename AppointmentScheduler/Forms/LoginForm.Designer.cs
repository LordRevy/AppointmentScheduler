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
            LatinBtn = new RadioButton();
            EnglishBtn = new RadioButton();
            Language = new Label();
            SuspendLayout();
            // 
            // Username
            // 
            Username.Location = new Point(171, 35);
            Username.Name = "Username";
            Username.Size = new Size(192, 23);
            Username.TabIndex = 0;
            // 
            // Password
            // 
            Password.Location = new Point(171, 80);
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
            labelUsername.Location = new Point(85, 38);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(60, 15);
            labelUsername.TabIndex = 3;
            labelUsername.Text = "Username";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(88, 83);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(57, 15);
            labelPassword.TabIndex = 4;
            labelPassword.Text = "Password";
            // 
            // LatinBtn
            // 
            LatinBtn.AutoSize = true;
            LatinBtn.Location = new Point(186, 129);
            LatinBtn.Name = "LatinBtn";
            LatinBtn.Size = new Size(51, 19);
            LatinBtn.TabIndex = 5;
            LatinBtn.TabStop = true;
            LatinBtn.Text = "Latin";
            LatinBtn.UseVisualStyleBackColor = true;
            // 
            // EnglishBtn
            // 
            EnglishBtn.AutoSize = true;
            EnglishBtn.Checked = true;
            EnglishBtn.Location = new Point(278, 129);
            EnglishBtn.Name = "EnglishBtn";
            EnglishBtn.Size = new Size(63, 19);
            EnglishBtn.TabIndex = 6;
            EnglishBtn.TabStop = true;
            EnglishBtn.Text = "English";
            EnglishBtn.UseVisualStyleBackColor = true;
            // 
            // Language
            // 
            Language.AutoSize = true;
            Language.Location = new Point(88, 129);
            Language.Name = "Language";
            Language.Size = new Size(59, 15);
            Language.TabIndex = 7;
            Language.Text = "Language";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(465, 201);
            Controls.Add(Language);
            Controls.Add(EnglishBtn);
            Controls.Add(LatinBtn);
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
        private RadioButton LatinBtn;
        private RadioButton EnglishBtn;
        private Label Language;
    }
}
