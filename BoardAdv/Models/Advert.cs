using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardAdv.Models
{
    public class Advert
    {
  
        public  int AdvertID { get; set; }
        public  string Title { get; set; }
        public  string Description { get; set; }
        public  string ImagePath { get; set; }
        public DateTime Date { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}