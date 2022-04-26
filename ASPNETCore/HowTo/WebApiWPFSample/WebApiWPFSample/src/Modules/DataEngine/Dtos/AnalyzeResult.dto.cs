namespace WebApiConsoleSample
{
    public class AnalyzeResultDto : BaseDto
    {
        public AnalyzeResultDto() { }
        
        /**The data source to analyze. */
        public string DataSource { get; set; }
        
        /**The token of analysis instance. */
        public string Token { get; set; }

    }
}