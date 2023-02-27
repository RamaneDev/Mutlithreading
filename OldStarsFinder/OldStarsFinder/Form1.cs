using System.Drawing.Imaging;
using static System.Windows.Forms.AxHost;

namespace OldStarsFinder
{
    public partial class Form1 : Form
    {
        // The number of processors or cores available in the computer for this application
        private int priProcessorCount = Environment.ProcessorCount;

        // The bitmaps list
        private List<Bitmap> prloBitmapList;

        // The long list with the old stars count
        private List<long> prliOldStarsCount;

        // The threads list
        private List<Thread> prloThreadList;

        // The original huge infrared bitmap portrait
        Bitmap proOriginalBitmap;

        // The AutoResetEvent instances array
        private AutoResetEvent[] praoAutoResetEventArray;

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


        private void ThreadOldStarsFinder(object poThreadParameter)
        {
            // Retrieve the thread number received in object poThreadParameter
            int liThreadNumber = (int)poThreadParameter;

            // The pixel matrix (bitmap) row number (Y)
            int liRow;

            // The pixel matrix (bitmap) col number (X)
            int liCol;

            // The pixel color
            Color loPixelColor;
            // Get my bitmap part from the bitmap list
            Bitmap loBitmap = prloBitmapList[liThreadNumber];
            // Reset my old stars counter
            prliOldStarsCount[liThreadNumber] = 0;
            // Iterate through each pixel matrix (bitmap) row
            for (liRow = 0; liRow < loBitmap.Height; liRow++)
            {
                // Iterate through each pixel matrix (bitmap) cols
                for (liCol = 0; liCol < loBitmap.Width; liCol++)
                {
                    // Get the pixel color for liCol and liRow
                    loPixelColor = loBitmap.GetPixel(liCol, liRow);
                    if (IsOldStar(loPixelColor))
                    {
                        // The color range corresponds to an old star
                        // Change its color to a pure blue
                        loBitmap.SetPixel(liCol, liRow, Color.Blue);
                        // Increase the old stars counter
                        prliOldStarsCount[liThreadNumber]++;
                    }
                }
            }

            // The thread finished its work. Signal that the work 
            // item has finished.
            praoAutoResetEventArray[liThreadNumber].Set();

        }


        private void WaitForThreadsToDie()
        {
            // Just wait for the threads to signal that every work 
            // item has finished
            WaitHandle.WaitAll(praoAutoResetEventArray);
        }

        private void ShowBitmapWithOldStars()
        {
          
            Bitmap loBitmap;
            // The starting row in each iteration
            int liStartRow = 0;
            // Calculate each bitmap's height
            int liEachBitmapHeight = ((int)(proOriginalBitmap.Height / priProcessorCount)) + 1;
            // Create a new bitmap with the whole width and 
            // height
            loBitmap = new Bitmap(proOriginalBitmap.Width, proOriginalBitmap.Height);

            Graphics g = Graphics.FromImage((Image)loBitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.
            InterpolationMode.HighQualityBicubic;
            for (int liThreadNumber = 0; liThreadNumber < priProcessorCount; liThreadNumber++)
            {
                // Draw each portion in its corresponding 
                // absolute starting row
                g.DrawImage(prloBitmapList[liThreadNumber], 0, liStartRow);
                // Increase the starting row
                liStartRow += liEachBitmapHeight;
            }
            // Show the bitmap in the PictureBox picStarsBitmap
            picStarsBitmap.Image = loBitmap;
            g.Dispose();
        }

        private void butFindOldStars_Click(object sender, EventArgs e)
        {

            // Create the AutoResetEvent array with the number of 
            // cores available
            praoAutoResetEventArray = new AutoResetEvent[priProcessorCount];


            proOriginalBitmap = new Bitmap(picStarsBitmap.Image);
      
            // Create the thread list; the long list and the bitmap list
            prloThreadList = new List<Thread>(priProcessorCount);
            prliOldStarsCount = new List<long>(priProcessorCount);
            prloBitmapList = new List<Bitmap>(priProcessorCount);
            int liStartRow = 0;
            int liEachBitmapHeight = ((int)(proOriginalBitmap.Height / priProcessorCount)) + 1;
            int liHeightToAdd = proOriginalBitmap.Height;
            Bitmap loBitmap;
            // Initialize the threads
            for (int liThreadNumber = 0; liThreadNumber < priProcessorCount; liThreadNumber++)
            {
                // Just to occupy the number
                prliOldStarsCount.Add(0);
                if (liEachBitmapHeight > liHeightToAdd)
                {
                    // The last bitmap height perhaps is less than the other bitmaps height
                    liEachBitmapHeight = liHeightToAdd;
                }
                loBitmap = CropBitmap(proOriginalBitmap, new Rectangle(0, liStartRow, proOriginalBitmap.Width, liEachBitmapHeight));
                liHeightToAdd -= liEachBitmapHeight;
                liStartRow += liEachBitmapHeight;
                prloBitmapList.Add(loBitmap);

                // Create a new AutoResetEvent instance for that thread with 
                // its initial state set to false
                praoAutoResetEventArray[liThreadNumber] = new AutoResetEvent(false);

                // Add the new thread, with a parameterized start 
                // (to allow parameters)
                prloThreadList.Add(new Thread(new ParameterizedThreadStart(ThreadOldStarsFinder)));

            }
            // Now, start the threads
            for (int liThreadNumber = 0; liThreadNumber < priProcessorCount; liThreadNumber++)
            {
                prloThreadList[liThreadNumber].Start(liThreadNumber);
            }
            WaitForThreadsToDie();
            ShowBitmapWithOldStars();
        }
    }

}