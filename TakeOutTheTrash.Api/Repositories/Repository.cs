using System.Collections.Generic;
using TakeOutTheTrash.Api.Models;

namespace TakeOutTheTrash.Api.Repositories
{
    public class Repository : IRepository
    {
        public Repository()
        {
        }

        public List<Prefecture> GetAllPrefectures() // default to empty collection?
        {
            var stubbedPrefectures = new List<Prefecture>();

            return stubbedPrefectures;
        }

        public List<City> GetAllCitiesByPrefectureId(int id) // default to empty collection?
        {
            var stubbedCities = new List<City>();

            return stubbedCities;
        }

        public City GetCityById(int id)
        {
            var stubbedCity = new City();

            return stubbedCity;
        }

        public bool AddFeedbackSubmission(FeedbackSubmission feedbackSubmission)
        {
            var stubbedDbInsertSuccess = true;

            return stubbedDbInsertSuccess;
        }
    }
}
