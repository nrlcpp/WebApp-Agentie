import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-fetch-data',
    templateUrl: './fetch-data.component.html'
})

    export class FetchDataComponent {
    public reservations: Reservation[];

    public name: string = "test";

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
            this.loadReservations();
    }

    loadReservations() {
        this.http.get<Reservation[]>(this.baseUrl + 'api/Reservations').subscribe(result => {
            this.reservations = result;
            console.log(this.reservations);
        }, error => console.error(error));
}

    delete(reservationId: string) {
        if (confirm('Are you sure you want to delete the reservation with id ' + reservationId + '?')) {
            this.http.delete(this.baseUrl + 'api/Reservations/' + reservationId)
                .subscribe
                (
                    result => {
                        alert('Reservation successfully deleted!');
                        this.loadReservations();
                    },
                    error => alert('Cannot delete reservation - maybe it has comments?')
                )
        }
    }

    submit() {

        var reservation: Reservation = <Reservation>{};
        reservation.sum = 2000;
        reservation.AddedOn = new Date();
        reservation.Currency = Currency.EUR;
        reservation.Type = Type.accommodation;
        reservation.DepartureTime = new Date();
        reservation.ArrivalTime = new Date();


        this.http.post(this.baseUrl + 'api/Reservations', reservation).subscribe(result => {
            console.log('success!');
            this.loadReservations();
        },
            error => {
                if (error.status == 400) {
                    console.log(error.error.errors)
                }
            });
    }
    }

        interface Reservation {
            id: number;
            sum: number;
            Location: number;
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

    



//    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

//        http.get<Reservation[]>(baseUrl + 'api/Reservations').subscribe(result => {
//            this.reservations = result;
//            console.log(this.reservations);
//        }, error => console.error(error));
//    }
//}
