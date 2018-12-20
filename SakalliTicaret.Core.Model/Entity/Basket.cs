namespace SakalliTicaret.Core.Model.Entity
{
    public class Basket:EntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string BasketKey { get; set; }
    }
}
