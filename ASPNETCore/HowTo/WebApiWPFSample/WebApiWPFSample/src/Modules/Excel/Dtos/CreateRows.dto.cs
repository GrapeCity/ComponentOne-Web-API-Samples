namespace WebApiConsoleSample
{
    public class CreateRowsDto : BaseDto
    {
        /**The excel file name that storage manager can recognize. */
        public string ExcelPath { set; get; }
        
        /**The sheet name. Can be null */
        public string SheetName { set; get; }

        /**The row index. */
        public int RowIndex { set; get; }

        /**The count of rows. */
        public int Count { set; get; }
    }
}