import { BaseDto } from "../../../Base/Base.Dto";

export class AnalyzeStatusDto extends BaseDto {

    /**The data source to analyze. */
    public DataSource: string;

    /**The token of analysis instance. */
    public Token: string;

}