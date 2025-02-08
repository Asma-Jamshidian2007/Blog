using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Blog_System.CoreLayer.Services.FileManager
{
    public interface IFileManager
    {
       public string SaveAndReturnFileName(IFormFile file, string savePath);
       public void DeleteImage(string fileName,string path);
    }
}
