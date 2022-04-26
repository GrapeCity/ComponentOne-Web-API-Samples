namespace WebApiConsoleSample
{
    public class AnalyzeStatusDto : BaseDto
    {
        public AnalyzeStatusDto() { }
        
        /**The data source to analyze. */
        public string DataSource { get; set; }
        
        /**The token of analysis instance. */
        public string Token { get; set; }

    }
}