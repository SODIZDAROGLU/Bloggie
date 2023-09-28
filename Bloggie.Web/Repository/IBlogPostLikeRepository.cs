namespace Bloggie.Web.Repository
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);
    }
}
