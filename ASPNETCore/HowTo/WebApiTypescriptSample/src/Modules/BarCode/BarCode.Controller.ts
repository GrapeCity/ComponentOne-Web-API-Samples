import { BaseController } from '../../Base/Base.Controller';
import * as FSExtra from 'fs-extra';
import * as Path from 'path';
import { BarCodeService } from './BarCode.Service';
import { GenerateBarCodeDto, BarCodeImageType, CaptionAlignment, CaptionPosition } from './Dtos/GenerateBarcode.dto';
import { AxiosResponse } from 'axios';
export class BarCodeController extends BaseController<BarCodeService>{

    constructor() {
        super(new BarCodeService());
    }

    public async Run(): Promise<any> {
        await this.GenerateBarCode();
    }
    private GetFileNameFromAxiosResponse(response: AxiosResponse<any>) : string{
        let filename = "";
        let disposition = response && response.headers ? response.headers['content-disposition'] : "";
        if (disposition && disposition.indexOf('attachment') !== -1) {
            let filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
            let matches = filenameRegex.exec(disposition);
            if (matches != null && matches[1]) {
                filename = matches[1].replace(/['"]/g, '');
            }
        }
        return filename ? filename : "barcode.png";
    }

    private async GenerateBarCode() {
        console.log("\n========= GenerateBarCode started ===========\n");
        let dto: GenerateBarCodeDto = new GenerateBarCodeDto();
        dto.Type = BarCodeImageType.PNG;
        dto.Text = "1234567890";
        dto.CodeType = "Code39";
        dto.BackColor = "White";
        dto.ForeColor = "Black";
        dto.CaptionAlignment = CaptionAlignment.CENTER;
        dto.CaptionPosition = CaptionPosition.BELOW;
        dto.CheckSumEnabled = false;

        try {
            let res = await this.service.GenerateBarCode(dto);
            let filename = this.GetFileNameFromAxiosResponse(res);
            FSExtra.ensureDirSync(Path.join(process.cwd(), "Output/BarCode"));
            let path = Path.join(process.cwd(), "Output/BarCode", filename);
            res.data.pipe(FSExtra.createWriteStream(path));
            console.log("BarCode successfully generated at ", path);
        } catch (err) {
            console.log(err);
        }
        console.log("\n========= GenerateBarCode ended =============\n");
    }

}