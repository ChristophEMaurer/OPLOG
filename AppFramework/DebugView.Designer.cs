namespace AppFramework.Debugging
{
    partial class DebugView
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
            this.lvData = new System.Windows.Forms.ListView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnStop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvData
            // 
            this.lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvData.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvData.FullRowSelect = true;
            this.lvData.Location = new System.Drawing.Point(0, 24);
            this.lvData.Name = "lvData";
            this.lvData.Size = new System.Drawing.Size(1010, 422);
            this.lvData.TabIndex = 0;
            this.lvData.UseCompatibleStateImageBehavior = false;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnClear,
            this.mnOptions,
            this.mnStart,
            this.mnStop});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1010, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // mnClear
            // 
            this.mnClear.Name = "mnClear";
            this.mnClear.Size = new System.Drawing.Size(46, 20);
            this.mnClear.Text = "Clear";
            this.mnClear.Click += new System.EventHandler(this.mnClear_Click);
            // 
            // mnOptions
            // 
            this.mnOptions.Name = "mnOptions";
            this.mnOptions.Size = new System.Drawing.Size(61, 20);
            this.mnOptions.Text = "Options";
            this.mnOptions.Click += new System.EventHandler(this.mnOptions_Click);
            // 
            // mnStop
            // 
            this.mnStop.Name = "mnStop";
            this.mnStop.Size = new System.Drawing.Size(43, 20);
            this.mnStop.Text = "Stop";
            this.mnStop.Click += new System.EventHandler(this.mnStop_Click);
            // 
            // mnStart
            // 
            this.mnStart.Name = "mnStart";
            this.mnStart.Size = new System.Drawing.Size(43, 20);
            this.mnStart.Text = "Start";
            this.mnStart.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // DebugView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 446);
            this.Controls.Add(this.lvData);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "DebugView";
            this.Text = "DebugView";
            this.Load += new System.EventHandler(this.DebugView_Load);
            this.Shown += new System.EventHandler(this.DebugView_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugView_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvData;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnClear;
        private System.Windows.Forms.ToolStripMenuItem mnOptions;
        private System.Windows.Forms.ToolStripMenuItem mnStop;
        private System.Windows.Forms.ToolStripMenuItem mnStart;
    }
}