namespace Bloggie.Web.Repository
{
    public interface IImageRepositary
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
