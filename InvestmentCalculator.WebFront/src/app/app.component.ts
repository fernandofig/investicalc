import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { createMask } from '@ngneat/input-mask';

import { IInvestment, IRevenue } from './datacontracts';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  errorMsgs: string[] = [];

  investment = {} as IInvestment;
  revenue = {} as IRevenue;

  invAmount = new FormControl('0,00');
  invMonths = new FormControl('2');

  currencyInputMask = createMask({
    alias: 'numeric',
    groupSeparator: '.',
    radixPoint: ',',
    digits: 2,
    digitsOptional: false,
    placeholder: '0,00',
  });

  constructor(private http: HttpClient) { }

  title = 'investicalc';

  requestCalc() {
    this.revenue = {} as IRevenue;

    if (this.validateParameters()) {
      const endpoint = `/api/Revenue/CDB?amount=${this.investment.amount}&months=${this.investment.months}`;

      this.http.get<IRevenue>(endpoint)
        .subscribe(
          {
            next: (result) => {
              this.revenue = result;
            },
            error: (ex) => {
              if (ex.status != 400)
                this.errorMsgs.push("Erro desconhecido. Favor contatar o administrador.");
              else {
                for (const prop in ex.error.errors) {
                  for (const err in ex.error.errors[prop]) {
                    this.errorMsgs.push(`${this.normalizeFieldName(prop)}: ${ex.error.errors[prop][err]}`);
                  }
                }
              }
            }
          }
        );
    }
  }

  validateParameters() {
    this.errorMsgs = [];

    let amount = this.normalizeDecimal(this.invAmount.value);
    let months = this.normalizeInt(this.invMonths.value?.toString());

    if (!this.invAmount.valid || isNaN(amount))
      this.errorMsgs.push("Valor Investido: valor informado inválido");
    else if (amount <= 0)
      this.errorMsgs.push("Valor Investido: valor deve ser positivo");

    if (isNaN(months))
      this.errorMsgs.push("Período: período inválido");
    else if(!this.invMonths.valid)
      this.errorMsgs.push("Período: deve ser especificado um período de no mínimo 2 meses");

    if (this.errorMsgs.length == 0) {
      this.investment.amount = amount;
      this.investment.months = months;
      return true;
    }

    return false;
  }

  normalizeDecimal(val: string | null) {
    if (typeof val == "string")
      val = val.replace(".", "").replace(/[^\d\,]/, "").replace(",", ".");
    else
      val = "0";

    return parseFloat(val);
  }

  normalizeInt(val: string | null | undefined) {
    let intVal: number;

    if (typeof val == "string") {
      intVal = parseFloat(val);
      intVal = intVal != parseInt(val) ? NaN : intVal;
    } else
      intVal = NaN;

    return intVal;
  }

  normalizeFieldName(fldName: string) {
    switch (fldName.toUpperCase()) {
      case "AMOUNT":
        return "Valor Investido";
      case "MONTHS":
        return "Período";
      default:
        return fldName;
    }
  }
}
