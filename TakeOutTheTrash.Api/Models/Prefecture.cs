using System.Collections.Generic;

namespace TakeOutTheTrash.Api.Models
{
    public class Prefecture
    {
        public Prefecture()
        {
            Cities = new List<City>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<City> Cities { get; set; }
    }
}
