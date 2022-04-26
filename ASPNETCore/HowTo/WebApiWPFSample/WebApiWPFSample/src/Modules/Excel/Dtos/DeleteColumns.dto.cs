namespace WebApiConsoleSample
{
    public class DeleteColumnsDto : BaseDto
    {
        /**The excel file name that storage manager can recognize. */
        public string ExcelPath { set; get; }

        /**The sheet name.*/
        public string SheetName { set; get; }

        /**The column indexes. format: {num}; {num},{num},...; {num}-{num}. for example: 1,3-5 */
        public string ColumnIndexs { set; get; }

    }
}