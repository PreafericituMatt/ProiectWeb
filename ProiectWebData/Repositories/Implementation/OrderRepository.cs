using IProiectWebData.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using ProiectWeb.Entities;

namespace ProiectWebData.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dbContext;

        public OrderRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<Order>> PlaceOrder(Order order, int userId)
        {
            var response = new ServiceResponse<Order>();

            var shoppingCartId = await GetShoppingCartId(userId);

            if (shoppingCartId == 0)
            {
                response.Success = false;
                response.Message = "Shopping cart does not exist";

                return response;
            }

            var orderId = await CreateOrder(userId);

            order.OrderDetails.OrderId = orderId;
            order.OrderId = orderId;

            var currentCart = await _dbContext.ShoppingCart.FirstOrDefaultAsync(c => c.UserId == userId);
            var currentCartItems = await _dbContext.ShoppingCartItems.FirstOrDefaultAsync(c => c.ShoppingCartId == shoppingCartId);
            var books = await _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == shoppingCartId).Select(b => new { b.BookId, b.Quantity, b.FinalAmount }).ToListAsync();

            var orderItems = new List<OrderItems>();
            foreach (var book in books)
            {
                orderItems.Add(new OrderItems
                {
                    BooksId = book.BookId,
                    FinalAmount = book.FinalAmount,
                    Quantity = book.Quantity,
                    OrderId = orderId,
                });
            }

            await _dbContext.AddRangeAsync(orderItems);
            await _dbContext.AddAsync(order.OrderDetails);
            _dbContext.ShoppingCart.Remove(currentCart);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Order.Include(o => o.OrderItems)
               .Include(o => o.OrderDetails).ThenInclude(od => od.BillingAddress)
               .Include(o => o.OrderDetails).ThenInclude(od => od.DeliveryAddress).FirstOrDefaultAsync(c => c.OrderId == orderId);

            response.Data = result;

            return response;
        }

        public async Task<ServiceResponse<List<Order>>> GetUserOrders(int userId)
        {
            return new ServiceResponse<List<Order>>
            {
                Data = await _dbContext.Order.Where(o => o.UserId == userId).Include(o => o.OrderItems)
               .Include(o => o.OrderDetails).ThenInclude(od => od.BillingAddress)
               .Include(o => o.OrderDetails).ThenInclude(od => od.DeliveryAddress).ToListAsync()
            };
        }

        public async Task<ServiceResponse<Order>> UpdateOrder(Order updatedOrder, int userId, int orderId)
        {
            var response = new ServiceResponse<Order>();

            var oldOrder = await _dbContext.Order.Where(o => o.UserId == userId && o.OrderId == orderId).Include(o => o.OrderItems)
               .Include(o => o.OrderDetails).ThenInclude(od => od.BillingAddress)
               .Include(o => o.OrderDetails).ThenInclude(od => od.DeliveryAddress).FirstOrDefaultAsync();

            var userEmail = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (oldOrder == null)
            {
                response.Success = false;
                response.Message = "Order doesn't exist";
            }
            else
            {
                oldOrder.OrderDetails = updatedOrder.OrderDetails;
                await _dbContext.SaveChangesAsync();

                response.Data = oldOrder;
            }

            return response;
        }

        private async Task<int> GetShoppingCartId(int id)
        {
            var data = await _dbContext.ShoppingCart.FirstOrDefaultAsync(c => c.UserId == id);

            if (data == null)
            {
                return 0;
            }

            return data.ShoppingCartId;
        }

        private async Task<int> CreateOrder(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var shoppingCart = await _dbContext.ShoppingCart.FirstOrDefaultAsync(c => c.UserId == userId);
            var finalAmount = _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == shoppingCart.ShoppingCartId).Select(c => c.FinalAmount).Sum();

            var order = new Order
            {
                UserEmail = user.Email,
                UserId = userId,
                TotalAmount = finalAmount,
                DeliveryStatus = "Order Placed"
            };

            var result = await _dbContext.Order.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return result.Entity.OrderId;
        }
    }
}
