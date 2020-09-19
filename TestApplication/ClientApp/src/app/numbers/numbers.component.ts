import { Component } from '@angular/core';
import { DataService } from './data.service';
import { Numbers } from "./numbers";

@Component({
  selector: 'app-numbers',
  templateUrl: './numbers.component.html',
  providers: [DataService]
})

export class NumbersComponent {

  numbers: Numbers[] = [];
  numberstr : string;

  constructor(private dataService: DataService) {}

  getSum() {
    var numbs = this.numberstr.split(' ').map(Number);
    var numb = new Numbers(numbs);
    this.dataService.getSquaredSum(numb)
      .subscribe((data: number) => {
        numb.result = data;
        this.numbers.unshift(numb);
      });
  }
}
