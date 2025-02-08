using Microsoft.AspNetCore.Http;

namespace Blog_System.CoreLayer.Services.FileManager
{
    public class FileManager : IFileManager
    {
        public string SaveAndReturnFileName(IFormFile file, string savePath)
        {
            if (file == null)
                throw new Exception("The file is not valid.");

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fullPath = Path.Combine(folderPath, fileName);

            try
            {
                using var stream = new FileStream(fullPath, FileMode.Create);
                file.CopyTo(stream);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving file: {ex.Message}");
            }

            return fileName;
        }

       public void DeleteImage(string fileName, string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),path,fileName);
            if (File.Exists(filePath)) { 
               File.Delete(filePath);
            }

        }
    }
}
