using System.Drawing;
using System.IO;


namespace AuditQueue.Services
{

    public class DataBar
    {
        public string Location { get; set; }
        public string ViolationDate { get; set; }
        public string ViolationTime { get; set; }
        public string Type { get; set; }
        public string Limit { get; set; }
        public string VehSpeed { get; set; }
    }
    public class DataBarRepo
    {

        const string Databar_Font = "Tahoma";

        string[] Overlay_Headers = {
            "Location",
            "Location Code",
           // "Amber Time",
           // "Lane",
            "Type",
            "Limit",
            "Date",
            "Violation Time",
            //"Red Time",
           // "Elapsed Time",
            "Speed",
            " "
        };



        string[] Overlay_TagImageHeaders =
        {

            "Date",
            "Time",
             "Location",
             "        ",
             "Type",
             "Posted Limit",
             "Speed"
        };

        public Bitmap ResizeAndAddDatabar(Bitmap RawImage, DataBar dataBar, int ResultWidth, int ResultHeight)
        {

            ProportionalDatabarParameters pdp = new ProportionalDatabarParameters(ResultWidth, ResultHeight);

            string[] Data = new string[7];

            Data[0] = dataBar.ViolationDate.Trim();
            Data[1] = dataBar.ViolationTime.Trim();
            Data[2] = dataBar.Location.Trim();
            Data[3] = " ";
            Data[4] = dataBar.Type.Trim();
            Data[5] = dataBar.Limit.Trim(); 
            Data[6] = dataBar.VehSpeed.Trim();
            return DrawBarForTagImage(Data, ResizeImage(RawImage, ResultWidth, ResultHeight), pdp);
        }

        private Bitmap ResizeImage(Bitmap RawImage, int ResultWidth, int ResultHeight)
        {
            Bitmap b = new Bitmap(ResultWidth, ResultHeight);

            Graphics g = Graphics.FromImage(b);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(RawImage, 0, 0, ResultWidth, ResultHeight);

            g.Flush();
            g.Dispose();

            return b;
        }

        public System.Drawing.Bitmap BytesToBitmap(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Bitmap img = (Bitmap)System.Drawing.Image.FromStream(ms);
                return img;

            }
        }

        private Bitmap DrawBarForTagImage(string[] Data, Bitmap RawImage, ProportionalDatabarParameters pdp)
        {
            Bitmap NewImage = new Bitmap(RawImage.Width, (int)(RawImage.Height + pdp.Height));

            //Open image as a graphics object for editing
            Graphics g = Graphics.FromImage(NewImage);

            //Copy in the old image below where the databar will be
            g.DrawImage(RawImage, 0, pdp.Height);

            Rectangle OverlayBackground = new Rectangle(0, 0, RawImage.Width, (int)(pdp.Height + 2));

            g.FillRectangle(Brushes.Black, OverlayBackground);


            //Create a font object for writing to the picture
            Font f = new Font(Databar_Font, pdp.FontSize, FontStyle.Regular, GraphicsUnit.Pixel);


            float CurY = pdp.Top_Margin;
            float CurX = pdp.Side_Margin;

            for (int i = 0; i < Overlay_TagImageHeaders.Length; i++)
            {
                CurY = pdp.Top_Margin;

                g.DrawString(Overlay_TagImageHeaders[i], f, Brushes.White, new PointF(CurX, CurY));

                CurY += pdp.Small_Line_Separation;
                g.DrawString(Data[i], f, Brushes.White, new PointF(CurX, CurY));

                CurY += pdp.Large_Line_Separation;
                //   g.DrawString(Overlay_Headers[i + 5], f, Brushes.White, new PointF(CurX, CurY));

                CurY += pdp.Small_Line_Separation;
                //  g.DrawString(Data[i + 5], f, Brushes.White, new PointF(CurX, CurY));

                //use large space between first and second columns because the location
                //name string can be long and requires more space
                //if (i == 0)

                //CurX += pdp.Column_Large_Separation;

                //else
                //if (i == 0 || i == 5) CurX += pdp.Column_Small_Separation;
                //else CurX += pdp.Column_Small_Separation2;

                if (i == 0)
                {

                    // var v1 = g.MeasureString(Overlay_Headers[6], f).Width;
                    //CurX += pdp.Spanish_Column_Large_Separation;
                    string text = Data[i];
                    // Console.WriteLine("Length is " + text.Length);
                    var v = g.MeasureString(text, f).Width + 20;
                    CurX += v; //g.MeasureString(Overlay_Headers[6], f).Width;
                }
                else
                {
                    CurX += pdp.Column_Small_Separation2;
                }

            }

            //commit changes back to the image
            g.Flush();

            return NewImage;

        }
        private Bitmap DrawBar(string[] Data, Bitmap RawImage, ProportionalDatabarParameters pdp)
        {
            Bitmap NewImage = new Bitmap(RawImage.Width, (int)(RawImage.Height + pdp.Height));

            //Open image as a graphics object for editing
            Graphics g = Graphics.FromImage(NewImage);

            //Copy in the old image below where the databar will be
            g.DrawImage(RawImage, 0, pdp.Height);

            Rectangle OverlayBackground = new Rectangle(0, 0, RawImage.Width, (int)(pdp.Height + 2));

            g.FillRectangle(Brushes.Black, OverlayBackground);


            //Create a font object for writing to the picture
            Font f = new Font(Databar_Font, pdp.FontSize, FontStyle.Regular, GraphicsUnit.Pixel);


            float CurY = pdp.Top_Margin;
            float CurX = pdp.Side_Margin;

            for (int i = 0; i < 5; i++)
            {
                CurY = pdp.Top_Margin;

                // g.DrawString(Overlay_Headers[i], f, Brushes.White, new PointF(CurX, CurY));
                g.DrawString(Overlay_TagImageHeaders[i], f, Brushes.White, new PointF(CurX, CurY));

                CurY += pdp.Small_Line_Separation;
                g.DrawString(Data[i], f, Brushes.White, new PointF(CurX, CurY));

                CurY += pdp.Large_Line_Separation;
                // g.DrawString(Overlay_Headers[i + 5], f, Brushes.White, new PointF(CurX, CurY));
                g.DrawString(Overlay_TagImageHeaders[i + 5], f, Brushes.White, new PointF(CurX, CurY));

                CurY += pdp.Small_Line_Separation;
                g.DrawString(Data[i + 5], f, Brushes.White, new PointF(CurX, CurY));

                //use large space between first and second columns because the location
                //name string can be long and requires more space
                //if (i == 0)

                //CurX += pdp.Column_Large_Separation;

                //else
                //if(i==0 || i==5)CurX += pdp.Column_Small_Separation;
                //else CurX += pdp.Column_Small_Separation2;
                if (i == 0)
                {

                    // var v1 = g.MeasureString(Overlay_Headers[6], f).Width;
                    //CurX += pdp.Spanish_Column_Large_Separation;
                    string text = Data[i];
                    // Console.WriteLine("Length is " + text.Length);
                    var v = g.MeasureString(text, f).Width;
                    CurX += v; //g.MeasureString(Overlay_Headers[6], f).Width;
                }
                else
                {
                    CurX += pdp.Column_Small_Separation2;
                }

            }

            //commit changes back to the image
            g.Flush();

            return NewImage;

        }
    }

    public class ProportionalDatabarParameters
    {

        public float Height;
        public float Top_Margin;
        public float Side_Margin;
        public float Small_Line_Separation;
        public float Large_Line_Separation;
        public float Column_Large_Separation;
        public float Column_Small_Separation;
        public float Column_Small_Separation2;
        public float FontSize;

        public const float Databar_Height = 0.1085F;
        public const float Databar_Top_Margin = 0.0125F;
        public const float Databar_Side_Margin = 0.0231F;
        public const float Databar_Small_Line_Separation = 0.0195F;
        public const float Databar_Large_Line_Separation = 0.0278F;
        public const float Databar_Column_Large_Separation = 0.299999F;
        public const float Databar_Column_Small_Separation = 0.22F;
        public const float Databar_Font_Size = 0.0195F;
        public const float Databar_Column_Small_Separation2 = 0.15F;

        public ProportionalDatabarParameters(int width, int height)
        {

            Height = height * Databar_Height;
            Top_Margin = height * Databar_Top_Margin;
            Side_Margin = width * Databar_Side_Margin;
            Small_Line_Separation = height * Databar_Small_Line_Separation;
            Large_Line_Separation = height * Databar_Large_Line_Separation;
            Column_Large_Separation = width * Databar_Column_Large_Separation;
            Column_Small_Separation = width * Databar_Column_Small_Separation;
            Column_Small_Separation2 = width * Databar_Column_Small_Separation2;

            FontSize = height * Databar_Font_Size;

        }

    }
}
