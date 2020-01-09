using AdminSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AdminSite.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(adminCredential user)
        {
            if (ModelState.IsValid)
            {
                using (RatnoTechEntities db = new RatnoTechEntities())
                {
                    var obj = db.adminCredentials.Where(m => m.username.Equals(user.username) && m.password.Equals(user.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["adminID"] = obj.aid.ToString();
                        Session["UserName"] = obj.username.ToString();
                        FormsAuthentication.SetAuthCookie(obj.username,true);
                        return RedirectToAction("AdminDashBoard");
                    }
                }
            }
            return View(user);
        }

        [Authorize]
        public ActionResult AdminDashBoard()
        {
            if (Session["adminID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Login");
        }
        [HttpPost]
        public ActionResult ProductData(ProductModel product)
        {
            //Use Namespace called :  System.IO  
            string FileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);

            //To Get File Extension  
            string FileExtension = Path.GetExtension(product.ImageFile.FileName);

            //Add Current Date To Attached File Name  
            FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

            //Get Upload path from Web.Config file AppSettings.  
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

            //Its Create complete path to store in server.  
            product.ImagePath = UploadPath + FileName;

            //To copy and save file into server.  
            product.ImageFile.SaveAs(product.ImagePath);
            return View();
        }
    }
}