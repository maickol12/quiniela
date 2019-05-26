using PagedList.Mvc;
using PagedList;
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
        public ActionResult Index(string sortOrder,string searchBy,String search,int? page)
        {
            if (!isLogin())
            {
                return RedirectToAction("index", "User");
            }
            searchBy                 = (searchBy == null) ? "" : searchBy;
            ViewBag.sortVname        = (sortOrder == "vNameAsc") ? "vNameDesc" : "vNameAsc";
            ViewBag.sortVlastName    = (sortOrder == "vLastNameAsc")? "vLastNameDesc": "vLastNameAsc";
            ViewBag.SortvUsuario     = (sortOrder == "vUsuarioAsc")?"vUsuarioDesc":"vUsuarioAsc";

            using (var db = new databaseModelContext())
            {

                var users = from s in db.tblUsers
                            select s;
    
                switch (sortOrder)
                {
                    case "vNameAsc":
                        users = users.OrderBy(s => s.vName);
                    break;
                    case "vnameDesc":
                         users = users.OrderByDescending(s => s.vName);
                    break;
                    case "vLastNameAsc":
                         users = users.OrderBy(s => s.vLastName);
                    break;
                    case "vLastNameDesc":
                        users = users.OrderByDescending(s => s.vLastName);
                    break;
                    case "vUsuarioAsc":
                        users = users.OrderBy(s => s.vUserName);
                        break;
                    case "vUsuarioDesc":
                        users = users.OrderByDescending(s => s.vUserName);
                        break;
                    default:
                        users = users.OrderBy(s => s.idUser);
                    break;
                }
                if (searchBy.ToLower().Trim().Equals("name"))
                {
                    if (!search.Equals(""))
                    {
                        users = users.Where(s => s.vName.Contains(search.Trim()));
                    }
                    
                } 
                return View(users.ToPagedList(page ?? 1, 10));

            }
        }
        private bool isLogin()
        {
            try
            {
                bool isLogin = (bool)Session["isLogin"];
                return isLogin;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}