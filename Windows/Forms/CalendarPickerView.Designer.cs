namespace Windows.Forms
{
    partial class CalendarPickerView
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
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.cmdOK = new Windows.Forms.OplButton();
            this.txtYearDec = new Windows.Forms.OplButton();
            this.cmdYearInc = new Windows.Forms.OplButton();
            this.txtYearDec10 = new Windows.Forms.OplButton();
            this.cmdYearInc10 = new Windows.Forms.OplButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // calendar
            // 
            this.calendar.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.calendar.Location = new System.Drawing.Point(12, 26);
            this.calendar.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.calendar.Name = "calendar";
            this.calendar.ShowWeekNumbers = true;
            this.calendar.TabIndex = 0;
            this.calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(312, 225);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(70, 28);
            this.cmdCancel.TabIndex = 14;
            this.cmdCancel.Text = "Abbrechen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(236, 225);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(70, 28);
            this.cmdOK.TabIndex = 13;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // txtYearDec
            // 
            this.txtYearDec.Location = new System.Drawing.Point(214, 26);
            this.txtYearDec.Name = "txtYearDec";
            this.txtYearDec.Size = new System.Drawing.Size(70, 28);
            this.txtYearDec.TabIndex = 15;
            this.txtYearDec.Text = "< 1 Jahr";
            this.txtYearDec.UseVisualStyleBackColor = true;
            this.txtYearDec.Click += new System.EventHandler(this.txtYearDec_Click);
            // 
            // cmdYearInc
            // 
            this.cmdYearInc.Location = new System.Drawing.Point(290, 26);
            this.cmdYearInc.Name = "cmdYearInc";
            this.cmdYearInc.Size = new System.Drawing.Size(70, 28);
            this.cmdYearInc.TabIndex = 16;
            this.cmdYearInc.Text = "1 Jahr >";
            this.cmdYearInc.UseVisualStyleBackColor = true;
            this.cmdYearInc.Click += new System.EventHandler(this.cmdYearInc_Click);
            // 
            // txtYearDec10
            // 
            this.txtYearDec10.Location = new System.Drawing.Point(214, 60);
            this.txtYearDec10.Name = "txtYearDec10";
            this.txtYearDec10.Size = new System.Drawing.Size(70, 28);
            this.txtYearDec10.TabIndex = 17;
            this.txtYearDec10.Text = "< 10 Jahre";
            this.txtYearDec10.UseVisualStyleBackColor = true;
            this.txtYearDec10.Click += new System.EventHandler(this.txtYearDec10_Click);
            // 
            // cmdYearInc10
            // 
            this.cmdYearInc10.Location = new System.Drawing.Point(290, 60);
            this.cmdYearInc10.Name = "cmdYearInc10";
            this.cmdYearInc10.Size = new System.Drawing.Size(70, 28);
            this.cmdYearInc10.TabIndex = 18;
            this.cmdYearInc10.Text = "10 Jahre >";
            this.cmdYearInc10.UseVisualStyleBackColor = true;
            this.cmdYearInc10.Click += new System.EventHandler(this.cmdYearInc10_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.calendar);
            this.groupBox1.Controls.Add(this.cmdYearInc10);
            this.groupBox1.Controls.Add(this.cmdYearInc);
            this.groupBox1.Controls.Add(this.txtYearDec);
            this.groupBox1.Controls.Add(this.txtYearDec10);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 207);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // CalendarPickerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(395, 271);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CalendarPickerView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Datum auswählen";
            this.Load += new System.EventHandler(this.CalendarPickerView_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar calendar;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplButton txtYearDec;
        private Windows.Forms.OplButton cmdYearInc;
        private Windows.Forms.OplButton txtYearDec10;
        private Windows.Forms.OplButton cmdYearInc10;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}