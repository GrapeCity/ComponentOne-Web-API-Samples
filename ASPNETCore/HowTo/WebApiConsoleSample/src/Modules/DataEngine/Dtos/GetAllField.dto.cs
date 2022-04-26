namespace WebApiConsoleSample
{
    public class GetAllFieldDto : BaseDto
    {
        public GetAllFieldDto() { }
        
        /**The data source to get the fields information. */
        public string DataSource { get; set; }
    }
}