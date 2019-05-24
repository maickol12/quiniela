using quiniela.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            ModelState.Remove("vConfirmPassword");
            ModelState.Remove("vPassword");
            if (!ModelState.IsValid)
            {
                return View();
            }
            using(var db = new databaseModelContext())
            {
                tblUsers user = db.tblUsers.Where(a => a.vUserName == u.vUserName && a.vPassword == u.vPassword).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("", "Usuario o Contraseña invalidos..");
                }
                else
                {
                    Session["idUser"]  = user.idUser; 
                    Session["isLogin"] = true;
                    Session["vNameUser"] = user.vName + " " + user.vLastName;
                    if (user.tblRole.Count > 0)
                    {
                        Session["vNameRol"] = user.tblRole.FirstOrDefault().vNameRol;
                    }
                    else
                    {
                        Session["vNameRol"] = "";
                    }
                    if (String.IsNullOrEmpty(user.vProfilePicture))
                    {
                        Session["vProfilePicture"] = "https://raw.githubusercontent.com/azouaoui-med/pro-sidebar-template/gh-pages/src/img/user.jpg";
                    }
                    else
                    {
                        var path = "~/Content/profilePicture/" + user.vProfilePicture;
                        Session["vProfilePicture"] = path;
                    }
                    return RedirectToAction("index","Dashboard");
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
                    var file = Request.Files["perfilPicture"];
                    var fileName = "";
                    if (file != null && file.ContentLength > 0)
                    {
                        fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/profilePicture/"),fileName);
                        file.SaveAs(path);
                    }

                    user.bActive = true;
                    user.vProfilePicture = fileName;
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