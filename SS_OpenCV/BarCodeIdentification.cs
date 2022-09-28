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
    class BarCodeIdentification
    {

        //************************************************************************************************************************
        //                                                BAR CODE IDENTIFICATION V1
        //************************************************************************************************************************
        public static bool BarCodeIdent(Emgu.CV.Image<Bgr, byte> imgForID, long resolution,
            out int xImgLeft, out int xImgRight, out int yImgUp, out int yImgBottom,
            out int xImgLeft2, out int xImgRight2, out int yImgUp2, out int yImgBottom2)
        {
            unsafe
            {
                int dimMask = 9; //dilatation mask dimensions
                int height = imgForID.Height, width = imgForID.Width;
                int numBorderElem = 0;                       //number of elements in the border
                List<int> listTagsBanned = new List<int>();  //list of banned tags, the tags from the bourders
                List<int> listTagsID = new List<int>();      //list of tags in the modified image
                List<int> listAreasID = new List<int>();     //list of areas for the corresponding tags
                List<int> listTagsAround = new List<int>();  //list of tags around the main bar code, to find for example the first digit
                int xImgLeftLim = 0, xImgRightLim = 0, yImgUpLim = 0, yImgBottomLim = 0; //boundaries to search for the second element
                xImgLeft2 = -1; xImgRight2 = -1; yImgBottom2 = -1; yImgUp2 = -1;
                //-------------------------------------------------------------------------------------------------


                //no need to filter the very small images
                if (resolution < 60000) {
                    xImgLeft = -1; xImgRight = -1; yImgUp = -1; yImgBottom = -1;
                    return true;
                }

                //determine mask dimensions based on the image resolution
                if (resolution > 1000000) dimMask = 17;
                else if (resolution > 700000) dimMask = 15;
                else if (resolution > 60000) dimMask = 13;

                //modify image to better identify the bar code
                ImageModification(imgForID, dimMask);

                //linked component algorithm - iterative
                int[,] tagsID = SS_OpenCV.ImageClass.IteractiveSegmentation(imgForID);

                //see border tags and how much is it
                for (int y = (dimMask / 2); y < height - (dimMask / 2); y++)
                {
                    if (tagsID[(dimMask / 2), y] != 0 && !listTagsBanned.Contains(tagsID[(dimMask / 2), y])) 
                        listTagsBanned.Add(tagsID[(dimMask / 2), y]);
                    if (tagsID[width - (dimMask / 2), y] != 0 && !listTagsBanned.Contains(tagsID[width - (dimMask / 2), y])) 
                        listTagsBanned.Add(tagsID[width - (dimMask / 2), y]);
                    if (tagsID[width - (dimMask / 2), y] != 0 || tagsID[(dimMask / 2), y] != 0) 
                        numBorderElem++;
                }
                for (int x = (dimMask / 2); x < width - (dimMask / 2); x++)
                {
                    if (tagsID[x, (dimMask / 2)] != 0 && !listTagsBanned.Contains(tagsID[x, (dimMask / 2)])) 
                        listTagsBanned.Add(tagsID[x, (dimMask / 2)]);
                    if (tagsID[x, height - (dimMask / 2)] != 0 && !listTagsBanned.Contains(tagsID[x, height - (dimMask / 2)])) 
                        listTagsBanned.Add(tagsID[x, height - (dimMask / 2)]);
                    if (tagsID[x, (dimMask / 2)] != 0 || tagsID[x, height - (dimMask / 2)] != 0) 
                        numBorderElem++;
                }
                List<int> listAreasBanned = new List<int>(new int[listTagsBanned.Count()]);


                //get every tag and the corresponding area
                for (int y = (dimMask / 2); y < height - (dimMask / 2); y++)
                {
                    for (int x = (dimMask / 2); x < width - (dimMask / 2); x++)
                    {
                        if (tagsID[x, y] != 0 && !listTagsBanned.Contains(tagsID[x, y]))
                        {
                            if (!listTagsID.Contains(tagsID[x, y]))
                            {
                                //add new element discovered
                                if (tagsID[x + 1, y + 1] != 0 && tagsID[x + 1, y] != 0)
                                {
                                    listTagsID.Add(tagsID[x, y]);
                                    listAreasID.Add(1);
                                }
                            }
                            //increase tag area
                            else listAreasID[listTagsID.FindIndex(ind => ind.Equals(tagsID[x, y]))]++;
                        }
                        //increase tag area of banned elements
                        else if (listTagsBanned.Contains(tagsID[x, y]))
                            listAreasBanned[listTagsBanned.FindIndex(ind => ind.Equals(tagsID[x, y]))]++;
                    }
                }


                //get the main tag with the highest area
                int mainTag = listTagsID[listAreasID.FindIndex(ind => ind.Equals(listAreasID.Max()))];

                //get the banned tag with the highest area
                int mainBannedTag = -1;      
                if (listTagsBanned.Count > 0)
                    mainBannedTag = listTagsBanned[listAreasBanned.FindIndex(ind => ind.Equals(listAreasBanned.Max()))];

                //see if modification was a success
                if (listAreasBanned.Count() != 0 && listAreasID.Count() != 0)
                {
                    //too much elements around the bar code
                    if (listAreasBanned.Max()/10 > listAreasID.Max())
                    {
                        xImgLeft = -1; xImgRight = -1; yImgUp = -1; yImgBottom = -1;
                        return false;
                    }
                }

                //no need to cut the image, if it is already clean
                if (listTagsID.Count == 1 || (listTagsID.Count < 3 && numBorderElem < 30))
                {
                    xImgLeft = -1; xImgRight = -1; yImgUp = -1; yImgBottom = -1;
                    return true;
                }

                //get main rectangle dimensions 
                BarCodeDimensionsID(mainTag, dimMask, tagsID, 
                    out xImgLeft, out xImgRight, out yImgUp, out yImgBottom,
                    out xImgLeftLim, out xImgRightLim, out yImgUpLim, out yImgBottomLim);

                //see tags of objects around the main one 
                for (int y = yImgUpLim; y < yImgBottomLim; y++)
                {
                    for (int x = xImgLeftLim; x < xImgRightLim; x++)
                    {
                        if (tagsID[x, y] != mainTag && tagsID[x, y] != 0 && !listTagsAround.Contains(tagsID[x, y]) && !listTagsBanned.Contains(tagsID[x, y]))
                            listTagsAround.Add(tagsID[x, y]);
                    }
                }

                //get the biggest object around the main one
                int bestSizeAround = 0, areasIndex = 0, tagAround = 0;     
                for (int i = 0; i < listTagsAround.Count; i++)
                {
                    areasIndex = listTagsID.FindIndex(ind => ind.Equals(listTagsAround[i]));
                    if (areasIndex == -1)  continue;
                    if (listAreasID[areasIndex] > bestSizeAround)
                    {
                        bestSizeAround = listAreasID[areasIndex];
                        tagAround = listTagsAround[i];
                    }
                }

                //get dimensions from the object around the main one
                if (bestSizeAround > (dimMask ^ 2) * 3)
                {
                    BarCodeDimensionsID(tagAround, dimMask, tagsID, 
                        out xImgLeft2, out xImgRight2, out yImgUp2, out yImgBottom2,
                        out _, out _, out _, out _);
                }

                return true;
            }
        }



        public static void ImageModification(Image<Bgr, byte> imgForID, int dimMask)
        {
            unsafe
            {
                //apply operations to modify the image
                SS_OpenCV.ImageClass.Sobel(imgForID, imgForID.Copy());
                SS_OpenCV.ImageClass.Negative(imgForID);
                SS_OpenCV.ImageClass.ConvertToBW_Otsu(imgForID);
                SS_OpenCV.ImageClass.Dilation(imgForID, imgForID.Copy(), SS_OpenCV.ImageClass.fill2DArray(dimMask, 1), dimMask);
            }
        }



        public static void BarCodeDimensionsID(int mainTag, int dimMask, int[,] tags,
            out int xImgLeft, out int xImgRight, out int yImgUp, out int yImgBottom,
            out int xImgLeftLim, out int xImgRightLim, out int yImgUpLim, out int yImgBottomLim)
        {
            unsafe
            {
                int width = tags.GetLength(0);
                int height = tags.GetLength(1);
                xImgLeft = 0; xImgRight = 0; yImgBottom = 0; yImgUp = 0;
                xImgLeftLim = 0; xImgRightLim = 0; yImgBottomLim = 0; yImgUpLim = 0;
                //-------------------------------------------------------------------------------------------------

                //find leftmost X
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (tags[x, y] == mainTag)
                        {
                            xImgLeft = x;
                            x = width;
                            break;
                        }
                    }
                }
                //find rightmost X
                for (int x = width - 1; x >= 0; x--)
                {
                    for (int y = height - 1; y >= 0; y--)
                    {
                        if (tags[x, y] == mainTag)
                        {
                            xImgRight = x;
                            x = 0;
                            break;
                        }
                    }
                }
                //find highest Y
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (tags[x, y] == mainTag)
                        {
                            yImgUp = y;
                            y = height;
                            break;
                        }
                    }
                }
                //find lowest Y
                for (int y = height - 1; y >= 0; y--)
                {
                    for (int x = width - 1; x >= 0; x--)
                    {
                        if (tags[x, y] == mainTag)
                        {
                            yImgBottom = y;
                            y = 0;
                            break;
                        }
                    }
                }

                //get some space to find the objects around
                int barcodeWidth = xImgRight - xImgLeft;
                int barcodeHeight = yImgBottom - yImgUp;
                const double cnstBound = 0.01;
                xImgLeftLim = xImgLeft - (int)(barcodeWidth * cnstBound * dimMask / 2); 
                xImgRightLim = xImgRight + (int)(barcodeWidth * cnstBound * dimMask / 2);
                yImgUpLim = yImgUp - (int)(barcodeHeight * cnstBound * dimMask / 2);
                yImgBottomLim = yImgBottom + (int)(barcodeHeight * cnstBound * dimMask / 2); 

                //bound limiter
                if (xImgLeftLim < 0) xImgLeftLim = dimMask / 2 + 1;
                if (xImgRightLim >= width) xImgRightLim = width - dimMask / 2 - 1;
                if (yImgUpLim < 0) yImgUpLim = dimMask / 2 + 1;
                if (yImgBottomLim >= height) yImgBottomLim = height - dimMask / 2 - 1;

                //get space from the object, to avoid errors
                xImgLeft -= dimMask;
                xImgRight += dimMask;
                yImgUp -= dimMask;
                yImgBottom += dimMask;
            }
        }





        //************************************************************************************************************************
        //                                                BAR CODE IDENTIFICATION V2
        //************************************************************************************************************************
        public static int[,] BarCodeIdentFor2(Emgu.CV.Image<Bgr, byte> imgForID, out int numberOfBC)
        {
            unsafe
            {
                numberOfBC = 0; //number of bar codes presented
                List<int> listTagsID = new List<int>();
                
                //modify image to better identify the bar code
                int dimMask = 3;
                ImageModification(imgForID, dimMask);

                //linked component algorithm - iterative
                int[,] tags = SS_OpenCV.ImageClass.IteractiveSegmentation(imgForID);
                int width = tags.GetLength(0);
                int height = tags.GetLength(1);


                //get every tag and the corresponding first coord (top left)
                List<string> listTopCorner = new List<string>();
                for (int y = (dimMask / 2); y < height - (dimMask / 2); y++)
                {
                    for (int x = (dimMask / 2); x < width - (dimMask / 2); x++)
                    {
                        if (tags[x, y] != 0 && !listTagsID.Contains(tags[x, y]))
                        {
                            if (tags[x + 1, y + 1] != 0 && tags[x + 1, y] != 0)
                            {
                                listTagsID.Add(tags[x, y]);
                                listTopCorner.Add(x + "/" + y);
                            }
                        }
                    }
                }


                //get the last coord of each tag (bottom right)
                int indexTag;
                int[,] listBottom = new int[2, listTagsID.Count];
                for (int y = height - (dimMask / 2); y > (dimMask / 2); y--)
                {
                    for (int x = width - (dimMask / 2); x > (dimMask / 2); x--)
                    {
                        if (tags[x, y] != 0 && listTagsID.Contains(tags[x, y]))
                        {
                            indexTag = listTagsID.FindIndex(ind => ind.Equals(tags[x, y]));
                            if (listBottom[1, indexTag] == 0)
                            {
                                listBottom[0, indexTag] = x;
                                listBottom[1, indexTag] = y;
                            }
                        }
                    }
                }


                //get and sort the biggest elements
                int[,] arrayBigObj = new int[4, 11]; //contains the info from the 4 biggest objects
                int[] arrayBestIndex = new int[4] { 0, 0, 0, 0 }; //sorted with the index of the biggest objects
                double[] arrayBestDiameter = new double[4] { 0, 0, 0, 0 }; //sorted with the distance of the biggest objects
                int[] arrayBestTags = new int[4]; //tags of the biggest objects
                int startX, startY, endX, endY;
                double currentDia;
                for (int i = 0; i < listTagsID.Count; i++)
                {
                    startX = Int32.Parse(listTopCorner[i].Split('/')[0]);
                    startY = Int32.Parse(listTopCorner[i].Split('/')[1]);
                    endX = listBottom[0, i];
                    endY = listBottom[1, i];
                    currentDia = Math.Sqrt(Math.Pow(startX - endX, 2) + Math.Pow(startY - endY, 2));
                    if(currentDia < 100) continue;
                    for (int j = 0; j < 4; j++)
                    {
                        //sort and update biggest objects
                        if (currentDia > arrayBestDiameter[j])
                        {
                            for (int k = 3; k > j; k--)
                            {
                                arrayBestDiameter[k] = arrayBestDiameter[k - 1];
                                arrayBestIndex[k] = arrayBestIndex[k - 1];
                            }
                            arrayBestDiameter[j] = currentDia;
                            arrayBestIndex[j] = i;
                            break;
                        }
                    }
                }

                //fill the array with info from the 4 biggest objects
                for (int j = 0; j < 4; j++)
                {
                    //tags
                    arrayBestTags[j] = listTagsID[arrayBestIndex[j]];
                    arrayBigObj[j, 0] = listTagsID[arrayBestIndex[j]];
                    //top coordinates
                    arrayBigObj[j, 1] = Int32.Parse(listTopCorner[arrayBestIndex[j]].Split('/')[0]);
                    arrayBigObj[j, 2] = Int32.Parse(listTopCorner[arrayBestIndex[j]].Split('/')[1]);
                    //bottom coordinates
                    arrayBigObj[j, 3] = listBottom[0, arrayBestIndex[j]];
                    arrayBigObj[j, 4] = listBottom[1, arrayBestIndex[j]];
                }


                //get the left coordinates
                int counterElem = 0;
                for (int x = (dimMask / 2); x < width - (dimMask / 2); x++)
                {
                    for (int y = height - (dimMask / 2); y > (dimMask / 2); y--)
                    {
                        if (arrayBestTags.Contains(tags[x, y]))
                        {
                            indexTag = Array.IndexOf(arrayBestTags, tags[x, y]);
                            if (arrayBigObj[indexTag, 5] == 0)
                            {
                                arrayBigObj[indexTag, 5] = x;
                                arrayBigObj[indexTag, 6] = y;
                                counterElem++;
                            }
                        }
                    }
                    if (counterElem == 4) break;
                }


                //get the right coordinates
                counterElem = 0;
                for (int x = width - (dimMask / 2); x > (dimMask / 2); x--)
                {
                    for (int y = (dimMask / 2); y < height - (dimMask / 2); y++)
                    {
                        if (arrayBestTags.Contains(tags[x, y]))
                        {
                            indexTag = Array.IndexOf(arrayBestTags, tags[x, y]);
                            if (arrayBigObj[indexTag, 7] == 0)
                            {
                                arrayBigObj[indexTag, 7] = x;
                                arrayBigObj[indexTag, 8] = y;
                                counterElem++;
                            }
                        }
                    }
                    if (counterElem == 4) break;
                }


                //see number of tags inside the main rectangle
                List<int> listCenterTags = new List<int>();
                for (int i = 0; i < 4; i++)
                {
                    //center coordinates
                    arrayBigObj[i, 9] = (arrayBigObj[i, 1] + arrayBigObj[i, 3] + arrayBigObj[i, 5] + arrayBigObj[i, 7]) / 4;
                    arrayBigObj[i, 10] = (arrayBigObj[i, 2] + arrayBigObj[i, 4] + arrayBigObj[i, 6] + arrayBigObj[i, 8]) / 4;

                    //get tags in a straight horizontal and vertical line
                    for (int x = arrayBigObj[i, 5]; x < arrayBigObj[i, 7]; x++)
                    {
                        if (!listCenterTags.Contains(tags[x, arrayBigObj[i, 10]]))
                            listCenterTags.Add(tags[x, arrayBigObj[i, 10]]);
                    }
                    for (int y = arrayBigObj[i, 2]; y < arrayBigObj[i, 4]; y++)
                    {
                        if (!listCenterTags.Contains(tags[arrayBigObj[i, 9], y]))
                            listCenterTags.Add(tags[arrayBigObj[i, 9], y]);
                    }

                    //decide if it is a BC depending on the number of elements inside the objects
                    if (listCenterTags.Count < 10 || listCenterTags.Count > 21)
                        arrayBigObj[i, 0] = -1;
                        
                    listCenterTags.Clear();
                }

                //convert to a array with the bar code info only
                int[,] arrayBarCodes = new int[2, 11];
                for (int i = 0; i < 4; i++)
                {
                    if(arrayBigObj[i, 0] != -1)
                    {
                        for(int j = 0; j < 11; j++)
                            arrayBarCodes[numberOfBC, j] = arrayBigObj[i, j];
                        numberOfBC++;
                    }
                }

                return arrayBarCodes;
            }
        }






        //************************************************************************************************************************
        //                                                BAR CODE DIMENSIONS
        //************************************************************************************************************************
        public static void BarCodeDimensions(int[,] tags,
            out int xImgLeft, out int xImgRight, out int yImgUp, out int yImgBottom,
            out int xBarLeft, out int yLowBarBottom, out int yHighBarBottom,
            out Point bc_centroid1, out Size bc_size1, out List<int> listLowBarTags)
        {
            unsafe
            {
                int width = tags.GetLength(0);
                int height = tags.GetLength(1);
                xImgLeft = 0; xImgRight = 0; yImgBottom = 0; yImgUp = 0;

                //find leftmost X
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (tags[x, y] != 0)
                        {
                            xImgLeft = x;
                            x = width;
                            break;
                        }
                    }
                }
                //find rightmost X
                for (int x = width - 1; x >= 0; x--)
                {
                    for (int y = height - 1; y >= 0; y--)
                    {
                        if (tags[x, y] != 0)
                        {
                            xImgRight = x;
                            x = 0;
                            break;
                        }
                    }
                }
                //find highest Y
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (tags[x, y] != 0)
                        {
                            yImgUp = y;
                            y = height;
                            break;
                        }
                    }
                }
                //find lowest Y
                for (int y = height - 1; y >= 0; y--)
                {
                    for (int x = width - 1; x >= 0; x--)
                    {
                        if (tags[x, y] != 0)
                        {
                            yImgBottom = y;
                            y = 0;
                            break;
                        }
                    }
                }

                //get the size of the rectangle and his center, to draw the image 
                int size_BC_x = xImgRight - xImgLeft;
                int size_BC_y = yImgBottom - yImgUp;
                int center_x = xImgRight - (size_BC_x / 2);
                int center_y = yImgBottom - (size_BC_y / 2);
                bc_centroid1 = new Point(center_x, center_y);
                bc_size1 = new Size(size_BC_x, size_BC_y);

                //get the tag of each bar
                listLowBarTags = new List<int>(); //list of bar tags 
                int y_middle = (yImgBottom + yImgUp) / 2 - 15; 
                for (int x = xImgLeft; x < xImgRight; x++)
                {
                    if (tags[x, y_middle] != 0 && !listLowBarTags.Contains(tags[x, y_middle]))
                        listLowBarTags.Add(tags[x, y_middle]);
                }

                //get the low bar y value
                int barCounter = 0;
                int[] arrayHighBar = Enumerable.Repeat(-1, listLowBarTags.Count()).ToArray();
                bool boolFirst = true;
                yLowBarBottom = 0;  // y value from the low bar codes 
                yHighBarBottom = 0; // y value from the high bar codes 
                for (int y = yImgBottom; y > yImgUp; y--)
                {
                    barCounter = 0;
                    arrayHighBar = Enumerable.Repeat(-1, listLowBarTags.Count()).ToArray();
                    for (int x = xImgLeft; x < xImgRight; x++)
                    {
                        if (listLowBarTags.Contains(tags[x, y]) && !arrayHighBar.Contains(tags[x, y]))
                        {
                            arrayHighBar[barCounter++] = tags[x, y];
                            if (boolFirst && barCounter == 2)
                            {
                                yHighBarBottom = y;
                                boolFirst = false;
                            }
                        }
                    }
                    if (barCounter == listLowBarTags.Count())
                    {
                        yLowBarBottom = y;
                        y = 0;
                        break;
                    }
                }

                //get the left bar coordinate
                xBarLeft = 0;  //x first right pixel for the bars
                for (int x = xImgLeft; x < xImgRight; x++)
                {
                    for (int y = yImgUp; y < yLowBarBottom - 1; y++)
                    {
                        if (tags[x, y] != 0)
                        {
                            xBarLeft = x;
                            x = xImgRight;
                            break;
                        }
                    }
                }
                //
            }
        }


    }
}
