import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { HttpClient } from '@angular/common/http';
import { response } from 'express';
import { CommonModule } from '@angular/common';
import { IProduct } from './shared/models/product';
import { IPagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'eShopping';
  products: IProduct[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<IPagination<IProduct>>('http://localhost:8000/api/v1/Catalog/GetAllProducts?PageIndex=1&PageSize=10').subscribe({
      next:response => {
        this.products = response.data,
        console.log(response)
      },
      error: error => console.log(error),
      complete: () => {
        console.log('Catalog API call completed');
      },
    });
  }
}
