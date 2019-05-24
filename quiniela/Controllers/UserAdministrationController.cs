using quiniela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quiniela.Controllers
{
    public class UserAdministrationController : Controller
    {
        // GET: UserAdministration
        public ActionResult Index()
        {
            
            using(var db = new databaseModelContext())
            {
                var users = from s in db.tblUsers
                            select s;
                
                return View(users.ToList());
            }
            
        }
    }
}