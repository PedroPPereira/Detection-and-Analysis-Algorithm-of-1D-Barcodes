using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.IO;

namespace SS_OpenCV
{
    class ImageClass
    {
        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < img.Height; y++)
                    {
                        for (x = 0; x < img.Width; x++)
                        {
                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            //make negative
                            blue = (byte)(255 - blue);
                            green = (byte)(255 - green);
                            red = (byte)(255 - red);

                            dataPtr[0] = blue;
                            dataPtr[1] = green;
                            dataPtr[2] = red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < img.Height; y++)
                    {
                        for (x = 0; x < img.Width; x++)
                        {
                            //retrive red component
                            red = dataPtr[2];

                            //put the red component on every RGB 
                            dataPtr[0] = red;
                            dataPtr[1] = red;
                            dataPtr[2] = red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void GreenComponent(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte green;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < img.Height; y++)
                    {
                        for (x = 0; x < img.Width; x++)
                        {
                            //retrive red component
                            green = dataPtr[1];

                            //put the red component on every RGB 
                            dataPtr[0] = green;
                            dataPtr[1] = green;
                            dataPtr[2] = green;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BlueComponent(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < img.Height; y++)
                    {
                        for (x = 0; x < img.Width; x++)
                        {
                            //retrive red component
                            blue = dataPtr[0];

                            //put the red component on every RGB 
                            dataPtr[0] = blue;
                            dataPtr[1] = blue;
                            dataPtr[2] = blue;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;
                int blue_bright, green_bright, red_bright;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            //get bright and contrast of each component 
                            blue_bright = (int)Math.Round((contrast * blue) + bright);
                            if (blue_bright > 255)
                                blue_bright = 255;
                            if (blue_bright < 0)
                                blue_bright = 0;

                            green_bright = (int)Math.Round((contrast * green) + bright);
                            if (green_bright > 255)
                                green_bright = 255;
                            if (green_bright < 0)
                                green_bright = 0;

                            red_bright = (int)Math.Round((contrast * red) + bright);
                            if (red_bright > 255)
                                red_bright = 255;
                            if (red_bright < 0)
                                red_bright = 0;

                            // store in the image
                            dataPtr[0] = (byte)blue_bright;
                            dataPtr[1] = (byte)green_bright;
                            dataPtr[2] = (byte)red_bright;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
        {
            unsafe
            {
                // obter apontador do inicio da imagem MIplImage 
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int w = imgCopy.Width;
                int h = imgCopy.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                byte blue, green, red;
                int x_o, y_o;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)

                for (int y = 0; y < h; y++)
                {
                    y_o = y - dy;

                    for (int x = 0; x < w; x++)
                    {
                        x_o = x - dx;

                        if (x_o > w - 1 || x_o < 0 || y_o > h - 1 || y_o < 0)
                        {
                            dataPtr_d[0] = 0;
                            dataPtr_d[1] = 0;
                            dataPtr_d[2] = 0;
                        }
                        else
                        {
                            // calcula endereço do pixel no ponto(x, y)
                            blue = (byte)(dataPtr + y_o * widthstep + x_o * nC)[0];
                            green = (byte)(dataPtr + y_o * widthstep + x_o * nC)[1];
                            red = (byte)(dataPtr + y_o * widthstep + x_o * nC)[2];

                            // store in the image
                            dataPtr_d[0] = blue;
                            dataPtr_d[1] = green;
                            dataPtr_d[2] = red;
                        }
                        // advance the pointer to the next pixel
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding;
                }
            }
        }

        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                // obter apontador do inicio da imagem MIplImage 
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int w = imgCopy.Width;
                int h = imgCopy.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                byte blue, green, red;
                int x_o, y_o;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                double cos, sen;

                cos = System.Math.Cos(angle);
                sen = System.Math.Sin(angle);

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        x_o = (int)Math.Round(((x - w / 2.0) * cos) - ((h / 2.0 - y) * sen) + w / 2.0);
                        y_o = (int)Math.Round(h / 2.0 - ((x - w / 2.0) * sen) - ((h / 2.0 - y) * cos));

                        if (x_o > w - 1 || x_o < 0 || y_o > h - 1 || y_o < 0)
                        {
                            dataPtr_d[0] = 0;
                            dataPtr_d[1] = 0;
                            dataPtr_d[2] = 0;
                        }
                        else
                        {
                            // calcula endereço do pixel no ponto(x, y)
                            blue = (byte)(dataPtr + y_o * widthstep + x_o * nC)[0];
                            green = (byte)(dataPtr + y_o * widthstep + x_o * nC)[1];
                            red = (byte)(dataPtr + y_o * widthstep + x_o * nC)[2];

                            // store in the image
                            dataPtr_d[0] = blue;
                            dataPtr_d[1] = green;
                            dataPtr_d[2] = red;
                        }

                        // advance the pointer to the next pixel
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding;
                }
            }
        }

        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {
            unsafe
            {
                // obter apontador do inicio da imagem MIplImage 
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int w = imgCopy.Width;
                int h = imgCopy.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                byte blue, green, red;
                int x_o, y_o;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        x_o = (int)Math.Round(x / scaleFactor);
                        y_o = (int)Math.Round(y / scaleFactor);

                        if (x_o > w - 1 || x_o < 0 || y_o > h - 1 || y_o < 0)
                        {
                            dataPtr_d[0] = 0;
                            dataPtr_d[1] = 0;
                            dataPtr_d[2] = 0;
                        }
                        else
                        {
                            // calcula endereço do pixel no ponto(x, y)
                            blue = (byte)(dataPtr + y_o * widthstep + x_o * nC)[0];
                            green = (byte)(dataPtr + y_o * widthstep + x_o * nC)[1];
                            red = (byte)(dataPtr + y_o * widthstep + x_o * nC)[2];

                            // store in the image
                            dataPtr_d[0] = blue;
                            dataPtr_d[1] = green;
                            dataPtr_d[2] = red;
                        }

                        // advance the pointer to the next pixel
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding;
                }
            }
        }

        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image copy
                byte* dataPtrCopy = (byte*)mCopy.imageData.ToPointer(); // Pointer to the image copy

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, x0, y0;
                double addX0 = centerX - (width / 2) / scaleFactor;
                double addY0 = centerY - (height / 2) / scaleFactor;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            x0 = (int)Math.Round((x / scaleFactor) + addX0);
                            y0 = (int)Math.Round((y / scaleFactor) + addY0);

                            dataPtr[0] = (x0 < 0 || y0 < 0 || x0 > width - 1 || y0 > height - 1) ? (byte)0 : (byte)(dataPtrCopy + y0 * m.widthStep + x0 * nChan)[0]; //blue
                            dataPtr[1] = (x0 < 0 || y0 < 0 || x0 > width - 1 || y0 > height - 1) ? (byte)0 : (byte)(dataPtrCopy + y0 * m.widthStep + x0 * nChan)[1]; //green
                            dataPtr[2] = (x0 < 0 || y0 < 0 || x0 > width - 1 || y0 > height - 1) ? (byte)0 : (byte)(dataPtrCopy + y0 * m.widthStep + x0 * nChan)[2]; //red

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                // obter apontador do inicio da imagem MIplImage 
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int widht = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                int sumBlue, sumRed, sumGreen;

                dataPtr += widthstep + nC;
                dataPtr_d += widthstep + nC;

                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < widht - 1; x++)
                    {
                        sumBlue = (int)((dataPtr - widthstep - nC)[0] + (dataPtr - widthstep)[0] + (dataPtr - widthstep + nC)[0] + (dataPtr - nC)[0] + dataPtr[0] + (dataPtr + nC)[0] + (dataPtr + widthstep - nC)[0] + (dataPtr + widthstep)[0] + (dataPtr + widthstep + nC)[0]);
                        sumGreen = (int)((dataPtr - widthstep - nC)[1] + (dataPtr - widthstep)[1] + (dataPtr - widthstep + nC)[1] + (dataPtr - nC)[1] + dataPtr[1] + (dataPtr + nC)[1] + (dataPtr + widthstep - nC)[1] + (dataPtr + widthstep)[1] + (dataPtr + widthstep + nC)[1]);
                        sumRed = (int)((dataPtr - widthstep - nC)[2] + (dataPtr - widthstep)[2] + (dataPtr - widthstep + nC)[2] + (dataPtr - nC)[2] + dataPtr[2] + (dataPtr + nC)[2] + (dataPtr + widthstep - nC)[2] + (dataPtr + widthstep)[2] + (dataPtr + widthstep + nC)[2]);

                        // store in the image
                        dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                        dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                        dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                        // advance the pointer to the next pixel
                        dataPtr_d += nC;
                        dataPtr += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding + 2 * nC;
                    dataPtr += padding + 2 * nC;
                }

                //Canto Superior Esquerdo
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                sumBlue = (int)(dataPtr[0] * 4 + (dataPtr + nC)[0] * 2 + (dataPtr + widthstep)[0] * 2 + (dataPtr + widthstep + nC)[0]);
                sumGreen = (int)(dataPtr[1] * 4 + (dataPtr + nC)[1] * 2 + (dataPtr + widthstep)[1] * 2 + (dataPtr + widthstep + nC)[1]);
                sumRed = (int)(dataPtr[2] * 4 + (dataPtr + nC)[2] * 2 + (dataPtr + widthstep)[2] * 2 + (dataPtr + widthstep + nC)[2]);

                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                dataPtr_d += nC;
                dataPtr += nC;

                //Margem Superior
                for (int x_ms = 1; x_ms < widht - 1; x_ms++)
                {

                    sumBlue = (int)(dataPtr[0] * 2 + (dataPtr - nC)[0] * 2 + (dataPtr + nC)[0] * 2 + (dataPtr + widthstep)[0] + (dataPtr + widthstep - nC)[0] + (dataPtr + widthstep + nC)[0]);
                    sumGreen = (int)(dataPtr[1] * 2 + (dataPtr - nC)[1] * 2 + (dataPtr + nC)[1] * 2 + (dataPtr + widthstep)[1] + (dataPtr + widthstep - nC)[1] + (dataPtr + widthstep + nC)[1]);
                    sumRed = (int)(dataPtr[2] * 2 + (dataPtr - nC)[2] * 2 + (dataPtr + nC)[2] * 2 + (dataPtr + widthstep)[2] + (dataPtr + widthstep - nC)[2] + (dataPtr + widthstep + nC)[2]);

                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                    dataPtr_d += nC;
                    dataPtr += nC;
                }

                //Canto Superior Direito
                sumBlue = (int)(dataPtr[0] * 4 + (dataPtr - nC)[0] * 2 + (dataPtr + widthstep)[0] * 2 + (dataPtr + widthstep - nC)[0]);
                sumGreen = (int)(dataPtr[1] * 4 + (dataPtr - nC)[1] * 2 + (dataPtr + widthstep)[1] * 2 + (dataPtr + widthstep - nC)[1]);
                sumRed = (int)(dataPtr[2] * 4 + (dataPtr - nC)[2] * 2 + (dataPtr + widthstep)[2] * 2 + (dataPtr + widthstep - nC)[2]);

                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                dataPtr_d += widthstep;
                dataPtr += widthstep;

                //Margem direita
                for (int y_md = 1; y_md < height - 1; y_md++)
                {
                    sumBlue = (int)(dataPtr[0] * 2 + (dataPtr - widthstep)[0] * 2 + (dataPtr + widthstep)[0] * 2 + (dataPtr - nC)[0] + (dataPtr - nC + widthstep)[0] + (dataPtr - nC - widthstep)[0]);
                    sumGreen = (int)(dataPtr[1] * 2 + (dataPtr - widthstep)[1] * 2 + (dataPtr + widthstep)[1] * 2 + (dataPtr - nC)[1] + (dataPtr - nC + widthstep)[1] + (dataPtr - nC - widthstep)[1]);
                    sumRed = (int)(dataPtr[2] * 2 + (dataPtr - widthstep)[2] * 2 + (dataPtr + widthstep)[2] * 2 + (dataPtr - nC)[2] + (dataPtr - nC + widthstep)[2] + (dataPtr - nC - widthstep)[2]);

                    // store in the image
                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                    dataPtr_d += widthstep;
                    dataPtr += widthstep;
                }

                //Canto Inferior Direito
                sumBlue = (int)(dataPtr[0] * 4 + (dataPtr - nC)[0] * 2 + (dataPtr - widthstep)[0] * 2 + (dataPtr - widthstep - nC)[0]);
                sumGreen = (int)(dataPtr[1] * 4 + (dataPtr - nC)[1] * 2 + (dataPtr - widthstep)[1] * 2 + (dataPtr - widthstep - nC)[1]);
                sumRed = (int)(dataPtr[2] * 4 + (dataPtr - nC)[2] * 2 + (dataPtr - widthstep)[2] * 2 + (dataPtr - widthstep - nC)[2]);

                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                dataPtr_d -= nC;
                dataPtr -= nC;

                //Margem inferior
                for (int x_mi = 1; x_mi < widht - 1; x_mi++)
                {
                    sumBlue = (int)(dataPtr[0] * 2 + (dataPtr - nC)[0] * 2 + (dataPtr + nC)[0] * 2 + (dataPtr - widthstep)[0] + (dataPtr - nC - widthstep)[0] + (dataPtr + nC - widthstep)[0]);
                    sumGreen = (int)(dataPtr[1] * 2 + (dataPtr - nC)[1] * 2 + (dataPtr + nC)[1] * 2 + (dataPtr - widthstep)[1] + (dataPtr - nC - widthstep)[1] + (dataPtr + nC - widthstep)[1]);
                    sumRed = (int)(dataPtr[2] * 2 + (dataPtr - nC)[2] * 2 + (dataPtr + nC)[2] * 2 + (dataPtr - widthstep)[2] + (dataPtr - nC - widthstep)[2] + (dataPtr + nC - widthstep)[2]);

                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                    dataPtr_d -= nC;
                    dataPtr -= nC;
                }

                //Canto Inferior Esquerdo
                sumBlue = (int)(dataPtr[0] * 4 + (dataPtr + nC)[0] * 2 + (dataPtr - widthstep)[0] * 2 + (dataPtr - widthstep + nC)[0]);
                sumGreen = (int)(dataPtr[1] * 4 + (dataPtr + nC)[1] * 2 + (dataPtr - widthstep)[1] * 2 + (dataPtr - widthstep + nC)[1]);
                sumRed = (int)(dataPtr[2] * 4 + (dataPtr + nC)[2] * 2 + (dataPtr - widthstep)[2] * 2 + (dataPtr - widthstep + nC)[2]);

                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                dataPtr_d -= widthstep;
                dataPtr -= widthstep;

                //Margem esquerda
                for (int y_me = 1; y_me < height - 1; y_me++)
                {
                    sumBlue = (int)(dataPtr[0] * 2 + (dataPtr - widthstep)[0] * 2 + (dataPtr + widthstep)[0] * 2 + (dataPtr + nC)[0] + (dataPtr + nC + widthstep)[0] + (dataPtr + nC - widthstep)[0]);
                    sumGreen = (int)(dataPtr[1] * 2 + (dataPtr - widthstep)[1] * 2 + (dataPtr + widthstep)[1] * 2 + (dataPtr + nC)[1] + (dataPtr + nC + widthstep)[1] + (dataPtr + nC - widthstep)[1]);
                    sumRed = (int)(dataPtr[2] * 2 + (dataPtr - widthstep)[2] * 2 + (dataPtr + widthstep)[2] * 2 + (dataPtr + nC)[2] + (dataPtr + nC + widthstep)[2] + (dataPtr + nC - widthstep)[2]);

                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                    dataPtr_d -= widthstep;
                    dataPtr -= widthstep;
                }
            }
        }

        public static void Mean_solutionB(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                // obter apontador do inicio da imagem MIplImage 

                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int widht = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                int sumBlue, sumRed, sumGreen;
                int prevSumBlue, prevSumGreen, prevSumRed;
                int y, x;
                int indexcore = height - 2;
                int[] prevValuesCoreBlue = new int[indexcore];
                int[] prevValuesCoreGreen = new int[indexcore];
                int[] prevValuesCoreRed = new int[indexcore];

                //first pixel
                sumBlue = (int)(dataPtr[0] * 4 + (dataPtr + nC)[0] * 2 + (dataPtr + widthstep)[0] * 2 + (dataPtr + widthstep + nC)[0]);
                sumGreen = (int)(dataPtr[1] * 4 + (dataPtr + nC)[1] * 2 + (dataPtr + widthstep)[1] * 2 + (dataPtr + widthstep + nC)[1]);
                sumRed = (int)(dataPtr[2] * 4 + (dataPtr + nC)[2] * 2 + (dataPtr + widthstep)[2] * 2 + (dataPtr + widthstep + nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                prevSumBlue = sumBlue;
                prevSumGreen = sumGreen;
                prevSumRed = sumRed;

                dataPtr += nC;
                dataPtr_d += nC;

                //first row-----------------------------------------------------------------------------------------------------------------------------------------------------
                //2nd pixel
                sumBlue = (int)prevSumBlue - ((dataPtr - nC)[0] * 2 + (dataPtr - nC + widthstep)[0]) + ((dataPtr + nC)[0] * 2 + (dataPtr + nC + widthstep)[0]);
                sumGreen = (int)prevSumGreen - ((dataPtr - nC)[1] * 2 + (dataPtr - nC + widthstep)[1]) + ((dataPtr + nC)[1] * 2 + (dataPtr + nC + widthstep)[1]);
                sumRed = (int)prevSumRed - ((dataPtr - nC)[2] * 2 + (dataPtr - nC + widthstep)[2]) + ((dataPtr + nC)[2] * 2 + (dataPtr + nC + widthstep)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                prevSumBlue = sumBlue;
                prevSumGreen = sumGreen;
                prevSumRed = sumRed;

                dataPtr += nC;
                dataPtr_d += nC;

                //3rd pixel to widht-1
                for (int x_ms = 2; x_ms < widht - 1; x_ms++)
                {
                    sumBlue = (int)prevSumBlue - ((dataPtr - 2 * nC)[0] * 2 + (dataPtr - 2 * nC + widthstep)[0]) + ((dataPtr + nC)[0] * 2 + (dataPtr + nC + widthstep)[0]);
                    sumGreen = (int)prevSumGreen - ((dataPtr - 2 * nC)[1] * 2 + (dataPtr - 2 * nC + widthstep)[1]) + ((dataPtr + nC)[1] * 2 + (dataPtr + nC + widthstep)[1]);
                    sumRed = (int)prevSumRed - ((dataPtr - 2 * nC)[2] * 2 + (dataPtr - 2 * nC + widthstep)[2]) + ((dataPtr + nC)[2] * 2 + (dataPtr + nC + widthstep)[2]);

                    // store in the image 
                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                    prevSumBlue = sumBlue;
                    prevSumGreen = sumGreen;
                    prevSumRed = sumRed;

                    dataPtr += nC;
                    dataPtr_d += nC;
                }

                //last pixel
                sumBlue = (int)prevSumBlue - ((dataPtr - 2 * nC)[0] * 2 + (dataPtr - 2 * nC + widthstep)[0]) + ((dataPtr)[0] * 2 + (dataPtr + widthstep)[0]);
                sumGreen = (int)prevSumGreen - ((dataPtr - 2 * nC)[1] * 2 + (dataPtr - 2 * nC + widthstep)[1]) + ((dataPtr)[1] * 2 + (dataPtr + widthstep)[1]);
                sumRed = (int)prevSumRed - ((dataPtr - 2 * nC)[2] * 2 + (dataPtr - 2 * nC + widthstep)[2]) + ((dataPtr)[2] * 2 + (dataPtr + widthstep)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                prevSumBlue = sumBlue;
                prevSumGreen = sumGreen;
                prevSumRed = sumRed;

                dataPtr += widthstep;
                dataPtr_d += widthstep;

                //last collumn-----------------------------------------------------------------------------------------------------------------------------------------------------
                //2nd pixel
                sumBlue = (int)prevSumBlue - ((dataPtr - widthstep)[0] * 2 + (dataPtr - widthstep - nC)[0]) + ((dataPtr + widthstep)[0] * 2 + (dataPtr + widthstep - nC)[0]);
                sumGreen = (int)prevSumGreen - ((dataPtr - widthstep)[1] * 2 + (dataPtr - widthstep - nC)[1]) + ((dataPtr + widthstep)[1] * 2 + (dataPtr + widthstep - nC)[1]);
                sumRed = (int)prevSumRed - ((dataPtr - widthstep)[2] * 2 + (dataPtr - widthstep - nC)[2]) + ((dataPtr + widthstep)[2] * 2 + (dataPtr + widthstep - nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                prevSumBlue = sumBlue;
                prevSumGreen = sumGreen;
                prevSumRed = sumRed;

                dataPtr += widthstep;
                dataPtr_d += widthstep;

                //3rd pixel to height-1
                for (int y_md = 2; y_md < height - 1; y_md++)
                {
                    sumBlue = (int)(prevSumBlue - ((dataPtr - 2 * widthstep)[0] * 2 + (dataPtr - 2 * widthstep - nC)[0]) + ((dataPtr + widthstep)[0] * 2 + (dataPtr + widthstep - nC)[0]));
                    sumGreen = (int)prevSumGreen - ((dataPtr - 2 * widthstep)[1] * 2 + (dataPtr - 2 * widthstep - nC)[1]) + ((dataPtr + widthstep)[1] * 2 + (dataPtr + widthstep - nC)[1]);
                    sumRed = (int)prevSumRed - ((dataPtr - 2 * widthstep)[2] * 2 + (dataPtr - 2 * widthstep - nC)[2]) + ((dataPtr + widthstep)[2] * 2 + (dataPtr + widthstep - nC)[2]);

                    // store in the image 
                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                    prevSumBlue = sumBlue;
                    prevSumGreen = sumGreen;
                    prevSumRed = sumRed;

                    dataPtr += widthstep;
                    dataPtr_d += widthstep;
                }

                //last pixel
                sumBlue = (int)prevSumBlue - ((dataPtr - 2 * widthstep)[0] * 2 + (dataPtr - 2 * widthstep - nC)[0]) + ((dataPtr)[0] * 2 + (dataPtr - nC)[0]);
                sumGreen = (int)prevSumGreen - ((dataPtr - 2 * widthstep)[1] * 2 + (dataPtr - 2 * widthstep - nC)[1]) + ((dataPtr)[1] * 2 + (dataPtr - nC)[1]);
                sumRed = (int)prevSumRed - ((dataPtr - 2 * widthstep)[2] * 2 + (dataPtr - 2 * widthstep - nC)[2]) + ((dataPtr)[2] * 2 + (dataPtr - nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                prevSumBlue = sumBlue;
                prevSumGreen = sumGreen;
                prevSumRed = sumRed;

                dataPtr -= nC;
                dataPtr_d -= nC;

                ////last row--------------------------------------------------------------------------------------------------------------------------------------------------
                //2nd pixel (from right->left)
                sumBlue = (int)prevSumBlue - ((dataPtr + nC)[0] * 2 + (dataPtr + nC - widthstep)[0]) + ((dataPtr - nC)[0] * 2 + (dataPtr - widthstep - nC)[0]);
                sumGreen = (int)prevSumGreen - ((dataPtr + nC)[1] * 2 + (dataPtr + nC - widthstep)[1]) + ((dataPtr - nC)[1] * 2 + (dataPtr - widthstep - nC)[1]);
                sumRed = (int)prevSumRed - ((dataPtr + nC)[2] * 2 + (dataPtr + nC - widthstep)[2]) + ((dataPtr - nC)[2] * 2 + (dataPtr - widthstep - nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                prevSumBlue = sumBlue;
                prevSumGreen = sumGreen;
                prevSumRed = sumRed;

                dataPtr -= nC;
                dataPtr_d -= nC;

                //3rd pixel to width-1
                for (int x_mi = 2; x_mi < widht - 1; x_mi++)
                {
                    sumBlue = (int)prevSumBlue - ((dataPtr + 2 * nC)[0] * 2 + (dataPtr + 2 * nC - widthstep)[0]) + ((dataPtr - nC)[0] * 2 + (dataPtr - widthstep - nC)[0]);
                    sumGreen = (int)prevSumGreen - ((dataPtr + 2 * nC)[1] * 2 + (dataPtr + 2 * nC - widthstep)[1]) + ((dataPtr - nC)[1] * 2 + (dataPtr - widthstep - nC)[1]);
                    sumRed = (int)prevSumRed - ((dataPtr + 2 * nC)[2] * 2 + (dataPtr + 2 * nC - widthstep)[2]) + ((dataPtr - nC)[2] * 2 + (dataPtr - widthstep - nC)[2]);

                    // store in the image 
                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                    prevSumBlue = sumBlue;
                    prevSumGreen = sumGreen;
                    prevSumRed = sumRed;

                    dataPtr -= nC;
                    dataPtr_d -= nC;
                }

                //last pixel
                sumBlue = (int)prevSumBlue - ((dataPtr + 2 * nC)[0] * 2 + (dataPtr + 2 * nC - widthstep)[0]) + ((dataPtr)[0] * 2 + (dataPtr - widthstep)[0]);
                sumGreen = (int)prevSumGreen - ((dataPtr + 2 * nC)[1] * 2 + (dataPtr + 2 * nC - widthstep)[1]) + ((dataPtr)[1] * 2 + (dataPtr - widthstep)[1]);
                sumRed = (int)prevSumRed - ((dataPtr + 2 * nC)[2] * 2 + (dataPtr + 2 * nC - widthstep)[2]) + ((dataPtr)[2] * 2 + (dataPtr - widthstep)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                prevSumBlue = sumBlue;
                prevSumGreen = sumGreen;
                prevSumRed = sumRed;

                dataPtr -= widthstep;
                dataPtr_d -= widthstep;

                //first collumn--------------------------------------------------------------------------------------------------------------------------------------------------
                //2nd pixel (from bottom->top)
                sumBlue = (int)prevSumBlue - ((dataPtr + widthstep)[0] * 2 + (dataPtr + nC + widthstep)[0]) + ((dataPtr - widthstep)[0] * 2 + (dataPtr - widthstep + nC)[0]);
                sumGreen = (int)prevSumGreen - ((dataPtr + widthstep)[1] * 2 + (dataPtr + nC + widthstep)[1]) + ((dataPtr - widthstep)[1] * 2 + (dataPtr - widthstep + nC)[1]);
                sumRed = (int)prevSumRed - ((dataPtr + widthstep)[2] * 2 + (dataPtr + nC + widthstep)[2]) + ((dataPtr - widthstep)[2] * 2 + (dataPtr - widthstep + nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                prevSumBlue = sumBlue;
                prevSumGreen = sumGreen;
                prevSumRed = sumRed;

                prevValuesCoreBlue[indexcore - 1] = sumBlue;
                prevValuesCoreGreen[indexcore - 1] = sumGreen;
                prevValuesCoreRed[indexcore - 1] = sumRed;
                indexcore--;

                dataPtr -= widthstep;
                dataPtr_d -= widthstep;

                //3rd pixel to width-1
                for (int y_me = 2; y_me < height - 1; y_me++)
                {
                    sumBlue = (int)prevSumBlue - ((dataPtr + 2 * widthstep)[0] * 2 + (dataPtr + 2 * widthstep + nC)[0]) + ((dataPtr - widthstep)[0] * 2 + (dataPtr - widthstep + nC)[0]);
                    sumGreen = (int)prevSumGreen - ((dataPtr + 2 * widthstep)[1] * 2 + (dataPtr + 2 * widthstep + nC)[1]) + ((dataPtr - widthstep)[1] * 2 + (dataPtr - widthstep + nC)[1]);
                    sumRed = (int)prevSumRed - ((dataPtr + 2 * widthstep)[2] * 2 + (dataPtr + 2 * widthstep + nC)[2]) + ((dataPtr - widthstep)[2] * 2 + (dataPtr - widthstep + nC)[2]);

                    // store in the image 
                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                    prevSumBlue = sumBlue;
                    prevSumGreen = sumGreen;
                    prevSumRed = sumRed;

                    prevValuesCoreBlue[indexcore - 1] = sumBlue;
                    prevValuesCoreGreen[indexcore - 1] = sumGreen;
                    prevValuesCoreRed[indexcore - 1] = sumRed;
                    indexcore--;

                    dataPtr -= widthstep;
                    dataPtr_d -= widthstep;
                }

                //core
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                dataPtr += widthstep + nC;
                dataPtr_d += widthstep + nC;

                indexcore = 0;

                //core's first collumn 


                for (y = 1; y < height - 1; y++)
                {
                    prevSumBlue = prevValuesCoreBlue[indexcore];
                    prevSumGreen = prevValuesCoreGreen[indexcore];
                    prevSumRed = prevValuesCoreRed[indexcore];

                    sumBlue = (int)prevSumBlue - ((dataPtr - nC)[0] + (dataPtr - nC - widthstep)[0] + (dataPtr + widthstep - nC)[0]) + ((dataPtr + nC)[0] + (dataPtr + nC - widthstep)[0] + (dataPtr + widthstep + nC)[0]);
                    sumGreen = (int)prevSumGreen - ((dataPtr - nC)[1] + (dataPtr - nC - widthstep)[1] + (dataPtr + widthstep - nC)[1]) + ((dataPtr + nC)[1] + (dataPtr + nC - widthstep)[1] + (dataPtr + widthstep + nC)[1]);
                    sumRed = (int)prevSumRed - ((dataPtr - nC)[2] + (dataPtr - nC - widthstep)[2] + (dataPtr + widthstep - nC)[2]) + ((dataPtr + nC)[2] + (dataPtr + nC - widthstep)[2] + (dataPtr + widthstep + nC)[2]);

                    // store in the image 
                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                    prevValuesCoreBlue[indexcore] = sumBlue;
                    prevValuesCoreGreen[indexcore] = sumGreen;
                    prevValuesCoreRed[indexcore] = sumRed;

                    indexcore++;

                    dataPtr += widthstep;
                    dataPtr_d += widthstep;
                }

                //rest of the core
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                dataPtr += widthstep + 2 * nC;
                dataPtr_d += widthstep + 2 * nC;

                indexcore = 0;

                for (y = 1; y < height - 1; y++)
                {
                    prevSumBlue = prevValuesCoreBlue[indexcore];
                    prevSumGreen = prevValuesCoreGreen[indexcore];
                    prevSumRed = prevValuesCoreRed[indexcore];

                    for (x = 2; x < widht - 1; x++)
                    {
                        sumBlue = (int)prevSumBlue - ((dataPtr - 2 * nC)[0] + (dataPtr - 2 * nC - widthstep)[0] + (dataPtr + widthstep - 2 * nC)[0]) + ((dataPtr + nC)[0] + (dataPtr + nC - widthstep)[0] + (dataPtr + widthstep + nC)[0]);
                        sumGreen = (int)prevSumGreen - ((dataPtr - 2 * nC)[1] + (dataPtr - 2 * nC - widthstep)[1] + (dataPtr + widthstep - 2 * nC)[1]) + ((dataPtr + nC)[1] + (dataPtr + nC - widthstep)[1] + (dataPtr + widthstep + nC)[1]);
                        sumRed = (int)prevSumRed - ((dataPtr - 2 * nC)[2] + (dataPtr - 2 * nC - widthstep)[2] + (dataPtr + widthstep - 2 * nC)[2]) + ((dataPtr + nC)[2] + (dataPtr + nC - widthstep)[2] + (dataPtr + widthstep + nC)[2]);

                        prevSumBlue = sumBlue;
                        prevSumGreen = sumGreen;
                        prevSumRed = sumRed;

                        // store in the image 
                        dataPtr_d[0] = (byte)Math.Round(sumBlue / 9.0);
                        dataPtr_d[1] = (byte)Math.Round(sumGreen / 9.0);
                        dataPtr_d[2] = (byte)Math.Round(sumRed / 9.0);

                        dataPtr += nC;
                        dataPtr_d += nC;
                    }
                    indexcore++;

                    dataPtr += padding + 3 * nC;
                    dataPtr_d += padding + 3 * nC;
                }
            }
        }

        public static void Mean_solutionC(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int size)
        {
            unsafe
            {
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int widht = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                int sumBlue, sumRed, sumGreen;
                int[,] newimage_blue = new int[widht, height];
                int[,] newimage_green = new int[widht, height];
                int[,] newimage_red = new int[widht, height];
                int index_widht = 0;
                int index_height = 0;

                //first pixel
                sumBlue = (int)(dataPtr[0] * 16 + (dataPtr + nC)[0] * 4 + (dataPtr + 2 * nC)[0] * 4 + (dataPtr + 3 * nC)[0] * 4
                          + (dataPtr + widthstep)[0] * 4 + (dataPtr + widthstep + nC)[0] + (dataPtr + widthstep + 2 * nC)[0] + (dataPtr + widthstep + 3 * nC)[0]
                          + (dataPtr + 2 * widthstep)[0] * 4 + (dataPtr + 2 * widthstep + nC)[0] + (dataPtr + 2 * widthstep + 2 * nC)[0] + (dataPtr + 2 * widthstep + 3 * nC)[0]
                          + (dataPtr + 3 * widthstep)[0] * 4 + (dataPtr + 3 * widthstep + nC)[0] + (dataPtr + 3 * widthstep + 2 * nC)[0] + (dataPtr + 3 * widthstep + 3 * nC)[0]);

                sumGreen = (int)(dataPtr[1] * 16 + (dataPtr + nC)[1] * 4 + (dataPtr + 2 * nC)[1] * 4 + (dataPtr + 3 * nC)[1] * 4
                          + (dataPtr + widthstep)[1] * 4 + (dataPtr + widthstep + nC)[1] + (dataPtr + widthstep + 2 * nC)[1] + (dataPtr + widthstep + 3 * nC)[1]
                          + (dataPtr + 2 * widthstep)[1] * 4 + (dataPtr + 2 * widthstep + nC)[1] + (dataPtr + 2 * widthstep + 2 * nC)[1] + (dataPtr + 2 * widthstep + 3 * nC)[1]
                          + (dataPtr + 3 * widthstep)[1] * 4 + (dataPtr + 3 * widthstep + nC)[1] + (dataPtr + 3 * widthstep + 2 * nC)[1] + (dataPtr + 3 * widthstep + 3 * nC)[1]);

                sumRed = (int)(dataPtr[2] * 16 + (dataPtr + nC)[2] * 4 + (dataPtr + 2 * nC)[2] * 4 + (dataPtr + 3 * nC)[2] * 4
                          + (dataPtr + widthstep)[2] * 4 + (dataPtr + widthstep + nC)[2] + (dataPtr + widthstep + 2 * nC)[2] + (dataPtr + widthstep + 3 * nC)[2]
                          + (dataPtr + 2 * widthstep)[2] * 4 + (dataPtr + 2 * widthstep + nC)[2] + (dataPtr + 2 * widthstep + 2 * nC)[2] + (dataPtr + 2 * widthstep + 3 * nC)[2]
                          + (dataPtr + 3 * widthstep)[2] * 4 + (dataPtr + 3 * widthstep + nC)[2] + (dataPtr + 3 * widthstep + 2 * nC)[2] + (dataPtr + 3 * widthstep + 3 * nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += nC;
                dataPtr_d += nC;
                index_widht++;

                //first row-----------------------------------------------------------------------------------------------------------------------------------------------------
                //2nd pixel
                sumBlue = (int)newimage_blue[index_widht - 1, index_height] - ((dataPtr - nC)[0] * 4 + (dataPtr - nC + widthstep)[0] + (dataPtr - nC + 2 * widthstep)[0] + (dataPtr - nC + 3 * widthstep)[0]) + ((dataPtr + 3 * nC)[0] * 4 + (dataPtr + 3 * nC + widthstep)[0] + (dataPtr + 3 * nC + 2 * widthstep)[0] + (dataPtr + 3 * nC + 3 * widthstep)[0]);
                sumGreen = (int)newimage_green[index_widht - 1, index_height] - ((dataPtr - nC)[1] * 4 + (dataPtr - nC + widthstep)[1] + (dataPtr - nC + 2 * widthstep)[1] + (dataPtr - nC + 3 * widthstep)[1]) + ((dataPtr + 3 * nC)[1] * 4 + (dataPtr + 3 * nC + widthstep)[1] + (dataPtr + 3 * nC + 2 * widthstep)[1] + (dataPtr + 3 * nC + 3 * widthstep)[1]);
                sumRed = (int)newimage_red[index_widht - 1, index_height] - ((dataPtr - nC)[2] * 4 + (dataPtr - nC + widthstep)[2] + (dataPtr - nC + 2 * widthstep)[2] + (dataPtr - nC + 3 * widthstep)[2]) + ((dataPtr + 3 * nC)[2] * 4 + (dataPtr + 3 * nC + widthstep)[2] + (dataPtr + 3 * nC + 2 * widthstep)[2] + (dataPtr + 3 * nC + 3 * widthstep)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += nC;
                dataPtr_d += nC;
                index_widht++;

                //3rd pixel
                sumBlue = (int)newimage_blue[index_widht - 1, index_height] - ((dataPtr - 2 * nC)[0] * 4 + (dataPtr - 2 * nC + widthstep)[0] + (dataPtr - 2 * nC + 2 * widthstep)[0] + (dataPtr - 2 * nC + 3 * widthstep)[0]) + ((dataPtr + 3 * nC)[0] * 4 + (dataPtr + 3 * nC + widthstep)[0] + (dataPtr + 3 * nC + 2 * widthstep)[0] + (dataPtr + 3 * nC + 3 * widthstep)[0]);
                sumGreen = (int)newimage_green[index_widht - 1, index_height] - ((dataPtr - 2 * nC)[1] * 4 + (dataPtr - 2 * nC + widthstep)[1] + (dataPtr - 2 * nC + 2 * widthstep)[1] + (dataPtr - 2 * nC + 3 * widthstep)[1]) + ((dataPtr + 3 * nC)[1] * 4 + (dataPtr + 3 * nC + widthstep)[1] + (dataPtr + 3 * nC + 2 * widthstep)[1] + (dataPtr + 3 * nC + 3 * widthstep)[1]);
                sumRed = (int)newimage_red[index_widht - 1, index_height] - ((dataPtr - 2 * nC)[2] * 4 + (dataPtr - 2 * nC + widthstep)[2] + (dataPtr - 2 * nC + 2 * widthstep)[2] + (dataPtr - 2 * nC + 3 * widthstep)[2]) + ((dataPtr + 3 * nC)[2] * 4 + (dataPtr + 3 * nC + widthstep)[2] + (dataPtr + 3 * nC + 2 * widthstep)[2] + (dataPtr + 3 * nC + 3 * widthstep)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += nC;
                dataPtr_d += nC;
                index_widht++;

                //4th 

                sumBlue = (int)newimage_blue[index_widht - 1, index_height] - ((dataPtr - 3 * nC)[0] * 4 + (dataPtr - 3 * nC + widthstep)[0] + (dataPtr - 3 * nC + 2 * widthstep)[0] + (dataPtr - 3 * nC + 3 * widthstep)[0]) + ((dataPtr + 3 * nC)[0] * 4 + (dataPtr + 3 * nC + widthstep)[0] + (dataPtr + 3 * nC + 2 * widthstep)[0] + (dataPtr + 3 * nC + 3 * widthstep)[0]);
                sumGreen = (int)newimage_green[index_widht - 1, index_height] - ((dataPtr - 3 * nC)[1] * 4 + (dataPtr - 3 * nC + widthstep)[1] + (dataPtr - 3 * nC + 2 * widthstep)[1] + (dataPtr - 3 * nC + 3 * widthstep)[1]) + ((dataPtr + 3 * nC)[1] * 4 + (dataPtr + 3 * nC + widthstep)[1] + (dataPtr + 3 * nC + 2 * widthstep)[1] + (dataPtr + 3 * nC + 3 * widthstep)[1]);
                sumRed = (int)newimage_red[index_widht - 1, index_height] - ((dataPtr - 3 * nC)[2] * 4 + (dataPtr - 3 * nC + widthstep)[2] + (dataPtr - 3 * nC + 2 * widthstep)[2] + (dataPtr - 3 * nC + 3 * widthstep)[2]) + ((dataPtr + 3 * nC)[2] * 4 + (dataPtr + 3 * nC + widthstep)[2] + (dataPtr + 3 * nC + 2 * widthstep)[2] + (dataPtr + 3 * nC + 3 * widthstep)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += nC;
                dataPtr_d += nC;
                index_widht++;

                //4th to width-3
                for (int x_ms = 4; x_ms < widht - 3; x_ms++)
                {
                    sumBlue = (int)newimage_blue[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[0] * 4 + (dataPtr - 4 * nC + widthstep)[0] + (dataPtr - 4 * nC + 2 * widthstep)[0] + (dataPtr - 4 * nC + 3 * widthstep)[0]) + ((dataPtr + 3 * nC)[0] * 4 + (dataPtr + 3 * nC + widthstep)[0] + (dataPtr + 3 * nC + 2 * widthstep)[0] + (dataPtr + 3 * nC + 3 * widthstep)[0]);
                    sumGreen = (int)newimage_green[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[1] * 4 + (dataPtr - 4 * nC + widthstep)[1] + (dataPtr - 4 * nC + 2 * widthstep)[1] + (dataPtr - 4 * nC + 3 * widthstep)[1]) + ((dataPtr + 3 * nC)[1] * 4 + (dataPtr + 3 * nC + widthstep)[1] + (dataPtr + 3 * nC + 2 * widthstep)[1] + (dataPtr + 3 * nC + 3 * widthstep)[1]);
                    sumRed = (int)newimage_red[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[2] * 4 + (dataPtr - 4 * nC + widthstep)[2] + (dataPtr - 4 * nC + 2 * widthstep)[2] + (dataPtr - 4 * nC + 3 * widthstep)[2]) + ((dataPtr + 3 * nC)[2] * 4 + (dataPtr + 3 * nC + widthstep)[2] + (dataPtr + 3 * nC + 2 * widthstep)[2] + (dataPtr + 3 * nC + 3 * widthstep)[2]);

                    // store in the image 
                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                    newimage_blue[index_widht, index_height] = sumBlue;
                    newimage_green[index_widht, index_height] = sumGreen;
                    newimage_red[index_widht, index_height] = sumRed;

                    dataPtr += nC;
                    dataPtr_d += nC;
                    index_widht++;
                }

                //
                sumBlue = (int)newimage_blue[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[0] * 4 + (dataPtr - 4 * nC + widthstep)[0] + (dataPtr - 4 * nC + 2 * widthstep)[0] + (dataPtr - 4 * nC + 3 * widthstep)[0]) + ((dataPtr + 2 * nC)[0] * 4 + (dataPtr + 2 * nC + widthstep)[0] + (dataPtr + 2 * nC + 2 * widthstep)[0] + (dataPtr + 2 * nC + 3 * widthstep)[0]);
                sumGreen = (int)newimage_green[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[1] * 4 + (dataPtr - 4 * nC + widthstep)[1] + (dataPtr - 4 * nC + 2 * widthstep)[1] + (dataPtr - 4 * nC + 3 * widthstep)[1]) + ((dataPtr + 2 * nC)[1] * 4 + (dataPtr + 2 * nC + widthstep)[1] + (dataPtr + 2 * nC + 2 * widthstep)[1] + (dataPtr + 2 * nC + 3 * widthstep)[1]);
                sumRed = (int)newimage_red[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[2] * 4 + (dataPtr - 4 * nC + widthstep)[2] + (dataPtr - 4 * nC + 2 * widthstep)[2] + (dataPtr - 4 * nC + 3 * widthstep)[2]) + ((dataPtr + 2 * nC)[2] * 4 + (dataPtr + 2 * nC + widthstep)[2] + (dataPtr + 2 * nC + 2 * widthstep)[2] + (dataPtr + 2 * nC + 3 * widthstep)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += nC;
                dataPtr_d += nC;
                index_widht++;

                //
                sumBlue = (int)newimage_blue[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[0] * 4 + (dataPtr - 4 * nC + widthstep)[0] + (dataPtr - 4 * nC + 2 * widthstep)[0] + (dataPtr - 4 * nC + 3 * widthstep)[0]) + ((dataPtr + nC)[0] * 4 + (dataPtr + nC + widthstep)[0] + (dataPtr + nC + 2 * widthstep)[0] + (dataPtr + nC + 3 * widthstep)[0]);
                sumGreen = (int)newimage_green[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[1] * 4 + (dataPtr - 4 * nC + widthstep)[1] + (dataPtr - 4 * nC + 2 * widthstep)[1] + (dataPtr - 4 * nC + 3 * widthstep)[1]) + ((dataPtr + nC)[1] * 4 + (dataPtr + nC + widthstep)[1] + (dataPtr + nC + 2 * widthstep)[1] + (dataPtr + nC + 3 * widthstep)[1]);
                sumRed = (int)newimage_red[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[2] * 4 + (dataPtr - 4 * nC + widthstep)[2] + (dataPtr - 4 * nC + 2 * widthstep)[2] + (dataPtr - 4 * nC + 3 * widthstep)[2]) + ((dataPtr + nC)[2] * 4 + (dataPtr + nC + widthstep)[2] + (dataPtr + nC + 2 * widthstep)[2] + (dataPtr + nC + 3 * widthstep)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += nC;
                dataPtr_d += nC;
                index_widht++;

                //last pixel
                sumBlue = (int)newimage_blue[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[0] * 4 + (dataPtr - 4 * nC + widthstep)[0] + (dataPtr - 4 * nC + 2 * widthstep)[0] + (dataPtr - 4 * nC + 3 * widthstep)[0]) + ((dataPtr)[0] * 4 + (dataPtr + widthstep)[0] + (dataPtr + 2 * widthstep)[0] + (dataPtr + 3 * widthstep)[0]);
                sumGreen = (int)newimage_green[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[1] * 4 + (dataPtr - 4 * nC + widthstep)[1] + (dataPtr - 4 * nC + 2 * widthstep)[1] + (dataPtr - 4 * nC + 3 * widthstep)[1]) + ((dataPtr)[1] * 4 + (dataPtr + widthstep)[1] + (dataPtr + 2 * widthstep)[1] + (dataPtr + 3 * widthstep)[1]);
                sumRed = (int)newimage_red[index_widht - 1, index_height] - ((dataPtr - 4 * nC)[2] * 4 + (dataPtr - 4 * nC + widthstep)[2] + (dataPtr - 4 * nC + 2 * widthstep)[2] + (dataPtr - 4 * nC + 3 * widthstep)[2]) + ((dataPtr)[2] * 4 + (dataPtr + widthstep)[2] + (dataPtr + 2 * widthstep)[2] + (dataPtr + 3 * widthstep)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                //first collumn
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                dataPtr += widthstep;
                dataPtr_d += widthstep;
                index_widht = 0;
                index_height = 1;

                //2nd pixel
                sumBlue = (int)newimage_blue[index_widht, index_height - 1] - ((dataPtr - widthstep)[0] * 4 + (dataPtr - widthstep + nC)[0] + (dataPtr - widthstep + 2 * nC)[0] + (dataPtr - widthstep + 3 * nC)[0]) + ((dataPtr + 3 * widthstep)[0] * 4 + (dataPtr + 3 * widthstep + nC)[0] + (dataPtr + 3 * widthstep + 2 * nC)[0] + (dataPtr + 3 * widthstep + 3 * nC)[0]);
                sumGreen = (int)newimage_green[index_widht, index_height - 1] - ((dataPtr - widthstep)[1] * 4 + (dataPtr - widthstep + nC)[1] + (dataPtr - widthstep + 2 * nC)[1] + (dataPtr - widthstep + 3 * nC)[1]) + ((dataPtr + 3 * widthstep)[1] * 4 + (dataPtr + 3 * widthstep + nC)[1] + (dataPtr + 3 * widthstep + 2 * nC)[1] + (dataPtr + 3 * widthstep + 3 * nC)[1]);
                sumRed = (int)newimage_red[index_widht, index_height - 1] - ((dataPtr - widthstep)[2] * 4 + (dataPtr - widthstep + nC)[2] + (dataPtr - widthstep + 2 * nC)[2] + (dataPtr - widthstep + 3 * nC)[2]) + ((dataPtr + 3 * widthstep)[2] * 4 + (dataPtr + 3 * widthstep + nC)[2] + (dataPtr + 3 * widthstep + 2 * nC)[2] + (dataPtr + 3 * widthstep + 3 * nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += widthstep;
                dataPtr_d += widthstep;
                index_height++;

                //3rd pixel
                sumBlue = (int)newimage_blue[index_widht, index_height - 1] - ((dataPtr - 2 * widthstep)[0] * 4 + (dataPtr - 2 * widthstep + nC)[0] + (dataPtr - 2 * widthstep + 2 * nC)[0] + (dataPtr - 2 * widthstep + 3 * nC)[0]) + ((dataPtr + 3 * widthstep)[0] * 4 + (dataPtr + 3 * widthstep + nC)[0] + (dataPtr + 3 * widthstep + 2 * nC)[0] + (dataPtr + 3 * widthstep + 3 * nC)[0]);
                sumGreen = (int)newimage_green[index_widht, index_height - 1] - ((dataPtr - 2 * widthstep)[1] * 4 + (dataPtr - 2 * widthstep + nC)[1] + (dataPtr - 2 * widthstep + 2 * nC)[1] + (dataPtr - 2 * widthstep + 3 * nC)[1]) + ((dataPtr + 3 * widthstep)[1] * 4 + (dataPtr + 3 * widthstep + nC)[1] + (dataPtr + 3 * widthstep + 2 * nC)[1] + (dataPtr + 3 * widthstep + 3 * nC)[1]);
                sumRed = (int)newimage_red[index_widht, index_height - 1] - ((dataPtr - 2 * widthstep)[2] * 4 + (dataPtr - 2 * widthstep + nC)[2] + (dataPtr - 2 * widthstep + 2 * nC)[2] + (dataPtr - 2 * widthstep + 3 * nC)[2]) + ((dataPtr + 3 * widthstep)[2] * 4 + (dataPtr + 3 * widthstep + nC)[2] + (dataPtr + 3 * widthstep + 2 * nC)[2] + (dataPtr + 3 * widthstep + 3 * nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += widthstep;
                dataPtr_d += widthstep;
                index_height++;

                //4th pixel
                sumBlue = (int)newimage_blue[index_widht, index_height - 1] - ((dataPtr - 3 * widthstep)[0] * 4 + (dataPtr - 3 * widthstep + nC)[0] + (dataPtr - 3 * widthstep + 2 * nC)[0] + (dataPtr - 3 * widthstep + 3 * nC)[0]) + ((dataPtr + 3 * widthstep)[0] * 4 + (dataPtr + 3 * widthstep + nC)[0] + (dataPtr + 3 * widthstep + 2 * nC)[0] + (dataPtr + 3 * widthstep + 3 * nC)[0]);
                sumGreen = (int)newimage_green[index_widht, index_height - 1] - ((dataPtr - 3 * widthstep)[1] * 4 + (dataPtr - 3 * widthstep + nC)[1] + (dataPtr - 3 * widthstep + 2 * nC)[1] + (dataPtr - 3 * widthstep + 3 * nC)[1]) + ((dataPtr + 3 * widthstep)[1] * 4 + (dataPtr + 3 * widthstep + nC)[1] + (dataPtr + 3 * widthstep + 2 * nC)[1] + (dataPtr + 3 * widthstep + 3 * nC)[1]);
                sumRed = (int)newimage_red[index_widht, index_height - 1] - ((dataPtr - 3 * widthstep)[2] * 4 + (dataPtr - 3 * widthstep + nC)[2] + (dataPtr - 3 * widthstep + 2 * nC)[2] + (dataPtr - 3 * widthstep + 3 * nC)[2]) + ((dataPtr + 3 * widthstep)[2] * 4 + (dataPtr + 3 * widthstep + nC)[2] + (dataPtr + 3 * widthstep + 2 * nC)[2] + (dataPtr + 3 * widthstep + 3 * nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += widthstep;
                dataPtr_d += widthstep;
                index_height++;

                //4th to width-3
                for (int y_me = 4; y_me < height - 3; y_me++)
                {
                    sumBlue = (int)newimage_blue[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[0] * 4 + (dataPtr - 4 * widthstep + nC)[0] + (dataPtr - 4 * widthstep + 2 * nC)[0] + (dataPtr - 4 * widthstep + 3 * nC)[0]) + ((dataPtr + 3 * widthstep)[0] * 4 + (dataPtr + 3 * widthstep + nC)[0] + (dataPtr + 3 * widthstep + 2 * nC)[0] + (dataPtr + 3 * widthstep + 3 * nC)[0]);
                    sumGreen = (int)newimage_green[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[1] * 4 + (dataPtr - 4 * widthstep + nC)[1] + (dataPtr - 4 * widthstep + 2 * nC)[1] + (dataPtr - 4 * widthstep + 3 * nC)[1]) + ((dataPtr + 3 * widthstep)[1] * 4 + (dataPtr + 3 * widthstep + nC)[1] + (dataPtr + 3 * widthstep + 2 * nC)[1] + (dataPtr + 3 * widthstep + 3 * nC)[1]);
                    sumRed = (int)newimage_red[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[2] * 4 + (dataPtr - 4 * widthstep + nC)[2] + (dataPtr - 4 * widthstep + 2 * nC)[2] + (dataPtr - 4 * widthstep + 3 * nC)[2]) + ((dataPtr + 3 * widthstep)[2] * 4 + (dataPtr + 3 * widthstep + nC)[2] + (dataPtr + 3 * widthstep + 2 * nC)[2] + (dataPtr + 3 * widthstep + 3 * nC)[2]);

                    // store in the image 
                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                    newimage_blue[index_widht, index_height] = sumBlue;
                    newimage_green[index_widht, index_height] = sumGreen;
                    newimage_red[index_widht, index_height] = sumRed;

                    dataPtr += widthstep;
                    dataPtr_d += widthstep;
                    index_height++;
                }

                //
                sumBlue = (int)newimage_blue[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[0] * 4 + (dataPtr - 4 * widthstep + nC)[0] + (dataPtr - 4 * widthstep + 2 * nC)[0] + (dataPtr - 4 * widthstep + 3 * nC)[0]) + ((dataPtr + 2 * widthstep)[0] * 4 + (dataPtr + 2 * widthstep + nC)[0] + (dataPtr + 2 * widthstep + 2 * nC)[0] + (dataPtr + 2 * widthstep + 3 * nC)[0]);
                sumGreen = (int)newimage_green[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[1] * 4 + (dataPtr - 4 * widthstep + nC)[1] + (dataPtr - 4 * widthstep + 2 * nC)[1] + (dataPtr - 4 * widthstep + 3 * nC)[1]) + ((dataPtr + 2 * widthstep)[1] * 4 + (dataPtr + 2 * widthstep + nC)[1] + (dataPtr + 2 * widthstep + 2 * nC)[1] + (dataPtr + 2 * widthstep + 3 * nC)[1]);
                sumRed = (int)newimage_red[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[2] * 4 + (dataPtr - 4 * widthstep + nC)[2] + (dataPtr - 4 * widthstep + 2 * nC)[2] + (dataPtr - 4 * widthstep + 3 * nC)[2]) + ((dataPtr + 2 * widthstep)[2] * 4 + (dataPtr + 2 * widthstep + nC)[2] + (dataPtr + 2 * widthstep + 2 * nC)[2] + (dataPtr + 2 * widthstep + 3 * nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += widthstep;
                dataPtr_d += widthstep;
                index_height++;

                //
                sumBlue = (int)newimage_blue[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[0] * 4 + (dataPtr - 4 * widthstep + nC)[0] + (dataPtr - 4 * widthstep + 2 * nC)[0] + (dataPtr - 4 * widthstep + 3 * nC)[0]) + ((dataPtr + widthstep)[0] * 4 + (dataPtr + widthstep + nC)[0] + (dataPtr + widthstep + 2 * nC)[0] + (dataPtr + widthstep + 3 * nC)[0]);
                sumGreen = (int)newimage_green[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[1] * 4 + (dataPtr - 4 * widthstep + nC)[1] + (dataPtr - 4 * widthstep + 2 * nC)[1] + (dataPtr - 4 * widthstep + 3 * nC)[1]) + ((dataPtr + widthstep)[1] * 4 + (dataPtr + widthstep + nC)[1] + (dataPtr + widthstep + 2 * nC)[1] + (dataPtr + widthstep + 3 * nC)[1]);
                sumRed = (int)newimage_red[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[2] * 4 + (dataPtr - 4 * widthstep + nC)[2] + (dataPtr - 4 * widthstep + 2 * nC)[2] + (dataPtr - 4 * widthstep + 3 * nC)[2]) + ((dataPtr + widthstep)[2] * 4 + (dataPtr + widthstep + nC)[2] + (dataPtr + widthstep + 2 * nC)[2] + (dataPtr + widthstep + 3 * nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += widthstep;
                dataPtr_d += widthstep;
                index_height++;

                //last pixel
                sumBlue = (int)newimage_blue[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[0] * 4 + (dataPtr - 4 * widthstep + nC)[0] + (dataPtr - 4 * widthstep + 2 * nC)[0] + (dataPtr - 4 * widthstep + 3 * nC)[0]) + ((dataPtr)[0] * 4 + (dataPtr + nC)[0] + (dataPtr + 2 * nC)[0] + (dataPtr + 3 * nC)[0]);
                sumGreen = (int)newimage_green[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[1] * 4 + (dataPtr - 4 * widthstep + nC)[1] + (dataPtr - 4 * widthstep + 2 * nC)[1] + (dataPtr - 4 * widthstep + 3 * nC)[1]) + ((dataPtr)[1] * 4 + (dataPtr + nC)[1] + (dataPtr + 2 * nC)[1] + (dataPtr + 3 * nC)[1]);
                sumRed = (int)newimage_red[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep)[2] * 4 + (dataPtr - 4 * widthstep + nC)[2] + (dataPtr - 4 * widthstep + 2 * nC)[2] + (dataPtr - 4 * widthstep + 3 * nC)[2]) + ((dataPtr)[2] * 4 + (dataPtr + nC)[2] + (dataPtr + 2 * nC)[2] + (dataPtr + 3 * nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                //second row
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                dataPtr += widthstep + nC;
                dataPtr_d += widthstep + nC;
                index_widht = 1;
                index_height = 1;

                for (int y_i = 0; y_i < 3; y_i++)
                {
                    for (int x_i = 1; x_i < widht; x_i++)
                    {
                        sumBlue = (int)newimage_blue[index_widht - 1, index_height] - ((dataPtr - nC - widthstep)[0] * 3 + (dataPtr - nC)[0] + (dataPtr - nC + widthstep)[0] + (dataPtr - nC + 2 * widthstep)[0] + (dataPtr - nC + 3 * widthstep)[0]) + ((dataPtr + 3 * nC)[0] * 4 + (dataPtr + 3 * nC + widthstep)[0] + (dataPtr + 3 * nC + 2 * widthstep)[0] + (dataPtr + 3 * nC + 3 * widthstep)[0]);
                        sumGreen = (int)newimage_green[index_widht - 1, index_height] - ((dataPtr - nC)[1] * 4 + (dataPtr - nC + widthstep)[1] + (dataPtr - nC + 2 * widthstep)[1] + (dataPtr - nC + 3 * widthstep)[1]) + ((dataPtr + 3 * nC)[1] * 4 + (dataPtr + 3 * nC + widthstep)[1] + (dataPtr + 3 * nC + 2 * widthstep)[1] + (dataPtr + 3 * nC + 3 * widthstep)[1]);
                        sumRed = (int)newimage_red[index_widht - 1, index_height] - ((dataPtr - nC)[2] * 4 + (dataPtr - nC + widthstep)[2] + (dataPtr - nC + 2 * widthstep)[2] + (dataPtr - nC + 3 * widthstep)[2]) + ((dataPtr + 3 * nC)[2] * 4 + (dataPtr + 3 * nC + widthstep)[2] + (dataPtr + 3 * nC + 2 * widthstep)[2] + (dataPtr + 3 * nC + 3 * widthstep)[2]);
                    }
                }

                //core
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                dataPtr += 4 * nC + 4 * widthstep;
                dataPtr_d += 4 * nC + 4 * widthstep;
                index_widht = 4;
                index_height = 4;

                //first pixel
                sumBlue = (int)(
                               (dataPtr - 3 * widthstep - 3 * nC)[0] + (dataPtr - 3 * widthstep - 2 * nC)[0] + (dataPtr - 3 * widthstep - nC)[0] + (dataPtr - 3 * widthstep)[0] + (dataPtr - 3 * widthstep + nC)[0] + (dataPtr - 3 * widthstep + 2 * nC)[0] + (dataPtr - 3 * widthstep + 3 * nC)[0] +
                              +(dataPtr - 2 * widthstep - 3 * nC)[0] + (dataPtr - 2 * widthstep - 2 * nC)[0] + (dataPtr - 2 * widthstep - nC)[0] + (dataPtr - 2 * widthstep)[0] + (dataPtr - 2 * widthstep + nC)[0] + (dataPtr - 2 * widthstep + 2 * nC)[0] + (dataPtr - 2 * widthstep + 3 * nC)[0] +
                              +(dataPtr - widthstep - 3 * nC)[0] + (dataPtr - widthstep - 2 * nC)[0] + (dataPtr - widthstep - nC)[0] + (dataPtr - widthstep)[0] + (dataPtr - widthstep + nC)[0] + (dataPtr - widthstep + 2 * nC)[0] + (dataPtr - widthstep + 3 * nC)[0] +
                              +(dataPtr - 3 * nC)[0] + (dataPtr - 2 * nC)[0] + (dataPtr - nC)[0] + (dataPtr)[0] + (dataPtr + nC)[0] + (dataPtr + 2 * nC)[0] + (dataPtr + 3 * nC)[0] +
                              +(dataPtr + widthstep - 3 * nC)[0] + (dataPtr + widthstep - 2 * nC)[0] + (dataPtr + widthstep - nC)[0] + (dataPtr + widthstep)[0] + (dataPtr + widthstep + nC)[0] + (dataPtr + widthstep + 2 * nC)[0] + (dataPtr + widthstep + 3 * nC)[0] +
                              +(dataPtr + 2 * widthstep - 3 * nC)[0] + (dataPtr + 2 * widthstep - 2 * nC)[0] + (dataPtr + 2 * widthstep - nC)[0] + (dataPtr + 2 * widthstep)[0] + (dataPtr + 2 * widthstep + nC)[0] + (dataPtr + 2 * widthstep + 2 * nC)[0] + (dataPtr + 2 * widthstep + 3 * nC)[0] +
                              +(dataPtr + 3 * widthstep - 3 * nC)[0] + (dataPtr + 3 * widthstep - 2 * nC)[0] + (dataPtr + 3 * widthstep - nC)[0] + (dataPtr + 3 * widthstep)[0] + (dataPtr + 3 * widthstep + nC)[0] + (dataPtr + 3 * widthstep + 2 * nC)[0] + (dataPtr + 3 * widthstep + 3 * nC)[0]);

                sumGreen = (int)(
                                (dataPtr - 3 * widthstep - 3 * nC)[1] + (dataPtr - 3 * widthstep - 2 * nC)[1] + (dataPtr - 3 * widthstep - nC)[1] + (dataPtr - 3 * widthstep)[1] + (dataPtr - 3 * widthstep + nC)[1] + (dataPtr - 3 * widthstep + 2 * nC)[1] + (dataPtr - 3 * widthstep + 3 * nC)[1] +
                               +(dataPtr - 2 * widthstep - 3 * nC)[1] + (dataPtr - 2 * widthstep - 2 * nC)[1] + (dataPtr - 2 * widthstep - nC)[1] + (dataPtr - 2 * widthstep)[1] + (dataPtr - 2 * widthstep + nC)[1] + (dataPtr - 2 * widthstep + 2 * nC)[1] + (dataPtr - 2 * widthstep + 3 * nC)[1] +
                               +(dataPtr - widthstep - 3 * nC)[1] + (dataPtr - widthstep - 2 * nC)[1] + (dataPtr - widthstep - nC)[1] + (dataPtr - widthstep)[1] + (dataPtr - widthstep + nC)[1] + (dataPtr - widthstep + 2 * nC)[1] + (dataPtr - widthstep + 3 * nC)[1] +
                               +(dataPtr - 3 * nC)[1] + (dataPtr - 2 * nC)[1] + (dataPtr - nC)[1] + (dataPtr)[1] + (dataPtr + nC)[1] + (dataPtr + 2 * nC)[1] + (dataPtr + 3 * nC)[1] +
                               +(dataPtr + widthstep - 3 * nC)[1] + (dataPtr + widthstep - 2 * nC)[1] + (dataPtr + widthstep - nC)[1] + (dataPtr + widthstep)[1] + (dataPtr + widthstep + nC)[1] + (dataPtr + widthstep + 2 * nC)[1] + (dataPtr + widthstep + 3 * nC)[1] +
                               +(dataPtr + 2 * widthstep - 3 * nC)[1] + (dataPtr + 2 * widthstep - 2 * nC)[1] + (dataPtr + 2 * widthstep - nC)[1] + (dataPtr + 2 * widthstep)[1] + (dataPtr + 2 * widthstep + nC)[1] + (dataPtr + 2 * widthstep + 2 * nC)[1] + (dataPtr + 2 * widthstep + 3 * nC)[1] +
                               +(dataPtr + 3 * widthstep - 3 * nC)[1] + (dataPtr + 3 * widthstep - 2 * nC)[1] + (dataPtr + 3 * widthstep - nC)[1] + (dataPtr + 3 * widthstep)[1] + (dataPtr + 3 * widthstep + nC)[1] + (dataPtr + 3 * widthstep + 2 * nC)[1] + (dataPtr + 3 * widthstep + 3 * nC)[1]);

                sumRed = (int)(
                               (dataPtr - 3 * widthstep - 3 * nC)[2] + (dataPtr - 3 * widthstep - 2 * nC)[2] + (dataPtr - 3 * widthstep - nC)[2] + (dataPtr - 3 * widthstep)[2] + (dataPtr - 3 * widthstep + nC)[2] + (dataPtr - 3 * widthstep + 2 * nC)[2] + (dataPtr - 3 * widthstep + 3 * nC)[2] +
                              +(dataPtr - 2 * widthstep - 3 * nC)[2] + (dataPtr - 2 * widthstep - 2 * nC)[2] + (dataPtr - 2 * widthstep - nC)[2] + (dataPtr - 2 * widthstep)[2] + (dataPtr - 2 * widthstep + nC)[2] + (dataPtr - 2 * widthstep + 2 * nC)[2] + (dataPtr - 2 * widthstep + 3 * nC)[2] +
                              +(dataPtr - widthstep - 3 * nC)[2] + (dataPtr - widthstep - 2 * nC)[2] + (dataPtr - widthstep - nC)[2] + (dataPtr - widthstep)[2] + (dataPtr - widthstep + nC)[2] + (dataPtr - widthstep + 2 * nC)[2] + (dataPtr - widthstep + 3 * nC)[2] +
                              +(dataPtr - 3 * nC)[2] + (dataPtr - 2 * nC)[2] + (dataPtr - nC)[2] + (dataPtr)[2] + (dataPtr + nC)[2] + (dataPtr + 2 * nC)[2] + (dataPtr + 3 * nC)[2] +
                              +(dataPtr + widthstep - 3 * nC)[2] + (dataPtr + widthstep - 2 * nC)[2] + (dataPtr + widthstep - nC)[2] + (dataPtr + widthstep)[2] + (dataPtr + widthstep + nC)[2] + (dataPtr + widthstep + 2 * nC)[2] + (dataPtr + widthstep + 3 * nC)[2] +
                              +(dataPtr + 2 * widthstep - 3 * nC)[2] + (dataPtr + 2 * widthstep - 2 * nC)[2] + (dataPtr + 2 * widthstep - nC)[2] + (dataPtr + 2 * widthstep)[2] + (dataPtr + 2 * widthstep + nC)[2] + (dataPtr + 2 * widthstep + 2 * nC)[2] + (dataPtr + 2 * widthstep + 3 * nC)[2] +
                              +(dataPtr + 3 * widthstep - 3 * nC)[2] + (dataPtr + 3 * widthstep - 2 * nC)[2] + (dataPtr + 3 * widthstep - nC)[2] + (dataPtr + 3 * widthstep)[2] + (dataPtr + 3 * widthstep + nC)[2] + (dataPtr + 3 * widthstep + 2 * nC)[2] + (dataPtr + 3 * widthstep + 3 * nC)[2]);

                // store in the image 
                dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                newimage_blue[index_widht, index_height] = sumBlue;
                newimage_green[index_widht, index_height] = sumGreen;
                newimage_red[index_widht, index_height] = sumRed;

                dataPtr += nC;
                dataPtr_d += nC;
                index_widht++;

                //first row
                for (int x = 5; x < widht - 3; x++)
                {
                    sumBlue = (int)newimage_blue[index_widht - 1, index_height] - ((dataPtr - 4 * nC - 3 * widthstep)[0] + (dataPtr - 4 * nC - 2 * widthstep)[0] + (dataPtr - 4 * nC - widthstep)[0] + (dataPtr - 4 * nC)[0] + (dataPtr - 4 * nC + widthstep)[0] + (dataPtr - 4 * nC + 2 * widthstep)[0] + (dataPtr - 4 * nC + 3 * widthstep)[0]) + ((dataPtr + 3 * nC - 3 * widthstep)[0] + (dataPtr + 3 * nC - 2 * widthstep)[0] + (dataPtr + 3 * nC - widthstep)[0] + (dataPtr + 3 * nC)[0] + (dataPtr + 3 * nC + widthstep)[0] + (dataPtr + 3 * nC + 2 * widthstep)[0] + (dataPtr + 3 * nC + 3 * widthstep)[0]);
                    sumGreen = (int)newimage_green[index_widht - 1, index_height] - ((dataPtr - 4 * nC - 3 * widthstep)[1] + (dataPtr - 4 * nC - 2 * widthstep)[1] + (dataPtr - 4 * nC - widthstep)[1] + (dataPtr - 4 * nC)[1] + (dataPtr - 4 * nC + widthstep)[1] + (dataPtr - 4 * nC + 2 * widthstep)[1] + (dataPtr - 4 * nC + 3 * widthstep)[1]) + ((dataPtr + 3 * nC - 3 * widthstep)[1] + (dataPtr + 3 * nC - 2 * widthstep)[1] + (dataPtr + 3 * nC - widthstep)[1] + (dataPtr + 3 * nC)[1] + (dataPtr + 3 * nC + widthstep)[1] + (dataPtr + 3 * nC + 2 * widthstep)[1] + (dataPtr + 3 * nC + 3 * widthstep)[1]);
                    sumRed = (int)newimage_red[index_widht - 1, index_height] - ((dataPtr - 4 * nC - 3 * widthstep)[2] + (dataPtr - 4 * nC - 2 * widthstep)[2] + (dataPtr - 4 * nC - widthstep)[2] + (dataPtr - 4 * nC)[2] + (dataPtr - 4 * nC + widthstep)[2] + (dataPtr - 4 * nC + 2 * widthstep)[2] + (dataPtr - 4 * nC + 3 * widthstep)[2]) + ((dataPtr + 3 * nC - 3 * widthstep)[2] + (dataPtr + 3 * nC - 2 * widthstep)[2] + (dataPtr + 3 * nC - widthstep)[2] + (dataPtr + 3 * nC)[2] + (dataPtr + 3 * nC + widthstep)[2] + (dataPtr + 3 * nC + 2 * widthstep)[2] + (dataPtr + 3 * nC + 3 * widthstep)[2]);

                    // store in the image 
                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                    newimage_blue[index_widht, index_height] = sumBlue;
                    newimage_green[index_widht, index_height] = sumGreen;
                    newimage_red[index_widht, index_height] = sumRed;

                    dataPtr += nC;
                    dataPtr_d += nC;
                    index_widht++;
                }

                //first collumn
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                dataPtr += 4 * nC + 5 * widthstep;
                dataPtr_d += 4 * nC + 5 * widthstep;
                index_widht = 4;
                index_height = 5;

                for (int y = 5; y < height - 3; y++)
                {
                    sumBlue = (int)newimage_blue[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep - 3 * nC)[0] + (dataPtr - 4 * widthstep - 2 * nC)[0] + (dataPtr - 4 * widthstep - nC)[0] + (dataPtr - 4 * widthstep)[0] + (dataPtr - 4 * widthstep + nC)[0] + (dataPtr - 4 * widthstep + 2 * nC)[0] + (dataPtr - 4 * widthstep + 3 * nC)[0]) + ((dataPtr + 3 * widthstep - 3 * nC)[0] + (dataPtr + 3 * widthstep - 2 * nC)[0] + (dataPtr + 3 * widthstep - nC)[0] + (dataPtr + 3 * widthstep)[0] + (dataPtr + 3 * widthstep + nC)[0] + (dataPtr + 3 * widthstep + 2 * nC)[0] + (dataPtr + 3 * widthstep + 3 * nC)[0]);
                    sumGreen = (int)newimage_green[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep - 3 * nC)[1] + (dataPtr - 4 * widthstep - 2 * nC)[1] + (dataPtr - 4 * widthstep - nC)[1] + (dataPtr - 4 * widthstep)[1] + (dataPtr - 4 * widthstep + nC)[1] + (dataPtr - 4 * widthstep + 2 * nC)[1] + (dataPtr - 4 * widthstep + 3 * nC)[1]) + ((dataPtr + 3 * widthstep - 3 * nC)[1] + (dataPtr + 3 * widthstep - 2 * nC)[1] + (dataPtr + 3 * widthstep - nC)[1] + (dataPtr + 3 * widthstep)[1] + (dataPtr + 3 * widthstep + nC)[1] + (dataPtr + 3 * widthstep + 2 * nC)[1] + (dataPtr + 3 * widthstep + 3 * nC)[1]);
                    sumRed = (int)newimage_red[index_widht, index_height - 1] - ((dataPtr - 4 * widthstep - 3 * nC)[2] + (dataPtr - 4 * widthstep - 2 * nC)[2] + (dataPtr - 4 * widthstep - nC)[2] + (dataPtr - 4 * widthstep)[2] + (dataPtr - 4 * widthstep + nC)[2] + (dataPtr - 4 * widthstep + 2 * nC)[2] + (dataPtr - 4 * widthstep + 3 * nC)[2]) + ((dataPtr + 3 * widthstep - 3 * nC)[2] + (dataPtr + 3 * widthstep - 2 * nC)[2] + (dataPtr + 3 * widthstep - nC)[2] + (dataPtr + 3 * widthstep)[2] + (dataPtr + 3 * widthstep + nC)[2] + (dataPtr + 3 * widthstep + 2 * nC)[2] + (dataPtr + 3 * widthstep + 3 * nC)[2]);

                    // store in the image 
                    dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                    dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                    dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                    newimage_blue[index_widht, index_height] = sumBlue;
                    newimage_green[index_widht, index_height] = sumGreen;
                    newimage_red[index_widht, index_height] = sumRed;

                    dataPtr += widthstep;
                    dataPtr_d += widthstep;
                    index_height++;
                }

                //rest of the core
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                dataPtr += 5 * nC + 5 * widthstep;
                dataPtr_d += 5 * nC + 5 * widthstep;
                index_widht = 5;
                index_height = 5;

                for (int y = 5; y < height - 3; y++)
                {
                    for (int x = 6; x < widht - 2; x++)
                    {
                        sumBlue = newimage_blue[index_widht, index_height - 1] - newimage_blue[index_widht - 1, index_height - 1] + newimage_blue[index_widht - 1, index_height] + (dataPtr - 4 * widthstep - 4 * nC)[0] - (dataPtr + 3 * widthstep - 4 * nC)[0] - (dataPtr - 4 * widthstep + 3 * nC)[0] + (dataPtr + 3 * widthstep + 3 * nC)[0];
                        sumGreen = newimage_green[index_widht, index_height - 1] - newimage_green[index_widht - 1, index_height - 1] + newimage_green[index_widht - 1, index_height] + (dataPtr - 4 * widthstep - 4 * nC)[1] - (dataPtr + 3 * widthstep - 4 * nC)[1] - (dataPtr - 4 * widthstep + 3 * nC)[1] + (dataPtr + 3 * widthstep + 3 * nC)[1];
                        sumRed = newimage_red[index_widht, index_height - 1] - newimage_red[index_widht - 1, index_height - 1] + newimage_red[index_widht - 1, index_height] + (dataPtr - 4 * widthstep - 4 * nC)[2] - (dataPtr + 3 * widthstep - 4 * nC)[2] - (dataPtr - 4 * widthstep + 3 * nC)[2] + (dataPtr + 3 * widthstep + 3 * nC)[2];

                        // store in the image 
                        dataPtr_d[0] = (byte)Math.Round(sumBlue / 49.0);
                        dataPtr_d[1] = (byte)Math.Round(sumGreen / 49.0);
                        dataPtr_d[2] = (byte)Math.Round(sumRed / 49.0);

                        newimage_blue[index_widht, index_height] = sumBlue;
                        newimage_green[index_widht, index_height] = sumGreen;
                        newimage_red[index_widht, index_height] = sumRed;

                        dataPtr += nC;
                        dataPtr_d += nC;
                        index_widht++;
                    }
                    dataPtr_d += padding + 8 * nC;
                    dataPtr += padding + 8 * nC;
                    index_height++;
                    index_widht = 5;
                }
            }
        }

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight)
        {
            unsafe
            {
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int widht = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                int sumBlue, sumGreen, sumRed;
                int blue, red, green;

                dataPtr += widthstep + nC;
                dataPtr_d += widthstep + nC;

                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < widht - 1; x++)
                    {
                        sumBlue = (int)((dataPtr - widthstep - nC)[0] * matrix[0, 0] + (dataPtr - widthstep)[0] * matrix[1, 0] + (dataPtr - widthstep + nC)[0] * matrix[2, 0] + (dataPtr - nC)[0] * matrix[0, 1] + dataPtr[0] * matrix[1, 1] + (dataPtr + nC)[0] * matrix[2, 1] + (dataPtr + widthstep - nC)[0] * matrix[0, 2] + (dataPtr + widthstep)[0] * matrix[1, 2] + (dataPtr + widthstep + nC)[0] * matrix[2, 2]);
                        sumGreen = (int)((dataPtr - widthstep - nC)[1] * matrix[0, 0] + (dataPtr - widthstep)[1] * matrix[1, 0] + (dataPtr - widthstep + nC)[1] * matrix[2, 0] + (dataPtr - nC)[1] * matrix[0, 1] + dataPtr[1] * matrix[1, 1] + (dataPtr + nC)[1] * matrix[2, 1] + (dataPtr + widthstep - nC)[1] * matrix[0, 2] + (dataPtr + widthstep)[1] * matrix[1, 2] + (dataPtr + widthstep + nC)[1] * matrix[2, 2]);
                        sumRed = (int)((dataPtr - widthstep - nC)[2] * matrix[0, 0] + (dataPtr - widthstep)[2] * matrix[1, 0] + (dataPtr - widthstep + nC)[2] * matrix[2, 0] + (dataPtr - nC)[2] * matrix[0, 1] + dataPtr[2] * matrix[1, 1] + (dataPtr + nC)[2] * matrix[2, 1] + (dataPtr + widthstep - nC)[2] * matrix[0, 2] + (dataPtr + widthstep)[2] * matrix[1, 2] + (dataPtr + widthstep + nC)[2] * matrix[2, 2]);

                        blue = (int)Math.Round(sumBlue / matrixWeight);
                        if (blue > 255) blue = 255;
                        if (blue < 0) blue = 0;

                        green = (int)Math.Round(sumGreen / matrixWeight);
                        if (green > 255) green = 255;
                        if (green < 0) green = 0;

                        red = (int)Math.Round(sumRed / matrixWeight);
                        if (red > 255) red = 255;
                        if (red < 0) red = 0;

                        // store in the image
                        dataPtr_d[0] = (byte)blue;
                        dataPtr_d[1] = (byte)green;
                        dataPtr_d[2] = (byte)red;

                        // advance the pointer to the next pixel
                        dataPtr_d += nC;
                        dataPtr += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding + 2 * nC;
                    dataPtr += padding + 2 * nC;
                }

                //Canto Superior Esquerdo
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                sumBlue = (int)((dataPtr)[0] * matrix[0, 0] + (dataPtr)[0] * matrix[1, 0] + (dataPtr + nC)[0] * matrix[2, 0] + (dataPtr)[0] * matrix[0, 1] + dataPtr[0] * matrix[1, 1] + (dataPtr + nC)[0] * matrix[2, 1] + (dataPtr + widthstep)[0] * matrix[0, 2] + (dataPtr + widthstep)[0] * matrix[1, 2] + (dataPtr + widthstep + nC)[0] * matrix[2, 2]);
                sumGreen = (int)((dataPtr)[1] * matrix[0, 0] + (dataPtr)[1] * matrix[1, 0] + (dataPtr + nC)[1] * matrix[2, 0] + (dataPtr)[1] * matrix[0, 1] + dataPtr[1] * matrix[1, 1] + (dataPtr + nC)[1] * matrix[2, 1] + (dataPtr + widthstep)[1] * matrix[0, 2] + (dataPtr + widthstep)[1] * matrix[1, 2] + (dataPtr + widthstep + nC)[1] * matrix[2, 2]);
                sumRed = (int)((dataPtr)[2] * matrix[0, 0] + (dataPtr)[2] * matrix[1, 0] + (dataPtr + nC)[2] * matrix[2, 0] + (dataPtr)[2] * matrix[0, 1] + dataPtr[2] * matrix[1, 1] + (dataPtr + nC)[2] * matrix[2, 1] + (dataPtr + widthstep)[2] * matrix[0, 2] + (dataPtr + widthstep)[2] * matrix[1, 2] + (dataPtr + widthstep + nC)[2] * matrix[2, 2]);

                blue = (int)Math.Round(sumBlue / matrixWeight);
                if (blue > 255) blue = 255;
                if (blue < 0) blue = 0;

                green = (int)Math.Round(sumGreen / matrixWeight);
                if (green > 255) green = 255;
                if (green < 0) green = 0;

                red = (int)Math.Round(sumRed / matrixWeight);
                if (red > 255) red = 255;
                if (red < 0) red = 0;

                // store in the image
                dataPtr_d[0] = (byte)blue;
                dataPtr_d[1] = (byte)green;
                dataPtr_d[2] = (byte)red;

                dataPtr_d += nC;
                dataPtr += nC;

                //Margem Superior
                for (int x_ms = 1; x_ms < widht - 1; x_ms++)
                {
                    sumBlue = (int)((dataPtr - nC)[0] * matrix[0, 0] + (dataPtr)[0] * matrix[1, 0] + (dataPtr + nC)[0] * matrix[2, 0] + (dataPtr - nC)[0] * matrix[0, 1] + dataPtr[0] * matrix[1, 1] + (dataPtr + nC)[0] * matrix[2, 1] + (dataPtr + widthstep - nC)[0] * matrix[0, 2] + (dataPtr + widthstep)[0] * matrix[1, 2] + (dataPtr + widthstep + nC)[0] * matrix[2, 2]);
                    sumGreen = (int)((dataPtr - nC)[1] * matrix[0, 0] + (dataPtr)[1] * matrix[1, 0] + (dataPtr + nC)[1] * matrix[2, 0] + (dataPtr - nC)[1] * matrix[0, 1] + dataPtr[1] * matrix[1, 1] + (dataPtr + nC)[1] * matrix[2, 1] + (dataPtr + widthstep - nC)[1] * matrix[0, 2] + (dataPtr + widthstep)[1] * matrix[1, 2] + (dataPtr + widthstep + nC)[1] * matrix[2, 2]);
                    sumRed = (int)((dataPtr - nC)[2] * matrix[0, 0] + (dataPtr)[2] * matrix[1, 0] + (dataPtr + nC)[2] * matrix[2, 0] + (dataPtr - nC)[2] * matrix[0, 1] + dataPtr[2] * matrix[1, 1] + (dataPtr + nC)[2] * matrix[2, 1] + (dataPtr + widthstep - nC)[2] * matrix[0, 2] + (dataPtr + widthstep)[2] * matrix[1, 2] + (dataPtr + widthstep + nC)[2] * matrix[2, 2]);

                    blue = (int)Math.Round(sumBlue / matrixWeight);
                    if (blue > 255) blue = 255;
                    if (blue < 0) blue = 0;

                    green = (int)Math.Round(sumGreen / matrixWeight);
                    if (green > 255) green = 255;
                    if (green < 0) green = 0;

                    red = (int)Math.Round(sumRed / matrixWeight);
                    if (red > 255) red = 255;
                    if (red < 0) red = 0;

                    // store in the image
                    dataPtr_d[0] = (byte)blue;
                    dataPtr_d[1] = (byte)green;
                    dataPtr_d[2] = (byte)red;

                    dataPtr_d += nC;
                    dataPtr += nC;
                }

                //Canto Superior Direito
                sumBlue = (int)((dataPtr - nC)[0] * matrix[0, 0] + (dataPtr)[0] * matrix[1, 0] + (dataPtr)[0] * matrix[2, 0] + (dataPtr - nC)[0] * matrix[0, 1] + dataPtr[0] * matrix[1, 1] + (dataPtr)[0] * matrix[2, 1] + (dataPtr + widthstep - nC)[0] * matrix[0, 2] + (dataPtr + widthstep)[0] * matrix[1, 2] + (dataPtr + widthstep)[0] * matrix[2, 2]);
                sumGreen = (int)((dataPtr - nC)[1] * matrix[0, 0] + (dataPtr)[1] * matrix[1, 0] + (dataPtr)[1] * matrix[2, 0] + (dataPtr - nC)[1] * matrix[0, 1] + dataPtr[1] * matrix[1, 1] + (dataPtr)[1] * matrix[2, 1] + (dataPtr + widthstep - nC)[1] * matrix[0, 2] + (dataPtr + widthstep)[1] * matrix[1, 2] + (dataPtr + widthstep)[1] * matrix[2, 2]);
                sumRed = (int)((dataPtr - nC)[2] * matrix[0, 0] + (dataPtr)[2] * matrix[1, 0] + (dataPtr)[2] * matrix[2, 0] + (dataPtr - nC)[2] * matrix[0, 1] + dataPtr[2] * matrix[1, 1] + (dataPtr)[2] * matrix[2, 1] + (dataPtr + widthstep - nC)[2] * matrix[0, 2] + (dataPtr + widthstep)[2] * matrix[1, 2] + (dataPtr + widthstep)[2] * matrix[2, 2]);

                blue = (int)Math.Round(sumBlue / matrixWeight);
                if (blue > 255) blue = 255;
                if (blue < 0) blue = 0;

                green = (int)Math.Round(sumGreen / matrixWeight);
                if (green > 255) green = 255;
                if (green < 0) green = 0;

                red = (int)Math.Round(sumRed / matrixWeight);
                if (red > 255) red = 255;
                if (red < 0) red = 0;

                // store in the image
                dataPtr_d[0] = (byte)blue;
                dataPtr_d[1] = (byte)green;
                dataPtr_d[2] = (byte)red;

                dataPtr_d += widthstep;
                dataPtr += widthstep;

                //Margem direita
                for (int y_md = 1; y_md < height - 1; y_md++)
                {
                    sumBlue = (int)((dataPtr - nC - widthstep)[0] * matrix[0, 0] + (dataPtr - widthstep)[0] * matrix[1, 0] + (dataPtr - widthstep)[0] * matrix[2, 0] + (dataPtr - nC)[0] * matrix[0, 1] + dataPtr[0] * matrix[1, 1] + (dataPtr)[0] * matrix[2, 1] + (dataPtr + widthstep - nC)[0] * matrix[0, 2] + (dataPtr + widthstep)[0] * matrix[1, 2] + (dataPtr + widthstep)[0] * matrix[2, 2]);
                    sumGreen = (int)((dataPtr - nC - widthstep)[1] * matrix[0, 0] + (dataPtr - widthstep)[1] * matrix[1, 0] + (dataPtr - widthstep)[1] * matrix[2, 0] + (dataPtr - nC)[1] * matrix[0, 1] + dataPtr[1] * matrix[1, 1] + (dataPtr)[1] * matrix[2, 1] + (dataPtr + widthstep - nC)[1] * matrix[0, 2] + (dataPtr + widthstep)[1] * matrix[1, 2] + (dataPtr + widthstep)[1] * matrix[2, 2]);
                    sumRed = (int)((dataPtr - nC - widthstep)[2] * matrix[0, 0] + (dataPtr - widthstep)[2] * matrix[1, 0] + (dataPtr - widthstep)[2] * matrix[2, 0] + (dataPtr - nC)[2] * matrix[0, 1] + dataPtr[2] * matrix[1, 1] + (dataPtr)[2] * matrix[2, 1] + (dataPtr + widthstep - nC)[2] * matrix[0, 2] + (dataPtr + widthstep)[2] * matrix[1, 2] + (dataPtr + widthstep)[2] * matrix[2, 2]);

                    blue = (int)Math.Round(sumBlue / matrixWeight);
                    if (blue > 255) blue = 255;
                    if (blue < 0) blue = 0;

                    green = (int)Math.Round(sumGreen / matrixWeight);
                    if (green > 255) green = 255;
                    if (green < 0) green = 0;

                    red = (int)Math.Round(sumRed / matrixWeight);
                    if (red > 255) red = 255;
                    if (red < 0) red = 0;

                    // store in the image
                    dataPtr_d[0] = (byte)blue;
                    dataPtr_d[1] = (byte)green;
                    dataPtr_d[2] = (byte)red;

                    dataPtr_d += widthstep;
                    dataPtr += widthstep;
                }

                //Canto Inferior Direito
                sumBlue = (int)((dataPtr - nC - widthstep)[0] * matrix[0, 0] + (dataPtr - widthstep)[0] * matrix[1, 0] + (dataPtr - widthstep)[0] * matrix[2, 0] + (dataPtr - nC)[0] * matrix[0, 1] + dataPtr[0] * matrix[1, 1] + (dataPtr)[0] * matrix[2, 1] + (dataPtr - nC)[0] * matrix[0, 2] + (dataPtr)[0] * matrix[1, 2] + (dataPtr)[0] * matrix[2, 2]);
                sumGreen = (int)((dataPtr - nC - widthstep)[1] * matrix[0, 0] + (dataPtr - widthstep)[1] * matrix[1, 0] + (dataPtr - widthstep)[1] * matrix[2, 0] + (dataPtr - nC)[1] * matrix[0, 1] + dataPtr[1] * matrix[1, 1] + (dataPtr)[1] * matrix[2, 1] + (dataPtr - nC)[1] * matrix[0, 2] + (dataPtr)[1] * matrix[1, 2] + (dataPtr)[1] * matrix[2, 2]);
                sumRed = (int)((dataPtr - nC - widthstep)[2] * matrix[0, 0] + (dataPtr - widthstep)[2] * matrix[1, 0] + (dataPtr - widthstep)[2] * matrix[2, 0] + (dataPtr - nC)[2] * matrix[0, 1] + dataPtr[2] * matrix[1, 1] + (dataPtr)[2] * matrix[2, 1] + (dataPtr - nC)[2] * matrix[0, 2] + (dataPtr)[2] * matrix[1, 2] + (dataPtr)[2] * matrix[2, 2]);

                blue = (int)Math.Round(sumBlue / matrixWeight);
                if (blue > 255) blue = 255;
                if (blue < 0) blue = 0;

                green = (int)Math.Round(sumGreen / matrixWeight);
                if (green > 255) green = 255;
                if (green < 0) green = 0;

                red = (int)Math.Round(sumRed / matrixWeight);
                if (red > 255) red = 255;
                if (red < 0) red = 0;

                // store in the image
                dataPtr_d[0] = (byte)blue;
                dataPtr_d[1] = (byte)green;
                dataPtr_d[2] = (byte)red;

                dataPtr_d -= nC;
                dataPtr -= nC;

                //Margem inferior
                for (int x_mi = 1; x_mi < widht - 1; x_mi++)
                {
                    sumBlue = (int)((dataPtr - nC - widthstep)[0] * matrix[0, 0] + (dataPtr - widthstep)[0] * matrix[1, 0] + (dataPtr - widthstep + nC)[0] * matrix[2, 0] + (dataPtr - nC)[0] * matrix[0, 1] + dataPtr[0] * matrix[1, 1] + (dataPtr + nC)[0] * matrix[2, 1] + (dataPtr - nC)[0] * matrix[0, 2] + (dataPtr)[0] * matrix[1, 2] + (dataPtr + nC)[0] * matrix[2, 2]);
                    sumGreen = (int)((dataPtr - nC - widthstep)[1] * matrix[0, 0] + (dataPtr - widthstep)[1] * matrix[1, 0] + (dataPtr - widthstep + nC)[1] * matrix[2, 0] + (dataPtr - nC)[1] * matrix[0, 1] + dataPtr[1] * matrix[1, 1] + (dataPtr + nC)[1] * matrix[2, 1] + (dataPtr - nC)[1] * matrix[0, 2] + (dataPtr)[1] * matrix[1, 2] + (dataPtr + nC)[1] * matrix[2, 2]);
                    sumRed = (int)((dataPtr - nC - widthstep)[2] * matrix[0, 0] + (dataPtr - widthstep)[2] * matrix[1, 0] + (dataPtr - widthstep + nC)[2] * matrix[2, 0] + (dataPtr - nC)[2] * matrix[0, 1] + dataPtr[2] * matrix[1, 1] + (dataPtr + nC)[2] * matrix[2, 1] + (dataPtr - nC)[2] * matrix[0, 2] + (dataPtr)[2] * matrix[1, 2] + (dataPtr + nC)[2] * matrix[2, 2]);

                    blue = (int)Math.Round(sumBlue / matrixWeight);
                    if (blue > 255) blue = 255;
                    if (blue < 0) blue = 0;

                    green = (int)Math.Round(sumGreen / matrixWeight);
                    if (green > 255) green = 255;
                    if (green < 0) green = 0;

                    red = (int)Math.Round(sumRed / matrixWeight);
                    if (red > 255) red = 255;
                    if (red < 0) red = 0;

                    // store in the image
                    dataPtr_d[0] = (byte)blue;
                    dataPtr_d[1] = (byte)green;
                    dataPtr_d[2] = (byte)red;

                    dataPtr_d -= nC;
                    dataPtr -= nC;
                }

                //Canto Inferior Esquerdo
                sumBlue = (int)((dataPtr - widthstep)[0] * matrix[0, 0] + (dataPtr - widthstep)[0] * matrix[1, 0] + (dataPtr - widthstep + nC)[0] * matrix[2, 0] + (dataPtr)[0] * matrix[0, 1] + dataPtr[0] * matrix[1, 1] + (dataPtr + nC)[0] * matrix[2, 1] + (dataPtr)[0] * matrix[0, 2] + (dataPtr)[0] * matrix[1, 2] + (dataPtr + nC)[0] * matrix[2, 2]);
                sumGreen = (int)((dataPtr - widthstep)[1] * matrix[0, 0] + (dataPtr - widthstep)[1] * matrix[1, 0] + (dataPtr - widthstep + nC)[1] * matrix[2, 0] + (dataPtr)[1] * matrix[0, 1] + dataPtr[1] * matrix[1, 1] + (dataPtr + nC)[1] * matrix[2, 1] + (dataPtr)[1] * matrix[0, 2] + (dataPtr)[1] * matrix[1, 2] + (dataPtr + nC)[1] * matrix[2, 2]);
                sumRed = (int)((dataPtr - widthstep)[2] * matrix[0, 0] + (dataPtr - widthstep)[2] * matrix[1, 0] + (dataPtr - widthstep + nC)[2] * matrix[2, 0] + (dataPtr)[2] * matrix[0, 1] + dataPtr[2] * matrix[1, 1] + (dataPtr + nC)[2] * matrix[2, 1] + (dataPtr)[2] * matrix[0, 2] + (dataPtr)[2] * matrix[1, 2] + (dataPtr + nC)[2] * matrix[2, 2]);

                blue = (int)Math.Round(sumBlue / matrixWeight);
                if (blue > 255) blue = 255;
                if (blue < 0) blue = 0;

                green = (int)Math.Round(sumGreen / matrixWeight);
                if (green > 255) green = 255;
                if (green < 0) green = 0;

                red = (int)Math.Round(sumRed / matrixWeight);
                if (red > 255) red = 255;
                if (red < 0) red = 0;

                // store in the image
                dataPtr_d[0] = (byte)blue;
                dataPtr_d[1] = (byte)green;
                dataPtr_d[2] = (byte)red;

                dataPtr_d -= widthstep;
                dataPtr -= widthstep;

                //Margem esquerda
                for (int y_me = 1; y_me < height - 1; y_me++)
                {
                    sumBlue = (int)((dataPtr - widthstep)[0] * matrix[0, 0] + (dataPtr - widthstep)[0] * matrix[1, 0] + (dataPtr - widthstep + nC)[0] * matrix[2, 0] + (dataPtr)[0] * matrix[0, 1] + dataPtr[0] * matrix[1, 1] + (dataPtr + nC)[0] * matrix[2, 1] + (dataPtr + widthstep)[0] * matrix[0, 2] + (dataPtr + widthstep)[0] * matrix[1, 2] + (dataPtr + nC + widthstep)[0] * matrix[2, 2]);
                    sumGreen = (int)((dataPtr - widthstep)[1] * matrix[0, 0] + (dataPtr - widthstep)[1] * matrix[1, 0] + (dataPtr - widthstep + nC)[1] * matrix[2, 0] + (dataPtr)[1] * matrix[0, 1] + dataPtr[1] * matrix[1, 1] + (dataPtr + nC)[1] * matrix[2, 1] + (dataPtr + widthstep)[1] * matrix[0, 2] + (dataPtr + widthstep)[1] * matrix[1, 2] + (dataPtr + nC + widthstep)[1] * matrix[2, 2]);
                    sumRed = (int)((dataPtr - widthstep)[2] * matrix[0, 0] + (dataPtr - widthstep)[2] * matrix[1, 0] + (dataPtr - widthstep + nC)[2] * matrix[2, 0] + (dataPtr)[2] * matrix[0, 1] + dataPtr[2] * matrix[1, 1] + (dataPtr + nC)[2] * matrix[2, 1] + (dataPtr + widthstep)[2] * matrix[0, 2] + (dataPtr + widthstep)[2] * matrix[1, 2] + (dataPtr + nC + widthstep)[2] * matrix[2, 2]);

                    blue = (int)Math.Round(sumBlue / matrixWeight);
                    if (blue > 255) blue = 255;
                    if (blue < 0) blue = 0;

                    green = (int)Math.Round(sumGreen / matrixWeight);
                    if (green > 255) green = 255;
                    if (green < 0) green = 0;

                    red = (int)Math.Round(sumRed / matrixWeight);
                    if (red > 255) red = 255;
                    if (red < 0) red = 0;

                    // store in the image
                    dataPtr_d[0] = (byte)blue;
                    dataPtr_d[1] = (byte)green;
                    dataPtr_d[2] = (byte)red;

                    dataPtr_d -= widthstep;
                    dataPtr -= widthstep;
                }
            }
        }

        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int widht = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                int SxBlue, SxGreen, SxRed;
                int SyBlue, SyGreen, SyRed;
                int SBlue, SGreen, SRed;
                int y, x;

                dataPtr += widthstep + nC;
                dataPtr_d += widthstep + nC;

                //core
                for (y = 1; y < height - 1; y++)
                {
                    for (x = 1; x < widht - 1; x++)
                    {
                        SxBlue = ((dataPtr - nC - widthstep)[0] + (dataPtr - nC)[0] * 2 + (dataPtr - nC + widthstep)[0]) - ((dataPtr + nC - widthstep)[0] + (dataPtr + nC)[0] * 2 + (dataPtr + nC + widthstep)[0]);
                        SxGreen = ((dataPtr - nC - widthstep)[1] + (dataPtr - nC)[1] * 2 + (dataPtr - nC + widthstep)[1]) - ((dataPtr + nC - widthstep)[1] + (dataPtr + nC)[1] * 2 + (dataPtr + nC + widthstep)[1]);
                        SxRed = ((dataPtr - nC - widthstep)[2] + (dataPtr - nC)[2] * 2 + (dataPtr - nC + widthstep)[2]) - ((dataPtr + nC - widthstep)[2] + (dataPtr + nC)[2] * 2 + (dataPtr + nC + widthstep)[2]);

                        SyBlue = ((dataPtr - nC + widthstep)[0] + (dataPtr + widthstep)[0] * 2 + (dataPtr + nC + widthstep)[0]) - ((dataPtr - nC - widthstep)[0] + (dataPtr - widthstep)[0] * 2 + (dataPtr + nC - widthstep)[0]);
                        SyGreen = ((dataPtr - nC + widthstep)[1] + (dataPtr + widthstep)[1] * 2 + (dataPtr + nC + widthstep)[1]) - ((dataPtr - nC - widthstep)[1] + (dataPtr - widthstep)[1] * 2 + (dataPtr + nC - widthstep)[1]);
                        SyRed = ((dataPtr - nC + widthstep)[2] + (dataPtr + widthstep)[2] * 2 + (dataPtr + nC + widthstep)[2]) - ((dataPtr - nC - widthstep)[2] + (dataPtr - widthstep)[2] * 2 + (dataPtr + nC - widthstep)[2]);

                        SBlue = Math.Abs(SxBlue) + Math.Abs(SyBlue);
                        SGreen = Math.Abs(SxGreen) + Math.Abs(SyGreen);
                        SRed = Math.Abs(SxRed) + Math.Abs(SyRed);

                        if (SBlue > 255) SBlue = 255;
                        if (SGreen > 255) SGreen = 255;
                        if (SRed > 255) SRed = 255;

                        // store in the image
                        dataPtr_d[0] = (byte)SBlue;
                        dataPtr_d[1] = (byte)SGreen;
                        dataPtr_d[2] = (byte)SRed;

                        dataPtr += nC;
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding + 2 * nC;
                    dataPtr += padding + 2 * nC;
                }

                //canto superior esquerdo
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                SxBlue = ((dataPtr)[0] + (dataPtr)[0] * 2 + (dataPtr + widthstep)[0]) - ((dataPtr + nC)[0] + (dataPtr + nC)[0] * 2 + (dataPtr + nC + widthstep)[0]);
                SxGreen = ((dataPtr)[1] + (dataPtr)[1] * 2 + (dataPtr + widthstep)[1]) - ((dataPtr + nC)[1] + (dataPtr + nC)[1] * 2 + (dataPtr + nC + widthstep)[1]);
                SxRed = ((dataPtr)[2] + (dataPtr)[2] * 2 + (dataPtr + widthstep)[2]) - ((dataPtr + nC)[2] + (dataPtr + nC)[2] * 2 + (dataPtr + nC + widthstep)[2]);

                SyBlue = ((dataPtr + widthstep)[0] + (dataPtr + widthstep)[0] * 2 + (dataPtr + nC + widthstep)[0]) - ((dataPtr)[0] + (dataPtr)[0] * 2 + (dataPtr + nC)[0]);
                SyGreen = ((dataPtr + widthstep)[1] + (dataPtr + widthstep)[1] * 2 + (dataPtr + nC + widthstep)[1]) - ((dataPtr)[1] + (dataPtr)[1] * 2 + (dataPtr + nC)[1]);
                SyRed = ((dataPtr + widthstep)[2] + (dataPtr + widthstep)[2] * 2 + (dataPtr + nC + widthstep)[2]) - ((dataPtr)[2] + (dataPtr)[2] * 2 + (dataPtr + nC)[2]);

                SBlue = Math.Abs(SxBlue) + Math.Abs(SyBlue);
                SGreen = Math.Abs(SxGreen) + Math.Abs(SyGreen);
                SRed = Math.Abs(SxRed) + Math.Abs(SyRed);

                if (SBlue > 255) SBlue = 255;
                if (SGreen > 255) SGreen = 255;
                if (SRed > 255) SRed = 255;

                // store in the image
                dataPtr_d[0] = (byte)SBlue;
                dataPtr_d[1] = (byte)SGreen;
                dataPtr_d[2] = (byte)SRed;

                dataPtr += nC;
                dataPtr_d += nC;

                //margem superior
                for (int x_sup = 1; x_sup < widht - 1; x_sup++)
                {
                    SxBlue = ((dataPtr - nC)[0] + (dataPtr - nC)[0] * 2 + (dataPtr + widthstep - nC)[0]) - ((dataPtr + nC)[0] + (dataPtr + nC)[0] * 2 + (dataPtr + nC + widthstep)[0]);
                    SxGreen = ((dataPtr - nC)[1] + (dataPtr - nC)[1] * 2 + (dataPtr + widthstep - nC)[1]) - ((dataPtr + nC)[1] + (dataPtr + nC)[1] * 2 + (dataPtr + nC + widthstep)[1]);
                    SxRed = ((dataPtr - nC)[2] + (dataPtr - nC)[2] * 2 + (dataPtr + widthstep - nC)[2]) - ((dataPtr + nC)[2] + (dataPtr + nC)[2] * 2 + (dataPtr + nC + widthstep)[2]);

                    SyBlue = ((dataPtr + widthstep - nC)[0] + (dataPtr + widthstep)[0] * 2 + (dataPtr + nC + widthstep)[0]) - ((dataPtr - nC)[0] + (dataPtr)[0] * 2 + (dataPtr + nC)[0]);
                    SyGreen = ((dataPtr + widthstep - nC)[1] + (dataPtr + widthstep)[1] * 2 + (dataPtr + nC + widthstep)[1]) - ((dataPtr - nC)[1] + (dataPtr)[1] * 2 + (dataPtr + nC)[1]);
                    SyRed = ((dataPtr + widthstep - nC)[2] + (dataPtr + widthstep)[2] * 2 + (dataPtr + nC + widthstep)[2]) - ((dataPtr - nC)[2] + (dataPtr)[2] * 2 + (dataPtr + nC)[2]);

                    SBlue = Math.Abs(SxBlue) + Math.Abs(SyBlue);
                    SGreen = Math.Abs(SxGreen) + Math.Abs(SyGreen);
                    SRed = Math.Abs(SxRed) + Math.Abs(SyRed);

                    if (SBlue > 255) SBlue = 255;
                    if (SGreen > 255) SGreen = 255;
                    if (SRed > 255) SRed = 255;

                    // store in the image
                    dataPtr_d[0] = (byte)SBlue;
                    dataPtr_d[1] = (byte)SGreen;
                    dataPtr_d[2] = (byte)SRed;

                    dataPtr += nC;
                    dataPtr_d += nC;
                }

                //canto superior direito
                SxBlue = ((dataPtr - nC)[0] + (dataPtr - nC)[0] * 2 + (dataPtr + widthstep - nC)[0]) - ((dataPtr)[0] + (dataPtr)[0] * 2 + (dataPtr + widthstep)[0]);
                SxGreen = ((dataPtr - nC)[1] + (dataPtr - nC)[1] * 2 + (dataPtr + widthstep - nC)[1]) - ((dataPtr)[1] + (dataPtr)[1] * 2 + (dataPtr + widthstep)[1]);
                SxRed = ((dataPtr - nC)[2] + (dataPtr - nC)[2] * 2 + (dataPtr + widthstep - nC)[2]) - ((dataPtr)[2] + (dataPtr)[2] * 2 + (dataPtr + widthstep)[2]);

                SyBlue = ((dataPtr + widthstep - nC)[0] + (dataPtr + widthstep)[0] * 2 + (dataPtr + widthstep)[0]) - ((dataPtr - nC)[0] + (dataPtr)[0] * 2 + (dataPtr)[0]);
                SyGreen = ((dataPtr + widthstep - nC)[1] + (dataPtr + widthstep)[1] * 2 + (dataPtr + widthstep)[1]) - ((dataPtr - nC)[1] + (dataPtr)[1] * 2 + (dataPtr)[1]);
                SyRed = ((dataPtr + widthstep - nC)[2] + (dataPtr + widthstep)[2] * 2 + (dataPtr + widthstep)[2]) - ((dataPtr - nC)[2] + (dataPtr)[2] * 2 + (dataPtr)[2]);

                SBlue = Math.Abs(SxBlue) + Math.Abs(SyBlue);
                SGreen = Math.Abs(SxGreen) + Math.Abs(SyGreen);
                SRed = Math.Abs(SxRed) + Math.Abs(SyRed);

                if (SBlue > 255) SBlue = 255;
                if (SGreen > 255) SGreen = 255;
                if (SRed > 255) SRed = 255;

                // store in the image
                dataPtr_d[0] = (byte)SBlue;
                dataPtr_d[1] = (byte)SGreen;
                dataPtr_d[2] = (byte)SRed;

                dataPtr += widthstep;
                dataPtr_d += widthstep;

                //margem direita
                for (int y_dir = 1; y_dir < height - 1; y_dir++)
                {
                    SxBlue = ((dataPtr - nC - widthstep)[0] + (dataPtr - nC)[0] * 2 + (dataPtr + widthstep - nC)[0]) - ((dataPtr - widthstep)[0] + (dataPtr)[0] * 2 + (dataPtr + widthstep)[0]);
                    SxGreen = ((dataPtr - nC - widthstep)[1] + (dataPtr - nC)[1] * 2 + (dataPtr + widthstep - nC)[1]) - ((dataPtr - widthstep)[1] + (dataPtr)[1] * 2 + (dataPtr + widthstep)[1]);
                    SxRed = ((dataPtr - nC - widthstep)[2] + (dataPtr - nC)[2] * 2 + (dataPtr + widthstep - nC)[2]) - ((dataPtr - widthstep)[2] + (dataPtr)[2] * 2 + (dataPtr + widthstep)[2]);

                    SyBlue = ((dataPtr + widthstep - nC)[0] + (dataPtr + widthstep)[0] * 2 + (dataPtr + widthstep)[0]) - ((dataPtr - nC - widthstep)[0] + (dataPtr - widthstep)[0] * 2 + (dataPtr - widthstep)[0]);
                    SyGreen = ((dataPtr + widthstep - nC)[1] + (dataPtr + widthstep)[1] * 2 + (dataPtr + widthstep)[1]) - ((dataPtr - nC - widthstep)[1] + (dataPtr - widthstep)[1] * 2 + (dataPtr - widthstep)[1]);
                    SyRed = ((dataPtr + widthstep - nC)[2] + (dataPtr + widthstep)[2] * 2 + (dataPtr + widthstep)[2]) - ((dataPtr - nC - widthstep)[2] + (dataPtr - widthstep)[2] * 2 + (dataPtr - widthstep)[2]);

                    SBlue = Math.Abs(SxBlue) + Math.Abs(SyBlue);
                    SGreen = Math.Abs(SxGreen) + Math.Abs(SyGreen);
                    SRed = Math.Abs(SxRed) + Math.Abs(SyRed);

                    if (SBlue > 255) SBlue = 255;
                    if (SGreen > 255) SGreen = 255;
                    if (SRed > 255) SRed = 255;

                    // store in the image
                    dataPtr_d[0] = (byte)SBlue;
                    dataPtr_d[1] = (byte)SGreen;
                    dataPtr_d[2] = (byte)SRed;

                    dataPtr += widthstep;
                    dataPtr_d += widthstep;
                }

                //canto inferior direito
                SxBlue = ((dataPtr - nC - widthstep)[0] + (dataPtr - nC)[0] * 2 + (dataPtr - nC)[0]) - ((dataPtr - widthstep)[0] + (dataPtr)[0] * 2 + (dataPtr)[0]);
                SxGreen = ((dataPtr - nC - widthstep)[1] + (dataPtr - nC)[1] * 2 + (dataPtr - nC)[1]) - ((dataPtr - widthstep)[1] + (dataPtr)[1] * 2 + (dataPtr)[1]);
                SxRed = ((dataPtr - nC - widthstep)[2] + (dataPtr - nC)[2] * 2 + (dataPtr - nC)[2]) - ((dataPtr - widthstep)[2] + (dataPtr)[2] * 2 + (dataPtr)[2]);

                SyBlue = ((dataPtr - nC)[0] + (dataPtr)[0] * 2 + (dataPtr)[0]) - ((dataPtr - nC - widthstep)[0] + (dataPtr - widthstep)[0] * 2 + (dataPtr - widthstep)[0]);
                SyGreen = ((dataPtr - nC)[1] + (dataPtr)[1] * 2 + (dataPtr)[1]) - ((dataPtr - nC - widthstep)[1] + (dataPtr - widthstep)[1] * 2 + (dataPtr - widthstep)[1]);
                SyRed = ((dataPtr - nC)[2] + (dataPtr)[2] * 2 + (dataPtr)[2]) - ((dataPtr - nC - widthstep)[2] + (dataPtr - widthstep)[2] * 2 + (dataPtr - widthstep)[2]);

                SBlue = Math.Abs(SxBlue) + Math.Abs(SyBlue);
                SGreen = Math.Abs(SxGreen) + Math.Abs(SyGreen);
                SRed = Math.Abs(SxRed) + Math.Abs(SyRed);

                if (SBlue > 255) SBlue = 255;
                if (SGreen > 255) SGreen = 255;
                if (SRed > 255) SRed = 255;

                // store in the image
                dataPtr_d[0] = (byte)SBlue;
                dataPtr_d[1] = (byte)SGreen;
                dataPtr_d[2] = (byte)SRed;

                dataPtr -= nC;
                dataPtr_d -= nC;

                //margem inferior
                for (int x_inf = 1; x_inf < widht - 1; x_inf++)
                {
                    SxBlue = ((dataPtr - nC - widthstep)[0] + (dataPtr - nC)[0] * 2 + (dataPtr - nC)[0]) - ((dataPtr - widthstep + nC)[0] + (dataPtr + nC)[0] * 2 + (dataPtr + nC)[0]);
                    SxGreen = ((dataPtr - nC - widthstep)[1] + (dataPtr - nC)[1] * 2 + (dataPtr - nC)[1]) - ((dataPtr - widthstep + nC)[1] + (dataPtr + nC)[1] * 2 + (dataPtr + nC)[1]);
                    SxRed = ((dataPtr - nC - widthstep)[2] + (dataPtr - nC)[2] * 2 + (dataPtr - nC)[2]) - ((dataPtr - widthstep + nC)[2] + (dataPtr + nC)[2] * 2 + (dataPtr + nC)[2]);

                    SyBlue = ((dataPtr - nC)[0] + (dataPtr)[0] * 2 + (dataPtr + nC)[0]) - ((dataPtr - nC - widthstep)[0] + (dataPtr - widthstep)[0] * 2 + (dataPtr - widthstep + nC)[0]);
                    SyGreen = ((dataPtr - nC)[1] + (dataPtr)[1] * 2 + (dataPtr + nC)[1]) - ((dataPtr - nC - widthstep)[1] + (dataPtr - widthstep)[1] * 2 + (dataPtr - widthstep + nC)[1]);
                    SyRed = ((dataPtr - nC)[2] + (dataPtr)[2] * 2 + (dataPtr + nC)[2]) - ((dataPtr - nC - widthstep)[2] + (dataPtr - widthstep)[2] * 2 + (dataPtr - widthstep + nC)[2]);

                    SBlue = Math.Abs(SxBlue) + Math.Abs(SyBlue);
                    SGreen = Math.Abs(SxGreen) + Math.Abs(SyGreen);
                    SRed = Math.Abs(SxRed) + Math.Abs(SyRed);

                    if (SBlue > 255) SBlue = 255;
                    if (SGreen > 255) SGreen = 255;
                    if (SRed > 255) SRed = 255;

                    // store in the image
                    dataPtr_d[0] = (byte)SBlue;
                    dataPtr_d[1] = (byte)SGreen;
                    dataPtr_d[2] = (byte)SRed;

                    dataPtr -= nC;
                    dataPtr_d -= nC;
                }

                //canto inferior esquerdo
                SxBlue = ((dataPtr - widthstep)[0] + (dataPtr)[0] * 2 + (dataPtr)[0]) - ((dataPtr - widthstep + nC)[0] + (dataPtr + nC)[0] * 2 + (dataPtr + nC)[0]);
                SxGreen = ((dataPtr - widthstep)[1] + (dataPtr)[1] * 2 + (dataPtr)[1]) - ((dataPtr - widthstep + nC)[1] + (dataPtr + nC)[1] * 2 + (dataPtr + nC)[1]);
                SxRed = ((dataPtr - widthstep)[2] + (dataPtr)[2] * 2 + (dataPtr)[2]) - ((dataPtr - widthstep + nC)[2] + (dataPtr + nC)[2] * 2 + (dataPtr + nC)[2]);

                SyBlue = ((dataPtr)[0] + (dataPtr)[0] * 2 + (dataPtr + nC)[0]) - ((dataPtr - widthstep)[0] + (dataPtr - widthstep)[0] * 2 + (dataPtr - widthstep + nC)[0]);
                SyGreen = ((dataPtr)[1] + (dataPtr)[1] * 2 + (dataPtr + nC)[1]) - ((dataPtr - widthstep)[1] + (dataPtr - widthstep)[1] * 2 + (dataPtr - widthstep + nC)[1]);
                SyRed = ((dataPtr)[2] + (dataPtr)[2] * 2 + (dataPtr + nC)[2]) - ((dataPtr - widthstep)[2] + (dataPtr - widthstep)[2] * 2 + (dataPtr - widthstep + nC)[2]);

                SBlue = Math.Abs(SxBlue) + Math.Abs(SyBlue);
                SGreen = Math.Abs(SxGreen) + Math.Abs(SyGreen);
                SRed = Math.Abs(SxRed) + Math.Abs(SyRed);

                if (SBlue > 255) SBlue = 255;
                if (SGreen > 255) SGreen = 255;
                if (SRed > 255) SRed = 255;

                // store in the image
                dataPtr_d[0] = (byte)SBlue;
                dataPtr_d[1] = (byte)SGreen;
                dataPtr_d[2] = (byte)SRed;

                dataPtr -= widthstep;
                dataPtr_d -= widthstep;

                //margem esquerda
                for (int y_esq = 1; y_esq < height - 1; y_esq++)
                {
                    SxBlue = ((dataPtr - widthstep)[0] + (dataPtr)[0] * 2 + (dataPtr + widthstep)[0]) - ((dataPtr - widthstep + nC)[0] + (dataPtr + nC)[0] * 2 + (dataPtr + nC + widthstep)[0]);
                    SxGreen = ((dataPtr - widthstep)[1] + (dataPtr)[1] * 2 + (dataPtr + widthstep)[1]) - ((dataPtr - widthstep + nC)[1] + (dataPtr + nC)[1] * 2 + (dataPtr + nC + widthstep)[1]);
                    SxRed = ((dataPtr - widthstep)[2] + (dataPtr)[2] * 2 + (dataPtr + widthstep)[2]) - ((dataPtr - widthstep + nC)[2] + (dataPtr + nC)[2] * 2 + (dataPtr + nC + widthstep)[2]);

                    SyBlue = ((dataPtr + widthstep)[0] + (dataPtr + widthstep)[0] * 2 + (dataPtr + nC + widthstep)[0]) - ((dataPtr - widthstep)[0] + (dataPtr - widthstep)[0] * 2 + (dataPtr - widthstep + nC)[0]);
                    SyGreen = ((dataPtr + widthstep)[1] + (dataPtr + widthstep)[1] * 2 + (dataPtr + nC + widthstep)[1]) - ((dataPtr - widthstep)[1] + (dataPtr - widthstep)[1] * 2 + (dataPtr - widthstep + nC)[1]);
                    SyRed = ((dataPtr + widthstep)[2] + (dataPtr + widthstep)[2] * 2 + (dataPtr + nC + widthstep)[2]) - ((dataPtr - widthstep)[2] + (dataPtr - widthstep)[2] * 2 + (dataPtr - widthstep + nC)[2]);

                    SBlue = Math.Abs(SxBlue) + Math.Abs(SyBlue);
                    SGreen = Math.Abs(SxGreen) + Math.Abs(SyGreen);
                    SRed = Math.Abs(SxRed) + Math.Abs(SyRed);

                    if (SBlue > 255) SBlue = 255;
                    if (SGreen > 255) SGreen = 255;
                    if (SRed > 255) SRed = 255;

                    // store in the image
                    dataPtr_d[0] = (byte)SBlue;
                    dataPtr_d[1] = (byte)SGreen;
                    dataPtr_d[2] = (byte)SRed;

                    dataPtr -= widthstep;
                    dataPtr_d -= widthstep;
                }
            }
        }

        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int widht = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                int y, x;
                int GBlue, GGreen, GRed;

                //core + margem superior + margem esquerda
                for (y = 0; y < height - 1; y++)
                {
                    for (x = 0; x < widht - 1; x++)
                    {
                        GBlue = Math.Abs(dataPtr[0] - (dataPtr + nC)[0]) + Math.Abs(dataPtr[0] - (dataPtr + widthstep)[0]);
                        GGreen = Math.Abs(dataPtr[1] - (dataPtr + nC)[1]) + Math.Abs(dataPtr[1] - (dataPtr + widthstep)[1]);
                        GRed = Math.Abs(dataPtr[2] - (dataPtr + nC)[2]) + Math.Abs(dataPtr[2] - (dataPtr + widthstep)[2]);

                        if (GBlue > 255) GBlue = 255;
                        if (GGreen > 255) GGreen = 255;
                        if (GRed > 255) GRed = 255;

                        // store in the image
                        dataPtr_d[0] = (byte)GBlue;
                        dataPtr_d[1] = (byte)GGreen;
                        dataPtr_d[2] = (byte)GRed;

                        dataPtr += nC;
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding + nC;
                    dataPtr += padding + nC;
                }

                //margem direita
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                dataPtr += nC * (widht - 1);
                dataPtr_d += nC * (widht - 1);

                for (int y_dir = 0; y_dir < height - 1; y_dir++)
                {
                    GBlue = Math.Abs(dataPtr[0] - (dataPtr)[0]) + Math.Abs(dataPtr[0] - (dataPtr + widthstep)[0]);
                    GGreen = Math.Abs(dataPtr[1] - (dataPtr)[1]) + Math.Abs(dataPtr[1] - (dataPtr + widthstep)[1]);
                    GRed = Math.Abs(dataPtr[2] - (dataPtr)[2]) + Math.Abs(dataPtr[2] - (dataPtr + widthstep)[2]);

                    if (GBlue > 255) GBlue = 255;
                    if (GGreen > 255) GGreen = 255;
                    if (GRed > 255) GRed = 255;

                    // store in the image
                    dataPtr_d[0] = (byte)GBlue;
                    dataPtr_d[1] = (byte)GGreen;
                    dataPtr_d[2] = (byte)GRed;

                    dataPtr += widthstep;
                    dataPtr_d += widthstep;
                }

                //canto inferior direito
                dataPtr_d[0] = 0;
                dataPtr_d[1] = 0;
                dataPtr_d[2] = 0;

                dataPtr -= nC;
                dataPtr_d -= nC;

                //margem inferior
                for (int x_inf = 1; x_inf < widht; x_inf++)
                {
                    GBlue = Math.Abs(dataPtr[0] - (dataPtr + nC)[0]) + Math.Abs(dataPtr[0] - (dataPtr)[0]);
                    GGreen = Math.Abs(dataPtr[1] - (dataPtr + nC)[1]) + Math.Abs(dataPtr[1] - (dataPtr)[1]);
                    GRed = Math.Abs(dataPtr[2] - (dataPtr + nC)[2]) + Math.Abs(dataPtr[2] - (dataPtr)[2]);

                    if (GBlue > 255) GBlue = 255;
                    if (GGreen > 255) GGreen = 255;
                    if (GRed > 255) GRed = 255;

                    // store in the image
                    dataPtr_d[0] = (byte)GBlue;
                    dataPtr_d[1] = (byte)GGreen;
                    dataPtr_d[2] = (byte)GRed;

                    dataPtr -= nC;
                    dataPtr_d -= nC;
                }
            }
        }

        public static void Roberts(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int widht = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                int y, x;
                int GBlue, GGreen, GRed;

                //core + margem superior + margem esquerda
                for (y = 0; y < height - 1; y++)
                {
                    for (x = 0; x < widht - 1; x++)
                    {
                        GBlue = Math.Abs(dataPtr[0] - (dataPtr + nC + widthstep)[0]) + Math.Abs((dataPtr + nC)[0] - (dataPtr + widthstep)[0]);
                        GGreen = Math.Abs(dataPtr[1] - (dataPtr + nC + widthstep)[1]) + Math.Abs((dataPtr + nC)[1] - (dataPtr + widthstep)[1]);
                        GRed = Math.Abs(dataPtr[2] - (dataPtr + nC + widthstep)[2]) + Math.Abs((dataPtr + nC)[2] - (dataPtr + widthstep)[2]);

                        if (GBlue > 255) GBlue = 255;
                        if (GGreen > 255) GGreen = 255;
                        if (GRed > 255) GRed = 255;

                        // store in the image
                        dataPtr_d[0] = (byte)GBlue;
                        dataPtr_d[1] = (byte)GGreen;
                        dataPtr_d[2] = (byte)GRed;

                        dataPtr += nC;
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding + nC;
                    dataPtr += padding + nC;
                }

                //margem direita
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtr_d = (byte*)m_d.imageData.ToPointer();

                dataPtr += nC * (widht - 1);
                dataPtr_d += nC * (widht - 1);

                for (int y_dir = 0; y_dir < height - 1; y_dir++)
                {
                    GBlue = Math.Abs(dataPtr[0] - (dataPtr + widthstep)[0]) + Math.Abs((dataPtr)[0] - (dataPtr + widthstep)[0]);
                    GGreen = Math.Abs(dataPtr[1] - (dataPtr + widthstep)[1]) + Math.Abs((dataPtr)[1] - (dataPtr + widthstep)[1]);
                    GRed = Math.Abs(dataPtr[2] - (dataPtr + widthstep)[2]) + Math.Abs((dataPtr)[2] - (dataPtr + widthstep)[2]);

                    if (GBlue > 255) GBlue = 255;
                    if (GGreen > 255) GGreen = 255;
                    if (GRed > 255) GRed = 255;

                    // store in the image
                    dataPtr_d[0] = (byte)GBlue;
                    dataPtr_d[1] = (byte)GGreen;
                    dataPtr_d[2] = (byte)GRed;

                    dataPtr += widthstep;
                    dataPtr_d += widthstep;
                }

                //canto inferior direito
                dataPtr_d[0] = 0;
                dataPtr_d[1] = 0;
                dataPtr_d[2] = 0;

                dataPtr -= nC;
                dataPtr_d -= nC;

                //margem inferior
                for (int x_inf = 1; x_inf < widht; x_inf++)
                {
                    GBlue = Math.Abs(dataPtr[0] - (dataPtr + nC)[0]) + Math.Abs((dataPtr + nC)[0] - (dataPtr)[0]);
                    GGreen = Math.Abs(dataPtr[1] - (dataPtr + nC)[1]) + Math.Abs((dataPtr + nC)[1] - (dataPtr)[1]);
                    GRed = Math.Abs(dataPtr[2] - (dataPtr + nC)[2]) + Math.Abs((dataPtr + nC)[2] - (dataPtr)[2]);

                    if (GBlue > 255) GBlue = 255;
                    if (GGreen > 255) GGreen = 255;
                    if (GRed > 255) GRed = 255;

                    // store in the image
                    dataPtr_d[0] = (byte)GBlue;
                    dataPtr_d[1] = (byte)GGreen;
                    dataPtr_d[2] = (byte)GRed;

                    dataPtr -= nC;
                    dataPtr_d -= nC;
                }
            }
        }

        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                imgCopy.SmoothMedian(3).CopyTo(img);
            }
        }

        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                int[] histogram_gray = new int[256];

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                for (y = 0; y < img.Height; y++)
                {
                    for (x = 0; x < img.Width; x++)
                    {
                        //retrive 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        gray = (int)Math.Round((blue + green + red) / 3.0);

                        histogram_gray[gray] += 1;

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }

                return histogram_gray;
            }
        }

        public static int[,] Histogram_RGB(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                int[,] histogram_rgb = new int[3, 256];

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                for (y = 0; y < img.Height; y++)
                {
                    for (x = 0; x < img.Width; x++)
                    {
                        //retrive 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        histogram_rgb[0, blue] += 1;
                        histogram_rgb[1, green] += 1;
                        histogram_rgb[2, red] += 1;

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }

                return histogram_rgb;
            }
        }

        public static int[,] Histogram_All(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                int[,] histogram_rgb_gray = new int[4, 256];

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                for (y = 0; y < img.Height; y++)
                {
                    for (x = 0; x < img.Width; x++)
                    {
                        //retrive 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        gray = (int)Math.Round((blue + green + red) / 3.0);

                        histogram_rgb_gray[0, gray] += 1;
                        histogram_rgb_gray[1, blue] += 1;
                        histogram_rgb_gray[2, green] += 1;
                        histogram_rgb_gray[3, red] += 1;


                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }

                return histogram_rgb_gray;
            }
        }

        public static void Rotation_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                // obter apontador do inicio da imagem MIplImage 
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int w = imgCopy.Width;
                int h = imgCopy.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                byte blue, green, red;
                double x_o, y_o;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                double cos, sen, inter;
                int s_e, s_d, i_e, i_d;
                double offset_x, offset_y;

                cos = System.Math.Cos(angle);
                sen = System.Math.Sin(angle);

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        x_o = ((x - w / 2.0) * cos) - ((h / 2.0 - y) * sen) + w / 2.0;
                        y_o = h / 2.0 - ((x - w / 2.0) * sen) - ((h / 2.0 - y) * cos);

                        offset_x = x_o - (int)x_o;
                        offset_y = y_o - (int)y_o;

                        if (x_o > w - 1 || x_o < 0 || y_o > h - 1 || y_o < 0)
                        {
                            dataPtr_d[0] = 0;
                            dataPtr_d[1] = 0;
                            dataPtr_d[2] = 0;
                        }
                        else
                        {
                            // calcula endereço do pixel no ponto(x, y)
                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[0];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[0];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[0];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[0];


                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            blue = (byte)inter;

                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[1];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[1];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[1];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[1];

                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            green = (byte)inter;

                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[2];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[2];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[2];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[2];

                            //inter = inter_x1 * (1-offset_y) + inter_x2 * offset_y
                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            red = (byte)inter;

                            // store in the image
                            dataPtr_d[0] = blue;
                            dataPtr_d[1] = green;
                            dataPtr_d[2] = red;
                        }
                        // advance the pointer to the next pixel
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding;
                }
            }
        }

        public static void Scale_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {
            unsafe
            {
                // obter apontador do inicio da imagem MIplImage 
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int w = imgCopy.Width;
                int h = imgCopy.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                byte blue, green, red;
                double x_o, y_o;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                int s_e, s_d, i_e, i_d;
                double offset_x, offset_y, inter;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        x_o = x / scaleFactor;
                        y_o = y / scaleFactor;

                        offset_x = x_o - (int)x_o;
                        offset_y = y_o - (int)y_o;

                        if (x_o > w - 1 || x_o < 0 || y_o > h - 1 || y_o < 0)
                        {
                            dataPtr_d[0] = 0;
                            dataPtr_d[1] = 0;
                            dataPtr_d[2] = 0;
                        }
                        else
                        {
                            // calcula endereço do pixel no ponto(x, y)
                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[0];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[0];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[0];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[0];

                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            blue = (byte)inter;

                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[1];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[1];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[1];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[1];

                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            green = (byte)inter;

                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[2];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[2];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[2];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[2];

                            //inter = inter_x1 * (1-offset_y) + inter_x2 * offset_y
                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            red = (byte)inter;

                            // store in the image
                            dataPtr_d[0] = blue;
                            dataPtr_d[1] = green;
                            dataPtr_d[2] = red;
                        }
                        // advance the pointer to the next pixel
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding;
                }
            }
        }

        public static void Scale_point_xy_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                // obter apontador do inicio da imagem MIplImage 
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int w = imgCopy.Width;
                int h = imgCopy.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                byte blue, green, red;
                double x_o, y_o;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                int s_e, s_d, i_e, i_d;
                double offset_x, offset_y, inter;
                double add_xo = centerX - (w / 2) / scaleFactor;
                double add_yo = centerY - (h / 2) / scaleFactor;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        x_o = (double)x / scaleFactor + add_xo;
                        y_o = (double)y / scaleFactor + add_yo;

                        offset_x = x_o - (int)x_o;
                        offset_y = y_o - (int)y_o;

                        if (x_o > w - 1 || x_o < 0 || y_o > h - 1 || y_o < 0)
                        {
                            dataPtr_d[0] = 0;
                            dataPtr_d[1] = 0;
                            dataPtr_d[2] = 0;
                        }
                        else
                        {
                            // calcula endereço do pixel no ponto(x, y)
                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[0];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[0];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[0];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[0];

                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            blue = (byte)inter;

                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[1];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[1];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[1];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[1];

                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            green = (byte)inter;

                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[2];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[2];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[2];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[2];

                            //inter = inter_x1 * (1-offset_y) + inter_x2 * offset_y
                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            red = (byte)inter;

                            // store in the image
                            dataPtr_d[0] = blue;
                            dataPtr_d[1] = green;
                            dataPtr_d[2] = red;
                        }
                        // advance the pointer to the next pixel
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding;
                }
            }
        }

        public static void Equalization(Image<Bgr, byte> img)
        {
            unsafe
            {
                Image<Ycc, byte> img_ycc = img.Convert<Ycc, byte>();

                int[] histogram_gray = new int[256];
                int[] newH = new int[256];
                int acumHist, acumHistmin = 0;
                bool acummin = false;
                double new_value;

                MIplImage m = img_ycc.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;


                for (y = 0; y < img.Height; y++)
                {
                    for (x = 0; x < img.Width; x++)
                    {
                        histogram_gray[dataPtr[0]] += 1;

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }

                acumHist = 0;
                for (y = 0; y < histogram_gray.Length; y++)
                {
                    acumHist += histogram_gray[y];

                    if (!acummin && histogram_gray[y] != 0)
                    {
                        acummin = true;
                        acumHistmin = histogram_gray[y];
                    }

                    new_value = (((double)acumHist - acumHistmin) / (width * height - acumHistmin)) * (histogram_gray.Length - 1);
                    newH[y] = (int)Math.Round(new_value);
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                for (y = 0; y < img.Height; y++)
                {
                    for (x = 0; x < img.Width; x++)
                    {
                        dataPtr[0] = (byte)newH[dataPtr[0]];

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }

                img.ConvertFrom<Ycc, byte>(img_ycc);
            }
        }

        public static void ConvertToBW(Emgu.CV.Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {
                // obter apontador do inicio da imagem MIplImage 
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int w = img.Width;
                int h = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int blue, green, red, gray;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        //retrive 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        gray = (int)Math.Round((blue + green + red) / 3.0);

                        if (gray <= threshold)
                        {
                            dataPtr[0] = 0;
                            dataPtr[1] = 0;
                            dataPtr[2] = 0;
                        }
                        else
                        {
                            dataPtr[0] = 255;
                            dataPtr[1] = 255;
                            dataPtr[2] = 255;
                        }

                        // advance the pointer to the next pixel
                        dataPtr += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
            }
        }

        public static int OTSU(int[] hist)
        {
            unsafe
            {
                int q1, q2, u1, u2, threshold;
                double max, var;
                int resolucao = 0;

                Array.ForEach(hist, delegate (int i) { resolucao += i; });

                q1 = hist[0];
                q2 = resolucao - q1;

                u1 = hist[0];
                u2 = 0;

                for (int i = 1; i < hist.Length; i++)
                {
                    u2 = u2 + i * hist[i];
                }

                threshold = 0;
                if (u1 > 0 && q1 > 0 && q2 > 0)
                    max = (double)q1 / resolucao * (double)q2 / resolucao * Math.Pow((double)u1 / q1 - (double)u2 / q2, 2);
                else
                    max = 0;

                for (int i = 1; i < hist.Length; i++)
                {
                    q1 = q1 + hist[i];
                    if (q1 == 0)
                        continue;

                    q2 = q2 - hist[i];
                    if (q2 == 0)
                        break;

                    u1 = u1 + i * hist[i];
                    u2 = u2 - i * hist[i];

                    var = (double)q1 / resolucao * (double)q2 / resolucao * Math.Pow((double)u1 / q1 - (double)u2 / q2, 2);

                    if (var > max)
                    {
                        threshold = i;
                        max = var;
                    }
                }
                return threshold;
            }
        }

        public static void ConvertToBW_Otsu(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                int[] hist = Histogram_Gray(img);
                int threshold = OTSU(hist);
                ConvertToBW(img, threshold);
            }
        }










        //************************************************************************************************************************
        //                                                GENERAL PURPOSE FUNCTIONS
        //************************************************************************************************************************
        public static int[,] fill2DArray(int dim, int val)
        {
            int[,] arr = new int[dim, dim];
            for (int i = 0; i < dim; ++i)
            {
                for (int j = 0; j < dim; ++j)
                {
                    arr[i, j] = val;
                }
            }
            return arr;
        }



        public static void InitOutputVars(out Point bc_centroid1, out Size bc_size1, out string bc_image1, out string bc_number1,
            out Point bc_centroid2, out Size bc_size2, out string bc_image2, out string bc_number2)
        {
            bc_centroid1 = Point.Empty;
            bc_size1 = Size.Empty;
            bc_image1 = null;
            bc_number1 = null;

            bc_image2 = null;
            bc_number2 = null;
            bc_centroid2 = Point.Empty;
            bc_size2 = Size.Empty;
        }






        //************************************************************************************************************************
        //                                                IMAGE PROCESSING FUNCTIONS
        //************************************************************************************************************************
        public static int[,] IteractiveSegmentation(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                bool boolChanges = true; //check for changes in the tag array
                bool direction = true;   //iteration direction (bottom-up/right-left OR top-down/left-right)
                int[,] tags = new int[width, height]; 
                //-------------------------------------------------------------------------------------------------


                //first run, each black pixel w/ a different tag
                for (int y = 0, counter = 1; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (dataPtr[0] == 0) 
                            tags[x, y] = counter++;
                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }


                //joint tags for pixels in the same object
                while (boolChanges) //ends when no changes were made during the iteration
                {
                    boolChanges = false;
                    //propagation top-down/left-right
                    if (direction == true)
                    {
                        for (int y = 1; y < height - 1; y++)
                        {
                            for (int x = 1; x < width - 1; x++)
                            {
                                if (tags[x, y] != 0)
                                {
                                    //update tag w/ the lowest value in the neighborhood (8px adjacency)
                                    for (int i = x - 1; i <= x + 1; i++)
                                    {
                                        for (int j = y - 1; j <= y + 1; j++)
                                        {
                                            if (tags[i, j] < tags[x, y] && tags[i, j] != 0)
                                            {
                                                tags[x, y] = tags[i, j];
                                                boolChanges = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //propagation bottom-up/right-left
                    else
                    {
                        for (int y = height - 2; y > 0; y--)
                        {
                            for (int x = width - 2; x > 0; x--)
                            {
                                if (tags[x, y] != 0)
                                {
                                    //update tag w/ the lowest value in the neighborhood (8px adjacency)
                                    for (int i = x - 1; i <= x + 1; i++)
                                    {
                                        for (int j = y - 1; j <= y + 1; j++)
                                        {
                                            if (tags[i, j] < tags[x, y] && tags[i, j] != 0)
                                            {
                                                tags[x, y] = tags[i, j];
                                                boolChanges = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //change direction for each iteration
                    direction = !direction;
                }
                return tags;
            }
        }



        public static void Dilation(Emgu.CV.Image<Bgr, byte> imgCopy, Image<Bgr, byte> img, int[,] mask, int dim)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage m_d = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                //-------------------------------------------------------------------------------------------------

                dataPtr += nC * (dim / 2) + widthstep * (dim / 2);
                dataPtr_d += nC * (dim / 2) + widthstep * (dim / 2);


                for (int y = (dim / 2); y < height - (dim / 2); y++)
                {
                    for (int x = (dim / 2); x < width - (dim / 2); x++)
                    {
                        if (dataPtr[0] == 0)
                        {
                            for (int i = -(dim / 2); i < (dim / 2); i++)
                            {
                                for (int j = -(dim / 2); j < (dim / 2); j++)
                                {
                                    //fill every neighborhood pixel, according to the mask
                                    if (mask[i + (dim / 2), j + (dim / 2)] == 1)  
                                    {
                                        (dataPtr_d + nC * i + widthstep * j)[0] = 0;
                                        (dataPtr_d + nC * i + widthstep * j)[1] = 0;
                                        (dataPtr_d + nC * i + widthstep * j)[2] = 0;
                                    }
                                }
                            }
                        }
                        //advance the pointer to the next pixel
                        dataPtr += nC;
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding + (2 * (dim / 2)) * nC;
                    dataPtr_d += padding + (2 * (dim / 2)) * nC;
                }
            }
        }



        public static void Erosion(Emgu.CV.Image<Bgr, byte> imgCopy, Image<Bgr, byte> img, int[,] mask, int dim)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage m_d = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                bool bool_mask = true; //se if the neighborhood respects the mask appearance
                //-------------------------------------------------------------------------------------------------

                dataPtr += nC * (dim / 2) + widthstep * (dim / 2);
                dataPtr_d += nC * (dim / 2) + widthstep * (dim / 2);


                for (int y = (dim / 2); y < height - (dim / 2) - 1; y++)
                {
                    for (int x = (dim / 2); x < width - (dim / 2) - 1; x++)
                    {
                        if (dataPtr[0] == 0)
                        {
                            bool_mask = true;
                            for (int i = 0; i < dim; i++)
                            {
                                for (int j = 0; j < dim; j++)
                                {
                                    if (mask[i, j] == 1 && (dataPtr + nC * (i - 1) + widthstep * (j - 1))[0] == 255)
                                        bool_mask = false;
                                }
                            }
                            if (bool_mask == false)
                            {
                                dataPtr_d[0] = 255;
                                dataPtr_d[1] = 255;
                                dataPtr_d[2] = 255;
                            }
                        }
                        //advance the pointer to the next pixel
                        dataPtr += nC;
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding + (2 * (dim / 2)) * nC;
                    dataPtr_d += padding + (2 * (dim / 2)) * nC;
                }
            }
        }



        public static void Rotation_Bilinear_White(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                // obter apontador do inicio da imagem MIplImage 
                MIplImage m = imgCopy.MIplImage;
                MIplImage m_d = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int w = imgCopy.Width;
                int h = imgCopy.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                byte blue, green, red;
                double x_o, y_o;
                int padding = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                double cos, sen, inter;
                int s_e, s_d, i_e, i_d;
                double offset_x, offset_y;

                cos = System.Math.Cos(angle);
                sen = System.Math.Sin(angle);

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        x_o = ((x - w / 2.0) * cos) - ((h / 2.0 - y) * sen) + w / 2.0;
                        y_o = h / 2.0 - ((x - w / 2.0) * sen) - ((h / 2.0 - y) * cos);

                        offset_x = x_o - (int)x_o;
                        offset_y = y_o - (int)y_o;

                        if (x_o > w - 1 || x_o < 0 || y_o > h - 1 || y_o < 0)
                        {
                            dataPtr_d[0] = 255;
                            dataPtr_d[1] = 255;
                            dataPtr_d[2] = 255;
                        }
                        else
                        {
                            // calcula endereço do pixel no ponto(x, y)
                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[0];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[0];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[0];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[0];


                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            blue = (byte)inter;

                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[1];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[1];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[1];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[1];

                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            green = (byte)inter;

                            s_e = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[2];
                            s_d = (dataPtr + (int)Math.Round(y_o - 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[2];
                            i_e = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o - 0.5) * nC)[2];
                            i_d = (dataPtr + (int)Math.Round(y_o + 0.5) * widthstep + (int)Math.Round(x_o + 0.5) * nC)[2];

                            //inter = inter_x1 * (1-offset_y) + inter_x2 * offset_y
                            inter = Math.Round((1 - offset_y) * ((1 - offset_x) * s_e + offset_x * s_d) + offset_y * ((1 - offset_x) * i_e + offset_x * i_d));
                            red = (byte)inter;

                            // store in the image
                            dataPtr_d[0] = blue;
                            dataPtr_d[1] = green;
                            dataPtr_d[2] = red;
                        }
                        // advance the pointer to the next pixel
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr_d += padding;
                }
            }
        }






        //************************************************************************************************************************
        //                                                IMAGE MANIPULATION FUNCTIONS
        //************************************************************************************************************************
        public static Image<Bgr, byte> ImageCut(Image<Bgr, byte> imgCopy, int xMax, int xMin, int yMax, int yMin)
        {
            unsafe
            {
                imgCopy.ROI = Rectangle.Empty;
                Rectangle roi = new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
                imgCopy.ROI = roi;
            }
            return imgCopy.Copy();
        }



        public static void ImageClean(Image<Bgr, byte> img, 
            int xMax1, int xMin1, int yMax1, int yMin1,
            int xMax2, int xMin2, int yMax2, int yMin2)
        {
            unsafe
            {
                //end earlier with there is no need for a cleanup
                if (xMin1 == -1) return;

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                //-------------------------------------------------------------------------------------------------


                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if(!(xMin1 < x && x < xMax1 && yMin1 < y && y < yMax1) && !(xMin2 < x && x < xMax2 && yMin2 < y && y < yMax2))
                        {
                            dataPtr[0] = 255;
                            dataPtr[1] = 255;
                            dataPtr[2] = 255;
                        }
                        // advance the pointer to the next pixel
                        dataPtr += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
            }
        }



        public static void ImageJoin(Image<Bgr, byte> img, Image<Bgr, byte> img2)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage m_d = img2.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr_d = (byte*)m_d.imageData.ToPointer();
                int width = img2.Width;
                int height = img2.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                //-------------------------------------------------------------------------------------------------


                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        dataPtr[0] = Math.Min(dataPtr[0], dataPtr_d[0]);
                        dataPtr[1] = Math.Min(dataPtr[1], dataPtr_d[1]);
                        dataPtr[2] = Math.Min(dataPtr[2], dataPtr_d[2]);
                        // advance the pointer to the next pixel
                        dataPtr += nC;
                        dataPtr_d += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                    dataPtr_d += padding;
                }
            }
        }



        public static int[,] ImageNoiseClean(Image<Bgr, byte> img, int[,] tags, 
            int xMax1, int xMin1, int yMax1, int yMin1,
            int xMax2, int xMin2, int yMax2, int yMin2)
        {
            unsafe
            {
                //end earlier with there is no need for a cleanup
                if (xMin1 == -1)
                    return tags;

                int width = tags.GetLength(0);
                int height = tags.GetLength(1);
                List<int> listTags = new List<int>();
                List<int> listAreas = new List<int>();

                //get the image boundaries
                if (xMin2 != -1)
                {
                    if (xMin2 < xMin1) xMin1 = xMin2;
                    if (xMax2 > xMax1) xMax1 = xMax2;
                    if (yMin2 < yMin1) yMin1 = yMin2;
                    if (yMax2 > yMax1) yMax1 = yMax2;
                }

                //get all the tags from the image
                for (int y = yMin1; y < yMax1; y++)
                {
                    for (int x = xMin1; x < xMax1; x++)
                    {
                        if (tags[x, y] != 0)
                        {
                            if (!listTags.Contains(tags[x, y]))
                            {
                                //add a new tag
                                if (tags[x + 1, y + 1] != 0 && tags[x + 1, y] != 0)
                                {
                                    listTags.Add(tags[x, y]);
                                    listAreas.Add(1);
                                }
                            }
                            //increase current tag area
                            else listAreas[listTags.FindIndex(ind => ind.Equals(tags[x, y]))]++;
                        }
                    }
                }

                //eliminate border tags
                for (int y = yMin1+1; y < yMax1-1; y++)
                {
                    if (tags[xMin1 + 1, y] != 0 && listTags.Contains(tags[xMin1 + 1, y]))
                        listTags[listTags.FindIndex(ind => ind.Equals(tags[xMin1 + 1, y]))] = -1;
                    if (tags[xMax1 - 1, y] != 0 && listTags.Contains(tags[xMax1 - 1, y]))
                        listTags[listTags.FindIndex(ind => ind.Equals(tags[xMax1 - 1, y]))] = -1;
                }
                for (int x = xMin1 + 1; x < xMax1 - 1; x++)
                {
                    if (tags[x, yMin1 + 1] != 0 && listTags.Contains(tags[x, yMin1 + 1]))
                        listTags[listTags.FindIndex(ind => ind.Equals(tags[x, yMin1 + 1]))] = -1;
                    if (tags[x, yMax1 - 1] != 0 && listTags.Contains(tags[x, yMax1 - 1]))
                        listTags[listTags.FindIndex(ind => ind.Equals(tags[x, yMax1 - 1]))] = -1;
                }

                //eliminate tags too small
                for (int i=0; i<listTags.Count(); i++)
                {
                    if(listAreas[i] < 200) 
                        listTags[i] = -1;
                }

                //update tag array and image, with the noise elimination
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int nC = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (!listTags.Contains(tags[x, y])) 
                        {
                            tags[x, y] = 0;
                            dataPtr[0] = 255;
                            dataPtr[1] = 255;
                            dataPtr[2] = 255;
                        }
                        // advance the pointer to the next pixel
                        dataPtr += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
                return tags;
            }
        }



        public static void ImageDraw(Image<Bgr, byte> img, 
            int xImgLeft, int xImgRight, int yImgUp, int yImgBottom,
            int xBarLeft, int yHighBarBottom, int yLowBarBottom)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                //-------------------------------------------------------------------------------------------------


                for (int y = 0; y < img.Height; y++)
                {
                    for (int x = 0; x < img.Width; x++)
                    {
                        if (x == xImgLeft || x == xImgRight || y == yImgUp || y == yImgBottom)
                        {
                            dataPtr[0] = 0;
                            dataPtr[1] = 0;
                            dataPtr[2] = 255;
                        }
                        if (x == xBarLeft || y == yHighBarBottom)
                        {
                            dataPtr[0] = 255;
                            dataPtr[1] = 0;
                            dataPtr[2] = 0;
                        }
                        if (y == yLowBarBottom)
                        {
                            dataPtr[0] = 0;
                            dataPtr[1] = 255;
                            dataPtr[2] = 0;
                        }
                        // advance the pointer to the next pixel
                        dataPtr += nC;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
            }
        }






        //************************************************************************************************************************
        //                                                MAIN BAR CODE FUNCTION
        //************************************************************************************************************************
        /// <summary>
        /// Barcode reader - SS final project
        /// </summary>
        /// <param name="img">Original image</param>
        /// <param name="type">image type</param>
        /// <param name="bc_centroid1">output the centroid of the first barcode </param>
        /// <param name="bc_size1">output the size of the first barcode </param>
        /// <param name="bc_image1">output a string containing the first barcode read from the bars</param>
        /// <param name="bc_number1">output a string containing the first barcode read from the numbers in the bottom</param>
        /// <param name="bc_centroid2">output the centroid of the second barcode </param>
        /// <param name="bc_size2">output the size of the second barcode</param>
        /// <param name="bc_image2">output a string containing the second barcode read from the bars. It returns null, if it does not exist.</param>
        /// <param name="bc_number2">output a string containing the second barcode read from the numbers in the bottom. It returns null, if it does not exist.</param>
        /// <returns>image with barcodes detected</returns>
        public static Image<Bgr, byte> BarCodeReader(Image<Bgr, byte> img, int type,
            out Point bc_centroid1, out Size bc_size1, out string bc_image1, out string bc_number1,
            out Point bc_centroid2, out Size bc_size2, out string bc_image2, out string bc_number2)
        {
            unsafe
            {
                //image variables
                Image<Bgr, byte> imgForID = img.Copy(); //image to identify the bar code in a image with other elements
                Image<Bgr, byte> imgBC = img.Copy();    //image related to the bar code only
                Image<Bgr, byte> imgTemp = img.Copy();  //temporary image for 2nd bar code
                MIplImage m = imgBC.MIplImage;
                int width = img.Width;
                int height = img.Height;
                long resolution = height * width;
                //bar code identification
                bool boolID_BC = true; 
                //number of bar codes
                int numberOfBC = 1;
                //bar code dimensions
                int xImgLeft = -1, xImgRight = -1, yImgUp = -1, yImgBottom = -1;
                int xImgLeft2 = 0, xImgRight2 = 0, yImgUp2 = 0, yImgBottom2 = 0;
                int xBarLeft = 0, yLowBarBottom = 0, yHighBarBottom = 0;
                //bar code recognition
                string[] digitsValue; //binary bar values from bar recognition
                double PERC = 0.5;    //const to determine minimum relation between white and black columns 
                //tags array for the Linked Component Algorithm
                int[,] tags = new int[width, height];
                //list of bar tags 
                List<int> listLowBarTags = new List<int>();
                //output variables
                Point bc_centroid;
                Size bc_size;
                string bc_image, bc_number;
                //bar code information
                int[,] arrayBCinfo = new int[2, 11];
                //bar code digit table 
                string[] first_digits = { "LLLLLL", "LLGLGG", "LLGGLG", "LLGGGL", "LGLLGG", "LGGLLG", "LGGGLL", "LGLGLG", "LGLGGL", "LGGLGL" };
                string[] L_code = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
                string[] G_code = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
                string[] R_code = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
                //------------------------------------------------------------------------------------------------------------------------------------------------
                
                
                //init output variables
                InitOutputVars(out bc_centroid1, out bc_size1, out bc_image1, out bc_number1, 
                    out bc_centroid2, out bc_size2, out bc_image2, out bc_number2);

                //--------------(1) BAR CODE IDENTIFICATION--------------//
                boolID_BC = SS_OpenCV.BarCodeIdentification.BarCodeIdent(imgForID, resolution,
                                out xImgLeft, out xImgRight, out yImgUp, out yImgBottom,
                                out xImgLeft2, out xImgRight2, out yImgUp2, out yImgBottom2);
                if (!boolID_BC) //second identification algorithm
                {
                    imgForID = img.Copy();
                    arrayBCinfo = SS_OpenCV.BarCodeIdentification.BarCodeIdentFor2(imgForID, out numberOfBC);
                }

                //perform detection for each BC detected
                for (int indBC=0; indBC < numberOfBC; indBC++)
                {
                    PERC = 0.5;
                    if (!boolID_BC)
                    {
                        xImgLeft = arrayBCinfo[indBC, 5];
                        xImgRight = arrayBCinfo[indBC, 7];
                        yImgUp = arrayBCinfo[indBC, 2];
                        yImgBottom = arrayBCinfo[indBC, 4];
                    }

                    //binarization
                    ConvertToBW_Otsu(imgBC);

                    //--------------(2) BAR CODE FILTRATION--------------//
                    ImageClean(imgBC, xImgRight, xImgLeft, yImgBottom, yImgUp,
                        xImgRight2, xImgLeft2, yImgBottom2, yImgUp2);

                    //deal with the noise from the bars
                    if (resolution > 1000000 && indBC != 1) { 
                        Dilation(imgBC, imgBC.Copy(), fill2DArray(5, 1), 5);
                        Erosion(imgBC, imgBC.Copy(), fill2DArray(3, 1), 3);
                    }

                    //linked component algorithm - iterative
                    tags = IteractiveSegmentation(imgBC);

                    //eliminate noise around the bar code
                    tags = ImageNoiseClean(imgBC, tags, 
                        xImgRight, xImgLeft, yImgBottom, yImgUp,
                        xImgRight2, xImgLeft2, yImgBottom2, yImgUp2);
                    
                    //--------------(3) BAR CODE ROTATION--------------//
                    if (SS_OpenCV.BarCodeRotation.RotateBarCode(imgBC, tags, resolution)) {
                        ConvertToBW_Otsu(imgBC);
                        tags = IteractiveSegmentation(imgBC);
                    }

                    //--------------(4) BAR CODE DIMENSIONS--------------//
                    SS_OpenCV.BarCodeIdentification.BarCodeDimensions(tags,
                        out xImgLeft, out xImgRight, out yImgUp, out yImgBottom,
                        out xBarLeft, out yLowBarBottom, out yHighBarBottom,
                        out bc_centroid, out bc_size, out listLowBarTags);

                    //--------------(5) BAR RECOGNITION--------------//
                    do
                    {
                        digitsValue = SS_OpenCV.BarRecognition.BarRecon(tags, PERC, xBarLeft, xImgRight, yImgUp, yLowBarBottom);
                        bc_image = SS_OpenCV.BarRecognition.ConvertBinToDec(digitsValue, first_digits, L_code, R_code, G_code);
                        PERC += 0.02;
                    }
                    while (bc_image.Contains("-1") && PERC<1);

                    //--------------(6) DIGIT RECOGNITION--------------//
                    bc_number = SS_OpenCV.DigitRecognition.DigitRecon(imgBC, tags, listLowBarTags,
                        xImgLeft, xImgRight, yImgBottom, yLowBarBottom, yHighBarBottom);

                    //send results to the correspondig variables, the BC one or two
                    if (indBC == 0)
                    {
                        bc_centroid1 = bc_centroid;
                        bc_size1 = bc_size;
                        bc_image1 = bc_image;
                        bc_number1 = bc_number;
                    }
                    else if (indBC == 1)
                    {
                        bc_centroid2 = bc_centroid;
                        bc_size2 = bc_size;
                        bc_image2 = bc_image;
                        bc_number2 = bc_number;
                    }

                    //draw the BC limits over the destination image
                    ImageDraw(imgBC, xImgLeft, xImgRight, yImgUp, yImgBottom,
                        xBarLeft, yHighBarBottom, yLowBarBottom);

                    //join the bar codes in one image
                    const int cnstBound = 5;
                    if (numberOfBC == 2 && indBC == 0)
                    {
                        ImageClean(imgBC, xImgRight+ cnstBound, xImgLeft- cnstBound, yImgBottom+ cnstBound, yImgUp- cnstBound,
                            xImgRight2, xImgLeft2, yImgBottom2, yImgUp2);
                        imgTemp = imgBC;
                        imgBC = img.Copy();
                    }
                    else if (numberOfBC == 2 && indBC == 1)
                    {
                        ImageClean(imgBC, xImgRight + cnstBound, xImgLeft - cnstBound, yImgBottom + cnstBound, yImgUp - cnstBound,
                            xImgRight2, xImgLeft2, yImgBottom2, yImgUp2);
                        ImageJoin(imgBC, imgTemp);
                    }
                }

                //draw the rectangle over the destination image
                //imgBC.Draw(new Rectangle(bc_centroid1.X - bc_size1.Width / 2, bc_centroid1.Y - bc_size1.Height / 2, bc_size1.Width, bc_size1.Height), new Bgr(0, 255, 0), 3);
                //download image
                //imgForID.Save(@"D:\Users\Pedro\Desktop\imgForID.png");
                //imgBC.Save(@"D:\Users\Pedro\Desktop\imgBC.png");
                //img.Copy().Save(@"D:\Users\Pedro\Desktop\img.png");
                return imgBC;
            }
        }
    }
}