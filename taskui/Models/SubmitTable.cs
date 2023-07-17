namespace taskui.Models
{
    public class SubmitTable
    {
        public int HeaderId { get; set; }
        public string ReleaseNameTable { get; set; }
        public DateTime ReleaseDateTable { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int DetailId { get; set; }
        public short DetailTypeTable { get; set; }
        public string DetailNameTable { get; set; }
        public string DetailDescriptionTable { get; set; }
    }
    public class SubmitBody
    {
        public SubmitTable[] BodyData { get; set; }
    }
}
