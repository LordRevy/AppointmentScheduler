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
            label1 = new Label();
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // Username
            // 
            Username.Location = new Point(117, 35);
            Username.Name = "Username";
            Username.Size = new Size(192, 23);
            Username.TabIndex = 0;
            // 
            // Password
            // 
            Password.Location = new Point(117, 80);
            Password.Name = "Password";
            Password.Size = new Size(192, 23);
            Password.TabIndex = 1;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(219, 166);
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
            labelUsername.Location = new Point(31, 38);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(60, 15);
            labelUsername.TabIndex = 3;
            labelUsername.Text = "Username";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(34, 83);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(57, 15);
            labelPassword.TabIndex = 4;
            labelPassword.Text = "Password";
            // 
            // LatinBtn
            // 
            LatinBtn.AutoSize = true;
            LatinBtn.Location = new Point(132, 129);
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
            EnglishBtn.Location = new Point(224, 129);
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
            Language.Location = new Point(34, 129);
            Language.Name = "Language";
            Language.Size = new Size(59, 15);
            Language.TabIndex = 7;
            Language.Text = "Language";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(361, 38);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 8;
            label1.Text = "Timezone";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Mountain", "Eastern", "Pacific", "Central European" });
            comboBox1.Location = new Point(332, 56);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 9;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(465, 201);
            Controls.Add(comboBox1);
            Controls.Add(label1);
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
        private Label label1;
        private ComboBox comboBox1;
    }
}
