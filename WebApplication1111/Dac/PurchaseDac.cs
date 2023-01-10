using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using WebApplication1111.Models;
using WebApplication1111.Store;

namespace WebApplication1111.Dac
{
    public class PurchaseDac
    {
        static IStore<PurchaseModel> store = new FileStore<PurchaseModel>();

        static public void Create(string name, string count, string userID, string code, string account)
        {
            store.Save(new PurchaseModel(name, count, userID, code, account));
        }

        static public List<PurchaseModel> GetAll()
        {
            return store.GetAll();
        }
        static public List<PurchaseModel> Get(string userID)
        {
            return store.Get(item => item.UserID == userID);
        }

        static public void Delete(string purchaseCode)
        {
            var allPurchases = store.GetAll();
            var filterdPurchases = allPurchases.FindAll(item => !(item.Code == purchaseCode));

            store.Init();
            foreach (var purchase in filterdPurchases)
            {
                store.Save(new PurchaseModel(purchase.Product, purchase.Count, purchase.UserID, purchase.Code, purchase.Account));
            }

        }

        static public void Update(PurchaseModel purchaseItem)
        {
            var allUserPurchases = store.GetAll();
            foreach (var item in allUserPurchases)
            {
                if (item.Code == purchaseItem.Code)
                {
                    item.Product = purchaseItem.Product;
                    item.Count = purchaseItem.Count;
                    item.Account = purchaseItem.Account;
                }
            }

            store.Init();
            foreach (var purchase in allUserPurchases)
            {
                store.Save(new PurchaseModel(purchase.Product, purchase.Count, purchase.UserID, purchase.Code, purchase.Account));
            }
        }
    }
}