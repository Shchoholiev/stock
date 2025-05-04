namespace Stock.Core.Entities
{
    public class Item : EntityBase
    {
        public string Name { get; set; }

        public string Unit { get; set; }

        public double Price { get; set; }

        public int Count { get; set; }

        public DateTime LastAppeal { get; set; }
    }
}
