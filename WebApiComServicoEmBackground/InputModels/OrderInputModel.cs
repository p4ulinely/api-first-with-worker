using Domain.Entities;

namespace InputModels
{
    public class OrderInputModel
    {
        public Guid Id { get; protected set; }
        public DateTime OrderDate { get; set; }
        public int ItemCode { get; set; }

        public static implicit operator Order(OrderInputModel model)
        {
            var entity = new Order(model.ItemCode, model.OrderDate);

            entity.SetId(model.Id);

            return entity;
        }

        public static implicit operator OrderInputModel(Order entity)
        {
            return new OrderInputModel
            {
                Id = entity.Id,
                OrderDate = entity.OrderDate,
                ItemCode = entity.ItemCode,
            };
        }
    }
}