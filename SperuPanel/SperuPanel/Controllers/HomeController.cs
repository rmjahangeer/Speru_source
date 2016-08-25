using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SperuPanel.Models;
namespace SperuPanel.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        SperuEntities se = new SperuEntities();

        public ActionResult Index()
        {
            // return View();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AccountValidation()
        {


            //LogIn l = new LogIn();
            var name = Request["name"];
            var Vpssword = Request["password"];
            try
            {

                adminlogin res = se.adminlogins.First(x => x.name.Equals(name) && x.password.Equals(Vpssword));

                if (res != null)
                {

                    return RedirectToAction("Shownews");
                }

            }
            catch
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }
        public ActionResult Shownews()
        {
            //    Speru s = new Speru();
            return View(se.sperunews.ToList());
        }
        public ActionResult Insertnews()
        {
            //    Speru s = new Speru();
            sperunew s = new sperunew();
            s.heading = Request["heading"];
            s.description = Request["description"];
            String img = Request["image"];


            if (img != null && img.Length > 0)
            {
                s.image_url = img;
                //  }
            }
            else
            {
                HttpPostedFileBase file = Request.Files["imageUpload"];
                string fname = s.heading + ".jpg";
                string path = Path.Combine(Server.MapPath("~/images"), fname);

                s.image_url = "/images/" + fname;
                //if(file !=null){
                file.SaveAs(path);
            }
            s.date = Request["date"];
            se.sperunews.Add(s);
            se.SaveChanges();
            return RedirectToAction("Shownews");
        }
        public ActionResult Addnews()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }

        public ActionResult Showhelps()
        {
            return View(se.speruhelps.ToList());
        }

        public ActionResult Addhelp()
        {
            //    Speru s = new Speru();
            speruhelp s = new speruhelp();
            s.name = Request["name"];
            s.email = Request["email"];
            s.message = Request["message"];
            //  s.date = Request["date"];
            se.speruhelps.Add(s);
            se.SaveChanges();
            return RedirectToAction("Shownews");
        }

        public ActionResult AddHelpApp()
        {
            speruhelp s = new speruhelp();
            s.name = Request["name"];
            s.email = Request["email"];
            s.message = Request["message"];
            //  s.date = Request["date"];
            if (s != null)
            {
                se.speruhelps.Add(s);
                se.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Deletenews()
        {
            sperunew sn = new sperunew();
            int id = Int32.Parse(Request["id"]);

            sn = se.sperunews.FirstOrDefault(x => x.Id == id);
            if (sn != null)
            {
                se.sperunews.Remove(sn);
                se.SaveChanges();
                return RedirectToAction("Shownews");
            }
            else
            {
                return RedirectToAction("Shownews");
            }

        }

        public ActionResult Deletehelp()
        {
            speruhelp sh = new speruhelp();
            int id = Int32.Parse(Request["id"]);

            sh = se.speruhelps.FirstOrDefault(x => x.Id.Equals(id));
            if (sh != null)
            {
                se.speruhelps.Remove(sh);
                se.SaveChanges();
                return RedirectToAction("Showhelps");
            }
            else
            {
                return RedirectToAction("Showhelps");
            }

        }

        public ActionResult JsonData_news()
        {
            return Json(se.sperunews.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult JsonData_help()
        {
            return Json(se.speruhelps.ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}
