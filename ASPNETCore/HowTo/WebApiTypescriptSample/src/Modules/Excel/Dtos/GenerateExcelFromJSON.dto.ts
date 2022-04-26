import { BaseDto } from "../../../Base/Base.Dto";

export class GenerateExcelFromJSONDto extends BaseDto {

    /**The exported file name. */
    public FileName: string;

    /**The Template file name (if not provide, use generated template automatically). */
    public TemplateFileName: string;

    /**The file format of the excel. */
    public Type: string;

    /**A data collection which is posted from client side. */
    public Data: string;
    
}