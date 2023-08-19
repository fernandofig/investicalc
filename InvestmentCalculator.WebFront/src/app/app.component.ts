import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { Investment } from './datacontracts';
import { Revenue } from './datacontracts';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  public investment?: Investment;

  /*constructor(http: HttpClient) {
    http.get<WeatherForecast[]>('/weatherforecast')
      .subscribe(
        {
          next: (result) => this.forecasts = result,
          error: (err) => console.error(err)
        });
  }*/

  title = 'investicalc';
}
