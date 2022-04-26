namespace WebApiConsoleSample
{
    public class CreateColumnsDto : BaseDto
    {
        /**The excel file name that storage manager can recognize. */
        public string ExcelPath { set; get; }
        /**The sheet name. */
        public string SheetName { set; get; }
        /**The column index. */
        public int ColumnIndex { set; get; }
        /**The count of columns. */
        public int Count { set; get; }
    }
}