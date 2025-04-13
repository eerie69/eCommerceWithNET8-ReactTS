using api.Interfaces;
using api.Models;
using DemoShop.Data;
using DemoShop.Helpers;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Reviews> CreateAsync(Reviews reviewsModel)
        {
            await _context.Reviews.AddAsync(reviewsModel);
            await _context.SaveChangesAsync();
            return reviewsModel;
        }

        public async Task<Reviews?> DeleteAsync(int id)
        {
            //Проверка есть ли наш айди или нет.
            var reviewsModel = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (reviewsModel == null)
            {
                return null;
            }
            //мы берем наш коммертарии(comment) и удаляем(Remove) тот самый(айди)
            //А так же метод Remove нельзя ассинхронно исопльзовать.
            _context.Reviews.Remove(reviewsModel);
            await _context.SaveChangesAsync();
            return reviewsModel;
        }

        public async Task<List<Reviews>> GetAllAsync(CommentQueryObject queryObject)
        {
            var reviews = _context.Reviews
                .Include(a => a.AppUser)
                .Include(r => r.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.Title))
            {
                reviews = reviews.Where(s => s.Product != null && s.Product.Title == queryObject.Title);
            };
            if (queryObject.IsDecsending == true)
            {
                reviews = reviews.OrderByDescending(s => s.CreatedOn);
            }
            return await reviews.ToListAsync();
        }

        public async Task<Reviews?> GetByIdAsync(int id)
        {
            return await _context.Reviews.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Reviews?> UpdateAsync(int id, Reviews reviewsModel)
        {
            var existingReviews = await _context.Reviews.FindAsync(id);

            if (existingReviews == null)
            {
                return null;
            }


            existingReviews.Content = reviewsModel.Content;
            existingReviews.Rating = reviewsModel.Rating;

            await _context.SaveChangesAsync();

            return existingReviews;
        }
    }
}
