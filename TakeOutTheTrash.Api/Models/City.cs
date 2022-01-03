using System.Collections.Generic;

namespace TakeOutTheTrash.Api.Models
{
    public class City
    {
        public City()
        {
            Rules = new List<Rule>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Rating { get; set; }

        public CityPrefecture Prefecture { get; set; }

        public List<Rule> Rules { get; set; }
    }
}
