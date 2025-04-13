using GetProducts = DemoShop.Models.Product;

namespace api.Dtos.Reviews
{
    public class DetailsAndReviewsDto
    {
        public GetProducts? Products { get; set; }
        public CreateReviewsDto CreateComment { get; set; } = new CreateReviewsDto();
    }
}
