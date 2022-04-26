import { BaseDto } from "../../../Base/Base.Dto";

export class DeleteRowDto extends BaseDto {
    
    /**The excel file name that storage manager can recognize. */
    public ExcelPath: string;

    /**The sheet name.*/
    public SheetName: string;

    /**the row indexes. format: {num}; {num},{num},...; {num}-{num}. for example: 1,3-5 */
    public RowIndexs: string;

}