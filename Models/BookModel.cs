using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Provide title information for the book.")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
