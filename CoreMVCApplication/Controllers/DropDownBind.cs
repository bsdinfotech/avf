using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVCApplication.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace CoreMVCApplication.Controllers
{
    public class DropDownBind : Controller
    {
        ClsUtility util = new ClsUtility();
        public IActionResult Index()
        {
            BindDdl();
           // BindTable();
            //BindEmploymentType();
            //BindBreadType();
            //Bindpreptype();
            //List<TypeMode> masterobj = Populateddl();
            //  return View(new SelectList(masterobj, "TypeMaster_id", "type"));
            return View();

        }

        [HttpPost]
        public IActionResult Index(Short sh)
        {
            int st_value;
            if (sh.status == true)
            {
                st_value = 1;
            }
            else
            {
                st_value = 0;
            }
            string sqlquery = "Insert into short(type_Master,status,entry_date,short_name,Name,ParentId)values('" + sh.type_Master + "', '" + st_value + "', getdate(), '" + sh.short_name + "','" + sh.Name + "','" + sh.ParentId + "')";
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

            return View();
        }
        /*  [HttpPost]
          private static List<TypeMode> Populateddl()
          {
              List<TypeMode> masterobj = new List<TypeMode>();
              string Connection = "Data Source=DESKTOP-HR90T67\\SQLEXPRESS;Initial Catalog=CoreMVCApplication;Integrated Security=True; User ID=sa;Password=bsdinfotech";
              using (SqlConnection sqlcon = new SqlConnection(Connection))
              {
                  using (SqlCommand cmd = new SqlCommand("select TypeMaster_id,type from tblTypeMaster"))
                  {
                      using (SqlDataAdapter sda = new SqlDataAdapter())
                      {
                          cmd.Connection = sqlcon;
                          sqlcon.Open();
                          sda.SelectCommand = cmd;
                          SqlDataReader sdr = cmd.ExecuteReader();
                          while (sdr.Read())
                          {
                              masterobj.Add(new TypeMode
                              {
                                  TypeMaster_id = Convert.ToInt32(sdr["TypeMaster_id"]),
                                  type = sdr["type"].ToString(),

                              });
                          }
                      }
                      //ViewBag.MasterTypeList = masterobj;
                      return masterobj;
                  }
              }
              //if (ty.TypeMaster_id==0)
              //{
              //    ModelState.AddModelError("", "Select Type");
              //}
              //int selectedvalue = ty.TypeMaster_id;
              //ViewBag.selectedvalue = ty.TypeMaster_id;
              //List<TypeMode> TypeList = new List<TypeMode>();

              //return View();
          }*/

        public IActionResult BindDdl()
        {
            string query = "select TypeMaster_id,type from tblTypeMaster";
            DataSet ds1 = util.BindDropDown(query);
            List<SelectListItem> Websitelist = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                Websitelist.Add(new SelectListItem { Text = dr1["type"].ToString(), Value = dr1["TypeMaster_id"].ToString() });
            }
            ViewBag.Websitelist = Websitelist;
            return View();
        }

        /*  public IActionResult BindEmploymentType()
          {
              string query = "select TypeMaster_id,Employment_type from tblTypeMaster";
              DataSet ds1 = util.BindDropDown(query);
              List<SelectListItem> Websitelist = new List<SelectListItem>();
              foreach (DataRow dr1 in ds1.Tables[0].Rows)
              {
                  Websitelist.Add(new SelectListItem { Text = dr1["Employment_type"].ToString(), Value = dr1["TypeMaster_id"].ToString() });
              }
              ViewBag.Employmenttype = Websitelist;
              return View();
          }

          public IActionResult BindBreadType()
          {
              string query = "select TypeMaster_id,bread_type from tblTypeMaster";
              DataSet ds1 = util.BindDropDown(query);
              List<SelectListItem> Websitelist = new List<SelectListItem>();
              foreach (DataRow dr1 in ds1.Tables[0].Rows)
              {
                  Websitelist.Add(new SelectListItem { Text = dr1["bread_type"].ToString(), Value = dr1["TypeMaster_id"].ToString() });
              }
              ViewBag.breadtype = Websitelist;
              return View();
          }

          public IActionResult Bindpreptype()
          {
              string query = "select TypeMaster_id,prep_type from tblTypeMaster";
              DataSet ds1 = util.BindDropDown(query);
              List<SelectListItem> Websitelist = new List<SelectListItem>();
              foreach (DataRow dr1 in ds1.Tables[0].Rows)
              {
                  Websitelist.Add(new SelectListItem { Text = dr1["prep_type"].ToString(), Value = dr1["TypeMaster_id"].ToString() });
              }
              ViewBag.preptype = Websitelist;
              return View();
          }
        */
        //public IActionResult BindTable()
        //{
        //    string query = "select type_Master,short_name,Name from short";
        //   DataSet ds = util.BindDropDown(query);
        //    List<SelectListItem> TableList = new List<SelectListItem>();
        //    foreach(DataRow dr1 in ds.Tables[0].Rows)
        //    {
        //        TableList.Add(new SelectListItem {Selected=ds.Tables.IsReadOnly });
        //    }
        //    ViewBag.TableList = TableList;
        //    return View();
        //}



    }
}
