using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ооп6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap b;
        Graphics g;
        Point startPoint;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Задание размеров поверхности рисования
            this.b = new Bitmap(this.pictureBox2.Size.Width,
            this.pictureBox2.Size.Height);
            this.g = Graphics.FromImage(this.b);
            this.pictureBox2.Image = b;
            this.g.Clear(Color.White);
            this.FigureList.SelectedIndex = 0;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.pictureBox1.BackColor;
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.BackColor = this.colorDialog1.Color;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.g.Clear(Color.White);
                    this.g.DrawImage(Image.FromFile(this.openFileDialog1.FileName), new
                    Point(0, 0));
                    this.pictureBox2.Invalidate();
                }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.g.Clear(Color.White);
            this.pictureBox2.Invalidate();
        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.b.Save(this.saveFileDialog1.FileName,
                System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPoint = new Point(e.X, e.Y);
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            Pen newPen = new Pen(this.pictureBox1.BackColor, (float)this.numericUpDown1.Value);
            switch(this.PenList.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    newPen.DashStyle = DashStyle.Dash;
                    break;
                case 2:
                    newPen.DashStyle = DashStyle.Dot;
                    break;
                case 3:
                    newPen.DashStyle = DashStyle.DashDot;
                    break;
            }

            Brush newBrush;
            switch(this.BrushList.SelectedIndex)
            {
                case 0:
                    newBrush = new SolidBrush(this.pictureBox1.BackColor);
                    break;
                case 1:
                    newBrush = new HatchBrush(HatchStyle.Cross, this.pictureBox1.BackColor);
                    break;
                case 2:
                    newBrush = new HatchBrush(HatchStyle.DottedGrid, this.pictureBox1.BackColor);
                    break;
                case 3:
                    newBrush = new HatchBrush(HatchStyle.Wave, this.pictureBox1.BackColor);
                    break;
                default:
                    newBrush = new SolidBrush(this.pictureBox1.BackColor);
                    break;
            }


            switch(this.FigureList.SelectedIndex)
            {
                case 0:  //line
                    this.g.DrawLine(newPen, this.startPoint, new Point(e.X, e.Y));
                    this.pictureBox2.Invalidate();
                    break;

                case 1: //ellipse
                    this.g.DrawEllipse(newPen, this.startPoint.X, this.startPoint.Y, 
                        e.X - this.startPoint.X, e.Y - this.startPoint.Y);
                    if (this.FillCheckBox.Checked)
                    {
                        this.g.FillEllipse(newBrush, this.startPoint.X, this.startPoint.Y,
                        e.X - this.startPoint.X, e.Y - this.startPoint.Y);
                    }
                    this.pictureBox2.Invalidate();
                    break;

                case 2: //rectangle
                    this.g.DrawRectangle(newPen, this.startPoint.X, this.startPoint.Y,
                        e.X - this.startPoint.X, e.Y - this.startPoint.Y);
                    if (this.FillCheckBox.Checked)
                    {
                        this.g.FillRectangle(newBrush, this.startPoint.X, this.startPoint.Y,
                        e.X - this.startPoint.X, e.Y - this.startPoint.Y);
                    }
                    this.pictureBox2.Invalidate();
                    break;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }
    }
}
