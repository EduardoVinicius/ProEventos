import { Batch } from "./Batch";
import { SocialNetwork } from "./SocialNetwork";
import { Speaker } from "./Speaker";

export interface Event {
    Id: number;
    Location: string;
    EventDate?: Date;
    Subject: string;
    PeopleQuantity: number;
    ImageURL: string;
    Phone: string;
    Email: string;
    Batch: Batch[];
    SocialNetworks: SocialNetwork[];
    SpeakersEvents: Speaker[];
}