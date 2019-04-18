using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;

namespace SignSupportDemo.Utilities.File
{
    public class FileHelpers
    {
        public static byte[] ProcessFormFile(IFormFile formFile, ModelStateDictionary modelState)
        {
            // Use Path.GetFileName to obtain the file name, which will
            // strip any path information passed as part of the
            // FileName property. HtmlEncode the result in case it must 
            // be returned in an error message.
            var fileName = WebUtility.HtmlEncode(
                Path.GetFileName(formFile.FileName));

            if (formFile.ContentType.ToLower() != "application/pdf"
                && formFile.ContentType.ToLower() != "application/xml"
                && formFile.ContentType.ToLower() != "text/xml")
            {
                modelState.AddModelError(formFile.Name,
                    $"The file ({fileName}) must be a pdf or xml file.");
            }

            // Check the file length and don't bother attempting to
            // read it if the file contains no content. This check
            // doesn't catch files that only have a BOM as their
            // content, so a content length check is made later after 
            // reading the file's content to catch a file that only
            // contains a BOM.
            if (formFile.Length == 0)
            {
                modelState.AddModelError(formFile.Name,
                    $"The file ({fileName}) is empty.");
            }
            else if (formFile.Length > 1048576)
            {
                modelState.AddModelError(formFile.Name,
                    $"The file ({fileName}) exceeds 1 MB.");
            }
            else
            {
                try
                {
                    byte[] fileContents = null;

                    // The StreamReader is created to read files that are UTF-8 encoded. 
                    // If uploads require some other encoding, provide the encoding in the 
                    // using statement. To change to 32-bit encoding, change 
                    // new UTF8Encoding(...) to new UTF32Encoding().
                    using (var memoryStream = new MemoryStream())
                    {
                        formFile.CopyTo(memoryStream);
                        fileContents = memoryStream.ToArray();

                        // Check the content length in case the file's only
                        // content was a BOM and the content is actually
                        // empty after removing the BOM.
                        if (fileContents.Length > 0)
                        {
                            return fileContents;
                        }
                        else
                        {
                            modelState.AddModelError(formFile.Name,
                                                     $"The file ({fileName}) is empty.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    modelState.AddModelError(formFile.Name,
                                             $"The file ({fileName}) upload failed. " +
                                             $"Please contact the Help Desk for support. Error: {ex.Message}");
                    // Log the exception
                }
            }

            return new byte[0];
        }
    }
}
