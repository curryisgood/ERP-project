using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace WebApplication1111.Store
{
    public class FileStore<T> : IStore<T>
    {
        static List<T> store;

        public string Name { get; set; }
        private string fileName
        {
            get
            {
                return $"{Name}.dat";
            }
        }

        public FileStore()
        {
            this.Name = typeof(T).Name;

            if (!File.Exists(this.fileName))
            {
                File.Create(this.fileName).Close();
            }

            using (var fs = File.OpenRead(this.fileName))
            {
                if (fs.Length == 0)
                {
                    store = new List<T>();
                }
                else
                {
                    var formatter = new BinaryFormatter();
                    store = (List<T>)formatter.Deserialize(fs);
                }
            }
        }

        public void Save(T data)
        {
            store.Add(data);
            var fs = File.OpenWrite(fileName);
            var formatter = new BinaryFormatter();
            formatter.Serialize(fs, store);
            fs.Close();
        }

        public T GetID(Predicate<T> match)
        {
            return store.Find(match);
        }

        public List<T> GetAll()
        {
            return store;
        }

        public List<T> Get(Predicate<T> match)
        {
            return store.FindAll(match);
        }

        public void Init()
        {
            store = new List<T>();
            this.Name = typeof(T).Name;

            if (File.Exists(this.fileName))
            {
                File.Delete(this.fileName);
            }
        }
    }
}
     