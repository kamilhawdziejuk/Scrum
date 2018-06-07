import { Component, Inject } from '@angular/core';
import { Http, Response, Headers, RequestOptions, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';

// Observable class extensions
import 'rxjs/add/observable/of';
import 'rxjs/add/observable/throw';

// Observable operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'poker',
    templateUrl: './poker.component.html'
})
export class PokerComponent {
    public pokerValues: PokerValue[];
    public randomPokerValues: PokerValue[];
    private currentNr: number;
    private _http: Http;
    private _originUrl: string;
    headers: Headers;
    options: RequestOptions;

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/Poker/GetPokerValues').subscribe(result => {
            this.pokerValues = result.json() as PokerValue[];
            this.randomPokerValues = result.json() as PokerValue[];
        });
        this._http = http;
        this.currentNr = -1;
        this._originUrl = originUrl;


        this.headers = new Headers({
            'Content-Type': 'application/json',
            'Accept': 'q=0.8;application/json;q=0.9'
        });
        this.options = new RequestOptions({ headers: this.headers });
        this.utcTime();
    }

    public setCurrentPokerValue(nr: number) {
        this.currentNr = nr;

        //let currentPokerValue = this.pokerValues[nr];
        //for (var i = 0, n = this.pokerValues.length; i < n; i++) {
        //    this.pokerValues[i].editable = false;
        //}
        //this.pokerValues[nr].editable = true;
        //var data = { id = "4" };
        //let data = JSON.stringify(currentPokerValue);
        var data = { id: 4 };
        //this.options = new RequestOptions({ headers: this.headers, params: data });
        //var url = this._originUrl + '/api/Poker/SetPokerValue';
       // this._http.get(this._originUrl + '/api/Poker/GetPokerValues');
        //this._http.get(url, this.options);//, this.options);
    }   

    utcTime(): void {
        setInterval(() => {
            if (this.currentNr >= 0) {

                for (var i = 0; i < this.pokerValues.length; i++) {
                    if (i !== this.currentNr) {
                        this.pokerValues[i].points = Math.floor(Math.random() * (21 - 0 + 1)) + 1;
                    }
                }
            }
        }, 1000);       
    }

}

interface PokerValue {
    name: string;
    points: number;
    editable: boolean;
}
