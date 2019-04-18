using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignSupportDemo.Utilities.DB;
using SignSupportDemo.Utilities.Error;

namespace SignSupportDemo.Pages
{
    public class SignResultModel : PageModel
    {
        public GeneralError GeneralError { get; set; }

        public SignStorageObject SignStorageObject { get; set; }

        public IActionResult OnGet(string id)
        {
            try
            {
                SignStorageObject = SignStorageObjectFinder.Find(id);
            }
            catch (Exception Error)
            {
                GeneralError = ErrorHelper.GetError(Error);
            }
            return Page();
        }
    }
}