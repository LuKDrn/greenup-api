using System;

namespace GreenUp.Web.Mvc.Models.Auth
{
    public class TokenApiModel
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
