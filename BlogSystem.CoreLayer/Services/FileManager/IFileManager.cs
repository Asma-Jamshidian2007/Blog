using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Blog_System.CoreLayer.Services.FileManager
{
    public interface IFileManager
    {
        string SaveFile(IFormFile file, string savePath);
    }
}
