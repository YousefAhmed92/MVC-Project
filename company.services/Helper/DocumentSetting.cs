using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.services.Helper
{
    public class DocumentSetting
    {
        public static string UploadFile(IFormFile file , string FolderName)
        {
            //1. get folder path 
            //var FolderPath = @"C:\\Users\\youse\\source\\repos\\Company.Web\\Company.app\\wwwroot\\Files\\Images\\";
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            //2. get file name
            var FileName = $"{Guid.NewGuid()}-{file.FileName}";

            //3. combine step 1 + step 2
            var FilePath = Path.Combine(FolderPath, FileName);

            //4. file stream 
            using var FileStream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(FileStream);
            return FileName;
        }

    }
}
