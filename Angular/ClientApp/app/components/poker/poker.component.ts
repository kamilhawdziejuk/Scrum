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

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/Poker/GetPokerValues').subscribe(result => {
            this.pokerValues = result.json() as PokerValue[];
        });
        this._http = http;
        this.currentNr = -1;
        this._originUrl = originUrl;

        this.utcTime();
    }

    public setCurrentPokerValue(nr: number) {
        this.currentNr = nr;

        let dataToPass = JSON.stringify(this.pokerValues[nr]);
        let headers = new Headers();
        headers.append("Content-Type", "application/json");

        var url = this._originUrl + '/api/Poker/SetPokerValue';
        this._http.post(url, dataToPass, {
            headers: headers
        }).map(response => response.json())
            .subscribe();        
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
        }, 5000);       
    }
}

interface PokerValue {
    name: string;
    points: number;
    editable: boolean;
}
