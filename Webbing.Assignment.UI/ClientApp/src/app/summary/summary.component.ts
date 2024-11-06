import { HttpClient } from '@angular/common/http';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output
} from '@angular/core';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html'
})
export class SummaryComponent implements OnInit {

  @Input() simsCount: number = 0;
  @Input() totalUsage: number = 0;
  @Input() topCustomers: Customer[] = [];

  @Output() topCustomerClick = new EventEmitter<Customer>();
  @Output() refresh = new EventEmitter();

  constructor(private readonly http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<Customer[]>('https://localhost:7187/api/usages-group-by-customer?FromDate=2020-10-10&ToDate=2026-10-10')
      .subscribe(
        (response: Customer[]) => {
          console.log(response);
          this.topCustomers = response;
        });
  }

  onTopCustomerClick(customer: Customer) {
    this.topCustomerClick.emit(customer);
  }

  onRefresh() {
    this.refresh.emit();
  }
}

export class Customer {
  id: number = 0;
  name: string = "";
  sims: number = 0;
  usage: number = 0;
}
