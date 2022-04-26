import { BaseDto } from "../../../Base/Base.Dto";

export class FindTextDto extends BaseDto {

    /**The excel file name that storage manager can recognize. */
    public ExcelPath: string;

    /**The sheet name (if not provide, find in all sheets). */
    public SheetName: string;

    /**The value which is used to search. */
    public Text: string;

    /**A boolean value indicates whether to search the value with case sensitive. */
    public MatchCase: boolean;

    /**A boolean value indicates whether to search the value with matching the whole cell. */
    public WholeCell: boolean;

}