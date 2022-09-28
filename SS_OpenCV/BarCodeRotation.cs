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
    class BarCodeRotation
    {

        //************************************************************************************************************************
        //                                                FIND BAR CODE CORNERS
        //************************************************************************************************************************
        public static int[,] FindTagCorners(int[,] sideTagInfo, int tagsXY, int numIter, int x, int y, out int boolEnd)
        {
            boolEnd = 0;
            //boolEnd = 0 -> continue
            //        = 1 -> break the nested for
            //        = 2 -> break the main for
            if (sideTagInfo[numIter, 0] == 0)
            { //get first tag element and info
                sideTagInfo[numIter, 0] = tagsXY;
                sideTagInfo[numIter, 1] = x;
                sideTagInfo[numIter, 2] = y;
            }

            if (sideTagInfo[numIter, 0] == tagsXY)
            { //increment 1st tag size
                sideTagInfo[numIter, 3]++;
                boolEnd = 1; //break;
            }
            else if (sideTagInfo[numIter, 3] < 3) // before 2
            { //ignore the very small tag elements
                sideTagInfo[numIter, 0] = 0;
                sideTagInfo[numIter, 3] = 0;
                boolEnd = 1; //break;
            }
            else if (sideTagInfo[numIter, 4] == 0 && sideTagInfo[numIter, 0] != 0)
            { //go to the second tag element
                sideTagInfo[numIter, 4] = tagsXY;
                sideTagInfo[numIter, 5] = x;
                sideTagInfo[numIter, 6] = y;
            }

            if (sideTagInfo[numIter, 4] == tagsXY)
            { //increment 2nd tag size
                sideTagInfo[numIter, 7]++;
                boolEnd = 1; //break;
            }
            else if (sideTagInfo[numIter, 7] < 3) // before 2
            { //ignore the very small tag elements
                sideTagInfo[numIter, 4] = 0;
                sideTagInfo[numIter, 7] = 0;
                boolEnd = 1; //break;
            }
            else if (sideTagInfo[numIter, 4] != 0 && sideTagInfo[numIter, 0] != 0)
            { //ended 2nd tag and iteration
                boolEnd = 2;
            }

            return sideTagInfo;
        }





        //************************************************************************************************************************
        //                                                BAR CODE ROTATION
        //************************************************************************************************************************
        public static bool RotateBarCode(Emgu.CV.Image<Bgr, byte> img, int[,] tags, long resolution)
        {
            unsafe
            {
                int width = img.Width;
                int height = img.Height;
                int[,] sideTagInfo = new int[8, 8]; //array with all the info to find the right rotation
                int boolEnd = 0; //control iteration of elements in search of the last bar
                float angle = 0;
                int bestCol = 0, bestCol2 = 0, bestRow2 = 0;
                int bestSize = 0, bestSize2 = 0;
                int errorMargin = 3;
                //-------------------------------------------------------------------------------------------------


                //[0] get first two tag elements going up to bottom for (0,0)
                for (int y = 0; y < height - 1; y++)
                {
                    for (int x = 0; x < width - 1; x++)
                    {
                        if (tags[x, y] != 0)
                        {
                            sideTagInfo = FindTagCorners(sideTagInfo, tags[x, y], 0, x, y, out boolEnd);
                            if (boolEnd == 2) y = height;
                            if (boolEnd != 0) break;
                        }
                    }
                }
                //[1] get first two tag elements going left to right for (0,0)
                for (int x = 0; x < width - 1; x++)
                {
                    for (int y = 0; y < height - 1; y++)
                    {
                        if (tags[x, y] != 0)
                        {
                            sideTagInfo = FindTagCorners(sideTagInfo, tags[x, y], 1, x, y, out boolEnd);
                            if (boolEnd == 2) x = width;
                            if (boolEnd != 0) break;
                        }
                    }
                }
                //[2] get first two tag elements going right to left for (width,0)
                for (int x = width - 1; x > 0; x--)
                {
                    for (int y = 0; y < height - 1; y++)
                    {
                        if (tags[x, y] != 0)
                        {
                            sideTagInfo = FindTagCorners(sideTagInfo, tags[x, y], 2, x, y, out boolEnd);
                            if (boolEnd == 2) x = 0;
                            if (boolEnd != 0) break;
                        }
                    }
                }
                //[3] get first two tag elements going up to bottom for (width,0)
                for (int y = 0; y < height - 1; y++)
                {
                    for (int x = width - 1; x > 0; x--)
                    {
                        if (tags[x, y] != 0)
                        {
                            sideTagInfo = FindTagCorners(sideTagInfo, tags[x, y], 3, x, y, out boolEnd);
                            if (boolEnd == 2) y = height;
                            if (boolEnd != 0) break;
                        }
                    }
                }
                //[4] get first two tag elements going bottom to up for (width,height)
                for (int y = height - 1; y > 0; y--)
                {
                    for (int x = width - 1; x > 0; x--)
                    {
                        if (tags[x, y] != 0)
                        {
                            sideTagInfo = FindTagCorners(sideTagInfo, tags[x, y], 4, x, y, out boolEnd);
                            if (boolEnd == 2) y = 0;
                            if (boolEnd != 0) break;
                        }
                    }
                }
                //[5] get first two tag elements going right to left for (width,height)
                for (int x = width - 1; x > 0; x--)
                {
                    for (int y = height - 1; y > 0; y--)
                    {
                        if (tags[x, y] != 0)
                        {
                            sideTagInfo = FindTagCorners(sideTagInfo, tags[x, y], 5, x, y, out boolEnd);
                            if (boolEnd == 2) x = 0;
                            if (boolEnd != 0) break;
                        }
                    }
                }
                //[6] get first two tag elements going left to right for (0,height)
                for (int x = 0; x < width - 1; x++) //1
                {
                    for (int y = height - 1; y > 0; y--)
                    {
                        if (tags[x, y] != 0)
                        {
                            sideTagInfo = FindTagCorners(sideTagInfo, tags[x, y], 6, x, y, out boolEnd);
                            if (boolEnd == 2) x = width;
                            if (boolEnd != 0) break;
                        }
                    }
                }
                //[7] get first two tag elements going bottom to up for (0,height)
                for (int y = height - 1; y > 0; y--)
                {
                    for (int x = 0; x < width - 1; x++)
                    {
                        if (tags[x, y] != 0) //1S
                        {
                            sideTagInfo = FindTagCorners(sideTagInfo, tags[x, y], 7, x, y, out boolEnd);
                            if (boolEnd == 2) y = 0;
                            if (boolEnd != 0) break;
                        }
                    }
                }

                //get the highest bar size for the first and second tag
                int size1stElem = (from row in Enumerable.Range(0, sideTagInfo.GetLength(0)) select sideTagInfo[row, 3]).Max();
                int size2ndElem = (from row in Enumerable.Range(0, sideTagInfo.GetLength(0)) select sideTagInfo[row, 7]).Max();

                //error margin depending on image resolution
                if (resolution > 1000000) errorMargin = 6;
                else if (resolution > 700000) errorMargin = 5;
                else if (resolution > 60000) errorMargin = 4;

                //end earlier if there is no angle (bar code horizontal w/ digits at the bottom)
                if (Math.Abs(sideTagInfo[3, 3] - sideTagInfo[4, 7]) < 3 && sideTagInfo[0, 1] - sideTagInfo[7, 5] < errorMargin && (sideTagInfo[3, 3] == size1stElem || sideTagInfo[4, 7] == size2ndElem))
                    return false;
                // bar code vertical w/ digits at the left
                else if (Math.Abs(sideTagInfo[5, 3] - sideTagInfo[6, 7]) < 3 && Math.Abs(sideTagInfo[2, 1] - sideTagInfo[1, 6]) < errorMargin && (sideTagInfo[5, 3] == size1stElem || sideTagInfo[6, 7] == size2ndElem))
                    angle = -(float)(0.5 * Math.PI);
                // bar code horizontal w/ digits at the top
                else if (Math.Abs(sideTagInfo[7, 3] - sideTagInfo[0, 7]) < 3 && Math.Abs(sideTagInfo[4, 1] - sideTagInfo[3, 5]) < errorMargin && (sideTagInfo[7, 3] == size1stElem || sideTagInfo[0, 7] == size2ndElem))
                    angle = (float)Math.PI;
                // bar code vertical w/ digits at the right
                else if (Math.Abs(sideTagInfo[1, 3] - sideTagInfo[2, 7]) < 3 && Math.Abs(sideTagInfo[6, 2] - sideTagInfo[5, 6]) < errorMargin && (sideTagInfo[1, 3] == size1stElem || sideTagInfo[2, 7] == size2ndElem))
                    angle = (float)(0.5 * Math.PI);
                else
                {
                    //see if the highest bar is a 1st or 2nd tag
                    if(size1stElem == size2ndElem)
                    {
                        // bar code vertical w/ digits at the bottom
                        if ((sideTagInfo[3, 3] == size1stElem || sideTagInfo[4, 7] == size2ndElem) && (sideTagInfo[2, 3] == sideTagInfo[5, 3]) )
                            return false;
                        // bar code vertical w/ digits at the left
                        else if ((sideTagInfo[5, 3] == size1stElem || sideTagInfo[6, 7] == size2ndElem) && (sideTagInfo[4, 3] == sideTagInfo[7, 3]))
                            angle = -(float)(0.5 * Math.PI);
                        // bar code horizontal w/ digits at the top
                        else if ((sideTagInfo[7, 3] == size1stElem || sideTagInfo[0, 7] == size2ndElem) && (sideTagInfo[1, 3] == sideTagInfo[6, 3]))
                            angle = (float)Math.PI;
                        // bar code vertical w/ digits at the right
                        else if ((sideTagInfo[1, 3] == size1stElem || sideTagInfo[2, 7] == size2ndElem) && (sideTagInfo[0, 3] == sideTagInfo[3, 3]))
                            angle = (float)(0.5 * Math.PI);

                        if (angle != 0)
                        {
                            SS_OpenCV.ImageClass.Rotation_Bilinear_White(img, img.Copy(), angle);
                            return true;
                        }
                    }
                    
                    if(size1stElem >= size2ndElem)
                    {
                        bestSize = size1stElem;
                        bestCol = 3;
                        bestSize2 = size2ndElem;
                    }
                    else
                    {
                        bestSize = size2ndElem;
                        bestCol = 7;
                        bestSize2 = size1stElem;
                    }

                    //get the corresponding best row and tag
                    int bestRow = Enumerable.Range(0, sideTagInfo.GetLength(0))
                                         .Where(index => sideTagInfo[index, bestCol] == bestSize).First();
                    int bestTag = sideTagInfo[bestRow, bestCol - 3];

                    //in case the values are too similar
                    if(Math.Abs(size1stElem - size2ndElem) < 2)
                    {
                        int tempCol = 0;
                        if (bestCol == 3) tempCol = 7;
                        if (bestCol == 7) tempCol = 3;
                        //get the other element from the best row
                        int tempSize = sideTagInfo[bestRow, tempCol];
                        int tempRow = Enumerable.Range(0, sideTagInfo.GetLength(0))
                                         .Where(index => sideTagInfo[index, tempCol] == bestSize2).First();
                        int tempsize1stElem = sideTagInfo[tempRow, bestCol];
                        if(tempSize > tempsize1stElem)
                        {
                            bestRow = tempRow;
                            bestCol = tempCol;
                            bestTag = sideTagInfo[bestRow, tempCol - 3];
                        }
                    }

                    //associated with the best value, there is always another coordinate 
                    if (bestRow % 2 == 0) bestRow2 = bestRow + 1;
                    else bestRow2 = bestRow - 1;
                    bestCol2 = Enumerable.Range(0, sideTagInfo.GetLength(1))
                                         .Where(index => sideTagInfo[bestRow2, index] == bestTag).First() + 3;

                    //determine rotation angle
                    switch (bestRow)
                    {
                        //imagem with inclination \ and digits up = -(Angle+180)
                        case 0:
                            angle = -(float)(Math.Atan2(sideTagInfo[0, bestCol - 2] - sideTagInfo[1, bestCol2 - 2], sideTagInfo[1, bestCol2 - 1] - sideTagInfo[0, bestCol - 1]) + Math.PI);
                            break;
                        case 1:
                            angle = -(float)(Math.Atan2(sideTagInfo[0, bestCol2 - 2] - sideTagInfo[1, bestCol - 2], sideTagInfo[1, bestCol - 1] - sideTagInfo[0, bestCol2 - 1]) + Math.PI);
                            break;
                        //imagem with inclination / and digits down = Angle
                        case 2:
                            angle = (float)Math.Atan2(sideTagInfo[2, bestCol - 2] - sideTagInfo[3, bestCol2 - 2], sideTagInfo[2, bestCol - 1] - sideTagInfo[3, bestCol2 - 1]);
                            break;
                        case 3:
                            angle = (float)Math.Atan2(sideTagInfo[2, bestCol2 - 2] - sideTagInfo[3, bestCol - 2], sideTagInfo[2, bestCol2 - 1] - sideTagInfo[3, bestCol - 1]);
                            break;
                        //imagem with inclination \ and digits down = -Angle
                        case 4:
                            angle = -(float)Math.Atan2(sideTagInfo[5, bestCol2 - 2] - sideTagInfo[4, bestCol - 2], sideTagInfo[4, bestCol - 1] - sideTagInfo[5, bestCol2 - 1]);
                            break;
                        case 5:
                            angle = -(float)Math.Atan2(sideTagInfo[5, bestCol - 2] - sideTagInfo[4, bestCol2 - 2], sideTagInfo[4, bestCol2 - 1] - sideTagInfo[5, bestCol - 1]);
                            break;
                        //imagem with inclination / and digits up = Angle+180
                        case 6:
                            angle = (float)(Math.Atan2(sideTagInfo[7, bestCol2 - 2] - sideTagInfo[6, bestCol - 2], sideTagInfo[7, bestCol2 - 1] - sideTagInfo[6, bestCol - 1]) + Math.PI);
                            break;
                        case 7:
                            angle = (float)(Math.Atan2(sideTagInfo[7, bestCol - 2] - sideTagInfo[6, bestCol2 - 2], sideTagInfo[7, bestCol - 1] - sideTagInfo[6, bestCol2 - 1]) + Math.PI);
                            break;
                    }
                }

                SS_OpenCV.ImageClass.Rotation_Bilinear_White(img, img.Copy(), angle);
                return true;
            }
        }


    }
}
