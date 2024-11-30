using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; //Using the directive

namespace MovieList.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a year.")]
        [Range(1889, 2999, ErrorMessage = "Year must be after 1889.")]
        public int? Year { get; set; }
        [Required(ErrorMessage = "Please enter a rating.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }

        [Required(ErrorMessage = "Please enter a genre.")]
        public string GenreId { get; set; }
        [ValidateNever]
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Please enter a release date.")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please enter the director.")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Please enter the duration.")]
        [Range(1, 1000, ErrorMessage = "Duration must be between 1 and 1000 minutes.")]
        public int Duration { get; set; }

        public string Slug => Name?.Replace(' ', '-').ToLower() + '-' + Year?.ToString();
    }
}
