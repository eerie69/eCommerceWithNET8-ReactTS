namespace api.Dtos.Reviews
{
    public class ReviewsDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public double Rating { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = string.Empty;
        public string? UserAvatar { get; set; } = string.Empty;
        public int? ProductId { get; set; }
    }
}
