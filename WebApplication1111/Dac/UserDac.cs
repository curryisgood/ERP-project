using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1111.Models;
using WebApplication1111.Store;

namespace WebApplication1111.Dac
{
    public class UserDac
    {
        static IStore<UserModel> store = new FileStore<UserModel>();
        static public void Create(string id, string password)
        {
            store.Save(new UserModel(id, password));
        }

        static public UserModel Get(string id)
        {
            return store.GetID(item => item.ID == id);
        }

    }
}