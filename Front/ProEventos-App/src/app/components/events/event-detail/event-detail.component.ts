import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss']
})
export class EventDetailComponent implements OnInit {

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      subject: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      location: ['', Validators.required],
      eventDate: ['', Validators.required],
      peopleQuantity: ['', [Validators.required, Validators.max(120000)]],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageURL: ['', Validators.required]
    }); 
  }

  public resetForm(): void {
    this.form.reset();
  }
}
