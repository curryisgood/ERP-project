using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1111.Models
{
    [Serializable]
    public class SaleModel
    {
        public string Product { get; set; }
        public string Count { get; set; }
        public string UserID { get; set; }
        public string Code { get; set; }
        public string Account { get; set; }

        public SaleModel(){}
        public SaleModel(string product, string count, string UserID, string code, string account)
        {
            this.Product = product;
            this.Count = count;
            this.UserID = UserID;
            this.Code = code;
            this.Account = account;
        }

    }
}