using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Author name can only contain letters and spaces")]
        [MinLength(2, ErrorMessage = "Author name must be at least 2 characters")]
        public string Author { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "ISBN must be exactly 13 digits")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        [GenreValidation(ErrorMessage = "Invalid genre. Allowed values: Fiction, Non-Fiction, Science, Technology, History, Biography, Fantasy, Mystery, Romance, Thriller")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price can have maximum 2 decimal places")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Publication year is required")]
        [Range(1800, 2024, ErrorMessage = "Publication year must be between 1800 and current year")]
        public int PublicationYear { get; set; }

        [Required(ErrorMessage = "Quantity available is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity available must be non-negative")]
        public int QuantityAvailable { get; set; }
    }

    public class GenreValidationAttribute : ValidationAttribute
    {
        private readonly string[] validGenres = new[]
        {
            "Fiction", "Non-Fiction", "Science", "Technology",
            "History", "Biography", "Fantasy", "Mystery",
            "Romance", "Thriller"
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Genre is required");
            }

            string genre = value.ToString();
            if (!validGenres.Contains(genre))
            {
                return new ValidationResult($"Genre must be one of: {string.Join(", ", validGenres)}");
            }

            return ValidationResult.Success;
        }
    }
}
