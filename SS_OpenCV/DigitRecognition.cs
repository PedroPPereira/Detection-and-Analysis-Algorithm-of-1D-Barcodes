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

    class DigitRecognition
    {
        //************************************************************************************************************************
        //                                                DIGITS RECOGNITION 
        //************************************************************************************************************************
        public static string DigitRecon(Image<Bgr, byte> img, int[,] tags, List<int> listLowBarTags,
            int xImgLeft, int xImgRight, int yImgBottom, int yLowBarBottom, int yHighBarBottom)
        {
            unsafe
            {
                int[] digitsTags = Enumerable.Repeat(-1, 13).ToArray(); //array with the tag of each number
                int[] digitsUpperY = Enumerable.Repeat(-1, 13).ToArray(); //array with the upper y value of each number
                int[] digitsBottomY = Enumerable.Repeat(-1, 13).ToArray(); //array with the bottom y value of each number
                int[] digitsLeftX = Enumerable.Repeat(-1, 13).ToArray(); //array with the left x value of each number
                int[] digitsRightX = Enumerable.Repeat(-1, 13).ToArray(); //array with the right x value of each number
                int xDigRight = xImgLeft; //get digit x boundaries
                bool getLeftValue = true, getRightValue = true;
                Image<Bgr, byte> digitImage; //new image with only the current digit
                int[] digitsValue = new int[13]; //value from each digit
                //-------------------------------------------------------------------------------------------------


                //get tag of each digit
                int indexDigit = 0;
                for (int x = xImgLeft; x < xImgRight; x++)
                {
                    if (tags[x, yHighBarBottom + 5] != 0 && !digitsTags.Contains(tags[x, yHighBarBottom + 5]))
                        digitsTags[indexDigit++] = tags[x, yHighBarBottom + 5];
                }
                if(digitsTags.Contains(-1))
                {
                    for (int x = xImgLeft; x < xImgRight; x++)
                    {
                        for (int y = yHighBarBottom; y > yLowBarBottom; y--)
                        {
                            if (tags[x, y] != 0 && !digitsTags.Contains(tags[x, y]) && !listLowBarTags.Contains(tags[x, y]))
                                digitsTags[indexDigit++] = tags[x, y];
                        }
                        if (!digitsTags.Contains(-1)) break;
                    }
                }

                //get each digit upper y value
                for (int y = yLowBarBottom; y < yHighBarBottom; y++)
                {
                    for (int x = xImgLeft; x < xImgRight; x++)
                    {
                        if (digitsTags.Contains(tags[x, y]) && digitsUpperY[Array.IndexOf(digitsTags, tags[x, y])] == -1)
                            digitsUpperY[Array.IndexOf(digitsTags, tags[x, y])] = y;
                    }
                    if (!digitsUpperY.Contains(-1))
                    {
                        y = yHighBarBottom;
                        break;
                    }
                }

                //get the width limits (his x left and x right) for each digit
                for (int i = 0; i < digitsTags.Length; i++)
                {
                    getLeftValue = true;
                    for (int x = xDigRight; x < xImgRight; x++)
                    {
                        getRightValue = false;
                        for (int y = digitsUpperY[i]; y < yImgBottom; y++)
                        {
                            if (digitsTags[i] == tags[x, y])
                            {
                                if (getLeftValue)
                                {
                                    //left digit limit
                                    digitsLeftX[i] = x; 
                                    getLeftValue = false;
                                }
                                getRightValue = true;
                                break;
                            }
                        }
                        if (!getLeftValue && !getRightValue) //see when the digits stops appering
                        {
                            //right digit limit
                            xDigRight = x-1; 
                            digitsRightX[i] = x; 
                            break;
                        }
                    }
                }

                //get each digit bottom y value
                for (int y = yImgBottom; y > yLowBarBottom; y--)
                {
                    for (int x = xImgLeft; x < xImgRight; x++)
                    {
                        if (digitsTags.Contains(tags[x, y]) && digitsBottomY[Array.IndexOf(digitsTags, tags[x, y])] == -1)
                            digitsBottomY[Array.IndexOf(digitsTags, tags[x, y])] = y;
                    }
                    if (!digitsBottomY.Contains(-1))
                    {
                        y = yLowBarBottom;
                        break;
                    }
                }
                
                //cut image to only get the current digit
                for (int i = 0; i < digitsTags.Length; i++)
                {
                    digitImage = SS_OpenCV.ImageClass.ImageCut(img.Copy(), digitsRightX[i], digitsLeftX[i], digitsBottomY[i], digitsUpperY[i]);
                    digitsValue[i] = DigitCompare(digitImage);
                }


                return string.Join("", digitsValue);
            }
        }






        //************************************************************************************************************************
        //                                                BAR CODE DIGIT COMPARISION W/ FOLDER DIGIT
        //************************************************************************************************************************
        public static int DigitCompare(Image<Bgr, byte> digitImage)
        {
            unsafe
            {
                //get all the images stored at "...Debug/digits/" to be compared
                string digitFolderPath = System.IO.Directory.GetCurrentDirectory() + "\\digits\\";
                string[] allImages = Directory.GetFiles(digitFolderPath, "*.*", SearchOption.AllDirectories);
                int numEqual, numDiff;
                double bestValue = -1; //best score for the comparison
                string bestImage = ""; //best image path 
                //variables for the bar code digit image
                MIplImage m = digitImage.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int width = digitImage.Width;
                int height = digitImage.Height;
                //variables for the project folder images
                Image<Bgr, byte> folderImg;
                MIplImage m_d;
                byte* dataPtr_d; // Pointer to the image
                int nChan_d; // number of channels - 3
                int padding_d;
                //-------------------------------------------------------------------------------------------------


                //iterate every image from the "digits" folder
                for (int i = 0; i < allImages.Length; i++)
                {
                    //get current image from the folder and make it equivalent to the bar code digit
                    folderImg = new Image<Bgr, byte>(allImages[i]);
                    folderImg = folderImg.Resize(width, height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                    SS_OpenCV.ImageClass.ConvertToBW_Otsu(folderImg);

                    numEqual = 0; //number of pixels equal in both images
                    numDiff = 0;  //number of pixels different in both images
                    m_d = folderImg.MIplImage;
                    nChan_d = m_d.nChannels; // number of channels - 3
                    padding_d = m_d.widthStep - m_d.nChannels * m_d.width; // alinhament bytes (padding)
                    dataPtr = (byte*)m.imageData.ToPointer();
                    dataPtr_d = (byte*)m_d.imageData.ToPointer();
                    
                    //seach for the same pixels in both images
                    for (int y = 0; y < height - 1; y++)
                    {
                        for (int x = 0; x < width - 1; x++)
                        {
                            if (dataPtr[0] == dataPtr_d[0]) numEqual++;
                            else numDiff++;
                            dataPtr += nChan;
                            dataPtr_d += nChan_d;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr_d += padding_d + nChan_d;
                        dataPtr += padding + nChan;
                    }
                    //update best image or not
                    if ( ((double)numEqual / numDiff) > bestValue)
                    {
                        bestValue = ((double)numEqual / numDiff);
                        bestImage = allImages[i];
                    }
                }

                return int.Parse(Path.GetFileNameWithoutExtension(bestImage).Split('_')[0]);
            }
        }


    }
}
