using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApplication1111.Models;
using WebApplication1111.Store;

namespace WebApplication1111.Dac
{
    public class AccountDac
    {
        static IStore<AccountModel> store = new FileStore<AccountModel>();

        static public void Create(string code, string name, string userid)
        {
            store.Save(new AccountModel(code, name, userid));
        }
        static public List<AccountModel> Get(string id)
        {
            return store.Get(x => x.UserID == id);
        }

        static public void Delete(string code, string name, string userID)
        {
            var accounts = store.GetAll();
            var fileteracc = accounts.FindAll(x => !(x.Code == code && x.UserID == userID));
            store.Init();
            foreach (var accnt in fileteracc)
            {
                store.Save(new AccountModel(accnt.Code, accnt.Name, accnt.UserID));
            }
        }

        static public void Update(AccountModel accntlst)
        {
            var allacntList = store.GetAll();

            var fileteracc = allacntList.Find(x => !(x.Name == accntlst.Name));
            //if(fileteracc != null)
            //{
            //    throw new Exception("이미 존재하는 품목");
            //}
            foreach (var lst in allacntList)
            {
                if (lst.Code == accntlst.Code && lst.UserID == accntlst.UserID)
                {
                    lst.Name = accntlst.Name;

                }
            }
            store.Init();
            foreach (var acnt in allacntList)
            {
                store.Save(new AccountModel(acnt.Code, acnt.Name, acnt.UserID));
            }

        }

    }
}