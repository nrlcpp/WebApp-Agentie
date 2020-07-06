import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReservationsRoutingModule } from './reservations - routing.module';
import { AngularMaterialModule } from '../shared/angular-material.module';
import { CoreModule } from '../core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ReservationsService } from './reservations.service';



@NgModule({
  declarations: [ReservationsRoutingModule.routedComponents],
  imports: [
      CommonModule,
      ReservationsRoutingModule,
      AngularMaterialModule,
      CoreModule,
      FormsModule,
      ReactiveFormsModule,
    ],
    providers: [ReservationsService],
})
export class ReservationsModule { }
