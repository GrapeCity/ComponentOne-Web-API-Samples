import { BaseService } from '../../Base/Base.Service';
import { ConfigService } from '../../Config/Config.Service';
import { Cmd } from '../../Network/Command';
import { GenerateBarCodeDto } from './Dtos/GenerateBarcode.dto';
import axios from 'axios';
import { HttpParams } from '../../Network/HttpParams';

export class BarCodeService extends BaseService {

    public GenerateBarCode(dto: GenerateBarCodeDto) {
        let url = ConfigService.Instance().WebApiSericeUrl + Cmd.BarCode.GENERATE;
        url += "?" + new HttpParams()
            .Append("type", dto.Type)
            .Append("text", dto.Text)
            .Append("BackColor", dto.BackColor)
            .Append("ForeColor", dto.ForeColor)
            .Append("CodeType", dto.CodeType)
            .Append("CaptionAlignment", dto.CaptionAlignment)
            .Append("CaptionPosition", dto.CaptionPosition)
            .Append("CheckSumEnabled", dto.CheckSumEnabled ? "true" : "false")
            .ToString();
        return axios({
            method: "get",
            url: url,
            responseType: "stream"
        });
    }
}