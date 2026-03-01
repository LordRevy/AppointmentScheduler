namespace AppointmentScheduler.Forms
{
    partial class MainForm
    {
        private const string V = "MainForm";

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
            AppointmentTable = new DataGridView();
            CustomerTable = new DataGridView();
            AddAptBtn = new Button();
            AptIdText = new TextBox();
            CustIdText = new TextBox();
            UserIdText = new TextBox();
            TitleText = new TextBox();
            UpdateBtn = new Button();
            DeleteBtn = new Button();
            CustTableId = new TextBox();
            NameText = new TextBox();
            AddressText = new TextBox();
            PhoneText = new TextBox();
            AddCustBtn = new Button();
            UpdateCustBtn = new Button();
            DeleteCustBtn = new Button();
            GenRptBtn = new Button();
            AptStartTime = new DateTimePicker();
            AptEndTime = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            AptDate = new DateTimePicker();
            TypeText = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)AppointmentTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CustomerTable).BeginInit();
            SuspendLayout();
            // 
            // AppointmentTable
            // 
            AppointmentTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AppointmentTable.Location = new Point(12, 12);
            AppointmentTable.Name = "AppointmentTable";
            AppointmentTable.Size = new Size(835, 150);
            AppointmentTable.TabIndex = 0;
            AppointmentTable.SelectionChanged += AppointmentTable_SelectionChanged;
            // 
            // CustomerTable
            // 
            CustomerTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CustomerTable.Location = new Point(12, 247);
            CustomerTable.Name = "CustomerTable";
            CustomerTable.Size = new Size(546, 150);
            CustomerTable.TabIndex = 1;
            CustomerTable.SelectionChanged += CustomerTable_SelectionChanged;
            // 
            // AddAptBtn
            // 
            AddAptBtn.Location = new Point(12, 199);
            AddAptBtn.Name = "AddAptBtn";
            AddAptBtn.Size = new Size(70, 23);
            AddAptBtn.TabIndex = 2;
            AddAptBtn.Text = "Add";
            AddAptBtn.UseVisualStyleBackColor = true;
            AddAptBtn.Click += AddAptBtn_Click;
            // 
            // AptIdText
            // 
            AptIdText.Location = new Point(12, 168);
            AptIdText.Name = "AptIdText";
            AptIdText.PlaceholderText = "Appointment ID";
            AptIdText.Size = new Size(100, 23);
            AptIdText.TabIndex = 3;
            // 
            // CustIdText
            // 
            CustIdText.Location = new Point(118, 168);
            CustIdText.Name = "CustIdText";
            CustIdText.PlaceholderText = "Customer ID";
            CustIdText.Size = new Size(106, 23);
            CustIdText.TabIndex = 4;
            // 
            // UserIdText
            // 
            UserIdText.Location = new Point(230, 168);
            UserIdText.Name = "UserIdText";
            UserIdText.PlaceholderText = "User ID";
            UserIdText.Size = new Size(100, 23);
            UserIdText.TabIndex = 5;
            // 
            // TitleText
            // 
            TitleText.Location = new Point(336, 168);
            TitleText.Name = "TitleText";
            TitleText.PlaceholderText = "Title";
            TitleText.Size = new Size(109, 23);
            TitleText.TabIndex = 6;
            // 
            // UpdateBtn
            // 
            UpdateBtn.Location = new Point(88, 199);
            UpdateBtn.Name = "UpdateBtn";
            UpdateBtn.Size = new Size(70, 23);
            UpdateBtn.TabIndex = 10;
            UpdateBtn.Text = "Update";
            UpdateBtn.UseVisualStyleBackColor = true;
            UpdateBtn.Click += UpdateBtn_Click;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Location = new Point(164, 199);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(70, 23);
            DeleteBtn.TabIndex = 11;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
            DeleteBtn.Click += DeleteBtn_Click;
            // 
            // CustTableId
            // 
            CustTableId.Location = new Point(12, 403);
            CustTableId.Name = "CustTableId";
            CustTableId.PlaceholderText = "Customer ID";
            CustTableId.Size = new Size(106, 23);
            CustTableId.TabIndex = 12;
            // 
            // NameText
            // 
            NameText.Location = new Point(124, 403);
            NameText.Name = "NameText";
            NameText.PlaceholderText = "Name";
            NameText.Size = new Size(106, 23);
            NameText.TabIndex = 13;
            // 
            // AddressText
            // 
            AddressText.Location = new Point(236, 403);
            AddressText.Name = "AddressText";
            AddressText.PlaceholderText = "Address";
            AddressText.Size = new Size(106, 23);
            AddressText.TabIndex = 14;
            // 
            // PhoneText
            // 
            PhoneText.Location = new Point(348, 403);
            PhoneText.Name = "PhoneText";
            PhoneText.PlaceholderText = "Phone";
            PhoneText.Size = new Size(106, 23);
            PhoneText.TabIndex = 15;
            // 
            // AddCustBtn
            // 
            AddCustBtn.Location = new Point(562, 247);
            AddCustBtn.Name = "AddCustBtn";
            AddCustBtn.Size = new Size(72, 23);
            AddCustBtn.TabIndex = 16;
            AddCustBtn.Text = "Add";
            AddCustBtn.UseVisualStyleBackColor = true;
            AddCustBtn.Click += AddCustBtn_Click;
            // 
            // UpdateCustBtn
            // 
            UpdateCustBtn.Location = new Point(564, 276);
            UpdateCustBtn.Name = "UpdateCustBtn";
            UpdateCustBtn.Size = new Size(70, 23);
            UpdateCustBtn.TabIndex = 17;
            UpdateCustBtn.Text = "Update";
            UpdateCustBtn.UseVisualStyleBackColor = true;
            UpdateCustBtn.Click += UpdateCustBtn_Click;
            // 
            // DeleteCustBtn
            // 
            DeleteCustBtn.Location = new Point(564, 305);
            DeleteCustBtn.Name = "DeleteCustBtn";
            DeleteCustBtn.Size = new Size(70, 23);
            DeleteCustBtn.TabIndex = 18;
            DeleteCustBtn.Text = "Delete";
            DeleteCustBtn.UseVisualStyleBackColor = true;
            DeleteCustBtn.Click += DeleteCustBtn_Click;
            // 
            // GenRptBtn
            // 
            GenRptBtn.Location = new Point(740, 415);
            GenRptBtn.Name = "GenRptBtn";
            GenRptBtn.Size = new Size(109, 23);
            GenRptBtn.TabIndex = 19;
            GenRptBtn.Text = "Generate Report";
            GenRptBtn.UseVisualStyleBackColor = true;
            GenRptBtn.Click += GenRptBtn_Click;
            // 
            // AptStartTime
            // 
            AptStartTime.Format = DateTimePickerFormat.Time;
            AptStartTime.Location = new Point(764, 168);
            AptStartTime.Name = "AptStartTime";
            AptStartTime.Size = new Size(83, 23);
            AptStartTime.TabIndex = 22;
            // 
            // AptEndTime
            // 
            AptEndTime.Format = DateTimePickerFormat.Time;
            AptEndTime.Location = new Point(764, 197);
            AptEndTime.Name = "AptEndTime";
            AptEndTime.Size = new Size(83, 23);
            AptEndTime.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(697, 174);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 24;
            label1.Text = "Start Time";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(701, 203);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 25;
            label2.Text = "End Time";
            // 
            // AptDate
            // 
            AptDate.Format = DateTimePickerFormat.Short;
            AptDate.Location = new Point(547, 197);
            AptDate.Name = "AptDate";
            AptDate.Size = new Size(121, 23);
            AptDate.TabIndex = 26;
            // 
            // TypeText
            // 
            TypeText.FormattingEnabled = true;
            TypeText.Items.AddRange(new object[] { "Scrum", "Project", "Monthly", "Cust Consult", "Executive", "Other" });
            TypeText.Location = new Point(547, 168);
            TypeText.Name = "TypeText";
            TypeText.Size = new Size(121, 23);
            TypeText.TabIndex = 27;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(509, 171);
            label3.Name = "label3";
            label3.Size = new Size(32, 15);
            label3.TabIndex = 28;
            label3.Text = "Type";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(509, 203);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 29;
            label4.Text = "Date";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(861, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(TypeText);
            Controls.Add(AptDate);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(AptEndTime);
            Controls.Add(AptStartTime);
            Controls.Add(GenRptBtn);
            Controls.Add(DeleteCustBtn);
            Controls.Add(UpdateCustBtn);
            Controls.Add(AddCustBtn);
            Controls.Add(PhoneText);
            Controls.Add(AddressText);
            Controls.Add(NameText);
            Controls.Add(CustTableId);
            Controls.Add(DeleteBtn);
            Controls.Add(UpdateBtn);
            Controls.Add(TitleText);
            Controls.Add(UserIdText);
            Controls.Add(CustIdText);
            Controls.Add(AptIdText);
            Controls.Add(AddAptBtn);
            Controls.Add(CustomerTable);
            Controls.Add(AppointmentTable);
            Name = "MainForm";
            Text = "Appointment Scheduler";
            ((System.ComponentModel.ISupportInitialize)AppointmentTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)CustomerTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView AppointmentTable;
        private DataGridView CustomerTable;
        private Button AddAptBtn;
        private TextBox AptIdText;
        private TextBox CustIdText;
        private TextBox UserIdText;
        private TextBox TitleText;
        private TextBox StartText;
        private TextBox EndText;
        private Button UpdateBtn;
        private Button DeleteBtn;
        private TextBox CustTableId;
        private TextBox NameText;
        private TextBox AddressText;
        private TextBox PhoneText;
        private Button AddCustBtn;
        private Button UpdateCustBtn;
        private Button DeleteCustBtn;
        private Button GenRptBtn;
        private DateTimePicker AptStartTime;
        private DateTimePicker AptEndTime;
        private Label label1;
        private Label label2;
        private DateTimePicker AptDate;
        private ComboBox TypeText;
        private Label label3;
        private Label label4;
    }
}