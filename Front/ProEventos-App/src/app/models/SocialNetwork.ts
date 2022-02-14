import { Event } from "./Event";
import { Speaker } from "./Speaker";

export interface SocialNetwork {
    id: number;
    name: string;
    URL: string;
    eventId: number;
    speakerId: number;
}
