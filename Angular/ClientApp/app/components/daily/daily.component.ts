import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'daily',
    templateUrl: './daily.component.html'
})
export class DailyComponent {
    public dailyUsers: Daily[];
    private currentNr: number;

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/Daily/RandomizeParticipants').subscribe(result => {
            this.dailyUsers = result.json() as Daily[];
        });
        this.currentNr = -1;
        this.utcTime();
    }

    public incrementCounter() {
        this.currentNr++;
    }

    utcTime(): void {
        setInterval(() => {
            if (this.currentNr >= 0) {
                this.dailyUsers[this.currentNr].timer++;
            }
        }, 1000);
       
    }
}

interface Daily {
    name: string;
    timer: number;
}
