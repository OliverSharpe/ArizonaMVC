using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Arizona.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(30)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(250)]
        public string Message { get; set; }

    }
}
