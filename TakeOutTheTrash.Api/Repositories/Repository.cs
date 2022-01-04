using System.Collections.Generic;
using TakeOutTheTrash.Api.Models;

namespace TakeOutTheTrash.Api.Repositories
{
    public class Repository : IRepository
    {
        public Repository()
        {
        }

        public List<Prefecture> GetAllPrefectures() // default to empty collection
        {
            var stubbedPrefectures = new List<Prefecture>();

            return stubbedPrefectures;
        }
    }
}
