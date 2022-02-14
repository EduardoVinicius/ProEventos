import { Event } from './../models/Event';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class EventService {
  baseURL = 'https://localhost:5001/api/events';

  constructor(private http: HttpClient) { }

  public getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseURL);
  }

  public getEventsBySubject(subject: string): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.baseURL}/${subject}/subject`);
  }

  public getEventById(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.baseURL}/${id}`);
  }
}
