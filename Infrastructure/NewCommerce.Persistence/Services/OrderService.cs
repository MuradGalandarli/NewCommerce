using Microsoft.EntityFrameworkCore;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.DTOs.Order;
using NewCommerce.Application.Repositoryes;


namespace NewCommerce.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        private IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        public OrderService(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            string orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);

            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                OrderCode = orderCode,
                Description = createOrder.Description

            });
           await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                        .ThenInclude(b => b.User)
                        .Include(o => o.Basket)
                           .ThenInclude(b => b.BasketItems)
                           .ThenInclude(bi => bi.Product);

            var data = query.Skip(page * size).Take(size);
          
            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreateDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName
                }).ToListAsync()
            };
        }

        public async Task<SingleOrder> GetOrderId(string id)
        {
            var data = await _orderReadRepository.Table
                                  .Include(o => o.Basket)
                                      .ThenInclude(b => b.BasketItems)
                                          .ThenInclude(bi => bi.Product)
                                                  .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new()
            {
                Id = data.Id.ToString(),
                BasketItems = data.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address = data.Address,
                CreatedDate = data.CreateDate,
                Description = data.Description,
                OrderCode = data.OrderCode
            };
        }
    }
}
