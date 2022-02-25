import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Event } from '@app/models/Event';
import { EventService } from '@app/services/event.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss']
})
export class EventDetailComponent implements OnInit {

  event = {} as Event;
  form!: FormGroup;
  saveMode = 'post';

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return { 
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: true
    };
  }

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private router: ActivatedRoute,
              private eventService: EventService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService) {
    this.localeService.use('pt-br');
  }

  public loadEvent(): void {
    const eventIdParam = this.router.snapshot.paramMap.get('id');

    if (eventIdParam != null) {
      
      this.spinner.show();

      this.saveMode = 'put';

      this.eventService.getEventById(+eventIdParam).subscribe({
        next: (e: Event) => {
          this.event = {... e};
          this.form.patchValue(this.event);
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('Error trying to load event!', 'Error!');
          console.error(error);
        },
        complete: () => {
          this.spinner.hide();
        }
      });
    }
  }

  ngOnInit(): void {
    this.loadEvent();
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

  public cssValidator(formField: FormControl): any {
    return {'is-invalid': formField!.errors && formField!.touched};
  }

  public saveChange(): void {
    this.spinner.show();
    if (this.form.valid) {

      this.event =  (this.saveMode == 'post') ? {... this.form.value} : {id: this.event.id, ... this.form.value};

      this.eventService[this.saveMode](this.event).subscribe(
        () => {
          this.toastr.success('Event saved successfully!', 'Success!');
        },
        (error: any) => {
          console.error(error);
          this.spinner.hide();
          this.toastr.error('Error trying to save event!', 'Error')
        },
        () => {
          this.spinner.hide();
        }
      );

    }
  }
}
