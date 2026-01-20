using Backend.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;

        public UploadController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Vui lòng chọn file ảnh");

            var result = await _cloudinaryService.UploadImage(file);

            return Ok(result);
        }

        [HttpDelete("{publicId}")]
        public async Task<IActionResult> Delete(string publicId)
        {
            var result = await _cloudinaryService.DeleteImage(publicId);
            return Ok(result);
        }
    }
}