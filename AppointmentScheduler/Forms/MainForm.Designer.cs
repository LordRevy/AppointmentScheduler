namespace AppointmentScheduler.Forms
{
    partial class MainForm
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
            Appointments = new DataGridView();
            AppointmentID = new DataGridViewTextBoxColumn();
            CustomerID = new DataGridViewTextBoxColumn();
            UserID = new DataGridViewTextBoxColumn();
            Title = new DataGridViewTextBoxColumn();
            Type = new DataGridViewTextBoxColumn();
            Start = new DataGridViewTextBoxColumn();
            End = new DataGridViewTextBoxColumn();
            CustomerTable = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Name = new DataGridViewTextBoxColumn();
            Address = new DataGridViewTextBoxColumn();
            Phone = new DataGridViewTextBoxColumn();
            AddAptBtn = new Button();
            AptIdText = new TextBox();
            CustIdText = new TextBox();
            UserIdText = new TextBox();
            TitleText = new TextBox();
            TypeText = new TextBox();
            StartText = new TextBox();
            EndText = new TextBox();
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
            ((System.ComponentModel.ISupportInitialize)Appointments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CustomerTable).BeginInit();
            SuspendLayout();
            // 
            // Appointments
            // 
            Appointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Appointments.Columns.AddRange(new DataGridViewColumn[] { AppointmentID, CustomerID, UserID, Title, Type, Start, End });
            Appointments.Location = new Point(12, 12);
            Appointments.Name = "Appointments";
            Appointments.Size = new Size(776, 150);
            Appointments.TabIndex = 0;
            // 
            // AppointmentID
            // 
            AppointmentID.HeaderText = "AppointmentID";
            AppointmentID.Name = "AppointmentID";
            // 
            // CustomerID
            // 
            CustomerID.HeaderText = "CustomerID";
            CustomerID.Name = "CustomerID";
            // 
            // UserID
            // 
            UserID.HeaderText = "UserID";
            UserID.Name = "UserID";
            // 
            // Title
            // 
            Title.HeaderText = "Title";
            Title.Name = "Title";
            // 
            // Type
            // 
            Type.HeaderText = "Type";
            Type.Name = "Type";
            // 
            // Start
            // 
            Start.HeaderText = "Start";
            Start.Name = "Start";
            // 
            // End
            // 
            End.HeaderText = "End";
            End.Name = "End";
            // 
            // CustomerTable
            // 
            CustomerTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CustomerTable.Columns.AddRange(new DataGridViewColumn[] { ID, Name, Address, Phone });
            CustomerTable.Location = new Point(12, 247);
            CustomerTable.Name = "CustomerTable";
            CustomerTable.Size = new Size(444, 150);
            CustomerTable.TabIndex = 1;
            // 
            // ID
            // 
            ID.HeaderText = "CustomerID";
            ID.Name = "ID";
            // 
            // Name
            // 
            Name.HeaderText = "Name";
            Name.Name = "Name";
            // 
            // Address
            // 
            Address.HeaderText = "Address";
            Address.Name = "Address";
            // 
            // Phone
            // 
            Phone.HeaderText = "Phone";
            Phone.Name = "Phone";
            // 
            // AddAptBtn
            // 
            AddAptBtn.Location = new Point(12, 197);
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
            // TypeText
            // 
            TypeText.Location = new Point(447, 168);
            TypeText.Name = "TypeText";
            TypeText.PlaceholderText = "Type";
            TypeText.Size = new Size(111, 23);
            TypeText.TabIndex = 7;
            // 
            // StartText
            // 
            StartText.Location = new Point(564, 168);
            StartText.Name = "StartText";
            StartText.PlaceholderText = "Start";
            StartText.Size = new Size(109, 23);
            StartText.TabIndex = 8;
            // 
            // EndText
            // 
            EndText.Location = new Point(679, 168);
            EndText.Name = "EndText";
            EndText.PlaceholderText = "End";
            EndText.Size = new Size(109, 23);
            EndText.TabIndex = 9;
            // 
            // UpdateBtn
            // 
            UpdateBtn.Location = new Point(88, 197);
            UpdateBtn.Name = "UpdateBtn";
            UpdateBtn.Size = new Size(70, 23);
            UpdateBtn.TabIndex = 10;
            UpdateBtn.Text = "Update";
            UpdateBtn.UseVisualStyleBackColor = true;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Location = new Point(164, 197);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(70, 23);
            DeleteBtn.TabIndex = 11;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
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
            AddCustBtn.Location = new Point(460, 403);
            AddCustBtn.Name = "AddCustBtn";
            AddCustBtn.Size = new Size(72, 23);
            AddCustBtn.TabIndex = 16;
            AddCustBtn.Text = "Add";
            AddCustBtn.UseVisualStyleBackColor = true;
            AddCustBtn.Click += AddCustBtn_Click;
            // 
            // UpdateCustBtn
            // 
            UpdateCustBtn.Location = new Point(462, 374);
            UpdateCustBtn.Name = "UpdateCustBtn";
            UpdateCustBtn.Size = new Size(70, 23);
            UpdateCustBtn.TabIndex = 17;
            UpdateCustBtn.Text = "Update";
            UpdateCustBtn.UseVisualStyleBackColor = true;
            // 
            // DeleteCustBtn
            // 
            DeleteCustBtn.Location = new Point(462, 345);
            DeleteCustBtn.Name = "DeleteCustBtn";
            DeleteCustBtn.Size = new Size(70, 23);
            DeleteCustBtn.TabIndex = 18;
            DeleteCustBtn.Text = "Delete";
            DeleteCustBtn.UseVisualStyleBackColor = true;
            // 
            // GenRptBtn
            // 
            GenRptBtn.Location = new Point(679, 403);
            GenRptBtn.Name = "GenRptBtn";
            GenRptBtn.Size = new Size(109, 23);
            GenRptBtn.TabIndex = 19;
            GenRptBtn.Text = "Generate Report";
            GenRptBtn.UseVisualStyleBackColor = true;
            GenRptBtn.Click += GenRptBtn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
            Controls.Add(EndText);
            Controls.Add(StartText);
            Controls.Add(TypeText);
            Controls.Add(TitleText);
            Controls.Add(UserIdText);
            Controls.Add(CustIdText);
            Controls.Add(AptIdText);
            Controls.Add(AddAptBtn);
            Controls.Add(CustomerTable);
            Controls.Add(Appointments);
            Name = "MainForm";
            Text = "Appointment Scheduler";
            ((System.ComponentModel.ISupportInitialize)Appointments).EndInit();
            ((System.ComponentModel.ISupportInitialize)CustomerTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Appointments;
        private DataGridViewTextBoxColumn AppointmentID;
        private DataGridViewTextBoxColumn CustomerID;
        private DataGridViewTextBoxColumn UserID;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn Type;
        private DataGridViewTextBoxColumn Start;
        private DataGridViewTextBoxColumn End;
        private DataGridView CustomerTable;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Name;
        private DataGridViewTextBoxColumn Address;
        private DataGridViewTextBoxColumn Phone;
        private Button AddAptBtn;
        private TextBox AptIdText;
        private TextBox CustIdText;
        private TextBox UserIdText;
        private TextBox TitleText;
        private TextBox TypeText;
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
    }
}