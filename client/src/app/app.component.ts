import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from './shared/models/product';
import { IPagination } from './shared/models/pagination';
import {BasketService} from './basket/basket.service';
@Component({
  selector: 'app-root',
  standalone: false,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'eShopping';

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    const basket_username = localStorage.getItem('basket_username');
    if (basket_username){
      this.basketService.getBasket(basket_username);
    }
  }
}
