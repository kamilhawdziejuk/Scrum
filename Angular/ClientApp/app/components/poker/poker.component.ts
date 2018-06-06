import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'poker',
    templateUrl: './poker.component.html'
})
export class DailyComponent {
    public pokerValues: PokerValue[];
    private currentNr: number;

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/Poker/RandomizeParticipants').subscribe(result => {
            this.pokerValues = result.json() as PokerValue[];
        });
        this.currentNr = -1;
        this.utcTime();
    }

    public setCurrentUserNr(nr : number) {
        this.currentNr = nr;
    }

    utcTime(): void {
        setInterval(() => {
            if (this.currentNr >= 0) {
                this.pokerValues[this.currentNr].timer++;
            }
        }, 1000);
       
    }
}

interface PokerValue {
    name: string;
    points: number;
}
