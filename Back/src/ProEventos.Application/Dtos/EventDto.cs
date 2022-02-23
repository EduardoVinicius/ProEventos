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

        [Display(Name = "Number of People")]
        [Range(1, 120000, ErrorMessage = "{0} must be between 1 and 120000 inclusive!")]
        public int PeopleQuantity { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "It is not a valid image!")]
        public string ImageURL { get; set; }

        [Required(ErrorMessage = "Field {0} is required!")]
        [Phone(ErrorMessage = "Phone number must be valid.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The {0} field is required!")]
        [EmailAddress(ErrorMessage = "{0} field must contain a valid email.")]
        public string Email { get; set; }

        public IEnumerable<BatchDto> Batches { get; set; }
        public IEnumerable<SocialNetworkDto> SocialNetworks { get; set; }
        public IEnumerable<SpeakerDto> Speakers { get; set; }
    }
}