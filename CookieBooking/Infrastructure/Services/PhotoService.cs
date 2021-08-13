using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CookieBooking.Helpers;
using CookieBooking.Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.Apikey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
        }
        public Task<DeletionResult> DeletePhoto(string publicId)
        {
            throw new NotImplementedException();
        }

        public async Task<ImageUploadResult> UploadPhoto(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }
    }
}
