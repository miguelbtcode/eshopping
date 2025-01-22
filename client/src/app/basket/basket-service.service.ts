import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject} from 'rxjs';
import {Basket, IBasket} from '../shared/models/basket';

@Injectable({
  providedIn: 'root'
})
export class BasketServiceService {

  baseUrl = 'http://localhost:8001';

  constructor(private http: HttpClient) { }

  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();

  getBasket(username: string){
    return this.http.get<IBasket>(this.baseUrl + '/api/v1/Basket/GetBasket/miguelbtcode').subscribe({
      next: basket => this.basketSource.next(basket)
    });
  }

  setBasket(basket: IBasket){
    return this.http.post<IBasket>(this.baseUrl + '/api/v1/Basket/CreateBasket', basket).subscribe({
      next: basket => this.basketSource.next(basket)
    });
  }

  getCurrentBasket(){
    return this.basketSource.value;
  }
}
