import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/type';
import { StoreParams } from '../shared/models/storeParams';

@Injectable({
  providedIn: 'root',
})
export class StoreService {

  baseUrl = 'http://localhost:8000';

  constructor(private http: HttpClient) {}

  getProductById(id: string){
    return this.http.get<IProduct>(this.baseUrl + '/api/v1/Catalog/GetProductById/' + id);
  }

  getProducts(storeParams: StoreParams) {
    let params = new HttpParams();
    if (storeParams.brandId) {
      params = params.append('brandId', storeParams.brandId);
    }
    if (storeParams.typeId) {
      params = params.append('typeId', storeParams.typeId);
    }
    if (storeParams.search) {
      params = params.append('search', storeParams.search);
    }

    params = params.append('sort', storeParams.sort);
    params = params.append('pageIndex', storeParams.pageNumber);
    params = params.append('pageSize', storeParams.pageSize);

    return this.http.get<IPagination<IProduct>>(
      this.baseUrl + '/api/v1/Catalog/GetAllProducts?PageIndex=1&PageSize=10',
      { params }
    );
  }

  getBrands() {
    return this.http.get<IBrand[]>(
      this.baseUrl + '/api/v1/Catalog/GetAllBrands'
    );
  }

  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + '/api/v1/Catalog/GetAllTypes');
  }
}
