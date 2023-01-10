using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1111.Models
{
    [Serializable]
    public class UserModel
    {
        public string ID { get; set; }
        public string Password { get; set; }

        public UserModel() { }
        public UserModel(string id, string pwd)
        {
            this.ID = id;
            this.Password = pwd;
        }

    }
}