import { Batch } from "./Batch";
import { SocialNetwork } from "./SocialNetwork";
import { Speaker } from "./Speaker";

export interface Event {
    id: number;
    location: string;
    eventDate?: Date;
    subject: string;
    peopleQuantity: number;
    imageURL: string;
    phone: string;
    email: string;
    batch: Batch[];
    socialNetworks: SocialNetwork[];
    speakersEvents: Speaker[];
}
