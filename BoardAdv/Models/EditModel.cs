using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoardAdv.Models
{
    public class EditModel
    {
        public int AdvertID { get; set; }

        [Display(Name = "Новая тема:")]
        public string Title { get; set; }

        [Display(Name = "Новое описание:")]
        public string Description { get; set; }

        [Display(Name = "Новая картинка:")]
        public HttpPostedFileBase Image { get; set; }
    }
}