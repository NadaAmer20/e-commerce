namespace first.Models
{
    public class OrderDetail
    {
       

        public int Id { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }//null
        //eager loading 
        public OrderDetail() { }
        public OrderDetail(int OrderId,int ProductId)
        {
            this.OrderId = OrderId;
            this.ProductId = ProductId;
            Quantity = 0;
          
        }
    }
}
