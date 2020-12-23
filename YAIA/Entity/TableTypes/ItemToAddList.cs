namespace Entity.TableTypes
{
    public class ItemToAddList
    {
        public string Sku { get; set; }
        public int Units { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }

        public ItemToAddList(string sku, int units, float discount, string description)
        {
            Sku = sku;
            Units = units;
            Discount = discount;
            Description = description;
        }
    }
}