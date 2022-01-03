namespace TakeOutTheTrash.Api.Models
{
    public class Rule
    {
        public string Name { get; set; }

        #nullable enable
        public string? Description { get; set; }

        public string? Instructions { get; set; }

        public string? IrregularFrequency { get; set; }
    }
}
