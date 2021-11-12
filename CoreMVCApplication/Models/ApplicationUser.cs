using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreMVCApplication.Models;

namespace CoreMVCApplication.Models
{
    public class ApplicationUser:DbContext
    {
        public ApplicationUser(DbContextOptions<ApplicationUser> options) : base(options)
        {

        }
        public DbSet<User> SignUp { get; set; }
        public DbSet<CoreMVCApplication.Models.Image> Image { get; set; }
    }
}
