using SignSupportDemo.SignSupport.API;
using System;
using System.Collections.Generic;

namespace SignSupportDemo.Utilities.DB
{
    public class SignStorageObjectFactory
    {
        public static SignStorageObject Create(List<DocumentStorageObject> documentStorageObjects, List<TbsData> tbsDatas, SignRequestGenerationResponse response)
        {
            int minutes = int.Parse(Startup.Configuration["Storage:MaxStateRetentionInMinutes"]);
            return new SignStorageObject
            {
                Documents = documentStorageObjects,
                TbsDatas = tbsDatas,
                Id = response.RelayState,
                SignRequest = response.SignRequest,
                Expires = DateTime.Now.AddMinutes(minutes)
            };
        }
    }
}
