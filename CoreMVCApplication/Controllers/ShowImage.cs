using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVCApplication.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Protocols;

namespace CoreMVCApplication.Controllers
{
    public class ShowImage : Controller
    {
        string Connection = "Data Source = 144.168.39.28,1533; Persist Security Info = True; User ID = e_vet; Password = klqusjntmpyrvgzf0iad";
        public IActionResult Index()
        {
            List<Image> images = GetImages();
            return View(images);
        }
        [HttpPost]
        /*public ActionResult Index(int imageId)
        {
            List<Image> images = GetImages();
            Image image = images.Find(p => p.id == imageId);
            if (image != null)
            {
                image.IsSelected = true;
                ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(image.image, 0, image.image.Length);
            }
            return View(images);
        }*/
        private List<Image> GetImages()
        {
            string query = "SELECT * FROM Image";
            List<Image> images = new List<Image>();
            using (SqlConnection con = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            images.Add(new Image
                            {
                                id = Convert.ToInt32(sdr["id"]),
                                image = sdr["imgPath"].ToString(),

                            });
                        }
                        ViewData["imgurl"] = images;
                    }
                    con.Close();
                }

                return images;
            }

        }
    }
}
