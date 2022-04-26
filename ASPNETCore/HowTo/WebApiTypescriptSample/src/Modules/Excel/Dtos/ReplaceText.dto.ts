import { BaseDto } from "../../../Base/Base.Dto";

export class ReplaceTextDto extends BaseDto {

    /**The excel file name that storage manager can recognize. */
    public ExcelPath: string;

    /**The sheet name (if not provide, replace in all sheets). */
    public SheetName: string;

    /**The text which is replaced. */
    public Text: string;

    /**The new text to replace. */
    public NewText: string;

    /**A boolean value indicates whether to search the value with case sensitive. */
    public MatchCase: boolean;
    
    /**A boolean value indicates whether to search the value with matching a whole word. */
    public WholeCell: boolean;

}