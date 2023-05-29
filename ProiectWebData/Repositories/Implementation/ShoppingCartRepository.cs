
using Microsoft.EntityFrameworkCore;
using ProiectWeb.Entities;
using ProiectWebData.Repositories.Interface;

namespace ProiectWebData.Repositories.Implementation
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DataContext _dbContext;
        private readonly ISqlCommands<ShoppingCart> _sqlCommands;
        private readonly ISqlCommands<ShoppingCartItems> _SCIsqlCommands;

        public ShoppingCartRepository(DataContext dbContext, ISqlCommands<ShoppingCart> sqlCommand, ISqlCommands<ShoppingCartItems> sCIsqlCommands)
        {
            _dbContext = dbContext;
            _sqlCommands = sqlCommand;
            _SCIsqlCommands = sCIsqlCommands;
        }

        public async Task<ServiceResponse<ShoppingCartItems>> AddToCart(int bookId, int quantity, float finalAmount, int userId)
        {
            var response = new ServiceResponse<ShoppingCartItems>();
            var id = await GetShoppingCartId(userId);

            var oldCart = _dbContext.ShoppingCartItems.Where(c => c.BookId == bookId && c.ShoppingCartId == id).FirstOrDefault();

            if (oldCart != null)
            {
                oldCart.Quantity = oldCart.Quantity + quantity;
                oldCart.FinalAmount = oldCart.FinalAmount + finalAmount;

                await _dbContext.SaveChangesAsync();

                return response;

            }

            var shoppingCart = new ShoppingCartItems
            {
                BookId = bookId,
                ShoppingCartId = id,
                FinalAmount = finalAmount,
                Quantity = quantity,
            };

            await _dbContext.ShoppingCartItems.AddAsync(shoppingCart);
            await _dbContext.SaveChangesAsync();

            response.Data = shoppingCart;

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteShoppingCartItem(int shoppingCartId, int userId)
        {
            var response = new ServiceResponse<bool>();
            var validate = await _dbContext.ShoppingCartItems.FindAsync(shoppingCartId);

            var data = _dbContext.ShoppingCartItems.FirstOrDefaultAsync(c => c.ShoppingCartItemsId == shoppingCartId);

            if (validate != null)
            {
                _dbContext.ShoppingCartItems.Remove(data.Result);
                await _dbContext.SaveChangesAsync();

                response.Message = "Deleted Succesfully";

                return response;
            }
            else
            {
                response.Message = "Not Found";
                response.Success = false;

                return response;
            }
        }

        public async Task<ServiceResponse<List<ShoppingCart>>> GetAllItems()
        {
            var query = "SELECT * FROM ShoppingCart ";

            return new ServiceResponse<List<ShoppingCart>>
            {
                Data = _sqlCommands.GetAll(query).ToList()
            };
        }

        public async Task<ServiceResponse<List<ShoppingCartItems>>> GetCurrentShoppingCart(int id)
        {

            var response = new ServiceResponse<List<ShoppingCartItems>>();

            var cartId = await _dbContext.ShoppingCart.Where(c => c.UserId == id).Select(x => x.ShoppingCartId).FirstOrDefaultAsync();
            
            if(cartId == 0)
            {
                response.Success = false;
                response.Message = "Shopping cart empty";

                return response;
            }

            var query = $"SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = {cartId}";

            response.Data = _SCIsqlCommands.GetAll(query).ToList();

            if(response.Data == null)
            {
                response.Success=false;
                return response;
            }

            return response;
        }

        public async Task<int> GetShoppingCartId(int id)
        {
            var data = await _dbContext.ShoppingCart.FirstOrDefaultAsync(c => c.UserId == id);

            if (data == null)
            {
                var newCart = new ShoppingCart()
                {
                    UserId = id,
                };
                var entity = await _dbContext.ShoppingCart.AddAsync(newCart);

                await _dbContext.SaveChangesAsync();
                return entity.Entity.ShoppingCartId;
            }

            return data.ShoppingCartId;
        }

        public bool ValidateBookId(int itemId)
        {
            var validateItemId = _dbContext.Items.Find(itemId);

            if (validateItemId == null)
            {
                return false;
            }
            return true;
        }
    }
}
