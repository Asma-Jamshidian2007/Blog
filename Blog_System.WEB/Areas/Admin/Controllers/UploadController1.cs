using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Blog_System.WEB.Areas.Admin.Controllers
{
    [Route("Admin/Post")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile upload)
        {
            if (upload == null || upload.Length == 0)
                return BadRequest(new { error = "No file uploaded" });

            var uploadsFolder = Path.Combine(_env.WebRootPath, "images/posts/Content");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(upload.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await upload.CopyToAsync(stream);
            }

            var imageUrl = $"/images/posts/Content/{fileName}";

            return Ok(new { url = imageUrl });
        }
    }
}
