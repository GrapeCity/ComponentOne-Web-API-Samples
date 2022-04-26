import { BaseDto } from "../../../Base/Base.Dto";

export class CreateRowsDto extends BaseDto {
    
    /**The excel file name that storage manager can recognize. */
    public ExcelPath: string;

    /**The sheet name. Can be null */
    public SheetName: string;

    /**The row index. */
    public RowIndex: number;

    /**The count of rows. */
    public Count: number;
}