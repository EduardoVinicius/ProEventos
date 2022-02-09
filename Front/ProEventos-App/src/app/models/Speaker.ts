import { SocialNetwork } from "./SocialNetwork";

export interface Speaker {
    Id: number;
    Name: string;
    MiniResume: string;
    ImageURL: string;
    Phone: string;
    Email: string;
    SocialNetworks: SocialNetwork[];
    SpeakersEvents: Speaker[];
}