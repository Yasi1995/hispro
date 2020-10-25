using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace histogrameLab
{
    class Histogram
    {
        IplImage image, image1, histImage, sobelx, sobely, tempx, tempy;

        public void LoadImage() {

            image = Cv.LoadImage("surf.jpg" , LoadMode.Color);
            Cv.SaveImage("1.jpg",image);
        }
        public void ConvertToGray() {
            image1 = Cv.CreateImage(image.Size , BitDepth.U8 , 1);
            Cv.CvtColor(image , image1 , ColorConversion.RgbaToGray);
            Cv.SaveImage("2.jpg", image1);
        }
        public void DrawHistogram() {

            //variable declaration
            float [] range = {0, 255 };
            float[][] ranges = { range}; //defining 2D array
            int his_size = 256;
            float min_value, max_value = 0;

            //call convertgray function
            ConvertToGray();

            //create an inage to hold the histogarm
            histImage = Cv.CreateImage(image1.Size, BitDepth.U8 , 1);

            //create a histogarm to store information from the image
            CvHistogram hist = Cv.CreateHist(new int [] { his_size },
                HistogramFormat.Array , ranges, true);

                //rangers ==> starting and ending values of the range
                //HistogramFormat.Array ==> 2D array
                //true ==> each scale of the histogarm is eqaully distributed
                //new int [] { his_size } ==> y axis is given as a dynamic array since the limit is not known/fixed

            //calculate the histogram and apply to histogram
            Cv.CalcHist(image1, hist);

            //grab min and max
            Cv.GetMinMaxHistValue(hist , out min_value , out max_value);

            //scale the bin values (gaps) so that thay will fit in the image representation
            Cv.Scale(hist.Bins , hist.Bins ,((double) histImage.Height) / max_value , 0 );

            //hist.Bins ==> for the source and the destination to be scaled.
            //((double) histImage.Height) / max_value ==> scale value.
            //0 ==> to represent a null mask. its a default parameter.

            //set all histogram values to 255
            histImage.Set(CvColor.White);

            //create a factor for scaling along the width
            int bin_w = Cv.Round(((double)histImage.Width / his_size));

            int i;
            for(i=0; i<his_size; i++)
            {
                histImage.Rectangle(
                    new CvPoint(i*bin_w, image1.Height),
                    new CvPoint((i+1)*bin_w, image1.Height - Cv.Round(hist.Bins[i])),
                    CvColor.Black, //line drawn in black
                    -1,  //thickness
                    LineType.Link8 , //indicated the link type (connected)
                    0  //shift bit (factional bits in the coordinate)
                    );
            }
            Cv.SaveImage("3.jpg", histImage);

        }
        public void DrawEqualizedHistogram()
        {

            //variable declaration
            float[] range = { 0, 255 };
            float[][] ranges = { range }; //defining 2D array
            int his_size = 256;
            float min_value, max_value = 0;

            //call convertgray function
            ConvertToGray();

            //create an inage to hold the histogarm
            histImage = Cv.CreateImage(image1.Size, BitDepth.U8, 1);

            //call equalized method
            Cv.EqualizeHist(image1 , image1);

            //create a histogarm to store information from the image
            CvHistogram hist = Cv.CreateHist(new int[] { his_size },
                HistogramFormat.Array, ranges, true);

            //rangers ==> starting and ending values of the range
            //HistogramFormat.Array ==> 2D array
            //true ==> each scale of the histogarm is eqaully distributed
            //new int [] { his_size } ==> y axis is given as a dynamic array since the limit is not known/fixed
        

            //calculate the histogram and apply to histogram
            Cv.CalcHist(image1, hist);

            //grab min and max
            Cv.GetMinMaxHistValue(hist, out min_value, out max_value);

            //scale the bin values (gaps) so that thay will fit in the image representation
            Cv.Scale(hist.Bins, hist.Bins, ((double)histImage.Height) / max_value, 0);

            //hist.Bins ==> for the source and the destination to be scaled.
            //((double) histImage.Height) / max_value ==> scale value.
            //0 ==> to represent a null mask. its a default parameter.

            //set all histogram values to 255
            histImage.Set(CvColor.White);

            //create a factor for scaling along the width
            int bin_w = Cv.Round(((double)histImage.Width / his_size));

            int i;
            for (i = 0; i < his_size; i++)
            {
                histImage.Rectangle(
                    new CvPoint(i * bin_w, image1.Height),
                    new CvPoint((i + 1) * bin_w, image1.Height - Cv.Round(hist.Bins[i])),
                    CvColor.Black, //line drawn in black
                    -1,  //thickness
                    LineType.Link8, //indicated the link type (connected)
                    0  //shift bit (factional bits in the coordinate)
                    );
            }
            Cv.SaveImage("4.jpg", histImage);
            Cv.SaveImage("5.jpg", image1);

        }
        public void Sobel() //detect edges
        {
            //convert to gray images
            ConvertToGray();

            sobelx = Cv.CreateImage(image.Size, BitDepth.U8, 1);
            sobely = Cv.CreateImage(image.Size, BitDepth.U8, 1);

            //create temp images
            //to store negative values. U8 is not capable of storing negative values
            tempx = Cv.CreateImage(image.Size, BitDepth.S16, 1);
            tempy = Cv.CreateImage(image.Size, BitDepth.S16, 1);

            Cv.Sobel(image1, tempx, 1, 0, ApertureSize.Size3);
            Cv.Sobel(image1, tempx, 0, 1, ApertureSize.Size3);

            //convert signed 16 to unsighed 8
            Cv.ConvertScaleAbs(tempx, sobelx);
            Cv.ConvertScaleAbs(tempy, sobely);

            //save
            Cv.SaveImage("sobelx.jpg", sobelx);
            Cv.SaveImage("sobely.jpg", sobely);
        }
    }
}
