namespace AppointmentScheduler.Forms
{
    partial class Reports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GetRpts = new Button();
            Close = new Button();
            AptByMonth = new CheckBox();
            UsrSchedules = new CheckBox();
            CustSchedule = new CheckBox();
            CustIdTxt = new TextBox();
            SuspendLayout();
            // 
            // GetRpts
            // 
            GetRpts.Location = new Point(213, 208);
            GetRpts.Name = "GetRpts";
            GetRpts.Size = new Size(90, 23);
            GetRpts.TabIndex = 0;
            GetRpts.Text = "Get Reports";
            GetRpts.UseVisualStyleBackColor = true;
            GetRpts.Click += GetRpts_Click;
            // 
            // Close
            // 
            Close.Location = new Point(75, 208);
            Close.Name = "Close";
            Close.Size = new Size(75, 23);
            Close.TabIndex = 1;
            Close.Text = "Close";
            Close.UseVisualStyleBackColor = true;
            // 
            // AptByMonth
            // 
            AptByMonth.AutoSize = true;
            AptByMonth.Location = new Point(12, 28);
            AptByMonth.Name = "AptByMonth";
            AptByMonth.Size = new Size(157, 19);
            AptByMonth.TabIndex = 5;
            AptByMonth.Text = "Appointments By Month";
            AptByMonth.UseVisualStyleBackColor = true;
            // 
            // UsrSchedules
            // 
            UsrSchedules.AutoSize = true;
            UsrSchedules.Location = new Point(12, 74);
            UsrSchedules.Name = "UsrSchedules";
            UsrSchedules.Size = new Size(105, 19);
            UsrSchedules.TabIndex = 6;
            UsrSchedules.Text = "User Schedules";
            UsrSchedules.UseVisualStyleBackColor = true;
            // 
            // CustSchedule
            // 
            CustSchedule.AutoSize = true;
            CustSchedule.Location = new Point(12, 117);
            CustSchedule.Name = "CustSchedule";
            CustSchedule.Size = new Size(134, 19);
            CustSchedule.TabIndex = 7;
            CustSchedule.Text = "Customers Schedule";
            CustSchedule.UseVisualStyleBackColor = true;
            CustSchedule.CheckedChanged += CustSchedule_CheckedChanged;
            // 
            // CustIdTxt
            // 
            CustIdTxt.Location = new Point(213, 115);
            CustIdTxt.Name = "CustIdTxt";
            CustIdTxt.PlaceholderText = "Customer ID";
            CustIdTxt.Size = new Size(90, 23);
            CustIdTxt.TabIndex = 9;
            // 
            // Reports
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(315, 243);
            Controls.Add(CustIdTxt);
            Controls.Add(CustSchedule);
            Controls.Add(UsrSchedules);
            Controls.Add(AptByMonth);
            Controls.Add(Close);
            Controls.Add(GetRpts);
            Name = "Reports";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button GetRpts;
        private Button Close;
        private CheckBox AptByMonth;
        private CheckBox UsrSchedules;
        private CheckBox CustSchedule;
        private TextBox CustIdTxt;
    }
}