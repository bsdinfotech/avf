using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVCApplication.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMVCApplication.Controllers
{
    public class Admin : Controller
    {
        ClsUtility util = new ClsUtility();
        // string ExistingImage;
        public IActionResult Index()
        {
            return View();
        }
       
        #region Menu Master Page
        public IActionResult MenuMaster()
        {
            DataTable dt = new DataTable();
            dt = util.execQuery("select isnull((select max(isnull(PrintOrder,1)) as PrintOrder from tblmenu   where isnull(MenuParents,0) = 0),0) + 1 as PrintOrder");
            if (dt.Rows.Count > 0)
            {
                ViewBag.PrintOrderValue = dt.Rows[0]["PrintOrder"].ToString();
            }
            ViewBag.SubmitValue = "Save";
            
            return View();
        }
        /*--------Create Update Menu Master --------- */
        [HttpPost]
        public IActionResult MenuMasterSaveUpdate(MenuMaster menumaster)
        {
            string sqlquery;
            int view; int add; int modify; int inquire;
            int del;
            int parent;
            int status_value;
            try
            {
                ViewBag.SubmitValue = "Save";
                if (menumaster.MenuImageFile != null)
                {
                    if (menumaster.MenuImageFile.Length > 0)
                    {
                        string filename = Path.GetFileNameWithoutExtension(menumaster.MenuImageFile.FileName);
                        string extension = Path.GetExtension(menumaster.MenuImageFile.FileName);
                        menumaster.MenuImage = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine("wwwroot/MenuImages/", filename);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            menumaster.MenuImageFile.CopyTo(fileStream);
                        }
                    }
                }
                else
                {
                    menumaster.MenuImage = "noPhoto.png";
                }

                if (menumaster.Status == true)
                {
                    status_value = 1;
                }
                else
                {
                    status_value = 0;
                }
                if (menumaster.Views == true)
                {
                    view = 1;
                }
                else
                {
                    view = 0;
                }
                if (menumaster.Adds == true)
                {
                    add = 1;
                }
                else
                {
                    add = 0;
                }
                if (menumaster.Modify == true)
                {
                    modify = 1;
                }
                else
                {
                    modify = 0;
                }
                if (menumaster.inquire == true)
                {
                    inquire = 1;
                }
                else
                {
                    inquire = 0;
                }
                if (menumaster.deleted == true)
                {
                    del = 1;
                }
                else
                {
                    del = 0;
                }
                if (menumaster.Parent == true)
                {
                    parent = 1;
                }
                else
                {
                    parent = 0;
                }
                if (menumaster.id == 0)
                {
                    sqlquery = "Insert into tblMenu(Name,ShortName,Views,Adds,Modify,inquire,deleted,Parent,PageName,MenuParents,MenuImage,PrintOrder,Other1,Other2,Other3,Status,CreateDate)values('" + menumaster.Name + "','" + menumaster.ShortName + "','" + view + "','" + add + "','" + modify + "','" + inquire + "','" + del + "','" + parent + "','" + menumaster.PageName + "','" + menumaster.MenuParents + "','" + menumaster.MenuImage + "','" + menumaster.PrintOrder + "','" + menumaster.Other1 + "','" + menumaster.Other2 + "','" + menumaster.Other3 + "','" + status_value + "',getdate() )";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewData["msg"] = " insert Success";
                       
                    }
                    else
                    {
                        ViewData["msg"] = "Not insert";
                    }
                }
                else
                {
                    if (menumaster.MenuImageFile != null)
                    {
                        sqlquery = "UPDATE tblMenu SET Name = '" + menumaster.Name + "', ShortName = '" + menumaster.ShortName + "', Views ='" + view + "', Adds='" + add + "',Modify='" + modify + "' ,inquire='" + inquire + "',deleted='" + del + "',Parent='" + parent + "',PageName='" + menumaster.PageName + "', MenuParents='" + menumaster.MenuParents + "', MenuImage='" + menumaster.MenuImage + "',PrintOrder='" + menumaster.PrintOrder + "',Other1='" + menumaster.Other1 + "',Other2='" + menumaster.Other2 + "',Other3='" + menumaster.Other3 + "',Status='" + status_value + "',ModifyDate=getdate() where id=" + menumaster.id + "";

                    }
                    else
                    {
                        sqlquery = "UPDATE tblMenu SET Name = '" + menumaster.Name + "', ShortName = '" + menumaster.ShortName + "', Views ='" + view + "', Adds='" + add + "',Modify='" + modify + "' ,inquire='" + inquire + "',deleted='" + del + "',Parent='" + parent + "',PageName='" + menumaster.PageName + "', MenuParents='" + menumaster.MenuParents + "', MenuImage='" + menumaster.ExistingImage + "',PrintOrder='" + menumaster.PrintOrder + "',Other1='" + menumaster.Other1 + "',Other2='" + menumaster.Other2 + "',Other3='" + menumaster.Other3 + "',Status='" + status_value + "',ModifyDate=getdate() where id=" + menumaster.id + "";
                    }

                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewData["msg"] = " Update Success";
                      

                    }
                    else
                    {
                        ViewData["msg"] = "Not Update";
                    }
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return View("MenuMaster");
        }
        /*-----------Show All Record On MenuMaster Page--------*/
        [HttpGet]
        public JsonResult GetAllMenuList()
        {
            string sqlQuery;
            try
            {
                sqlQuery = "Select id, isnull(Name,'')as Name , isnull(ShortName,'')as ShortName, isnull(PageName,'')as PageName, isnull(Status,0)as Status From tblMenu";
                DataSet ds = util.TableBind(sqlQuery);
                List<MenuMaster> MenuList = new List<MenuMaster>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MenuList.Add(new MenuMaster
                    {
                        id = Convert.ToInt32(dr["id"]),
                        Name = Convert.ToString(dr["Name"]),
                        ShortName = Convert.ToString(dr["ShortName"]),
                        PageName = Convert.ToString(dr["PageName"]),
                        Status = Convert.ToBoolean(dr["Status"])
                    });
                }
                return Json(MenuList);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
     
        /*-------------Edit Menu List record by Id -------*/
        [HttpPost]
        public JsonResult EditMenuById(int id)
        {
            MenuMaster menulist = new MenuMaster();
            try
            {
                string sql = "Select id, isnull(Name,'')as Name , isnull(ShortName,'')as ShortName, Views,Adds,Modify,inquire,deleted, Parent, PageName, MenuParents, MenuImage, PrintOrder, Other1, Other2, Other3, isnull(PageName,'')as PageName, isnull(Status,0)as Status From tblMenu WHERE id= '" + id + "'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    menulist.id = Convert.ToInt32(dr["id"]);
                    menulist.Name = dr["Name"].ToString();
                    menulist.ShortName = dr["ShortName"].ToString();
                    menulist.Views = Convert.ToBoolean(dr["Views"]);
                    menulist.Adds = Convert.ToBoolean(dr["Adds"]);
                    menulist.Modify = Convert.ToBoolean(dr["Modify"]);
                    menulist.inquire = Convert.ToBoolean(dr["inquire"]);
                    menulist.deleted = Convert.ToBoolean(dr["deleted"]);
                    menulist.Parent = Convert.ToBoolean(dr["Parent"]);
                    menulist.PageName = dr["PageName"].ToString();
                    menulist.MenuParents = dr["MenuParents"].ToString();
                    menulist.MenuImage = dr["MenuImage"].ToString();
                    menulist.PrintOrder = Convert.ToInt32(dr["PrintOrder"]);
                    menulist.Other1 = dr["Other1"].ToString();
                    menulist.Other2 = dr["Other2"].ToString();
                    menulist.Other3 = dr["Other3"].ToString();
                    menulist.Status = Convert.ToBoolean(dr["Status"]);
                }
                menulist.ExistingImage = menulist.MenuImage;
                ViewBag.SubmitValue = "Update";
            }
            catch (Exception ex)
            {

            }
            //var Data = new {doctorDetails= doctorDetails };
            return Json(menulist);
        }
     
        /*-----------delete Menu List by Id--------*/
        [HttpPost]
        public JsonResult DeleteMenuList(int id)
        {
            string messagge;
            string Sqlquery = "DELETE FROM tblMenu WHERE id=" + id + "";
            string status = util.MultipleTransactions(Sqlquery);
            if (status == "Successfull")
            {
                messagge = "Delete Successfull!!";
                ViewBag.SubmitValue = "Save";
                //petList = ShowAllPetParent();
            }
            else
            {
                messagge = "Failed to Delete";
                //ViewData["msg"] = "Not Delete";
            }

            var Data = new { msg = messagge };
            return Json(Data);
        }
        #endregion

        #region UserRight Page
        public IActionResult UserRight()
        {
           
            BindDdlprofiletype();
            ViewBag.SubmitValue = "Save";
            return View();
        }
       [HttpPost]
        public IActionResult GetMenuListByUserRight(string clientID)
        {
            try
            {
                string sql = "exec Sp_GetMenuRights @ProfileId='" + Convert.ToInt32(clientID) + "'";
                DataSet ds = util.TableBind(sql);
                List<MenuList> menulist = new List<MenuList>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    menulist.Add(new MenuList
                    {
                        id = Convert.ToInt32(dr["MenuId"]),
                        MenuName = Convert.ToString(dr["Menuname"]),
                        View = Convert.ToString(dr["FlagView"]),
                        Add = Convert.ToString(dr["FlagAdd"]),
                        Modify = Convert.ToString(dr["FlagModify"]),
                        FlagDelete = Convert.ToString(dr["FlagDelete"]),
                        OtherName1 = Convert.ToString(dr["OtherName1"]),
                        FlagOther1 = Convert.ToString(dr["FlagOther1"]),
                        OtherName2 = Convert.ToString(dr["OtherName2"]),
                        FlagOther2 = Convert.ToString(dr["FlagOther2"]),
                        OtherName3 = Convert.ToString(dr["OtherName3"]),
                        FlagOther3 = Convert.ToString(dr["FlagOther3"]),
                    });
                }
                return Json(menulist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult SaveMenuUserRights()
        {
            try
            {
                ViewBag.SubmitValue = "Save";
            }
            catch(Exception ex)
            {

            }
            return View("UserRight");
        }

        public IActionResult ClearUserRight()
        {
            ViewBag.SubmitValue = "Save";
            return View("UserRight");
        }
        #endregion

        #region User Profile Master Page
        public IActionResult UserProfile()
        {
            ViewBag.SubmitValue = "Save";
           
            return View();
        }
        /*----------------Create and Update Record of User Profile-----------*/
        [HttpPost]
        public IActionResult UserProfileSaveUpdate(AdminUserProfile Profile)
        {
            try
            {
                string sqlquery;
                string status;
                int st_value;
                ViewBag.SubmitValue = "Save";
                if (Profile.Status == true)
                {
                    st_value = 1;
                }
                else
                {
                    st_value = 0;
                }
                if (Profile.id == 0)
                {
                    sqlquery = "Insert into tblProfile(Name,ShortName,Status,CreateDate)values('" + Profile.Name + "','" + Profile.ShortName + "','" + st_value + "',getdate())";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                       
                        ViewData["msg"] = " insert Success";
                    }
                    else
                    {
                        ViewData["msg"] = "Not insert";
                    }
                }
                else
                {
                    sqlquery = "UPDATE tblProfile SET Name = '" + Profile.Name + "', ShortName = '" + Profile.ShortName + "', Status ='" + st_value + "', ModifyDate=getdate() WHERE id=" + Profile.id + "";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        
                        ViewData["msg"] = "Update Success";
                    }
                    else
                    {
                        ViewData["msg"] = "Not Updated";
                    }
                }
            }
            catch (Exception ex)
            {


            }
            return View("UserProfile");
        }
        /*----------Show All Record on UserProfile Master Page--------*/
        [HttpGet]
        public JsonResult GetuserProfileList()
        {
            string sqlQuery;
            try
            {
                sqlQuery = "Select id ,isnull(Name,'')as Name , isnull(ShortName,'') as ShortName , isnull(Status,0)as Status From tblProfile";
                DataSet ds = util.TableBind(sqlQuery);
                List<AdminUserProfile> UserProfile = new List<AdminUserProfile>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserProfile.Add(new AdminUserProfile
                    {
                        id = Convert.ToInt32(dr["id"]),
                        Name = Convert.ToString(dr["Name"]),
                        ShortName = Convert.ToString(dr["ShortName"]),
                        Status = Convert.ToBoolean(dr["Status"]),
                    });
                }
                return Json(UserProfile);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        /*--------------Edit Record By Id On User Profile Master Page---------*/
        [HttpPost]
        public JsonResult GetUserProfileData(int id)
        {
            AdminUserProfile UserProfile = new AdminUserProfile();
            try
            {
                string sql = "SELECT id, isnull(Name,'')as Name , isnull(ShortName,'')as ShortName, isnull(Status,0)as Status FROM tblProfile WHERE id= " + id + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserProfile.id = Convert.ToInt32(dr["id"]);
                    UserProfile.Name = dr["Name"].ToString();
                    UserProfile.ShortName = dr["ShortName"].ToString();
                    UserProfile.Status = Convert.ToBoolean(dr["Status"]);
                }
                
                ViewBag.SubmitValue = "Update";
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(UserProfile);
        }

        /*-------Delete Record by id----------*/
        [HttpPost]
        public JsonResult DeleteUserProfile(int? id)
        {
            string messagge;
            string Sqlquery = "Delete  From tblProfile where id='" + id + "'";
            string status = util.MultipleTransactions(Sqlquery);
            if (status == "Successfull")
            {
                messagge = "Delete Successfull!!";
                ViewBag.SubmitValue = "Save";
                //petList = ShowAllPetParent();
            }
            else
            {
                messagge = "Failed to Delete";
                //ViewData["msg"] = "Not Delete";
            }

            var Data = new { msg = messagge };
            return Json(Data);
        }
        #endregion

        #region User Master Page (User registration)
        /*-----------------------------Create and Update User Master Record-------------------------------------------*/
        public IActionResult UserMaster()
        {
            ViewBag.SubmitValue = "Save";
           // GetAdminUserMasterList();
            BindDdlprofiletype();
            return View();
        }
        [HttpPost]
        public IActionResult UserMaster(AdminUserMaster um)
        {
            string sqlquery;
            try
            {
                //ViewData["imagePath1"]=um.Profile_Image_Name;
                ViewBag.SubmitValue = "Save";
                if (um.ProfileImage != null)
                {
                    if (um.ProfileImage.Length > 0)
                    {
                        string filename = Path.GetFileNameWithoutExtension(um.ProfileImage.FileName);
                        string extension = Path.GetExtension(um.ProfileImage.FileName);
                        um.Profile_Image_Name = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine("wwwroot/Images/", filename);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            um.ProfileImage.CopyTo(fileStream);
                        }
                    }
                }
                else
                {
                    um.Profile_Image_Name = "noPhoto.png";
                }
                //else
                //{
                //    if (um.id == 0)
                //    {

                //        um.Profile_Image_Name = "noPhoto.png";
                //    }
                //    else
                //    {
                //        um.Profile_Image_Name = ExistingImage;
                //    }
                //}
                int st_value;
                if (um.Status == true)
                {
                    st_value = 1;
                }
                else
                {
                    st_value = 0;
                }
                if (um.id == 0)
                {
                    if (um.Password == um.ReEnterPassword)
                    {
                        if (um.Password.Length == 8)
                        {
                            sqlquery = "Insert into tbluser(Firstname,LastName,Designation,Place_of_Posting,Profile_Image,Mobile_No,Telephone_No,Email,Password,CreateDate,Status,profiletype)values('" + um.Firstname + "','" + um.LastName + "','" + um.Designation + "','" + um.Place_of_Posting + "','" + um.Profile_Image_Name + "','" + um.Mobile_No + "','" + um.Telephone_No + "','" + um.Email + "','" + util.cryption(um.Password.Trim()) + "',getdate(),'" + st_value + "','" + um.profiletype + "')";
                            string status = util.MultipleTransactions(sqlquery);
                            if (status == "Successfull")
                            {
                                ModelState.Clear();
                                ViewData["msg"] = " insert Success";

                            }

                            else
                            {
                                ViewData["msg"] = "Not insert";
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

                }
                else
                {
                    if (um.ProfileImage != null)
                    {
                        sqlquery = "UPDATE tbluser SET Firstname = '" + um.Firstname + "', LastName = '" + um.LastName + "', Designation ='" + um.Designation + "', Place_of_Posting='" + um.Place_of_Posting + "',Profile_Image='" + um.Profile_Image_Name + "' ,Mobile_No='" + um.Mobile_No + "',Telephone_No='" + um.Telephone_No + "',Email='" + um.Email + "',Password='" + util.cryption(um.Password.Trim()) + "',ModifyDate=getdate(),Status='" + st_value + "',profiletype='" + um.profiletype + "' WHERE id=" + um.id + " ";

                    }
                    else
                    {
                        sqlquery = "UPDATE tbluser SET Firstname = '" + um.Firstname + "', LastName = '" + um.LastName + "', Designation ='" + um.Designation + "', Place_of_Posting='" + um.Place_of_Posting + "',Profile_Image='" + um.Profile_Image_Name + "' ,Mobile_No='" + um.Mobile_No + "',Telephone_No='" + um.Telephone_No + "',Email='" + um.Email + "',Password='" + util.cryption(um.Password.Trim()) + "',ModifyDate=getdate(),Status='" + st_value + "',profiletype='" + um.profiletype + "' WHERE id=" + um.id + " ";
                    }

                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewData["msg"] = " Update Success";
                       

                    }
                    else
                    {
                        ViewData["msg"] = "Not Update";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        /*----------------------------------Show All Record on User Master Page ----------------------------------*/
        [HttpGet]
        public JsonResult GetUserMasterList()
        {
            string sqlQuery;
            try
            {
                sqlQuery = "SELECT id, Firstname, LastName,Designation,Place_of_Posting,Profile_Image,Mobile_No from tbluser";
                DataSet ds = util.TableBind(sqlQuery);
                List<AdminUserMaster> UserMaster = new List<AdminUserMaster>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserMaster.Add(new AdminUserMaster
                    {
                        id = Convert.ToInt32(dr["id"]),
                        Firstname = Convert.ToString(dr["Firstname"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Designation = Convert.ToString(dr["Designation"]),
                        Place_of_Posting = Convert.ToString(dr["Place_of_Posting"]),
                        Profile_Image_Name = Convert.ToString(dr["Profile_Image"]),
                        Mobile_No = Convert.ToString(dr["Mobile_No"]),
                    });
                }
                return Json(UserMaster);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /*-------------------------For Edit A/c to Id--------------------------------*/
        [HttpPost]
        public JsonResult EditUserById(int id)
        {
            AdminUserMaster usermaster = new AdminUserMaster();
            int status = 0;
            try
            {
                string sql = "Select id, isnull(Firstname,'')as Firstname , isnull(LastName,'')as LastName, isnull(Designation,'')as Designation,isnull(Place_of_Posting,'')as Place_of_Posting, isnull(Profile_Image,'')as Profile_Image ,isnull( Mobile_No,'')as Mobile_No , isnull(Telephone_No,'') as Telephone_No , isnull( Email,'')as Email , isnull(profiletype,'')as profiletype, isnull(username,'')as username,isnull(Password,'')as Password, isnull(Status,0)as Status FROM tbluser WHERE id= '" + id + "'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    usermaster.id = Convert.ToInt32(dr["id"]);
                    usermaster.Firstname = dr["Firstname"].ToString();
                    usermaster.LastName = dr["LastName"].ToString();
                    usermaster.Designation = dr["Designation"].ToString();
                    usermaster.Place_of_Posting = dr["Place_of_Posting"].ToString();
                    usermaster.Profile_Image_Name = dr["Profile_Image"].ToString();
                    usermaster.Mobile_No = dr["Mobile_No"].ToString();
                    usermaster.Telephone_No = dr["Telephone_No"].ToString();
                    usermaster.Email = dr["Email"].ToString();
                    usermaster.profiletype = dr["profiletype"].ToString();
                    //usermaster.username = dr["username"].ToString();
                    usermaster.Password = dr["Password"].ToString();
                    usermaster.Status = Convert.ToBoolean(dr["Status"]);
                }
                usermaster.ExistingImage = usermaster.Profile_Image_Name;
                ViewBag.SubmitValue = "Update";
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(usermaster);
        }
      

        /*-------------------------For Single Record Delete A/C Id----------------------------------------*/
        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            string messagge;
            string Sqlquery = "DELETE FROM tbluser WHERE id=" + id + "";
            string status = util.MultipleTransactions(Sqlquery);
            if (status == "Successfull")
            {
                messagge = "Delete Successfull!!";
                ViewBag.SubmitValue = "Save";
                //petList = ShowAllPetParent();
            }
            else
            {
                messagge = "Failed to Delete";
                //ViewData["msg"] = "Not Delete";
            }

            var Data = new { msg = messagge };
            return Json(Data);
        }

        /*---------------------For Clear-----------------------*/
        public IActionResult clear()
        {
            ViewBag.SubmitValue = "Save";
            //GetAdminUserMasterList();
            return View("UserMaster");
        }
        /*-----------For Bind DropDown User Profile ----------*/

        public IActionResult BindDdlprofiletype()
        {
            string query = "select id,Name from tblProfile";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> Websitelist = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                Websitelist.Add(new SelectListItem { Text = dr1["Name"].ToString(), Value = dr1["id"].ToString() });
            }
            ViewBag.Websitelist = Websitelist;
            return View();
        }
        #endregion

        

    }
}
