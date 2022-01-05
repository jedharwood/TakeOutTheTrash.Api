using System;

namespace TakeOutTheTrash.Api.Models
{
    public class FeedbackSubmission
    {
        public int CityId { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ActionedDate { get; set; }

        public DateTime? RepliedDate { get; set; }

        #nullable enable
        public string? Email { get; set; }
    }
}
