using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1111.Models
{
    [Serializable]
    public class AccountModel
    {
        public string Code { get; set; }
        public string Name { get; set; } 
        public string UserID { get; set; }

        public AccountModel() { }
        public AccountModel(string code, string name, string userid)
        {
            this.Code = code;
            this.Name = name;
            this.UserID = userid;
        }
    }
}