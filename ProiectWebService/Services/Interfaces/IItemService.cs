using ProiectWebService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectWebService.Services.Interfaces
{
    public interface IItemService
    {
        Task<ServiceResponse<List<ItemsDto>>> GetAllItems();
    }
}
