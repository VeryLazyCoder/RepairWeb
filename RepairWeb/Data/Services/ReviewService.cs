using Microsoft.EntityFrameworkCore;
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

        public async Task CreateReview(Request request, int rating, string comment)
        {
            var review = new Review()
            {
                Comment = comment,
                Rating = rating,
                Executor = request.Executor,
                Request = request,
                ReviewDate = DateOnly.FromDateTime(DateTime.Now),
            };

            await _context.Reviews
                .AddAsync(review);

            request.Executor.Reviews.Add(review);
            request.Executor.AverageRating = (float)request.Executor.Reviews
                .Average(r => r.Rating);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Review>> GetLatestReviews(int count)
        {
            return await _context.Reviews
                .OrderByDescending(r => r.ReviewDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Review>> GetExecutorsReview(string id)
        {
            return await _context.Reviews
                .Include(r => r.Executor)
                .Include(r => r.Request)
                .Where(rev => rev.ExecutorId == id)
                .ToListAsync();
        }
    }
}
