import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'daily',
    templateUrl: './daily.component.html'
})
export class DailyComponent {
    public dailyUsers: Daily[];

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/Daily/RandomizeParticipants').subscribe(result => {
            this.dailyUsers = result.json() as Daily[];
        });
    }
}

interface Daily {
    name: string;
    time: string;
}
