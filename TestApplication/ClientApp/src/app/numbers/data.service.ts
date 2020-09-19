import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Numbers } from './numbers';

@Injectable()
export class DataService {
  private url = "/api/GetSquaredSum";

  constructor(private http: HttpClient) {
  }

  getSquaredSum(numbers: Numbers) {
    return this.http.post(this.url, numbers);
  }
}
