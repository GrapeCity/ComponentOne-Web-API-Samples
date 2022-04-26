import { BaseController } from '../../Base/Base.Controller';
import { ExcelService } from './Excel.Service';
import { SplitExcelDto } from './Dtos/SplitExcel.dto';
import { FindTextDto } from './Dtos/FindText.dto';
import { ReplaceTextDto } from './Dtos/ReplaceText.dto';
import { GenerateExcelFromJSONDto } from './Dtos/GenerateExcelFromJSON.dto';
import { GenerateExcelFromXMLDto } from './Dtos/GenerateExcelFromXML.dto';
import * as FSExtra from 'fs-extra';
import * as Path from 'path';
export class ExcelController extends BaseController<ExcelService>{

    constructor() {
        super(new ExcelService());
    }

    public async Run(): Promise<any> {
        // await this.SplitExcel();
        // await this.FindText();
        // await this.ReplaceText();
        // await this.GenerateExcelFromJSON();
        await this.GenerateExcelFromXML();
    }

    private async SplitExcel() {
        console.log("\n========= SplitExcel started ===========\n");
        let dto: SplitExcelDto = new SplitExcelDto();
        dto.ExcelPath = "ExcelRoot/FlexGrid.xlsx";
        dto.OutputPath = "ExcelRoot/OutputFiles";
        dto.OutputNames = ["Sheet1.xlsx", "Sheet2.xlsx"];
        let res = await this.service.Split(dto);
        console.log(res.data);
        console.log("\n========= SplitExcel ended =============\n");
    }

    private async FindText() {
        console.log("\n========= FindText started ===========\n");
        let dto: FindTextDto = new FindTextDto();
        dto.ExcelPath = "ExcelRoot/FlexGrid.xlsx";
        dto.Text = "Japan";
        dto.WholeCell = false;
        dto.MatchCase = false;
        dto.SheetName = null;
        let res = await this.service.FindText(dto);
        console.log(res.data);
        console.log("\n========= FindText ended =============\n");
    }

    private async ReplaceText() {
        console.log("\n========= ReplaceText started ===========\n");
        let dto: ReplaceTextDto = new ReplaceTextDto();
        dto.ExcelPath = "ExcelRoot/FlexGrid.xlsx";
        dto.Text = "Japan";
        dto.NewText = "JAPAN";
        dto.WholeCell = false;
        dto.MatchCase = false;
        dto.SheetName = null;
        let res = await this.service.ReplaceText(dto);
        console.log(res.data);
        console.log("\n========= ReplaceText ended =============\n");
    }
    private async GenerateExcelFromJSON() {
        console.log("\n========= GenerateExcelFromJSON started ===========\n");
        let dto: GenerateExcelFromJSONDto = new GenerateExcelFromJSONDto();
        dto.Data = "[{\"id\":0,\"date\":\"2015-06-24T16:00:00.000Z\",\"time\":\"2015-08-18T18:50:00.000Z\",\"country\":\"Japan\",\"countryMapped\":3,\"downloads\":436,\"sales\":4314.523264765739,\"expenses\":4330.620157998055,\"checked\":true},{\"id\":1,\"date\":\"2015-02-28T16:00:00.000Z\",\"time\":\"2015-05-19T21:01:00.000Z\",\"country\":\"Italy\",\"countryMapped\":4,\"downloads\":676,\"sales\":2558.132621925324,\"expenses\":1959.2562899924815,\"checked\":false},{\"id\":2,\"date\":\"2015-07-04T16:00:00.000Z\",\"time\":\"2015-03-20T02:19:00.000Z\",\"country\":\"Germany\",\"countryMapped\":1,\"downloads\":488,\"sales\":6382.134975865483,\"expenses\":4719.182880362496,\"checked\":false},{\"id\":3,\"date\":\"2015-02-08T16:00:00.000Z\",\"time\":\"2015-10-05T08:52:00.000Z\",\"country\":\"Italy\",\"countryMapped\":4,\"downloads\":964,\"sales\":7840.539840981364,\"expenses\":375.3066179342568,\"checked\":false},{\"id\":4,\"date\":\"2015-03-14T16:00:00.000Z\",\"time\":\"2015-10-04T09:20:00.000Z\",\"country\":\"Italy\",\"countryMapped\":4,\"downloads\":706,\"sales\":6771.57775266096,\"expenses\":4210.299032274634,\"checked\":false}]";
        dto.FileName = "excel";
        dto.Type = "xlsx";
        dto.TemplateFileName = "ExcelRoot\\Templates\\JSONDataTemplate.xlsx";
        try {
            let res = await this.service.GenerateExcelFromJSON(dto);
            FSExtra.ensureDirSync(Path.join(process.cwd(), "Output/Excel"));
            let path = Path.join(process.cwd(), "Output/Excel", `${dto.FileName}.${dto.Type}`);
            if (FSExtra.existsSync(path)) {
                let index: number = 1;
                while (FSExtra.existsSync(path)) {
                    index++;
                    path = Path.join(process.cwd(), "Output/Excel", `${dto.FileName}(${index}).${dto.Type}`);
                }
            }
            res.data.pipe(FSExtra.createWriteStream(path));
            console.log("ExcelFile successfully generated at ", path);
        } catch (err) {
            console.log(err);
        }
        console.log("\n========= GenerateExcelFromJSON ended =============\n");
    }

    private async GenerateExcelFromXML() {
        console.log("\n========= GenerateExcelFromXML started ===========\n");
        let dto: GenerateExcelFromXMLDto = new GenerateExcelFromXMLDto();
        dto.DataFileName = "ExcelRoot/10rowsdata.xml";
        dto.FileName = "excel";
        dto.Type = "xlsx";
        dto.TemplateFileName = "ExcelRoot/Templates/XmlDataTemplate.xlsx";
        try {
            let res = await this.service.GenerateExcelFromXML(dto);
            FSExtra.ensureDirSync(Path.join(process.cwd(), "Output/Excel"));
            let path = Path.join(process.cwd(), "Output/Excel", `${dto.FileName}.${dto.Type}`);
            if (FSExtra.existsSync(path)) {
                let index: number = 1;
                while (FSExtra.existsSync(path)) {
                    index++;
                    path = Path.join(process.cwd(), "Output/Excel", `${dto.FileName}(${index}).${dto.Type}`);
                }
            }
            res.data.pipe(FSExtra.createWriteStream(path));
            console.log("ExcelFile successfully generated at ", path);
        } catch (err) {
            console.log(err);
        }
        console.log("\n========= GenerateExcelFromXML ended =============\n");
    }

}