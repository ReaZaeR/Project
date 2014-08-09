using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoardAdv.Models
{
    public class AddAdvert
    {
        [Required]
        [Display(Name = "Тема:")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Описание:")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Картинка:")]
        public HttpPostedFileBase Image { get; set; }
    }
}