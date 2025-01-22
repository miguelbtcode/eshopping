import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreComponent } from './store.component';
import { ProductItemsComponent } from './product-items/product-items.component';
import { SharedModule } from '../shared/shared.module';
import { StoreRoutingModule } from './store-routing.module';

@NgModule({
  declarations: [
    StoreComponent,
    ProductItemsComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    StoreRoutingModule,
  ]
})
export class StoreModule { }
