import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.css']
})
export class ReservationDetailsComponent implements OnInit {
    private reservation: ReservationWithDetails;

    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private route: ActivatedRoute) {
    }

    loadReservation(reservationId: string) {
        this.http.get<ReservationWithDetails>(this.baseUrl + 'api/Reservations/' + reservationId).subscribe(result => {
            this.reservation = result;
            console.log(this.reservation);
        }, error => console.error(error));
    }

    ngOnInit() {
        this.route.paramMap.subscribe(params => {
            this.loadReservation(params.get('reservationId'));
        });
    }
  }

interface ReservationWithDetails {
    id: number;
    sum: number;
    Location: string;
    AddedOn: Date;
    Currency: Currency;
    Type: Type;
    DepartureTime: Date;
    ArrivalTime: Date;
    Documents: boolean;

}
enum Type {
    circuit = 0,
    stay = 1,
    accommodation = 2,
    transport = 3,
    others = 4
}

enum Currency {
    EUR = 0,
    RON = 1,
    USD = 2
}

interface Remarks {
    id: number;
    agent: string;
    content: string;
    importance: Importance;
}

enum Importance {
    hight = 0,
    medium = 1,
    lower = 2
}
