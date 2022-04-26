namespace WebApiConsoleSample
{
    public class Cmd
    {
        public class Excel{
            public static string SPLIT = "api/excel/{0}/split";
            public static string FIND_TEXT = "api/excel/{0}/find";
            public static string REPLACE_TEXT = "api/excel/{0}/replace";
            public static string ROWS = "api/excel/{0}/rows/{1}";
            public static string COLUMNS = "api/excel/{0}/columns/{1}";
            public static string GENERATE = "api/excel";

        }

        public class BarCode{
            public static string GENERATE = "api/barcode";
        }
        
        public class DataEngine{
            public static string GET_ALL_FIELD = "api/dataengine/{0}/fields";
            public static string ANALYZE_DATA = "api/dataengine/{0}/analyses";
            public static string ANALYZE_RESULT = "api/dataengine/{0}/analyses/{1}/result";
        }
        

    }
}