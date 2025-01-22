import { Component, Input } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import {BasketService} from '../../basket/basket.service';

@Component({
  selector: 'app-product-items',
  standalone: false,
  templateUrl: './product-items.component.html',
  styleUrl: './product-items.component.scss'
})
export class ProductItemsComponent {
  @Input() product?: IProduct;

  constructor(private basketService: BasketService){}

  addItemToBasket(){
    this.product && this.basketService.addItemToBasket(this.product);
  }
}
