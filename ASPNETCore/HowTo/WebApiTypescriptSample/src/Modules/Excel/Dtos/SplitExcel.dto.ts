import { BaseDto } from '../../../Base/Base.Dto';

export class SplitExcelDto extends BaseDto {

    /**The excel file name that storage manager can recognize. */
    public ExcelPath: string;

    /**The output path in storage(if not provide, the default output path same as source). */
    public OutputPath: string;

    /**The output file names (if not provide, the output file names will be generated automatically). */
    public OutputNames: string[];

}