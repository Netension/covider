using System;
using System.ComponentModel.DataAnnotations;

namespace Netension.Covider.Application.Options
{
    public class CouchDbOptions
    {
        [Required]
        public Uri Url { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
