
using System.Linq;

namespace Client
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
            this.PortLabel = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.AddPatientBtn = new System.Windows.Forms.Button();
            this.AddDoctorBtn = new System.Windows.Forms.Button();
            this.AddUserBtn = new System.Windows.Forms.Button();
            this.ViewPatientsBtn = new System.Windows.Forms.Button();
            this.ViewDoctorsBtn = new System.Windows.Forms.Button();
            this.ViewUsersBtn = new System.Windows.Forms.Button();
            this.AddAdmissionBtn = new System.Windows.Forms.Button();
            this.ViewAdmissionBtn = new System.Windows.Forms.Button();
            this.AddWardBtn = new System.Windows.Forms.Button();
            this.ViewWardsBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(12, 31);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(50, 17);
            this.PortLabel.TabIndex = 0;
            this.PortLabel.Text = "Port: 0";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Location = new System.Drawing.Point(12, 9);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(28, 17);
            this.IPLabel.TabIndex = 1;
            this.IPLabel.Text = "IP: ";
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Location = new System.Drawing.Point(12, 48);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(51, 17);
            this.LoginLabel.TabIndex = 2;
            this.LoginLabel.Text = "Login: ";
            // 
            // AddPatientBtn
            // 
            this.AddPatientBtn.Location = new System.Drawing.Point(204, 12);
            this.AddPatientBtn.Name = "AddPatientBtn";
            this.AddPatientBtn.Size = new System.Drawing.Size(188, 132);
            this.AddPatientBtn.TabIndex = 3;
            this.AddPatientBtn.Text = "Додати пацієнта";
            this.AddPatientBtn.UseVisualStyleBackColor = true;
            this.AddPatientBtn.Click += new System.EventHandler(this.AddPatientBtn_Click);
            // 
            // AddDoctorBtn
            // 
            this.AddDoctorBtn.Location = new System.Drawing.Point(426, 12);
            this.AddDoctorBtn.Name = "AddDoctorBtn";
            this.AddDoctorBtn.Size = new System.Drawing.Size(188, 132);
            this.AddDoctorBtn.TabIndex = 4;
            this.AddDoctorBtn.Text = "Додати Лікаря";
            this.AddDoctorBtn.UseVisualStyleBackColor = true;
            // 
            // AddUserBtn
            // 
            this.AddUserBtn.Enabled = false;
            this.AddUserBtn.Location = new System.Drawing.Point(646, 12);
            this.AddUserBtn.Name = "AddUserBtn";
            this.AddUserBtn.Size = new System.Drawing.Size(188, 132);
            this.AddUserBtn.TabIndex = 5;
            this.AddUserBtn.Text = "Додати користувача";
            this.AddUserBtn.UseVisualStyleBackColor = true;
            // 
            // ViewPatientsBtn
            // 
            this.ViewPatientsBtn.Location = new System.Drawing.Point(204, 175);
            this.ViewPatientsBtn.Name = "ViewPatientsBtn";
            this.ViewPatientsBtn.Size = new System.Drawing.Size(188, 132);
            this.ViewPatientsBtn.TabIndex = 6;
            this.ViewPatientsBtn.Text = "Переглянути пацієнтів";
            this.ViewPatientsBtn.UseVisualStyleBackColor = true;
            // 
            // ViewDoctorsBtn
            // 
            this.ViewDoctorsBtn.Location = new System.Drawing.Point(426, 175);
            this.ViewDoctorsBtn.Name = "ViewDoctorsBtn";
            this.ViewDoctorsBtn.Size = new System.Drawing.Size(188, 132);
            this.ViewDoctorsBtn.TabIndex = 7;
            this.ViewDoctorsBtn.Text = "Переглянути лікарів";
            this.ViewDoctorsBtn.UseVisualStyleBackColor = true;
            // 
            // ViewUsersBtn
            // 
            this.ViewUsersBtn.Enabled = false;
            this.ViewUsersBtn.Location = new System.Drawing.Point(646, 175);
            this.ViewUsersBtn.Name = "ViewUsersBtn";
            this.ViewUsersBtn.Size = new System.Drawing.Size(188, 132);
            this.ViewUsersBtn.TabIndex = 8;
            this.ViewUsersBtn.Text = "Переглянути користувачів";
            this.ViewUsersBtn.UseVisualStyleBackColor = true;
            // 
            // AddAdmissionBtn
            // 
            this.AddAdmissionBtn.Location = new System.Drawing.Point(204, 340);
            this.AddAdmissionBtn.Name = "AddAdmissionBtn";
            this.AddAdmissionBtn.Size = new System.Drawing.Size(188, 132);
            this.AddAdmissionBtn.TabIndex = 9;
            this.AddAdmissionBtn.Text = "Додати надходження";
            this.AddAdmissionBtn.UseVisualStyleBackColor = true;
            // 
            // ViewAdmissionBtn
            // 
            this.ViewAdmissionBtn.Location = new System.Drawing.Point(204, 519);
            this.ViewAdmissionBtn.Name = "ViewAdmissionBtn";
            this.ViewAdmissionBtn.Size = new System.Drawing.Size(188, 132);
            this.ViewAdmissionBtn.TabIndex = 10;
            this.ViewAdmissionBtn.Text = "Переглянути надходження";
            this.ViewAdmissionBtn.UseVisualStyleBackColor = true;
            // 
            // AddWardBtn
            // 
            this.AddWardBtn.Location = new System.Drawing.Point(426, 340);
            this.AddWardBtn.Name = "AddWardBtn";
            this.AddWardBtn.Size = new System.Drawing.Size(188, 132);
            this.AddWardBtn.TabIndex = 11;
            this.AddWardBtn.Text = "Додати палату";
            this.AddWardBtn.UseVisualStyleBackColor = true;
            // 
            // ViewWardsBtn
            // 
            this.ViewWardsBtn.Location = new System.Drawing.Point(426, 519);
            this.ViewWardsBtn.Name = "ViewWardsBtn";
            this.ViewWardsBtn.Size = new System.Drawing.Size(188, 132);
            this.ViewWardsBtn.TabIndex = 12;
            this.ViewWardsBtn.Text = "Переглянути палати";
            this.ViewWardsBtn.UseVisualStyleBackColor = true;
            // 
            // ExitBtn
            // 
            this.ExitBtn.Location = new System.Drawing.Point(646, 340);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(188, 311);
            this.ExitBtn.TabIndex = 13;
            this.ExitBtn.Text = "Вийти";
            this.ExitBtn.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 682);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.ViewWardsBtn);
            this.Controls.Add(this.AddWardBtn);
            this.Controls.Add(this.ViewAdmissionBtn);
            this.Controls.Add(this.AddAdmissionBtn);
            this.Controls.Add(this.ViewUsersBtn);
            this.Controls.Add(this.ViewDoctorsBtn);
            this.Controls.Add(this.ViewPatientsBtn);
            this.Controls.Add(this.AddUserBtn);
            this.Controls.Add(this.AddDoctorBtn);
            this.Controls.Add(this.AddPatientBtn);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.PortLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Button AddPatientBtn;
        private System.Windows.Forms.Button AddDoctorBtn;
        private System.Windows.Forms.Button AddUserBtn;
        private System.Windows.Forms.Button ViewPatientsBtn;
        private System.Windows.Forms.Button ViewDoctorsBtn;
        private System.Windows.Forms.Button ViewUsersBtn;
        private System.Windows.Forms.Button AddAdmissionBtn;
        private System.Windows.Forms.Button ViewAdmissionBtn;
        private System.Windows.Forms.Button AddWardBtn;
        private System.Windows.Forms.Button ViewWardsBtn;
        private System.Windows.Forms.Button ExitBtn;
    }
}