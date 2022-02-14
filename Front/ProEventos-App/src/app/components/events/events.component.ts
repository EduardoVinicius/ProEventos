import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Event } from '../../models/Event';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {
  modalRef?: BsModalRef;
  public events: Event[] = [];
  public filteredEvents: Event[] = [];

  public imageWidth = 80;
  public imageMargin = 5;
  public showImage = true;
  private listedFilter = '';

  public get listFilter() : string  {
    return this.listedFilter;
  }

  public set listFilter(value: string) {
    this.listedFilter = value;
    this.filteredEvents = this.listFilter ? this.filterEvents(this.listFilter) : this.events;
  }

  public filterEvents(filterBy: string): Event[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.events.filter(
      (event: any) => event.subject.toLocaleLowerCase().indexOf(filterBy) !== -1
        || event.location.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  constructor(private eventService: EventService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.spinner.show();
    this.getEvents();
  }

  public changeImage(): void {
    this.showImage = !this.showImage;
  }

  public getEvents(): void {
    this.eventService.getEvents().subscribe({
      next: (events: Event[]) => {
        this.events = events
        this.filteredEvents = this.events;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Error loading events', 'Error');
      },
      complete: () => this.spinner.hide()
    });
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('Event removed successfully!', 'Deleted!');
  }

  decline(): void {
    this.modalRef?.hide();
  }
}
