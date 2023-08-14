using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repository
{
    //
    public interface ITagRepository
    {
        //Definition of methods
        Task<IEnumerable<Tag>> GetAllAsync();
        //Tag? = nullable
        Task<Tag?> GetAsync(Guid id);
        //Add async need entire tag object
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);
    }
}
