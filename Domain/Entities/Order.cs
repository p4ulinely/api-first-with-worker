namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public Order(int itemCode, DateTime orderDate)
        {
            Id = Guid.NewGuid();
            ItemCode = itemCode;
            OrderDate = orderDate;
        }

        public Guid Id { get; protected set; }
        public int ItemCode { get; protected set; }
        public DateTime OrderDate { get; protected set; }

        public void SetId(Guid id)
            => Id = Id;
            
        public override void Validacoes()
        {
            throw new NotImplementedException();
        }
    }
}
