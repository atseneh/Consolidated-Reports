namespace Consolidated_App.Models
{
    public class Select2Result
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool selected { get; set; }
        public bool disabled { get; set; }
        public string text2 { get; set; }
        public string text3 { get; set; }
        public int text4 { get; set; }
        public string actualid { get; set; }
        public Decimal text5 { get; set; }
        public List<Select2Result> children { get; set; }
    }
}
