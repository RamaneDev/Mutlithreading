using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Windows.Forms.AxHost;

namespace OldStarsFinder
{
    public partial class Form1 : Form
    {
        private int Count = 0;

        // Number of bitmaps to break the original into and add to 
        // the list of bitmaps.
        private List<Bitmap> BitmapList;

        //List of bitmaps to use the ParallelForEach on.
        Bitmap OriginalBitmap;

        private String prsOldStarsCount = "0";

        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap CropBitmap(Bitmap proBitmap, Rectangle proRectangle)
        {
            // Create a new bitmap copying the portion of the original 
            // defined by proRectangle and keeping its PixelFormat
            Bitmap loCroppedBitmap = proBitmap.Clone(proRectangle, proBitmap.PixelFormat);

            // Return the cropped bitmap
            return loCroppedBitmap;
        }

        public bool IsOldStar(Color poPixelColor)
        {
            // Hue between 150 and 258
            // Saturation more than 0.10
            // Brightness more than 0.90
            return ((poPixelColor.GetHue() >= 150) &&
             (poPixelColor.GetHue() <= 258) &&
             (poPixelColor.GetSaturation() >= 0.10) &&
             (poPixelColor.GetBrightness() <= 0.90));
        }


        private void ThreadOldStarsFinder(Bitmap loBitmap)
        {
            int liRow; // The pixel matrix (bitmap)row number(Y)
            int liCol; // The pixel matrix (bitmap)col number(X)
            Color loPixelColor; // The pixel color
                                // Iterate through each pixel matrix (bitmap) rows
            for (liRow = 0; liRow < loBitmap.Height; liRow++)
            {
                // Iterate through each pixel matrix (bitmap) cols
                for (liCol = 0; liCol < loBitmap.Width; liCol++)
                {
                    // Get the pixel Color for liCol and liRow
                    loPixelColor = loBitmap.GetPixel(liCol, liRow);
                    if (IsOldStar(loPixelColor))
                    {
                        // The color range corresponds to an old star
                        // Change its color to a pure blue
                        loBitmap.SetPixel(liCol, liRow, Color.Blue);

                        lock (prsOldStarsCount)
                        {
                            int i = Convert.
                           ToInt32(prsOldStarsCount);
                            i = i + 1;
                            prsOldStarsCount = i.ToString();
                        }
                    }
                }
            }
        }

        private void ShowBitmapWithOldStars()
        {

            Bitmap loBitmap;

            // The starting row in each iteration
            int liStartRow = 0;

            // Calculate each bitmap's height
            int liEachBitmapHeight = ((int)(OriginalBitmap.Height / Count)) + 1;

            // Create a new bitmap with the whole width and height
            loBitmap = new Bitmap(OriginalBitmap.Width, OriginalBitmap.Height);

            Graphics g = Graphics.FromImage((Image)loBitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            for (int liThreadNumber = 0; liThreadNumber < Count; liThreadNumber++)
            {
                // Draw each portion in its corresponding absolute starting row
                g.DrawImage(BitmapList[liThreadNumber], 0, liStartRow);
                // Increase the starting row
                liStartRow += liEachBitmapHeight;
            }
            // Show the bitmap in the PictureBox picStarsBitmap
            picStarsBitmap.Image = loBitmap;
            //picStarsBitmap.Image.Save("c:\\packt\\resulting_image.png", ImageFormat.Png);
            tbCount.Text = prsOldStarsCount;
            g.Dispose();
        }

        private void butFindOldStars_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            Count = Convert.ToInt32(tbTasks.Text);
            OriginalBitmap = new Bitmap(picStarsBitmap.Image);
            BitmapList = new List<Bitmap>(Count);
            int StartRow = 0;
            int EachBitmapHeight = ((int)(OriginalBitmap.Height / Count)) + 1;
            int HeightToAdd = OriginalBitmap.Height;
            Bitmap loBitmap;
            // Breakup the bitmap into a list of bitmaps.
            for (int i = 0; i < Count; i++)
            {
                if (EachBitmapHeight > HeightToAdd)
                {
                    // The last bitmap height perhaps is less than the other bitmaps height
                    EachBitmapHeight = HeightToAdd;
                }
                loBitmap = CropBitmap(OriginalBitmap, new
                Rectangle(0, StartRow, OriginalBitmap.Width, EachBitmapHeight));
                HeightToAdd -= EachBitmapHeight;
                StartRow += EachBitmapHeight;
                BitmapList.Add(loBitmap);
            }

            //Iterate through the list of bitmaps with the Parallel.ForEach command.
            // here we call Parallel.ForEach synchronously the main Thread will wait 
            // until thre forEach loop complete
            Parallel.ForEach(BitmapList, item => ThreadOldStarsFinder(item));
            
            ShowBitmapWithOldStars();

            TimeSpan ts = DateTime.Now - start;

            Debug.Print("Time: {0}", ts);
        }

    }

}