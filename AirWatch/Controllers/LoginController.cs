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
            Session["AccountID"] = tbl_Accounts.ACCOUNTID;
            return RedirectToAction("../Home/Index");
        }
        public ActionResult Logout()
        {
            TBL_ACCOUNTS account = Session["currentUserAccount"] as TBL_ACCOUNTS;
            LoginModel current = new LoginModel();
            current.username = account.USERNAME;
            Session["currentUserLoginModel"] = current;
            Session.Remove("currentUserAccount");
            return RedirectToAction("SignIn");
        }
        [HttpPost]
        public ActionResult ChangePassword(AppModel model)
        {
            AppModel appModel = new AppModel();
            if (appModel.Account == null)
            {
                return RedirectToAction("../Login/SignIn");
            }

            if(model.ChangePass.firstPass != model.ChangePass.secondPass)
            {
                Session["Message"] = "Password does not matched";
                Session["Type"] = "error";
            }
            else
            {
                Data data = new Data();
                TBL_ACCOUNTS currentAccount = appModel.Account;
                currentAccount.ACCOUNTID = new Guid();
                currentAccount.PASSWORD = model.ChangePass.firstPass;

                TBL_ACCOUNTS filter = new TBL_ACCOUNTS();
                filter.ACCOUNTID = appModel.AccountID;
                string message = data.Update(currentAccount, filter, appModel.AccountID);

                Session["Message"] = "New password successfully saved.";
                Session["Type"] = "success";
            }

            return RedirectToAction("../Home/Index");
        }
    }
}