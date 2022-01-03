using System.Collections.Generic;
using TakeOutTheTrash.Api.Models;

namespace TakeOutTheTrash.Api.Responses
{
    public class PrefecturesResponse
    {
        public PrefecturesResponse(List<Prefecture> prefectures)
        {
            Prefectures = prefectures;
        }

        public List<Prefecture> Prefectures { get; set; }
    }
}
