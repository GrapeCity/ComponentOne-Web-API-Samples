namespace WebApiConsoleSample
{
    public class SplitExcelDto : BaseDto
    {
 
        /**The excel file name that storage manager can recognize. */
        public string ExcelPath { set; get; }
 
        /**The output path in storage(if not provide, the default output path same as source). */
        public string OutputPath { set; get; }
 
        /**The output file names (if not provide, the output file names will be generated automatically). */
        public string[] OutputNames { set; get; }
 
    }
}