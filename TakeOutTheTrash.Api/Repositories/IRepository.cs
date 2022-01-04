using System.Collections.Generic;
using TakeOutTheTrash.Api.Models;

namespace TakeOutTheTrash.Api.Repositories
{
    public interface IRepository
    {
        List<Prefecture> GetAllPrefectures();

        City GetCityById(int id);
    }
}
