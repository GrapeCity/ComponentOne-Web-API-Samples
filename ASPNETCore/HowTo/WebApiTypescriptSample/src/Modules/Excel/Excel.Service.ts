import { BaseService } from "../../Base/Base.Service";
import { SplitExcelDto } from "./Dtos/SplitExcel.dto";
import { ConfigService } from '../../Config/Config.Service';
import { Cmd } from '../../Network/Command';
import axios from 'axios';
import { FindTextDto } from "./Dtos/FindText.dto";
import { ReplaceTextDto } from "./Dtos/ReplaceText.dto";
import { GenerateExcelFromJSONDto } from "./Dtos/GenerateExcelFromJSON.dto";
import { GenerateExcelFromXMLDto } from './Dtos/GenerateExcelFromXML.dto';
import * as FormData from 'form-data';
import { HttpParams } from '../../Network/HttpParams';
export class ExcelService extends BaseService {
    constructor() {
        super();
    }

    /**Split an excel file from storage to multiple excel files and save it into storage. */
    public Split(dto: SplitExcelDto) {
        let url = ConfigService.Instance().WebApiSericeUrl + Cmd.Excel.SPLIT.replace("{0}", dto.ExcelPath);
        url += `?outputpath=${dto.OutputPath}`;
        for (let outputname of dto.OutputNames) {
            url += `&outputnames=${outputname}`;
        }
        return axios.get(url);
    }

    /**Find text in excel, return all matches info. */
    public FindText(dto: FindTextDto) {
        let url = ConfigService.Instance().WebApiSericeUrl + Cmd.Excel.FIND_TEXT.replace("{0}", dto.ExcelPath + (dto.SheetName != null ? ("/" + dto.SheetName) : ""));
        url += "?" + new HttpParams()
            .Append("text", dto.Text)
            .Append("wholecell", dto.WholeCell ? "true" : "false")
            .Append("matchcase", dto.MatchCase ? "true" : "false")
            .ToString();
        return axios.get(url);
    }

    /**Replace text in excel. */
    public ReplaceText(dto: ReplaceTextDto) {
        let url = ConfigService.Instance().WebApiSericeUrl + Cmd.Excel.REPLACE_TEXT.replace("{0}", dto.ExcelPath + (dto.SheetName != null ? ("/" + dto.SheetName) : ""));
        url += "?" + new HttpParams()
            .Append("text", dto.Text)
            .Append("newtext", dto.NewText)
            .Append("wholecell", dto.WholeCell ? "true" : "false")
            .Append("matchcase", dto.MatchCase ? "true" : "false")
            .ToString();
        return axios.get(url);
    }

    public GenerateExcelFromJSON(dto: GenerateExcelFromJSONDto) {
        let url = ConfigService.Instance().WebApiSericeUrl + Cmd.Excel.GENERATE;
        let body = new FormData();
        body.append('FileName', dto.FileName);
        body.append('TemplateFileName', dto.TemplateFileName);
        body.append('type', dto.Type);
        body.append('data', dto.Data);
        return axios({
            method: "post",
            url: url,
            data: body,
            responseType: "stream",
            headers: body.getHeaders()
        });
    }
    public GenerateExcelFromXML(dto: GenerateExcelFromXMLDto) {
        let url = ConfigService.Instance().WebApiSericeUrl + Cmd.Excel.GENERATE;
        url += "?" + new HttpParams()
            .Append("FileName", dto.FileName)
            .Append("type", dto.Type)
            .Append("TemplateFileName", dto.TemplateFileName)
            .Append("DataFileName", dto.DataFileName)
            .ToString();
        return axios({
            method: "get",
            url: url,
            responseType: "stream"
        });
    }

}
