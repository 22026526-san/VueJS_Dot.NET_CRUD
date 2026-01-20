using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Backend.Core.DTOs;

namespace Backend.Core.Interfaces
{
    public interface ICloudinaryService
    {
        // Upload file và trả về URL + PublicId
        Task<CloudinaryUploadResult> UploadImage(IFormFile file);

        // Xóa file dựa trên PublicId
        Task<CloudinaryDeleteResult> DeleteImage(string publicId);
    }
}