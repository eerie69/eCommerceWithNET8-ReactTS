using api.Models;
using DemoShop.Helpers;

namespace api.Interfaces
{
    public interface IReviewsRepository
    {
        Task<List<Reviews>> GetAllAsync(CommentQueryObject queryObject);
        Task<Reviews?> GetByIdAsync(int id);
        Task<Reviews> CreateAsync(Reviews reviewsModel);
        Task<Reviews?> UpdateAsync(int id, Reviews reviewsModel);
        Task<Reviews?> DeleteAsync(int id);
    }
}
