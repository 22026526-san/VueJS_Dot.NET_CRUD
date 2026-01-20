namespace Backend.Core.DTOs
{
    public class CloudinaryUploadResult
    {
        public required string Url { get; set; }
        public required string PublicId { get; set; }
    }

    public class CloudinaryDeleteResult
    {
        public required string Result { get; set; }
    }
}