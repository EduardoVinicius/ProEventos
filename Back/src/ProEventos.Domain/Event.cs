using System;
using System.Collections.Generic;

namespace ProEventos.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime? EventDate { get; set; }
        public string Subject { get; set; }
        public int PeopleQuantity { get; set; }
        public string ImageURL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<Batch> Batches { get; set; }
        public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
        public IEnumerable<SpeakerEvent> SpeakersEvents { get; set; }
    }
}