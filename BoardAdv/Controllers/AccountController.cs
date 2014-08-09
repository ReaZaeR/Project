using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardAdv.Models;
using System.Web.Security;
using BoardAdv.Reposit;

namespace BoardAdv.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogModel model)
        {
            if (ModelState.IsValid)
            {

                Repository repository = new Repository();

                if (repository.GetUser(model.Login, model.Password) != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View();
        }

        //---------------------------------------------------------------------------------------
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegModel model)
        {
            if (ModelState.IsValid)
            {
             
                Repository repository = new Repository();
                
                if (repository.GetUser(model.Login) == null)
                {
                    repository.NewUser(model.Login, model.Password);    
              
                    if (repository.GetUser(model.Login, model.Password) != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View();
        }
        //-----------------------------------------------------------------------------------------------------
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
