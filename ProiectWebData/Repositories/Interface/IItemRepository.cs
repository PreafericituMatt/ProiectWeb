using ProiectWebData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectWebData.Repositories.Interface
{
    public interface IItemRepository
    {
        public Task<ServiceResponse<List<Items>>> GetAllItems();
        public Task<ServiceResponse<List<Items>>> GetPopular();
        public Task<ServiceResponse<List<Items>>> GetPromoted();
    }
}
