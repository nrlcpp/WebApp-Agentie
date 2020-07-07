import { Component, OnInit } from '@angular/core';

import { Reservation } from '../reservations.models';
import { ReservationsService } from '../reservations.service';
import { PaginatedReservations } from '../paginatedReservations.models';
import { PageEvent } from '@angular/material';

@Component({
    selector: 'app-reservations-list',
    templateUrl: './reservations-list.component.html',
    styleUrls: ['./reservations-list.component.css']
})

export class ReservationsListComponent implements OnInit {

    public displayedColumns: string[] = ['id', 'sum', 'currency', 'numberOfRemarks', 'action'];
    public reservations: PaginatedReservations;
    public pageEvent: PageEvent;



    constructor(private reservationsService: ReservationsService) {
    }

    ngOnInit() {
        this.loadReservations(null);
    }

    loadReservations(event?: PageEvent) {
        this.reservations = null;

        this.reservationsService.listReservations(event).subscribe(res => {
            this.reservations = res;
        })
    }

    deleteReservation(reservation: Reservation) {
        this.reservationsService.deleteReservation(reservation.Id).subscribe(x => {
            this.loadReservations
        })

    }
}
