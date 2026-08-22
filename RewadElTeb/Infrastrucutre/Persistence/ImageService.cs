using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistence
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(
            IFormFile image,
            string folder)
        {
            // wwwroot/images/doctors
            // أو
            // wwwroot/images/Departments

            var folderPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                folder);

            // لو الفولدر مش موجود هيعمله
            Directory.CreateDirectory(folderPath);

            // استخراج امتداد الصورة
            var extension =
                Path.GetExtension(image.FileName);

            // اسم Unique للصورة
            var fileName =
                $"{Guid.NewGuid()}{extension}";

            // المسار الكامل للصورة
            var filePath =
                Path.Combine(
                    folderPath,
                    fileName);

            // إنشاء الملف ونسخ الصورة داخله
            using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await image.CopyToAsync(stream);

            // القيمة اللي هتتحفظ في Database
            return $"/images/{folder}/{fileName}";
        }

        public Task DeleteImageAsync(
            string imageUrl,
            string folder)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return Task.CompletedTask;

            // نجيب اسم الملف فقط
            var fileName =
                Path.GetFileName(imageUrl);

            // نبني المسار الكامل
            var filePath =
                Path.Combine(
                    _environment.WebRootPath,
                    "images",
                    folder,
                    fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }
    }
}