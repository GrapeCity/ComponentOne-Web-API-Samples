export class Cmd {
    static Excel = class {
        public static SPLIT: string = "api/excel/{0}/split";
        public static FIND_TEXT: string = "api/excel/{0}/find";
        public static REPLACE_TEXT: string = "api/excel/{0}/replace";
        public static ROWS: string = "api/excel/{0}/rows/{1}";
        public static COLUMNS: string = "api/excel/{0}/columns/{1}";
        public static GENERATE: string = "api/excel";
    }

    static BarCode = class {
        public static GENERATE: string = "api/barcode";
    }

    static DataEngine = class {
        public static GET_ALL_FIELD: string = "api/dataengine/{0}/fields";
        public static ANALYZE_DATA: string = "api/dataengine/{0}/analyses";
        public static ANALYZE_RESULT: string = "api/dataengine/{0}/analyses/{1}/result";
        public static ANALYZE_STATUS: string = "api/dataengine/{0}/analyses/{1}/status";
    }

}