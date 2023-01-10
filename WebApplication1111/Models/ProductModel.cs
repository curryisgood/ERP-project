using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1111.Models
{
    [Serializable]
    public class ProductModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int CntStock { get; set; }
        public string userID { get; set; }

        public ProductModel() { }
        public ProductModel(string code, string name, string type, int stock, string userid)
        {
            this.Code = code;
            this.Name = name;
            this.Type = type;
            this.CntStock = stock;
            this.userID = userid;
        }
    }
}