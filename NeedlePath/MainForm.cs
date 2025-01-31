﻿using FellowOakDicom;
using FellowOakDicom.Imaging;
using Synapse5Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Web;


namespace NeedlePath
{
    public partial class MainForm : Form
    {
        Synapse5 syn;
        List<string> dcmfiles;
        int dcmidx = 0;
        DicomFile dcmfile;
        DicomImage dcmimage;
        Bitmap bmp;
        float thick;
        const float THICK_NEEDLE = 2;       // thickness of needle in mm
        const float THICK_MIN = 2;          // minimum thickness in pixels
        const float RADIUS_FACTOR = 2.5F;   // ratio of radius to line thickness
        const float THICK_ANGLE = 2;        // thickness of lines drawn on angle diagrams
        const double EXTEND_TIP = 10;       // factor to extend needle trajectory
        const double X_MOUSE_SCALE = 1;     // scaling of width change to mouse movement
        const double Y_MOUSE_SCALE = 1;     // scaling of center change to mouse movement
        readonly Color COLOR_START = Color.FromArgb(255, 127, 127);
        readonly Color COLOR_TARGET = Color.FromArgb(127, 255, 127);
        readonly Color COLOR_TIP = Color.FromArgb(255, 255, 127);


        int anchor_width, anchor_center;
        Point anchor;
        int center = 0, width = 0;
        bool leftMouseDown;
        bool showMarkers = true;
        bool downloaded = false;

        const double EPSILON = 1e-5;        // avoid divide by zero error when distance is zero
        double start_x = 0, start_y = 0, start_z = 0;
        double target_x = 0, target_y = 0, target_z = 0;
        double tip_x = 0, tip_y = 0, tip_z = 0;

        public MainForm()
        {
            InitializeComponent();

            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            DateTime compileDate = new DateTime(2000, 1, 1).Add(new TimeSpan(v.Build * TimeSpan.TicksPerDay + v.Revision * TimeSpan.TicksPerSecond * 2));
            this.labelVersion.Text = $"Build {compileDate}";

            new DicomSetupBuilder().RegisterServices(s => s.AddFellowOakDicom().AddImageManager<WinFormsImageManager>()).Build();
            pb.MouseWheel += new MouseEventHandler(this.pb_MouseWheel);
            syn = new Synapse5();

            DialogResult result = MessageBox.Show(this, "This software is for educational purposes. " +
                "It has not been validated for clinical use.  Do you want to continue?",
                "NeedlePath - Disclaimer", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                Load += (s, e) => Close();
            }
        }

        private void load_dicom()
        {
            try
            {
                dcmfile = DicomFile.Open(dcmfiles[dcmidx]);
                dcmimage = new DicomImage(dcmfile.Dataset);
                repaint_bg_image();
                repaint();
            }
            catch (Exception e)
            {
                textBoxLine(e.Message);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (dcmfile != null)
            {
                switch (keyData)
                {
                    case Keys.Home:
                        dcmidx = 0;
                        load_dicom();
                        return true;
                    case Keys.End:
                        dcmidx = dcmfiles.Count - 1;
                        load_dicom();
                        return true;
                    case Keys.Down:
                        dcmidx += 1;
                        if (dcmidx >= dcmfiles.Count)
                        {
                            dcmidx = dcmfiles.Count - 1;
                        }
                        else
                        {
                            load_dicom();
                        }
                        return true;
                    case Keys.Up:
                        dcmidx -= 1;
                        if (dcmidx < 0)
                        {
                            dcmidx = 0;
                        }
                        else
                        {
                            load_dicom();
                        }
                        return true;
                    case Keys.PageDown:
                        dcmidx += 10;
                        if (dcmidx >= dcmfiles.Count)
                        {
                            dcmidx = dcmfiles.Count - 1;
                        }
                        load_dicom();
                        return true;
                    case Keys.PageUp:
                        dcmidx -= 10;
                        if (dcmidx < 0)
                        {
                            dcmidx = 0;
                        }
                        load_dicom();
                        return true;
                    case Keys.D0:
                    case Keys.NumPad0:
                        center = 400;
                        width = 2000;
                        repaint_bg_image();
                        return true;
                    case Keys.D1:
                    case Keys.NumPad1:
                        center = 55;
                        width = 426;
                        repaint_bg_image();
                        return true;
                    case Keys.D2:
                    case Keys.NumPad2:
                        center = -585;
                        width = 1800;
                        repaint_bg_image();
                        return true;
                    case Keys.D3:
                    case Keys.NumPad3:
                        center = 50;
                        width = 150;
                        repaint_bg_image();
                        return true;
                    case Keys.D4:
                    case Keys.NumPad4:
                        center = 100;
                        width = 300;
                        repaint_bg_image();
                        return true;
                    case Keys.D9:
                    case Keys.NumPad9:
                        if (dcmfile != null)
                        {
                            center = dcmfile.Dataset.GetValueOrDefault<int>(DicomTag.WindowCenter, 0, center);
                            width = dcmfile.Dataset.GetValueOrDefault<int>(DicomTag.WindowWidth, 0, width);
                            repaint_bg_image();
                            return true;
                        }
                        break;
                    case Keys.S:
                        rbStart.Checked = true;
                        return true;
                    case Keys.R:
                        rbTarget.Checked = true;
                        return true;
                    case Keys.T:
                        rbTip.Checked = true;
                        return true;
                    case Keys.A:
                        toggle_button();
                        repaint();
                        return true;
                    case Keys.C:
                        reset_markers();
                        repaint();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void set_point(Point p)
        {
            double px, py, pz, x_pix, y_pix, rows, cols;

            try
            {
                x_pix = dcmfile.Dataset.GetValue<double>(DicomTag.PixelSpacing, 0);
                y_pix = dcmfile.Dataset.GetValue<double>(DicomTag.PixelSpacing, 1);
                rows = dcmfile.Dataset.GetValue<int>(DicomTag.Rows, 0);
                cols = dcmfile.Dataset.GetValue<int>(DicomTag.Columns, 0);
                px = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 0);
                py = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 1);
                pz = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 2);
            }
            catch (Exception e)
            {
                textBoxLine(e.Message);
                return;
            }

            int min_dim = 0;
            if (pb.Width < pb.Height)
            {
                p.Y = p.Y - (pb.Height - pb.Width) / 2;
                min_dim = pb.Width;
            }
            else
            {
                p.X = p.X - (pb.Width - pb.Height) / 2;
                min_dim = pb.Height;
            }

            px += (float)p.X / min_dim * cols * x_pix;
            py += (float)p.Y / min_dim * rows * y_pix;

            if (rbStart.Checked)
            {
                start_x = px;
                start_y = py;
                start_z = pz;
            }
            if (rbTarget.Checked)
            {
                target_x = px;
                target_y = py;
                target_z = pz;
            }
            if (rbTip.Checked)
            {
                tip_x = px;
                tip_y = py;
                tip_z = pz;
            }
            if (!showMarkers) toggle_button();

            repaint();
        }

        public static double Clamp(double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        private void drawLineSegment(Graphics g, double x1, double y1, double z1, double x2, double y2, double z2, Color c)
        {
            // Draw a line segment depicting intersection of a line from (x1,y1,z1) to (x2,y2,z2) with the plane of the current image
            double x_pos, y_pos, z_pos, x_pix, y_pix, z_pix, rows, cols;
            try
            {
                x_pos = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 0);
                y_pos = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 1);
                z_pos = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 2);
                x_pix = dcmfile.Dataset.GetValue<double>(DicomTag.PixelSpacing, 0);
                y_pix = dcmfile.Dataset.GetValue<double>(DicomTag.PixelSpacing, 1);
                z_pix = dcmfile.Dataset.GetValue<double>(DicomTag.SliceThickness, 0);
                rows = dcmfile.Dataset.GetValue<int>(DicomTag.Rows, 0);
                cols = dcmfile.Dataset.GetValue<int>(DicomTag.Columns, 0);
            }
            catch (Exception e)
            {
                textBoxLine(e.Message);
                return;
            }

            int min_dim = 0;
            int buffer_x = 0, buffer_y = 0;
            if (pb.Width < pb.Height)
            {
                buffer_y = (pb.Height - pb.Width) / 2;
                min_dim = pb.Width;
            }
            else
            {
                buffer_x = (pb.Width - pb.Height) / 2;
                min_dim = pb.Height;
            }
            double form_pix_x = cols * x_pix / min_dim;
            double form_pix_y = rows * y_pix / min_dim;

            double start, end;

            if ((z_pos > Math.Max(z1, z2)) || (z_pos < Math.Min(z1, z2))) return;

            double delta_z = z2 - z1;
            if (delta_z == 0)
            {
                start = 0;
                end = 1;
            }
            else
            {
                start = Clamp((z_pos - z1 - z_pix) / delta_z, 0, 1);
                end = Clamp((z_pos - z1 + z_pix) / delta_z, 0, 1);
            }
            double x1_pix = (x1 - x_pos) / form_pix_x + buffer_x;
            double y1_pix = (y1 - y_pos) / form_pix_y + buffer_y;
            double x2_pix = (x2 - x_pos) / form_pix_x + buffer_x;
            double y2_pix = (y2 - y_pos) / form_pix_y + buffer_y;
            Point start_segment = new Point();
            Point end_segment = new Point();
            start_segment.X = (int)(x1_pix + start * (x2_pix - x1_pix));
            start_segment.Y = (int)(y1_pix + start * (y2_pix - y1_pix));
            end_segment.X = (int)(x1_pix + end * (x2_pix - x1_pix));
            end_segment.Y = (int)(y1_pix + end * (y2_pix - y1_pix));
            g.DrawLine(new Pen(c, thick), start_segment, end_segment);
        }

        private double in_plane_print(double angle)
        {
            angle = angle * 180 / Math.PI;
            if (angle > 90) angle = 180 - angle;
            if (angle < -90) angle = -180 - angle;
            return angle;
        }

        private double in_plane_difference(double angle1, double angle2)
        {
            double difference = Math.Abs(angle1 - angle2) * 180 / Math.PI;
            if (difference > 180) difference = 360 - difference;
            return difference;
        }
        private void repaint()
        {
            if (dcmfile == null) return;
            if (this.WindowState == FormWindowState.Minimized) return;
            richTextBox1.Text = "";

            double x_pos, y_pos, z_pos, x_pix, y_pix, rows, cols;

            try
            {
                x_pos = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 0);
                y_pos = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 1);
                z_pos = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 2);
                x_pix = dcmfile.Dataset.GetValue<double>(DicomTag.PixelSpacing, 0);
                y_pix = dcmfile.Dataset.GetValue<double>(DicomTag.PixelSpacing, 1);
                rows = dcmfile.Dataset.GetValue<int>(DicomTag.Rows, 0);
                cols = dcmfile.Dataset.GetValue<int>(DicomTag.Columns, 0);
            }
            catch (Exception e)
            {
                textBoxLine(e.Message);
                return;
            }

            int min_dim = 0;
            int buffer_x = 0, buffer_y = 0;
            if (pb.Width < pb.Height)
            {
                buffer_y = (pb.Height - pb.Width) / 2;
                min_dim = pb.Width;
            }
            else
            {
                buffer_x = (pb.Width - pb.Height) / 2;
                min_dim = pb.Height;
            }
            double form_pix_x = cols * x_pix / min_dim;
            double form_pix_y = rows * y_pix / min_dim;



            thick = THICK_NEEDLE / ((float)(form_pix_x + form_pix_y) / 2);
            if (thick < THICK_MIN) thick = THICK_MIN;
            float radius = RADIUS_FACTOR * thick;

            bmp = new Bitmap(pb.Width, pb.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillRectangle(Brushes.Transparent, new Rectangle(Point.Empty, bmp.Size));
                if (showMarkers)
                {
                    Point p = new Point();
                    if (start_z == z_pos)
                    {
                        p.X = (int)((start_x - x_pos) / form_pix_x) + buffer_x;
                        p.Y = (int)((start_y - y_pos) / form_pix_y) + buffer_y;
                        g.DrawEllipse(new Pen(COLOR_START, thick), p.X - radius, p.Y - radius, 2 * radius, 2 * radius);
                    }
                    if (target_z == z_pos)
                    {
                        p.X = (int)((target_x - x_pos) / form_pix_x) + buffer_x;
                        p.Y = (int)((target_y - y_pos) / form_pix_y) + buffer_y;
                        g.DrawEllipse(new Pen(COLOR_TARGET, thick), p.X - radius, p.Y - radius, 2 * radius, 2 * radius);
                    }
                    if (tip_z == z_pos)
                    {
                        p.X = (int)((tip_x - x_pos) / form_pix_x) + buffer_x;
                        p.Y = (int)((tip_y - y_pos) / form_pix_y) + buffer_y;
                        g.DrawEllipse(new Pen(COLOR_TIP, thick), p.X - radius, p.Y - radius, 2 * radius, 2 * radius);
                    }

                    if ((start_x != 0) && (target_x != 0)) drawLineSegment(g, start_x, start_y, start_z, target_x, target_y, target_z, COLOR_START);
                    if ((target_x != 0) && (tip_x != 0)) drawLineSegment(g, tip_x, tip_y, tip_z, target_x, target_y, target_z, COLOR_TARGET);
                    if ((start_x != 0) && (tip_x != 0))
                    {
                        double extend_x = start_x + (tip_x - start_x) * EXTEND_TIP;
                        double extend_y = start_y + (tip_y - start_y) * EXTEND_TIP;
                        double extend_z = start_z + (tip_z - start_z) * EXTEND_TIP;
                        drawLineSegment(g, start_x, start_y, start_z, extend_x, extend_y, extend_z, COLOR_TIP);
                    }
                }

                if (pb.Image != null) pb.Image.Dispose();
                pb.Image = bmp;
            }

            if (start_x != 0)
            {
                double target_inplane = 0;
                double target_outplane = 0;
                double tip_inplane = 0;
                double tip_outplane = 0;

                if (target_x != 0)
                {
                    double distance_start_target = Math.Sqrt((target_x - start_x) * (target_x - start_x) + (target_y - start_y) * (target_y - start_y) + (target_z - start_z) * (target_z - start_z));
                    target_inplane = Math.Atan2(target_y - start_y, target_x - start_x);
                    target_outplane = Math.Asin((target_z - start_z) / (distance_start_target + EPSILON));
                    textBoxLine($"Start to target = {distance_start_target:0} mm", COLOR_START);
                    textBoxLine($"In-plane angle = {in_plane_print(target_inplane):0}°", COLOR_START);
                    textBoxLine($"Out-of-plane angle = {target_outplane * 180 / Math.PI:0}°", COLOR_START);
                    textBoxLine("");
                }
                if (tip_x != 0)
                {
                    double distance_start_tip = Math.Sqrt((tip_x - start_x) * (tip_x - start_x) + (tip_y - start_y) * (tip_y - start_y) + (tip_z - start_z) * (tip_z - start_z));
                    tip_inplane = Math.Atan2(tip_y - start_y, tip_x - start_x);
                    tip_outplane = Math.Asin((tip_z - start_z) / (distance_start_tip + EPSILON));


                    textBoxLine($"Start to tip = {distance_start_tip:0} mm", COLOR_TIP);

                    string output_text = $"In-plane angle = {in_plane_print(tip_inplane):0}°";
                    if (target_x != 0) output_text += $", correction = {in_plane_difference(target_inplane, tip_inplane):0}°";
                    textBoxLine(output_text, COLOR_TIP);

                    output_text = $"Out-of-plane angle = {tip_outplane * 180 / Math.PI:0}°";
                    if (target_x != 0) output_text += $", correction = {(target_outplane - tip_outplane) * 180 / Math.PI:0}°";
                    textBoxLine(output_text, COLOR_TIP);
                    textBoxLine("");

                    if (target_x != 0)
                    {
                        double distance_tip_target = Math.Sqrt((tip_x - target_x) * (tip_x - target_x) + (tip_y - target_y) * (tip_y - target_y) + (tip_z - target_z) * (tip_z - target_z));
                        textBoxLine($"Tip to target = {distance_tip_target:0} mm", COLOR_TARGET);
                    }
                }

                if ((target_x != 0) || (tip_x != 0))
                {
                    bmp = new Bitmap(pb_inplane.Width, pb_inplane.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.FillRectangle(Brushes.Black, new Rectangle(Point.Empty, bmp.Size));
                        g.DrawLine(new Pen(Color.White, THICK_ANGLE), pb_inplane.Width / 2, 0, pb_inplane.Width / 2, pb_inplane.Height);
                        g.DrawLine(new Pen(Color.White, THICK_ANGLE), 0, pb_inplane.Height / 2, pb_inplane.Width, pb_inplane.Height / 2);

                        if (target_x != 0)
                        {
                            g.DrawLine(new Pen(COLOR_START, THICK_ANGLE), pb_inplane.Width / 2, pb_inplane.Height / 2,
                                (int)(pb_inplane.Width * 0.5 * (1 - Math.Cos(target_inplane))),
                                (int)(pb_inplane.Height * 0.5 * (1 - Math.Sin(target_inplane))));
                        }
                        if (tip_x != 0)
                        {
                            g.DrawLine(new Pen(COLOR_TIP, THICK_ANGLE), pb_inplane.Width / 2, pb_inplane.Height / 2,
                                (int)(pb_inplane.Width * 0.5 * (1 - Math.Cos(tip_inplane))),
                                (int)(pb_inplane.Height * 0.5 * (1 - Math.Sin(tip_inplane))));

                        }
                    }
                    if (pb_inplane.Image != null) pb_inplane.Image.Dispose();
                    pb_inplane.Image = bmp;

                    bmp = new Bitmap(pb_outplane.Width, pb_outplane.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.FillRectangle(Brushes.Black, new Rectangle(Point.Empty, bmp.Size));
                        g.DrawLine(new Pen(Color.White, THICK_ANGLE), pb_outplane.Width / 2, 0, pb_outplane.Width / 2, pb_outplane.Height / 2);
                        g.DrawLine(new Pen(Color.White, THICK_ANGLE), 0, pb_outplane.Height / 2, pb_outplane.Width, pb_outplane.Height / 2);

                        if (target_x != 0)
                        {
                            g.DrawLine(new Pen(COLOR_START, THICK_ANGLE), pb_outplane.Width / 2, pb_outplane.Height / 2,
                                (int)(pb_outplane.Width * 0.5 * (1 + Math.Sin(target_outplane))),
                                (int)(pb_outplane.Height * 0.5 * (1 - Math.Cos(target_outplane))));
                        }

                        if (tip_x != 0)
                        {
                            g.DrawLine(new Pen(COLOR_TIP, THICK_ANGLE), pb_outplane.Width / 2, pb_outplane.Height / 2,
                                (int)(pb_outplane.Width * 0.5 * (1 + Math.Sin(tip_outplane))),
                                (int)(pb_outplane.Height * 0.5 * (1 - Math.Cos(tip_outplane))));
                        }

                    }
                    if (pb_outplane.Image != null) pb_outplane.Image.Dispose();
                    pb_outplane.Image = bmp;
                }
            }
            else
            {
                bmp = new Bitmap(pb_inplane.Width, pb_inplane.Height);
                if (pb_inplane.Image != null) pb_inplane.Image.Dispose();
                pb_inplane.Image = bmp;

                bmp = new Bitmap(pb_outplane.Width, pb_outplane.Height);
                if (pb_outplane.Image != null) pb_outplane.Image.Dispose();
                pb_outplane.Image = bmp;
            }

        }

        private void toggle_button()
        {
            showMarkers = !showMarkers;
            if (showMarkers)
            {
                btn_Markers_Hide.Text = "Hide Markers";
            }
            else
            {
                btn_Markers_Hide.Text = "Show Markers";
            }
        }

        private void btn_Markers_Hide_Click(object sender, EventArgs e)
        {
            toggle_button();
            repaint();
        }

        private void reset_markers()
        {
            start_x = 0; start_y = 0; start_z = 0;
            target_x = 0; target_y = 0; target_z = 0;
            tip_x = 0; tip_y = 0; tip_z = 0;
        }
        private void btn_Markers_Clear_Click(object sender, EventArgs e)
        {
            reset_markers();
            repaint();
        }

        private void pb_MouseWheel(object sender, MouseEventArgs e)
        {
            if (dcmfile == null) return;
            int oldidx = dcmidx;
            if (e.Delta > 0) dcmidx--;
            if (e.Delta < 0) dcmidx++;
            if (dcmidx < 0) dcmidx = 0;
            if (dcmidx >= dcmfiles.Count) dcmidx = dcmfiles.Count - 1;
            if (dcmidx != oldidx) load_dicom();
        }

        private void repaint_bg_image()
        {
            if (dcmfile != null)
            {
                if (width > 0)
                {
                    dcmimage.WindowCenter = center;
                    dcmimage.WindowWidth = width;
                }
                else
                {
                    center = (int)dcmimage.WindowCenter;
                    width = (int)dcmimage.WindowWidth;
                }
                bmp = new Bitmap(dcmimage.RenderImage().AsSharedBitmap());
                if (pb.BackgroundImage != null) pb.BackgroundImage.Dispose();
                pb.BackgroundImage = bmp;


                try
                {
                    //double z_pos = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 2);
                    //labelz.Text = $"SL:{sl} Z:{z_pos} ({dcmidx + 1}/{dcmfiles.Count})";
                    double sl = dcmfile.Dataset.GetValue<double>(DicomTag.SliceLocation, 0);
                    label_zPosition.Text = $"SL:{sl} ({dcmidx + 1}/{dcmfiles.Count})";
                }
                catch
                {
                }
            }
        }


        private void textBoxLine(string text, Color? color = null)
        {
            text += Environment.NewLine;
            richTextBox1.BackColor = Color.Black;
            richTextBox1.SelectionColor = color ?? richTextBox1.ForeColor;
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.AppendText(text);
            richTextBox1.ScrollToCaret();
            richTextBox1.SelectionColor = richTextBox1.ForeColor;
        }

        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (dcmfile != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    anchor = e.Location;
                    anchor_width = width;
                    anchor_center = center;
                }
                else
                {
                    set_point(e.Location);
                    leftMouseDown = true;
                }
            }

        }

        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int dx = e.X - anchor.X;
                int dy = e.Y - anchor.Y;
                width = anchor_width - (int)(X_MOUSE_SCALE * dx);
                center = anchor_center + (int)(Y_MOUSE_SCALE * dy);
                repaint_bg_image();
            }
            else if (leftMouseDown)
            {
                set_point(e.Location);
            }

        }


        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                anchor = Point.Empty;
            }
            else if (leftMouseDown)
            {
                leftMouseDown = false;
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData("UniformResourceLocator") is MemoryStream ms)
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
            Array data = e.Data.GetData("FileDrop") as Array;
            if ((data != null) && (data.GetValue(0) is String))
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
            if (e.Data.GetDataPresent("Text"))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            repaint();
        }



        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (backgroundWorker1.IsBusy) return;

            if (e.Data.GetDataPresent("Text"))
            {
                string datasource = (String)e.Data.GetData("Text");
                System.Collections.Specialized.NameValueCollection qscoll = HttpUtility.ParseQueryString(datasource);

                String studyUID = qscoll.Get("studyUID");
                String seriesUID = qscoll.Get("DDSeriesIUID");
                if (seriesUID == null)
                {
                    seriesUID = qscoll.Get("seriesUID");
                }
                String objectUID = qscoll.Get("objectUID");
                if (objectUID == null)
                {
                    objectUID = qscoll.Get("DDFirstSOPInstanceUID");
                }
                if ((studyUID != null) && (seriesUID != null) && (objectUID != null))
                {
                    DownloadObject dObj = new DownloadObject();
                    dObj.datasource = datasource;
                    dObj.studyUID = studyUID;
                    dObj.seriesUID = seriesUID;
                    dObj.objectUID = objectUID;

                    cleanFiles();
                    textBoxLine("Setting up image retrieval...");
                    backgroundWorker1.RunWorkerAsync(dObj);
                }
            }

            Array data = e.Data.GetData("FileDrop") as Array;
            if ((data != null) && (data.GetValue(0) is String))
            {
                string dcmdir = ((string[])data)[0];
                FileAttributes attr = File.GetAttributes(dcmdir);

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    List<string> imageFiles = Directory.GetFiles(dcmdir, "*.dcm").ToList<string>();
                    if (imageFiles.Count > 0)
                    {
                        cleanFiles();
                        downloaded = false;
                        dcmidx = 0;
                        dcmfiles = imageFiles;
                        load_dicom();
                    }
                }

            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            DownloadObject dObj = e.Argument as DownloadObject;

            Uri site = new Uri(dObj.datasource);
            string url = site.GetLeftPart(UriPartial.Authority);
            if (!syn.SetAccessToken(url))
            {
                syn.SetAccessTokenEnvironment(url);
            }
            dynamic result = syn.GetStudyDetails(dObj.studyUID);

            string tempPath = Path.GetTempPath();
            dcmfiles = new List<string>();
            downloaded = true;

            foreach (dynamic s in result.series)
            {
                if (s.iuid == dObj.seriesUID)
                {
                    ProgressObject pObj = new ProgressObject();
                    int total = s.images.Count;

                    for (int i = 0; i < total; i++)
                    {
                        string imageUID = (string)s.images[i].iuid;
                        string imageFile = Path.Combine(tempPath, imageUID + ".dcm");
                        dcmfiles.Add(imageFile);
                        syn.GetDicom(dObj.studyUID, dObj.seriesUID, imageUID, imageFile);

                        pObj.message = $"Retrieving {i + 1} of {total}";
                        backgroundWorker1.ReportProgress((int)(i * 100.0 / total), pObj);

                        if (imageUID == dObj.objectUID)
                        {
                            dcmidx = dcmfiles.Count - 1;
                        }
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            ProgressObject pObj = e.UserState as ProgressObject;
            textBoxLine(pObj.message);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                load_dicom();
            }
            else
            {
                textBoxLine("Retrieval not completed successfully.");
                textBoxLine(e.Error.Message);
                textBoxLine(e.Error.StackTrace);
                cleanFiles();
            }
        }
        class ProgressObject
        {
            public string message;
        }
        class DownloadObject
        {
            public string datasource;
            public string studyUID;
            public string seriesUID;
            public string objectUID;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            cleanFiles();
        }

        private void cleanFiles()
        {
            if (downloaded)
            {
                foreach (string fn in dcmfiles)
                {
                    try
                    {
                        File.Delete(fn);
                    }
                    catch
                    {

                    }
                }
                dcmfile = null;
                dcmimage = null;
            }
        }

    }
}
