using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PRN221_PE_1407.Models
{
    public partial class AccountUser
    {
        public string UserId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage ="Password is required")]
        public string UserPassword { get; set; }
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Full name is required")]
        public string UserFullName { get; set; }
        public int? UserRole { get; set; }
    }
}
