using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace histogrameLab
{
    public partial class Form1 : Form
    {
        Histogram his = new Histogram();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            his.LoadImage();
            pictureBox1.ImageLocation = "1.jpg";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            his.ConvertToGray();
            pictureBox2.ImageLocation = "2.jpg";
           // pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
           //to adjest size of the picture to the box if not done in the interface
        }

        private void button2_Click(object sender, EventArgs e)
        {
            his.DrawHistogram();
            pictureBox3.ImageLocation = "3.jpg";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            his.DrawEqualizedHistogram();
            pictureBox4.ImageLocation = "4.jpg";
            pictureBox5.ImageLocation = "5.jpg";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            his.Sobel();
            pictureBox4.ImageLocation = "sobelx.jpg";
            pictureBox5.ImageLocation = "sobely.jpg";
        }
    }
}
