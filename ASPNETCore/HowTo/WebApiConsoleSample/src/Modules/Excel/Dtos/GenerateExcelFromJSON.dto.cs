namespace WebApiConsoleSample
{
    public class GenerateExcelFromJSONDto : BaseDto
    {
        /**The exported file name. */
        public string FileName { set; get; }
        /**The Template file name (if not provide, use generated template automatically). */
        public string TemplateFileName { set; get; }
        /**The file format of the excel. */
        public string Type { set; get; }
        /**A data collection which is posted from client side. */
        public string Data { set; get; }

    }
}