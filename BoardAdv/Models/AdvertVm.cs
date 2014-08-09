using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardAdv.Models
{
    public class AdvertVm
    {
        public int AdvertID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public int? UserId { get; set; }
        public string UserName { get; set; }
        public int PageNumber { get; set; }

        public AdvertVm() { }

        public AdvertVm(Advert advert, int page)
        {
            AdvertID = advert.AdvertID;
            Title = advert.Title;
            Description = advert.Description;
            ImagePath = advert.ImagePath;
            UserId = advert.UserId;
            UserName = advert.User.Login;
            PageNumber = page;
        }
    }

}