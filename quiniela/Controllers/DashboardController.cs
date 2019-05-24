using quiniela.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpGet]
        public ActionResult logout()
        {
            Session["isLogin"] = false;
            Session["idUser"]  = 0;
            return RedirectToAction("index", "user");
        }
    }
}