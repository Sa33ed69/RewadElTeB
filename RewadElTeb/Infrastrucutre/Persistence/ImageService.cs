using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Infrastructure.Persistence
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
                throw new ArgumentException("Invalid image.");

            const long maxFileSize = 5 * 1024 * 1024;

            if (image.Length > maxFileSize)
                throw new ArgumentException(
                    "Image size cannot exceed 5 MB.");

            var folderPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "doctors");

            Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}.jpg";

            var filePath = Path.Combine(
                folderPath,
                fileName);

            using var inputStream = image.OpenReadStream();

            using var imageSharp =
                await Image.LoadAsync(inputStream);

            imageSharp.Mutate(x =>
                x.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new Size(1200, 1200)
                }));

            var encoder = new JpegEncoder
            {
                Quality = 80
            };

            await imageSharp.SaveAsync(
                filePath,
                encoder);

            return $"/images/doctors/{fileName}";
        }

        public Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return Task.CompletedTask;

            var fileName = Path.GetFileName(imageUrl);

            var filePath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "doctors",
                fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }
    }
}