using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardAdv.Models;
using System.Data.Entity;
using System.Web.Security;
using System.IO;
using BoardAdv.Reposit;

namespace BoardAdv.Reposit
{
    public class Repository
    {
        DBContext db = new DBContext();

        //---------------User-------------
        public User GetUser(string _login, string _pass)
        {
            User user = db.Users.FirstOrDefault(u => u.Login == _login && u.Password == _pass);;
            return user;
        }

        public User GetUser(string _login) 
        {
            User user = null;
            user = db.Users.FirstOrDefault(u => u.Login == _login);
            return user;
        }

        public void NewUser(string _login, string _pass) 
        {
            db.Users.Add(new User { Login = _login, Password = _pass });
            db.SaveChanges();
        }
        //---------------------------------------------------

        //-------------Advert---------------------------
        public Advert GetAdvert(int _id) 
        {
            return db.Adverts.FirstOrDefault(b => b.AdvertID == _id);

        }

        public void AddAdvert(AddAdvert _advert, string _indname) 
        {
             var user = db.Users.FirstOrDefault(b => b.Login == _indname);
             string appData = AppDomain.CurrentDomain.BaseDirectory + "Content/Images";
             string randomFileName = Path.GetRandomFileName();
             string files = Path.Combine(appData, randomFileName + Path.GetExtension(_advert.Image.FileName));
             _advert.Image.SaveAs(files);
                               
              var item = new Advert
              {         
                   Date = DateTime.Now,
                   Description = _advert.Description,
                   ImagePath = "\\Content\\Images\\" + randomFileName + Path.GetExtension(_advert.Image.FileName),
                   Title = _advert.Title,
                   UserId = user.Id,
              };
            db.Adverts.Add(item);
            db.SaveChanges();
        }

        public void DeleteAdvert(Advert _advert) 
        {
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + _advert.ImagePath);
            db.Adverts.Remove(_advert);
            db.SaveChanges();
        }


        public void EditAdvert(EditModel _model) 
        {
            Advert advert = db.Adverts.FirstOrDefault(a => a.AdvertID == _model.AdvertID);

            if (_model.Title != null)
            {
                advert.Title = _model.Title;
            }
            if (_model.Description != null)
            {
                advert.Description = _model.Description;
            }
            if (_model.Image != null)
            {
                System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + advert.ImagePath);
                _model.Image.SaveAs(AppDomain.CurrentDomain.BaseDirectory + advert.ImagePath);
            }
            db.SaveChanges();
        }

        public List<AdvertVm> GerSearchAdvert(string _str, int _page) 
        {
            var adverts = db.Adverts
                .Where(a => a.Title.ToLower().Contains(_str) || a.Description.ToLower().Contains(_str))
                .Include(p => p.User).ToList();

            var result = new List<AdvertVm>();

            if (_page < 1)
            {
                _page = 1;
            }

            if (_page > adverts.Count / 3)
            {
                if (adverts.Count % 3 != 0) { _page = (adverts.Count / 3) + 1; }
                else { _page = (adverts.Count / 3); }
            }

            if (adverts.Count != 0)
            {
                for (int i = (_page - 1) * 3; i < _page * 3 && i < adverts.Count; i++)
                {
                    result.Add(new AdvertVm(adverts.ElementAt(i), _page));
                }
            }
            return result;
        }

        //--------------------------------------------------
    }
}