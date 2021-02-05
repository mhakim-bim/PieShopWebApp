using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetAllFeedback();

        Feedback GetFeedbackById(int id);

        Feedback AddNewFeedback(Feedback newFeedback);

    }
}
