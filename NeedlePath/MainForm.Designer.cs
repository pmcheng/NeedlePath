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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.btn_Markers = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label_zPosition = new System.Windows.Forms.Label();
            this.label_inplane = new System.Windows.Forms.Label();
            this.label_outplane = new System.Windows.Forms.Label();
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
            this.pb.Location = new System.Drawing.Point(349, 52);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(400, 400);
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_MouseDown);
            this.pb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_MouseMove);
            this.pb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MouseUp);
            // 
            // pb_inplane
            // 
            this.pb_inplane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_inplane.Location = new System.Drawing.Point(9, 272);
            this.pb_inplane.Name = "pb_inplane";
            this.pb_inplane.Size = new System.Drawing.Size(160, 160);
            this.pb_inplane.TabIndex = 2;
            this.pb_inplane.TabStop = false;
            // 
            // pb_outplane
            // 
            this.pb_outplane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pb_outplane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_outplane.Location = new System.Drawing.Point(175, 272);
            this.pb_outplane.Name = "pb_outplane";
            this.pb_outplane.Size = new System.Drawing.Size(160, 160);
            this.pb_outplane.TabIndex = 3;
            this.pb_outplane.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbTip);
            this.groupBox1.Controls.Add(this.rbTarget);
            this.groupBox1.Controls.Add(this.rbStart);
            this.groupBox1.Location = new System.Drawing.Point(351, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 43);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Left-click selects";
            // 
            // rbTip
            // 
            this.rbTip.AutoSize = true;
            this.rbTip.Location = new System.Drawing.Point(185, 19);
            this.rbTip.Name = "rbTip";
            this.rbTip.Size = new System.Drawing.Size(40, 17);
            this.rbTip.TabIndex = 2;
            this.rbTip.Text = "&Tip";
            this.rbTip.UseVisualStyleBackColor = true;
            // 
            // rbTarget
            // 
            this.rbTarget.AutoSize = true;
            this.rbTarget.Location = new System.Drawing.Point(97, 19);
            this.rbTarget.Name = "rbTarget";
            this.rbTarget.Size = new System.Drawing.Size(56, 17);
            this.rbTarget.TabIndex = 1;
            this.rbTarget.Text = "Ta&rget";
            this.rbTarget.UseVisualStyleBackColor = true;
            // 
            // rbStart
            // 
            this.rbStart.AutoSize = true;
            this.rbStart.Checked = true;
            this.rbStart.Location = new System.Drawing.Point(22, 19);
            this.rbStart.Name = "rbStart";
            this.rbStart.Size = new System.Drawing.Size(47, 17);
            this.rbStart.TabIndex = 0;
            this.rbStart.TabStop = true;
            this.rbStart.Text = "&Start";
            this.rbStart.UseVisualStyleBackColor = true;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(116, 2);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(94, 18);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "NeedlePath";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAuthor
            // 
            this.labelAuthor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.labelAuthor.Location = new System.Drawing.Point(86, 23);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(153, 16);
            this.labelAuthor.TabIndex = 6;
            this.labelAuthor.Text = "by Phillip Cheng, MD MS";
            this.labelAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Markers
            // 
            this.btn_Markers.Location = new System.Drawing.Point(594, 4);
            this.btn_Markers.Name = "btn_Markers";
            this.btn_Markers.Size = new System.Drawing.Size(155, 25);
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
            // label_zPosition
            // 
            this.label_zPosition.AutoSize = true;
            this.label_zPosition.Location = new System.Drawing.Point(599, 33);
            this.label_zPosition.Name = "label_zPosition";
            this.label_zPosition.Size = new System.Drawing.Size(0, 13);
            this.label_zPosition.TabIndex = 8;
            // 
            // label_inplane
            // 
            this.label_inplane.AutoSize = true;
            this.label_inplane.Location = new System.Drawing.Point(53, 437);
            this.label_inplane.Name = "label_inplane";
            this.label_inplane.Size = new System.Drawing.Size(73, 13);
            this.label_inplane.TabIndex = 9;
            this.label_inplane.Text = "in-plane angle";
            // 
            // label_outplane
            // 
            this.label_outplane.AutoSize = true;
            this.label_outplane.Location = new System.Drawing.Point(209, 437);
            this.label_outplane.Name = "label_outplane";
            this.label_outplane.Size = new System.Drawing.Size(92, 13);
            this.label_outplane.TabIndex = 10;
            this.label_outplane.Text = "out-of-plane angle";
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(100, 42);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(126, 13);
            this.labelVersion.TabIndex = 11;
            this.labelVersion.Text = "Last Build Date and Time";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelVersion, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelAuthor, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 58);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(9, 68);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(326, 197);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 461);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label_outplane);
            this.Controls.Add(this.label_inplane);
            this.Controls.Add(this.label_zPosition);
            this.Controls.Add(this.btn_Markers);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pb_outplane);
            this.Controls.Add(this.pb_inplane);
            this.Controls.Add(this.pb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(777, 500);
            this.Name = "MainForm";
            this.Text = "NeedlePath";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
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
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Button btn_Markers;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label_zPosition;
        private System.Windows.Forms.Label label_inplane;
        private System.Windows.Forms.Label label_outplane;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

