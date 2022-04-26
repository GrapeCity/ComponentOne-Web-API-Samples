import { BaseDto } from "../../../Base/Base.Dto";

export class BarCodeImageType {
    public static PNG: string = "png";
    public static JPEG: string = "jpeg";
    public static BMP: string = "bmp";
    public static GIF: string = "gif";
    public static TIFF: string = "Tiff";
}

export class CaptionAlignment {
    public static LEADING: string = "Leading";
    public static TRAILING: string = "Trailing";
    public static CENTER: string = "Center";
    public static JUSTIFIED: string = "Justified";
    public static DISTRIBUTED: string = "Distributed";
}

export class CaptionPosition {
    public static NONE: string = "none";
    public static ABOVE: string = "above";
    public static BELOW: string = "below";
}

export class GenerateBarCodeDto extends BaseDto {

    public constructor() {
        super();
    }

    /**The file type of the barcode image. */
    public Type: string;

    /**The value that is encoded as a barcode image. */
    public Text: string;

    /**The type of encoding to use when generating the barcode image. */
    public CodeType: string;
    /**Gets or sets the background color for the barcode image. */
    public BackColor: string;

    /**Gets or sets the foreground color for the barcode image. */
    public ForeColor: string;

    /**Barcode caption position relative to the barcode symbol. */
    public CaptionPosition: string;

    /**Barcode caption alignment relative to the barcode. */
    public CaptionAlignment: string;

    /**Gets or sets a value indicating whether a checksum of the barcode will be computed and included in the barcode when applicable. */
    public CheckSumEnabled: boolean;

}