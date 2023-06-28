using Broadband.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Broadband.Data
{
    public class UserMessageDbContext:IdentityDbContext
    {
        public UserMessageDbContext(DbContextOptions<UserMessageDbContext> options) : base(options) { }


        public DbSet<UserMessage> Messages { get; set; }
        public DbSet<Login> LoginData { get; set; }
    }



}

