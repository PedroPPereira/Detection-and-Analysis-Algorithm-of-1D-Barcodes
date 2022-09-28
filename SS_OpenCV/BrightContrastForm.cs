using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class BrightContrastForm : Form
    {
        public BrightContrastForm()
        {
            InitializeComponent();
            labelBrightValue.Text = "0";
            labelContrastValue.Text = "0";
            this.Text = "Bright and Contrast Values";
        }
        public string LabelTextBright
        {
            get { return labelBrightValue.Text; }
            set { labelContrastValue.Text = value; }
        }
        public string LabelTextContrast
        {
            get { return labelContrastValue.Text; }
            set { labelContrastValue.Text = value; }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BrightContrastForm_Load(object sender, EventArgs e)
        {

        }



        private void trackBarBright_Scroll(object sender, EventArgs e)
        {
            labelBrightValue.Text = trackBarBright.Value.ToString();
        }

        private void trackBarContrast_Scroll(object sender, EventArgs e)
        {
            //float value = trackBarContrast.Value / 100;
            labelContrastValue.Text = (trackBarContrast.Value/100.0).ToString();
        }

        private void trackBarBright_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackBarContrast_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
