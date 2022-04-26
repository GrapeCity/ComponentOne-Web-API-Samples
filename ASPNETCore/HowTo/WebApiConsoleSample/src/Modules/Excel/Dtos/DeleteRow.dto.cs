namespace WebApiConsoleSample
{
    public class DeleteRowDto : BaseDto
    {
        /**The excel file name that storage manager can recognize. */
        public string ExcelPath { set; get; }

        /**The sheet name.*/
        public string SheetName { set; get; }

        /**the row indexes. format: {num}; {num},{num},...; {num}-{num}. for example: 1,3-5 */
        public string RowIndexs { set; get; }

    }
}