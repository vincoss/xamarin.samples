using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForms_EnterpriseApp_Authentication.Models
{
    public class User
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string UserPrincipalName { get; set; }
        public string Token { get; set; }
    }
}
