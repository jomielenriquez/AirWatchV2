using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using AirWatch.Models;
using AirWatch.Repository;

namespace AirWatch.Controllers
{
    public class LoginController : Controller
    {
        //public static LoginModel loginModel = new LoginModel();
        // GET: Login
        public ActionResult SignIn()
        {
            LoginModel loginModel = Session["currentUserLoginModel"] as LoginModel;
            if(loginModel == null)
            {
                loginModel = new LoginModel();
            }
            return View(loginModel);
        }
        public ActionResult Login(LoginModel Login)
        {
            TBL_ACCOUNTS tbl_Accounts = LoginRepository.GetAccountByUsernamePassword(Login.username, Login.password); 
            if(tbl_Accounts == null)
            {
                LoginModel current = new LoginModel();
                current.username = Login.username;
                current.ErrorMessage = "Invalid username or password";
                Session["currentUserLoginModel"] = current;
                return RedirectToAction("SignIn");
            }
            Session["currentUserAccount"] = tbl_Accounts;
            return RedirectToAction("../Home/Index");
        }
    }
}