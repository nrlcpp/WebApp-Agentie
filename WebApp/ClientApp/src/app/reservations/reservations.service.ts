import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ApplicationService } from '../core/services/application.service';
import { PageEvent } from '@angular/material';
import { Reservation } from './reservations.models';

@Injectable({
  providedIn: 'root'
})
export class ReservationsService {

    constructor(
        private http: HttpClient,
        private applicationService: ApplicationService) { }

    getReservation(id: number) {
        return this.http.get<Reservation>(`${this.applicationService.baseUrl}api/Reservations/${id}`);
    }

    listReservations(event?: PageEvent) {

        let pageIndex = event ? event.pageIndex + "" : "0";
        let itemsPerPage = event ? event.pageSize + "" : "25";
        console.log(event);
        let params = new HttpParams().set("page", pageIndex).set("itemsPerPage", itemsPerPage); //Create new HttpParams
        //return this.http.get<PaginatedReservations>(`${this.applicationService.baseUrl}api/Reservations`, { params: params });
    }

    saveReservation(reservation: Reservation) {
        return this.http.post(`${this.applicationService.baseUrl}api/Reservations`, reservation);
    }

    modifyReservation(reservation: Reservation) {
        return this.http.put(`${this.applicationService.baseUrl}api/Reservations/${reservation.Id}`, reservation);
    }

    deleteReservation(id: number) {
        return this.http.delete<any>(`${this.applicationService.baseUrl}api/Reservations/${id}`);
    }
}
