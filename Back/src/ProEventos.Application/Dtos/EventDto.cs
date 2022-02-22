using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string EventDate { get; set; }

        [Required(ErrorMessage = "The {0} field is required!")]
        // [MinLength(3, ErrorMessage = "{0} must be at least 3 characters long.")]
        // [MaxLength(50, ErrorMessage = "{0} can only have up to 50 characters.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Subject length ranges from 3 up to 50 characters.")]
        public string Subject { get; set; }
        public int PeopleQuantity { get; set; }
        public string ImageURL { get; set; }
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "{0} field must contain a valid email.")]
        public string Email { get; set; }

        public IEnumerable<BatchDto> Batches { get; set; }
        public IEnumerable<SocialNetworkDto> SocialNetworks { get; set; }
        public IEnumerable<SpeakerDto> Speakers { get; set; }
    }
}