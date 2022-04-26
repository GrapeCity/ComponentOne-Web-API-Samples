import { BaseService } from './Base.Service';
export abstract class BaseController<S extends BaseService>{
    
    public service: S;
    
    constructor(s: S) {
        this.service = s;
    }
    
    public async abstract Run() : Promise<any>;

}