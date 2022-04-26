import { BaseService } from '../../Base/Base.Service';
import { ConfigService } from '../../Config/Config.Service';
import { Cmd } from '../../Network/Command';
import axios from 'axios';
import { GetAllFieldsDto } from './Dtos/GetAllFields.dto';
import { AnalyzeDataDto } from './Dtos/AnalyzeData.dto';
import { AnalyzeResultDto } from './Dtos/AnalyzeResult.dto';
import { AnalyzeStatusDto } from './Dtos/AnalyzeStatus.dto';

export class DataEngineService extends BaseService {

    /* Gets all the fields in the data. */
    public GetAllFields(dto: GetAllFieldsDto) {
        let url = ConfigService.Instance().WebApiSericeUrl + Cmd.DataEngine.GET_ALL_FIELD.replace("{0}", dto.DataSource);
        return axios.get(url);
    }

    /* Analyze the data from the specified data source. */
    public AnalyzeData(dto: AnalyzeDataDto) {
        let url = ConfigService.Instance().WebApiSericeUrl + Cmd.DataEngine.ANALYZE_DATA.replace("{0}", dto.DataSource);
        return axios({
            method: "post",
            url: url,
            data: JSON.parse(dto.ViewDefinition)
        });
    }

    /* Get the analysis result data. */
    public AnalyzeResult(dto: AnalyzeResultDto) {
        let url = ConfigService.Instance().WebApiSericeUrl + Cmd.DataEngine.ANALYZE_RESULT.replace("{0}", dto.DataSource).replace("{1}", dto.Token);
        return axios.get(url);
    }

    /**Get the status of the analysis. */
    public AnalyzeStatus(dto: AnalyzeStatusDto){
        let url = ConfigService.Instance().WebApiSericeUrl + Cmd.DataEngine.ANALYZE_STATUS.replace("{0}", dto.DataSource).replace("{1}", dto.Token);
        return axios.get(url);
    }
}