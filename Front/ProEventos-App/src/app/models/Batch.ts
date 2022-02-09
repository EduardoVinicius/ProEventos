import { Event } from "./Event";

export interface Batch {
    Id: number;
    Name: string;
    Price: number;
    StartDate?: Date;
    EndDate?: Date;
    Quantity: number;
    EventId: number;
    Event: Event;
}