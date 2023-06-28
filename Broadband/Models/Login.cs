using Broadband.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Broadband.Models
{
    public class Login : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(256)")]
        public string Packages { get; set; }


    }
}
