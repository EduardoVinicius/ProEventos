import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { EventService } from 'src/app/services/event.service';

import { Event } from "../../../models/Event";

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.scss']
})
export class EventListComponent implements OnInit {

  modalRef?: BsModalRef;
  public events: Event[] = [];
  public filteredEvents: Event[] = [];
  public eventId?: number;

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
              private spinner: NgxSpinnerService,
              private router: Router) { }

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

  openModal(event: any, template: TemplateRef<any>, eventId: number) {
    event.stopPropagation();
    this.eventId = eventId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventService.deleteEvent(this.eventId).subscribe(
      (result: any) => {
        console.log(result);
        this.toastr.success('Event removed successfully!', 'Deleted!');
        this.getEvents();
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Error trying to delete event ${this.eventId}`, 'Error');
      }
    ).add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
  }

  eventDetail(id: number): void {
    this.router.navigate([`events/detail/${id}`]);
  }

}
