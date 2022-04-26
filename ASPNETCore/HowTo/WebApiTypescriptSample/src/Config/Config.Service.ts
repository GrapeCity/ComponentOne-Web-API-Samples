import { Config } from './Config.Model';
import * as data from '../../assets/config.json';

export class ConfigService {

    private _config: Config = new Config();

    private constructor() {

    }

    private static _instance: ConfigService = null;

    public static Instance(): ConfigService {
        if (ConfigService._instance == null) {
            ConfigService._instance = new ConfigService();
            ConfigService._instance.LoadConfig();
        }
        return ConfigService._instance;
    }

    private LoadConfig(): void {
        this._config = data;
    }

    public get WebApiSericeUrl(): string {
        return this._config.WebApiServiceUrl;
    }
    public set WebApiServiceUrl(value: string) {
        this._config.WebApiServiceUrl = value;
    }


}