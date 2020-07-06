import { NgModule } from '@angular/core';

import { ReservationsEditComponent } from './reservations-edit/reservations-edit.component';
import { ReservationsListComponent } from './reservations-list/reservations-list.component';
import { Routes, RouterModule } from '@angular/router';
import { ReservationsComponent } from './reservations.component';

const routes: Routes = [
    {
        path: '', component: ReservationsComponent,
        children: [
            { path: '', redirectTo: 'list', pathMatch: 'full' },
            { path: 'list', component: ReservationsListComponent },
            { path: 'edit/:id', component: ReservationsEditComponent },
            { path: 'edit', component: ReservationsEditComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ReservationsRoutingModule {
    static routedComponents = [ReservationsComponent, ReservationsListComponent, ReservationsEditComponent];
}
