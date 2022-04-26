import { BaseDto } from "../../../Base/Base.Dto";

export class DeleteColumnsDto extends BaseDto {
    
    /**The excel file name that storage manager can recognize. */
    public ExcelPath: string;

    /**The sheet name.*/
    public SheetName: string;

    /**The column indexes. format: {num}; {num},{num},...; {num}-{num}. for example: 1,3-5 */
    public ColumnIndexs: string;

}