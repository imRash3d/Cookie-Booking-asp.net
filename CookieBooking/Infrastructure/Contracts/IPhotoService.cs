using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Contracts
{
  public  interface IPhotoService
    {
        Task<ImageUploadResult> UploadPhoto(IFormFile file );
        Task<DeletionResult> DeletePhoto(string publicId);
    }
}
