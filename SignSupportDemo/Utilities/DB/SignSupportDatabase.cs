using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignSupportDemo.Utilities.DB
{
    public class SignSupportDatabase
    {
        public static void Upsert(SignStorageObject storageObject)
        {
            Task.Run(() => DeleteExpiredAsync());
            using (var db = GetDatabase())
            {
                var storageObjects = db.GetCollection<SignStorageObject>("signSupportStorage");
                storageObjects.Upsert(storageObject);
            }
        }


        public static IList<SignStorageObject> FindAll()
        {
            using (var db = GetDatabase())
            {
                var storageObjects = db.GetCollection<SignStorageObject>("signSupportStorage");
                return storageObjects.FindAll().ToList();
            }
        }

        public static SignStorageObject FindById(string id)
        {
            using (var db = GetDatabase())
            {
                var storageObjects = db.GetCollection<SignStorageObject>("signSupportStorage");
                return storageObjects.FindById(id);
            }
        }

        public static int Count()
        {
            using (var db = GetDatabase())
            {
                var storageObjects = db.GetCollection<SignStorageObject>("signSupportStorage");
                return storageObjects.Count();
            }
        }

        public static void Drop()
        {
            using (var db = GetDatabase())
            {
                db.DropCollection("signSupportStorage");
            }
        }

        public static void DeleteExpired()
        {
            using (var db = GetDatabase())
            {
                var storageObjects = db.GetCollection<SignStorageObject>("signSupportStorage");
                int numberOfDeletedObjects = storageObjects.Delete(x => x.Expires == null || x.Expires < DateTime.Now);
                Console.WriteLine("Deleted {0} expired objects.", numberOfDeletedObjects);
            }
        }

        private static void DeleteExpiredAsync()
        {
            Random rnd = new Random();
            int seconds = rnd.Next(1, 11);
            Thread.Sleep(seconds);
            SignSupportDatabase.DeleteExpired();
        }

        private static LiteDatabase GetDatabase()
        {
            return new LiteDatabase(@"demo.db");
        }
    }
}
