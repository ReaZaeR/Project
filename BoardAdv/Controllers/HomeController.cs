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

namespace BoardAdv.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
  
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        // ------------------------ Добавить --------------
        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddAdvert advert)
        {
            if (ModelState.IsValid) 
            {
                Repository repository = new Repository();
                repository.AddAdvert(advert, User.Identity.Name);

                return RedirectToAction("Index", "Home");
            }
            return View(advert);
        }

        // -------------------------- Удаление ---------------
        [Authorize]
        public ActionResult DeleteAdvert(int id)
        {
                           
                Repository repository = new Repository();

                var user = repository.GetUser(User.Identity.Name);
                var advert = repository.GetAdvert(id);

                if ( repository.GetAdvert(id) != null)
                {
                    if(user.Id == advert.UserId)
                    {
                        repository.DeleteAdvert(advert);

                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Login", "Account");// нет прав
                }
                return RedirectToAction("Index", "Home");// объявления не найдено
            
        }
        //-------------------------Редактировать -----------------
        [Authorize]
        [HttpGet]
        public ActionResult EditAdvert(int id)
        {
            ViewBag.AdvertID = id;
            Repository repository = new Repository();

            var user = repository.GetUser(User.Identity.Name);
            var advert = repository.GetAdvert(id);

            if (user.Id == advert.UserId)
            {
                var model = new EditModel()
                {
                    AdvertID = advert.AdvertID,
                    Description = advert.Description,
                    Title = advert.Title
                };
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditAdvert(EditModel model)
        {
            Repository repository = new Repository();

            var user = repository.GetUser(User.Identity.Name);

            if (repository.GetAdvert(model.AdvertID) != null)
            {
                if (user.Id == repository.GetAdvert(model.AdvertID).UserId)
                {                   
                    repository.EditAdvert(model);
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Login", "Account");// нет прав
            }
            return RedirectToAction("Index", "Home");// объявления нет

        }
        //---------------------------------------------------------------
        [HttpGet]
        public ActionResult Search(string str, int page = 1)
        {
            str = (str == null) ? "" : str.ToLower();

            Repository repository = new Repository();
            return  View(repository.GerSearchAdvert(str,page));    
          
        }

    }
}
