﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardAdv.Models
{
    public class User
    {
        public  int Id { get; set; }
        public  string Login { get; set; }
        public  string Password { get; set; }
        public  IEnumerable<Advert> Adverts { get; set; }
    }
}