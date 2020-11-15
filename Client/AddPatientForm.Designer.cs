namespace Client
{
    partial class AddPatientForm
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
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.NameLabel = new System.Windows.Forms.Label();
            this.MiddleNameLabel = new System.Windows.Forms.Label();
            this.LastNameLabel = new System.Windows.Forms.Label();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.InsurancePolicyLabel = new System.Windows.Forms.Label();
            this.DateOfBirthLabel = new System.Windows.Forms.Label();
            this.FirstNameTextBox = new System.Windows.Forms.TextBox();
            this.MiddleNameTextBox = new System.Windows.Forms.TextBox();
            this.LastNameTextBox = new System.Windows.Forms.TextBox();
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.InsurancePolicyTextBox = new System.Windows.Forms.TextBox();
            this.DateOfBirthPicker = new System.Windows.Forms.DateTimePicker();
            this.AddBtn = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 19);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(36, 17);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Імя: ";
            // 
            // MiddleNameLabel
            // 
            this.MiddleNameLabel.AutoSize = true;
            this.MiddleNameLabel.Location = new System.Drawing.Point(12, 47);
            this.MiddleNameLabel.Name = "MiddleNameLabel";
            this.MiddleNameLabel.Size = new System.Drawing.Size(93, 17);
            this.MiddleNameLabel.TabIndex = 1;
            this.MiddleNameLabel.Text = "По батькові: ";
            // 
            // LastNameLabel
            // 
            this.LastNameLabel.AutoSize = true;
            this.LastNameLabel.Location = new System.Drawing.Point(12, 75);
            this.LastNameLabel.Name = "LastNameLabel";
            this.LastNameLabel.Size = new System.Drawing.Size(78, 17);
            this.LastNameLabel.TabIndex = 2;
            this.LastNameLabel.Text = "Прізвище: ";
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(12, 103);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(64, 17);
            this.AddressLabel.TabIndex = 3;
            this.AddressLabel.Text = "Адреса: ";
            // 
            // InsurancePolicyLabel
            // 
            this.InsurancePolicyLabel.AutoSize = true;
            this.InsurancePolicyLabel.Location = new System.Drawing.Point(12, 131);
            this.InsurancePolicyLabel.Name = "InsurancePolicyLabel";
            this.InsurancePolicyLabel.Size = new System.Drawing.Size(123, 17);
            this.InsurancePolicyLabel.TabIndex = 4;
            this.InsurancePolicyLabel.Text = "Страховий поліс: ";
            // 
            // DateOfBirthLabel
            // 
            this.DateOfBirthLabel.AutoSize = true;
            this.DateOfBirthLabel.Location = new System.Drawing.Point(12, 159);
            this.DateOfBirthLabel.Name = "DateOfBirthLabel";
            this.DateOfBirthLabel.Size = new System.Drawing.Size(135, 17);
            this.DateOfBirthLabel.TabIndex = 5;
            this.DateOfBirthLabel.Text = "Дата народження: ";
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.Location = new System.Drawing.Point(182, 19);
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.Size = new System.Drawing.Size(213, 22);
            this.FirstNameTextBox.TabIndex = 6;
            // 
            // MiddleNameTextBox
            // 
            this.MiddleNameTextBox.Location = new System.Drawing.Point(182, 47);
            this.MiddleNameTextBox.Name = "MiddleNameTextBox";
            this.MiddleNameTextBox.Size = new System.Drawing.Size(213, 22);
            this.MiddleNameTextBox.TabIndex = 7;
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.Location = new System.Drawing.Point(182, 75);
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.Size = new System.Drawing.Size(213, 22);
            this.LastNameTextBox.TabIndex = 8;
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Location = new System.Drawing.Point(182, 103);
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(213, 22);
            this.AddressTextBox.TabIndex = 9;
            // 
            // InsurancePolicyTextBox
            // 
            this.InsurancePolicyTextBox.Location = new System.Drawing.Point(182, 131);
            this.InsurancePolicyTextBox.Name = "InsurancePolicyTextBox";
            this.InsurancePolicyTextBox.Size = new System.Drawing.Size(213, 22);
            this.InsurancePolicyTextBox.TabIndex = 10;
            // 
            // DateOfBirthPicker
            // 
            this.DateOfBirthPicker.Location = new System.Drawing.Point(182, 159);
            this.DateOfBirthPicker.Name = "DateOfBirthPicker";
            this.DateOfBirthPicker.Size = new System.Drawing.Size(213, 22);
            this.DateOfBirthPicker.TabIndex = 11;
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(15, 197);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(380, 35);
            this.AddBtn.TabIndex = 12;
            this.AddBtn.Text = "Додати";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(12, 257);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(70, 21);
            this.StatusLabel.TabIndex = 13;
            this.StatusLabel.Text = "Status: ";
            // 
            // AddPatientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 342);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.DateOfBirthPicker);
            this.Controls.Add(this.InsurancePolicyTextBox);
            this.Controls.Add(this.AddressTextBox);
            this.Controls.Add(this.LastNameTextBox);
            this.Controls.Add(this.MiddleNameTextBox);
            this.Controls.Add(this.FirstNameTextBox);
            this.Controls.Add(this.DateOfBirthLabel);
            this.Controls.Add(this.InsurancePolicyLabel);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.LastNameLabel);
            this.Controls.Add(this.MiddleNameLabel);
            this.Controls.Add(this.NameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPatientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddPatientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label MiddleNameLabel;
        private System.Windows.Forms.Label LastNameLabel;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.Label InsurancePolicyLabel;
        private System.Windows.Forms.Label DateOfBirthLabel;
        private System.Windows.Forms.TextBox FirstNameTextBox;
        private System.Windows.Forms.TextBox MiddleNameTextBox;
        private System.Windows.Forms.TextBox LastNameTextBox;
        private System.Windows.Forms.TextBox AddressTextBox;
        private System.Windows.Forms.TextBox InsurancePolicyTextBox;
        private System.Windows.Forms.DateTimePicker DateOfBirthPicker;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Label StatusLabel;
    }
}