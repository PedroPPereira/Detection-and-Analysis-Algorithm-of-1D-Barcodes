using System;
using System.Diagnostics;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Linq;
using System.IO;

namespace SS_OpenCV
{ 
    public partial class MainForm : Form
    {
        Image<Bgr, Byte> img = null; // working image
        Image<Bgr, Byte> imgUndo = null; // undo backup image - UNDO
        string title_bak = "";

        public MainForm()
        {
            InitializeComponent();
            customizedDesign(); // new layout
            title_bak = Text;
        }

        private void customizedDesign() // new layout
        {
            panelColorSubMenu.Visible = false;
            panelTranslationSubMenu.Visible = false;
            panelFiltersSubMenu.Visible = false;
            panelHistogramsSubMenu.Visible = false;
            panelConvertBWSubMenu.Visible = false;

        }

        private void hideSubMenu() // new layout
        {
            if (panelColorSubMenu.Visible == true)
                panelColorSubMenu.Visible = false;
            if (panelTranslationSubMenu.Visible == true)
                panelTranslationSubMenu.Visible = false;
            if (panelFiltersSubMenu.Visible == true)
                panelFiltersSubMenu.Visible = false;
            if (panelHistogramsSubMenu.Visible == true)
                panelHistogramsSubMenu.Visible = false;
            if (panelConvertBWSubMenu.Visible == true)
                panelConvertBWSubMenu.Visible = false;
        }

        private void showSubMenu(Panel SubMenu) // new layout
        {
            if (SubMenu.Visible == false)
            {
                hideSubMenu();
                SubMenu.Visible = true;
            }
            else
                SubMenu.Visible = false;
        }

        /// <summary>
        /// Opens a new image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(openFileDialog1.FileName);
                Text = title_bak + " [" +
                        openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1) +
                        "]";
                imgUndo = img.Copy();
                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh();
            }
        }

        /// <summary>
        /// Saves an image with a new name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageViewer.Image.Save(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// restore last undo copy of the working image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgUndo == null) // verify if the image is already opened
                return; 
            Cursor = Cursors.WaitCursor;
            img = imgUndo.Copy();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        /// <summary>
        /// Change visualization mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zoom
            if (autoZoomToolStripMenuItem.Checked)
            {
                ImageViewer.SizeMode = PictureBoxSizeMode.Zoom;
                ImageViewer.Dock = DockStyle.Fill;
            }
            else // with scroll bars
            {
                ImageViewer.Dock = DockStyle.None;
                ImageViewer.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        /// <summary>
        /// Show authors form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorsForm form = new AuthorsForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Calculate the image negative
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Negative(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        /// <summary>
        /// Call automated image processing check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void evalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvalForm eval = new EvalForm();
            eval.ShowDialog();
        }

        /// <summary>
        /// Call image convertion to gray scale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToGray(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void redChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.RedChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void greenChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.GreenComponent(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void blueChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.BlueComponent(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void brightContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            
            BrightContrastForm formBar = new BrightContrastForm(); formBar.ShowDialog();
            int bright = Convert.ToInt32(formBar.LabelTextBright);
            double contrast = Convert.ToDouble(formBar.LabelTextContrast);
            /*
            InputBox form = new InputBox("Bright Value;"); form.ShowDialog();
            int bright = Convert.ToInt32(form.ValueTextBox.Text);
            InputBox form2 = new InputBox("Contrast Value;"); form2.ShowDialog();
            double contrast = Convert.ToDouble(form2.ValueTextBox.Text);
            */

            Debug.WriteLine("bright=" + bright.ToString() + " / contrast=" + contrast.ToString() + "\n");
            ImageClass.BrightContrast(img, bright, contrast);
            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void translationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("X?");
            form1.ShowDialog();
            int dx = Convert.ToInt32(form1.ValueTextBox.Text);

            InputBox form2 = new InputBox("Y");
            form2.ShowDialog();
            int dy = Convert.ToInt32(form2.ValueTextBox.Text);

            ImageClass.Translation(img, imgUndo, dx, dy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void rotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("Ângulo?");
            form1.ShowDialog();
            int angle = Convert.ToInt32(form1.ValueTextBox.Text);

            ImageClass.Rotation(img, imgUndo, angle);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void scaleFactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("Fator de escala?");
            form1.ShowDialog();
            float scaleFactor = (float)Convert.ToDouble(form1.ValueTextBox.Text);

            ImageClass.Scale(img, imgUndo, scaleFactor);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        //create mouse variables
        int mouseX, mouseY;
        bool mouseFlag = false;

        private void scalePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("Fator de escala?");
            form1.ShowDialog();
            float scaleFactor = (float)Convert.ToDouble(form1.ValueTextBox.Text);

            ImageClass.Scale(img, imgUndo, scaleFactor);

            //get mouse coordinates
            mouseFlag = true;
            while (mouseFlag)
                Application.DoEvents(); //wait for mouseclick event

            ImageClass.Scale_point_xy(img, imgUndo, scaleFactor, mouseX, mouseY);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void bilinearRotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("Ângulo?");
            form1.ShowDialog();
            int angle = Convert.ToInt32(form1.ValueTextBox.Text);

            ImageClass.Rotation_Bilinear(img, imgUndo, angle);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void bilinearScaleFactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("Fator de escala?");
            form1.ShowDialog();
            float scaleFactor = (float)Convert.ToDouble(form1.ValueTextBox.Text);

            ImageClass.Scale_Bilinear(img, imgUndo, scaleFactor);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void bilinearScalePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("Fator de escala?");
            form1.ShowDialog();
            float scaleFactor = (float)Convert.ToDouble(form1.ValueTextBox.Text);

            ImageClass.Scale(img, imgUndo, scaleFactor);

            //get mouse coordinates
            mouseFlag = true;
            while (mouseFlag)
                Application.DoEvents(); //wait for mouseclick event

            ImageClass.Scale_point_xy_Bilinear(img, imgUndo, scaleFactor, mouseX, mouseY);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void ImageViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if (mouseFlag)
            {
                mouseX = e.X; //get mouse coordinates
                mouseY = e.Y;

                mouseFlag = false; //unlock while mouseFlag
            }
        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Mean(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void meanBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Mean_solutionB(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void meanCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            int size = 7;

            ImageClass.Mean_solutionC(img, imgUndo, size);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void nonUniformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            MatrixForm form1 = new MatrixForm();
            form1.ShowDialog();

            if(form1.comboBoxFilter.Text=="Mean 3x3")
            { 
                float num1 = Convert.ToSingle(form1.textBox1.Text);
                float num2 = Convert.ToSingle(form1.textBox2.Text);
                float num3 = Convert.ToSingle(form1.textBox3.Text);
                float num4 = Convert.ToSingle(form1.textBox4.Text);
                float num5 = Convert.ToSingle(form1.textBox5.Text);
                float num6 = Convert.ToSingle(form1.textBox6.Text);
                float num7 = Convert.ToSingle(form1.textBox7.Text);
                float num8 = Convert.ToSingle(form1.textBox8.Text);
                float num9 = Convert.ToSingle(form1.textBox9.Text);

                float weight = Convert.ToSingle(form1.textBox10.Text);
                float[,] matrix = new float[,] { { num1, num2, num3 }, { num4, num5, num6 }, { num7, num8, num9 } };
            
                ImageClass.NonUniform(img, imgUndo, matrix, weight);
            }

            

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

            hideSubMenu();
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Sobel(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void robertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Roberts(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void diferentiationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Diferentiation(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Median(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void grayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[] array = ImageClass.Histogram_Gray(img);

            FormHistogram form1 = new FormHistogram(array);
            form1.ShowDialog();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[,] array = ImageClass.Histogram_RGB(img);

            FormHistogram2 form1 = new FormHistogram2(array);
            form1.ShowDialog();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[,] array = ImageClass.Histogram_All(img);

            FormHistogram3 form1 = new FormHistogram3(array);
            form1.ShowDialog();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void equalizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Equalization(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void convertToBWToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[] hist = ImageClass.Histogram_Gray(img); //faz o mesmo que otsu
            int threshold = ImageClass.OTSU(hist);

            ImageClass.ConvertToBW(img, threshold);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void convertOtsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToBW_Otsu(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void barCodeReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int type = 1;

            Image<Bgr, byte> imgBC = ImageClass.BarCodeReader(img, type, out Point bc_centroid1, out Size bc_size1, out string bc_image1, out string bc_number1,
            out Point bc_centroid2, out Size bc_size2, out string bc_image2, out string bc_number2);

            ImageViewer.Image = imgBC.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            MessageBox.Show(bc_number1);

            if (bc_number2 != null) { 
                MessageBox.Show(bc_number2); 
            }

            Cursor = Cursors.Default; // normal cursor
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        ///
        /// new layout
        ///
        ////////////////////////////////////////////////////////////////////////////////////////////

        #region newlayout

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            showSubMenu(panelColorSubMenu);
        }

        private void buttonNegative_Click(object sender, EventArgs e)
        {
            //code
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Negative(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

            hideSubMenu();
        } 
        
        private void buttonGray_Click(object sender, EventArgs e)
        {
            //code
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToGray(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

            hideSubMenu();
        }

        private void buttonRedChannel_Click(object sender, EventArgs e)
        {
            //code
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.RedChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

            hideSubMenu();
        }

        private void buttonBrightContrast_Click(object sender, EventArgs e)
        {
            //code
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();


            BrightContrastForm formBar = new BrightContrastForm(); formBar.ShowDialog();
            int bright = Convert.ToInt32(formBar.LabelTextBright);
            double contrast = Convert.ToDouble(formBar.LabelTextContrast);
            /*
            InputBox form = new InputBox("Bright Value;"); form.ShowDialog();
            int bright = Convert.ToInt32(form.ValueTextBox.Text);
            InputBox form2 = new InputBox("Contrast Value;"); form2.ShowDialog();
            double contrast = Convert.ToDouble(form2.ValueTextBox.Text);
            */

            Debug.WriteLine("bright=" + bright.ToString() + " / contrast=" + contrast.ToString() + "\n");
            ImageClass.BrightContrast(img, bright, contrast);
            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 

            hideSubMenu();
        }

        private void buttonTransform_Click(object sender, EventArgs e)
        {
            showSubMenu(panelTranslationSubMenu);
        }

        private void buttonTranslation_Click(object sender, EventArgs e)
        {
            //code
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("X?");
            form1.ShowDialog();
            int dx = Convert.ToInt32(form1.ValueTextBox.Text);

            InputBox form2 = new InputBox("Y");
            form2.ShowDialog();
            int dy = Convert.ToInt32(form2.ValueTextBox.Text);

            ImageClass.Translation(img, imgUndo, dx, dy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

            hideSubMenu();
        }        
        
        private void buttonRotation_Click(object sender, EventArgs e)
        {
            //code
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("Ângulo?");
            form1.ShowDialog();
            int angle = Convert.ToInt32(form1.ValueTextBox.Text);

            ImageClass.Rotation(img, imgUndo, angle);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

            hideSubMenu();
        }

        private void buttonZoom_Click(object sender, EventArgs e)
        {
            //code
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("Fator de escala?");
            form1.ShowDialog();
            float scaleFactor = (float)Convert.ToDouble(form1.ValueTextBox.Text);

            ImageClass.Scale(img, imgUndo, scaleFactor);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

            hideSubMenu();
        }

        private void buttonZoomPoint_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form1 = new InputBox("Fator de escala?");
            form1.ShowDialog();
            float scaleFactor = (float)Convert.ToDouble(form1.ValueTextBox.Text);

            ImageClass.Scale(img, imgUndo, scaleFactor);

            //get mouse coordinates
            mouseFlag = true;
            while (mouseFlag)
                Application.DoEvents(); //wait for mouseclick event

            ImageClass.Scale_point_xy(img, imgUndo, scaleFactor, mouseX, mouseY);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            showSubMenu(panelFiltersSubMenu);
        }

        private void buttonMean_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Mean(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

            hideSubMenu();
        }

        private void buttonNonUniform_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            MatrixForm form1 = new MatrixForm();
            form1.ShowDialog();

            float num1 = Convert.ToSingle(form1.textBox1.Text);
            float num2 = Convert.ToSingle(form1.textBox2.Text);
            float num3 = Convert.ToSingle(form1.textBox3.Text);
            float num4 = Convert.ToSingle(form1.textBox4.Text);
            float num5 = Convert.ToSingle(form1.textBox5.Text);
            float num6 = Convert.ToSingle(form1.textBox6.Text);
            float num7 = Convert.ToSingle(form1.textBox7.Text);
            float num8 = Convert.ToSingle(form1.textBox8.Text);
            float num9 = Convert.ToSingle(form1.textBox9.Text);

            float weight = Convert.ToSingle(form1.textBox10.Text);
            float[,] matrix = new float[,] { { num1, num2, num3 }, { num4, num5, num6 }, { num7, num8, num9 } };

            ImageClass.NonUniform(img, imgUndo, matrix, weight);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

            hideSubMenu();
        }

        private void buttonSobel_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Sobel(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }        

        private void buttonRoberts_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Roberts(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void buttonDiferentiation_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Diferentiation(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void buttonMedian_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Median(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void buttonHistograms_Click(object sender, EventArgs e)
        {
            showSubMenu(panelHistogramsSubMenu);
        }

        private void buttonHistogramGray_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[] array = ImageClass.Histogram_Gray(img);

            FormHistogram form1 = new FormHistogram(array);
            form1.ShowDialog();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void buttonHistogramRGB_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[,] array = ImageClass.Histogram_RGB(img);

            FormHistogram2 form1 = new FormHistogram2(array);
            form1.ShowDialog();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void buttonHistogramAll_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[,] array = ImageClass.Histogram_All(img);

            FormHistogram3 form1 = new FormHistogram3(array);
            form1.ShowDialog();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void buttonEqualization_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Equalization(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void buttonConvertBW_Click(object sender, EventArgs e)
        {
            showSubMenu(panelConvertBWSubMenu);
        }

        private void buttonConvertBW1_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[] hist = ImageClass.Histogram_Gray(img); //faz o mesmo que otsu
            int threshold = ImageClass.OTSU(hist);

            ImageClass.ConvertToBW(img, threshold);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void buttonConvertOtsu_Click_1(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToBW_Otsu(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void buttonBarCode_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int type = 1;

            Image<Bgr, byte> imgBC = ImageClass.BarCodeReader(img, type, out Point bc_centroid1, out Size bc_size1, out string bc_image1, out string bc_number1,
            out Point bc_centroid2, out Size bc_size2, out string bc_image2, out string bc_number2);

            ImageViewer.Image = imgBC.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            MessageBox.Show(bc_number1);

            if (bc_number2 != null)
            {
                MessageBox.Show(bc_number2);
            }

            Cursor = Cursors.Default; // normal cursor
        }
        #endregion

    }
}