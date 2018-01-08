namespace AppFramework.Debugging
{
    partial class DebugMaskView
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
            this.grpFlags1 = new System.Windows.Forms.GroupBox();
            this.chkFlag8 = new System.Windows.Forms.CheckBox();
            this.chkFlag7 = new System.Windows.Forms.CheckBox();
            this.chkFlag6 = new System.Windows.Forms.CheckBox();
            this.chkFlag5 = new System.Windows.Forms.CheckBox();
            this.chkFlag4 = new System.Windows.Forms.CheckBox();
            this.chkFlag3 = new System.Windows.Forms.CheckBox();
            this.chkToggle1 = new System.Windows.Forms.CheckBox();
            this.chkFlag2 = new System.Windows.Forms.CheckBox();
            this.chkFlag1 = new System.Windows.Forms.CheckBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.grpFlags3 = new System.Windows.Forms.GroupBox();
            this.chkFlag24 = new System.Windows.Forms.CheckBox();
            this.chkFlag23 = new System.Windows.Forms.CheckBox();
            this.chkFlag22 = new System.Windows.Forms.CheckBox();
            this.chkFlag21 = new System.Windows.Forms.CheckBox();
            this.chkFlag20 = new System.Windows.Forms.CheckBox();
            this.chkFlag19 = new System.Windows.Forms.CheckBox();
            this.chkToggle3 = new System.Windows.Forms.CheckBox();
            this.chkFlag18 = new System.Windows.Forms.CheckBox();
            this.chkFlag17 = new System.Windows.Forms.CheckBox();
            this.grpFlags4 = new System.Windows.Forms.GroupBox();
            this.chkFlag32 = new System.Windows.Forms.CheckBox();
            this.chkFlag31 = new System.Windows.Forms.CheckBox();
            this.chkFlag30 = new System.Windows.Forms.CheckBox();
            this.chkFlag29 = new System.Windows.Forms.CheckBox();
            this.chkFlag28 = new System.Windows.Forms.CheckBox();
            this.chkFlag27 = new System.Windows.Forms.CheckBox();
            this.chkToggle4 = new System.Windows.Forms.CheckBox();
            this.chkFlag26 = new System.Windows.Forms.CheckBox();
            this.chkFlag25 = new System.Windows.Forms.CheckBox();
            this.grpMask = new System.Windows.Forms.GroupBox();
            this.cmdSetMask = new System.Windows.Forms.Button();
            this.txtMask = new System.Windows.Forms.TextBox();
            this.lblMask = new System.Windows.Forms.Label();
            this.grpFlags2 = new System.Windows.Forms.GroupBox();
            this.chkFlag16 = new System.Windows.Forms.CheckBox();
            this.chkFlag15 = new System.Windows.Forms.CheckBox();
            this.chkFlag14 = new System.Windows.Forms.CheckBox();
            this.chkFlag13 = new System.Windows.Forms.CheckBox();
            this.chkFlag12 = new System.Windows.Forms.CheckBox();
            this.chkFlag11 = new System.Windows.Forms.CheckBox();
            this.chkToggle2 = new System.Windows.Forms.CheckBox();
            this.chkFlag10 = new System.Windows.Forms.CheckBox();
            this.chkFlag9 = new System.Windows.Forms.CheckBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkIncludeDebugFlags = new System.Windows.Forms.CheckBox();
            this.chkIncludeDebugMask = new System.Windows.Forms.CheckBox();
            this.chkIncludeThreadId = new System.Windows.Forms.CheckBox();
            this.chkIncludeTimestamp = new System.Windows.Forms.CheckBox();
            this.chkToggleAll = new System.Windows.Forms.CheckBox();
            this.grpFlags1.SuspendLayout();
            this.grpFlags3.SuspendLayout();
            this.grpFlags4.SuspendLayout();
            this.grpMask.SuspendLayout();
            this.grpFlags2.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFlags1
            // 
            this.grpFlags1.Controls.Add(this.chkFlag8);
            this.grpFlags1.Controls.Add(this.chkFlag7);
            this.grpFlags1.Controls.Add(this.chkFlag6);
            this.grpFlags1.Controls.Add(this.chkFlag5);
            this.grpFlags1.Controls.Add(this.chkFlag4);
            this.grpFlags1.Controls.Add(this.chkFlag3);
            this.grpFlags1.Controls.Add(this.chkToggle1);
            this.grpFlags1.Controls.Add(this.chkFlag2);
            this.grpFlags1.Controls.Add(this.chkFlag1);
            this.grpFlags1.Location = new System.Drawing.Point(15, 20);
            this.grpFlags1.Name = "grpFlags1";
            this.grpFlags1.Size = new System.Drawing.Size(270, 230);
            this.grpFlags1.TabIndex = 0;
            this.grpFlags1.TabStop = false;
            this.grpFlags1.Text = "0x0000 00XX (Framework)";
            // 
            // chkFlag8
            // 
            this.chkFlag8.AutoSize = true;
            this.chkFlag8.Location = new System.Drawing.Point(6, 180);
            this.chkFlag8.Name = "chkFlag8";
            this.chkFlag8.Size = new System.Drawing.Size(151, 17);
            this.chkFlag8.TabIndex = 8;
            this.chkFlag8.Text = "LOG_FUNCTION_TRACE";
            this.chkFlag8.UseVisualStyleBackColor = true;
            this.chkFlag8.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag7
            // 
            this.chkFlag7.AutoSize = true;
            this.chkFlag7.Location = new System.Drawing.Point(6, 157);
            this.chkFlag7.Name = "chkFlag7";
            this.chkFlag7.Size = new System.Drawing.Size(121, 17);
            this.chkFlag7.TabIndex = 7;
            this.chkFlag7.Text = "LOG_CTOR_DTOR";
            this.chkFlag7.UseVisualStyleBackColor = true;
            this.chkFlag7.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag6
            // 
            this.chkFlag6.AutoSize = true;
            this.chkFlag6.Location = new System.Drawing.Point(6, 134);
            this.chkFlag6.Name = "chkFlag6";
            this.chkFlag6.Size = new System.Drawing.Size(106, 17);
            this.chkFlag6.TabIndex = 6;
            this.chkFlag6.Text = "LOG_SW_TEST";
            this.chkFlag6.UseVisualStyleBackColor = true;
            this.chkFlag6.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag5
            // 
            this.chkFlag5.AutoSize = true;
            this.chkFlag5.Location = new System.Drawing.Point(6, 111);
            this.chkFlag5.Name = "chkFlag5";
            this.chkFlag5.Size = new System.Drawing.Size(132, 17);
            this.chkFlag5.TabIndex = 5;
            this.chkFlag5.Text = "LOG_MAINTENANCE";
            this.chkFlag5.UseVisualStyleBackColor = true;
            this.chkFlag5.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag4
            // 
            this.chkFlag4.AutoSize = true;
            this.chkFlag4.Location = new System.Drawing.Point(6, 88);
            this.chkFlag4.Name = "chkFlag4";
            this.chkFlag4.Size = new System.Drawing.Size(105, 17);
            this.chkFlag4.TabIndex = 4;
            this.chkFlag4.Text = "LOG_VERBOSE";
            this.chkFlag4.UseVisualStyleBackColor = true;
            this.chkFlag4.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag3
            // 
            this.chkFlag3.AutoSize = true;
            this.chkFlag3.Location = new System.Drawing.Point(6, 65);
            this.chkFlag3.Name = "chkFlag3";
            this.chkFlag3.Size = new System.Drawing.Size(79, 17);
            this.chkFlag3.TabIndex = 3;
            this.chkFlag3.Text = "LOG_INFO";
            this.chkFlag3.UseVisualStyleBackColor = true;
            this.chkFlag3.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkToggle1
            // 
            this.chkToggle1.AutoSize = true;
            this.chkToggle1.Location = new System.Drawing.Point(6, 203);
            this.chkToggle1.Name = "chkToggle1";
            this.chkToggle1.Size = new System.Drawing.Size(73, 17);
            this.chkToggle1.TabIndex = 2;
            this.chkToggle1.Text = "Toggle All";
            this.chkToggle1.UseVisualStyleBackColor = true;
            this.chkToggle1.CheckedChanged += new System.EventHandler(this.chkToggle1_CheckedChanged);
            // 
            // chkFlag2
            // 
            this.chkFlag2.AutoSize = true;
            this.chkFlag2.Location = new System.Drawing.Point(6, 42);
            this.chkFlag2.Name = "chkFlag2";
            this.chkFlag2.Size = new System.Drawing.Size(107, 17);
            this.chkFlag2.TabIndex = 1;
            this.chkFlag2.Text = "LOG_WARNING";
            this.chkFlag2.UseVisualStyleBackColor = true;
            this.chkFlag2.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag1
            // 
            this.chkFlag1.AutoSize = true;
            this.chkFlag1.Location = new System.Drawing.Point(6, 19);
            this.chkFlag1.Name = "chkFlag1";
            this.chkFlag1.Size = new System.Drawing.Size(93, 17);
            this.chkFlag1.TabIndex = 0;
            this.chkFlag1.Text = "LOG_ERROR";
            this.chkFlag1.UseVisualStyleBackColor = true;
            this.chkFlag1.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(443, 601);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(118, 28);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // grpFlags3
            // 
            this.grpFlags3.Controls.Add(this.chkFlag24);
            this.grpFlags3.Controls.Add(this.chkFlag23);
            this.grpFlags3.Controls.Add(this.chkFlag22);
            this.grpFlags3.Controls.Add(this.chkFlag21);
            this.grpFlags3.Controls.Add(this.chkFlag20);
            this.grpFlags3.Controls.Add(this.chkFlag19);
            this.grpFlags3.Controls.Add(this.chkToggle3);
            this.grpFlags3.Controls.Add(this.chkFlag18);
            this.grpFlags3.Controls.Add(this.chkFlag17);
            this.grpFlags3.Location = new System.Drawing.Point(15, 256);
            this.grpFlags3.Name = "grpFlags3";
            this.grpFlags3.Size = new System.Drawing.Size(270, 230);
            this.grpFlags3.TabIndex = 4;
            this.grpFlags3.TabStop = false;
            this.grpFlags3.Text = "0x00XX 0000";
            // 
            // chkFlag24
            // 
            this.chkFlag24.AutoSize = true;
            this.chkFlag24.Location = new System.Drawing.Point(6, 180);
            this.chkFlag24.Name = "chkFlag24";
            this.chkFlag24.Size = new System.Drawing.Size(88, 17);
            this.chkFlag24.TabIndex = 17;
            this.chkFlag24.Text = "0x0080 0000";
            this.chkFlag24.UseVisualStyleBackColor = true;
            this.chkFlag24.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag23
            // 
            this.chkFlag23.AutoSize = true;
            this.chkFlag23.Location = new System.Drawing.Point(6, 157);
            this.chkFlag23.Name = "chkFlag23";
            this.chkFlag23.Size = new System.Drawing.Size(88, 17);
            this.chkFlag23.TabIndex = 16;
            this.chkFlag23.Text = "0x0040 0000";
            this.chkFlag23.UseVisualStyleBackColor = true;
            this.chkFlag23.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag22
            // 
            this.chkFlag22.AutoSize = true;
            this.chkFlag22.Location = new System.Drawing.Point(6, 134);
            this.chkFlag22.Name = "chkFlag22";
            this.chkFlag22.Size = new System.Drawing.Size(88, 17);
            this.chkFlag22.TabIndex = 15;
            this.chkFlag22.Text = "0x0020 0000";
            this.chkFlag22.UseVisualStyleBackColor = true;
            this.chkFlag22.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag21
            // 
            this.chkFlag21.AutoSize = true;
            this.chkFlag21.Location = new System.Drawing.Point(6, 111);
            this.chkFlag21.Name = "chkFlag21";
            this.chkFlag21.Size = new System.Drawing.Size(88, 17);
            this.chkFlag21.TabIndex = 14;
            this.chkFlag21.Text = "0x0010 0000";
            this.chkFlag21.UseVisualStyleBackColor = true;
            this.chkFlag21.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag20
            // 
            this.chkFlag20.AutoSize = true;
            this.chkFlag20.Location = new System.Drawing.Point(6, 88);
            this.chkFlag20.Name = "chkFlag20";
            this.chkFlag20.Size = new System.Drawing.Size(88, 17);
            this.chkFlag20.TabIndex = 13;
            this.chkFlag20.Text = "0x0008 0000";
            this.chkFlag20.UseVisualStyleBackColor = true;
            this.chkFlag20.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag19
            // 
            this.chkFlag19.AutoSize = true;
            this.chkFlag19.Location = new System.Drawing.Point(6, 65);
            this.chkFlag19.Name = "chkFlag19";
            this.chkFlag19.Size = new System.Drawing.Size(88, 17);
            this.chkFlag19.TabIndex = 12;
            this.chkFlag19.Text = "0x0004 0000";
            this.chkFlag19.UseVisualStyleBackColor = true;
            this.chkFlag19.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkToggle3
            // 
            this.chkToggle3.AutoSize = true;
            this.chkToggle3.Location = new System.Drawing.Point(6, 203);
            this.chkToggle3.Name = "chkToggle3";
            this.chkToggle3.Size = new System.Drawing.Size(73, 17);
            this.chkToggle3.TabIndex = 11;
            this.chkToggle3.Text = "Toggle All";
            this.chkToggle3.UseVisualStyleBackColor = true;
            this.chkToggle3.CheckedChanged += new System.EventHandler(this.chkToggle3_CheckedChanged);
            // 
            // chkFlag18
            // 
            this.chkFlag18.AutoSize = true;
            this.chkFlag18.Location = new System.Drawing.Point(6, 42);
            this.chkFlag18.Name = "chkFlag18";
            this.chkFlag18.Size = new System.Drawing.Size(88, 17);
            this.chkFlag18.TabIndex = 10;
            this.chkFlag18.Text = "0x0002 0000";
            this.chkFlag18.UseVisualStyleBackColor = true;
            this.chkFlag18.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag17
            // 
            this.chkFlag17.AutoSize = true;
            this.chkFlag17.Location = new System.Drawing.Point(6, 19);
            this.chkFlag17.Name = "chkFlag17";
            this.chkFlag17.Size = new System.Drawing.Size(88, 17);
            this.chkFlag17.TabIndex = 9;
            this.chkFlag17.Text = "0x0001 0000";
            this.chkFlag17.UseVisualStyleBackColor = true;
            this.chkFlag17.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // grpFlags4
            // 
            this.grpFlags4.Controls.Add(this.chkFlag32);
            this.grpFlags4.Controls.Add(this.chkFlag31);
            this.grpFlags4.Controls.Add(this.chkFlag30);
            this.grpFlags4.Controls.Add(this.chkFlag29);
            this.grpFlags4.Controls.Add(this.chkFlag28);
            this.grpFlags4.Controls.Add(this.chkFlag27);
            this.grpFlags4.Controls.Add(this.chkToggle4);
            this.grpFlags4.Controls.Add(this.chkFlag26);
            this.grpFlags4.Controls.Add(this.chkFlag25);
            this.grpFlags4.Location = new System.Drawing.Point(291, 256);
            this.grpFlags4.Name = "grpFlags4";
            this.grpFlags4.Size = new System.Drawing.Size(270, 230);
            this.grpFlags4.TabIndex = 5;
            this.grpFlags4.TabStop = false;
            this.grpFlags4.Text = "0xXX00 0000";
            // 
            // chkFlag32
            // 
            this.chkFlag32.AutoSize = true;
            this.chkFlag32.Location = new System.Drawing.Point(6, 180);
            this.chkFlag32.Name = "chkFlag32";
            this.chkFlag32.Size = new System.Drawing.Size(88, 17);
            this.chkFlag32.TabIndex = 26;
            this.chkFlag32.Text = "0x8000 0000";
            this.chkFlag32.UseVisualStyleBackColor = true;
            this.chkFlag32.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag31
            // 
            this.chkFlag31.AutoSize = true;
            this.chkFlag31.Location = new System.Drawing.Point(6, 157);
            this.chkFlag31.Name = "chkFlag31";
            this.chkFlag31.Size = new System.Drawing.Size(88, 17);
            this.chkFlag31.TabIndex = 25;
            this.chkFlag31.Text = "0x4000 0000";
            this.chkFlag31.UseVisualStyleBackColor = true;
            this.chkFlag31.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag30
            // 
            this.chkFlag30.AutoSize = true;
            this.chkFlag30.Location = new System.Drawing.Point(6, 134);
            this.chkFlag30.Name = "chkFlag30";
            this.chkFlag30.Size = new System.Drawing.Size(88, 17);
            this.chkFlag30.TabIndex = 24;
            this.chkFlag30.Text = "0x2000 0000";
            this.chkFlag30.UseVisualStyleBackColor = true;
            this.chkFlag30.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag29
            // 
            this.chkFlag29.AutoSize = true;
            this.chkFlag29.Location = new System.Drawing.Point(6, 111);
            this.chkFlag29.Name = "chkFlag29";
            this.chkFlag29.Size = new System.Drawing.Size(88, 17);
            this.chkFlag29.TabIndex = 23;
            this.chkFlag29.Text = "0x1000 0000";
            this.chkFlag29.UseVisualStyleBackColor = true;
            this.chkFlag29.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag28
            // 
            this.chkFlag28.AutoSize = true;
            this.chkFlag28.Location = new System.Drawing.Point(6, 88);
            this.chkFlag28.Name = "chkFlag28";
            this.chkFlag28.Size = new System.Drawing.Size(88, 17);
            this.chkFlag28.TabIndex = 22;
            this.chkFlag28.Text = "0x0800 0000";
            this.chkFlag28.UseVisualStyleBackColor = true;
            this.chkFlag28.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag27
            // 
            this.chkFlag27.AutoSize = true;
            this.chkFlag27.Location = new System.Drawing.Point(6, 65);
            this.chkFlag27.Name = "chkFlag27";
            this.chkFlag27.Size = new System.Drawing.Size(88, 17);
            this.chkFlag27.TabIndex = 21;
            this.chkFlag27.Text = "0x0400 0000";
            this.chkFlag27.UseVisualStyleBackColor = true;
            this.chkFlag27.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkToggle4
            // 
            this.chkToggle4.AutoSize = true;
            this.chkToggle4.Location = new System.Drawing.Point(6, 203);
            this.chkToggle4.Name = "chkToggle4";
            this.chkToggle4.Size = new System.Drawing.Size(73, 17);
            this.chkToggle4.TabIndex = 20;
            this.chkToggle4.Text = "Toggle All";
            this.chkToggle4.UseVisualStyleBackColor = true;
            this.chkToggle4.CheckedChanged += new System.EventHandler(this.chkToggle4_CheckedChanged);
            // 
            // chkFlag26
            // 
            this.chkFlag26.AutoSize = true;
            this.chkFlag26.Location = new System.Drawing.Point(6, 42);
            this.chkFlag26.Name = "chkFlag26";
            this.chkFlag26.Size = new System.Drawing.Size(88, 17);
            this.chkFlag26.TabIndex = 19;
            this.chkFlag26.Text = "0x0200 0000";
            this.chkFlag26.UseVisualStyleBackColor = true;
            this.chkFlag26.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag25
            // 
            this.chkFlag25.AutoSize = true;
            this.chkFlag25.Location = new System.Drawing.Point(6, 19);
            this.chkFlag25.Name = "chkFlag25";
            this.chkFlag25.Size = new System.Drawing.Size(88, 17);
            this.chkFlag25.TabIndex = 18;
            this.chkFlag25.Text = "0x0100 0000";
            this.chkFlag25.UseVisualStyleBackColor = true;
            this.chkFlag25.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // grpMask
            // 
            this.grpMask.Controls.Add(this.cmdSetMask);
            this.grpMask.Controls.Add(this.txtMask);
            this.grpMask.Controls.Add(this.lblMask);
            this.grpMask.Location = new System.Drawing.Point(12, 492);
            this.grpMask.Name = "grpMask";
            this.grpMask.Size = new System.Drawing.Size(273, 94);
            this.grpMask.TabIndex = 5;
            this.grpMask.TabStop = false;
            this.grpMask.Text = "Debug mask";
            // 
            // cmdSetMask
            // 
            this.cmdSetMask.Location = new System.Drawing.Point(157, 39);
            this.cmdSetMask.Name = "cmdSetMask";
            this.cmdSetMask.Size = new System.Drawing.Size(57, 28);
            this.cmdSetMask.TabIndex = 2;
            this.cmdSetMask.Text = "&Set";
            this.cmdSetMask.UseVisualStyleBackColor = true;
            this.cmdSetMask.Click += new System.EventHandler(this.cmdSetMask_Click);
            // 
            // txtMask
            // 
            this.txtMask.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMask.Location = new System.Drawing.Point(9, 44);
            this.txtMask.Name = "txtMask";
            this.txtMask.Size = new System.Drawing.Size(142, 20);
            this.txtMask.TabIndex = 1;
            // 
            // lblMask
            // 
            this.lblMask.AutoSize = true;
            this.lblMask.Location = new System.Drawing.Point(6, 28);
            this.lblMask.Name = "lblMask";
            this.lblMask.Size = new System.Drawing.Size(143, 13);
            this.lblMask.TabIndex = 0;
            this.lblMask.Text = "&Mask (HEX) 0xXXXX XXXX: ";
            // 
            // grpFlags2
            // 
            this.grpFlags2.Controls.Add(this.chkFlag16);
            this.grpFlags2.Controls.Add(this.chkFlag15);
            this.grpFlags2.Controls.Add(this.chkFlag14);
            this.grpFlags2.Controls.Add(this.chkFlag13);
            this.grpFlags2.Controls.Add(this.chkFlag12);
            this.grpFlags2.Controls.Add(this.chkFlag11);
            this.grpFlags2.Controls.Add(this.chkToggle2);
            this.grpFlags2.Controls.Add(this.chkFlag10);
            this.grpFlags2.Controls.Add(this.chkFlag9);
            this.grpFlags2.Location = new System.Drawing.Point(291, 20);
            this.grpFlags2.Name = "grpFlags2";
            this.grpFlags2.Size = new System.Drawing.Size(270, 230);
            this.grpFlags2.TabIndex = 7;
            this.grpFlags2.TabStop = false;
            this.grpFlags2.Text = "0x0000 XX00 (Framework)";
            // 
            // chkFlag16
            // 
            this.chkFlag16.AutoSize = true;
            this.chkFlag16.Location = new System.Drawing.Point(6, 180);
            this.chkFlag16.Name = "chkFlag16";
            this.chkFlag16.Size = new System.Drawing.Size(119, 17);
            this.chkFlag16.TabIndex = 8;
            this.chkFlag16.Text = "LOG_RESERVED8";
            this.chkFlag16.UseVisualStyleBackColor = true;
            this.chkFlag16.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag15
            // 
            this.chkFlag15.AutoSize = true;
            this.chkFlag15.Location = new System.Drawing.Point(6, 157);
            this.chkFlag15.Name = "chkFlag15";
            this.chkFlag15.Size = new System.Drawing.Size(119, 17);
            this.chkFlag15.TabIndex = 7;
            this.chkFlag15.Text = "LOG_RESERVED7";
            this.chkFlag15.UseVisualStyleBackColor = true;
            this.chkFlag15.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag14
            // 
            this.chkFlag14.AutoSize = true;
            this.chkFlag14.Location = new System.Drawing.Point(6, 134);
            this.chkFlag14.Name = "chkFlag14";
            this.chkFlag14.Size = new System.Drawing.Size(119, 17);
            this.chkFlag14.TabIndex = 6;
            this.chkFlag14.Text = "LOG_RESERVED6";
            this.chkFlag14.UseVisualStyleBackColor = true;
            this.chkFlag14.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag13
            // 
            this.chkFlag13.AutoSize = true;
            this.chkFlag13.Location = new System.Drawing.Point(6, 111);
            this.chkFlag13.Name = "chkFlag13";
            this.chkFlag13.Size = new System.Drawing.Size(119, 17);
            this.chkFlag13.TabIndex = 5;
            this.chkFlag13.Text = "LOG_RESERVED5";
            this.chkFlag13.UseVisualStyleBackColor = true;
            this.chkFlag13.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag12
            // 
            this.chkFlag12.AutoSize = true;
            this.chkFlag12.Location = new System.Drawing.Point(6, 88);
            this.chkFlag12.Name = "chkFlag12";
            this.chkFlag12.Size = new System.Drawing.Size(119, 17);
            this.chkFlag12.TabIndex = 4;
            this.chkFlag12.Text = "LOG_RESERVED4";
            this.chkFlag12.UseVisualStyleBackColor = true;
            this.chkFlag12.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag11
            // 
            this.chkFlag11.AutoSize = true;
            this.chkFlag11.Location = new System.Drawing.Point(6, 65);
            this.chkFlag11.Name = "chkFlag11";
            this.chkFlag11.Size = new System.Drawing.Size(119, 17);
            this.chkFlag11.TabIndex = 3;
            this.chkFlag11.Text = "LOG_RESERVED3";
            this.chkFlag11.UseVisualStyleBackColor = true;
            this.chkFlag11.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkToggle2
            // 
            this.chkToggle2.AutoSize = true;
            this.chkToggle2.Location = new System.Drawing.Point(6, 203);
            this.chkToggle2.Name = "chkToggle2";
            this.chkToggle2.Size = new System.Drawing.Size(73, 17);
            this.chkToggle2.TabIndex = 2;
            this.chkToggle2.Text = "Toggle All";
            this.chkToggle2.UseVisualStyleBackColor = true;
            this.chkToggle2.CheckedChanged += new System.EventHandler(this.chkToggle2_CheckedChanged);
            // 
            // chkFlag10
            // 
            this.chkFlag10.AutoSize = true;
            this.chkFlag10.Location = new System.Drawing.Point(6, 42);
            this.chkFlag10.Name = "chkFlag10";
            this.chkFlag10.Size = new System.Drawing.Size(119, 17);
            this.chkFlag10.TabIndex = 1;
            this.chkFlag10.Text = "LOG_RESERVED2";
            this.chkFlag10.UseVisualStyleBackColor = true;
            this.chkFlag10.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // chkFlag9
            // 
            this.chkFlag9.AutoSize = true;
            this.chkFlag9.Location = new System.Drawing.Point(6, 19);
            this.chkFlag9.Name = "chkFlag9";
            this.chkFlag9.Size = new System.Drawing.Size(119, 17);
            this.chkFlag9.TabIndex = 0;
            this.chkFlag9.Text = "DebugFlagTcpIp";
            this.chkFlag9.UseVisualStyleBackColor = true;
            this.chkFlag9.CheckedChanged += new System.EventHandler(this.chkFlag_CheckedChanged);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkIncludeDebugFlags);
            this.grpOptions.Controls.Add(this.chkIncludeDebugMask);
            this.grpOptions.Controls.Add(this.chkIncludeThreadId);
            this.grpOptions.Controls.Add(this.chkIncludeTimestamp);
            this.grpOptions.Location = new System.Drawing.Point(291, 492);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(270, 94);
            this.grpOptions.TabIndex = 8;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // chkIncludeDebugFlags
            // 
            this.chkIncludeDebugFlags.AutoSize = true;
            this.chkIncludeDebugFlags.Location = new System.Drawing.Point(125, 42);
            this.chkIncludeDebugFlags.Name = "chkIncludeDebugFlags";
            this.chkIncludeDebugFlags.Size = new System.Drawing.Size(119, 17);
            this.chkIncludeDebugFlags.TabIndex = 3;
            this.chkIncludeDebugFlags.Text = "Include debug flags";
            this.chkIncludeDebugFlags.UseVisualStyleBackColor = true;
            this.chkIncludeDebugFlags.CheckedChanged += new System.EventHandler(this.chkIncludeDebugFlags_CheckedChanged);
            // 
            // chkIncludeDebugMask
            // 
            this.chkIncludeDebugMask.AutoSize = true;
            this.chkIncludeDebugMask.Location = new System.Drawing.Point(125, 19);
            this.chkIncludeDebugMask.Name = "chkIncludeDebugMask";
            this.chkIncludeDebugMask.Size = new System.Drawing.Size(122, 17);
            this.chkIncludeDebugMask.TabIndex = 2;
            this.chkIncludeDebugMask.Text = "Include debug mask";
            this.chkIncludeDebugMask.UseVisualStyleBackColor = true;
            this.chkIncludeDebugMask.CheckedChanged += new System.EventHandler(this.chkIncludeDebugMask_CheckedChanged);
            // 
            // chkIncludeThreadId
            // 
            this.chkIncludeThreadId.AutoSize = true;
            this.chkIncludeThreadId.Location = new System.Drawing.Point(6, 42);
            this.chkIncludeThreadId.Name = "chkIncludeThreadId";
            this.chkIncludeThreadId.Size = new System.Drawing.Size(105, 17);
            this.chkIncludeThreadId.TabIndex = 1;
            this.chkIncludeThreadId.Text = "Include thread id";
            this.chkIncludeThreadId.UseVisualStyleBackColor = true;
            this.chkIncludeThreadId.CheckedChanged += new System.EventHandler(this.chkIncludeThreadId_CheckedChanged);
            // 
            // chkIncludeTimestamp
            // 
            this.chkIncludeTimestamp.AutoSize = true;
            this.chkIncludeTimestamp.Location = new System.Drawing.Point(6, 19);
            this.chkIncludeTimestamp.Name = "chkIncludeTimestamp";
            this.chkIncludeTimestamp.Size = new System.Drawing.Size(111, 17);
            this.chkIncludeTimestamp.TabIndex = 0;
            this.chkIncludeTimestamp.Text = "Include timestamp";
            this.chkIncludeTimestamp.UseVisualStyleBackColor = true;
            this.chkIncludeTimestamp.CheckedChanged += new System.EventHandler(this.chkIncludeTimestamp_CheckedChanged);
            // 
            // chkToggleAll
            // 
            this.chkToggleAll.AutoSize = true;
            this.chkToggleAll.Location = new System.Drawing.Point(297, 608);
            this.chkToggleAll.Name = "chkToggleAll";
            this.chkToggleAll.Size = new System.Drawing.Size(72, 17);
            this.chkToggleAll.TabIndex = 9;
            this.chkToggleAll.Text = "Toggle all";
            this.chkToggleAll.UseVisualStyleBackColor = true;
            this.chkToggleAll.CheckedChanged += new System.EventHandler(this.chkToggleAll_CheckedChanged);
            // 
            // DebugMaskView
            // 
            this.AcceptButton = this.cmdCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(579, 647);
            this.Controls.Add(this.chkToggleAll);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.grpFlags2);
            this.Controls.Add(this.grpMask);
            this.Controls.Add(this.grpFlags4);
            this.Controls.Add(this.grpFlags3);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpFlags1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DebugMaskView";
            this.Text = "Debug Flags";
            this.Load += new System.EventHandler(this.DebugMaskView_Load);
            this.grpFlags1.ResumeLayout(false);
            this.grpFlags1.PerformLayout();
            this.grpFlags3.ResumeLayout(false);
            this.grpFlags3.PerformLayout();
            this.grpFlags4.ResumeLayout(false);
            this.grpFlags4.PerformLayout();
            this.grpMask.ResumeLayout(false);
            this.grpMask.PerformLayout();
            this.grpFlags2.ResumeLayout(false);
            this.grpFlags2.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFlags1;
        private System.Windows.Forms.CheckBox chkToggle1;
        private System.Windows.Forms.CheckBox chkFlag2;
        private System.Windows.Forms.CheckBox chkFlag1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.CheckBox chkFlag8;
        private System.Windows.Forms.CheckBox chkFlag7;
        private System.Windows.Forms.CheckBox chkFlag6;
        private System.Windows.Forms.CheckBox chkFlag5;
        private System.Windows.Forms.CheckBox chkFlag4;
        private System.Windows.Forms.CheckBox chkFlag3;
        private System.Windows.Forms.GroupBox grpFlags3;
        private System.Windows.Forms.GroupBox grpFlags4;
        private System.Windows.Forms.GroupBox grpMask;
        private System.Windows.Forms.GroupBox grpFlags2;
        private System.Windows.Forms.CheckBox chkFlag16;
        private System.Windows.Forms.CheckBox chkFlag15;
        private System.Windows.Forms.CheckBox chkFlag14;
        private System.Windows.Forms.CheckBox chkFlag13;
        private System.Windows.Forms.CheckBox chkFlag12;
        private System.Windows.Forms.CheckBox chkFlag11;
        private System.Windows.Forms.CheckBox chkToggle2;
        private System.Windows.Forms.CheckBox chkFlag10;
        private System.Windows.Forms.CheckBox chkFlag9;
        private System.Windows.Forms.CheckBox chkFlag24;
        private System.Windows.Forms.CheckBox chkFlag23;
        private System.Windows.Forms.CheckBox chkFlag22;
        private System.Windows.Forms.CheckBox chkFlag21;
        private System.Windows.Forms.CheckBox chkFlag20;
        private System.Windows.Forms.CheckBox chkFlag19;
        private System.Windows.Forms.CheckBox chkToggle3;
        private System.Windows.Forms.CheckBox chkFlag18;
        private System.Windows.Forms.CheckBox chkFlag17;
        private System.Windows.Forms.CheckBox chkFlag32;
        private System.Windows.Forms.CheckBox chkFlag31;
        private System.Windows.Forms.CheckBox chkFlag30;
        private System.Windows.Forms.CheckBox chkFlag29;
        private System.Windows.Forms.CheckBox chkFlag28;
        private System.Windows.Forms.CheckBox chkFlag27;
        private System.Windows.Forms.CheckBox chkToggle4;
        private System.Windows.Forms.CheckBox chkFlag26;
        private System.Windows.Forms.CheckBox chkFlag25;
        private System.Windows.Forms.Label lblMask;
        private System.Windows.Forms.Button cmdSetMask;
        private System.Windows.Forms.TextBox txtMask;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkIncludeDebugFlags;
        private System.Windows.Forms.CheckBox chkIncludeDebugMask;
        private System.Windows.Forms.CheckBox chkIncludeThreadId;
        private System.Windows.Forms.CheckBox chkIncludeTimestamp;
        private System.Windows.Forms.CheckBox chkToggleAll;
    }
}