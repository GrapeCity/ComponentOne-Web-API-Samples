namespace WebApiConsoleSample
{
    public class GenerateExcelFromXMLDto : BaseDto
    {
        /**The exported file name. */
        public string FileName { set; get; }
        /**The Template file name (if not provide, use generated template automatically). */
        public string TemplateFileName { set; get; }
        /**The file format of the excel. */
        public string Type { set; get; }
        /**The xml data file name that storage manager can recognize. The content of the xml data file is collection-like, a root element with multiple same elements as its children */
        public string DataFileName { set; get; }

    }
}