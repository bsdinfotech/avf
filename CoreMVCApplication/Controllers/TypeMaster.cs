using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVCApplication.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace CoreMVCApplication.Controllers
{
    
    public class TypeMaster : Controller
    {
        ClsUtility cls = new ClsUtility();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TypeMode ty)
        {
            int st_value;
            if(ty.status==true)
            {
                st_value = 1;
            }
            else
            {
                st_value = 0;
            }
            string sqlquery = "Insert into tblTypeMaster(type,status,entry_date)values('" + ty.type+ "', '"+st_value+"', getdate())";
            string status = cls.MultipleTransactions(sqlquery);
            if(status == "Successfull")
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
        //[HttpPost]
        //public IActionResult getType()
        //{
        //    List<TypeMode> masterobj = new List<TypeMode>();
        //    string Connection = "Data Source=DESKTOP-HR90T67\\SQLEXPRESS;Initial Catalog=CoreMVCApplication;Integrated Security=True; User ID=sa;Password=bsdinfotech";
        //    using (SqlConnection sqlcon = new SqlConnection(Connection))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("select * from tblTypeMaster"))
        //        {
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                cmd.Connection = sqlcon;
        //                sqlcon.Open();
        //                sda.SelectCommand = cmd;
        //                SqlDataReader sdr = cmd.ExecuteReader();
        //                while (sdr.Read())
        //                {
        //                    TypeMode tmobj = new TypeMode();
        //                    tmobj.type = sdr["type"].ToString();
        //                    masterobj.Add(tmobj);
        //                }
        //            }
        //            ViewBag.MasterTypeList = masterobj;
        //            return View(masterobj);
        //        }
        //    }
        //}
    }
}
