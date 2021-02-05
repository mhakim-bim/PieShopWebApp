using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class FeedbackRepository :IFeedbackRepository
    {
        private AppDbContext _context;

        public FeedbackRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IEnumerable<Feedback> GetAllFeedback()
        {
             return _context.Feedbacks;
        }

        public Feedback GetFeedbackById(int id)
        {
            return _context.Feedbacks.FirstOrDefault(f => f.FeedbackId == id);
        }

        public Feedback AddNewFeedback(Feedback feedback)
        {
            if (feedback != null)
            {
                _context.Add(feedback);
                _context.SaveChanges();
                return feedback;
            }

            return null;
        }
    }
}
