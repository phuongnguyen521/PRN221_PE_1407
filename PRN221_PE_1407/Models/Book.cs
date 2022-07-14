using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PRN221_PE_1407.Models
{
    public partial class Book
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Book Id is required")]
        [StringLength(12, ErrorMessage ="Book Id shall be from 6 to 12 characters", MinimumLength = 6)]
        public string BookId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage ="Book Name is required")]
        public string BookName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage ="Quantity is required")]
        public int? Quantity { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Author name is required")]
        public string AuthorName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Publisher Id is required")]
        public string PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}
