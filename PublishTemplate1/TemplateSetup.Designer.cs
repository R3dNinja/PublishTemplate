namespace PublishTemplate
{
    partial class TemplateSetup
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SITE = new System.Windows.Forms.RadioButton();
            this.INTERIOR = new System.Windows.Forms.RadioButton();
            this.ARCHITECTURE = new System.Windows.Forms.RadioButton();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxMasterTemplate = new System.Windows.Forms.TextBox();
            this.txtBoxWorksharedTemplate = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SITE);
            this.groupBox1.Controls.Add(this.INTERIOR);
            this.groupBox1.Controls.Add(this.ARCHITECTURE);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Template Type";
            // 
            // SITE
            // 
            this.SITE.AutoSize = true;
            this.SITE.Location = new System.Drawing.Point(7, 68);
            this.SITE.Name = "SITE";
            this.SITE.Size = new System.Drawing.Size(43, 17);
            this.SITE.TabIndex = 2;
            this.SITE.TabStop = true;
            this.SITE.Text = "Site";
            this.SITE.UseVisualStyleBackColor = true;
            // 
            // INTERIOR
            // 
            this.INTERIOR.AutoSize = true;
            this.INTERIOR.Location = new System.Drawing.Point(7, 44);
            this.INTERIOR.Name = "INTERIOR";
            this.INTERIOR.Size = new System.Drawing.Size(57, 17);
            this.INTERIOR.TabIndex = 1;
            this.INTERIOR.TabStop = true;
            this.INTERIOR.Text = "Interior";
            this.INTERIOR.UseVisualStyleBackColor = true;
            // 
            // ARCHITECTURE
            // 
            this.ARCHITECTURE.AutoSize = true;
            this.ARCHITECTURE.Location = new System.Drawing.Point(7, 20);
            this.ARCHITECTURE.Name = "ARCHITECTURE";
            this.ARCHITECTURE.Size = new System.Drawing.Size(82, 17);
            this.ARCHITECTURE.TabIndex = 0;
            this.ARCHITECTURE.TabStop = true;
            this.ARCHITECTURE.Text = "Architecture";
            this.ARCHITECTURE.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(303, 114);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(110, 23);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Create Template";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(303, 143);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 142);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(245, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Template Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mater Template File";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Output Workshared File";
            // 
            // txtBoxMasterTemplate
            // 
            this.txtBoxMasterTemplate.Location = new System.Drawing.Point(136, 32);
            this.txtBoxMasterTemplate.Name = "txtBoxMasterTemplate";
            this.txtBoxMasterTemplate.Size = new System.Drawing.Size(277, 20);
            this.txtBoxMasterTemplate.TabIndex = 8;
            // 
            // txtBoxWorksharedTemplate
            // 
            this.txtBoxWorksharedTemplate.Location = new System.Drawing.Point(137, 90);
            this.txtBoxWorksharedTemplate.Name = "txtBoxWorksharedTemplate";
            this.txtBoxWorksharedTemplate.Size = new System.Drawing.Size(276, 20);
            this.txtBoxWorksharedTemplate.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(271, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Select Master Template";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(271, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Select Output File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TemplateSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 173);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBoxWorksharedTemplate);
            this.Controls.Add(this.txtBoxMasterTemplate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.groupBox1);
            this.Name = "TemplateSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TemplateSetup";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton SITE;
        private System.Windows.Forms.RadioButton INTERIOR;
        private System.Windows.Forms.RadioButton ARCHITECTURE;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxMasterTemplate;
        private System.Windows.Forms.TextBox txtBoxWorksharedTemplate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}