namespace Entity.TableTypes
{
    public class ItemList
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }

        public ItemList(string sku, int quantity)
        {
            Sku = sku;
            Quantity = quantity;
        }
    }
}