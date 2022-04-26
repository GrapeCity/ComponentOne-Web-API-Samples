import { BaseDto } from "../../../Base/Base.Dto";

export class CreateColumnsDto extends BaseDto {

    /**The excel file name that storage manager can recognize. */
    public ExcelPath: string;

    /**The sheet name. */
    public SheetName: string;

    /**The column index. */
    public ColumnIndex: number;
    
    /**The count of columns. */
    public Count: number;
}