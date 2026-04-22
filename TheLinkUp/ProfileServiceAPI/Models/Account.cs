using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileServiceAPI.Models
{
    public class Account
    {
        public int UserId { get; set; }

        // KEEP THESE REQUIRED FOR REGISTRATION
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be at least 4 characters long.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be at least 3 characters long.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name cannot contain numbers or special characters.")]
        public string FullName { get; set; }

        public List<string> GalleryImages { get; set; } = new List<string>();

        // MAKE THESE NULLABLE/OPTIONAL FOR REGISTRATION
        // We remove [Required] so Create Account works.


        public bool IsHidden { get; set; }

        public int? Age { get; set; } // Nullable int

        public string? Height { get; set; }

        public string? Weight { get; set; }

        public string? Occupation { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for your photo.")]
        public string? PhotoURL { get; set; }

        public string? Description { get; set; }

        public string? Goals { get; set; }

        public string? CommitmentType { get; set; }

        public string? FavoriteMovie { get; set; }

        public string? FavoriteRestaurant { get; set; }

        public string? FavoriteBook { get; set; }

        public string? FavoritePoem { get; set; }

        public string? FavoriteQuote { get; set; }
    }
}