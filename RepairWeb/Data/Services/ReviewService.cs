using RepairWeb.Data.Entities;

namespace RepairWeb.Data.Services
{
    public class ReviewService
    {
        private ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateReview()
        {

        }

        public async Task<List<Review>> GetReviews()
        {
            throw new NotImplementedException();
        }
    }
}
