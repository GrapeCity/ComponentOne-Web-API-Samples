namespace WebApiConsoleSample
{
    public class AnalyzeDataDto : BaseDto
    {
        public AnalyzeDataDto() { }
        
        /**The data source to get the fields information. */
        public string DataSource { get; set; }
        
        /**The view definition to analyze. For example:
            {
            fields:[
            {"binding":"Active","dataType":3},
            {"binding":"Country","dataType":1},
            {"binding":"Date","dataType":4},
            {"binding":"Discount","dataType":2},
            {"binding":"Downloads","dataType":2},
            {"binding":"ID","dataType":2},
            {"binding":"Product","dataType":1},
            {"binding":"Sales","dataType":2}
            ],
            rowFields:{items:["Product"]},
            columnFields:{items:["Country"]},
            valueFields:{items:["Sales"]}
            }
        */
        public string ViewDefinition { get; set; }


    }
}