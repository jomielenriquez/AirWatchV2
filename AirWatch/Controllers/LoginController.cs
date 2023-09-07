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
                current.MessageType = "error";
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
        public ActionResult ChangePassWordusingEmail(LoginModel loginModel)
        {
            string code = Session["AirWatchVerificationCode"].ToString();
            if (loginModel.retrievalData.activationcode != code)
            {
                loginModel.retrievalData.activationcode = "";
                loginModel.MessageType = "error";
                loginModel.ErrorMessage = "Invalid one-time pin";
                loginModel.confirmedRetrievalData = true;
                Session["currentUserLoginModel"] = loginModel;
            }
            else if (loginModel.retrievalData.newPassword != loginModel.retrievalData.confirmPassword)
            {
                loginModel.MessageType = "error";
                loginModel.ErrorMessage = "Not matched by password.";
                loginModel.confirmedRetrievalData = true;
                Session["currentUserLoginModel"] = loginModel;
            }
            else
            {
                TBL_ACCOUNTS account = LoginRepository.GetAccountByEmail(loginModel.retrievalData.email);
                Data data = new Data();
                
                TBL_ACCOUNTS filter = new TBL_ACCOUNTS();
                filter.ACCOUNTID = account.ACCOUNTID;
                
                TBL_ACCOUNTS currentAccount = account;
                currentAccount.ACCOUNTID = new Guid();
                currentAccount.PASSWORD = loginModel.retrievalData.confirmPassword;

                string message = data.Update(currentAccount, filter, filter.ACCOUNTID);

                loginModel.MessageType = "success";
                loginModel.ErrorMessage = "New password successfully saved.";
                Session["currentUserLoginModel"] = loginModel;
                return RedirectToAction("SignIn");
            }

            return RedirectToAction("../Login/ForgotPassword");
        }
        public ActionResult ForgotPassword()
        {
            LoginModel loginModel = Session["currentUserLoginModel"] as LoginModel;
            if (loginModel == null)
            {
                loginModel = new LoginModel();
            }
            return View(loginModel);
        }
        [HttpPost]
        public ActionResult GenerateVerifiction(LoginModel loginModel)
        {
            bool checkStatus = ActivationRepository.CheckStatusWithEmail(loginModel.retrievalData.email);
            if (!checkStatus) {
                loginModel.ErrorMessage = "We don't have a record of your email in our system. Please check if email is correct.";
                loginModel.confirmedRetrievalData = false;
            }
            else
            {
                string code = ActivationRepository.CreateActivationWithEmail(loginModel.retrievalData.email);
                Session["AirWatchVerificationCode"] = code;
                loginModel.confirmedRetrievalData = true;
                loginModel.retrievalData.otp = code;
                Session["currentUserLoginModel"] = loginModel;

                return RedirectToAction("../Login/SendEmail");
            }

            Session["currentUserLoginModel"] = loginModel;
            
            return RedirectToAction("../Login/ForgotPassword");
        }
        public ActionResult SendEmail()
        {
            LoginModel loginModel = Session["currentUserLoginModel"] as LoginModel;
            if (loginModel == null)
            {
                loginModel = new LoginModel();
            }
            return View(loginModel);
        }
    }
}