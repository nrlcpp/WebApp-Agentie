import { Reservation } from "./reservations.models";

export interface PaginatedReservations {
    currentPage: number;
    totalItems: number;
    itemsPerPage: number;
    totalPages: number;
    items: Reservation[];
}
