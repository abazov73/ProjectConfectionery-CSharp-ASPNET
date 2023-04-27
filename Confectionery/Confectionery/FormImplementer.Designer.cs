namespace Confectionery
{
    partial class FormImplementer
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
            this.textBoxWorkExperience = new System.Windows.Forms.TextBox();
            this.textBoxFIO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxQualification = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxWorkExperience
            // 
            this.textBoxWorkExperience.Location = new System.Drawing.Point(118, 76);
            this.textBoxWorkExperience.Name = "textBoxWorkExperience";
            this.textBoxWorkExperience.Size = new System.Drawing.Size(191, 27);
            this.textBoxWorkExperience.TabIndex = 11;
            // 
            // textBoxFIO
            // 
            this.textBoxFIO.Location = new System.Drawing.Point(118, 6);
            this.textBoxFIO.Name = "textBoxFIO";
            this.textBoxFIO.Size = new System.Drawing.Size(330, 27);
            this.textBoxFIO.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Опыт работы:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(64, 6);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(45, 20);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "ФИО:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(354, 163);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(254, 163);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(94, 29);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(118, 39);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(330, 27);
            this.textBoxPassword.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Пароль:";
            // 
            // textBoxQualification
            // 
            this.textBoxQualification.Location = new System.Drawing.Point(118, 109);
            this.textBoxQualification.Name = "textBoxQualification";
            this.textBoxQualification.Size = new System.Drawing.Size(191, 27);
            this.textBoxQualification.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-2, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Квалификация";
            // 
            // FormImplementer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 202);
            this.Controls.Add(this.textBoxQualification);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxWorkExperience);
            this.Controls.Add(this.textBoxFIO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Name = "FormImplementer";
            this.Text = "Исполнитель";
            this.Load += new System.EventHandler(this.FormImplementer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxWorkExperience;
        private TextBox textBoxFIO;
        private Label label1;
        private Label labelName;
        private Button buttonCancel;
        private Button buttonSave;
        private TextBox textBoxPassword;
        private Label label2;
        private TextBox textBoxQualification;
        private Label label3;
    }
}