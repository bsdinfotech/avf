using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVCApplication.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web;
using Microsoft.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.PowerBI.Api.Models;
using Newtonsoft.Json;
using Nancy.Json;

namespace CoreMVCApplication.Controllers
{
    public class UserController : Controller
    {
        ClsUtility util = new ClsUtility();
        public IActionResult Index()
        {
           // GoogleResponse();
            return View();
        }
       // private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public IActionResult SignIn()
        {
            //GoogleResponse();
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(AdminUserMaster login)
        {
            DataTable dt = util.execQuery("SELECT * FROM tblSignUp WHERE Email='" + login.Email + "'" + "AND Pasword='" + util.cryption(login.Password.Trim()) + "'");
            if (dt.Rows.Count>0)
            {
                HttpContext.Session.SetString("firstname", dt.Rows[0]["FullName"].ToString());
                HttpContext.Session.SetString("userId", dt.Rows[0]["signUp_Id"].ToString());
                HttpContext.Session.SetString("UserEmail", dt.Rows[0]["Email"].ToString());
                return RedirectToAction("Dashboard", "Patients");
            }
            else
            {
                ViewBag.Message = "Email and password Invalid";  
            }
            return View("SignIn");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUp sg)
        {
            if (sg.Password == sg.RePassword)
            {
                DataTable dt = util.SelectParticular("tblSignUp", "*", "Email='" + sg.Email + "'");
                if (dt.Rows.Count > 0)
                {
                    ViewBag.ErrorMsg = "Your email id is already Exit";
                }
                else
                {
                    string sqlquery = "Insert into tblSignUp(FullName,Email,Pasword)values('" + sg.FullName + "','" + sg.Email + "','" + util.cryption(sg.Password) + "')";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Your Email is  " + sg.Email + " and Password is " + sg.Password + "";
                    }
                    else
                    {
                        ViewBag.Message = "Not insert";
                    }
                }
            }
            else
            {
                ViewBag.Message = "Password and Re-password are not same.";
            }
               
            return View();
        }
        

        /*-------------For Google Authentication--------------*/
        public async Task GoogleLogin()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }
     
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                }) ;
            string jsondata = JsonConvert.SerializeObject(claims);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<GoogleAuthentication> listgoogledata = (List<GoogleAuthentication>)javaScriptSerializer.Deserialize<List<GoogleAuthentication>>(jsondata);
            string ProviderKey = string.Empty;
            string FullName = string.Empty;
            string FirstName = string.Empty;
            string LastName = string.Empty;
            string email = string.Empty;
            string ProfileImage = string.Empty;
            string LoginProvider = string.Empty;
            foreach (var google in listgoogledata)
            {
                if (google.Type.Contains("nameidentifier"))
                {
                    ProviderKey = google.Value;
                }
                else if (google.Type.Contains("givenname"))
                {
                    FirstName = google.Value;
                }
                else if(google.Type.Contains("surname"))
                {
                    LastName = google.Value;
                }
                else if (google.Type.Contains("name"))
                {
                    FullName = google.Value;
                }
                else if (google.Type.Contains("emailaddress"))
                {
                    email = google.Value;
                }
            }
            DataTable dt = util.execQuery("SELECT * FROM tbluser WHERE ProviderKey='" + ProviderKey + "'");
            if (dt.Rows.Count > 0)
            {
                HttpContext.Session.SetString("firstname", dt.Rows[0]["Firstname"].ToString());
                HttpContext.Session.SetString("userId", dt.Rows[0]["id"].ToString());
                HttpContext.Session.SetString("UserEmail", dt.Rows[0]["Email"].ToString());
                return RedirectToAction("Dashboard", "Patients");
            }
            else
            {
                string sqlquery = "Insert into tbluser(email,FullName,FirstName,LastName,LoginProvider,ProviderKey)values('" + email + "','"+ FullName+"','"+FirstName+"','"+LastName+"','Google','"+ProviderKey+"')";
                string status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    HttpContext.Session.SetString("firstname", dt.Rows[0]["Firstname"].ToString());
                    HttpContext.Session.SetString("userId", dt.Rows[0]["id"].ToString());
                    HttpContext.Session.SetString("UserEmail", dt.Rows[0]["Email"].ToString());
                    ModelState.Clear();
                    ViewBag.Message = "External User Insert Success";
                    return RedirectToAction("Dashboard", "Patients");

                }
                else
                {
                    ViewBag.Message = "External User Not insert";
                }
            }
            ViewBag.ListGoogleData = listgoogledata;
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            //await HttpContext.SignOutAsync();
            //return RedirectToAction("SignIn", "User");
            HttpContext.Session.Clear();
            //HttpContext.Session.Abandon();
            return View("SignIn");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(AdminUserMaster login)
        {
            if (login.Password == login.ReEnterPassword)
            {
                if (login.Password.Length == 8)
                {
                    string sqlquery = "update tbluser set Password='" + util.cryption(login.Password) + "' where Email='" + login.Email + "'";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {

                        ViewData["msg"] = "Password Updated Successfull";
                        // ModelState.Clear();

                    }
                    else
                    {
                        ViewData["msg"] = "Not Update";
                    }
                }
                else
                {
                    ViewData["msg"] = " Please Enter Strong Password With 8 digit and Sprcial Character ";
                }
            }
            else
            {
                ViewData["msg"] = "The password and ReEnter password do not match.";
            }
            //ViewBag.SubmitValue = "Update";
            return View();
        }
    }
}



