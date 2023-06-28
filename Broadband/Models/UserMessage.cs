using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Broadband.Models
{
    
    public class UserMessage
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        
        public string Query
        {
            get; set;
        }
    }
}
