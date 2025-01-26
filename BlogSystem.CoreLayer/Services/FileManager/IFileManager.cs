using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.Services.FileManager
{
    public interface IFileManager
    {
        string SaveFile(IFormFile file, string savePath);
    }
    public class FileManager : IFileManager
    {
        public string SaveFile(IFormFile file, string savePath)
        {
            if (file == null)
                throw new Exception("File is null");
            var fileName= $"{Guid.NewGuid()}{file.FileName}";
            var folderPath=Path.Combine(Directory.GetCurrentDirectory(),savePath.Replace("/","\\"));
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
             var fulPath=Path.Combine(folderPath+fileName);
            using var stream = new FileStream(fulPath,FileMode.Create);
            file.CopyTo(stream);
            return fileName;
        }
    }
}
