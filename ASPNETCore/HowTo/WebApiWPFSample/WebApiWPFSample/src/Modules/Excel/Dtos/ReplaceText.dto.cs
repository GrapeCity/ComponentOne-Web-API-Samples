namespace WebApiConsoleSample
{
    public class ReplaceTextDto : BaseDto
    {
        /**The excel file name that storage manager can recognize. */
        public string ExcelPath { set; get; }

        /**The sheet name (if not provide, replace in all sheets). */
        public string SheetName { set; get; }

        /**The text which is replaced. */
        public string Text { set; get; }

        /**The new text to replace. */
        public string NewText { set; get; }

        /**A boolean value indicates whether to search the value with case sensitive. */
        public bool MatchCase { set; get; }
        
        /**A boolean value indicates whether to search the value with matching a whole word. */
        public bool WholeCell { set; get; }

    }
}