namespace WebApiConsoleSample
{
    public class FindTextDto : BaseDto
    {
        /**The excel file name that storage manager can recognize. */
        public string ExcelPath { set; get; }
        /**The sheet name (if not provide, find in all sheets). */
        public string SheetName { set; get; }
        /**The value which is used to search. */
        public string Text { set; get; }
        /**A boolean value indicates whether to search the value with case sensitive. */
        public bool MatchCase { set; get; }
        /**A boolean value indicates whether to search the value with matching the whole cell. */
        public bool WholeCell { set; get; }
        
    }
}