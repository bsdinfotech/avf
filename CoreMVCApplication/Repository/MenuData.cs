using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreMVCApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMVCApplication.Repository
{
    public class MenuData
    {
       // ClsUtility Util = new ClsUtility();
        string AppPath = "";
        string ProfileId = "0";
        
        public static List<Menu> GetMenu(string ProfileId , string dept)
        {
            ClsUtility Util = new ClsUtility();
            List<Menu> MenuList = new List<Menu>();
            //string sqlQuery = "Sp_SetMenu_GetRights_New";
            StringBuilder str = new StringBuilder();
            str.Append("<ul>");
            string sql = "[Sp_SetMenu_GetRights_New] @ProfileId = " + ProfileId + ", @PageUrl = '', @AppPath='' ";
            DataSet ds = Util.Fill(sql);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // str.Append(dr["MenuString"].ToString());
                    MenuList.Add(new Menu
                    {
                        MID = Convert.ToInt32(dr["MenuId"]),
                        MenuName = Convert.ToString(dr["Menuname"]),
                        MenuURL = Convert.ToString(dr["ShortName"]),
                        MenuParent = Convert.ToString(dr["MenuParents"])
                    });
                }
            }
            str.Append("</ul>");
            return MenuList;

            //SqlCommand cmd = new SqlCommand(sqlQuery, util.CON);
            //cmd.Parameters.AddWithValue("@MenuId", ProfileId);
            //util.CON.Open();
            //SqlDataReader idr = cmd.ExecuteReader();

            //if (idr.HasRows)
            //{
            //    while (idr.Read())
            //    {
            //        MenuList.Add(new Menu
            //        {
            //            MID = Convert.ToInt32(idr["MenuId"]),
            //            MenuName = Convert.ToString(idr["Menuname"]),
            //            MenuURL = Convert.ToString(idr["ShortName"]),
            //            MenuParent = Convert.ToString(idr["MenuParents"])
            //        });
            //    }
            //}
            //return MenuList;
        }
    }
}
