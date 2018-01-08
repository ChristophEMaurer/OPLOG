namespace Operationen.Wizards.ImportRichtlinien
{
    partial class SelectGebiet
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvGebiete = new Windows.Forms.OplListView();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvGebiete
            // 
            this.lvGebiete.Location = new System.Drawing.Point(21, 57);
            this.lvGebiete.Name = "lvGebiete";
            this.lvGebiete.Size = new System.Drawing.Size(314, 246);
            this.lvGebiete.TabIndex = 5;
            this.lvGebiete.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 43);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wählen Sie das Gebiet aus, für welches die Richtlinien importiert werden sollen:";
            // 
            // SelectGebiet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvGebiete);
            this.Controls.Add(this.label1);
            this.Name = "SelectGebiet";
            this.Size = new System.Drawing.Size(350, 320);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplListView lvGebiete;
        private System.Windows.Forms.Label label1;
    }
}
