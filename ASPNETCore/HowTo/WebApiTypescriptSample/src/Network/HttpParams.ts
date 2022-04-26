import * as QueryString from 'querystring';
export class HttpParams {

    private Params: Map<string, string> = new Map<string, string>();

    public HttpParams() {
    }

    public Set(key: string, value: string): HttpParams {
        if (key == null || key.length == 0) return this;
        if (this.Params.has(key)) {
            this.Params.set(key, QueryString.escape(value));
        }
        else {
            this.Params.set(key, QueryString.escape(value));
        }
        return this;
    }
    public Append(key: string, value: string): HttpParams {
        if (key == null || key.length == 0) return this;
        if (this.Params.has(key)) {
            this.Params.set(key, this.Params.get(key) + `&${key}=${QueryString.escape(value)}`);
        }
        else {
            this.Params.set(key, QueryString.escape(value));
        }
        return this;
    }

    public ToString(): string {
        let prms: string = "";
        this.Params.forEach((value: string, key: string) => {
            prms += `${prms.length == 0 ? "" : "&"}${key}=${value}`;
        });
        return prms;
    }

}