namespace taskui.Models
{
    public class SubmitHeader
    {
        public string ReleaseName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public ICollection<Details> Details { get; set; }
    }
    public class Details
    {
        public short Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
