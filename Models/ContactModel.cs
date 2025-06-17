using System.ComponentModel.DataAnnotations;

namespace AFC.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Range(1000000000, 9999999999, ErrorMessage = "Enter a valid phone number")]
        public int Phone { get; set; }
        [Range(0, 9999, ErrorMessage = "Extension must be between 0 and 9999")]
        public int Ext { get; set; }
        [StringLength(256, ErrorMessage = "Company Name cannot exceed 256 characters")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [StringLength(256, ErrorMessage = "Website cannot exceed 256 characters")]
        public string Website { get; set; }

        [StringLength(500, ErrorMessage = "Subject cannot exceed 500 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
