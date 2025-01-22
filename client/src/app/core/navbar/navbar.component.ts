import { Component } from '@angular/core';
import {BasketService} from '../../basket/basket.service';
import {IBasketItem} from '../../shared/models/basket';

@Component({
  selector: 'app-navbar',
  standalone: false,
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {

  constructor(public basketService: BasketService) {}

  getBasketCount(items: IBasketItem[]){
    return items.reduce((sum, item) => sum + item.quantity, 0);
  }
}
