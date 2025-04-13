using api.Dtos.Reviews;
using api.Models;
using DemoShop.Models;

namespace api.Mappers
{
    public static class ReviewsMappers
    {
        public static ReviewsDto ToReviewsDto(this Reviews reviewsModel)
        {
            return new ReviewsDto
            {
                Id = reviewsModel.Id,
                Content = reviewsModel.Content,
                Rating = reviewsModel.Rating,
                CreatedOn = reviewsModel.CreatedOn,
                CreatedBy = reviewsModel.AppUser?.UserName ?? "Unknown",
                UserAvatar = reviewsModel.AppUser?.ProfileImageUrl ?? "Avatar",
                ProductId = reviewsModel.ProductId
            };
        }


        public static Reviews ToReviewsFromCreate(this CreateReviewsDto reviewsDto, int productId)
        {
            return new Reviews
            {
                Content = reviewsDto.Content,
                Rating = reviewsDto.Rating,
                ProductId = productId
            };
        }

        public static Reviews ToReviewsFromUpdate(this UpdateReviewsRequestDto reviewsDto, int productId)
        {
            return new Reviews
            {
                Content = reviewsDto.Content,
                Rating = reviewsDto.Rating,
                ProductId = productId
            };
        }
    }
}
