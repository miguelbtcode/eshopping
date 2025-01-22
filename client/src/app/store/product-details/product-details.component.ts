import { Component, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { StoreService } from '../store.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-details',
  imports: [ CommonModule ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  product?: IProduct;

  constructor(private storeService: StoreService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id) {
      this.storeService.getProductById(id).subscribe({
        next:(response) => {
          this.product = response;
        }, error: (error) => console.log(error)
      });
    }
  }
}
