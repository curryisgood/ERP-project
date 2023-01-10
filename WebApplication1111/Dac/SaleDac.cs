using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1111.Models;
using WebApplication1111.Store;

namespace WebApplication1111.Dac
{
    public class SaleDac
    {
        static IStore<SaleModel> store = new FileStore<SaleModel>();
        static public void Create(string name, string count, string userID, string code, string account)
        {
            store.Save(new SaleModel(name, count, userID, code, account));
        }

        static public List<SaleModel> GetAll()
        {
            return store.GetAll();
        }
        static public List<SaleModel> Get(string userID)
        {
            return store.Get(item => item.UserID == userID);
        }

        static public void Delete(string purchaseCode){
            var allSales = store.GetAll();
            var filterdSales = allSales.FindAll(item => !(item.Code == purchaseCode));

            store.Init();
            foreach (var sale in filterdSales)
            {
                store.Save(new SaleModel(sale.Product, sale.Count, sale.UserID, sale.Code, sale.Account));
            }

        }
        static public void Update(SaleModel saleItem)
        {
            var allSales = store.GetAll();
            foreach (var item in allSales)
            {
                if (item.Code == saleItem.Code)
                {
                    item.Product = saleItem.Product;
                    item.Count = saleItem.Count;
                    item.Account = saleItem.Account;
                }
            }

            store.Init();
            foreach (var sale in allSales)
            {
                store.Save(new SaleModel(sale.Product, sale.Count, sale.UserID, sale.Code, sale.Account));
            }


        }
    }
}