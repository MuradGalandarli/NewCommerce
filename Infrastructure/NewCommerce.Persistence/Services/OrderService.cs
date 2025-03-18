using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.DTOs.Order;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Application.Repositoryes.CompletedOrder;
using NewCommerce.Domain.Entitys;
using NewCommerce.Persistence.Repositoryes.CompletedOrder;


namespace NewCommerce.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        private IOrderReadRepository _orderReadRepository;
        private ICompletedOrderWriteRepository _completedOrderWriteRepository;
        private ICompletedOrderReadRepository _completedOrderReadRepository;



        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository = null)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
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

            var data2 = from order in data
                        join completedOrder in _completedOrderReadRepository.Table
                           on order.Id equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            id = order.Id,
                            CreatedDate = order.CreateDate,
                            Ordercode = order.OrderCode,
                            Baskets = order.Basket,
                            Completed = _co != null ? true : false
                        };
            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data2.Select(o => new
                {
                    Id = o.id,
                    CreateDate = o.CreatedDate,
                    OrderCode = o.Ordercode,
                    TotalPrice = o.Baskets.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    Username = o.Baskets.User.UserName,
                    o.Completed
                }).ToListAsync()
            };
        }

        public async Task<SingleOrder> GetOrderId(string id)
        {
            var data = _orderReadRepository.Table
                                  .Include(o => o.Basket)
                                      .ThenInclude(b => b.BasketItems)
                                          .ThenInclude(bi => bi.Product);
                                                   

            var data2 = await (from order in data
                               join completedOrder in _completedOrderReadRepository.Table
                                    on order.Id equals completedOrder.OrderId into co
                               from _co in co.DefaultIfEmpty()
                               select new
                               {
                                   Id = order.Id,
                                   CreatedDate = order.CreateDate,
                                   OrderCode = order.OrderCode,
                                   Basket = order.Basket,
                                   Completed = _co != null ? true : false,
                                   Address = order.Address,
                                   Description = order.Description
                               }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new()
            {
                Id = data2.Id.ToString(),
                BasketItems = data2.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address = data2.Address,
                CreatedDate = data2.CreatedDate,
                Description = data2.Description,
                OrderCode = data2.OrderCode ,
                Completed = data2.Completed ,
            };
        }

        public async Task CompletedOrderAsync(string id)
        {
           Order order = await _orderReadRepository.GetById(id);
            if(order != null)
            {
               await _completedOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id) });
            }
        }
    }
}
