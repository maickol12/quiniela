using quiniela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quiniela.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(tblUsers u)
        {
            ModelState.Remove("bActive");
            ModelState.Remove("vName");
            ModelState.Remove("vLastName");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("vPassword");
            if (!ModelState.IsValid)
            {
                return View();
            }
            using(var db = new databaseModelContext())
            {
                int count = db.tblUsers.Where(a => a.vUserName == u.vUserName && a.vPassword == u.vPassword).Count();
                if (count == 0)
                {
                    ModelState.AddModelError("", "Usuario o Contraseña invalidos..");
                }
                else
                {
                    Session["isLogin"] = true;
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult crearUsuario()
        {
            return View();
        }
        [HttpPost]
        public ActionResult crearUsuario(tblUsers user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            int i = 0;
            using (var db = new databaseModelContext())
            {
                try
                {
                    user.bActive = true;
                    db.tblUsers.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }catch(Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View();
                }
            }
        }
    }
}