using System.Collections.Generic;
using TakeOutTheTrash.Api.Models;

namespace TakeOutTheTrash.Api.Repositories
{
    public interface IRepository
    {
        List<Prefecture> GetAllPrefectures();

        List<City> GetAllCitiesByPrefectureId(int id);

        City GetCityById(int id);

        bool AddFeedbackSubmission(FeedbackSubmission feedbackSubmission);
    }
}
