namespace WebApiConsoleSample
{
    public class BarCodeImageType{
        public static string PNG  = "png";
        public static string JPEG  = "jpeg";
        public static string BMP  = "bmp";
        public static string GIF  = "gif";
        public static string TIFF  = "Tiff";
    }

    public class CaptionAlignment
    {
        public static string LEADING = "Leading";
        public static string TRAILING = "Trailing";
        public static string CENTER = "Center";
        public static string JUSTIFIED = "Justified";
        public static string DISTRIBUTED = "Distributed";
        
    }

    public class CaptionPosition
    {
        public static string NONE = "none";
        public static string ABOVE = "above";
        public static string BELOW = "below";
    }
    public class GenerateBarCodeDto : BaseDto
    {
        public GenerateBarCodeDto() { }
        /**The file type of the barcode image. */
        public string Type { get; set; }

        /**The value that is encoded as a barcode image. */
        public string Text { get; set; }

        /**The type of encoding to use when generating the barcode image. */
        public string CodeType { get; set; }
        /**Gets or sets the background color for the barcode image. */
        public string BackColor { get; set; }

        /**Gets or sets the foreground color for the barcode image. */
        public string ForeColor { get; set; }

        /**Barcode caption position relative to the barcode symbol. */
        public string CaptionPosition { get; set; }

        /**Barcode caption alignment relative to the barcode. */
        public string CaptionAlignment { get; set; }

        /**Gets or sets a value indicating whether a checksum of the barcode will be computed and included in the barcode when applicable. */
        public bool CheckSumEnabled { get; set; }



    }
}