using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SS_OpenCV
{
    class BarRecognition
    {
        //************************************************************************************************************************
        //                                                BAR RECOGNITION 
        //************************************************************************************************************************
        public static string[] BarRecon(int[,] tags, double PERC, int xBarLeft, int xImgRight, int yImgUp, int yLowBarBottom)
        {
            unsafe
            {
                long resolution = (xImgRight- xBarLeft) * (yLowBarBottom - yImgUp);
                double numTotal = 0; //total number of black pixels in a column
                string currentBin = ""; //current color being iterated ("1"->black bar and "0"->white bar)
                string lastBin = ""; //last color being iterated
                int currentWidth = 1; //width of the current bar
                bool bool_index_6 = true; //separate between bar code group 1 and 2 
                string[] digitsValue = new string[12]; //array w/ all the binary codes from the bar
                int index = 0, index_digs = 0;
                int currentSize = 0;
                int inclinationOffset = 0;
                int y_middle = 0;
                //-------------------------------------------------------------------------------------------------

                if (resolution < 30000) PERC = 0.6;
                //get bar width, using approximations
                int bar_width = (int)Math.Round(((double)xImgRight - xBarLeft) / 95);

                //see if bar code has inclination
                for (int x = xBarLeft; x < xImgRight; x++)
                {
                    if (tags[x, yLowBarBottom - 2] == 0) inclinationOffset++;
                    else break;
                }

                //get the binary code from the bars
                for (int x = xBarLeft; x < xImgRight; x++)
                {
                    numTotal = 0;
                    //see if the column is in majority one or zero
                    if(inclinationOffset < bar_width + 1)
                    {
                        for (int y = yImgUp; y < yLowBarBottom; y++)
                        {
                            if (tags[x, y] != 0) numTotal++;
                        }
                        if (numTotal / ((yLowBarBottom - yImgUp)) > PERC) currentBin = "1";
                        else currentBin = "0";
                    }
                    else //if there is inclination, reduce bar height
                    {
                        y_middle = (yLowBarBottom + 3 * yImgUp) / 4; 
                        for (int y = yImgUp; y < y_middle; y++)
                        {
                            if (tags[x, y] != 0) numTotal++;
                        }
                        if (numTotal / ((y_middle - yImgUp)) > PERC) currentBin = "1";
                        else currentBin = "0";
                    }


                    //first iteration
                    if (lastBin.Equals(""))
                    {
                        lastBin = currentBin;
                        continue;
                    }

                    //increment bar width until it reaches a new bar with a different value
                    if (lastBin.Equals(currentBin))  
                        currentWidth++;
                    else
                    {
                        //get size of current bar
                        currentSize = (int)Math.Round((double)currentWidth / bar_width);
                        //get current bar width, using approximations-> to see bar size
                        for (int i = 0; i < currentSize; i++, index++)
                        {
                            //iteration for the first group of bars
                            if (index < 3 && bool_index_6) continue; //ignore the first three bars
                            if ((index - 3) % 7 == 0 && index - 3 > 0 && bool_index_6) index_digs++;

                            //iteration for the second group of bars
                            if (index_digs == 6 && bool_index_6)
                            {
                                bool_index_6 = false;
                                index = 0;
                            }
                            if (index < 5 && !bool_index_6) continue; //ignore the five middle bars
                            if ((index - 5) % 7 == 0 && index - 5 > 0 && !bool_index_6) index_digs++;

                            if (index_digs == 12) 
                                return digitsValue;

                            //update array with current binary value
                            digitsValue[index_digs] = digitsValue[index_digs] + lastBin;
                        }
                        //update info for the next bar
                        lastBin = currentBin;
                        currentWidth = 1;
                    }
                }

                return digitsValue;
            }
        }




        //************************************************************************************************************************
        //                                                CONVERT BINARY VALUE TO DECIMAL
        //************************************************************************************************************************
        public static string ConvertBinToDec(string[] digitsValue, string[] first_digits, string[] L_code, string[] R_code, string[] G_code)
        {
            unsafe
            {
                string digit = "L"; //to get, afterwards, the first digit
                //get second digit
                string bc_image = (Array.IndexOf(L_code, digitsValue[0])).ToString();

                //get rest of digits
                for (int i = 1; i < digitsValue.Length; i++)
                {
                    //first group of bars
                    if (i < 6)
                    {
                        if (Array.IndexOf(L_code, digitsValue[i]) != -1)
                        {
                            bc_image = bc_image + (Array.IndexOf(L_code, digitsValue[i])).ToString();
                            digit = digit + "L";
                        }
                        else
                        {
                            bc_image = bc_image + (Array.IndexOf(G_code, digitsValue[i])).ToString();
                            digit = digit + "G";
                        }
                    }
                    //second group of bars
                    else bc_image = bc_image + (Array.IndexOf(R_code, digitsValue[i])).ToString();
                    //end earlier if there is an error
                    if (bc_image.Contains("-1")) return "-1";
                }
                //first digit
                bc_image = (Array.IndexOf(first_digits, digit)).ToString() + bc_image;

                return bc_image;
            }
        }



    }
}
