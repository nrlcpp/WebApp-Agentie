using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Dto
{
    public class AuthenticatePostModel
    {
        private object user;
        private string token;

        public AuthenticatePostModel(object user, string token)
        {
            this.user = user;
            this.token = token;
        }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
