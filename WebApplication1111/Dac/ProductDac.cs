using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1111.Models;
using WebApplication1111.Store;

namespace WebApplication1111.Dac
{
    public class ProductDac
    {
        static IStore<ProductModel> store = new FileStore<ProductModel>();

        static public void Create(string code, string name, string type, int stock, string userid)
        {
            store.Save(new ProductModel(code, name, type, stock, userid));
        }

        static public List<ProductModel> Get(string id)
        {
            return store.Get(item => item.userID == id);
        }
        static public void Delete(string userID, string code, string productName)
        {
            var products = store.GetAll();
            var fileterProd = products.FindAll(item => !(item.userID == userID && item.Code == code));
            var userPurchases = PurchaseDac.Get(userID).FindAll(item => item.Product == productName);

            store.Init();
            foreach (var product in fileterProd)
            {
                store.Save(new ProductModel(product.Code, product.Name, product.Type, product.CntStock, product.userID));
            }
        }
        static public  void Update(ProductModel newProduct)
        {
            var products = store.GetAll();

            var filterd = ProductDac.Get(newProduct.userID)
                .Find(item => item.Name == newProduct.Name && item.Type == newProduct.Type);

            if (filterd != null)
            {
                throw new Exception("이미 존재하는 품목입니다.");
            }

            foreach (var item in products)
            {
                if (item.Code == newProduct.Code && item.userID == newProduct.userID)
                {
                    item.Name = newProduct.Name;
                    item.Type = newProduct.Type;
                    item.CntStock = newProduct.CntStock;
                }
            }
            store.Init();
            foreach (var product in products)
            {
                store.Save(new ProductModel(product.Code, product.Name, product.Type, product.CntStock, product.userID));
            }
        }
    }
}