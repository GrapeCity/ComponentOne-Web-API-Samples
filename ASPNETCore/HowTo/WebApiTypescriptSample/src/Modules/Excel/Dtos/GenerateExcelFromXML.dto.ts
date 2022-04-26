import { BaseDto } from "../../../Base/Base.Dto";

export class GenerateExcelFromXMLDto extends BaseDto {
    /**The exported file name. */
    public FileName: string;
    /**The Template file name (if not provide, use generated template automatically). */
    public TemplateFileName: string;
    /**The file format of the excel. */
    public Type: string;
    /**The xml data file name that storage manager can recognize. The content of the xml data file is collection-like, a root element with multiple same elements as its children */
    public DataFileName: string;
}