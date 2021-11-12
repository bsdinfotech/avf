using CoreMVCApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CoreMVCApplication.Controllers
{
    public class UploadExcel : Controller
    {
        ClsUtility Utility = new  ClsUtility();
        private readonly IHostingEnvironment _hostingEnvironment;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Import()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Import1()
        {
            string UserId = HttpContext.Session.GetString("userId");
            string sqlQuery = string.Empty;
            IFormFile file = Request.Form.Files[0];
            string folderName = "/wwwroot/Excel/";
            string extension = Path.GetExtension(file.FileName);
            string filename = Path.GetFileNameWithoutExtension(file.FileName);
            string status = string.Empty;
            //string webRootPath = HostingEnvironment.WebRootPath;
            //string webRootPath = _hostingEnvironment.WebRootPath;
            string webRootPath =  filename + extension;
            string newPath = Path.Combine(webRootPath, folderName);

            StringBuilder sb = new StringBuilder();
           
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        dt.Columns.Add(cell.ToString());
                        // dt.Columns.Add("RC" + col.ToString());
                    }
                    for (int rw = (sheet.FirstRowNum + 1); rw <= sheet.LastRowNum; rw++) //Read Excel File
                    {
                        DataRow dr = dt.NewRow();
                        IRow row = sheet.GetRow(rw);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int col = row.FirstCellNum; col < cellCount; col++)
                        {
                            if (row.GetCell(col) != null)

                                //dr[col.ToString()] = dr[row.GetCell(col).ToString()];
                                dr[col] = row.GetCell(col).ToString();
                        }
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        //sqlQuery += "Insert into ExcelData (Name,LastName,CreateUser,createdate)values('" + dt.Rows[row]["Name"].ToString() + "','"+dt.Rows[row]["LastName"].ToString()+ "','" + dt.Rows[row]["CreateUser"].ToString() + "',getdate())";
                        sqlQuery += "Insert into Diagnosis_Data (DataDictionaryId,Term_Name,Venom_Approved,Active,SubsetId,Subset,createuser,createdate,modifydate)values(" + dt.Rows[row]["DataDictionaryId"].ToString() + ",'" + dt.Rows[row]["Term_Name"].ToString() + "'," + dt.Rows[row]["Venom_Approved"].ToString() + "," + dt.Rows[row]["Active"].ToString() + "," + dt.Rows[row]["SubsetId"].ToString() + ",'" + dt.Rows[row]["Subset"].ToString() + "'," + UserId + ",convert(datetime,'" + System.DateTime.Now.ToString() + "',103),convert(datetime,'" + System.DateTime.Now.ToString() + "',103))";
                        //sqlQuery += "Insert into MST_CMA(cma_name,cma_shortname,cma_parent,cma_type,CreateUser,CreateDate,ModifyUser,ModifyDate,cma_Status,cma_Country,cma_category)values('" + dt.Rows[row]["cma_name"].ToString() + "','" + dt.Rows[row]["cma_shortname"].ToString() + "'," + dt.Rows[row]["cma_parent"].ToString() + ",'" + dt.Rows[row]["cma_type"].ToString() + "'," + UserId + ",convert(datetime,'" + System.DateTime.Now.ToString() + "',103),'"+UserId+"',convert(datetime,'" + System.DateTime.Now.ToString() + "',103), "+ dt.Rows[row]["cma_Status"].ToString() + ",'"+ dt.Rows[row]["cma_Country"].ToString() + "','"+ dt.Rows[row]["cma_category"].ToString() + "')";
                    }
                    status = Utility.MultipleTransactions(sqlQuery);

                    if (status == "Successfull")
                    {
                        ViewBag.messagge = "Excel Import";
                    }
                    else
                    {

                    }

                    sb.Append("<table class='table table-bordered'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                        }
                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");
                }
            }
            return this.Content(sb.ToString());
            
        }


        public ActionResult Download()
        {
            string Files = "wwwroot/Excel/excel1.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(Files);
            System.IO.File.WriteAllBytes(Files, fileBytes);
            MemoryStream ms = new MemoryStream(fileBytes);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "excel1.xlsx");
        }
        public async Task<IActionResult> Export()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"excel1.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("employee");
                IRow row = excelSheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("EmployeeId");
                row.CreateCell(1).SetCellValue("EmployeeName");
                row.CreateCell(2).SetCellValue("Age");
                row.CreateCell(3).SetCellValue("Sex");
                row.CreateCell(4).SetCellValue("Designation");
                row = excelSheet.CreateRow(1);
                row.CreateCell(0).SetCellValue(1);
                row.CreateCell(1).SetCellValue("Jack Supreu");
                row.CreateCell(2).SetCellValue(45);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Solution Architect");
                row = excelSheet.CreateRow(2);
                row.CreateCell(0).SetCellValue(2);
                row.CreateCell(1).SetCellValue("Steve khan");
                row.CreateCell(2).SetCellValue(33);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Software Engineer");
                row = excelSheet.CreateRow(3);
                row.CreateCell(0).SetCellValue(3);
                row.CreateCell(1).SetCellValue("Romi gill");
                row.CreateCell(2).SetCellValue(25);
                row.CreateCell(3).SetCellValue("FeMale");
                row.CreateCell(4).SetCellValue("Junior Consultant");
                row = excelSheet.CreateRow(4);
                row.CreateCell(0).SetCellValue(4);
                row.CreateCell(1).SetCellValue("Hider Ali");
                row.CreateCell(2).SetCellValue(34);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Accountant");
                row = excelSheet.CreateRow(5);
                row.CreateCell(0).SetCellValue(5);
                row.CreateCell(1).SetCellValue("Mathew");
                row.CreateCell(2).SetCellValue(48);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Human Resource");
                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }






       
        //[HttpPost]
        //public IActionResult Import(MST_Cma cmaObj)
        //{
        //    string sheet="";
        //    if (cmaObj.fileUpload != null)
        //    {
        //        if (cmaObj.fileUpload.Length > 0)
        //        {
        //            //string filename = Path.GetFileNameWithoutExtension(cmaObj.fileUpload.FileName);
        //            string filename = Path.GetFileNameWithoutExtension("AnimalMaster");
        //            string extension = Path.GetExtension(cmaObj.fileUpload.FileName);
        //            if (extension == ".xlsx")
        //            {
        //                //cmaObj.file = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
        //                cmaObj.file = filename = filename + extension;
        //                //string path = Path.Combine("wwwroot/Excel/", filename);
        //                string path = Path.Combine("wwwroot/Excel/", filename);
        //                FileInfo existingFile = new FileInfo(path);
        //                using (var fileStream = new FileStream(path, FileMode.Create))
        //                {
        //                    cmaObj.fileUpload.CopyTo(fileStream);
        //                }

        //                using (ExcelPackage xlPackage = new ExcelPackage(existingFile))
        //                {


        //                    // int worksheetcount = xlPackage.Workbook.Worksheets.Count;
        //                    foreach (ExcelWorksheet worksheet in xlPackage.Workbook.Worksheets)
        //                    {

        //                        sheet = worksheet.Name;
        //                    }

        //                }
        //                string excel = BindData(path, filename, sheet);

                      
        //            }

        //        }
        //    }
        //    else
        //    {

        //    }
        //    return View();
        //}

        //public string BindData(string file, string filename, string sheet)
        //{

        //    FileInfo existingFile = new FileInfo(file);
        //    DataSet ds = new DataSet();
        //    // string kkstr = "";

        //    using (ExcelPackage xlPackage = new ExcelPackage(existingFile))
        //    {
        //        // int worksheetcount = xlPackage.Workbook.Worksheets.Count;
        //        for (int i = 0; i < 1; i++)
        //        {
        //            ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[sheet];
        //            if (worksheet == null)
        //                continue;
        //            DataTable dt = new DataTable();
        //            ExcelCellAddress startCell = worksheet.Dimension.Start;
        //            ExcelCellAddress endCell = worksheet.Dimension.End;

        //            string setstyle = " style=\"color:#COLOR;font-weight:#WEIGHT;font-style:#STYLE;text-decoration:#DECORATION;vertical-align:#VALIGN;text-align:#ALIGN;font-family:#FAMILY;font-size:#FSIZEpx" + ";background-color:#BCOLOR\"";

        //            // create all the needed DataColumn
        //            for (int col = startCell.Column; col <= endCell.Column; col++)
        //            {
        //                dt.Columns.Add(col.ToString());
        //                dt.Columns.Add("RC" + col.ToString());
        //            }
        //            // place all the data into DataTable
        //            for (int row = startCell.Row; row <= endCell.Row; row++)
        //            {
        //                DataRow dr = dt.NewRow();
        //                int x = 0;
        //                string rc = "1.1";
        //                for (int col = startCell.Column; col <= endCell.Column; col++)
        //                {
        //                    rc = checkmergedcel(worksheet, worksheet.Cells[row, col]);
        //                    dr[col.ToString()] = string.IsNullOrEmpty(worksheet.Cells[row, col].Text) ? "" : worksheet.Cells[row, col].Text;
        //                    dr["RC" + col.ToString()] = rc;
        //                    //string str = worksheet.Cells[row, col].RichText.ToString() ;
        //                    //string filePath = existingFile.ToString();
        //                    //var file2 = new System.IO.FileInfo(filePath);
        //                    //using (ExcelPackage p = new ExcelPackage(file2))
        //                    //{

        //                    var wscell = worksheet.Cells[row, col];
        //                    var Style = wscell.Style;
        //                    var Font = Style.Font;


        //                    //var ws = worksheet;
        //                    dr["RC" + col.ToString()] = "" + rc + setstyle.Replace("#COLOR", Font.Color.Rgb).Replace("#WEIGHT", Font.Bold.ToString() == "True" ? "Bold" : "normal").Replace("#STYLE", Font.Italic.ToString() == "True" ? "Italic" : "normal").Replace("#DECORATION", Font.UnderLine.ToString() == "True" ? "underline" : "unset").Replace("#VALIGN", Font.VerticalAlign.ToString().Replace("False", "unset")).Replace("#ALIGN", Style.HorizontalAlignment.ToString().Replace("False", "normal")).Replace("#FAMILY", Font.Name.ToString().Replace("False", "normal")).Replace("#FSIZE", Font.Size.ToString().Replace("False", "normal").Replace("True", "Bold")).Replace("#BCOLOR", Style.Fill.BackgroundColor.Rgb);


        //                    //}
        //                }
        //                dt.Rows.Add(dr);
        //            }
        //            ds.Tables.Add(dt);
        //        }
        //    }

        //    string message = insertdataforporting(ds, filename);

        //    existingFile = null;
        //    GC.Collect();
        //    GC.Collect();
        //    return message;
        //}


        //private string checkmergedcel(ExcelWorksheet worksheet, ExcelAddress cellAddress)
        //{
        //    int colSpan = 1;
        //    int rowSpan = 1;
        //    //check if this is the start of a merged cell
        //    //ExcelAddress cellAddress = new ExcelAddress(currentCell.Address);
        //    var mCellsResult = (from c in worksheet.MergedCells
        //                        let addr = new ExcelAddress(c)
        //                        where cellAddress.Start.Row >= addr.Start.Row &&
        //                        cellAddress.End.Row <= addr.End.Row &&
        //                        cellAddress.Start.Column >= addr.Start.Column &&
        //                        cellAddress.End.Column <= addr.End.Column
        //                        select addr);

        //    if (mCellsResult.Count() > 0)
        //    {
        //        var mCells = mCellsResult.First();

        //        //if the cell and the merged cell do not share a common start address then skip this cell as it's already been covered by a previous item
        //        if (mCells.Start.Address != cellAddress.Start.Address)
        //            return "rowspan=0 colspan=0 ";


        //        if (mCells.Start.Column != mCells.End.Column)
        //        {
        //            colSpan += mCells.End.Column - mCells.Start.Column;
        //        }

        //        if (mCells.Start.Row != mCells.End.Row)
        //        {
        //            rowSpan += mCells.End.Row - mCells.Start.Row;
        //        }
        //    }

        //    return "rowspan='" + rowSpan.ToString() + "' colspan=" + colSpan.ToString();
        //}



        //public string insertdataforporting(DataSet ds, string filename)
        //{
            
        //    //string GUID = DateTime.Now.ToString("yyyyMMddHHmmssffff");
        //    //long UserId = int.Parse(Session["Userid"].ToString().ToString());
        //    //long EntryType = fd.UploadType == "" ? 0 : Convert.ToInt64(fd.UploadType);
        //    //fd.Status = "er";
        //    //// int kk = fd.Sector1.Length;
        //    //string strmain = "Insert into ExcelDataPortMaintest(TranId,headerid,header,sectorid1,sectorid2,sectorid3,sectorid4,sectorid5,sectorid6,sectorid7,EntryDate, EntryUser, EntryType, NoofRows, NoofCols, FileName) values (#TranId,(select isnull(max(headerid),0)+1 from ExcelDataPortMaintest),'" + ds.Tables[0].Rows[0][6].ToString() + "','" + (Convert.ToInt32(fd.Sector1.ToString().Length) < 2 ? "0" + fd.Sector1 : fd.Sector1) + "' ," + (Convert.ToInt32(fd.Sector2.ToString().Length) < 2 ? "0" + fd.Sector2 : fd.Sector2) + " ," + (Convert.ToInt32(fd.Sector3.ToString().Length) < 2 ? "0" + fd.Sector3 : fd.Sector3) + " ," + (Convert.ToInt32(fd.Sector4.ToString().Length) < 2 ? "0" + fd.Sector4 : fd.Sector4) + " ," + (Convert.ToInt32(fd.Sector5.ToString().Length) < 2 ? "0" + fd.Sector5 : fd.Sector5) + " ," + (Convert.ToInt32(fd.Sector6.ToString().Length) < 2 ? "0" + fd.Sector6 : fd.Sector6) + " ," + (Convert.ToInt32(fd.Sector7.ToString().Length) < 2 ? "0" + fd.Sector7 : fd.Sector7) + " ,getdate()," + UserId.ToString() + ", " + EntryType.ToString() + ", #NoofRows, #NoofCols, '#FileName') ; ";

        //    //string str = " Insert into ExcelDataPort(headerid,EntryDate,EntryUser, EntryType, TranId, DataLineNo";
        //    //string strval = " values((select max(headerid) from ExcelDataPortMaintest),getdate()," + UserId.ToString() + ", " + EntryType.ToString();
        //    //string checkguid = "";
        //    //string msg = "";
        //    //foreach (DataTable dt in ds.Tables)
        //    //{
        //    //    GUID = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
        //    //    if (checkguid != "")
        //    //    {
        //    //        checkguid += ", ";
        //    //    }
        //    //    checkguid += "'" + GUID + "'";
        //    //    string lastins = "";
        //    //    int kamincol = (dt.Columns.Count / 2) - 3;
        //    //    string stval = "";
        //    //    bool hasEmptyOrZero = false;
        //    //    bool hasCheckedAllRows = false;
        //    //    bool isNote = false;
        //    //    string colVal = "";
        //    //    for (int row = 0; row < dt.Rows.Count; row++)
        //    //    {
        //    //        DataRow dr = dt.Rows[row];
        //    //        string strins = str;
        //    //        string strinsval = strval;
        //    //        strinsval += ", '" + GUID + "', " + (row + 1).ToString() + "";

        //    //        int col = 1; stval = ""; hasEmptyOrZero = false; hasCheckedAllRows = false;
        //    //        isNote = false;
        //    //        foreach (DataColumn dc in dt.Columns)
        //    //        {
        //    //            strins += ", STR_" + (dc.ColumnName.ToString().Contains("RC") ? "RC_" : "") + col.ToString().PadLeft(3, '0');
        //    //            if (fd.UploadType == "2")
        //    //            {
        //    //                if (isNote == false)
        //    //                {
        //    //                    if (!dc.ColumnName.ToString().Contains("RC"))
        //    //                    {
        //    //                        colVal = dr[dc.ColumnName.ToString()].ToString().ToLower();
        //    //                        if (colVal.Contains("note :") || colVal.Contains("note* :"))
        //    //                            isNote = true;
        //    //                    }
        //    //                }
        //    //                if (col == 2)
        //    //                {
        //    //                    if (!dc.ColumnName.ToString().Contains("RC"))
        //    //                    {
        //    //                        stval = dr[dc.ColumnName.ToString()].ToString().Replace("'", "''");
        //    //                        if (!string.IsNullOrEmpty(stval) && stval != "0")
        //    //                            strinsval += ", N'" + stval + "-stateid" + "'";
        //    //                        else
        //    //                            strinsval += ", N'" + stval + "'";
        //    //                    }
        //    //                    else
        //    //                        strinsval += ", N'" + dr[dc.ColumnName.ToString()].ToString().Replace("'", "''") + "'";
        //    //                }
        //    //                else
        //    //                    strinsval += ", N'" + dr[dc.ColumnName.ToString()].ToString().Replace("'", "''") + "'";

        //    //                if (!string.IsNullOrEmpty(stval) && stval != "0")
        //    //                {
        //    //                    if (hasCheckedAllRows == false)
        //    //                    {
        //    //                        if (col > (fd.HasWorldLevel == 1 ? 5 : 4))
        //    //                        {
        //    //                            if (!dc.ColumnName.ToString().Contains("RC"))
        //    //                            {
        //    //                                colVal = dr[dc.ColumnName.ToString()].ToString().Replace("'", "''").Trim().Replace(" ", "");
        //    //                                if (!string.IsNullOrEmpty(colVal))
        //    //                                {
        //    //                                    decimal n;
        //    //                                    bool isNumber = decimal.TryParse(colVal, out n);
        //    //                                    if (isNumber == true)
        //    //                                    {
        //    //                                        if (Convert.ToInt32(n) > 0)
        //    //                                        {
        //    //                                            hasEmptyOrZero = true;
        //    //                                            hasCheckedAllRows = true;
        //    //                                        }
        //    //                                    }
        //    //                                    else
        //    //                                    {
        //    //                                        if (colVal != "-" && colVal != "_")
        //    //                                        {
        //    //                                            hasEmptyOrZero = true;
        //    //                                            hasCheckedAllRows = true;
        //    //                                        }
        //    //                                    }
        //    //                                }
        //    //                            }
        //    //                        }

        //    //                    }
        //    //                }
        //    //            }
        //    //            else
        //    //                strinsval += ", N'" + dr[dc.ColumnName.ToString()].ToString().Replace("'", "''") + "'";

        //    //            if (dc.ColumnName.ToString().Contains("RC"))
        //    //                col += 1;
        //    //        }
        //    //        if (fd.UploadType == "2")
        //    //        {
        //    //            if (!string.IsNullOrEmpty(stval) && stval != "0")
        //    //            {
        //    //                if (hasEmptyOrZero == false && isNote == false)
        //    //                    strinsval = strinsval.Replace(stval + "-stateid", "9999");
        //    //                else
        //    //                    strinsval = strinsval.Replace(stval + "-stateid", stval);
        //    //            }
        //    //        }
        //    //        lastins += (row == 0 ? strmain.Replace("#TranId", GUID).Replace("#NoofRows", dt.Rows.Count.ToString()).Replace("#NoofCols", kamincol.ToString()).Replace("#FileName", filename) : "") + strins + " ) " + strinsval + " ) ";

        //    //        if (row % 80 == 0)
        //    //        {

        //    //            msg = msg + Utility.execQuery(lastins, Session["uploadtypeid"].ToString() == "2" ? Utility.strIndiaStatall : Utility.strdoiall);
        //    //            lastins = "";
        //    //        }
        //    //    }
        //    //    if (lastins != "")
        //    //        msg = msg + Utility.execQuery(lastins, Session["uploadtypeid"].ToString() == "2" ? Utility.strIndiaStatall : Utility.strdoiall);
        //    //    string kk = fd.headerid;
        //    //    var parentid = fd.Sector7 != "0" ? fd.Sector7 : fd.Sector6 != "0" ? fd.Sector6 : fd.Sector5 != "0" ? fd.Sector5 : fd.Sector4 != "0" ? fd.Sector4 : fd.Sector3 != "0" ? fd.Sector3 : fd.Sector2 != "0" ? fd.Sector2 : fd.Sector1;
        //    //    var secid = fd.Sector7d != "0" ? fd.Sector7d : fd.Sector6d != "0" ? fd.Sector6d : fd.Sector5d != "0" ? fd.Sector5d : fd.Sector4d != "0" ? fd.Sector4d : fd.Sector3d != "0" ? fd.Sector3d : fd.Sector2d != "0" ? fd.Sector2d : fd.Sector1d;
        //    //    var label = fd.Sector7d != "0" ? 7 : fd.Sector6d != "0" ? 6 : fd.Sector5d != "0" ? 5 : fd.Sector4d != "0" ? 4 : fd.Sector3d != "0" ? 3 : fd.Sector2d != "0" ? 2 : 1;
        //    //    var clabel = fd.Sector7 != "0" ? 7 : fd.Sector6 != "0" ? 6 : fd.Sector5 != "0" ? 5 : fd.Sector4 != "0" ? 4 : fd.Sector3 != "0" ? 3 : fd.Sector2 != "0" ? 2 : 1;
        //    //    var cparentid = fd.Sector7 != "0" ? fd.Sector6 : fd.Sector6 != "0" ? fd.Sector5 : fd.Sector5 != "0" ? fd.Sector4 : fd.Sector4 != "0" ? fd.Sector3 : fd.Sector3 != "0" ? fd.Sector2 : fd.Sector2 != "0" ? fd.Sector1 : "";

        //    //    DataSet ds1 = Utility.Fill("exec SetUploadData @VAR_HEADERID='" + fd.headerid + "', @TranId='" + GUID + "',@UploadType ='" + fd.UploadType + "',@checkregionTotal='" + fd.chkregion + "'" + (Session["uploadtypeid"].ToString() == "3" ? ", @SplitState=" + fd.SplitState + ", @SplitRegion=" + fd.SplitRegion : ", @IsWorldLevel=" + fd.HasWorldLevel + ", @CountryId=" + fd.CountryId + ", @ColumnNo=" + fd.ColumnNumberForCalculation + ", @ColumnCondition='" + fd.ColumnCondidtion + "'"), Session["uploadtypeid"].ToString() == "2" ? Utility.strIndiaStatall : Utility.strdoiall);
        //    //    //, " + EntryType.ToString()
        //    //    if (ds1.Tables.Count > 0)
        //    //    {
        //    //        if (ds1.Tables[0].Rows.Count > 0)
        //    //        {
        //    //            fd.Text = ds1.Tables[0].Rows[0][0].ToString();
        //    //            try
        //    //            {
        //    //                string headid = ds1.Tables[0].Rows[0][0].ToString().Split(':')[1].Trim();
        //    //                if (fd.Text.Contains("Data uploaded successfully"))
        //    //                {
        //    //                    if (fd.Sector1d != "0" || fd.Sector1d != null)
        //    //                    {
        //    //                        DataSet ds2 = Utility.Fill("exec SP_CrossLink @headerid = " + headid + ", @parentid = '" + parentid + "', @Secid = '" + secid + "',@label='" + label + "',@clabel='" + clabel + "',@cparentid='" + cparentid + "',@list=''", Session["uploadtypeid"].ToString() == "2" ? Utility.strIndiaStatall : Utility.strdoiall);
        //    //                    }
        //    //                    ViewBag.headerid = "Data Save Successfully at Header id=" + headid;
        //    //                    fd.headerid = headid.Trim();
        //    //                    fd.Status = "ok";
        //    //                    showdata(headid);
        //    //                }
        //    //            }
        //    //            catch (Exception ex)
        //    //            {
        //    //                fd.Text = "Unable to port data." + fd.Text;
        //    //            }
        //    //        }
        //    //        else
        //    //            fd.Text = "Unable to port data." + fd.Text;
        //    //    }
        //    //    else
        //    //        fd.Text = "Unable to port data." + fd.Text;
        //    //    // DisplayMsg.Text = "Successfully Uploaded Data.";
        //    //}
        //    //return fd.Text;
        //    return "";
        //}







    }
}
