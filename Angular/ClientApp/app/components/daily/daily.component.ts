import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'daily',
    templateUrl: './daily.component.html'
})
export class DailyComponent {
    public dailyUsers: Daily[];
    public timer: number;

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/Daily/RandomizeParticipants').subscribe(result => {
            this.dailyUsers = result.json() as Daily[];
        });
        this.timer = 1;
        this.utcTime();
    }

    utcTime(): void {
        setInterval(() => {
            this.timer = this.timer+1;
        }, 1000);
       
    }
}

interface Daily {
    name: string;
    time: string;
}
