using Microsoft.EntityFrameworkCore;
using ProiectWebData.Entities;
using ProiectWebData.Repositories.Interface;


namespace ProiectWebData.Repositories.Implementation
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly DataContext _dbContext;

        public ItemsRepository(DataContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ServiceResponse<List<Items>>> GetAllItems()
        {
            return new ServiceResponse<List<Items>>
            {
                Data = await _dbContext.Items.ToListAsync()
            };
        }
    }
}
