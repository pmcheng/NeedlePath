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
            this.pb = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pb_inplane = new System.Windows.Forms.PictureBox();
            this.pb_outplane = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbTip = new System.Windows.Forms.RadioButton();
            this.rbTarget = new System.Windows.Forms.RadioButton();
            this.rbStart = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_inplane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_outplane)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb.Location = new System.Drawing.Point(602, 134);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(512, 512);
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.DragDrop += new System.Windows.Forms.DragEventHandler(this.pb_DragDrop);
            this.pb.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragEnter);
            this.pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_MouseDown);
            this.pb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_MouseMove);
            this.pb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(36, 106);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(529, 267);
            this.textBox1.TabIndex = 1;
            // 
            // pb_inplane
            // 
            this.pb_inplane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_inplane.Location = new System.Drawing.Point(36, 406);
            this.pb_inplane.Name = "pb_inplane";
            this.pb_inplane.Size = new System.Drawing.Size(240, 240);
            this.pb_inplane.TabIndex = 2;
            this.pb_inplane.TabStop = false;
            // 
            // pb_outplane
            // 
            this.pb_outplane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_outplane.Location = new System.Drawing.Point(325, 406);
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
            this.groupBox1.Location = new System.Drawing.Point(602, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(512, 82);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mouse Left Click";
            // 
            // rbTip
            // 
            this.rbTip.AutoSize = true;
            this.rbTip.Location = new System.Drawing.Point(377, 32);
            this.rbTip.Name = "rbTip";
            this.rbTip.Size = new System.Drawing.Size(55, 24);
            this.rbTip.TabIndex = 2;
            this.rbTip.Text = "Tip";
            this.rbTip.UseVisualStyleBackColor = true;
            // 
            // rbTarget
            // 
            this.rbTarget.AutoSize = true;
            this.rbTarget.Location = new System.Drawing.Point(207, 32);
            this.rbTarget.Name = "rbTarget";
            this.rbTarget.Size = new System.Drawing.Size(80, 24);
            this.rbTarget.TabIndex = 1;
            this.rbTarget.Text = "Target";
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
            this.rbStart.Text = "Start";
            this.rbStart.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(215, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "NeedlePath";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "by Phillip Cheng, MD MS";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 684);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pb_outplane);
            this.Controls.Add(this.pb_inplane);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pb);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "NeedlePath";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_inplane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_outplane)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pb_inplane;
        private System.Windows.Forms.PictureBox pb_outplane;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbTip;
        private System.Windows.Forms.RadioButton rbTarget;
        private System.Windows.Forms.RadioButton rbStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

