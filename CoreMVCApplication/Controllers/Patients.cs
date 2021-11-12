using CoreMVCApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Razor;
using System.Data.OleDb;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Windows.Input;
using QRCoder;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace CoreMVCApplication.Controllers
{
    public class Patients : Controller
    {
        //string petparentid;
        ClsUtility util = new ClsUtility();
        
        string finalStatus="";
        string msg = "";
        string finalMsg = "";
       // string query = "";
        string sqlquery;
        string QRdetails = "";
        DataTable dt = new DataTable();
        public IActionResult Index()
        {
            return View();
        }
        #region Dashboard
        public IActionResult Dashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            return View();
        }
        #endregion

        #region Patinet
        public IActionResult Patient()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            PopulatePetBreed();
            PopulatedGender();
            return View();
        }

        public IActionResult addpatient(string pet_id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            PopulatePetBreed();
            PopulatePetType();
            PopulatePetColour();
            PopulatedGender();

            Patient patient = new Patient();
            patient.Pet_Image = "noPhoto.png";
            string sql = "select mspp.PetParent_Id, mspp.FullName,mspp.Gender,mspp.Mobile_no,mspp.Email,mspp.Address,mspp.Notes, mpet.Pet_id, mpet.petName,mpet.PetParent_Id,mpet.Breed,mpet.PetType, mpet.Pet_Image,mpet.PetGender,mpet.Pet_DOB, mpet.Pet_Weight,mpet.Colour,mpet.AdoptedOn,mpet.MicrochipNo,mpet.DateOfChipping, (mpet.Notes)as PetNotes,mpet.Spay_Meutered,mpet.Loc_Chipping,mpet.Insurance  from Mst_PetParent mspp join MST_pet mpet On mpet.PetParent_Id = mspp.PetParent_Id where mpet.Pet_id = '" + pet_id + "'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    patient.PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]);
                    patient.FullName = dr["FullName"].ToString();
                    patient.Gender = dr["Gender"].ToString();
                    patient.Mobile_no = dr["Mobile_no"].ToString();
                    patient.Email = dr["Email"].ToString();
                    patient.Address = dr["Address"].ToString();
                    patient.Notes = dr["Notes"].ToString();

                    patient.Pet_id = Convert.ToInt32(dr["Pet_id"]);
                    patient.PetName = dr["petName"].ToString();
                    patient.Breed = dr["Breed"].ToString();
                    patient.PetType = dr["PetType"].ToString();
                    patient.PetGender = dr["PetGender"].ToString();
                    patient.Pet_DOB = dr["Pet_DOB"].ToString();
                    patient.Pet_Weight = Convert.ToInt32(dr["Pet_Weight"]);
                    patient.Colour = dr["Colour"].ToString();
                    patient.Pet_Image = dr["Pet_Image"].ToString();

                patient.AdoptedOn = dr["AdoptedOn"].ToString();
                    patient.MicrochipNo = dr["MicrochipNo"].ToString();
                    patient.DateOfChipping = dr["DateOfChipping"].ToString();
                    patient.PetNotes = dr["PetNotes"].ToString();

                    patient.Spay_Meutered = dr["Spay_Meutered"].ToString();
                    patient.Loc_Chipping = dr["Loc_Chipping"].ToString();
                    patient.Insurance = dr["Insurance"].ToString();

                }
            
            //var Data = new {petlist= petParent };
            //return View("addpatient", patient);
            // return RedirectToAction("addpatient", patient);
            return View("addpatient",patient);
        }
        
        [HttpPost]
        public IActionResult SavePatient(Patient patient)
        {
            string UserId = HttpContext.Session.GetString("userId");
            int status_value;
            try
            {
                if (patient.PetImage != null)
                {
                    if (patient.PetImage.Length > 0)
                    {
                        string filename = Path.GetFileNameWithoutExtension(patient.PetImage.FileName);
                        string extension = Path.GetExtension(patient.PetImage.FileName);
                        patient.Pet_Image = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine("wwwroot/Images/", filename);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            patient.PetImage.CopyTo(fileStream);
                        }
                    }
                }
                else
                {
                    patient.Pet_Image = "noPhoto.png";
                }
                //if (petparent.Status == true)
                //{
                //    status_value = 1;
                //}
                //else
                //{
                //    status_value = 0;
                //}
                if (patient.PetParent_Id == 0)
                {
                    DataTable dt = util.SelectParticular("Mst_PetParent", "*", "Mobile_no='" + patient.Mobile_no + "'");
                    if (dt.Rows.Count > 0)
                    {
                        ViewBag.ErrorMsg = "This mobile number is already Exit";
                    }
                    else
                    {
                        ViewBag.SubmitValue = "SAVE";
                        sqlquery = "Insert into Mst_PetParent(FullName,Gender,Mobile_no,Email,Address,Notes,CreateUser,CreateDate,City,area,pincode)values('" + patient.FullName + "','" + patient.Gender + "','" + patient.Mobile_no + "','" + patient.Email + "','" + patient.Address + "','" + patient.Notes + "','" + UserId + "',getdate(),'"+ patient.city + "','"+patient.area+"','"+patient.pincode+"')";


                        string status = util.MultipleTransactions(sqlquery);
                        if (status == "Successfull")
                        {
                            dt = util.execQuery("Select max(PetParent_Id) from Mst_PetParent ");
                            sqlquery = util.MultipleTransactions("Insert into Mst_Pet(PetParent_Id,PetName,Breed,PetType,Pet_Image,PetGender,Pet_DOB,Pet_Weight,Colour,AdoptedOn,MicrochipNo,DateOfChipping,Notes," +
                            "CreateUser,CreateDate)values('" + dt.Rows[0][0].ToString() + "','" + patient.PetName + "','" + patient.Breed + "','" + patient.PetType + "','" + patient.Pet_Image + "','" + patient.PetGender + "','" + patient.Pet_DOB + "','" + patient.Pet_Weight + "','" + patient.Colour + "','" + patient.AdoptedOn + "','" + patient.MicrochipNo + "','" + patient.DateOfChipping + "','" + patient.PetNotes + "','" + UserId + "',getdate())");
                            ModelState.Clear();
                            ViewBag.Message = "Patient added successfully.";
                            msg ="Patient added successfully";
                        }
                        else
                        {
                            ViewBag.Message = "Patient not added.";
                        }
                    }
                }
                /*  else
                  {
                      //ViewBag.SubmitValue = "UPDATE";
                      sqlquery += "UPDATE Mst_PetParent SET FullName = '" + patient.FullName + "', Gender = '" + patient.Gender + "', Mobile_no ='" + patient.Mobile_no + "', Email='" + patient.Email + "',Address='" + patient.Address + "' ,Notes='" + patient.Notes + "',ModifyUser='" + "" + "',ModifyDate=getdate() where PetParent_Id=" + patient.PetParent_Id + "";
                      if (patient.PetImage != null)
                      {
                          sqlquery = "UPDATE Mst_Pet SET PetName = '" + patient.PetParent + "', Bread = '" + patient.Breed + "', PetType ='" + patient.PetType + "', Pet_Image='" + patient.Pet_Image + "',PetGender='" + patient.PetGender + "' ,Pet_DOB='" + patient.Pet_DOB + "',Pet_Weight='" + patient.Pet_Weight + "',Colour='" + patient.Colour + "',AdoptedOn='" + patient.AdoptedOn + "',MicrochipNo='" + patient.MicrochipNo + "',DateOfChipping='" + patient.DateOfChipping + "',Notes='" + patient.Notes + "', ModifyUser='" + "" + "',ModifyDate=getdate() WHERE Pet_id=" + patient.Pet_id + " ";

                      }
                      else
                      {
                          sqlquery = "UPDATE Mst_Pet SET PetName = '" + patient.PetName + "', Bread = '" + patient.Breed + "', PetType ='" + patient.PetType + "', Pet_Image='" + patient.ExistingImage + "',PetGender='" + patient.PetGender + "' ,Pet_DOB='" + patient.Pet_DOB + "',Pet_Weight='" + patient.Pet_Weight + "',Colour='" + patient.Colour + "',AdoptedOn='" + patient.AdoptedOn + "',MicrochipNo='" + patient.MicrochipNo + "',DateOfChipping='" + patient.DateOfChipping + "',Notes='" + patient.Notes + "', ModifyUser='" + "" + "',ModifyDate=getdate() WHERE Pet_id=" + patient.Pet_id + " ";
                      }
                      string status = util.MultipleTransactions(sqlquery);
                      if (status == "Successfull")
                      {
                          ModelState.Clear();
                          ViewData["msg"] = " Update Success";
                          //ShowAllPetParent();
                      }
                      else
                      {
                          ViewData["msg"] = "Not Update";
                      }
                  }*/
            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
                throw;
            }
            finalStatus =
          "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> " , "" , " Select query", "addpatient.cshtml", "", "App", "Function Name ===>'SavePatient'");

            return RedirectToAction("Patient", "Patients");
        }
        [HttpGet]
        public JsonResult ShowPatientRecord()
        {
            string sql;
            sql = "select mpp.PetParent_Id , mpp.FullName,mpet.PetName,mpet.petGender,pb.PetBreedName,mpet.Pet_id ,mg.GenderName from Mst_PetParent mpp Join MST_pet mpet On mpp.PetParent_Id = mpet.PetParent_Id Join PetBreed pb on pb.BreedId = mpet.Breed join MST_Gender mg on mg.GenderId = mpet.petGender order by PetParent_Id desc";
            DataSet ds = util.TableBind(sql);
            List<Patient> patient = new List<Patient>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                patient.Add(new Patient
                {
                    PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]),
                    Pet_id = Convert.ToInt32(dr["Pet_id"]),
                    PetName = Convert.ToString(dr["PetName"]),
                    FullName = Convert.ToString(dr["FullName"]),
                    PetGender = Convert.ToString(dr["GenderName"]),
                    Breed = Convert.ToString(dr["PetBreedName"])
                }); 
            }
            return Json(patient);
        }
        [HttpPost]
        public IActionResult EditByPatientId(string pet_id)
        {
            Patient patient = new Patient();
            
                string sql = "select mspp.PetParent_Id, mspp.FullName,mspp.Gender,mspp.Mobile_no,mspp.Email,mspp.Address,mspp.Notes, mpet.Pet_id, mpet.petName,mpet.PetParent_Id,mpet.Breed,mpet.PetType, mpet.Pet_Image,mpet.PetGender,mpet.Pet_DOB, mpet.Pet_Weight,mpet.Colour,mpet.AdoptedOn,mpet.MicrochipNo,mpet.DateOfChipping, (mpet.Notes)as PetNotes,mpet.Spay_Meutered,mpet.Loc_Chipping,mpet.Insurance  from Mst_PetParent mspp join MST_pet mpet On mpet.PetParent_Id = mspp.PetParent_Id where mpet.Pet_id = '" + pet_id+"'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    patient.PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]);
                    patient.FullName = dr["FullName"].ToString();
                    patient.Gender = dr["Gender"].ToString();
                    patient.Mobile_no = dr["Mobile_no"].ToString();
                    patient.Email = dr["Email"].ToString();
                    patient.Address = dr["Address"].ToString();
                    patient.Notes = dr["Notes"].ToString();

                    patient.Pet_id = Convert.ToInt32(dr["Pet_id"]);
                    patient.PetName = dr["petName"].ToString();
                    patient.Breed = dr["Breed"].ToString();
                    patient.PetType = dr["PetType"].ToString();
                    patient.PetGender = dr["PetGender"].ToString();
                    patient.Pet_DOB = dr["Pet_DOB"].ToString();
                    patient.Pet_Weight = Convert.ToInt32(dr["Pet_Weight"]);
                    patient.Colour = dr["Colour"].ToString();

                    patient.AdoptedOn = dr["AdoptedOn"].ToString();
                    patient.MicrochipNo = dr["MicrochipNo"].ToString();
                    patient.DateOfChipping = dr["DateOfChipping"].ToString();
                    patient.PetNotes = dr["PetNotes"].ToString();

                    patient.Spay_Meutered = dr["Spay_Meutered"].ToString();
                    patient.Loc_Chipping = dr["Loc_Chipping"].ToString();
                    patient.Insurance = dr["Insurance"].ToString();
                    
                }
                ViewBag.SubmitValue = "Update";

            //var Data = new {petlist= petParent };
            //return View("addpatient", patient);
            return RedirectToAction("addpatient", patient);
        }
        [HttpPost]
        public  JsonResult DeletePatientById(string pet_id)
        {
            string messagge = string.Empty;
            string Sqlquery = "delete from MST_Pet where Pet_id = '" + pet_id + "'";
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

        #region Add Pet Parent
        public IActionResult PetParent(string petParent_Id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            PopulatedGender();
            PetParent petParent = new PetParent();
           
                string sql = " select PetParent_Id,FullName,Gender,Mobile_no,Email,Address,Notes from Mst_PetParent where PetParent_Id= " + petParent_Id + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    petParent.PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]);
                    petParent.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                    petParent.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                    petParent.Mobile_no = ds.Tables[0].Rows[0]["Mobile_no"].ToString();
                    petParent.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    petParent.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    petParent.Notes = ds.Tables[0].Rows[0]["Notes"].ToString();
                }
            return View("PetParent", petParent);
        }
        [HttpPost]
        public IActionResult PetParent(PetParent petparent)
        {
            
            int status_value;
            try
            {
                if (petparent.Status == true)
                {
                    status_value = 1;
                }
                else
                {
                    status_value = 0;
                }
                if (petparent.PetParent_Id == 0)
                {
                    DataTable dt = util.SelectParticular("Mst_PetParent", "*", "Mobile_no='" + petparent.Mobile_no + "'");
                    if (dt.Rows.Count > 0)
                    {
                        ViewBag.ErrorMsg = "This mobile number is already Exit";
                    }
                    else
                    {
                        ViewBag.SubmitValue = "SAVE";
                        sqlquery = "Insert into Mst_PetParent(FullName,Gender,Mobile_no,Email,Address,Notes,CreateUser,CreateDate,Status)values('" + petparent.FullName + "','" + petparent.Gender + "','" + petparent.Mobile_no + "','" + petparent.Email + "','" + petparent.Address + "','" + petparent.Notes + "','" + "" + "',getdate(),'" + status_value + "')";
                        string status = util.MultipleTransactions(sqlquery);
                        if (status == "Successfull")
                        {
                            ModelState.Clear();
                            ViewData["msg"] = "Pet parent added successfully.";
                            ShowAllPetParent();
                        }
                        else
                        {
                            ViewData["msg"] = "Pet parent not added.";
                        }
                    }
                    //HttpContext.Session.SetString("petParentId", petparent.PetParent_Id.ToString());
                    //petparentid = HttpContext.Session.GetString("petParentId");
                }
                else
                {
                    sqlquery = "UPDATE Mst_PetParent SET FullName = '" + petparent.FullName + "', Gender = '" + petparent.Gender + "', Mobile_no ='" + petparent.Mobile_no + "', Email='" + petparent.Email + "',Address='" + petparent.Address + "' ,Notes='" + petparent.Notes + "',Status='" + status_value + "',ModifyUser='" + "" + "',ModifyDate=getdate() where PetParent_Id=" + petparent.PetParent_Id + "";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewData["msg"] = "Pet parent modify successfully.";
                    }
                    else
                    {
                        ViewData["msg"] = "Pet parent not modify.";
                    }
                }
            }
            catch (Exception ex)
            {

                finalMsg = ex.Message;
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "PetParent.cshtml", "", "App", "Function Name ===>'PetParent'");
            return View();
        }

        [HttpPost]
        public JsonResult ShowAllPetParent()
        {
            
                string sql = "select PetParent_Id, isnull(FullName,'')as FullName ,isnull(Gender,'') as Gender ,isnull(Mobile_no,'')as Mobile_no ,isnull(Email,'') as Email , isnull(Address,'') as Address , isnull(Notes,'')as Notes ,isnull(Status,0) as Status  from Mst_PetParent";
                DataSet ds = util.TableBind(sql);
                List<PetParent> petList = new List<PetParent>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    petList.Add(new PetParent
                    {
                        PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]),
                        FullName = Convert.ToString(dr["FullName"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        Mobile_no = Convert.ToString(dr["Mobile_no"]),
                        Email = Convert.ToString(dr["Email"]),
                        Address = Convert.ToString(dr["Address"]),
                        Notes = Convert.ToString(dr["Notes"]),
                        Status = Convert.ToBoolean(dr["Status"])
                    });
                }
                return Json(petList);
           
        }
        [HttpPost]
        public JsonResult DeletePetParentById(string ppid)
        {
            string messagge = string.Empty;

            List<PetParent> petList = new List<PetParent>();
            string Sqlquery = "Delete From Mst_PetParent where PetParent_Id='" + ppid.ToString() + "'";
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
        [HttpPost]
        public IActionResult EditRecordById(string ppid)
        {
            PetParent petParent = new PetParent();
          
                string sql = "select PetParent_Id, isnull(FullName,'')as FullName ,isnull(Gender,'') as Gender ,isnull(Mobile_no,'')as Mobile_no ,isnull(Email,'') as Email , isnull(Address,'') as Address , isnull(Notes,'')as Notes ,isnull(Status,0) as Status  from Mst_PetParent WHERE PetParent_Id= " + ppid + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    petParent.PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]);
                    petParent.FullName = dr["FullName"].ToString();
                    petParent.Gender = dr["Gender"].ToString();
                    petParent.Mobile_no = dr["Mobile_no"].ToString();
                    petParent.Email = dr["Email"].ToString();
                    petParent.Address = dr["Address"].ToString();
                    petParent.Notes = dr["Notes"].ToString();
                    petParent.Status = Convert.ToBoolean(dr["Status"]);
                }
                
            //var Data = new {petlist= petParent };
            return Json(petParent);
        }
        #endregion

        #region Add Pet 
        public IActionResult Pet(int pet_id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            PopulatePetBreed();
            PopulatePetType();
            PopulatePetColour();
            PopulatePetParentName();
            PopulatedGender();
            ViewBag.SubmitValue = "Save";
            string sql;
            PetDetails patient = new PetDetails();
            try
            {
                sql = "select mpet.Pet_id,mspp.PetParent_Id, mpet.petName,mpet.PetParent_Id,mpet.Breed,mpet.PetType, mpet.Pet_Image,mpet.PetGender,mpet.Pet_DOB, mpet.Pet_Weight,mpet.Colour,mpet.AdoptedOn,mpet.MicrochipNo,mpet.DateOfChipping, (mpet.Notes)as PetNotes,mpet.Spay_Meutered,mpet.Loc_Chipping,mpet.Insurance  from Mst_PetParent mspp join MST_pet mpet On mpet.PetParent_Id = mspp.PetParent_Id where mpet.Pet_id = '" + pet_id + "'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    patient.PetParent = Convert.ToInt32(dr["PetParent_Id"]);
                    patient.PetName = dr["petName"].ToString();
                    patient.Pet_Image = dr["Pet_Image"].ToString();
                    patient.Pet_id = Convert.ToInt32(dr["Pet_id"]);
                    patient.PetGender = dr["PetGender"].ToString();
                    patient.Colour = dr["Colour"].ToString();
                    patient.Breed = dr["Breed"].ToString();
                    patient.PetType = dr["PetType"].ToString();
                    patient.Pet_Weight = Convert.ToInt32(dr["Pet_Weight"]);
                    patient.AdoptedOn = dr["AdoptedOn"].ToString();
                    patient.Pet_DOB = dr["Pet_DOB"].ToString();
                    patient.MicrochipNo = dr["MicrochipNo"].ToString();
                    patient.DateOfChipping = dr["DateOfChipping"].ToString();
                    patient.Spay_Meutered = dr["Spay_Meutered"].ToString();
                    patient.Loc_Chipping = dr["Loc_Chipping"].ToString();
                    patient.Insurance = dr["Insurance"].ToString();
                    patient.PetNotes = dr["PetNotes"].ToString();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(patient);
            // return View(petparentid);

        }
        [HttpPost]
        public IActionResult Pet(PetDetails details)
        
        {
            string sqlquery;
            int st_value;
            try
            {
                ViewBag.SubmitValue = "SAVE";
                if (details.PetImage != null)
                {
                    if (details.PetImage.Length > 0)
                    {
                        string filename = Path.GetFileNameWithoutExtension(details.PetImage.FileName);
                        string extension = Path.GetExtension(details.PetImage.FileName);
                        details.Pet_Image = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine("wwwroot/Images/", filename);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            details.PetImage.CopyTo(fileStream);
                        }
                    }
                }
                else
                {
                    details.Pet_Image = "noPhoto.png";
                }
                if (details.Status == true)
                {
                    st_value = 1;
                }
                else
                {
                    st_value = 0;
                }
                if (details.Pet_id == 0)
                {
                    sqlquery = "Insert into Mst_Pet(PetParent_Id,PetName,Breed,PetType,Pet_Image,PetGender,Pet_DOB,Pet_Weight,Colour,AdoptedOn,MicrochipNo,DateOfChipping,Notes," +
                        "CreateUser,CreateDate,Status,Spay_Meutered,Loc_Chipping,Insurance)values('" + details.PetParent + "','" + details.PetName + "','" + details.Breed + "','" + details.PetType + "','" + details.Pet_Image + "','" + details.PetGender + "','" + details.Pet_DOB + "','" + details.Pet_Weight + "','" + details.Colour + "','" + details.AdoptedOn + "','" + details.MicrochipNo + "','" + details.DateOfChipping + "','" + details.Notes + "','" + "" + "',getdate(),'" + st_value + "','"+details.Spay_Meutered+"','"+details.Loc_Chipping+"','"+details.Insurance+"')";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Pet details added successfully.";
                    }
                    else
                    {
                        ViewBag.Message = "Pet details not added.";
                    }
                }
                else
                {
                    ViewBag.SubmitValue = "UPDATE";
                    if (details.PetImage != null)
                    {
                        sqlquery = "UPDATE Mst_Pet SET PetName = '" + details.PetName + "', Breed = '" + details.Breed + "', PetType ='" + details.PetType + "', Pet_Image='" + details.Pet_Image + "',PetGender='" + details.PetGender + "' ,Pet_DOB='" + details.Pet_DOB + "',Pet_Weight='" + details.Pet_Weight + "',Colour='" + details.Colour + "',AdoptedOn='" + details.AdoptedOn + "',MicrochipNo='" + details.MicrochipNo + "',DateOfChipping='" + details.DateOfChipping + "',Notes='" + details.Notes + "',Spay_Meutered='" + details.Spay_Meutered + "', Loc_Chipping='" + details.Loc_Chipping + "',Insurance='"+details.Insurance+"', ModifyUser='" + "" + "',ModifyDate=getdate(),Status='" + st_value + "' WHERE Pet_id=" + details.Pet_id + " ";

                    }
                    else
                    {
                        sqlquery = "UPDATE Mst_Pet SET PetName = '" + details.PetName + "', Breed = '" + details.Breed + "', PetType ='" + details.PetType + "', Pet_Image='" + details.ExistingImage + "',PetGender='" + details.PetGender + "' ,Pet_DOB='" + details.Pet_DOB + "',Pet_Weight='" + details.Pet_Weight + "',Colour='" + details.Colour + "',AdoptedOn='" + details.AdoptedOn + "',MicrochipNo='" + details.MicrochipNo + "',DateOfChipping='" + details.DateOfChipping + "',Notes='" + details.Notes + "',Spay_Meutered='" + details.Spay_Meutered + "', Loc_Chipping='" + details.Loc_Chipping + "',Insurance='" + details.Insurance + "', ModifyUser='" + "" + "',ModifyDate=getdate(),Status='" + st_value + "' WHERE Pet_id=" + details.Pet_id + " ";
                    }
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Pet details modify successfully.";
                    }
                    else
                    {
                        ViewBag.Message = "Pet details not modify.";
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View();

        }
        [HttpPost]
        public IActionResult ShowAllPetRecord()
        {
            List<PetDetails> petDetails = new List<PetDetails>();
            try
            {
                string sql = "select Pet_id, isnull(pet.PetName,'') as PetName, isnull(pet.Breed,'') as Breed, isnull(pet.PetType,'') as PetType , isnull(pet.PetGender,'') as PetGender ,isnull(pet.Pet_DOB,'') as Pet_DOB ,isnull(pet.Pet_Weight,0) as Pet_Weight ,isnull(pet.Status,0) as Status , isnull(pb.PetBreedName,'') as PetBreedName , isnull(pty.PetTypeName,'')as PetTypeName, isnull(gen.GenderName,'')as GenderName from Mst_Pet pet join PetBreed pb on pb.BreedId = pet.Breed join PetType pty on pty.PetTypeId = pet.PetType join MST_Gender gen on gen.GenderId=pet.PetGender order by Pet_id desc";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    petDetails.Add(new PetDetails
                    {
                        Pet_id = Convert.ToInt32(dr["Pet_id"]),
                        PetName = Convert.ToString(dr["PetName"]),
                        Breed = Convert.ToString(dr["PetBreedName"]),
                        PetType = Convert.ToString(dr["PetTypeName"]),
                        PetGender = Convert.ToString(dr["GenderName"]),
                        Pet_DOB = Convert.ToString(dr["Pet_DOB"]),
                        Pet_Weight = Convert.ToInt32(dr["Pet_Weight"]),
                        Status = Convert.ToBoolean(dr["Status"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(petDetails);
        }
        [HttpPost]
        public JsonResult DeletePetRecordById(string petid)
        {
            string messagge = string.Empty;

            List<PetDetails> petDetails = new List<PetDetails>();
            string Sqlquery = "Delete From Mst_Pet where Pet_id='" + petid.ToString() + "'";
            string status = util.MultipleTransactions(Sqlquery);
            if (status == "Successfull")
            {
                messagge = "Delete Successfull!!";
                ViewBag.SubmitValue = "SAVE";
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
        [HttpPost]
        public JsonResult EditPetRecordById(string petid)
        {
            PetDetails petDetails = new PetDetails();
            try
            {
                string sql = "select Pet_id,	isnull(PetParent_Id,0) as PetParent_Id,	isnull(PetName,'') as PetName, 	isnull(Breed,'') as Breed , 	isnull(PetType,'') as PetType,	isnull(Pet_Image,'') as Pet_Image,	isnull(PetGender,'') as PetGender,	isnull(Pet_DOB,'') as Pet_DOB,	isnull(Pet_Weight,0) as Pet_Weight,	isnull(Colour,'') as Colour,	isnull(AdoptedOn,'') as AdoptedOn,isnull(MicrochipNo,'') as MicrochipNo,	isnull(DateOfChipping,'') as DateOfChipping, 	isnull(Notes,'') as Notes,	isnull(Status,0) as Status  from Mst_Pet  WHERE Pet_id= " + petid + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    petDetails.Pet_id = Convert.ToInt32(dr["Pet_id"]);
                    petDetails.PetParent = Convert.ToInt32(dr["PetParent_Id"]);
                    petDetails.PetName = dr["PetName"].ToString();
                    petDetails.Breed = dr["Breed"].ToString();
                    petDetails.PetType = dr["PetType"].ToString();
                    petDetails.Pet_Image = dr["Pet_Image"].ToString();
                    petDetails.PetGender = dr["PetGender"].ToString();
                    petDetails.Pet_DOB = dr["Pet_DOB"].ToString();
                    petDetails.Pet_Weight = Convert.ToInt32(dr["Pet_Weight"]);
                    petDetails.Colour = dr["Colour"].ToString();
                    petDetails.AdoptedOn = dr["AdoptedOn"].ToString();
                    petDetails.MicrochipNo = dr["MicrochipNo"].ToString();
                    petDetails.DateOfChipping = dr["DateOfChipping"].ToString();
                    petDetails.Notes = dr["Notes"].ToString();
                    petDetails.Status = Convert.ToBoolean(dr["Status"]);
                }
                petDetails.ExistingImage = petDetails.Pet_Image;
            }
            catch (Exception ex)
            {

            }
            ViewBag.SubmitValue = "UPDATE";
            // var Data = new { petlist = petDetails };
            return Json(petDetails);
        }
        [HttpGet]
        public JsonResult GetPetBreed(string id)
        {
            List<PetDetails> PetDetails = new List<PetDetails>();
            //PatientAppointment Appointment = new PatientAppointment();
            try
            {
                string sql = "exec Sp_Sel_PetBreed_by_PetType '"+id+"'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PetDetails.Add(new PetDetails
                    {
                        BreedId = Convert.ToInt32(dr["BreedId"]),
                        Breed = dr["PetBreedName"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(PetDetails);
        }
        #endregion

        #region Appointment
        public IActionResult PetAppointment(PatientAppointment appointment)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            PopulateAppointmentTime();
            PopulateAppointmentStatus();
            PopulateAppointmentType();
            PopulatePetBreed();
            PopulatePetType();
            PopulateDoctorName();
            // PopulatePetParentName();
            //PopulatePet();
            ViewBag.SubmitValue = "SAVE";
            return View();
        }
        [HttpPost]
        public IActionResult SaveNewAppointment(PatientAppointment appointment)
        {
            string UserId = HttpContext.Session.GetString("userId");
            DataTable dt = new DataTable();
           // string sqlquery ;
            int status_value;
            
            try
            {
                ViewBag.SubmitValue = "SAVE";
                //if (appointment.Status == true)
                //{
                //    status_value = 1;
                //}
                //else
                //{
                //    status_value = 0;
                //}
                if (appointment.Appointment_id == 0)
                {
                    sqlquery = "Insert into trn_PetAppointment(Pet_Id,BreadId,PetTypeId,PetS_ParentName,Appointment_status,Appointment_Date,Appointment_Type,Appointment_Time,Appointment_Notes,CreateUser,CreateDate,PetParent_Id,DoctorId,Dr_Fees)values('" + appointment.PetName + "','" + appointment.BreadId + "','" + appointment.PetType + "','" + appointment.PetS_ParentName + "','" + appointment.Appointment_status + "',convert(varchar(15),'" + appointment.Appointment_Date + "',103),'" + appointment.Appointment_Type + "','" + appointment.Appointment_Time + "','" + appointment.Appointment_Notes + "','" + UserId + "',convert(datetime,'" + System.DateTime.Now.ToString() + "',103),'" + appointment.PetParent_Id+"','"+appointment.doctorId+"' , '"+appointment.doctor_Fees+"')";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Appointment added successFully.";
                        msg= "Appointment added successFully.";
                    }
                    else
                    {
                        ViewBag.Message = "Appointment not added.";
                        msg = "Appointment not added.";
                    }
                }
                else
                {
                    sqlquery = "UPDATE trn_PetAppointment SET Pet_Id = '" + appointment.PetName + "', BreadId = '" + appointment.BreadId+ "', PetTypeId ='" + appointment.PetType + "', PetS_ParentName='" + appointment.PetS_ParentName + "',Appointment_status='" + appointment.Appointment_status + "' ,Appointment_Date='" + appointment.Appointment_Date + "',Appointment_Type='" + appointment.Appointment_Type + "',Appointment_Time='" + appointment.Appointment_Time + "',Appointment_Notes='" + appointment.Appointment_Notes + "',ModifyUser='" + UserId + "',ModifyDate=getdate(), PetParent_Id='"+appointment.PetParent_Id+ "',DoctorId='"+appointment.doctorId+ "', Dr_Fees='"+appointment.doctor_Fees+"'  where Appointment_id=" + appointment.Appointment_id + "";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        dt = util.execQuery("Select max(Appointment_id) from trn_PetAppointment ");
                        sqlquery = util.MultipleTransactions("Insert into His_trn_PetAppointment(Appointment_id,PetName, BreadId, PetTypeId, PetS_ParentName, Appointment_status, Appointment_Date, Appointment_Type, Appointment_Time, Appointment_Notes, CreateUser, ModifyDate, PetParent_Id, DoctorId, Dr_Fees)values('" + dt.Rows[0][0].ToString() + "','" + appointment.PetName + "','" + appointment.Breed + "','" + appointment.PetType + "','" + appointment.PetS_ParentName + "','" + appointment.Appointment_status + "','" + (appointment.Appointment_Date) + "','" + appointment.Appointment_Type + "','" + appointment.Appointment_Time + "','" + appointment.Appointment_Notes + "','" + UserId + "',getdate(),'" + appointment.PetParent_Id + "','" + appointment.doctorId + "' , '" + appointment.doctor_Fees + "')");
                        ModelState.Clear();
                        ViewBag.Message = "Appointment modify successFully.";
                        //ShowAllPetParent();
                    }
                    else
                    {
                        ViewBag.Message = "Appointment not modify.";
                    }
                }
            }
            catch (Exception ex)
            {

                finalMsg = ex.Message;
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "PetAppointment.cshtml", "", "App", "Function Name ===>'SaveNewAppointment'");
            return View("PetAppointment");
        }
        [HttpGet]
        public IActionResult AllAppointmentRecord(int Breedid, int ddlStatus_val, string date)
        {
            string sql;
            sql = "exec SP_Sel_Appointment " + Breedid + "," + ddlStatus_val + ",'" + date + "'";
            DataSet ds = util.TableBind(sql);
            List<PatientAppointment> appointment = new List<PatientAppointment>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                appointment.Add(new PatientAppointment
                {
                    Appointment_id = Convert.ToInt32(dr["Appointment_id"]),
                    PetName = Convert.ToString(dr["PetName"]),
                    Breed = Convert.ToString(dr["PetBreedName"]),
                    PetType = Convert.ToString(dr["PetTypeName"]),
                    PetS_ParentName = Convert.ToString(dr["PetS_ParentName"]),
                    Appointment_status = Convert.ToString(dr["AppointmentStatusName"]),
                    Appointment_Date = Convert.ToString(dr["Appointment_Date"]),
                    Appointment_Time = Convert.ToString(dr["Appointment_Time"]),
                    Status = Convert.ToBoolean(dr["Status"]),
                    Appointment_Type = Convert.ToString(dr["AppointmentTypeName"]),
                    FlagCheckIn=Convert.ToInt32(dr["FlagCheckIn"]),
                });
            }
            return Json(appointment);
        }
        [HttpPost]
        public JsonResult DeleteAppointmentById(string ApId)
        {
            string messagge = string.Empty;

            List<PatientAppointment> appointment = new List<PatientAppointment>();
            string Sqlquery = "Delete From trn_PetAppointment where Appointment_id='" + ApId.ToString() + "'";
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
        [HttpPost]
        public JsonResult EditAppointmentById(string ApId)
        {
            PatientAppointment appointment = new PatientAppointment();
            try
            {
                string sql = "select Appointment_id,Pet_Id,BreadId,PetTypeId,PetS_ParentName,Appointment_status,Appointment_Date,Appointment_Type,Appointment_Time,Appointment_Duration,Appointment_Notes,isnull(Status , 0) as Status ,PetParent_Id ,DoctorId,Dr_Fees from trn_PetAppointment WHERE Appointment_id= " + ApId + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    appointment.Appointment_id = Convert.ToInt32(dr["Appointment_id"]);
                    appointment.PetName = dr["Pet_Id"].ToString();
                    appointment.Breed = dr["BreadId"].ToString();
                    appointment.PetType = dr["PetTypeId"].ToString();
                    appointment.PetS_ParentName = dr["PetS_ParentName"].ToString();
                    appointment.Appointment_status = dr["Appointment_status"].ToString();
                    appointment.Appointment_Date = dr["Appointment_Date"].ToString();
                    appointment.Appointment_Type = dr["Appointment_Type"].ToString();
                    appointment.Appointment_Time = dr["Appointment_Time"].ToString();
                    appointment.Appointment_Duration = dr["Appointment_Duration"].ToString();
                    appointment.Appointment_Notes = dr["Appointment_Notes"].ToString();
                    appointment.PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]);
                    appointment.Status = Convert.ToBoolean(dr["Status"]);
                    appointment.doctorId = Convert.ToInt32(dr["DoctorId"]);
                    appointment.doctor_Fees = Convert.ToString(dr["Dr_Fees"]);
                    
                }
                ViewBag.SubmitValue = "Update";
            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            return Json(appointment);
        }
        [HttpGet]
        public IActionResult GetPetFromParentId(string id)
        {
            //HttpContext.Session.SetString("PetParentId",id);
            List<PatientAppointment> PetList = new List<PatientAppointment>();
            //PatientAppointment Appointment = new PatientAppointment();
            try
            {
                string sql = "select mpp.PetParent_Id,mp.PetName,mp.Pet_id from Mst_PetParent mpp JOIN Mst_Pet mp on mpp.PetParent_Id = mp.PetParent_Id Where mpp.PetParent_Id = '"+ id+"'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PetList.Add(new PatientAppointment
                    {
                        petId = Convert.ToInt32(dr["Pet_id"]),
                        PetName = dr["PetName"].ToString(),
                    }); 
                    //Appointment.Appointment_id = Convert.ToInt32(dr["NewPharmacy_id"]);
                    // Appointment.PetName = dr["Pet_id"].ToString();
                    //Appointment.Breed = dr["Bread"].ToString();
                    //Appointment.PetS_ParentName = dr["FullName"].ToString();
                    //Appointment.PetType = dr["PetType"].ToString();
                }
            }

            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
        "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "PetAppointment.cshtml", "", "App", "Function Name ===>'GetPetFromParentId'");
            return Json(PetList);
        }
        [HttpGet]
        public JsonResult GetPetPatientData(string id)
        {
            PatientAppointment Appointment = new PatientAppointment();
            try
            {
                string sql = "select mp.PetName , mp.Breed, mp.PetType , mpp.FullName , pb.PetBreedName, pt.PetTypeName from Mst_Pet mp join Mst_PetParent mpp on mpp.PetParent_Id = mp.PetParent_Id Join PetBreed pb on pb.BreedId = mp.Breed Join PetType pt on pt.PetTypeId = mp.PetType where Pet_id = '"+ id+"'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Appointment.Appointment_id = Convert.ToInt32(dr["NewPharmacy_id"]);
                    // Appointment.PetName = dr["Pet_id"].ToString();
                    Appointment.Breed = dr["PetBreedName"].ToString();
                    Appointment.PetS_ParentName = dr["FullName"].ToString();
                    Appointment.PetType = dr["PetType"].ToString();
                    Appointment.BreadId =Convert.ToInt32(dr["Breed"]);
                    Appointment.PetTypeName = dr["PetTypeName"].ToString();
                }
            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "PetAppointment.cshtml", "", "App", "Function Name ===>'GetPetPatientData'");
            return Json(Appointment);
        }

        [HttpGet]
        public JsonResult GetDr_Fees(int id)
        {
            PatientAppointment Appointment = new PatientAppointment();
            try
            {
                string sql = "select Dr_Fees from MST_Doctors where Dr_id='"+id+"'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Appointment.doctor_Fees = dr["Dr_Fees"].ToString();
                }
            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            return Json(Appointment);
        }

        public IActionResult patientCheck_In(int appointment_id)
        {
            
            PopulateAppointmentTime();
            PopulateAppointmentType();
            PopulatedpetId();
            PopulateDiagnosis();
            PopulateMedicineCategory();
            string sql;
            PatientAppointment appointment = new PatientAppointment();
                sql = "select pa.Appointment_id,pa.PetS_ParentName,pa.Appointment_Date,pa.Appointment_Type,pa.Appointment_Time,pa.Appointment_Notes,mpet.Pet_id,mpet.petName,mpet.Pet_Weight ,dr.Dr_Fees from trn_PetAppointment pa join MST_pet mpet    on  pa.Pet_id=mpet.Pet_id join MST_Doctors dr on dr.Dr_id = pa.doctorId where pa.Appointment_id= " + appointment_id + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    appointment.Appointment_id = Convert.ToInt32(dr["Appointment_id"]);
                    appointment.PetS_ParentName = dr["PetS_ParentName"].ToString();
                    appointment.Appointment_Date = dr["Appointment_Date"].ToString();
                    appointment.Appointment_Type = dr["Appointment_Type"].ToString();
                    appointment.Appointment_Time = dr["Appointment_Time"].ToString();
                    appointment.Appointment_Notes = dr["Appointment_Notes"].ToString();
                    appointment.petId = Convert.ToInt32(dr["Pet_id"]);
                    appointment.PetName = dr["PetName"].ToString();
                    appointment.Pet_Weight = Convert.ToInt32(dr["Pet_Weight"]);
                appointment.doctor_Fees = Convert.ToString(dr["Dr_Fees"]);
                }
            ViewBag.appointment = appointment;
            ViewBag.PatientAppointment = PatientHistory(appointment.petId);
            return View("patientCheck_In", appointment);
        }
        [HttpPost]
        public IActionResult patientCheck_In(PatientAppointment appointment)
        {
            string UserId = HttpContext.Session.GetString("userId");
            int FlagCheckIn = 1;
            int FlagCheckOut = 1;
            try
            {

                    sqlquery = "UPDATE trn_PetAppointment SET  Pet_Weight = '"+ appointment.Pet_Weight + "', BodyTem='"+appointment.BodyTem+ "' , Lvaccination_Date='"+appointment.Lvaccination_Date+ "' ,  Procedures='"+appointment.Procedures+ "' , PatientAllergies='"+appointment.PatientAllergies+ "' , Medication = '"+appointment.Medication+ "' , FlagCheckIn = '"+ FlagCheckIn + "' , FlagCheckOut='"+ FlagCheckOut + "', ModifyUser='" + UserId+"' where Appointment_id=" + appointment.Appointment_id + "";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewData["msg"] = "CheckIn successFully.";
                    }
                    else
                    {
                        ViewData["msg"] = " CheckIn not successFully.";
                    }
            }
            catch(Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "patientCheck_In.cshtml", "", "App", "Function Name ===>'patientCheck_In'");
            return View("PetAppointment");
        }
        public List<PatientAppointment> PatientHistory(int petId)
        {
            string sql;
            sql = "exec SP_PatientHistory '"+petId+"'";
            DataSet ds = util.TableBind(sql);
            List<PatientAppointment> appointment = new List<PatientAppointment>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                appointment.Add(new PatientAppointment
                {
                    Appointment_id = Convert.ToInt32(dr["Appointment_id"]),
                    PetName = Convert.ToString(dr["PetName"]),
                    Breed = Convert.ToString(dr["PetBreedName"]),
                    PetType = Convert.ToString(dr["PetTypeName"]),
                    PetS_ParentName = Convert.ToString(dr["PetS_ParentName"]),
                    //Appointment_status = Convert.ToString(dr["AppointmentStatusName"]),
                    Appointment_Date = Convert.ToString(dr["Appointment_Date"]),
                    Appointment_Time = Convert.ToString(dr["Appointment_Time"]),
                    //Status = Convert.ToBoolean(dr["Status"]),
                    // Appointment_Type = Convert.ToString(dr["AppointmentTypeName"])
                    Imaging_Radiologist = Convert.ToString(dr["Imaging_Radiologist"]),
                    Requested_By = Convert.ToString(dr["Requested_By"]),
                    Imaging_Result = Convert.ToString(dr["Imaging_Result"]),
                    Imaging_Type = Convert.ToString(dr["Imaging_Type"]),
                });
            }
            
            return appointment;
        }
        #endregion

        #region PetBreed
        public IActionResult PetBreed()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            ViewBag.SubmitValue = "SAVE";
            return View();
        }
        //For Save And Update Record
        [HttpPost]
        public IActionResult SavePetBreed(PetBreed Breed)
        {
            string UserId = HttpContext.Session.GetString("userId");
            string sqlquery;
            int status_value;
            try
            {
                ViewBag.SubmitValue = "SAVE";
                if (Breed.Status == true)
                {
                    status_value = 1;
                }
                else
                {
                    status_value = 0;
                }
                if (Breed.PetBreedId == 0)
                {
                    sqlquery = "Insert into mst_PetBreed(PetBreedName,CreateDate,Status,CreateUser)values('" + Breed.PetBreedName + "',getdate(),'" + status_value + "','" + UserId + "')";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewData["msg"] = "Pet breed added successfully.";
                    }
                    else
                    {
                        ViewData["msg"] = "Pet breed not added.";
                    }
                }
                else
                {
                    sqlquery = "UPDATE mst_PetBreed SET PetBreedName = '" + Breed.PetBreedName + "',Status='" + status_value + "', ModifyUser='" + UserId + "',ModifyDate=getdate() where PetBreedId=" + Breed.PetBreedId + "";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewData["msg"] = "Pet breed modify successFully.";
                    }
                    else
                    {
                        ViewData["msg"] = "Pet breed not modify.";
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View("PetBreed");
        }
        [HttpGet]
        public JsonResult ShowAllPetBreed()
        {
            try
            {
                string sql = "select PetBreedId,PetBreedName,Status from mst_PetBreed";
                DataSet ds = util.TableBind(sql);
                List<PetBreed> Breed = new List<PetBreed>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Breed.Add(new PetBreed
                    {
                        PetBreedId = Convert.ToInt32(dr["PetBreedId"]),
                        PetBreedName = Convert.ToString(dr["PetBreedName"]),
                        Status = Convert.ToBoolean(dr["Status"])
                    });
                }
                return Json(Breed);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult DeletePetBreedById(string petid)
        {
            string messagge = string.Empty;

            //List<PetBreed>Breed = new List<PetBreed>();
            string Sqlquery = "Delete From mst_PetBreed where PetBreedId='" + petid.ToString() + "'";
            string status = util.MultipleTransactions(Sqlquery);
            if (status == "Successfull")
            {
                messagge = "Delete Successfull!!";
                ViewBag.SubmitValue = "SAVE";
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
        [HttpPost]
        public JsonResult EditPetBreedById(string petid)
        {
            PetBreed Breed = new PetBreed();
            try
            {
                string sql = "select PetBreedId,PetBreedName,Status  from mst_PetBreed  WHERE PetBreedId= " + petid + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Breed.PetBreedId = Convert.ToInt32(dr["PetBreedId"]);
                    Breed.PetBreedName = (dr["PetBreedName"]).ToString();
                    Breed.Status = Convert.ToBoolean(dr["Status"]);

                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.SubmitValue = "UPDATE";
            // var Data = new { breedList = Breed };
            return Json(Breed);
        }
        #endregion

        #region All DropDownBind
        public IActionResult PopulatedpetId()
        {
            string query = "select Pet_id from MST_pet ";
            DataSet ds = util.BindDropDown(query);
            List<SelectListItem> Patinetid = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Patinetid.Add(new SelectListItem { Text = dr["Pet_id"].ToString(), Value = dr["Pet_id"].ToString() });
            }
            ViewBag.Patinetid = Patinetid;
            return View();
        }
        public IActionResult PopulatePetBreed()
        {
            string query = "select BreedId,PetBreedName from PetBreed";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> PetBreedList = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                PetBreedList.Add(new SelectListItem { Text = dr1["PetBreedName"].ToString(), Value = dr1["BreedId"].ToString() });
            }
            ViewBag.PetBreedList = PetBreedList;
            return View();
        }
        public IActionResult PopulatePetType()
        {
            string query = "select PetTypeId,PetTypeName from PetType";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> PetTypeList = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                PetTypeList.Add(new SelectListItem { Text = dr1["PetTypeName"].ToString(), Value = dr1["PetTypeId"].ToString() });
            }
            ViewBag.PetTypeList = PetTypeList;
            return View();
        }
        public IActionResult PopulatePetColour()
        {
            string query = "select PetColourId,PetcolourName from PetColour";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> PetColourList = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                PetColourList.Add(new SelectListItem { Text = dr1["PetcolourName"].ToString(), Value = dr1["PetColourId"].ToString() });
            }
            ViewBag.PetColourList = PetColourList;
            return View();
        }

        public IActionResult PopulateAppointmentTime()
        {
            string query = "select AppointmentTime_Id,AppointmentTime_Text from MST_AppointmentTime";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> AppointmentTime = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                AppointmentTime.Add(new SelectListItem { Text = dr1["AppointmentTime_Text"].ToString(), Value = dr1["AppointmentTime_Id"].ToString() });
            }
            ViewBag.AppointmentTime = AppointmentTime;
            return View();
        }

       
        public IActionResult PopulateAppointmentStatus()
        {
            string query = "select AppointmentStatusId,AppointmentStatusName from AppointmentStatus";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> AppointmentStatus = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                AppointmentStatus.Add(new SelectListItem { Text = dr1["AppointmentStatusName"].ToString(), Value = dr1["AppointmentStatusId"].ToString() });
            }
            ViewBag.AppointmentStatus = AppointmentStatus;
            return View();
        }

        public IActionResult PopulateAppointmentType()
        {
            string query = "select AppointmentTypeId,AppointmentTypeName from AppointmentType";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> AppointmentType = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                AppointmentType.Add(new SelectListItem { Text = dr1["AppointmentTypeName"].ToString(), Value = dr1["AppointmentTypeId"].ToString() });
            }
            ViewBag.AppointmentType = AppointmentType;
            return View();
        }

        public IActionResult PopulatePharmacyPet()
        {
            string query = "select pharmacy.NewPharmacy_id,mpet.PetName from MST_NewPharmacyRequest pharmacy join MST_Pet mpet ON mpet.Pet_id = pharmacy.Pet_id"; 
            DataSet ds1 = util.BindDropDown(query);                           
            List<SelectListItem> PatientName = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                PatientName.Add(new SelectListItem { Text = dr1["PetName"].ToString(), Value = dr1["NewPharmacy_id"].ToString() });
            }
            ViewBag.PatientName = PatientName;
            return View();
        }
        public JsonResult PopulatePetParentName()
        {
            string query = "select PetParent_Id, CONCAT(FullName,'  ' ,'-', ' ',Mobile_no,'') as FullName from Mst_PetParent";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> petParentName = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                petParentName.Add(new SelectListItem { Text = dr1["FullName"].ToString(), Value = dr1["PetParent_Id"].ToString() });
            }
            ViewBag.petParentName = petParentName;
            return Json(petParentName);
        }
        public IActionResult PopulatePriceCategory()
        {
            string query = "select CategoryId,CategoryName from PriceCategory";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> PriceCategory = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                PriceCategory.Add(new SelectListItem { Text = dr1["CategoryName"].ToString(), Value = dr1["CategoryId"].ToString() });
            }
            ViewBag.PriceCategory = PriceCategory;
            return View();
        }
        public IActionResult PopulateCountry()
        {
            string query = "select CountryId,CountryName from Country";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> Country = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                Country.Add(new SelectListItem { Text = dr1["CountryName"].ToString(), Value = dr1["CountryId"].ToString() });
            }
            ViewBag.Country = Country;
            return View();
        }
        public JsonResult PopulatePetName()
        {
            //string PetParentId = HttpContext.Session.GetString("PetParentId");
            //  PetParentId = petParent_Id;
            // string query = "select Pet_id , PetName from Mst_Pet where PetParent_Id = '"+PetParentId+"'";
            string query = "select Pet_id , PetName from Mst_Pet";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> Pet = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                Pet.Add(new SelectListItem { Text = dr1["PetName"].ToString(), Value = dr1["Pet_id"].ToString() });
            }
            ViewBag.Pet = Pet;
            return Json(Pet);
        }

        public IActionResult EditPetName(string petParent_Id)
        {
           // string PetParentId = HttpContext.Session.GetString("PetParentId");
            //  PetParentId = petParent_Id;
            string query = "select Pet_id , PetName from Mst_Pet where petParent_Id = '" + petParent_Id + "'";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> Pet = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                Pet.Add(new SelectListItem { Text = dr1["PetName"].ToString(), Value = dr1["Pet_id"].ToString() });
            }
            ViewBag.Pet = Pet;
            return Json(Pet);
        }

        public IActionResult PopulatedGender()
        {
            string query = "select GenderId,GenderName from MST_Gender";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> Gender = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                Gender.Add(new SelectListItem { Text = dr1["GenderName"].ToString(), Value = dr1["GenderId"].ToString() });
            }
            ViewBag.Gender = Gender;
            return View();
        }
        public IActionResult PopulateLabType()
        {
            string query = "select LabTypeId,LabTypeName from Cma_ViewLabType";
            DataSet ds = util.BindDropDown(query);
            List<SelectListItem> LabType = new List<SelectListItem>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                LabType.Add(new SelectListItem { Text = dr["LabTypeName"].ToString(), Value = dr["LabTypeId"].ToString() });
            }
            ViewBag.LabType = LabType;
            return View();
        }

        public IActionResult PopulateImgType()
        {
            string query = "select ImgTypeId,ImgTypeName from Cma_view_ImagingType";
            DataSet ds = util.BindDropDown(query);
            List<SelectListItem> ImgType = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ImgType.Add(new SelectListItem { Text = dr["ImgTypeName"].ToString(), Value = dr["ImgTypeId"].ToString() });
            }
            ViewBag.ImgType = ImgType;
            return View();
        }

        public IActionResult PopulateDr_Designation()
        {
            string query = "select DesignationId,DesignationName from View_Dr_Designation";
            DataSet ds = util.BindDropDown(query);
            List<SelectListItem> Dr_Designation = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Dr_Designation.Add(new SelectListItem { Text = dr["DesignationName"].ToString(), Value = dr["DesignationId"].ToString() });
            }
            ViewBag.Dr_Designation = Dr_Designation;
            return View();
        }
        public IActionResult PopulateDoctorName()
        {
            string query = "select Dr_id, Dr_Name from MST_Doctors";
            DataSet ds = util.BindDropDown(query);
            List<SelectListItem> DoctorName = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DoctorName.Add(new SelectListItem { Text = dr["Dr_Name"].ToString(), Value = dr["Dr_id"].ToString() });
            }
            ViewBag.DoctorName = DoctorName;
            return View();
        }
        public IActionResult PopulateDiagnosis()
        {
            string query = "select DataDictionaryId, Term_Name  from Diagnosis_Data";
            DataSet ds = util.BindDropDown(query);
            List<SelectListItem> Diagnosis = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Diagnosis.Add(new SelectListItem { Text = dr["Term_Name"].ToString(), Value = dr["DataDictionaryId"].ToString() });
            }
            ViewBag.Diagnosis = Diagnosis;
            return View();
        }

        public JsonResult PopulateMedicineSalt(int id)
        {
            string query = "select MedicineSaltId , MedicineSaltName , MedicineCategoryId  from MST_MedicineSalts  where MedicineCategoryId = "+id+"";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> MedicineSalt = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                MedicineSalt.Add(new SelectListItem { Text = dr1["MedicineSaltName"].ToString(), Value = dr1["MedicineSaltId"].ToString() });
            }
            ViewBag.Pet = MedicineSalt;
            return Json(MedicineSalt);
        }
        public JsonResult PopulateMedicine(int id)
        {
            string query = "select MedicineId,MedicineName ,MedicineSaltId from MST_Medicine  where MedicineSaltId = "+id+"";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> Medicine = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                Medicine.Add(new SelectListItem { Text = dr1["MedicineName"].ToString(), Value = dr1["MedicineId"].ToString() });
            }
            ViewBag.Pet = Medicine;
            return Json(Medicine);
        }
        public JsonResult PopulateMedicineBrand(int id)
        {
            string query = "select Medicine_BrandId,Medicine_Brand_Name,MedicineId from MST_Medicine_Brand where MedicineId = "+id+"";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> MedicineBrand = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                MedicineBrand.Add(new SelectListItem { Text = dr1["Medicine_Brand_Name"].ToString(), Value = dr1["Medicine_BrandId"].ToString() });
            }
            ViewBag.Pet = MedicineBrand;
            return Json(MedicineBrand);
        }
        public JsonResult PopulateMedicineComposition(int id)
        {
            string query = "select Medicine_CompositionId ,Composition,Medicine_BrandId from MST_Medicine_Composition where Medicine_BrandId ="+id+"";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> MedicineComposition = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                MedicineComposition.Add(new SelectListItem { Text = dr1["Composition"].ToString(), Value = dr1["Medicine_CompositionId"].ToString() });
            }
            ViewBag.Pet = MedicineComposition;
            return Json(MedicineComposition);
        }
        public IActionResult PopulateMedicineCategory()
        {
            //int id
            //string query = "select MedicineCategoryId, MedicineCategoryName from MST_MedicineCategory  where D_DataDictionaryId= "+id+"";
            string query = "select MedicineCategoryId, MedicineCategoryName from MST_MedicineCategory";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> MedicineCategory = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                MedicineCategory.Add(new SelectListItem { Text = dr1["MedicineCategoryName"].ToString(), Value = dr1["MedicineCategoryId"].ToString() });
            }
            ViewBag.MedicineCategory = MedicineCategory;
            return View(MedicineCategory);
        }
        #endregion

        #region Doctors
        public IActionResult Doctors()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            ViewBag.SubmitValue = "SAVE";
            PopulatedGender();
            PopulateDr_Designation();
            return View();
        }
        //For Save and update
        [HttpPost]
        public IActionResult CreateDoctor(Doctors Dr)
        {
            string UserId = HttpContext.Session.GetString("userId");
            
            int status_value;
            string radioValue;
            try
            {
                if (Dr.Dr_status == true)
                {
                    status_value = 1;
                }
                else
                {
                    status_value = 0;
                }
                if (Dr.Dr_Gender == "1")
                {
                    radioValue = "Male";
                }
                if (Dr.Dr_id == 0)
                {
                    ViewBag.SubmitValue = "SAVE";
                    sqlquery = "Insert into MST_Doctors(Dr_Name,Dr_Gender,Dr_Designation,Dr_Fees,Dr_Email,Dr_MobileNo,Dr_Address,CreateUser,CreateDate,Dr_status)values('" + Dr.Dr_Name + "','" + Dr.Dr_Gender + "','" + Dr.Dr_Designation + "','" + Dr.Dr_Fees + "','" + Dr.Dr_Email + "','" + Dr.Dr_MobileNo + "','" + Dr.Dr_Address + "','" + UserId + "',getdate(),'" + status_value + "')";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Doctor added Successfully,";

                    }
                    else
                    {
                        ViewBag.Message = "Doctor not added Successfully.";
                    }
                }
                else
                {
                    //ViewBag.SubmitValue = "UPDATE";
                    sqlquery = "UPDATE Mst_PetParent SET Dr_Name = '" + Dr.Dr_Name + "',Dr_Gender='" + Dr.Dr_Gender + "', Dr_Designation = '" + Dr.Dr_Designation + "', Dr_Fees='" + Dr.Dr_Fees + "', Dr_Email ='" + Dr.Dr_Email + "', Dr_MobileNo='" + Dr.Dr_MobileNo + "',Dr_Address='" + Dr.Dr_Address + "' ,Dr_status='" + status_value + "',ModifyUser='" + UserId + "',ModifyDate=getdate() where Dr_id=" + Dr.Dr_id + "";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Doctor modify successfully.";
                     
                    }
                    else
                    {
                        ViewBag.Message = "Doctor not modify.";
                    }
                }

            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "Doctors.cshtml", "", "App", "Function Name ===>'CreateDoctor'");
            return View("Doctors");
        }
        //For Show All Record of Doctor
        [HttpGet]
        public IActionResult ShowDoctorsDetails(int DesignationId)
        {
            string sql;
            List<Doctors> doctorDetails = new List<Doctors>();
            try
            {
                //sql = "select Dr_id,Dr_Name,Dr_Gender,Dr_Designation,Dr_Fees,Dr_Time,Dr_Email,Dr_MobileNo,Dr_Address,Dr_status from MST_Doctors";
                sql = "exec SP_SEL_DOCTOR " + DesignationId + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    doctorDetails.Add(new Doctors
                    {
                        Dr_id = Convert.ToInt32(dr["Dr_id"]),
                        Dr_Name = Convert.ToString(dr["Dr_Name"]),
                        Dr_Fees = Convert.ToString(dr["Dr_Fees"]),
                        Dr_Designation = Convert.ToString(dr["DesignationName"]),
                        Dr_Gender = Convert.ToString(dr["Dr_Gender"]),
                        Dr_MobileNo = Convert.ToString(dr["Dr_MobileNo"]),
                        Dr_Address = Convert.ToString(dr["Dr_Address"]),
                        //Dr_Time = Convert.ToString(dr["Dr_Time"]),
                        Dr_Email = Convert.ToString(dr["Dr_Email"]),
                        Dr_status = Convert.ToBoolean(dr["Dr_status"])
                    });
                }
            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "Doctors.cshtml", "", "App", "Function Name ===>'ShowDoctorsDetails'");
            return Json(doctorDetails);
        }
        //For Edit Doctor Details Id
        //Here drId(Doctor Id) Come from Ajax 
        [HttpPost]
        public JsonResult EditDoctorDetailsById(string drId)
        {
            Doctors doctorDetails = new Doctors();
            try
            {
                string sql = "select Dr_id,Dr_Name,Dr_Gender,Dr_Designation,Dr_Fees,Dr_Time,Dr_Email,Dr_MobileNo,Dr_Address,Dr_status from MST_Doctors WHERE Dr_id= " + drId + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    doctorDetails.Dr_id = Convert.ToInt32(dr["Dr_id"]);
                    doctorDetails.Dr_Name = dr["Dr_Name"].ToString();
                    doctorDetails.Dr_Designation = dr["Dr_Designation"].ToString();
                    doctorDetails.Dr_Fees = dr["Dr_Fees"].ToString();
                    doctorDetails.Dr_Gender = dr["Dr_Gender"].ToString();
                   // doctorDetails.Dr_Time = dr["Dr_Time"].ToString();
                    doctorDetails.Dr_Email = dr["Dr_Email"].ToString();
                    doctorDetails.Dr_MobileNo = dr["Dr_MobileNo"].ToString();
                    doctorDetails.Dr_Address = dr["Dr_Address"].ToString();
                    doctorDetails.Dr_status = Convert.ToBoolean(dr["Dr_status"]);
                }
                ViewBag.SubmitValue = "Update";
            }
            catch (Exception ex)
            {

            }
            //var Data = new {doctorDetails= doctorDetails };
            return Json(doctorDetails);
        }

        //For Delete Record by id
        //Here drId Come from Ajax
        [HttpPost]
        public JsonResult DeleteById(string drId)
        {
            string messagge = string.Empty;
            List<Doctors> doctorDetails = new List<Doctors>();
            string Sqlquery = "Delete From MST_Doctors where Dr_id='" + drId.ToString() + "'";
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

        #region Pharmacy

        public IActionResult Pharmacy()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            ViewBag.SubmitValue = "SAVE";
            PopulatePharmacyPet();
            PopulateAppointmentType();
            return View();
        }
        //For Save and Update
        [HttpPost]
        public IActionResult CreateNewPharmacy(NewPharmacyRequest NewPharmacy)
        {
            string UserId = HttpContext.Session.GetString("userId");
            //string sqlquery;
            try
            {
                if (NewPharmacy.NewPharmacy_id == 0)
                {
                    ViewBag.SubmitValue = "SAVE";
                    sqlquery = "Insert into MST_NewPharmacyRequest(Pet_id,Appointment_type,Pharmacy_name,Prescription_Date,Prescription,Quantity,Refills,CreateUser,CreateDate,PetParent_Id)values('" + NewPharmacy.Pet_id + "','" + NewPharmacy.Appointment_type + "','" + NewPharmacy.Pharmacy_name + "','" + NewPharmacy.Prescription_Date + "','" + NewPharmacy.Prescription + "','" + NewPharmacy.Quantity + "','" + NewPharmacy.Refills + "','" + UserId + "',getdate(),'"+NewPharmacy.PetParent_Id+"')";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "New pharmacy added successfully.";

                    }
                    else
                    {
                        ViewBag.Message = "New pharmacy not added.";
                    }
                }
                else
                {
                    ViewBag.SubmitValue = "UPDATE";
                    sqlquery = "UPDATE MST_NewPharmacyRequest SET Pet_id = '" + NewPharmacy.Pet_id + "',Appointment_type='" + NewPharmacy.Appointment_type + "', Pharmacy_name = '" + NewPharmacy.Pharmacy_name + "', Prescription_Date='" + NewPharmacy.Prescription_Date + "',Prescription='" + NewPharmacy.Prescription + "', Quantity ='" + NewPharmacy.Quantity + "', Refills='" + NewPharmacy.Quantity + "',ModifyUser='" + UserId + "',ModifyDate=getdate(),PetParent_Id='"+NewPharmacy.PetParent_Id+"' where NewPharmacy_id=" + NewPharmacy.NewPharmacy_id + "";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "New pharmacy modify successfully";
                    }
                    else
                    {
                        ViewBag.Message = "New pharmacy not modify.";
                    }
                }

            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "Pharmacy.cshtml", "", "App", "Function Name ===>'CreateNewPharmacy'");
            return View("Pharmacy");
        }
        //For Show Data 
        [HttpGet]
        public JsonResult ShowPharmacyDetails()
        {
            List<NewPharmacyRequest> NewPharmacy = new List<NewPharmacyRequest>();
            try
            {
                string sql = "select  isnull(pharmacy.NewPharmacy_id,0) as NewPharmacy_id ,isnull(pharmacy.Pet_id,0) as Pet_id,isnull(pharmacy.Appointment_type,'')as Appointment_type ,isnull(pharmacy.Pharmacy_name,'') as Pharmacy_name,isnull(pharmacy.Prescription_Date,'') as Prescription_Date ,isnull(pharmacy.Prescription,'') as Prescription ,isnull(pharmacy.Quantity,0)as Quantity, isnull(pharmacy.Refills,'') as Refills ,isnull(pharmacy.PetParent_Id,0)as PetParent_Id , isnull(mpet.PetName,'')as PetName , isnull(mpp.FullName,'')as FullName , isnull(apt.AppointmentTypeName,'')as AppointmentTypeName from MST_NewPharmacyRequest pharmacy join MST_Pet mpet ON mpet.Pet_id = pharmacy.Pet_id Join MST_PetParent mpp on mpp.PetParent_Id = pharmacy.PetParent_Id Join AppointmentType apt on apt.AppointmentTypeId = pharmacy.Appointment_type";
                DataSet ds = util.TableBind(sql);
              
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NewPharmacy.Add(new NewPharmacyRequest
                    {
                        NewPharmacy_id = Convert.ToInt32(dr["NewPharmacy_id"]),
                        Pet_id = Convert.ToString(dr["PetName"]),
                        Prescription_Date = Convert.ToString(dr["Prescription_Date"]),
                        Appointment_type = Convert.ToString(dr["AppointmentTypeName"]),
                        Pharmacy_name = Convert.ToString(dr["Pharmacy_name"]),
                        Prescription = Convert.ToString(dr["Prescription"]),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        Refills = Convert.ToString(dr["Refills"]),
                        PetParent_Id = (dr["FullName"]).ToString(),
                    });
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Json(NewPharmacy);
        }

        //[HttpGet]
        //public JsonResult CheckInRequestForPharmacy()
        //{
        //    List<PatientAppointment> requestPharmacy = new List<PatientAppointment>();
        //    string sql = "select * from Check_In where Stage = 1";
        //    DataSet ds = util.TableBind(sql);

        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        requestPharmacy.Add(new PatientAppointment
        //        {
        //            PetName = Convert.ToString(dr["petName"]),
        //            Prescription_Date = Convert.ToString(dr["CreateDate"]),
        //            Appointment_Type = Convert.ToString(dr["Visit_Type"]),
        //            Medication = Convert.ToString(dr["Medication"]),
        //            //Prescription = Convert.ToString(dr["Prescription"]),
        //           // Quantity = Convert.ToInt32(dr["Quantity"]),
        //           // Refills = Convert.ToString(dr["Refills"]),
        //            PetS_ParentName = (dr["PetS_ParentName"]).ToString(),
        //        });
        //    }
        //    return Json(requestPharmacy);
        //}
        //For Edit by Pharmacy Id
        [HttpPost]
        public JsonResult EditPharmacyById(string pharmacyId)
        {
            NewPharmacyRequest NewPharmacy = new NewPharmacyRequest();
            ViewBag.SubmitValue = "Update";
            try
            {
                string sql = "select pharmacy.NewPharmacy_id, pharmacy.Pet_id,  pharmacy.Appointment_type, pharmacy.Pharmacy_name, pharmacy.Prescription_Date, pharmacy.Prescription, pharmacy.Quantity, pharmacy.Refills, atype.AppointmentTypeName from MST_NewPharmacyRequest pharmacy join AppointmentType atype on atype.AppointmentTypeId = pharmacy.Appointment_type WHERE NewPharmacy_id = " + pharmacyId + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NewPharmacy.NewPharmacy_id = Convert.ToInt32(dr["NewPharmacy_id"]);
                    NewPharmacy.Pet_id = dr["Pet_id"].ToString();
                    NewPharmacy.Appointment_type = dr["Appointment_type"].ToString();
                    NewPharmacy.Appointment_type_name = dr["AppointmentTypeName"].ToString();
                    NewPharmacy.Pharmacy_name = dr["Pharmacy_name"].ToString();
                    NewPharmacy.Prescription_Date = dr["Prescription_Date"].ToString();
                    NewPharmacy.Prescription = dr["Prescription"].ToString();
                    NewPharmacy.Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                    NewPharmacy.Refills = dr["Refills"].ToString();
                }

            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
        "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "Pharmacy.cshtml", "", "App", "Function Name ===>'EditPharmacyById'");
            return Json(NewPharmacy);
        }
        //for Delete By Pharmacy Id
        [HttpPost]
        public JsonResult DeletePharmacyById(string pharmacyId)
        {
            string messagge = string.Empty;
            // List<NewPharmacyRequest> NewPharmacy = new List<NewPharmacyRequest>();
            string Sqlquery = "Delete From MST_NewPharmacyRequest where NewPharmacy_id='" + pharmacyId.ToString() + "'";
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
        [HttpGet]
        public JsonResult GetAppointmentType(string id)
        {
            NewPharmacyRequest NewPharmacy = new NewPharmacyRequest();
           
                string sql = "select isnull(petap.Appointment_Type,'') as Appointment_Type, isnull(atype.AppointmentTypeName , '') as AppointmentTypeName from trn_PetAppointment petap Join AppointmentType atype on atype.AppointmentTypeId = petap.Appointment_Type where Pet_Id = '" + id+"'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NewPharmacy.Appointment_type = dr["Appointment_Type"].ToString();
                NewPharmacy.Appointment_type_name = dr["AppointmentTypeName"].ToString();
                }
          
            return Json(NewPharmacy);
        }

        #region Dispense Pharmacy
        [HttpGet]
        public JsonResult GetPatientDetails(string id)
        {
           
            NewPharmacyRequest NewPharmacy = new NewPharmacyRequest();
            ViewBag.SubmitValue = "Update";
            try
            {
                string sql = "select phReq.NewPharmacy_id,phReq.Pet_id,phReq.Appointment_type,phReq.Pharmacy_name,phReq.Prescription_Date,phReq.Prescription,phReq.Quantity,phReq.Refills ,atype.AppointmentTypeName from MST_NewPharmacyRequest phReq join AppointmentType atype on atype.AppointmentTypeId = phReq.Appointment_type WHERE Pet_id = '"+id+"'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NewPharmacy.NewPharmacy_id = Convert.ToInt32(dr["NewPharmacy_id"]);
                    NewPharmacy.Pet_id = dr["Pet_id"].ToString();
                    NewPharmacy.Appointment_type = dr["Appointment_type"].ToString();
                    NewPharmacy.Appointment_type_name= dr["AppointmentTypeName"].ToString();
                    NewPharmacy.Pharmacy_name = dr["Pharmacy_name"].ToString();
                    NewPharmacy.Prescription_Date = dr["Prescription_Date"].ToString();
                    NewPharmacy.Prescription = dr["Prescription"].ToString();
                    NewPharmacy.Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                    NewPharmacy.Refills = dr["Refills"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "Pharmacy.cshtml", "", "App", "Function Name ===>'GetPatientDetails'");
            return Json(NewPharmacy);
        }
        [HttpPost]
        public JsonResult SaveDispensePharmacy(int id, string appointmentType, string pharmacyname, string PrescriptionDate, string prescription, int quantitiy, string refills , string billTo)
        {
            string UserId = HttpContext.Session.GetString("userId");
            string messagge = string.Empty;
            sqlquery = "UPDATE MST_NewPharmacyRequest SET Appointment_type='" + appointmentType + "', Pharmacy_name = '" + pharmacyname + "', Prescription_Date='" + PrescriptionDate + "',Prescription='" + prescription + "', Quantity ='" + quantitiy + "', Refills='" + refills + "', Bill_To='" + billTo + "', ModifyUser='" + UserId + "',ModifyDate=getdate() where NewPharmacy_id=" + id + "";
            string status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                messagge = "Success";
                ModelState.Clear();
            }
            else
            {
              
            }
            var Data = new { msg = messagge };
            return Json(Data);
        }
        #endregion

        #region Re-Issue Pharmacy
        [HttpGet]
        public IActionResult SaveReIssuePharmacy(NewPharmacyRequest NewPharmacy)
        {
            string UserId = HttpContext.Session.GetString("userId");
            sqlquery = "UPDATE MST_NewPharmacyRequest SET Pet_id = '" + NewPharmacy.Pet_id + "',Appointment_type='" + NewPharmacy.Appointment_type + "', Pharmacy_name = '" + NewPharmacy.Pharmacy_name + "',Quantity_ToReturn='"+NewPharmacy.Quantity_ToReturn+ "',Return_Reason='"+NewPharmacy.Return_Reason+ "', Return_Location='"+NewPharmacy.Return_Location+ "', Adjustment_Date='"+NewPharmacy.Adjustment_Date+ "', Credit_ToAccount='"+NewPharmacy.Credit_ToAccount+"', ModifyUser='" + UserId + "',ModifyDate=getdate() where NewPharmacy_id=" + NewPharmacy.NewPharmacy_id + "";
            string status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                ModelState.Clear();
                ViewData["msg"] = " Re-Issue Pharmacy Request Success";
               
            }
            else
            {
                ViewData["msg"] = "Re-Issue Pharmacy Request Not Success";
            }
            return View("Pharmacy");
        }
        [HttpPost]
        public JsonResult ReIssuePharmacySave(int id , string appointmentType, string pharmacyname , int QuantityToReturn,string ReturnReason,string ReturnLocation,string AdjustmentDate,string CreditToAccount)
        {
            string messagge = string.Empty;
            //NewPharmacyRequest NewPharmacy = new NewPharmacyRequest();
            //    NewPharmacy.NewPharmacy_id = id;
            //NewPharmacy.Appointment_type = appointmentType;
            //NewPharmacy.Pharmacy_name = pharmacyname;
            //NewPharmacy.Prescription_Date = PrescriptionDate;
            //NewPharmacy.Prescription = prescription;
            //NewPharmacy.Quantity = quantitiy;
            //NewPharmacy.Refills = refills;
            sqlquery = "UPDATE MST_NewPharmacyRequest SET Appointment_type='" + appointmentType + "', Pharmacy_name = '" + pharmacyname + "', Quantity_ToReturn='" + QuantityToReturn + "',Return_Reason='" + ReturnReason + "', Return_Location ='" + ReturnLocation + "', Adjustment_Date='" + AdjustmentDate + "', Credit_ToAccount='" + CreditToAccount + "', ModifyUser='" + "" + "',ModifyDate=getdate() where NewPharmacy_id=" + id + "";
            string status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                messagge = "Success";
                ModelState.Clear();
                //ViewData["msg"] = " Dispense Pharmacy Request Success";
            }
            else
            {
                // ViewData["msg"] = "Dispense Pharmacy Request Not Success";
            }
            var Data = new { msg = messagge };
            return Json(Data);
        }

        #endregion
        #endregion

        #region Inventory
        public IActionResult Inventory()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            return View();
        }
        public IActionResult AddNewInventory(string inventoryid)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            AddInventory Inventory = new AddInventory();
            try
            {
                string sql = "select Inventory_Id, isnull(Inventory_ItemName,'')as Inventory_ItemName ,isnull(Inventory_type,'')as Inventory_type ,isnull(CrossReference,'')as CrossReference ,isnull(ReorderPoint,'')as ReorderPoint ,isnull(SalesPrice,'')as SalesPrice ,isnull(DistributionUnit,'')as DistributionUnit ,isnull(DateRecieved,'')as DateRecieved ,isnull(Invoice_Number,'')as Invoice_Number ,isnull(Quantity,'')as Quantity ,isnull(Unit,'')as Unit ,isnull(PurchaseCost,'')as PurchaseCost ,isnull(SerialNumber,'')as SerialNumber ,isnull(ExpirationDate,'')as ExpirationDate ,isnull(Vendor,'')as Vendor ,isnull(VendorItemNumber,'') as VendorItemNumber,isnull(VendorLocation,'') as VendorLocation from MST_Inventory_Add WHERE Inventory_Id= '" + inventoryid + "'";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Inventory.Inventory_Id = Convert.ToInt32(dr["Inventory_Id"]);
                    Inventory.Inventory_ItemName = ds.Tables[0].Rows[0]["Inventory_ItemName"].ToString();
                    Inventory.Inventory_type = ds.Tables[0].Rows[0]["Inventory_type"].ToString();
                    Inventory.CrossReference = ds.Tables[0].Rows[0]["CrossReference"].ToString();
                    Inventory.ReorderPoint = ds.Tables[0].Rows[0]["ReorderPoint"].ToString();
                    Inventory.SalesPrice = ds.Tables[0].Rows[0]["SalesPrice"].ToString();
                    Inventory.DistributionUnit = ds.Tables[0].Rows[0]["DistributionUnit"].ToString();
                    Inventory.DateRecieved = ds.Tables[0].Rows[0]["DateRecieved"].ToString();
                    Inventory.Invoice_Number = ds.Tables[0].Rows[0]["Invoice_Number"].ToString();
                    Inventory.Quantity = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    Inventory.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                    Inventory.PurchaseCost = ds.Tables[0].Rows[0]["PurchaseCost"].ToString();
                    Inventory.SerialNumber = Convert.ToInt32(dr["SerialNumber"]);
                    Inventory.ExpirationDate = ds.Tables[0].Rows[0]["ExpirationDate"].ToString();
                    Inventory.Vendor = ds.Tables[0].Rows[0]["Vendor"].ToString();
                    Inventory.VendorItemNumber = Convert.ToInt32(dr["VendorItemNumber"]);
                    Inventory.VendorLocation = ds.Tables[0].Rows[0]["VendorLocation"].ToString();
                }
                //ViewBag.SubmitValue = "Update";

            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            ViewBag.SubmitValue = "Add";
            finalStatus =
         "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "Inventory.cshtml", "", "App", "Function Name ===>'AddNewInventory'");
            return View("AddNewInventory", Inventory);
        }
        [HttpPost]
        public IActionResult SaveNewInventory(AddInventory inventory)
        {
            string UserId = HttpContext.Session.GetString("userId");
            //string sqlquery;
            //int status_value;
            try
            {
                //if (petparent.Status == true)
                //{
                //    status_value = 1;
                //}
                //else
                //{
                //    status_value = 0;
                //}
                if (inventory.Inventory_Id == 0)
                {
                   
                    ViewBag.SubmitValue = "SAVE";
                    sqlquery = "Insert into MST_Inventory_Add(Inventory_ItemName,Inventory_type,CrossReference,ReorderPoint,SalesPrice,DistributionUnit,DateRecieved,Invoice_Number,Quantity,Unit,PurchaseCost,SerialNumber,ExpirationDate,Vendor,VendorItemNumber,VendorLocation,CreateUser,CreateDate)values('" + inventory.Inventory_ItemName + "','" + inventory.Inventory_type + "','" + inventory.CrossReference + "','" + inventory.ReorderPoint + "','" + inventory.SalesPrice + "','" + inventory.DistributionUnit + "','"+inventory.DateRecieved+"','"+inventory.Invoice_Number+"','"+inventory.Quantity+"','"+inventory.Unit+"','"+inventory.PurchaseCost+"','"+inventory.SerialNumber+"','"+inventory.ExpirationDate+"','"+inventory.Vendor+"','"+inventory.VendorItemNumber+"','"+inventory.VendorLocation+"','" + UserId + "',getdate())";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Inventory added successfully.";
                    }
                    else
                    {
                        ViewBag.Message = "Inventory not added";
                    }

                }
                else
                {
                    //ViewBag.SubmitValue = "UPDATE";
                    sqlquery = "UPDATE MST_Inventory_Add SET Inventory_ItemName='"+ inventory.Inventory_ItemName+ "',Inventory_type='"+ inventory.Inventory_type+ "',CrossReference='"+inventory.CrossReference+"',ReorderPoint='"+inventory.ReorderPoint+"',SalesPrice='"+inventory.SalesPrice+"',DistributionUnit='"+inventory.DistributionUnit+"',DateRecieved='"+inventory.DateRecieved+ "',Invoice_Number='" + inventory.Invoice_Number+"',Quantity='"+inventory.Quantity+"',Unit='"+inventory.Unit+"',PurchaseCost='"+inventory.PurchaseCost+"',SerialNumber='"+inventory.SerialNumber+"',ExpirationDate='"+inventory.ExpirationDate+"',Vendor='"+inventory.Vendor+"',VendorItemNumber='"+inventory.VendorItemNumber+"',VendorLocation='"+inventory.VendorLocation+"',ModifyUser='" + UserId + "',ModifyDate=getdate() where Inventory_Id=" + inventory.Inventory_Id + "";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "New Inventory Update";
                        msg = "New Inventory Update";
                    }
                    else
                    {
                        ViewBag.Message = "New Inventory Not Update";
                        msg = "New Inventory Not Update";
                    }
                }
            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
        "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "SaveNewInventory.cshtml", "", "App", "Function Name ===>'AddNewInventory'");
            return View("AddNewInventory");
        }
        public IActionResult InventoryRecievedItem()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            return View();
        }
        [HttpPost]
        public IActionResult SaveInventoryRecieved(InventoryRecieved ivr)
        {
            //string sqlquery;
            try
            {
                if (ivr.In_RecievedID==0)
                {
                    sqlquery = "INSERT INTO MST_Inventory_Recieved(DateRecieved,Vendor,Invoice_Number,Location,Inventory_ItemName,Quantity,PurchaseCost,VendorItemNumber,SerialLotNumber,ExpirationDate,CreateUser,CreateDate)VALUES('"+ivr.DateRecieved+"','"+ivr.Vendor+"','"+ivr.Invoice_Number+"','"+ivr.Location+"','"+ivr.Inventory_ItemName+"','"+ivr.Quantity+"','"+ivr.PurchaseCost+"','"+ivr.VendorItemNumber+"','"+ivr.SerialLotNumber+"','"+ivr.ExpirationDate+"','"+""+"',getdate())";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Inventory recieved added successfully.";
                        msg = "Inventory recieved added successfully.";
                    }
                    else
                    {
                        ViewBag.Message = "Inventory recieved not added";
                        msg = "Inventory recieved not added";
                    }
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
        "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "InventoryRecievedItem.cshtml", "", "App", "Function Name ===>'SaveInventoryRecieved'");
            return View("InventoryRecievedItem");
        }
        [HttpGet]
        public JsonResult ShowInventoryRecieved()
        {
            List<InventoryRecieved> inventoryRecieveds = new List<InventoryRecieved>();
            try
            {
                string sql = "select In_RecievedID,DateRecieved,Vendor,Invoice_Number,Location,Inventory_ItemName,Quantity,PurchaseCost,VendorItemNumber,SerialLotNumber,ExpirationDate from MST_Inventory_Recieved";
                DataSet ds = util.TableBind(sql);
                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    inventoryRecieveds.Add(new InventoryRecieved
                    {
                        In_RecievedID = Convert.ToInt32(dr["In_RecievedID"]),
                        Inventory_ItemName = Convert.ToString(dr["Inventory_ItemName"]),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        PurchaseCost = Convert.ToString(dr["PurchaseCost"]),
                        VendorItemNumber = Convert.ToInt32(dr["VendorItemNumber"]),
                        SerialLotNumber = Convert.ToInt32(dr["SerialLotNumber"]),
                        ExpirationDate = Convert.ToString(dr["ExpirationDate"]),
                    });
                }
               
            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
             finalStatus =
          "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> " , "" , " Select query", "InventoryRecievedItem.cshtml", "", "App", "Function Name ===>'ShowInventoryRecieved'");
            return Json(inventoryRecieveds);
        }
        [HttpPost]
        public JsonResult DelInventoryRecieved(string in_RecievedID)
        {
            string messagge;
            string Sqlquery = "Delete From MST_Inventory_Recieved where In_RecievedID='" + in_RecievedID.ToString() + "'";
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
        [HttpGet]
        public JsonResult ShowInventory()
        {
            List<AddInventory> AddInventory = new List<AddInventory>();
            try
            {
                string sql = "select Inventory_Id,Inventory_ItemName,Inventory_type,CrossReference,ReorderPoint,SalesPrice,DistributionUnit,DateRecieved,Invoice_Number,Quantity,Unit,PurchaseCost,SerialNumber,ExpirationDate,Vendor,VendorItemNumber,VendorLocation from MST_Inventory_Add";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    AddInventory.Add(new AddInventory
                    {
                        Inventory_Id = Convert.ToInt32(dr["Inventory_Id"]),
                        Inventory_ItemName = Convert.ToString(dr["Inventory_ItemName"]),
                        Quantity = Convert.ToString(dr["Quantity"]),

                    });
                }
               
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(AddInventory);
        }
        [HttpPost]
        public JsonResult DeleteInventoryById(string inventoryId)
        {
            string messagge = string.Empty;
            // List<NewPharmacyRequest> NewPharmacy = new List<NewPharmacyRequest>();
            string Sqlquery = "Delete From MST_Inventory_Add where Inventory_Id='" + inventoryId.ToString() + "'";
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

        #region Incidents
        public IActionResult Incidents()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            ViewBag.SubmitValue = "Save";
            return View();
        }
        [HttpPost]
        public IActionResult SaveIncident(Incidents incident)
        {
            string UserId = HttpContext.Session.GetString("userId");
            string sqlquery;
            //int status_value;
            try
            {
                //if (petparent.Status == true)
                //{
                //    status_value = 1;
                //}
                //else
                //{
                //    status_value = 0;
                //}
                if (incident.Incident_id == 0)
                {
                    ViewBag.SubmitValue = "SAVE";
                    sqlquery = "Insert into MST_Incident(DateofIncident,TimeOfIncident,CaseType,IncidentReportedTo,PatientImpocted,Notes,CreateUser,CreateDate)values('" + incident.DateofIncident + "','" + incident.TimeOfIncident + "','" + incident.CaseType + "','" + incident.IncidentReportedTo + "','" + incident.PatientImpocted + "','" + incident.Notes + "','" + UserId + "',getdate())";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Incident added successfully.";
                    }
                    else
                    {
                        ViewBag.Message = "Incident not added";
                    }
                  
                }
                else
                {
                    //ViewBag.SubmitValue = "UPDATE";
                    sqlquery = "UPDATE MST_Incident SET DateofIncident = '" + incident.DateofIncident + "', TimeOfIncident = '" + incident.TimeOfIncident + "', CaseType ='" + incident.CaseType + "', IncidentReportedTo='" + incident.IncidentReportedTo + "',PatientImpocted='" + incident.PatientImpocted + "' ,Notes='" + incident.Notes + "',ModifyUser='" + UserId + "',ModifyDate=getdate() where Incident_id=" + incident.Incident_id + "";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Incident modify successfully.";
                    }
                    else
                    {
                        ViewBag.Message = "Incident not modify";
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View("Incidents");
        }
        [HttpGet]
        public JsonResult ShowIncidentDetails()
        {
                string sql = "select Incident_id ,DateofIncident,TimeOfIncident,CaseType,IncidentReportedTo,PatientImpocted,Notes from MST_Incident";
                DataSet ds = util.TableBind(sql);
                List<Incidents> IncidentDetails = new List<Incidents>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    IncidentDetails.Add(new Incidents
                    {
                        Incident_id = Convert.ToInt32(dr["Incident_id"]),
                        DateofIncident = Convert.ToString(dr["DateofIncident"]),
                        TimeOfIncident = Convert.ToString(dr["TimeOfIncident"]),
                        CaseType = Convert.ToString(dr["CaseType"]),
                        IncidentReportedTo = Convert.ToString(dr["IncidentReportedTo"]),
                        PatientImpocted = Convert.ToString(dr["PatientImpocted"]),
                        Notes = Convert.ToString(dr["Notes"])
                    });
                }
                return Json(IncidentDetails);
        }
        [HttpPost]
        public JsonResult DeleteIncidentById(int Incident_id)
        {
            string messagge = string.Empty;
            string Sqlquery = "Delete From MST_Incident where Incident_id='" + Incident_id.ToString() + "'";
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
        [HttpPost]
        public JsonResult EditIncidentById(int Incident_id)
        {
            Incidents IncidentDetails = new Incidents();
          
                string sql = "select Incident_id ,DateofIncident,TimeOfIncident,CaseType,IncidentReportedTo,PatientImpocted,Notes from MST_Incident WHERE Incident_id= " + Incident_id + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    IncidentDetails.Incident_id = Convert.ToInt32(dr["Incident_id"]);
                    IncidentDetails.DateofIncident = dr["DateofIncident"].ToString();
                    IncidentDetails.TimeOfIncident = dr["TimeOfIncident"].ToString();
                    IncidentDetails.CaseType = dr["CaseType"].ToString();
                    IncidentDetails.IncidentReportedTo = dr["IncidentReportedTo"].ToString();
                    IncidentDetails.PatientImpocted = dr["PatientImpocted"].ToString();
                    IncidentDetails.Notes = dr["Notes"].ToString();
                }
                ViewBag.SubmitValue = "Update";
            //var Data = new {doctorDetails= doctorDetails };
            return Json(IncidentDetails);
        }
        #endregion

        #region Imaging
        public IActionResult ImagingRequest()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
           // PopulatePetName();
            PopulateAppointmentType();
            PopulateImgType();
            return View();
        }
        [HttpPost]
        public IActionResult SaveImaginRequest(ImagingRequest img)
        {
            string UserId = HttpContext.Session.GetString("userId");
            //string sqlquery;
            //int status_value;
            try
            {
                //if (petparent.Status == true)
                //{
                //    status_value = 1;
                //}
                //else
                //{
                //    status_value = 0;
                //}
                if (img.Imaging_Id == 0)
                {
                    ViewBag.SubmitValue = "SAVE";
                    sqlquery = "insert into MST_Imaging(Pet_Id,Appointment_type,Imaging_Type,Imaging_Radiologist,Requested_By,Imaging_Result,Imaging_Notes,CreateUser,CreateDate,PetParent_Id,OtherImaging_Type)values('" + img.PetName + "','" + img.Appointment_type + "','" + img.Imaging_Type + "','" + img.Imaging_Radiologist + "','" + img.Requested_By + "','" + img.Imaging_Result + "','"+img.Imaging_Notes+"','" + UserId + "',convert(datetime,'" + System.DateTime.Now.ToString() + "',103),'" + img.PetParent_Id+"','"+img.OtherImaging_Type + "')";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        dt = util.execQuery("Select max(Imaging_Id) from MST_Imaging ");
                        sqlquery = util.MultipleTransactions("insert into His_Imaging(Imaging_Id,Pet_Id,Appointment_type,Imaging_Type,Imaging_Radiologist,Requested_By,Imaging_Result,Imaging_Notes,CreateUser,CreateDate,ModifyDatePetParent_Id,OtherImaging_Type) VALUES ('" + dt.Rows[0][0].ToString() + "','" + img.PetName + "','" + img.Appointment_type + "','" + img.Imaging_Type + "','" + img.Imaging_Radiologist + "','" + img.Requested_By + "','" + img.Imaging_Result + "','" + img.Imaging_Notes + "','" + UserId + "',convert(datetime,'" + System.DateTime.Now.ToString() + "',103),convert(datetime,'" + System.DateTime.Now.ToString() + "',103),'" + img.PetParent_Id + "','" + img.OtherImaging_Type + "')");
                        ModelState.Clear();
                        ViewBag.Message = "Imaging request added successfully.";
                        msg= "Imaging request added successfully.";
                    }
                    else
                    {
                        ViewBag.Message = "Imaging request not added.";
                        msg= "Imaging request not added.";
                    }

                }
                else
                {
                    //ViewBag.SubmitValue = "UPDATE";
                    sqlquery = "UPDATE MST_Imaging SET Pet_Id='" + img.PetName + "',Appointment_type='" + img.Appointment_type + "',Imaging_Type='" + img.Imaging_Type+ "',Imaging_Radiologist='" + img.Imaging_Radiologist + "',Requested_By='" + img.Requested_By + "',Imaging_Result='" + img.Imaging_Result + "',Imaging_Notes='" + img.Imaging_Notes + "',ModifyUser='" + UserId + "',ModifyDate=getdate() , PetParent_Id='"+img.PetParent_Id + "' where Imaging_Id=" + img.Imaging_Id + "";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        dt = util.execQuery("Select max(Imaging_Id) from MST_Imaging ");
                        sqlquery = util.MultipleTransactions("insert into His_Imaging(Imaging_Id,Pet_Id,Appointment_type,Imaging_Type,Imaging_Radiologist,Requested_By,Imaging_Result,Imaging_Notes,CreateUser,CreateDate,ModifyDatePetParent_Id,OtherImaging_Type) VALUES ('" + dt.Rows[0][0].ToString() + "','" + img.PetName + "','" + img.Appointment_type + "','" + img.Imaging_Type + "','" + img.Imaging_Radiologist + "','" + img.Requested_By + "','" + img.Imaging_Result + "','" + img.Imaging_Notes + "','" + UserId + "',convert(datetime,'" + System.DateTime.Now.ToString() + "',103),convert(datetime,'" + System.DateTime.Now.ToString() + "',103),'" + img.PetParent_Id + "','" + img.OtherImaging_Type + "')");
                        ModelState.Clear();
                        ViewBag.Message = "Imaging request modify.";
                        msg = "Imaging request modify.";
                    }
                    else
                    {
                        ViewBag.Message = "Imaging request Not modify.";
                        msg= "Imaging request Not modify.";
                    }
                }
            }
            catch (Exception ex)
            {

                finalMsg = ex.Message;
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + msg + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "ImagingRequest.cshtml", "", "App", "Function Name ===>'SaveImaginRequest'");
            return View("ImagingRequest");
        }
        [HttpGet]
        public JsonResult ShowDataImgRequested()
        {
                //string sql = "select Imaging_Id,Pet_Id,Appointment_type,Imaging_Type,Imaging_Radiologist,Requested_By,Imaging_Result,Imaging_Notes,PetParent_Id from MST_Imaging";
                string sql = "select Img.Imaging_Id,Img.Pet_Id,Img.Appointment_type,Img.Imaging_Type,Img.Imaging_Radiologist,Img.Requested_By,Img.Imaging_Result,Img.Imaging_Notes,Img.PetParent_Id , Msp.PetName , atyp.AppointmentTypeName,imgtype.ImgTypeName ,convert(varchar, Img.CreateDate,103) as CreateDate from MST_Imaging Img join MST_Pet Msp on Img.Pet_Id = Msp.Pet_Id join AppointmentType atyp on atyp.AppointmentTypeId = Img.Appointment_type join Cma_view_ImagingType imgtype on imgtype.ImgTypeId= Img.Imaging_Type ";
                DataSet ds = util.TableBind(sql);
                List<ImagingRequest> img = new List<ImagingRequest>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    img.Add(new ImagingRequest
                    {
                        Imaging_Id = Convert.ToInt32(dr["Imaging_Id"]),
                        Requested_By = Convert.ToString(dr["Requested_By"]),
                        PetName = Convert.ToString(dr["PetName"]),
                        Imaging_Type = Convert.ToString(dr["ImgTypeName"]),
                        Imaging_Notes = Convert.ToString(dr["Imaging_Notes"]),
                        PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]),
                        DateRequested = Convert.ToString(dr["CreateDate"]),
                    }); 
                }
                return Json(img);
        }
        [HttpPost]
        public JsonResult DeleteImagingById(string imaging_Id)
        {
            string messagge;
            string Sqlquery = "Delete From MST_Imaging where Imaging_Id='" + imaging_Id + "'";
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
        [HttpPost]
        public JsonResult EditImgRequestById(int imaging_Id)
        {
            ImagingRequest Img = new ImagingRequest();
                string sql = "select img.Imaging_Id, img.Pet_Id, img.Appointment_type, img.Imaging_Type, img.Imaging_Radiologist, img.Requested_By, img.Imaging_Result, img.Imaging_Notes, img.PetParent_Id , atype.AppointmentTypeName  from MST_Imaging img join AppointmentType atype on atype.AppointmentTypeId = img.Appointment_type WHERE Imaging_Id= " + imaging_Id + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Img.Imaging_Id = Convert.ToInt32(dr["Imaging_Id"]);
                    Img.PetName = dr["Pet_Id"].ToString();
                    Img.Appointment_type = Convert.ToInt32(dr["Appointment_type"]);
                    Img.Appointment_type_Name = dr["AppointmentTypeName"].ToString();
                   Img.Imaging_Type = dr["Imaging_Type"].ToString();
                    Img.Imaging_Radiologist = dr["Imaging_Radiologist"].ToString();
                    Img.Requested_By = dr["Requested_By"].ToString();
                    Img.Imaging_Result = dr["Imaging_Result"].ToString();
                    Img.Imaging_Notes = dr["Imaging_Notes"].ToString();
                    Img.PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]);
                }
                ViewBag.SubmitValue = "Update";
            //var Data = new {doctorDetails= doctorDetails };
            return Json(Img);
        }
        public IActionResult ImagingComingSoon()
        {
            return View();
        }

        #endregion

        #region Lab
        public IActionResult Labs()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            PopulateAppointmentType();
            PopulateLabType();
            return View();
        }
        [HttpPost]
        public IActionResult SaveAndUpdateLabs(Labs lab)
        {
            string UserId = HttpContext.Session.GetString("userId");
            string sqlquery;
            string status;
            try
            {
                if (lab.Lab_Id == 0)
                {
                    sqlquery = "insert into MST_Labs (Pet_id,Appointment_type,LabType_id,Lab_Notes,CreateUser,CreateDate,PetParent_Id)values('" + lab.PetName + "','" + lab.Appointment_type + "','" + lab.LabType +"','"+lab.Lab_Notes+"','"+ UserId + "',getdate(),'"+lab.PetParent_Id+"')";
                     status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Lab request added successfully.";
                    }
                    else
                    {
                        ViewBag.Message = "Lab request not addedd";
                    }
                }
                else
                {
                    sqlquery = "Update MST_Labs Set Pet_id='" + lab.PetName + "',Appointment_type='"+lab.Appointment_type+ "',LabType_id='" + lab.LabType+"',Lab_Notes='"+lab.Lab_Notes+ "' ,ModifyUser='" + UserId + "',ModifyDate=getdate(),PetParent_Id='" + lab.PetParent_Id + "' where Lab_Id=" + lab.Lab_Id+ "";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Lab request modify successfully";
                    }
                    else
                    {
                        ViewBag.Message = "Lab request not modify";
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            
            return View("Labs");
        }
        [HttpGet]
        public JsonResult ShowLabData()
        {
            string sqlquery;
            List<Labs> lab = new List<Labs>();
           
                sqlquery = "SELECT lab.Lab_Id,isnull(lab.Pet_id,0)as Pet_id , isnull(lab.Appointment_type,0)as Appointment_type,isnull(lab.LabType_id,'')as LabType_id ,isnull(lab.Lab_Notes,'')as Lab_Notes ,convert(varchar ,lab.CreateDate,103)as CreateDate , isnull(pet.PetName,'') as PetName , isnull(mpp.FullName,'')as FullName , isnull(Lt.LabTypeName,'')as LabTypeName  From MST_Labs lab  join MST_Pet pet on pet.Pet_id = lab.Pet_id join MST_PetParent mpp on mpp.PetParent_Id = lab.PetParent_Id Join Cma_ViewLabType Lt on Lt.LabTypeId = lab.LabType_id ";
                DataSet ds = util.TableBind(sqlquery);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lab.Add(new Labs
                    {
                        Lab_Id = Convert.ToInt32(dr["Lab_Id"]),
                        PetName = Convert.ToString(dr["PetName"]),
                        Appointment_type = Convert.ToInt32(dr["Appointment_type"]),
                        LabType = Convert.ToString(dr["LabTypeName"]),
                        Lab_Notes = Convert.ToString(dr["Lab_Notes"]),
                        PetParent_Name = Convert.ToString(dr["FullName"]),
                        DateRequested = Convert.ToString(dr["CreateDate"]),
                    });
                }
            return Json(lab);
        }
        [HttpPost]
        public JsonResult EditLabData(int lab_Id)
        {
            string sqlquery;
            Labs lab = new Labs();
            sqlquery = "SELECT lab.Lab_Id,isnull(lab.Pet_id,0)as Pet_id , isnull(lab.Appointment_type,0)as Appointment_type,isnull(lab.LabType_id,'')as LabType_id ,isnull(lab.Lab_Notes,'')as Lab_Notes ,convert(varchar ,lab.CreateDate,103)as CreateDate , isnull(pet.PetName,'') as PetName , isnull(mpp.FullName,'')as FullName , isnull(Lt.LabTypeName,'')as LabTypeName,lab.PetParent_Id ,isnull(atype.AppointmentTypeName,'') as AppointmentTypeName   From MST_Labs lab  join MST_Pet pet on pet.Pet_id = lab.Pet_id join MST_PetParent mpp on mpp.PetParent_Id = lab.PetParent_Id Join Cma_ViewLabType Lt on Lt.LabTypeId = lab.LabType_id join AppointmentType atype on atype.AppointmentTypeId = lab.Appointment_type where Lab_id='" + lab_Id + "'";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lab.Lab_Id = Convert.ToInt32(dr["Lab_Id"]);
                lab.PetName = Convert.ToString(dr["Pet_id"]);
                lab.PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]);
                lab.Appointment_type = Convert.ToInt32(dr["Appointment_type"]);
                lab.Appointment_type_Name = dr["AppointmentTypeName"].ToString();
                lab.LabType = dr["LabType_id"].ToString();
                lab.Lab_Notes = dr["Lab_Notes"].ToString();
            }
            return Json(lab);
        }
        [HttpPost]
        public JsonResult DeleteLabDataById(int lab_Id)
        {
            string messagge;
            string Sqlquery = "Delete From MST_Labs where Lab_Id='" + lab_Id + "'";
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

        public IActionResult LabComingSoon()
        {
            return View();
        }
        #endregion

        #region Report
        public IActionResult LabReport(string Lab_Id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");

            }
            ViewBag.SubmitValue = "SAVE";
            // string userid = HttpContext.Session.GetString("userId");
            string sql;
            Report Lab = new Report();
            // Lab.Age = Lab.Pet_DOB + '/' + Lab.PetGender;
            
                sql = "exec SP_SEL_LAB_REPORT " + Lab_Id + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Lab.Lab_Id = Convert.ToInt32(dr["Lab_Id"]);
                    Lab.Pet_DOB = ds.Tables[0].Rows[0]["Pet_DOB"].ToString();
                    Lab.GenderName = ds.Tables[0].Rows[0]["GenderName"].ToString();
                    Lab.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                    Lab.PetName = ds.Tables[0].Rows[0]["PetName"].ToString();
                    Lab.PetBreedName = ds.Tables[0].Rows[0]["PetBreedName"].ToString();
                    Lab.Mobile_No = ds.Tables[0].Rows[0]["Mobile_No"].ToString();
                    Lab.Email = ds.Tables[0].Rows[0]["Email"].ToString();

                }

           
            return View("LabReport", Lab);
        }

        #endregion

        #region Billing
        public IActionResult Billing()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            return View();
        }
       
        public IActionResult NewInvoice(int Appointment_id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            DataSet ds;
            string[] Medication;
            PopulatePetParentName();
            PopulatePetName();
            PopulateAppointmentType();
            NewInvoice NewVoice =new  NewInvoice();
            if (Appointment_id !=0)
            {
                sqlquery = "exec Sp_SelAppointment_for_Invoice @Appointment_id='" + Appointment_id + "'";
                ds = util.TableBind(sqlquery);
                Medication = (ds.Tables[0].Rows[0]["Medication"].ToString()).Split(',');
                int index = Medication.Count();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NewVoice.Appointment_id = (ds.Tables[0].Rows[0]["Appointment_id"]).ToString();
                    NewVoice.InvoiceTo = (ds.Tables[0].Rows[0]["PetParent_Id"]).ToString();
                    NewVoice.PetName = (ds.Tables[0].Rows[0]["Pet_Id"]).ToString();
                    NewVoice.AdmissionDate = Convert.ToString(ds.Tables[0].Rows[0]["Appointment_Date"]);
                    NewVoice.AppointmentType = Convert.ToString(ds.Tables[0].Rows[0]["Appointment_Type"]);
                    NewVoice.Descriptions = Convert.ToString(Medication[3]);
                }
            }
            return View("NewInvoice",NewVoice);
        }
        [HttpGet]
        public JsonResult GetPetAppointment(int id)
        {
            NewInvoice NewVoice =new  NewInvoice();
           
                string sqlquery = "select PetS_ParentName , Appointment_Type , Appointment_Date from trn_PetAppointment where PetName='" + id + "'";
                DataSet ds = util.TableBind(sqlquery);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NewVoice.AdmissionDate = dr["Appointment_Date"].ToString();
                    NewVoice.AppointmentType = Convert.ToString(dr["Appointment_Type"]);
                }
            return Json(NewVoice);
        }
        [HttpPost]
        public JsonResult SaveNewInvoice(string jsonInput , string Invoice_ID , string Patient_ID , string PetParentId , string PetParentName, string PetNameId , string PetName , string AdmissionDate , string AppointmentTypeId , string AppointmentTypeText , string Remarks, string grandtotal,string paymentJson)
        {
          DataTable dt= util.JsonStringToDataTable(jsonInput);
            DataTable dt2 = util.JsonStringToDataTable(paymentJson);
            string UserId = HttpContext.Session.GetString("userId");
            string sqlQuery = string.Empty;
            string status= string.Empty;
            int row = dt.Rows.Count;
            int col = dt.Columns.Count;
            int i=0;
            string messagge= string.Empty;
            try
            {
                for (i=0;i<row;i++)
                {
                    sqlQuery += "insert into MST_NewInvoice(PatientID,PetParentId,PetParentName,PetId,PetName,AdmissionDate,AppointmentTypeId,AppointmentType,Descriptions,Quantity,perQuantityPrice,ActualCharges,Discount,TotalRupees,GrandTotal,Remarks,CreateUser,CreateDate,PaymentMode,PaymentAmount,PaidOn,TransactionID,PaymentNotes)values('" + Patient_ID + "','" + PetParentId + "','" + PetParentName + "','" + PetNameId + "','" + PetName + "','" + AdmissionDate + "','" + AppointmentTypeId + "','" + AppointmentTypeText + "','" + dt.Rows[i]["Descriptions"].ToString() + "','" + dt.Rows[i]["Qty"].ToString() + "','" + dt.Rows[i]["PerQtyPrice"].ToString() + "','" + dt.Rows[i]["ActualCharges"].ToString() + "','" + dt.Rows[i]["Discount"].ToString() + "','" + dt.Rows[i]["Total"].ToString() + "','" + grandtotal + "','" + Remarks + "','" + UserId + "',getdate(),'"+dt2.Rows[0]["PaymentType"] + "','"+dt2.Rows[0]["Amount"] + "','" + dt2.Rows[0]["Date"] + "','" + dt2.Rows[0]["TransactionId"] + "','" + dt2.Rows[0]["PaymentNotes"] + "')";
                    
                }
                status = util.MultipleTransactions(sqlQuery);
                if (status == "Successfull")
                {
                    messagge = "Invoice added";
                }
                else
                {
                    
                }

            }
            catch (Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + messagge + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "NewInvoice.cshtml", "", "App", "Function Name ===>'SaveNewInvoice '");
            var Data = new { msg = messagge };
            return Json(Data);
        }
        //[HttpGet]
        //public JsonResult GetDetailsForInvoice(int id)
        //{
        //    List<NewInvoice> invoice = new List<NewInvoice>();
        //    try
        //    {
        //        string sql = "exec Sp_Sel_PatientDetails_for_Invoice "+id+"";
        //        DataSet ds = util.TableBind(sql);
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            invoice.Add(new NewInvoice
        //            {
        //               // PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]),
        //               // PetName = Convert.ToString(dr["PetName"]),
        //                AdmissionDate = Convert.ToString("Appointment_Date"),
        //                AppointmentType = Convert.ToString(dr["Appointment_Time"]),
        //                Pharmacy_name = Convert.ToString(dr["Pharmacy_name"]),
        //                Quantity = Convert.ToInt32(dr["Quantity"])

        //            });
                   
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return Json(invoice);
        //}
        [HttpGet]
        public JsonResult GetInvoiceDetails()
        {
            List<NewInvoice> invoice = new List<NewInvoice>();
            string sql = "Select * from MST_NewInvoice";
            DataSet ds = util.TableBind(sql);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                invoice.Add(new NewInvoice
                {
                   

                });
            }

            return Json("");
        }
        [HttpPost]
        public JsonResult AddPayment(string jsondata)
        {
            DataTable dt = util.JsonStringToDataTable(jsondata);
            string UserId = HttpContext.Session.GetString("userId");
            string sqlquery = string.Empty;
            string status = string.Empty;
            int row = dt.Rows.Count;
            int col = dt.Columns.Count;
            int i = 0;
            string statement;
            string messagge = string.Empty;
            try
            {
                if (dt.Rows.Count > 0)
                {
                    statement = "INSERT";
                    sqlquery = "exec sp_trn_Payment '"+ statement + "', " + dt.Rows[0]["PetParentId"] + ",'" + dt.Rows[0]["PetParentName"] + "'," + dt.Rows[0]["PetId"] + ",'" + dt.Rows[0]["PetName"] + "'," + dt.Rows[0]["PaymentModeId"] + ",'" + dt.Rows[0]["PaymentMode"] + "','" + dt.Rows[0]["Amount"] + "', '" + dt.Rows[0]["PaidOn"] + "','" + dt.Rows[0]["TransactionId"] + "','" + dt.Rows[0]["Notes"] + "','" + UserId + "'";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        messagge = "Payment added";
                    }
                    else
                    {

                    }
                }

            }
            catch(Exception ex)
            {
                finalMsg = ex.Message;
            }
            finalStatus =
         "Status-" + finalMsg + ",Message-" + messagge + "";
            util.WriteLogFile("Final transaction Detail ===> " + finalStatus + ",  Query  ===> " + sqlquery + ",Total Fetch Data ===> ", ",Fetch Data List ===> ", "", " Select query", "NewInvoice.cshtml", "", "App", "Function Name ===>'AddPayment'");
            var Data = new { msg = messagge };
            return Json(Data);
        }
        public IActionResult InvoiceView()
        {
            return View();
        }
        #endregion

        #region Pricing
        public IActionResult Pricing()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");
            }
            PopulatePriceCategory();
            return View();
        }

        [HttpPost]
        public JsonResult SaveUpdatePricing(int id ,string ItemName, string PriceCategory, string Price)
        {
            string UserId = HttpContext.Session.GetString("userId");
            string sqlquery;
            string messagge = string.Empty;
            try
            {
                if (id == 0)
                {
                    sqlquery = "insert into MST_Pricing (ItemName,Category,price,CreateUser,CreateDate)values('" + ItemName + "','" + PriceCategory + "','" + Price + "','" + UserId + "',getdate())";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        messagge = "Success";
                        //ModelState.Clear();
                        //ViewData["msg"] = " Dispense Pharmacy Request Success";
                    }
                }
                else
                {
                    sqlquery = "Update MST_Pricing SET ItemName= '" + ItemName + "',Category='" + PriceCategory + "',price='" + Price + "',ModifyUser='" + UserId + "',ModifyDate=getdate() where Pricing_Id =" + id + "";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        messagge = "Success";
                        //ModelState.Clear();
                        //ViewData["msg"] = " Dispense Pharmacy Request Success";
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            var Data = new { msg = messagge };
            return Json(Data);

        }
        [HttpGet]
        public JsonResult GetAllDataofPricing()
        {
           
                string sql = "select mstp.Pricing_Id,mstp.ItemName,mstp.price,pc.CategoryName from MST_Pricing mstp Join pricecategory pc on pc.CategoryId = mstp.Category";
                DataSet ds = util.TableBind(sql);
                List<Pricing> Price = new List<Pricing>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Price.Add(new Pricing
                    {
                        Pricing_Id = Convert.ToInt32(dr["Pricing_Id"]),
                        ItemName = Convert.ToString(dr["ItemName"]),
                        Category = Convert.ToString(dr["CategoryName"]),
                        price = Convert.ToString(dr["price"]),
                    });
                }
                return Json(Price);
           
        }
        [HttpPost]
        public JsonResult EditPricingById(int pricing_Id)
        {
            Pricing price = new Pricing();
            ViewBag.SubmitValue = "Update";
         
                string sql = "select Pricing_Id,ItemName,price,Category from MST_Pricing  WHERE Pricing_Id= " + pricing_Id + "";
                DataSet ds = util.TableBind(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    price.Pricing_Id = Convert.ToInt32(dr["Pricing_Id"]);
                    price.ItemName = dr["ItemName"].ToString();
                    price.Category = dr["Category"].ToString();
                    price.price = dr["price"].ToString();
                }
         
            return Json(price);
        }
        [HttpPost]
        public JsonResult DeletePricingById(int pricing_Id)
        {
            string messagge;
            string Sqlquery = "Delete From MST_Pricing where Pricing_Id='" + pricing_Id + "'";
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

        [HttpGet]
        public JsonResult ImagingPriceDataShow()
        {
           
                string sql = "select mstp.Pricing_Id,mstp.ItemName,mstp.price,pc.CategoryName from MST_Pricing mstp Join pricecategory pc on pc.CategoryId = mstp.Category where pc.CategoryName = 'Imaging'";
                DataSet ds = util.TableBind(sql);
                List<Pricing> Price = new List<Pricing>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Price.Add(new Pricing
                    {
                        Pricing_Id = Convert.ToInt32(dr["Pricing_Id"]),
                        ItemName = Convert.ToString(dr["ItemName"]),
                        Category = Convert.ToString(dr["CategoryName"]),
                        price = Convert.ToString(dr["price"]),
                    });
                }
                return Json(Price);
           
        }
        [HttpGet]
        public JsonResult LabPriceDataShow()
        {
           
                string sql = "select mstp.Pricing_Id,mstp.ItemName,mstp.price,pc.CategoryName from MST_Pricing mstp Join pricecategory pc on pc.CategoryId = mstp.Category where pc.CategoryName = 'Lab'";
                DataSet ds = util.TableBind(sql);
                List<Pricing> Price = new List<Pricing>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Price.Add(new Pricing
                    {
                        Pricing_Id = Convert.ToInt32(dr["Pricing_Id"]),
                        ItemName = Convert.ToString(dr["ItemName"]),
                        Category = Convert.ToString(dr["CategoryName"]),
                        price = Convert.ToString(dr["price"]),
                    });
                }
                return Json(Price);
        }
        [HttpGet]
        public JsonResult ProcedurePriceDataShow()
        {
           
                string sql = "select mstp.Pricing_Id,mstp.ItemName,mstp.price,pc.CategoryName from MST_Pricing mstp Join pricecategory pc on pc.CategoryId = mstp.Category where pc.CategoryName = 'Procedure'";
                DataSet ds = util.TableBind(sql);
                List<Pricing> Price = new List<Pricing>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Price.Add(new Pricing
                    {
                        Pricing_Id = Convert.ToInt32(dr["Pricing_Id"]),
                        ItemName = Convert.ToString(dr["ItemName"]),
                        Category = Convert.ToString(dr["CategoryName"]),
                        price = Convert.ToString(dr["price"]),
                    });
                }
                return Json(Price);
        }
        [HttpGet]
        public JsonResult WordPriceDataShow()
        {
           
                string sql = "select mstp.Pricing_Id,mstp.ItemName,mstp.price,pc.CategoryName from MST_Pricing mstp Join pricecategory pc on pc.CategoryId = mstp.Category where pc.CategoryName = 'Word'";
                DataSet ds = util.TableBind(sql);
                List<Pricing> Price = new List<Pricing>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Price.Add(new Pricing
                    {
                        Pricing_Id = Convert.ToInt32(dr["Pricing_Id"]),
                        ItemName = Convert.ToString(dr["ItemName"]),
                        Category = Convert.ToString(dr["CategoryName"]),
                        price = Convert.ToString(dr["price"]),
                    });
                }
                return Json(Price);
        }

        #endregion

        #region Setting
        public IActionResult Setting(User UserMaster)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("firstname")))
            {
                return RedirectToAction("SignIn", "User");

            }
            ViewBag.SubmitValue = "SAVE";
            string userid = HttpContext.Session.GetString("userId");
            string sqlQuery;
            //List<User> UserMaster = new List<User>();
            try
            {
                //sqlQuery = "SELECT id, Firstname, Email,Mobile_No,City,Address from tbluser where id='" + userid + "'";
                sqlQuery = "select isnull(signUp_Id,'') as signUp_Id , isnull(FullName,'')as FullName , isnull(Email,'') as Email, isnull(Mobile_No,'')as Mobile_No , isnull(City,'')as City , isnull(Address,'') as Address from tblSignUp where signUp_Id='" + userid + "'";
                DataSet ds = util.TableBind(sqlQuery);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserMaster.id = Convert.ToInt32(dr["signUp_Id"]);
                    UserMaster.Firstname = Convert.ToString(dr["FullName"]);
                    UserMaster.Email = Convert.ToString(dr["Email"]);
                    UserMaster.Mobile_No = Convert.ToString(dr["Mobile_No"]);
                    UserMaster.City = Convert.ToString(dr["City"]);
                    UserMaster.Address = Convert.ToString(dr["Address"]);
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return View("Setting", UserMaster);
        }
        [HttpPost]
        public IActionResult ChangePassword(User model)
        {
            //ViewBag.SubmitValue = "SAVE";
            //string UserId = HttpContext.Session.GetString("LoginId");
            string LoginId = HttpContext.Session.GetString("userId");

            string message = "";
            //if (ModelState.IsValid)
            //{
                DataTable dt = util.execQuery("select count(*) total from tblSignUp where Pasword='" + util.cryption(model.Password.Trim().Replace(" ", "")) + "' and signUp_Id='" + LoginId + "' ");
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["total"]) > 0)
                    {
                        string NewPassword = model.NewPassword.Replace(" ", "");
                        string ConfirmPassword = model.ConfirmPassword.Replace(" ", "");
                        if (NewPassword == ConfirmPassword)
                        {
                            //message = util.MultipleTransactions("update tbluser set Password='" + NewPassword + "' where id='" + UserId + "'" );
                            message = ("update tblSignUp set Pasword='" + util.cryption(NewPassword) + "' where signUp_Id='" + LoginId + "'");
                            message = util.MultipleTransactions(message);
                            if (message == "Successfull")
                            {
                                ModelState.Clear();
                                message = "Password Update succesfully";
                                return RedirectToAction("SignIn", "User");
                            }
                        }
                        else
                        {
                            message = "Password and Confirm Password must match!";
                        }
                    }
                    else
                    {
                        message = "Old Password doest not exist!";
                    }
                }
                else
                {
                    message = "Old Password doest not exist!";
                }
                ViewBag.Message = message;
            //}

            return View("Setting", model);
        }
        [HttpPost]
        public IActionResult AccountUpdate(User model)
        {
            ViewBag.SubmitValue = "Update";

            string id = HttpContext.Session.GetString("LoginId");
            sqlquery = "update tblSignUp set FullName = '" + model.Firstname + "',Email='" + model.Email + "', City='"+model.City+ "' , Address = '"+model.Address+ "',Mobile_No='"+model.Mobile_No+"' where signUp_Id = '" + model.id + "'";
            string status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {

                ViewBag.Message = "Account Update Success";
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "Account Not Update";
            }

            return View("Setting", model);
        }
        #endregion

        #region MarketPlace
        public IActionResult MarketPlace()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveMarketPlace(MarketPlace market)
        {
            string sqlquery;
            string UserId = HttpContext.Session.GetString("userId");
            try
            {
                if (market.ItemImg != null)
                {
                    if (market.ItemImg.Length > 0)
                    {
                        string filename = Path.GetFileNameWithoutExtension(market.ItemImg.FileName);
                        string extension = Path.GetExtension(market.ItemImg.FileName);
                        market.product_Img = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine("wwwroot/MedicineImages/", filename);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            market.ItemImg.CopyTo(fileStream);
                        }
                    }
                }
                else
                {
                    market.product_Img = "noPhoto.png";
                }
                if(market.MaketPlace_Id==0)
                {
                    sqlquery = "INSERT into MST_MarketPlace (ItemName,Market_Category,Min_Quantitiy,Price,product_Img,CreateUser,CreateDate)values('"+market.ItemName+"','"+market.Market_Category+"','"+market.Min_Quantitiy+"','"+market.Price+"','"+market.product_Img+ "','" + UserId + "',getdate())";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Marketplace added successfully.";
                    }
                    else
                    {
                        ViewBag.Message = "Marketplace not added";
                    }
                }
                else
                {
                    sqlquery= "UPDATE MST_MarketPlace SET ItemName='"+market.ItemName+"',Market_Category='"+market.Market_Category+"',Min_Quantitiy='"+market.Min_Quantitiy+"',Price='"+market.Price+"',product_Img='"+market.product_Img+"',ModifyUser='"+UserId+"',ModifyDate=getdate()";
                    string status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Marketplace modify successfully.";
                    }
                    else
                    {
                        ViewBag.Message = "Marketplace not modify.";
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View("MarketPlace");
        }
        [HttpGet]
        public JsonResult showMarketPlacedata()
        {
            List<MarketPlace> market = new List<MarketPlace>();
            try
            {
                string sql = "select MaketPlace_Id,ItemName,Market_Category,Min_Quantitiy,Price,product_Img from MST_MarketPlace";
                DataSet ds = util.TableBind(sql);
                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    market.Add(new MarketPlace
                    {
                        MaketPlace_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["MaketPlace_Id"]),
                        ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString(),
                        Market_Category = ds.Tables[0].Rows[0]["Market_Category"].ToString(),
                        Min_Quantitiy = ds.Tables[0].Rows[0]["Min_Quantitiy"].ToString(),
                        Price= ds.Tables[0].Rows[0]["Price"].ToString()
                    });
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Json(market);
        }
        #endregion

        #region Breed
        public IActionResult Breed()
        {
            PopulateCountry();
            return View();
        }
        #endregion

        #region Common Master (MST_CMA)
        public IActionResult MST_CMA()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveMST_CMA(MST_Cma cma)
        {
            string UserId = HttpContext.Session.GetString("userId");
            string sqlquery;
            int st_value;
            if (cma.cma_Status == true)
            {
                st_value = 1;
            }
            else
            {
                st_value = 0;
            }
            if (cma.cma_id==0)
            {
                sqlquery = "Insert into MST_CMA(cma_name,cma_shortname,cma_parent,cma_type,cma_Status,cma_category,CreateUser,CreateDate)values('" + cma.cma_name+ "','" +cma.cma_shortname+ "','" + cma.cma_parent + "','" + cma.cma_type + "','" + st_value + "','"+cma.cma_category+"','" + UserId + "',getdate())";
                string status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    ModelState.Clear();
                    ViewBag.Message = "Master added SuccessFully!!";
                }
                else
                {
                    ViewBag.Message = "Master Not addedd";
                }
            }
            
            return View("MST_CMA");
        }
        #endregion

        #region Excel
        public IActionResult ExcelImport()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ExcelImport(MST_Cma cmaObj)
        {
            if (cmaObj.fileUpload != null)
            {
                if (cmaObj.fileUpload.Length > 0)
                {
                    //string filename = Path.GetFileNameWithoutExtension(cmaObj.fileUpload.FileName);
                    string filename = Path.GetFileNameWithoutExtension("AnimalMaster");
                    string extension = Path.GetExtension(cmaObj.fileUpload.FileName);
                    if (extension == ".xlsx")
                    {
                        //cmaObj.file = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        cmaObj.file = filename = filename + extension;
                        //string path = Path.Combine("wwwroot/Excel/", filename);
                        string path = Path.Combine("wwwroot/Excel/", filename);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            cmaObj.fileUpload.CopyTo(fileStream);
                        }
                        string conection_string = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + path + "; Extended 			Properties='Excel 12.0;IMEX=1;HDR=YES;'";
                        OleDbConnection excelcon = new OleDbConnection(conection_string);
                        excelcon.Open();
                        DataTable dtSheet = new DataTable();
                        dtSheet = excelcon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string ExcelSheetName = dtSheet.Rows[0]["Table_name"].ToString();
                        ExcelSheetName = "[" + ExcelSheetName + "]";
                        excelcon.Close();

                        OleDbDataAdapter adp = new OleDbDataAdapter("SELECT * FROM " + ExcelSheetName + " ", conection_string);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                    }

                }
            }
            else
            {

            }
            return View();
        }
        [HttpPost]
        public async Task<List<MST_Cma>> Import(IFormFile fileUpload)
        {
            var list = new List<MST_Cma>();
            using (var stream = new MemoryStream())
            {
                await fileUpload.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["A1"];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 1; row <= rowcount; row++)
                    {
                        list.Add(new MST_Cma
                        {
                            cma_name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            cma_shortname = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            cma_parent = Convert.ToInt32(worksheet.Cells[row, 3].Value.ToString().Trim()),
                            cma_type = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            cma_Status = Convert.ToBoolean(worksheet.Cells[row, 9].Value.ToString().Trim()),
                            cma_category = worksheet.Cells[row, 11].Value.ToString().Trim(),

                        });
                    }
                }
                ViewBag.list = list;
            }
            return ViewBag.list;

        }


        public IActionResult ImportExcelV()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Importexcel1(MST_Cma cmaObj)
        {
            if (cmaObj.fileUpload != null)
            {
                if (cmaObj.fileUpload.Length > 0)
                {
                    //string filename = Path.GetFileNameWithoutExtension(cmaObj.fileUpload.FileName);
                    string filename = Path.GetFileNameWithoutExtension("AnimalMaster");
                    string extension = Path.GetExtension(cmaObj.fileUpload.FileName);
                    if (extension.Trim() == ".xlsx")
                    {
                        //cmaObj.file = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        cmaObj.file = filename = filename + extension;
                        //string path = Path.Combine("wwwroot/Excel/", filename);
                        string path = Path.Combine("wwwroot/Excel/", filename);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            cmaObj.fileUpload.CopyTo(fileStream);
                        }
                        string conection_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        DataTable dt = util.ConvertXSLXtoDataTable(path, conection_string);
                        ViewBag.Data = dt;

                    }
                    else if(extension.Trim()==".xls")
                    {
                        cmaObj.file = filename = filename + extension;
                        //string path = Path.Combine("wwwroot/Excel/", filename);
                        string path = Path.Combine("wwwroot/Excel/", filename);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            cmaObj.fileUpload.CopyTo(fileStream);
                        }
                        string conection_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        DataTable dt = util.ConvertXSLXtoDataTable(path, conection_string);
                        


                        ViewBag.Data = dt;
                    }
                    else
                    {
                        ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                    }

                }
            }
            return View("ImportExcelV");
        }



        #endregion

        #region Patient Details
        public IActionResult PatientDetalis(int petParent_Id)
        {
            string sql;
            Patient patients = new Patient();

            sql = "exec Sp_Sel_Pet_And_PetParent '" + petParent_Id + "'";
            DataSet ds = util.TableBind(sql);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                patients.PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]);
                patients.FullName = dr["FullName"].ToString();
                patients.Gender = dr["Gender"].ToString();
                patients.Mobile_no = dr["Mobile_no"].ToString();
                patients.Email = dr["Email"].ToString();
                patients.Notes = dr["Notes"].ToString();
            }
            ViewBag.Patient = GetPatientsList(petParent_Id);
            return View("PatientDetalis", patients);
        }
        //...............GetPatients.................
        public List<Patient> GetPatientsList(int PetParent_Id)
        {
            List<Patient> Patients = new List<Patient>();
          
                string sql = "select pet_id, PetName,Pet_DOB,Pet_Image from Mst_Pet where PetParent_Id= '" + PetParent_Id + "'";
                DataSet ds = util.TableBind(sql);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    Patients.Add(new Patient
                    {
                        Pet_id = Convert.ToInt32(dr["pet_id"]),
                        Pet_Image = Convert.ToString(dr["Pet_Image"]),
                        PetName = Convert.ToString(dr["PetName"]),
                        Pet_DOB = Convert.ToString(dr["Pet_DOB"]),

                    });

                }
            return Patients;
        }
//.................PatientDetalisById using ajax......

        [HttpPost]
        public JsonResult PatientDetalisById(int pet_id)
        {
            string sql;
            Patient patient = new Patient();
            sql = "exec Sp_Sel_PetDetails_by_Id '" + pet_id + "'";
            DataSet ds = util.TableBind(sql);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                patient.PetName = dr["petName"].ToString();
                patient.PetParent_Id = Convert.ToInt32(dr["PetParent_Id"]);
                patient.Pet_Image = dr["Pet_Image"].ToString();
                patient.Pet_id = Convert.ToInt32(dr["Pet_id"]);
                patient.PetGender = dr["GenderName"].ToString();
                patient.Colour = dr["PetcolourName"].ToString();
                patient.Breed = dr["PetBreedName"].ToString();
                patient.Pet_Weight = Convert.ToInt32(dr["Pet_Weight"]);
                patient.AdoptedOn = dr["AdoptedOn"].ToString();
                patient.Pet_DOB = dr["Pet_DOB"].ToString();
                patient.MicrochipNo = dr["MicrochipNo"].ToString();
                patient.DateOfChipping = dr["DateOfChipping"].ToString();
                patient.Spay_Meutered = dr["Spay_Meutered"].ToString();
                patient.Loc_Chipping = dr["Loc_Chipping"].ToString();
                patient.Insurance = dr["Insurance"].ToString();
                patient.PetNotes = dr["PetNotes"].ToString();
                patient.FullName = dr["PetParentName"].ToString();
                patient.qr = CreateQRCode(patient.Pet_id, patient.PetName , patient.FullName);
            }
            return Json(patient);
        }
        #endregion


        public IActionResult Ph()
        { return View(); }
        public IActionResult Pagination_table()
        {
            return View();
        }
        
        public string CreateQRCode(int Pet_id , string petName , string PetParentName)
        {
            Patient qRCode = new Patient();
            qRCode.Pet_id = Pet_id;
            qRCode.PetName = petName;
            string id =  (qRCode.Pet_id).ToString();
            qRCode.QRCodeText ="PetId = " +id+ ' '+ "PetName = " + petName + "Pet Parent Name = " + PetParentName;
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(qRCode.QRCodeText, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            ViewBag.QrCodeUri = QrUri;
            return QrUri;
        }
    }

    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
