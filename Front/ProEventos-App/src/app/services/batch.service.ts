import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Batch } from "@app/models/Batch";
import { Observable } from "rxjs";
import { take } from "rxjs/operators";

@Injectable()
export class BatchService {
    baseURL = 'https://localhost:5001/api/events';

  constructor(private http: HttpClient) { }

  public getBatchesByEventId(eventId: number): Observable<Batch[]> {
    return this.http.get<Batch[]>(`${this.baseURL}/${eventId}`)
      .pipe(take(1));
  }

  public saveBatch(eventId: number, batches: Batch[]): Observable<Batch[]> {
    return this.http.put<Batch[]>(`${this.baseURL}/${eventId}`, batches)
      .pipe(take(1));
  }

  public deleteBatch(eventId: number, batchId: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${eventId}/${batchId}`)
      .pipe(take(1));
  }
}