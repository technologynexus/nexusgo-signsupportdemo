using SignSupportDemo.Utilities.Error;
using System;

namespace SignSupportDemo.Utilities.DB
{
    public class SignStorageObjectFinder
    {
        public static SignStorageObject Find(string id)
        {
            if (string.IsNullOrEmpty(id)) {
                throw GetException("Empty id cannot be found in demo-database");
            }
            SignStorageObject StorageObject = SignSupportDatabase.FindById(id);
            if (StorageObject != null)
            {
                return StorageObject;
            }
            throw GetException("Sign request with id " + id + " not found in demo-database");
        }

        private static Exception GetException(string detailedMessage)
        {
            return ErrorHelper.GetException("Invalid id argument!", detailedMessage);
        }
    }
}
