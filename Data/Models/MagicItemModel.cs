namespace ViciousMockeryGenerator.Data.Models
{
    public class MagicItemModel
    {
        public int Id { get; set; }
        public string Table { get; set; }
        public string MagicItem { get; set; }
        public Range D100 { get; set; }
    }
}
