namespace NeedlePath
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pb = new System.Windows.Forms.PictureBox();
            this.pb_inplane = new System.Windows.Forms.PictureBox();
            this.pb_outplane = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbTip = new System.Windows.Forms.RadioButton();
            this.rbTarget = new System.Windows.Forms.RadioButton();
            this.rbStart = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Markers = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelz = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_inplane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_outplane)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb.Location = new System.Drawing.Point(569, 134);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(512, 512);
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_MouseDown);
            this.pb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_MouseMove);
            this.pb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MouseUp);
            // 
            // pb_inplane
            // 
            this.pb_inplane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_inplane.Location = new System.Drawing.Point(36, 401);
            this.pb_inplane.Name = "pb_inplane";
            this.pb_inplane.Size = new System.Drawing.Size(240, 240);
            this.pb_inplane.TabIndex = 2;
            this.pb_inplane.TabStop = false;
            // 
            // pb_outplane
            // 
            this.pb_outplane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pb_outplane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_outplane.Location = new System.Drawing.Point(299, 401);
            this.pb_outplane.Name = "pb_outplane";
            this.pb_outplane.Size = new System.Drawing.Size(240, 240);
            this.pb_outplane.TabIndex = 3;
            this.pb_outplane.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbTip);
            this.groupBox1.Controls.Add(this.rbTarget);
            this.groupBox1.Controls.Add(this.rbStart);
            this.groupBox1.Location = new System.Drawing.Point(569, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 82);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Left-Click Selects";
            // 
            // rbTip
            // 
            this.rbTip.AutoSize = true;
            this.rbTip.Location = new System.Drawing.Point(212, 34);
            this.rbTip.Name = "rbTip";
            this.rbTip.Size = new System.Drawing.Size(55, 24);
            this.rbTip.TabIndex = 2;
            this.rbTip.Text = "&Tip";
            this.rbTip.UseVisualStyleBackColor = true;
            // 
            // rbTarget
            // 
            this.rbTarget.AutoSize = true;
            this.rbTarget.Location = new System.Drawing.Point(112, 32);
            this.rbTarget.Name = "rbTarget";
            this.rbTarget.Size = new System.Drawing.Size(80, 24);
            this.rbTarget.TabIndex = 1;
            this.rbTarget.Text = "Ta&rget";
            this.rbTarget.UseVisualStyleBackColor = true;
            // 
            // rbStart
            // 
            this.rbStart.AutoSize = true;
            this.rbStart.Checked = true;
            this.rbStart.Location = new System.Drawing.Point(22, 32);
            this.rbStart.Name = "rbStart";
            this.rbStart.Size = new System.Drawing.Size(69, 24);
            this.rbStart.TabIndex = 0;
            this.rbStart.TabStop = true;
            this.rbStart.Text = "&Start";
            this.rbStart.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(183, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "NeedlePath";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label2.Location = new System.Drawing.Point(142, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "by Phillip Cheng, MD MS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Markers
            // 
            this.btn_Markers.Location = new System.Drawing.Point(893, 34);
            this.btn_Markers.Name = "btn_Markers";
            this.btn_Markers.Size = new System.Drawing.Size(188, 37);
            this.btn_Markers.TabIndex = 7;
            this.btn_Markers.Text = "Hide Markers";
            this.btn_Markers.UseVisualStyleBackColor = true;
            this.btn_Markers.Click += new System.EventHandler(this.btn_Markers_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // labelz
            // 
            this.labelz.AutoSize = true;
            this.labelz.Location = new System.Drawing.Point(893, 91);
            this.labelz.Name = "labelz";
            this.labelz.Size = new System.Drawing.Size(0, 20);
            this.labelz.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 649);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(102, 649);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "in-plane angle";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 649);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "out-of-plane angle";
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(229, 73);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(44, 20);
            this.labelVersion.TabIndex = 11;
            this.labelVersion.Text = "Build";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelVersion, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(36, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(503, 99);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(36, 123);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(503, 258);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 674);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelz);
            this.Controls.Add(this.btn_Markers);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pb_outplane);
            this.Controls.Add(this.pb_inplane);
            this.Controls.Add(this.pb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1135, 730);
            this.Name = "MainForm";
            this.Text = "NeedlePath";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_inplane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_outplane)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.PictureBox pb_inplane;
        private System.Windows.Forms.PictureBox pb_outplane;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbTip;
        private System.Windows.Forms.RadioButton rbTarget;
        private System.Windows.Forms.RadioButton rbStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Markers;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelz;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

