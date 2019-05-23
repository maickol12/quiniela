using quiniela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quiniela.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            
            if (isLogin())
            {
                try
                {
                    int idUser = (int) Session["idUser"];
                    string vRole = "";
                    tblUsers user = null;
                    using (var db = new databaseModelContext())
                    {
                        user    = db.tblUsers.Find(idUser);
                        if (user != null)
                        {
                            if (user.tblRole.Count > 0)
                            {
                                vRole = user.tblRole.FirstOrDefault().vNameRol;
                            }
                        }
                    }
                    ViewBag.vNombreDeUsuario = user.vName + " " + user.vLastName;
                    ViewBag.vNameRol         = vRole;

                }
                catch(Exception e)
                {

                }
                return View();
            }
            else
            {
                return RedirectToAction("index", "User");
            }
            
        }

        private bool isLogin()
        {
            try
            {
                bool isLogin = (bool) Session["isLogin"];
                return isLogin;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}