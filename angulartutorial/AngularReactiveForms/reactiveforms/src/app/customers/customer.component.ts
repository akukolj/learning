import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidatorFn} from '@angular/forms';

import { Customer } from './customer';

function ratingRange(max: number, min: number): ValidatorFn {
  return (c: AbstractControl): { [key: string]: boolean} | null  => {
    if (c.value !== null && (isNaN(c.value) || c.value < min || c.value > max )) {
      return {'range': true};
    }
    return null;
  };
}
function emailMatcher(c: AbstractControl): {[key: string]: boolean} | null {
  const emailControl = c.get('email');
  const confirmEmail = c.get('confirmEmail');

  if (emailControl.pristine || confirmEmail.pristine) {
    return null;
  }

  if (emailControl.value === confirmEmail.value) {
    return null;
  }
  return {'match': true};
}

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  customer = new Customer();
  customerForm: FormGroup;
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.customerForm =  this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      emailGroup: this.fb.group({
        email: ['', [Validators.required,  Validators.email]],
        confirmEmail: ['', Validators.required]
      }, {validator: emailMatcher}),
      sendCatalog: true,
      phone: '',
      notification: 'email',
      rating: [null, ratingRange(5, 1)]
    });
  }

  populateTestData(): void {
    this.customerForm.setValue({
      firstName: 'Jack',
      lastName: 'Harkens',
      email: 'jack@tetete.com',
      sendCatalog: false
    });
  }

  save() {
    console.log(this.customerForm);
    console.log('Saved: ' + JSON.stringify(this.customerForm.value));
  }

  setNotification(notifyVal: string): void {
    const phoneControl = this.customerForm.get('phone');

    if (notifyVal === 'text') {
      phoneControl.setValidators(Validators.required);
    } else {
      phoneControl.clearValidators();
    }
    phoneControl.updateValueAndValidity();
  }
}
