import { Event } from "./Event";
import { Speaker } from "./Speaker";

export interface SocialNetwork {
    Id: number;
    Name: string;
    URL: string;
    EventId?: number;
    Event: Event;
    SpeakerId?: number;
    Speaker: Speaker;
}