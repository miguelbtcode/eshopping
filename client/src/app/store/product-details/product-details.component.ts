import { Component, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { StoreService } from '../store.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BreadcrumbService } from 'xng-breadcrumb';
import {BasketService} from '../../basket/basket.service';
import {IBasketItem} from '../../shared/models/basket';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [ CommonModule ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  product?: IProduct;
  quantity = 1;

  constructor(
    private storeService: StoreService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if(id){
      this.storeService.getProductById(id).subscribe({
        next:(response) =>{
          this.product = response;
          this.bcService.set('@productDetails', response.name);
          }, error:(error)=>console.log(error)
      });
    }
  }

  addItemToCart(){
    if (this.product){
      this.basketService.addItemToBasket(this.product, this.quantity);
    }
  }

  incrementQuantity(){
    this.quantity++;
  }

  decrementQuantity(){
    if (this.quantity > 1){
      this.quantity--;
    }
  }
}
