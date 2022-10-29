using FellowOakDicom;
using FellowOakDicom.Imaging;
using Synapse5Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Web;
using static System.Windows.Forms.AxHost;


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
        const int radius = 5;
        const int thick = 2;
        const double x_mouse_scale = 1;
        const double y_mouse_scale = 1;
        int anchor_width, anchor_center;
        Point anchor;
        int center, width = 0;
        bool leftMouseDown;

        double start_x, start_y, start_z=0;
        double target_x, target_y, target_z=0;
        double tip_x, tip_y, tip_z;

        public MainForm()
        {
            InitializeComponent();

            new DicomSetupBuilder().RegisterServices(s => s.AddFellowOakDicom().AddImageManager<WinFormsImageManager>()).Build();
            pb.MouseWheel+= new MouseEventHandler(this.pb_MouseWheel);
            pb.AllowDrop = true;
            syn = new Synapse5();

        }

        private void load_dicom()
        {
            dcmfile = DicomFile.Open(dcmfiles[dcmidx]);
            dcmimage = new DicomImage(dcmfile.Dataset);
            repaint_bg_image();
            repaint();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad0:
                    center = 400;
                    width = 2000;
                    repaint_bg_image();
                    break;
                case Keys.NumPad1:
                    center = 55;
                    width = 426;
                    repaint_bg_image();
                    break;
                case Keys.NumPad2:
                    center = -585;
                    width = 1800;
                    repaint_bg_image();
                    break;
            }
        }


        private void set_point(Point p)
        {
            double px, py, pz;

            double x_pix = dcmfile.Dataset.GetValue<double>(DicomTag.PixelSpacing, 0);
            double y_pix = dcmfile.Dataset.GetValue<double>(DicomTag.PixelSpacing, 1);
            double rows = dcmfile.Dataset.GetValue<int>(DicomTag.Rows, 0);
            double cols = dcmfile.Dataset.GetValue<int>(DicomTag.Columns, 0);

            px = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 0);
            px += (float) p.X / pb.Width * cols * x_pix;
            py = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 1);
            py += (float) p.Y / pb.Height * rows * y_pix;
            pz = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 2);

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

            repaint();
        }

        private void repaint()
        {
            double x_pos = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 0);
            double y_pos = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 1);
            double z_pos = dcmfile.Dataset.GetValue<double>(DicomTag.ImagePositionPatient, 2);
            double x_pix = dcmfile.Dataset.GetValue<double>(DicomTag.PixelSpacing, 0);
            double y_pix = dcmfile.Dataset.GetValue<double>(DicomTag.PixelSpacing, 1);
            double rows = dcmfile.Dataset.GetValue<int>(DicomTag.Rows, 0);
            double cols = dcmfile.Dataset.GetValue<int>(DicomTag.Columns, 0);

            double form_pix_x = cols * x_pix / pb.Width;
            double form_pix_y = cols * y_pix / pb.Height;
            bmp = new Bitmap(pb.Width, pb.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.Transparent, new Rectangle(Point.Empty, bmp.Size));
                Point p = new Point();
                if (start_z == z_pos)
                {
                    p.X = (int)((start_x - x_pos) / form_pix_x);
                    p.Y = (int)((start_y - y_pos) / form_pix_y);
                    g.DrawEllipse(new Pen(Color.Red, thick), p.X - radius, p.Y - radius, 2 * radius, 2 * radius);
                }
                if (target_z == z_pos)
                {
                    p.X = (int)((target_x - x_pos) / form_pix_x);
                    p.Y = (int)((target_y - y_pos) / form_pix_y);
                    g.DrawEllipse(new Pen(Color.Green, thick), p.X - radius, p.Y - radius, 2 * radius, 2 * radius);
                }
                if (tip_z == z_pos)
                {
                    p.X = (int)((tip_x - x_pos) / form_pix_x);
                    p.Y = (int)((tip_y - y_pos) / form_pix_y);
                    g.DrawEllipse(new Pen(Color.Yellow, thick), p.X - radius, p.Y - radius, 2 * radius, 2 * radius);
                }
                if (pb.Image != null) pb.Image.Dispose();
                pb.Image = bmp;
            }

            if (start_x != 0)
            {
                bmp = new Bitmap(pb_inplane.Width, pb_inplane.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(Point.Empty, bmp.Size));
                    g.DrawLine(new Pen(Color.White, thick), pb_inplane.Width / 2, 0, pb_inplane.Width / 2, pb_inplane.Height);
                    g.DrawLine(new Pen(Color.White, thick), 0, pb_inplane.Height / 2, pb_inplane.Width, pb_inplane.Height / 2);

                    if (target_x != 0)
                    {

                        double raw_inplane = Math.Atan2(target_y - start_y, target_x - start_x);

                        g.DrawLine(new Pen(Color.Red, thick), pb_inplane.Width / 2, pb_inplane.Height / 2,
                            (int)(pb_inplane.Width * 0.5 * (1 - Math.Cos(raw_inplane))),
                            (int)(pb_inplane.Height * 0.5 * (1 - Math.Sin(raw_inplane))));
                    }

                    if (tip_x != 0)
                    {
                        double raw_inplane = Math.Atan2((tip_y - start_y), (tip_x - start_x));
                        g.DrawLine(new Pen(Color.Yellow, thick), pb_inplane.Width / 2, pb_inplane.Height / 2,
                            (int)(pb_inplane.Width * 0.5 * (1 - Math.Cos(raw_inplane))),
                            (int)(pb_inplane.Height * 0.5 * (1 - Math.Sin(raw_inplane))));

                    }
                }
                if (pb_inplane.Image != null) pb_inplane.Image.Dispose();
                pb_inplane.Image = bmp;

                bmp = new Bitmap(pb_outplane.Width, pb_outplane.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(Point.Empty, bmp.Size));
                    g.DrawLine(new Pen(Color.White, thick), pb_outplane.Width / 2, 0, pb_outplane.Width / 2, pb_outplane.Height / 2);
                    g.DrawLine(new Pen(Color.White, thick), 0, pb_outplane.Height / 2, pb_outplane.Width, pb_outplane.Height / 2);

                    if (target_x != 0)
                    {
                        double distance_entry_target = Math.Sqrt((target_x-start_x) *(target_x-start_x) + (target_y-start_y)*(target_y-start_y) + (target_z-start_z)*(target_z-start_z));
                        double entry_outplane = Math.Asin((target_z-start_z) / distance_entry_target);
                        g.DrawLine(new Pen(Color.Red, thick), pb_outplane.Width / 2, pb_outplane.Height / 2,
                            (int)(pb_outplane.Width * 0.5 * (1 - Math.Sin(entry_outplane))),
                            (int)(pb_outplane.Height * 0.5 * (1 - Math.Cos(entry_outplane))));
                    }

                    if (tip_x != 0)
                    {
                        double distance_entry_tip = Math.Sqrt((tip_x - start_x) * (tip_x - start_x) + (tip_y - start_y) * (tip_y - start_y) + (tip_z - start_z) * (tip_z - start_z));
                        double tip_outplane = Math.Asin((tip_z - start_z) / distance_entry_tip);
                        g.DrawLine(new Pen(Color.Yellow, thick), pb_outplane.Width / 2, pb_outplane.Height / 2,
                            (int)(pb_outplane.Width * 0.5 * (1 - Math.Sin(tip_outplane))),
                            (int)(pb_outplane.Height * 0.5 * (1 - Math.Cos(tip_outplane))));
                    }

                }
                if (pb_outplane.Image != null) pb_outplane.Image.Dispose();
                pb_outplane.Image = bmp;

            }

        }



        private void pb_MouseWheel(object sender, MouseEventArgs e)
        {
            int oldidx = dcmidx;
            if (e.Delta > 0) dcmidx--;
            if (e.Delta < 0) dcmidx++;
            if (dcmidx < 0) dcmidx = 0;
            if (dcmidx >= dcmfiles.Count) dcmidx = dcmfiles.Count - 1;
            if (dcmidx != oldidx) load_dicom();
        }

        private void repaint_bg_image()
        {
            if (dcmimage!=null)
            {
                if (width>0)
                {
                    dcmimage.WindowCenter = center;
                    dcmimage.WindowWidth = width;
                } else
                {
                    center = (int)dcmimage.WindowCenter;
                    width = (int)dcmimage.WindowWidth;
                }
                bmp = new Bitmap(dcmimage.RenderImage().AsSharedBitmap());
                if (pb.BackgroundImage!=null) pb.BackgroundImage.Dispose();
                pb.BackgroundImage = bmp;
            }
        }

        private void textBoxLine(string line)
        {
            textBox1.Text += line + "\r\n";
            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData("UniformResourceLocator") is MemoryStream ms)
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
            if (e.Data.GetDataPresent("Text"))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (dcmimage != null)
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
                width = anchor_width - (int)(x_mouse_scale * dx);
                center = anchor_center + (int)(y_mouse_scale * dy);
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

        private void pb_DragDrop(object sender, DragEventArgs e)
        {
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
                    textBox1.Text = "";
                    //textBoxLine($"studyUID = {studyUID}");
                    //textBoxLine($"seriesUID = {seriesUID}");
                    //textBoxLine($"objectUID = {objectUID}");
                    Uri site = new Uri(datasource);
                    string url = site.GetLeftPart(UriPartial.Authority);
                    if (!syn.SetAccessToken(url))
                    {
                        syn.SetAccessTokenEnvironment(url);
                    }
                    dynamic result = syn.GetStudyDetails(studyUID);

                    string tempPath = Path.GetTempPath();
                    dcmfiles = new List<string>();

                    foreach (dynamic s in result.series)
                    {
                        if (s.iuid==seriesUID) {
                            foreach (dynamic t in s.images)
                            {
                                string imageUID = (string)t.iuid;
                                string imageFile = Path.Combine(tempPath, imageUID + ".dcm");
                                dcmfiles.Add(imageFile);
                                syn.GetDicom(studyUID, seriesUID, imageUID, imageFile);
                                if (imageUID == objectUID)
                                {
                                    dcmidx = dcmfiles.Count - 1;
                                }
                            }
                        }
                    }
                    load_dicom();
                }
            }
        }
    }
}
