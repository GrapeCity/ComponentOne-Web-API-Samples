import { ExcelController } from './Modules/Excel/Excel.Controller';
import { BarCodeController } from './Modules/BarCode/BarCode.Controller';
import { DataEngineController } from './Modules/DataEngine/DataEngine.Controller';

let excelController : ExcelController = new ExcelController();
excelController.Run();

// let barCodeController : BarCodeController = new BarCodeController();
// barCodeController.Run();

// let dataEngineController : DataEngineController = new DataEngineController();
// dataEngineController.Run();