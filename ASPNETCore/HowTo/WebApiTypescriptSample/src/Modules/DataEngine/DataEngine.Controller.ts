import { BaseController } from '../../Base/Base.Controller';
import * as FSExtra from 'fs-extra';
import * as Path from 'path';
import { DataEngineService } from './DataEngine.Service';
import { GetAllFieldsDto } from './Dtos/GetAllFields.dto';
import { AnalyzeDataDto } from './Dtos/AnalyzeData.dto';
import { AnalyzeResultDto } from './Dtos/AnalyzeResult.dto';
import { AnalyzeStatusDto } from './Dtos/AnalyzeStatus.dto';
export class DataEngineController extends BaseController<DataEngineService>{

    constructor() {
        super(new DataEngineService());
    }

    public async Run(): Promise<any> {
        // await this.GetAllFields();
        this.AnalyzeData();
    }


    private async GetAllFields() {
        console.log("\n========= GetAllFields started ===========\n");
        let dto: GetAllFieldsDto = new GetAllFieldsDto();
        dto.DataSource = "complex10";
        let res = await this.service.GetAllFields(dto);
        console.log(res);
        console.log("\n========= GetAllFields ended =============\n");
    }

    private async AnalyzeData() {
        console.log("\n========= AnalyzeData started ===========\n");
        let dto: AnalyzeDataDto = new AnalyzeDataDto();
        dto.DataSource = "complex10";
        dto.ViewDefinition = JSON.stringify({
            view: "{\"showZeros\":false,\"showColumnTotals\":0,\"showRowTotals\":0,\"defaultFilterType\":1,\"totalsBeforeData\":false,\"sortableGroups\":true,\"fields\":[{\"binding\":\"Active\",\"header\":\"Active\",\"dataType\":3,\"aggregate\":2,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Active\"},{\"binding\":\"Country\",\"header\":\"Country\",\"dataType\":1,\"aggregate\":2,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Country\"},{\"binding\":\"Date\",\"header\":\"Date\",\"dataType\":4,\"aggregate\":2,\"showAs\":0,\"descending\":false,\"format\":\"d\",\"isContentHtml\":false,\"key\":\"Date\"},{\"binding\":\"Discount\",\"header\":\"Discount\",\"dataType\":2,\"aggregate\":1,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Discount\"},{\"binding\":\"Downloads\",\"header\":\"Downloads\",\"dataType\":2,\"aggregate\":1,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Downloads\"},{\"binding\":\"ID\",\"header\":\"ID\",\"dataType\":2,\"aggregate\":1,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"ID\"},{\"binding\":\"Product\",\"header\":\"Product\",\"dataType\":1,\"aggregate\":2,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Product\"},{\"binding\":\"Sales\",\"header\":\"Sales\",\"dataType\":2,\"aggregate\":1,\"showAs\":0,\"descending\":false,\"format\":\"n0\",\"isContentHtml\":false,\"key\":\"Sales\"}],\"rowFields\":{\"items\":[\"Product\",\"Country\"]},\"columnFields\":{\"items\":[]},\"filterFields\":{\"items\":[]},\"valueFields\":{\"items\":[\"Sales\",\"Downloads\"]}}"
        });
        let res = await this.service.AnalyzeData(dto);
        console.log("AnalyzeData getting response: ", res.data);
        console.log("\n========= AnalyzeData ended =============\n");
        
        let IsCompleted: boolean = false;
        while (!IsCompleted)
        {
            await this.delay(1000);
            let analysisStatus = await this.AnalyzeStatus(res.data);
            let responseObj = analysisStatus.data;
            try{
                
                 if(responseObj){
                     if(responseObj.hasOwnProperty("executingStatus") && responseObj['executingStatus'] == "Completed"){
                         IsCompleted = true;
                     }
                 }
            }catch(e){    
                IsCompleted = true;
            }
        }
        this.AnalyzeResult(res.data);
    }
    async delay(ms: number) {
        await new Promise(resolve => setTimeout(() => resolve(), ms)).then(() => { });
    }

    private async AnalyzeResult(res: any) {
        try {
            console.log("\n========= AnalyzeResult started ===========\n");
            let dto: AnalyzeResultDto = new AnalyzeResultDto();
            dto.DataSource = "complex10";
            dto.Token = res["token"];
            var result = await this.service.AnalyzeResult(dto);
            console.log("Analyzed with result ", result.data);
            console.log("\n========= AnalyzeResult ended =============\n");
        }
        catch (error) {
            console.log(error);
            return Promise.resolve(null);
        }
    }

    private async AnalyzeStatus(res: any) {
        try {

            console.log("\n========= AnalyzeStatus started ===========\n");
            let dto: AnalyzeStatusDto = new AnalyzeStatusDto();
            dto.DataSource = "complex10";
            dto.Token = res["token"];
            var result = await this.service.AnalyzeStatus(dto);
            console.log("AnalyzeData getting response ", result.data);
            console.log("\n========= AnalyzeStatus ended =============\n");
            return result;
        }
        catch (e) {
            return null;
        }
    }
}