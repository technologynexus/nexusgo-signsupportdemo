using System;

namespace SignSupportDemo.Utilities.DB
{
    public class SignStorageObjectFinder
    {
        public static SignStorageObject Find(string id)
        {
            SignStorageObject storageObject = SignSupportDatabase.FindById(id);
            if (storageObject != null)
            {
                return storageObject;
            }
            throw new Exception("id argument invalid");
        }
    }
}
